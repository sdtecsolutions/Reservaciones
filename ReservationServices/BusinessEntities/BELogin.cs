using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ReservationServices.BusinessEntities
{
    public class BELogin
    {
        [DataMember]
        public string COD_USUA { get; set; }

        [DataMember]
        public string ALF_PASS { get; set; }
    }
}