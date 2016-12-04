using System.ServiceModel;
using ReservationREST.BusinessEntities;
using System.ServiceModel.Web;
using System.Collections.Generic;

namespace ReservationREST.ServiceApp
{
    [ServiceContract]
    [DataContractFormat]
    public interface IReserva
    {
        [OperationContract]
        [WebInvoke(Method = "GET", 
            UriTemplate = "pedidos/{COD_PEDI}", 
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BEReserva ListarPedido(string COD_PEDI);

        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "reservas",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BEReserva RegistrarReserva(BEReserva obj);

        [OperationContract]
        [WebInvoke(Method = "PUT",
           UriTemplate = "reservas",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BEReserva ActualizarReserva(BEReserva obj);

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "reservas/{FEC_INIC}/{FEC_FINA}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<BEOrden> ReporteReserva(string FEC_INIC, string FEC_FINA);
    }
}
