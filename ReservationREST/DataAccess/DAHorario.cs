using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReservationREST.BusinessEntities;

namespace ReservationREST.DataAccess
{
    public class DAHorario : IDisposable
    {
        private Database odb;
        private DbConnection ocn;
        public DAHorario()
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
        ~DAHorario()
        {
            Dispose(false);
        }

        /// <summary>
        /// Listar los horarios registrados en la base de datos
        /// </summary>
        public IDataReader ListarHorario()
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
        /// Obtener el horario
        /// </summary>
        public IDataReader ObtenerHorario(int COD_HORA)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                var ocmd = odb.GetStoredProcCommand("OBT_HORARIO", COD_HORA);
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
        /// Registrar el horario
        /// </summary>
        public void RegistrarHorario(BEHorario obj)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                using (var obts = ocn.BeginTransaction())
                {
                    try
                    {
                        using (var ocmd = odb.GetStoredProcCommand("INS_HORARIO", obj.COD_HORA,
                                                                                      obj.HOR_INIC,
                                                                                      obj.HOR_FINA))
                        {
                            ocmd.CommandTimeout = 2000;
                            odb.ExecuteNonQuery(ocmd, obts);
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
        /// Actualizar el horario
        /// </summary>
        public void ActualizarHorario(BEHorario obj)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                using (var obts = ocn.BeginTransaction())
                {
                    try
                    {
                        using (var ocmd = odb.GetStoredProcCommand("ACT_HORARIO", obj.COD_HORA,
                                                                                      obj.HOR_INIC,
                                                                                      obj.HOR_FINA))
                        {
                            ocmd.CommandTimeout = 2000;
                            odb.ExecuteNonQuery(ocmd, obts);
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
        /// Eliminar el horario
        /// </summary>
        public void EliminarHorario(int COD_HORA)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                using (var obts = ocn.BeginTransaction())
                {
                    try
                    {
                        using (var ocmd = odb.GetStoredProcCommand("DEL_HORARIO", COD_HORA))
                        {
                            ocmd.CommandTimeout = 2000;
                            odb.ExecuteNonQuery(ocmd, obts);
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
    }
}