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
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Console.WriteLine(responseText);
            Console.WriteLine("Notification success");
        }

        private static Boolean interesadoEnEvento(float lat_sen, float long_sen, float cen_lat, float cen_long, int radio)
        {
            return true;
        }

        private static void NotificarUsuarios(int eventoid, int sensorid, string nevento, string nmensaje)
        {
            //Busco coordenadas de sensor
            var postString = "{ id:" + sensorid+ " }";
            byte[] data = UTF8Encoding.UTF8.GetBytes(postString);
            HttpWebRequest request = WebRequest.Create("http://proyectotsi1.azurewebsites.net/api/Sensores/") as HttpWebRequest;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader read = new StreamReader(response.GetResponseStream());
            string body = read.ReadToEnd();
            dynamic misEventos = JsonConvert.DeserializeObject(body);
            Sensores sens = (Sensores)misEventos;
            float latitud_sensor = sens.Latitud;
            float longitud_sensor = sens.Longitud;

            //busco subscripciones a dicho evento (todos los usuarios subscriptos)
            postString = "{ id:" + sensorid + " }";
            data = UTF8Encoding.UTF8.GetBytes(postString);
            request = WebRequest.Create("http://proyectotsi1.azurewebsites.net/api/Subscripciones/subsByEvent") as HttpWebRequest;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            response = (HttpWebResponse)request.GetResponse();
            read = new StreamReader(response.GetResponseStream());
            body = read.ReadToEnd();
            dynamic misSubscripciones = JsonConvert.DeserializeObject(body);

            foreach (EventoSubscripcion ev in misSubscripciones)
            {
                //Si esta en el area marcada por el usuario
                if (interesadoEnEvento(latitud_sensor,longitud_sensor,ev.CentroLatitud,ev.CentroLongitud,ev.Radio))
                {
                    //Obtengo el usuario
                    postString = "{ id:" + sensorid + " }";
                    data = UTF8Encoding.UTF8.GetBytes(postString);
                    HttpWebRequest requestUsr = WebRequest.Create("http://proyectotsi1.azurewebsites.net/api/Usuarios/") as HttpWebRequest;
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    HttpWebResponse responseUsr = (HttpWebResponse)request.GetResponse();
                    StreamReader readUsr = new StreamReader(response.GetResponseStream());
                    string bodyUsr = readUsr.ReadToEnd();
                    dynamic miUsr = JsonConvert.DeserializeObject(bodyUsr);
                    Usuario usr = (Usuario)miUsr;

                    //Notifico
                    Notificar(usr.ChannelName, nevento, nmensaje);
                    }
            }

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
            var infotipo = "{ id:"+tipo_dato+"}";
            byte[] datatipo = UTF8Encoding.UTF8.GetBytes(infotipo);
            HttpWebRequest requestSensor = WebRequest.Create("http://mocksensores20170513030338.azurewebsites.net/api/sensores") as HttpWebRequest;
            string responseTxt;
            requestSensor.Method = "POST";
            requestSensor.ContentType = "application/x-www-form-urlencoded";
            requestSensor.ContentLength = datatipo.Length;
            using (var stream = requestSensor.GetRequestStream())
            {
                stream.Write(datatipo, 0, datatipo.Length);
            }
            HttpWebResponse responseSensor = (HttpWebResponse)requestSensor.GetResponse();
            responseTxt = new StreamReader(responseSensor.GetResponseStream()).ReadToEnd();
            // Contiene texto. Falta parsear.
            string id_sensor = responseTxt;
            Console.WriteLine("El id del sensor es: " + id_sensor);


            //Comienzo a consultar el sensor creado
            while (true)
            {
                //Consumo de sensor
                var info = "{ id:" + "5" + "}";
                byte[] data = UTF8Encoding.UTF8.GetBytes(info);
                HttpWebRequest request = WebRequest.Create("http://mocksensores20170513030338.azurewebsites.net/api/sensores") as HttpWebRequest;
                //request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream2 = request.GetRequestStream())
                {
                    stream2.Write(data, 0, data.Length);
                }
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

                //Diferencio por el tipo de dato del sensor
                if (tipo_dato == 0)
                {
                    //Itero en los eventos existentes para ver cuales se cumplen
                    foreach (var obj in misEventos)
                    {
                        if (tipo == obj.TipoDato)
                        {
                            //Si se cumple el evento notifico
                            if (responseText == obj.ValorLimite)
                            {
                                Console.WriteLine("Se cumple el evento " + obj.Nombre);
                                NotificarUsuarios(obj.EventoId, Int32.Parse(id_sensor), "Notificacion-" + obj.Nombre, "Mensaje de notificacion " + obj.Nombre);
                            }
                        }
                    }
                }
                else if (tipo_dato == 1)
                {
                    //Valor leido
                    int val_leido = Int32.Parse(responseText);
                    Console.WriteLine("Valor leido en entero es: " + val_leido);

                    //Itero en los eventos existentes para ver cuales se cumplen
                    foreach (var obj in misEventos)
                    {
                        if (tipo == obj.TipoDato)
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
                                NotificarUsuarios(obj.EventoId, Int32.Parse(id_sensor), "Notificacion-" + obj.Nombre, "Mensaje de notificacion " + obj.Nombre);
                            }
                        }
                    }
                }
                else
                {
                    //Itero en los eventos existentes para ver cuales se cumplen
                    foreach (var obj in misEventos)
                    {
                        if (tipo == obj.TipoDato)
                        {
                            //Si se cumple el evento notifico
                            if (responseText == obj.ValorLimite)
                            {
                                Console.WriteLine("Se cumple el evento " + obj.Nombre);
                                NotificarUsuarios(obj.EventoId, Int32.Parse(id_sensor), "Notificacion-" + obj.Nombre, "Mensaje de notificacion " + obj.Nombre);
                            }
                        }
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
