using System;
using System.Collections.Generic;
using ReservationREST.BusinessEntities;
using ReservationREST.BusinessRules;
using System.ServiceModel.Web;
using System.Net;

namespace ReservationREST.ServiceApp
{
    public class TipoDeporte : ITipoDeporte
    {
        /// <summary>
        /// Listar tipos de deporte
        /// </summary>
        public List<BETipoDeporte> Listar_TiposDeporte()
        {
            var obr = new BRTipoDeporte();
            var olst = obr.Listar_TiposDeporte();
            return (olst);
        }

        /// <summary>
        /// Registrar tipo de deporte
        /// </summary>
        public BETipoDeporte Registrar_TipoDeporte(BETipoDeporte obj)
        {
            try
            {
                var obr = new BRTipoDeporte();
                obr.Registrar_TipoDeporte(obj);
            }
            catch (Exception ex)
            {
                obj.ALF_MNSG_ERRO = ex.Message;
                throw new WebFaultException<string>(
                           obj.ALF_MNSG_ERRO, HttpStatusCode.InternalServerError
                           );

            }

            return (obj);
        }

        /// <summary>
        /// Actualizar tipo de deporte
        /// </summary>
        public BETipoDeporte Actualizar_TipoDeporte(BETipoDeporte obj)
        {
            var obr = new BRTipoDeporte();
            obr.Actualizar_TipoDeporte(obj);
            return obj;
        }

        public List<BETipoDeporte> Obtener_TipoDeporte(string COD_TIPO_DEPO)
        {
            //throw new NotImplementedException();
            var obr = new BRTipoDeporte();
            var olst = obr.Obtener_TipoDeporte(int.Parse(COD_TIPO_DEPO));
            return (olst);
        }

        /// <summary>
        /// Eliminar tipo de deporte
        /// </summary>
        public void Eliminar_TipoDeporte(string COD_TIPO_DEPO)
        {
            var obr = new BRTipoDeporte();
            obr.Eliminar_TipoDeporte(int.Parse(COD_TIPO_DEPO));
        }
    }
}
