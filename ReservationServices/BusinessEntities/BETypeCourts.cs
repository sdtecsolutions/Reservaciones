using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ReservationServices.BusinessEntities
{
    public class BETypeCourts
    {
        [DataMember]
        public int COD_TIPO_DEPO { get; set; }

        [DataMember]
        public int COD_TIPO_CANC { get; set; }

        [DataMember]
        public string ALF_TIPO_CANC { get; set; }
    }
}