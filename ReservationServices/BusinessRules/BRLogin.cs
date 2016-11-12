using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResultSetMappers;
using ReservationServices.BusinessEntities;
using ReservationServices.DataAccess;

namespace ReservationServices.BusinessRules
{
    public class BRLogin
    {
        /// <summary>
        /// Listar los tipos de deporte
        /// </summary>
        public int Login(BELogin obj)
        {
            var oda = new DALogin();
            var isValid = oda.Login(obj);
            return (isValid);
        }
    }
}