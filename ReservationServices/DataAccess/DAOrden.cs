using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReservationServices.BusinessEntities;

namespace ReservationServices.DataAccess
{
    public class DAOrden : IDisposable
    {
        private Database odb;
        private DbConnection ocn;
        public DAOrden()
        {
            odb = DatabaseFactory.CreateDatabase("ReservationConnectionString");
            ocn = odb.CreateConnection();
        }
        private bool disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                ocn.Dispose();
                ((IDisposable)odb).Dispose();
                ocn = null;
                odb = null;
            }
            disposed = true;
        }
        ~DAOrden()
        {
            Dispose(false);
        }

        /// <summary>
        /// Listar los tipos de deporte
        /// </summary>
        public IDataReader Listar_TiposDeporte()
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                var ocmd = odb.GetStoredProcCommand("LST_TIPO_DEPORTE");
                ocmd.CommandTimeout = 2000;
                var odr = odb.ExecuteReader(ocmd);
                return (odr);
            }
            finally
            {
                ocn.Close();
                Dispose(false);
            }
        }

        /// <summary>
        /// Listar los tipos de cancha
        /// </summary>
        public IDataReader Listar_TiposCancha(int COD_TIPO_DEPO)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                var ocmd = odb.GetStoredProcCommand("LST_TIPO_CANCHA", COD_TIPO_DEPO);
                ocmd.CommandTimeout = 2000;
                var odr = odb.ExecuteReader(ocmd);
                return (odr);
            }
            finally
            {
                ocn.Close();
                Dispose(false);
            }
        }

        /// <summary>
        /// Listar los horarios
        /// </summary>
        public IDataReader Listar_Horarios()
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                var ocmd = odb.GetStoredProcCommand("LST_HORARIO");
                ocmd.CommandTimeout = 2000;
                var odr = odb.ExecuteReader(ocmd);
                return (odr);
            }
            finally
            {
                ocn.Close();
                Dispose(false);
            }
        }

        /// <summary>
        /// Registrar la solicitud de reserva
        /// </summary>
        public void Registrar_Orden(BEOrden obj)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                using (var obts = ocn.BeginTransaction())
                {
                    try
                    {
                        using (var ocmd = odb.GetStoredProcCommand("INS_PEDIDO", null,
                                                                                 obj.COD_TIPO_DEPO,
                                                                                 obj.COD_TIPO_CANC,
                                                                                 obj.COD_HORA,
                                                                                 obj.FEC_HORA_RESE,
                                                                                 obj.ALF_NOMB,
                                                                                 obj.ALF_TIPO_DOCU,
                                                                                 obj.ALF_NUME_DOCU,
                                                                                 obj.ALF_CORR,
                                                                                 obj.ALF_NUME_TELE))
                        {
                            ocmd.CommandTimeout = 2000;
                            odb.ExecuteNonQuery(ocmd, obts);
                            obj.ALF_NUME_PEDI = Convert.ToString(odb.GetParameterValue(ocmd, "@ALF_NUME_PEDI"));
                            obts.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        obts.Rollback();
                        throw new ArgumentException(ex.Message);
                    }
                    finally
                    {
                        ocn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Listar las reservaciones
        /// </summary>
        public IDataReader Listar_Reservaciones()
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                var ocmd = odb.GetStoredProcCommand("LST_RESERVA");
                ocmd.CommandTimeout = 2000;
                var odr = odb.ExecuteReader(ocmd);
                return (odr);
            }
            finally
            {
                ocn.Close();
                Dispose(false);
            }
        }
    }
}