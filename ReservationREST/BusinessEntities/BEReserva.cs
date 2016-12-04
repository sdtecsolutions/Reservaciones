using System.Runtime.Serialization;

namespace ReservationREST.BusinessEntities
{
    [DataContract]
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
        public bool IND_CANC { get; set; }
        [DataMember]
        public string ALF_MNSG_ERRO { get; set; }
    }
}