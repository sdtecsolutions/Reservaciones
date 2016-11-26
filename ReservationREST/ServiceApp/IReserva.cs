using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ReservationREST.BusinessEntities;
using System.ServiceModel.Web;

namespace ReservationREST.ServiceApp
{
    [ServiceContract]
    public interface IReserva
    {
        [OperationContract]
        [WebInvoke(Method = "GET", 
            UriTemplate = "Buscar_Pedido/{COD_PEDI}", 
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        BEReserva Buscar_Pedido(string COD_PEDI);

        [OperationContract]
        [WebInvoke(Method = "POST",
           UriTemplate = "Registrar_Reserva",
           ResponseFormat = WebMessageFormat.Json)]
        BEReserva Registrar_Reserva(BEReserva obj);

        [OperationContract]
        [WebInvoke(Method = "PUT",
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "Actualizar_Reserva")]
        BEReserva Actualizar_Reserva(BEReserva obj);
    }
}
