using System.Runtime.Serialization;

namespace ReservationREST.BusinessEntities
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
        [DataMember]
        public string ALF_MNSG_ERRO { get; set; }
    }
}