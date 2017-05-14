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
    public class ValoresController : ApiController
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
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetValor(id));
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpPost]
        public HttpResponseMessage AddValor(Valores val)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.AddValor(val);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteValor(int id)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.DeleteValor(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
