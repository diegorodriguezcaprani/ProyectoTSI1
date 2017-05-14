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
    public class SensoresController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllSensores()
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetAllSensores());
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpGet]
        public HttpResponseMessage GetSensor(int id)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetSensor(id));
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json) };
        }

        [HttpPost]
        public HttpResponseMessage AddSensor(Sensores sen)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.AddSensor(sen);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteSensor(int id)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.DeleteSensor(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
