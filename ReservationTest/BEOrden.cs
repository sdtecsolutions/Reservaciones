using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationTest
{
    class BEOrden
    {
         
        public int COD_PEDI { get; set; }

         
        public string ALF_NUME_PEDI { get; set; }

         
        public int COD_TIPO_DEPO { get; set; }

         
        public int COD_TIPO_CANC { get; set; }

         
        public int COD_HORA { get; set; }

         
        public DateTime FEC_HORA_RESE { get; set; }

         
        public string FEC_RESE { get; set; }

         
        public string IND_ESTA { get; set; }

         
        public string HOR_INIC { get; set; }        
        public string ALF_HORA { get; set; }        
        public string ALF_TIPO_DEPO { get; set; }        
        public string ALF_TIPO_CANC { get; set; }
        
        public decimal MON_PAGA { get; set; }        
        public decimal MON_PAGO { get; set; }        
        public decimal MON_DEUD { get; set; }
        public int COD_CLIE { get; set; }
        public string ALF_NOMB { get; set; }     
        public string ALF_TIPO_DOCU { get; set; }       
        public string ALF_NUME_DOCU { get; set; }        
        public string ALF_CORR { get; set; }      
        public string ALF_NUME_TELE { get; set; }
        public int COD_RESE { get; set; }
        
        public string ALF_MNSG_ERRO { get; set; }
    }
}
