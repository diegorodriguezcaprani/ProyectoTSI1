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
        private static void Notificar(string nchannel, string nevento, string nmensaje) {
            var postString = "{ channel:"+nchannel+", evento:"+nevento+", mensaje:"+nmensaje+"}";
            byte[] data = UTF8Encoding.UTF8.GetBytes(postString);
            HttpWebRequest request = WebRequest.Create("http://proyectotsi1.azurewebsites.net/api/Pusher") as HttpWebRequest;
            string responseText;
            //var data = Encoding.ASCII.GetBytes(responseText);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
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

        private static void ProcedimientoHilo(object param)
        {
            ParamTask parametro = (ParamTask)param;
            int time = parametro.Frecuencia;
            string tipo = parametro.Tipo_dato;
            int tipo_dato;
            try
            {
                if ((tipo == "string") || (tipo == "String"))
                {
                    tipo_dato = 0;
                }
                else if ((tipo == "int") || (tipo == "Int") || (tipo == "float") || (tipo == "Float"))
                {
                    tipo_dato = 1;
                }
                else if ((tipo == "img") || (tipo == "Img"))
                {
                    tipo_dato = 2;
                }
                else
                {
                    tipo_dato = -1;
                    throw new ArgumentException("Tipo de Dato invalido");
                }
            }
            catch (ArgumentException e){
                throw;
            }

            //Me creo sensor de dicho tipo
            var postString = "{ id:"+tipo_dato+"}";
            byte[] data = UTF8Encoding.UTF8.GetBytes(postString);
            HttpWebRequest requestSensor = WebRequest.Create("http://mocksensores20170513030338.azurewebsites.net/api/sensores") as HttpWebRequest;
            string responseTxt;
            requestSensor.Method = "POST";
            requestSensor.ContentType = "application/x-www-form-urlencoded";
            requestSensor.ContentLength = data.Length;
            var stream = requestSensor.GetRequestStream();
            stream.Write(data, 0, data.Length);
            HttpWebResponse responseSensor = (HttpWebResponse)requestSensor.GetResponse();
            responseTxt = new StreamReader(responseSensor.GetResponseStream()).ReadToEnd();
            string id_sensor = responseTxt;
            Console.WriteLine("El id del sensor es: " + id_sensor);

            //Comienzo a consultar el sensor creado
            while (true)
            {
                //Consumo de sensor
                HttpWebRequest request = WebRequest.Create("http://mocksensores20170513030338.azurewebsites.net/api/sensores"+"/"+id_sensor) as HttpWebRequest;
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
                        Notificar("my-channel","Notificacion-Evento","Mensaje de notificacion.");
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
                string tipo_dato;
                int frec;
                ParamTask param;
                foreach (var tipo in misTipos)
                {
                    if (tipo.Id == obj.Tipo)
                    {
                        tipo_dato = (string)tipo.TipoDato;
                        frec = (int)tipo.Frecuencia;
                        Console.WriteLine("Frecuencia: " + frec);
                        param = new ParamTask()
                        {
                            Frecuencia = frec,
                            Tipo_dato = tipo_dato
                        };
                        Task task = new Task(ProcedimientoHilo, param);
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
