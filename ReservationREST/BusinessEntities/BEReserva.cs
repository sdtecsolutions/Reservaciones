using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationREST.BusinessEntities
{
    public class BEReserva
    {
        public int COD_RESE { get; set; }
        public int COD_PEDI { get; set; }
        public decimal MON_PAGA { get; set; }
        public decimal MON_PAGO { get; set; }
        public string IND_ESTA { get; set; }
    }
}