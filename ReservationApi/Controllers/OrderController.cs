using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReservationApi.srReservation;

namespace ReservationApi.Controllers
{
    public class OrderController : ApiController
    {
        /// <summary>
        /// Listar los tipos de deporte
        /// </summary>
        [HttpPost]
        [Route("listardeportes")]
        public HttpResponseMessage ListTypesSport()
        {
            try
            {
                var proxy = new OrderClient();
                var result = proxy.ListTypesSport();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Listar los tipos de cancha
        /// </summary>
        [HttpPost]
        [Route("listarcanchas")]
        public HttpResponseMessage ListTypeCourts([FromBody] int COD_TIPO_DEPO)
        {
            try
            {
                var proxy = new OrderClient();
                var result = proxy.ListTypeCourts(COD_TIPO_DEPO);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Listar los horarios
        /// </summary>
        [HttpPost]
        [Route("listarhorarios")]
        public HttpResponseMessage ListTimeTable()
        {
            try
            {
                var proxy = new OrderClient();
                var result = proxy.ListTimeTable();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Registrar la solicitud de reserva
        /// </summary>
        [HttpPost]
        [Route("registrarsolicitud")]
        public HttpResponseMessage SetOrder(BEOrder obj)
        {
            try
            {
                var proxy = new OrderClient();
                var order = proxy.SetOrder(obj);
                if (!string.IsNullOrWhiteSpace(order.ALF_MNSG_ERRO))
                    throw new ArgumentException(order.ALF_MNSG_ERRO);

                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Listar las reservaciones
        /// </summary>
        [HttpPost]
        [Route("listarreservaciones")]
        public HttpResponseMessage ListReservation()
        {
            try
            {
                var proxy = new OrderClient();
                var result = proxy.ListReservation();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
