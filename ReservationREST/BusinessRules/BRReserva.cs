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