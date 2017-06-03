using BusinessLogicLayer;
using DataAccessLayer;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EventosController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllEventos()
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetAllEventos());
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpGet]
        public HttpResponseMessage GetEvento(int id)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetEvento(id));
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }
        
        [HttpPost]
        public HttpResponseMessage AddEvento(Evento even)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.AddEvento(even);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [ActionName("AddEventoComplejo")]
        public HttpResponseMessage AddEventoComplejo(EventoComplejo even)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.AddEventoComplejo(even);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteEvento(int id)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.DeleteEvento(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
