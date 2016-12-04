using System.Runtime.Serialization;

namespace ReservationREST.BusinessEntities
{
    [DataContract(Name = "BETipoCancha")]
    public class BETipoCancha
    {
        [DataMember(Name = "COD_TIPO_CANC")]
        public int COD_TIPO_CANC { get; set; }
        [DataMember(Name = "ALF_TIPO_CANC")]
        public string ALF_TIPO_CANC { get; set; }
        [DataMember(Name = "COD_TIPO_DEPO")]
        public int COD_TIPO_DEPO { get; set; }
        [DataMember(Name = "ALF_TIPO_DEPO")]
        public string ALF_TIPO_DEPO { get; set; }
        [DataMember(Name = "NUM_JUGA")]
        public int NUM_JUGA { get; set; }
        [DataMember(Name = "MON_PREC")]
        public decimal MON_PREC { get; set; }
        [DataMember(Name = "ALF_MNSG_ERRO")]
        public string ALF_MNSG_ERRO { get; set; }

    }
}