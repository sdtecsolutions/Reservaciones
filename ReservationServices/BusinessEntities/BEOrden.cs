using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ReservationServices.BusinessEntities
{
    public class BEOrden
    {
        [DataMember]
        public int COD_PEDI { get; set; }

        [DataMember]
        public string ALF_NUME_PEDI { get; set; }

        [DataMember]
        public int COD_TIPO_DEPO { get; set; }

        [DataMember]
        public int COD_TIPO_CANC { get; set; }

        [DataMember]
        public int COD_HORA { get; set; }

        [DataMember]
        public DateTime FEC_HORA_RESE { get; set; }

        [DataMember]
        public string FEC_RESE { get; set; }

        [DataMember]
        public string IND_ESTA { get; set; }

        [DataMember]
        public string HOR_INIC { get; set; }

        [DataMember]
        public string ALF_HORA { get; set; }

        [DataMember]
        public string ALF_TIPO_DEPO { get; set; }

        [DataMember]
        public string ALF_TIPO_CANC { get; set; }

        [DataMember]
        public decimal MON_PAGA { get; set; }

        [DataMember]
        public decimal MON_PAGO { get; set; }

        [DataMember]
        public decimal MON_DEUD { get; set; }

        [DataMember]
        public int COD_CLIE { get; set; }

        [DataMember]
        public string ALF_NOMB { get; set; }

        [DataMember]
        public string ALF_TIPO_DOCU { get; set; }

        [DataMember]
        public string ALF_NUME_DOCU { get; set; }

        [DataMember]
        public string ALF_CORR { get; set; }

        [DataMember]
        public string ALF_NUME_TELE { get; set; }

        [DataMember]
        public int COD_RESE { get; set; }

        [DataMember]
        public string ALF_MNSG_ERRO { get; set; }
    }
}