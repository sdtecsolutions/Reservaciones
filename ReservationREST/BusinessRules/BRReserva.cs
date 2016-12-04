using System;
using System.Collections;
using System.Collections.Generic;
using ResultSetMappers;
using ReservationREST.BusinessEntities;
using ReservationREST.DataAccess;

namespace ReservationREST.BusinessRules
{
    public class BRReserva
    {
        /// <summary>
        /// Listar los tipos de deporte
        /// </summary>
        public BEReserva BuscarReserva(int COD_PEDI)
        {
            var oda = new DAReserva();
            using (var odr = oda.BuscarReserva(COD_PEDI))
            {
                var olst = new List<BEReserva>();
                ((IList)olst).LoadFromReader<BEReserva>(odr);
                var objr = new BEReserva();
                if (olst.Count > 0)
                    objr = olst[0];
                return (objr);
            }
        }

        /// <summary>
        /// Registrar reserva
        /// </summary>
        public void RegistrarReserva(BEReserva obj)
        {
            try
            {
                var oda = new DAReserva();
                oda.RegistrarReserva(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Actualizar reserva
        /// </summary>
        public void ActualizarReserva(BEReserva obj)
        {
            try
            {
                var oda = new DAReserva();
                oda.ActualizarReserva(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Reporte de reservas
        /// </summary>
        public List<BEOrden> ReporteReserva(DateTime FEC_INIC, DateTime FEC_FINA)
        {
            var oda = new DAReserva();
            using (var odr = oda.ReporteReserva(FEC_INIC, FEC_FINA))
            {
                var olst = new List<BEOrden>();
                ((IList)olst).LoadFromReader<BEOrden>(odr);
                return (olst);
            }
        }
    }
}