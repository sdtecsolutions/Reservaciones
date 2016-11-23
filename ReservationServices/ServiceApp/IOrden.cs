using System.Collections.Generic;
using System.ServiceModel;
using ReservationServices.BusinessEntities;

namespace ReservationServices.ServiceApp
{
    [ServiceContract]
    public interface IOrden
    {
        /// <summary>
        /// Lista los tipos de deporte
        /// </summary>
        [OperationContract]
        List<BETipoDeporte> Listar_TiposDeporte();

        /// <summary>
        /// Lista los tipos de cancha
        /// </summary>
        [OperationContract]
        List<BETipoCancha> Listar_TiposCancha(int COD_TIPO_DEPO);

        /// <summary>
        /// Lista los horarios
        /// </summary>
        [OperationContract]
        List<BEHorario> Listar_Horarios();

        /// <summary>
        /// Lista los horarios
        /// </summary>
        [OperationContract]
        BECliente Registrar_Cliente(BECliente obj);

        /// <summary>
        /// Lista los horarios
        /// </summary>
        [OperationContract]
        BEOrden Registrar_Orden(BEOrden obj);

        /// <summary>
        /// Lista las reservaciones
        /// </summary>
        [OperationContract]
        List<BEOrden> Listar_Reservaciones();

        /// <summary>
        /// Realizar logueo
        /// </summary>
        [OperationContract]
        int LoginUser(BELogin obj);
    }
}
