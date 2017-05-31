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
    public class RelacionesEventosController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllRelacionesEventos()
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetAllRelacionesEventos());
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpPost]
        public HttpResponseMessage AddRelacionEventos(EventoRelacion releven)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.AddRelacionEvento(releven);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteRelacionEventos(int idev1, int idev2)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.DeleteRelacionEvento(idev1,idev2);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
