using System.Collections.Generic;
using System.ServiceModel;
using ReservationServices.BusinessEntities;

namespace ReservationServices.ServiceApp
{
    [ServiceContract]
    public interface IOrder
    {
        /// <summary>
        /// Lista los tipos de deporte
        /// </summary>
        [OperationContract]
        List<BETypeSport> ListTypesSport();

        /// <summary>
        /// Lista los tipos de cancha
        /// </summary>
        [OperationContract]
        List<BETypeCourts> ListTypeCourts(int COD_TIPO_DEPO);

        /// <summary>
        /// Lista los horarios
        /// </summary>
        [OperationContract]
        List<BETimeTable> ListTimeTable();

        /// <summary>
        /// Lista los horarios
        /// </summary>
        [OperationContract]
        BEOrder SetOrder(BEOrder obj);

        /// <summary>
        /// Lista las reservaciones
        /// </summary>
        [OperationContract]
        List<BEOrder> ListReservation();

        /// <summary>
        /// Realizar logueo
        /// </summary>
        [OperationContract]
        int LoginUser(BELogin obj);
    }
}
