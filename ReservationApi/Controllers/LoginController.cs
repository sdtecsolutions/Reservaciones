using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReservationApi.srReservation;

namespace ReservationApi.Controllers
{
    public class LoginController : ApiController
    {
        /// <summary>
        /// Realizar logueo
        /// </summary>
        [HttpPost]
        [Route("loginusers")]
        public HttpResponseMessage LoginUser(BELogin obj)
        {
            try
            {
                var proxy = new OrderClient();
                var result = proxy.LoginUser(obj);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
