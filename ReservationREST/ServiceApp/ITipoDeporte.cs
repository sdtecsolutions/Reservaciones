using System.Collections.Generic;
using System.ServiceModel;
using ReservationREST.BusinessEntities;
using System.ServiceModel.Web;

namespace ReservationREST.ServiceApp
{
    [ServiceContract]
    public interface ITipoDeporte
    {
        /// <summary>
        /// Lista los tipos de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET", 
            UriTemplate = "tipodeportes",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<BETipoDeporte> ListarTipoDeporte();

        /// <summary>
        /// Obtener tipo de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "tipodeportes/{COD_TIPO_DEPO}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BETipoDeporte ObtenerTipoDeporte(string COD_TIPO_DEPO);

        /// <summary>
        /// Registrar los tipos de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", 
            UriTemplate = "tipodeportes",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BETipoDeporte RegistrarTipoDeporte(BETipoDeporte obj);

        /// <summary>
        /// Actualizar los tipos de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "PUT", 
            UriTemplate = "tipodeportes",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BETipoDeporte ActualizarTipoDeporte(BETipoDeporte obj);

        /// <summary>
        /// Eliminar los tipos de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "DELETE", 
            UriTemplate = "tipodeportes/{COD_TIPO_DEPO}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BETipoDeporte EliminarTipoDeporte(string COD_TIPO_DEPO);
    }
}
