﻿using BusinessLogicLayer;
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
    public class UsuariosController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllUsuarios()
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetAllUsuarios());
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpGet]
        public HttpResponseMessage GetUsuario(int id)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cap_negocio.GetUsuario(id));
            return new HttpResponseMessage() { Content = new StringContent(json) };
        }

        [HttpPost]
        public HttpResponseMessage AddUsuario(Usuario usr)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.AddUsuario(usr);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteUsuario(int id)
        {
            IBLayer cap_negocio = new BLayer(new DALayer());
            cap_negocio.DeleteUsuario(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
