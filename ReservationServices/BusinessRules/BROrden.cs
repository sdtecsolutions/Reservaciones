using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResultSetMappers;
using ReservationServices.BusinessEntities;
using ReservationServices.DataAccess;

namespace ReservationServices.BusinessRules
{
    public class BROrden
    {
        /// <summary>
        /// Listar los tipos de deporte
        /// </summary>
        public List<BETipoDeporte> Listar_TiposDeporte()
        {
            var oda = new DAOrden();
            using (var odr = oda.Listar_TiposDeporte())
            {
                var olst = new List<BETipoDeporte>();                
                ((IList)olst).LoadFromReader<BETipoDeporte>(odr);
                return (olst);
            }
        }

        /// <summary>
        /// Listar los tipos de cancha
        /// </summary>
        public List<BETipoCancha> Listar_TiposCancha(int COD_TIPO_DEPO)
        {
            var oda = new DAOrden();
            using (var odr = oda.Listar_TiposCancha(COD_TIPO_DEPO))
            {
                var olst = new List<BETipoCancha>();
                ((IList)olst).LoadFromReader<BETipoCancha>(odr);
                return (olst);
            }
        }

        /// <summary>
        /// Listar los horarios
        /// </summary>
        public List<BEHorario> Listar_Horarios()
        {
            var oda = new DAOrden();
            using (var odr = oda.Listar_Horarios())
            {
                var olst = new List<BEHorario>();
                ((IList)olst).LoadFromReader<BEHorario>(odr);
                return (olst);
            }
        }

        /// <summary>
        /// Registrar cliente
        /// </summary>
        public void Registrar_Cliente(BECliente obj)
        {
            try
            {
                var oda = new DAOrden();
                oda.Registrar_Cliente(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Registrar la solicitud de reserva
        /// </summary>
        public void Registrar_Orden(BEOrden obj)
        {
            try
            {
                var oda = new DAOrden();
                oda.Registrar_Orden(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Listar las reservaciones
        /// </summary>
        public List<BEOrden> Listar_Reservaciones()
        {
            var oda = new DAOrden();
            using (var odr = oda.Listar_Reservaciones())
            {
                var olst = new List<BEOrden>();
                ((IList)olst).LoadFromReader<BEOrden>(odr);
                return (olst);
            }
        }
    }
}