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

namespace Consumer
{
    class Consumir
    {
        private static void ProcedimientoHilo()
        {
            HttpWebRequest request = WebRequest.Create("http://mocksensores20170513030338.azurewebsites.net/api/sensores") as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var encoding = ASCIIEncoding.ASCII;
            string responseText;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                responseText = reader.ReadToEnd();
                Console.WriteLine(responseText);
            }

            if (Int32.Parse(responseText) > 30)
            {
                request = WebRequest.Create("http://proyectotsi1.azurewebsites.net/api/Pusher") as HttpWebRequest;
                var data = Encoding.ASCII.GetBytes(responseText);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                    /*responseText = readerPusher.ReadToEnd();
                    Console.WriteLine(responseText);*/
                }
                response = (HttpWebResponse)request.GetResponse();
                responseText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Console.WriteLine(responseText);
            }
           

            /*//Consumo del ws del sensor
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://mocksensores20170513030338.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));

            HttpResponseMessage response = client.GetAsync("api/sensores").Result;
            if (response.Content != null)
            {
                Console.WriteLine(response.Content.GetType());
                Console.WriteLine(response.Content.ToString());

                if (temp > 30)
                {
                    //Llamo al servicio de pusher
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://proyectotsi1.azurewebsites.net/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));

                    HttpResponseMessage response = client.GetAsync("api/Pusher").Result;

                }

            }
            else
            {
                Console.WriteLine("No se pudo obtener valores de dicho sensor");
            }


            client.Dispose();*/
            Thread.Sleep(5000);
           
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Task task = new Task(ProcedimientoHilo);
                task.Start();
                task.Wait();
                task.Dispose();
            }
        }
    }
}
