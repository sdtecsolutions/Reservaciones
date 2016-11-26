using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public BEReserva Buscar_Reserva(int COD_PEDI)
        {
            var oda = new DAReserva();
            using (var odr = oda.Buscar_Reserva(COD_PEDI))
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
        public void Registrar_Reserva(BEReserva obj)
        {
            try
            {
                var oda = new DAReserva();
                oda.Registrar_Reserva(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Actualizar reserva
        /// </summary>
        public void Actualizar_Reserva(BEReserva obj)
        {
            try
            {
                var oda = new DAReserva();
                oda.Actualizar_Reserva(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}