﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ReservationREST.BusinessEntities
{
    [DataContract(Name = "BETipoDeporte")]
    public class BETipoDeporte
    {

            [DataMember(Name = "COD_TIPO_DEPO")]
            public int COD_TIPO_DEPO { get; set; }

            [DataMember(Name = "ALF_TIPO_DEPO")]
            public string ALF_TIPO_DEPO { get; set; }

            [DataMember(Name = "ALF_MNSG_ERRO")]
            public string ALF_MNSG_ERRO { get; set; }
        
    }
}