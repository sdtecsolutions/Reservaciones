using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ReservationServices.BusinessEntities
{
    public class BEHorario
    {
        [DataMember]
        public int COD_HORA { get; set; }

        [DataMember]
        public string HOR_INIC { get; set; }

        [DataMember]
        public string HOR_FINA { get; set; }

        [DataMember]
        public string ALF_HORA { get; set; }
    }
}