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
    public class HistoricoEventosController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllHistoricoEventos()
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetAllHistoricoEventos());
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpGet]
        public HttpResponseMessage GetHistoricoEvento(int id)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetHistoricoEvento(id));
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpPost]
        public HttpResponseMessage AddHistoricoEvento(HistoricoEvento even)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.AddHistoricoEvento(even);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
