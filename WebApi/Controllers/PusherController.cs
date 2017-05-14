using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class PusherController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Pusher()
        {
            var options = new PusherOptions();
            options.Encrypted = true;

            var pusher = new Pusher("330934", "6e63ad3e4d4ab9070643", "afd98f579ce0cac30bd5", options);

            var result = pusher.Trigger("my-channel", "my-event", new { message = "Temperatura elevada." });
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }
}
