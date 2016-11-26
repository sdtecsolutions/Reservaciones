using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResultSetMappers;
using ReservationREST.BusinessEntities;
using ReservationREST.DataAccess;

namespace ReservationREST.BusinessRules
{
    public class BRTipoDeporte
    {
        /// <summary>
        /// Listar los tipos de deporte
        /// </summary>
        public List<BETipoDeporte> Listar_TiposDeporte()
        {
            var oda = new DATipoDeporte();
            using (var odr = oda.Listar_TiposDeporte())
            {
                var olst = new List<BETipoDeporte>();
                ((IList)olst).LoadFromReader<BETipoDeporte>(odr);
                return (olst);
            }
        }

        /// <summary>
        /// Registrar tipo de deporte
        /// </summary>
        public void Registrar_TipoDeporte(BETipoDeporte obj)
        {
            try
            {
                var oda = new DATipoDeporte();
                oda.Registrar_TipoDeporte(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Actualizar tipo de deporte
        /// </summary>
        public void Actualizar_TipoDeporte(BETipoDeporte obj)
        {
            try
            {
                var oda = new DATipoDeporte();
                oda.Actualizar_TipoDeporte(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Obtener tipo de deporte
        /// </summary>
        public List<BETipoDeporte> Obtener_TipoDeporte(int COD_TIPO_DEPO)
        {
            var oda = new DATipoDeporte();
            using (var odr = oda.Obtener_TipoDeporte(COD_TIPO_DEPO))
            {
                var olst = new List<BETipoDeporte>();
                ((IList)olst).LoadFromReader<BETipoDeporte>(odr);
                return (olst);
            }
        }

        /// <summary>
        /// Eliminar tipo de deporte
        /// </summary>
        public void Eliminar_TipoDeporte(int COD_TIPO_DEPO)
        {
            try
            {
                var oda = new DATipoDeporte();
                oda.Eliminar_TipoDeporte(COD_TIPO_DEPO);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}