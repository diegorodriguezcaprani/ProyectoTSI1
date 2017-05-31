using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogicLayer;
using DataAccessLayer;
using Shared.Entities;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReportesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllValores()
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetAllValores());
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpGet]
        public HttpResponseMessage GetValor(int id)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetValoresDeSensor(id));
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpGet]
        public HttpResponseMessage GetValorIdFecha(int id, String fecha)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetValoresDeSensorConFecha(id, fecha));
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }
    }
}
