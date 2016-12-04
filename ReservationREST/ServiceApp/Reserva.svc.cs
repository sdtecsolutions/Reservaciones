using System;
using ReservationREST.BusinessEntities;
using ReservationREST.BusinessRules;
using System.Collections.Generic;

namespace ReservationREST.ServiceApp
{
    public class Reserva : IReserva
    {
        public BEReserva ListarPedido(string COD_PEDI)
        {
            var obr = new BRReserva();
            var obj = obr.BuscarReserva(Convert.ToInt32(COD_PEDI));
            return (obj);
        }
        public BEReserva RegistrarReserva(BEReserva obj)
        {
            try
            {
                var obr = new BRReserva();
                obr.RegistrarReserva(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

        public BEReserva ActualizarReserva(BEReserva obj)
        {
            try
            {
                var obr = new BRReserva();
                obr.ActualizarReserva(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

        public List<BEOrden> ReporteReserva(string FEC_INIC, string FEC_FINA)
        {
            var obr = new BRReserva();
            var inic = Convert.ToDateTime(FEC_INIC);
            var fina = Convert.ToDateTime(FEC_FINA);
            var olst = obr.ReporteReserva(inic, fina);
            return (olst);
        }
    }
}
