using System;
using System.Collections;
using System.Collections.Generic;
using ResultSetMappers;
using ReservationREST.BusinessEntities;
using ReservationREST.DataAccess;

namespace ReservationREST.BusinessRules
{
    public class BRHorario
    {
        /// <summary>
        /// Listar los horarios registrados
        /// </summary>
        public List<BEHorario> ListarHorario()
        {
            var oda = new DAHorario();
            using (var odr = oda.ListarHorario())
            {
                var olst = new List<BEHorario>();
                ((IList)olst).LoadFromReader<BEHorario>(odr);
                return (olst);
            }
        }

        /// <summary>
        /// Obtener horario
        /// </summary>
        public BEHorario ObtenerHorario(int COD_HORA)
        {
            var obj = new BEHorario();
            var oda = new DAHorario();
            using (var odr = oda.ObtenerHorario(COD_HORA))
            {
                var olst = new List<BEHorario>();
                ((IList)olst).LoadFromReader<BEHorario>(odr);
                if (olst.Count > 0)
                    obj = olst[0];

                return (obj);
            }
        }

        /// <summary>
        /// Registrar horario
        /// </summary>
        public void RegistrarHorario(BEHorario obj)
        {
            try
            {
                var oda = new DAHorario();
                oda.RegistrarHorario(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Actualizar horario
        /// </summary>
        public void ActualizarHorario(BEHorario obj)
        {
            try
            {
                var oda = new DAHorario();
                oda.ActualizarHorario(obj);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Eliminar horario
        /// </summary>
        public void EliminarHorario(int COD_HORA)
        {
            try
            {
                var oda = new DAHorario();
                oda.EliminarHorario(COD_HORA);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}