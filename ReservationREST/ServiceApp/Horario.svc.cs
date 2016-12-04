using ReservationREST.BusinessEntities;
using ReservationREST.BusinessRules;
using System;
using System.Collections.Generic;

namespace ReservationREST.ServiceApp
{
    public class Horario : IHorario
    {
        /// <summary>
        /// Listar horarios
        /// </summary>
        public List<BEHorario> ListarHorario()
        {
            var obr = new BRHorario();
            var olst = obr.ListarHorario();
            return (olst);
        }

        /// <summary>
        /// Obtener horario
        /// </summary>
        public BEHorario ObtenerHorario(string COD_HORA)
        {
            var obr = new BRHorario();
            var obj = obr.ObtenerHorario(int.Parse(COD_HORA));
            return (obj);
        }

        /// <summary>
        /// Registrar horario
        /// </summary>
        public BEHorario RegistrarHorario(BEHorario obj)
        {
            try
            {
                var obr = new BRHorario();
                obr.RegistrarHorario(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

        /// <summary>
        /// Actualizar horario
        /// </summary>
        public BEHorario ActualizarHorario(BEHorario obj)
        {
            try
            {
                var obr = new BRHorario();
                obr.ActualizarHorario(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

        /// <summary>
        /// Eliminar horario
        /// </summary>
        public BEHorario EliminarHorario(string COD_HORA)
        {
            var obj = new BEHorario();
            try
            {
                var obr = new BRHorario();
                obr.EliminarHorario(int.Parse(COD_HORA));
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }
    }
}
