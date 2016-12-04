using ReservationREST.BusinessEntities;
using ReservationREST.BusinessRules;
using System;
using System.Collections.Generic;

namespace ReservationREST.ServiceApp
{
    public class TipoCancha : ITipoCancha
    {
        /// <summary>
        /// Listar tipos de cancha
        /// </summary>
        public List<BETipoCancha> ListarTipoCancha()
        {
            var obr = new BRTipoCancha();
            var olst = obr.ListarTipoCancha();
            return (olst);
        }

        /// <summary>
        /// Listar tipos de cancha filtrados por deporte
        /// </summary>
        public List<BETipoCancha> ListarTipoCanchaDeporte(string COD_TIPO_DEPO)
        {
            var obr = new BRTipoCancha();
            var olst = obr.ListarTipoCanchaDeporte(Convert.ToInt32(COD_TIPO_DEPO));
            return (olst);
        }

        /// <summary>
        /// Obtener tipo de cancha
        /// </summary>
        public BETipoCancha ObtenerTipoCancha(string COD_TIPO_CANC)
        {
            var obr = new BRTipoCancha();
            var obj = obr.ObtenerTipoCancha(int.Parse(COD_TIPO_CANC));
            return (obj);
        }

        /// <summary>
        /// Registrar tipo de cancha
        /// </summary>
        public BETipoCancha RegistrarTipoCancha(BETipoCancha obj)
        {
            try
            {
                var obr = new BRTipoCancha();
                obr.RegistrarTipoCancha(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

        /// <summary>
        /// Actualizar tipo de cancha
        /// </summary>
        public BETipoCancha ActualizarTipoCancha(BETipoCancha obj)
        {
            try
            {
                var obr = new BRTipoCancha();
                obr.ActualizarTipoCancha(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

        /// <summary>
        /// Eliminar tipo de cancha
        /// </summary>
        public BETipoCancha EliminarTipoCancha(string COD_TIPO_CANC)
        {
            var obj = new BETipoCancha();
            try
            {
                var obr = new BRTipoCancha();
                obr.EliminarTipoCancha(int.Parse(COD_TIPO_CANC));
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }
    }
}
