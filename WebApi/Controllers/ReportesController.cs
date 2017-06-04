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
        [ActionName("Valores")]
        public HttpResponseMessage GetAllValores()
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetAllValores());
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpGet]
        [ActionName("Valores")]
        public HttpResponseMessage GetValor(int idSensor)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetValoresDeSensor(idSensor));
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpGet]
        [ActionName("Valores")]
        public HttpResponseMessage GetValorIdFecha(int idSensor, String fecha)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetValoresDeSensorConFecha(idSensor, fecha));
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpGet]
        [ActionName("Eventos")]
        public HttpResponseMessage GetAllHistoricoEventos()
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetAllHistoricoEventos());
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpGet]
        [ActionName("Eventos")]
        public HttpResponseMessage GetHistoricoEventos(int idEvento)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetAllHistoricoEventosDeEventoId(idEvento));
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpGet]
        [ActionName("Eventos")]
        public HttpResponseMessage GetHistoricoEventosFecha(int idEvento, string fecha)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetAllHistoricoEventosFecha(idEvento,fecha));
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }
    }
}
