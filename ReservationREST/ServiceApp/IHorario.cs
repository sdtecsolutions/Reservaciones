using ReservationREST.BusinessEntities;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ReservationREST.ServiceApp
{
    [ServiceContract]
    public interface IHorario
    {
        /// <summary>
        /// Lista los horarios
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "horarios",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<BEHorario> ListarHorario();

        /// <summary>
        /// Obtener tipo de deporte
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "horarios/{COD_HORA}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BEHorario ObtenerHorario(string COD_HORA);

        /// <summary>
        /// Registrar los horarios
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "horarios",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BEHorario RegistrarHorario(BEHorario obj);

        /// <summary>
        /// Actualizar los horarios
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            UriTemplate = "horarios",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BEHorario ActualizarHorario(BEHorario obj);

        /// <summary>
        /// Eliminar los horarios
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "horarios/{COD_HORA}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BEHorario EliminarHorario(string COD_HORA);
    }
}
