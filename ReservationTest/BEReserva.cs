using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationTest
{
    class BEReserva
    {        
        public int COD_RESE { get; set; }        
        public int COD_PEDI { get; set; }        
        public decimal MON_PAGA { get; set; }        
        public decimal MON_PAGO { get; set; }        
        public decimal MON_DEUD { get; set; }        
        public string IND_ESTA { get; set; }        
        public bool IND_CANC { get; set; }        
        public string ALF_MNSG_ERRO { get; set; }
    }
}
