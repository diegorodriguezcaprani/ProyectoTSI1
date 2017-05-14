using System;
using System.Collections.Generic;
using System.Linq;
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
            //Consumo del ws del sensor
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://mocksensores20170513030338.azurewebsites.net/");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));

            HttpResponseMessage response = client.GetAsync("api/sensores").Result;
            if (response.Content != null)
            {
                Console.WriteLine(response.Content.GetType());
                Console.WriteLine(response.Content.ToString());

            }
            else
            {
                Console.WriteLine("No se pudo obtener valores de dicho sensor");
            }

            //Chequeo si no se cumple algun evento, y en caso de cumplirse notifico mediante pusher (usando ws)


            client.Dispose();
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
