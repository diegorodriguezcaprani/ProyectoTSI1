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
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Login(Login usr)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.Login(usr));
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }
    }
}
