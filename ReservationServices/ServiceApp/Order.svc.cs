using System;
using System.Collections.Generic;
using ReservationServices.BusinessEntities;
using ReservationServices.BusinessRules;

namespace ReservationServices.ServiceApp
{
    public class Order : IOrder
    {
        /// <summary>
        /// Listar los tipos de deporte
        /// </summary>
        public List<BETypeSport> ListTypesSport()
        {
            var obr = new BROrder();
            var olst = obr.ListTypesSport();
            return (olst);
        }

        /// <summary>
        /// Listar los tipos de cancha
        /// </summary>
        public List<BETypeCourts> ListTypeCourts(int COD_TIPO_DEPO)
        {
            var obr = new BROrder();
            var olst = obr.ListTypeCourts(COD_TIPO_DEPO);
            return (olst);
        }

        /// <summary>
        /// Listar los horarios
        /// </summary>
        public List<BETimeTable> ListTimeTable()
        {
            var obr = new BROrder();
            var olst = obr.ListTimeTable();
            return (olst);
        }

        /// <summary>
        /// Registrar la solicitud de reserva
        /// </summary>
        public BEOrder SetOrder(BEOrder obj)
        {
            try
            {
                var result = DateTime.Compare(obj.FEC_HORA_RESE, DateTime.Today);
                if (result < 0)
                    throw new ArgumentException("La fecha de reserva debe ser mayor o igual a la actual.");

                if (result == 0)
                {
                    var splhr = obj.HOR_INIC.Split(':');
                    var hr = new TimeSpan(Convert.ToInt32(splhr[0]), Convert.ToInt32(splhr[1]), 0);
                    result = TimeSpan.Compare(hr, DateTime.Now.TimeOfDay);
                    if (result < 0)
                        throw new ArgumentException("El horario seleccionado debe ser mayor a la hora actual.");
                }

                var obr = new BROrder();
                obr.SetOrder(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

        /// <summary>
        /// Listar las reservaciones
        /// </summary>
        public List<BEOrder> ListReservation()
        {
            var obr = new BROrder();
            var olst = obr.ListReservation();
            return (olst);
        }

        /// <summary>
        /// Realizar logueo
        /// </summary>
        public int LoginUser(BELogin obj)
        {
            var obr = new BRLogin();
            var isValid = obr.Login(obj);
            return (isValid);
        }
    }
}
