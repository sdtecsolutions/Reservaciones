using System;
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
        public HttpResponseMessage Listar_TiposDeporte()
        {
            try
            {
                var proxy = new OrdenClient();
                var result = proxy.Listar_TiposDeporte();

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
        public HttpResponseMessage Listar_TiposCancha([FromBody] int COD_TIPO_DEPO)
        {
            try
            {
                var proxy = new OrdenClient();
                var result = proxy.Listar_TiposCancha(COD_TIPO_DEPO);

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
        public HttpResponseMessage Listar_Horarios()
        {
            try
            {
                var proxy = new OrdenClient();
                var result = proxy.Listar_Horarios();

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
        public HttpResponseMessage Registrar_Orden(BEOrden obj)
        {
            try
            {
                var proxy = new OrdenClient();

                var objcl = new BECliente()
                {
                    ALF_TIPO_DOCU = obj.ALF_TIPO_DOCU,
                    ALF_NUME_DOCU = obj.ALF_NUME_DOCU,
                    ALF_NOMB = obj.ALF_NOMB,
                    ALF_CORR = obj.ALF_CORR,
                    ALF_NUME_TELE = obj.ALF_NUME_TELE
                };
                var cliente = proxy.Registrar_Cliente(objcl);
                if (!string.IsNullOrWhiteSpace(cliente.ALF_MNSG_ERRO))
                    throw new ArgumentException(cliente.ALF_MNSG_ERRO);

                obj.COD_CLIE = cliente.COD_CLIE;
                var order = proxy.Registrar_Orden(obj);
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
        public HttpResponseMessage Listar_Reservaciones()
        {
            try
            {
                var proxy = new OrdenClient();
                var result = proxy.Listar_Reservaciones();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
