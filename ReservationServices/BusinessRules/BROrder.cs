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
    public class BROrder
    {
        /// <summary>
        /// Listar los tipos de deporte
        /// </summary>
        public List<BETypeSport> ListTypesSport()
        {
            var oda = new DAOrder();
            using (var odr = oda.ListTypesSport())
            {
                var olst = new List<BETypeSport>();                
                ((IList)olst).LoadFromReader<BETypeSport>(odr);
                return (olst);
            }
        }

        /// <summary>
        /// Listar los tipos de cancha
        /// </summary>
        public List<BETypeCourts> ListTypeCourts(int COD_TIPO_DEPO)
        {
            var oda = new DAOrder();
            using (var odr = oda.ListTypeCourts(COD_TIPO_DEPO))
            {
                var olst = new List<BETypeCourts>();
                ((IList)olst).LoadFromReader<BETypeCourts>(odr);
                return (olst);
            }
        }

        /// <summary>
        /// Listar los horarios
        /// </summary>
        public List<BETimeTable> ListTimeTable()
        {
            var oda = new DAOrder();
            using (var odr = oda.ListTimeTable())
            {
                var olst = new List<BETimeTable>();
                ((IList)olst).LoadFromReader<BETimeTable>(odr);
                return (olst);
            }
        }

        /// <summary>
        /// Registrar la solicitud de reserva
        /// </summary>
        public void SetOrder(BEOrder obj)
        {
            try
            {
                var oda = new DAOrder();
                oda.SetOrder(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Listar las reservaciones
        /// </summary>
        public List<BEOrder> ListReservation()
        {
            var oda = new DAOrder();
            using (var odr = oda.ListReservation())
            {
                var olst = new List<BEOrder>();
                ((IList)olst).LoadFromReader<BEOrder>(odr);
                return (olst);
            }
        }
    }
}