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
    public class SubscripcionesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllSubscripciones()
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetAllSubscripciones());
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpGet]
        public HttpResponseMessage GetSubscripcion(int id)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetSubscripcion(id));
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpPost]
        public HttpResponseMessage AddSubscripcion(Subscripcion sub)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.AddSubscripcion(sub);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteSubscripcion(int id)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.DeleteSubscripcion(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
