using System;
using System.Collections.Generic;
using ReservationREST.BusinessEntities;
using ReservationREST.BusinessRules;

namespace ReservationREST.ServiceApp
{
    public class TipoDeporte : ITipoDeporte
    {
        /// <summary>
        /// Listar tipos de deporte
        /// </summary>
        public List<BETipoDeporte> ListarTipoDeporte()
        {
            var obr = new BRTipoDeporte();
            var olst = obr.ListarTipoDeporte();
            return (olst);
        }

        /// <summary>
        /// Obtener tipo de deporte
        /// </summary>
        public BETipoDeporte ObtenerTipoDeporte(string COD_TIPO_DEPO)
        {
            var obr = new BRTipoDeporte();
            var obj = obr.ObtenerTipoDeporte(int.Parse(COD_TIPO_DEPO));
            return (obj);
        }

        /// <summary>
        /// Registrar tipo de deporte
        /// </summary>
        public BETipoDeporte RegistrarTipoDeporte(BETipoDeporte obj)
        {
            try
            {
                var obr = new BRTipoDeporte();
                obr.RegistrarTipoDeporte(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

        /// <summary>
        /// Actualizar tipo de deporte
        /// </summary>
        public BETipoDeporte ActualizarTipoDeporte(BETipoDeporte obj)
        {
            try
            {
                var obr = new BRTipoDeporte();
                obr.ActualizarTipoDeporte(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }

        /// <summary>
        /// Eliminar tipo de deporte
        /// </summary>
        public BETipoDeporte EliminarTipoDeporte(string COD_TIPO_DEPO)
        {
            var obj = new BETipoDeporte();
            try
            {
                var obr = new BRTipoDeporte();
                obr.EliminarTipoDeporte(int.Parse(COD_TIPO_DEPO));
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
            }

            return (obj);
        }
    }
}
