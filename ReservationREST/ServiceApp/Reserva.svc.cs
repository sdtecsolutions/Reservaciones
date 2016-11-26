using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ReservationREST.BusinessEntities;
using ReservationREST.BusinessRules;

namespace ReservationREST.ServiceApp
{
    public class Reserva : IReserva
    {
        public BEReserva Buscar_Pedido(string COD_PEDI)
        {
            var obr = new BRReserva();
            var obj = obr.Buscar_Reserva(Convert.ToInt32(COD_PEDI));
            return (obj);
        }
        public BEReserva Registrar_Reserva(BEReserva obj)
        {
            try
            {
                var obr = new BRReserva();
                obr.Registrar_Reserva(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

        public BEReserva Actualizar_Reserva(BEReserva obj)
        {
            try
            {
                var obr = new BRReserva();
                obr.Actualizar_Reserva(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

    }
}
