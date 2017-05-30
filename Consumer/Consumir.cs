using Newtonsoft.Json;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Consumer
{
    class Consumir
    {
        private static void Notificar() {
            HttpWebRequest request = WebRequest.Create("http://proyectotsi1.azurewebsites.net/api/Pusher") as HttpWebRequest;
            string responseText;
            //var data = Encoding.ASCII.GetBytes(responseText);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                //stream.Write(data, 0, data.Length);
                /*responseText = readerPusher.ReadToEnd();
                Console.WriteLine(responseText);*/
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Console.WriteLine(responseText);
            Console.WriteLine("Notification success");
        }

        private static void ProcedimientoHilo(object time)
        {
            while (true)
            {
                //Consumo de sensor
                HttpWebRequest request = WebRequest.Create("http://mocksensores20170513030338.azurewebsites.net/api/sensores") as HttpWebRequest;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var encoding = ASCIIEncoding.UTF8;
                string responseText;
                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                {
                    responseText = reader.ReadToEnd();
                    Console.WriteLine("Valor leido del sensor: " + responseText);
                }

                //Leo todos los eventos posibles
                request = WebRequest.Create("http://proyectotsi1.azurewebsites.net/api/Eventos") as HttpWebRequest;
                response = (HttpWebResponse)request.GetResponse();
                StreamReader read = new StreamReader(response.GetResponseStream());
                
                string body = read.ReadToEnd();
                dynamic misEventos = JsonConvert.DeserializeObject(body);

                //Valor leido
                int val_leido = Int32.Parse(responseText);
                Console.WriteLine("Valor leido en entero es: " + val_leido);

                //Itero en los eventos existentes para ver cuales se cumplen
                foreach (var obj in misEventos)
                {
                    //Valor limite del evento
                    Console.WriteLine("VALOR LIMITE ANTES: " + obj.ValorLimite);
                    bool es = int.TryParse((string)obj.ValorLimite, out int val);
                    Console.WriteLine("VALOR LIMITE DESP: " + val);

                    //Compruebo el evento de acuerdo al operador
                    bool cumple_evento = false;
                    switch (obj.Operador)
                    {
                        case "Max":
                            cumple_evento = val > val_leido;
                            break;
                        case "Min":
                            cumple_evento = val < val_leido;
                            break;
                        case "Igual":
                            cumple_evento = val == val_leido;
                            break;
                    }


                    //Si se cumple el evento notifico
                    if (cumple_evento)
                    {
                        Console.WriteLine("Se cumple el evento " + obj.Nombre);
                        Notificar();
                    }

                }

                Thread.Sleep((int)time);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Inicie main...");
            //Busco todos los tipos
            HttpWebRequest request = WebRequest.Create("http://proyectotsi1.azurewebsites.net/api/Tipos") as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var encoding = ASCIIEncoding.ASCII;
            StreamReader read = new StreamReader(response.GetResponseStream());
            string body = read.ReadToEnd();
            dynamic misTipos = JsonConvert.DeserializeObject(body);

            Console.WriteLine("Obtuve los tipos...");

            //Busco todos los sensores
            request = WebRequest.Create("http://proyectotsi1.azurewebsites.net/api/Sensores") as HttpWebRequest;
            response = (HttpWebResponse)request.GetResponse();
            encoding = ASCIIEncoding.ASCII;
            read = new StreamReader(response.GetResponseStream());
            body = read.ReadToEnd();
            dynamic misSensores = JsonConvert.DeserializeObject(body);

            Console.WriteLine("Obtuve los sensores...");

            foreach (var obj in misSensores)
            {
                int frec;
                foreach (var tipo in misTipos)
                {
                    if (tipo.Id == obj.Tipo)
                    {
                        frec = (int)tipo.Frecuencia;
                        Console.WriteLine("Frecuencia: " + frec);
                        Task task = new Task(ProcedimientoHilo, frec);
                        task.Start();
                    }
                }
            }

            Console.WriteLine("Cree los tasks...");

            while (true)
            {
                /*
                Task task = new Task(ProcedimientoHilo,200);
                task.Start();
                task.Wait();
                task.Dispose();
                */
            }
        }
    }
}
