using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ReservationREST.BusinessEntities
{
    public class BEReserva
    {
        [DataMember]
        public int? COD_RESE { get; set; }
        [DataMember]
        public int COD_PEDI { get; set; }
        [DataMember]
        public decimal MON_PAGA { get; set; }
        [DataMember]
        public decimal MON_PAGO { get; set; }
        [DataMember]
        public decimal MON_DEUD { get; set; }
        [DataMember]
        public string IND_ESTA { get; set; }
        [DataMember]
        public string ALF_MNSG_ERRO { get; set; }
    }
}