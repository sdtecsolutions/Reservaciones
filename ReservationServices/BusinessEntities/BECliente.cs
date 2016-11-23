using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ReservationServices.BusinessEntities
{
    public class BECliente
    {
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
        public string ALF_MNSG_ERRO { get; set; }
    }
}