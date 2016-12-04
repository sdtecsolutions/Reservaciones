using System;
using System.Collections;
using System.Collections.Generic;
using ResultSetMappers;
using ReservationREST.BusinessEntities;
using ReservationREST.DataAccess;

namespace ReservationREST.BusinessRules
{
    public class BRTipoCancha
    {
        /// <summary>
        /// Listar los tipos de cancha
        /// </summary>
        public List<BETipoCancha> ListarTipoCancha()
        {
            var oda = new DATipoCancha();
            using (var odr = oda.ListarTipoCancha())
            {
                var olst = new List<BETipoCancha>();
                ((IList)olst).LoadFromReader<BETipoCancha>(odr);
                return (olst);
            }
        }

        /// <summary>
        /// Listar los tipos de cancha filtrados por deporte
        /// </summary>
        public List<BETipoCancha> ListarTipoCanchaDeporte(int COD_TIPO_DEPO)
        {
            var oda = new DATipoCancha();
            using (var odr = oda.ListarTipoCanchaDeporte(COD_TIPO_DEPO))
            {
                var olst = new List<BETipoCancha>();
                ((IList)olst).LoadFromReader<BETipoCancha>(odr);
                return (olst);
            }
        }

        /// <summary>
        /// Obtener tipo de cancha
        /// </summary>
        public BETipoCancha ObtenerTipoCancha(int COD_TIPO_CANC)
        {
            var obj = new BETipoCancha();
            var oda = new DATipoCancha();
            using (var odr = oda.ObtenerTipoCancha(COD_TIPO_CANC))
            {
                var olst = new List<BETipoCancha>();
                ((IList)olst).LoadFromReader<BETipoCancha>(odr);
                if (olst.Count > 0)
                    obj = olst[0];

                return (obj);
            }
        }

        /// <summary>
        /// Registrar tipo de cancha
        /// </summary>
        public void RegistrarTipoCancha(BETipoCancha obj)
        {
            try
            {
                var oda = new DATipoCancha();
                oda.RegistrarTipoCancha(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Actualizar tipo de cancha
        /// </summary>
        public void ActualizarTipoCancha(BETipoCancha obj)
        {
            try
            {
                var oda = new DATipoCancha();
                oda.ActualizarTipoCancha(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Eliminar tipo de cancha
        /// </summary>
        public void EliminarTipoCancha(int COD_TIPO_CANC)
        {
            try
            {
                var oda = new DATipoCancha();
                oda.EliminarTipoCancha(COD_TIPO_CANC);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}