using System;
using System.Collections;
using System.Collections.Generic;
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
        public List<BETipoDeporte> ListarTipoDeporte()
        {
            var oda = new DATipoDeporte();
            using (var odr = oda.ListarTipoDeporte())
            {
                var olst = new List<BETipoDeporte>();
                ((IList)olst).LoadFromReader<BETipoDeporte>(odr);
                return (olst);
            }
        }

        /// <summary>
        /// Obtener tipo de deporte
        /// </summary>
        public BETipoDeporte ObtenerTipoDeporte(int COD_TIPO_DEPO)
        {
            var obj = new BETipoDeporte();
            var oda = new DATipoDeporte();
            using (var odr = oda.ObtenerTipoDeporte(COD_TIPO_DEPO))
            {
                var olst = new List<BETipoDeporte>();
                ((IList)olst).LoadFromReader<BETipoDeporte>(odr);
                if (olst.Count > 0)
                    obj = olst[0];

                return (obj);
            }
        }

        /// <summary>
        /// Registrar tipo de deporte
        /// </summary>
        public void RegistrarTipoDeporte(BETipoDeporte obj)
        {
            try
            {
                var oda = new DATipoDeporte();
                oda.RegistrarTipoDeporte(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Actualizar tipo de deporte
        /// </summary>
        public void ActualizarTipoDeporte(BETipoDeporte obj)
        {
            try
            {
                var oda = new DATipoDeporte();
                oda.ActualizarTipoDeporte(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Eliminar tipo de deporte
        /// </summary>
        public void EliminarTipoDeporte(int COD_TIPO_DEPO)
        {
            try
            {
                var oda = new DATipoDeporte();
                oda.EliminarTipoDeporte(COD_TIPO_DEPO);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}