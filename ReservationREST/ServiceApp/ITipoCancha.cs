using ReservationREST.BusinessEntities;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ReservationREST.ServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITipoCancha" in both code and config file together.
    [ServiceContract]
    public interface ITipoCancha
    {
        /// <summary>
        /// Lista los tipos de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "tipocanchas",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<BETipoCancha> ListarTipoCancha();

        /// <summary>
        /// Obtener tipo de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "tipocanchas/{COD_TIPO_CANC}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BETipoCancha ObtenerTipoCancha(string COD_TIPO_CANC);

        /// <summary>
        /// Registrar los tipos de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "tipocanchas",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BETipoCancha RegistrarTipoCancha(BETipoCancha obj);

        /// <summary>
        /// Actualizar los tipos de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            UriTemplate = "tipocanchas",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BETipoCancha ActualizarTipoCancha(BETipoCancha obj);

        /// <summary>
        /// Eliminar los tipos de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "tipocanchas/{COD_TIPO_CANC}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BETipoCancha EliminarTipoCancha(string COD_TIPO_CANC);
    }
}
