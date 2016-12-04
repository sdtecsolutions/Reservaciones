using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReservationApi.srTipoDeporte;

namespace ReservationApi.Controllers
{
    public class TipoDeporteController : ApiController
    {
        /// <summary>
        /// Listar los tipos de deporte
        /// </summary>
        [HttpGet]
        [Route("listardeportes")]
        public HttpResponseMessage Listar_TiposDeporte()
        {
            try
            {
                var proxy = new TipoDeporteClient();
                var result = proxy.Listar_TiposDeporte();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Registrar el tipo de deporte
        /// </summary>
        [HttpPost]
        [Route("registrartipodeporte")]
        public HttpResponseMessage Registrar_TipoDeporte(BETipoDeporte obj)
        {
            try
            {
                var proxy = new TipoDeporteClient();

                var objcl = new BETipoDeporte()
                {
                    COD_TIPO_DEPO = obj.COD_TIPO_DEPO,
                    ALF_TIPO_DEPO = obj.ALF_TIPO_DEPO
                };
                var tipodeporte = proxy.Registrar_TipoDeporte(objcl);
                if (!string.IsNullOrWhiteSpace(tipodeporte.ALF_MNSG_ERRO))
                    throw new ArgumentException(tipodeporte.ALF_MNSG_ERRO);


                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Actualizar el tipo de deporte
        /// </summary>
        [HttpPut]
        [Route("actualizartipodeporte")]
        public HttpResponseMessage Actualizar_TipoDeporte(BETipoDeporte obj)
        {
            try
            {
                var proxy = new TipoDeporteClient();

                var objcl = new BETipoDeporte()
                {
                    COD_TIPO_DEPO = obj.COD_TIPO_DEPO,
                    ALF_TIPO_DEPO = obj.ALF_TIPO_DEPO
                };
                var tipodeporte = proxy.Actualizar_TipoDeporte(objcl);
                if (!string.IsNullOrWhiteSpace(tipodeporte.ALF_MNSG_ERRO))
                    throw new ArgumentException(tipodeporte.ALF_MNSG_ERRO);


                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Eliminar el tipo de deporte
        /// </summary>
 
    }
}
