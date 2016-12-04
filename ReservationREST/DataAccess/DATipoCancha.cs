using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReservationREST.BusinessEntities;

namespace ReservationREST.DataAccess
{
    public class DATipoCancha : IDisposable
    {
        private Database odb;
        private DbConnection ocn;
        public DATipoCancha()
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
        ~DATipoCancha()
        {
            Dispose(false);
        }

        /// <summary>
        /// Listar los tipos de cancha registrados
        /// </summary>
        public IDataReader ListarTipoCancha()
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                var ocmd = odb.GetStoredProcCommand("LST_TIPO_CANCHA01");
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
        /// Obtener el tipo de cancha
        /// </summary>
        public IDataReader ObtenerTipoCancha(int COD_TIPO_CANCHA)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                var ocmd = odb.GetStoredProcCommand("OBT_TIPO_CANCHA", COD_TIPO_CANCHA);
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
        /// Registrar el tipo de cancha
        /// </summary>
        public void RegistrarTipoCancha(BETipoCancha obj)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                using (var obts = ocn.BeginTransaction())
                {
                    try
                    {
                        using (var ocmd = odb.GetStoredProcCommand("INS_TIPOCANCHA", obj.COD_TIPO_CANC,
                                                                                      obj.ALF_TIPO_CANC,
                                                                                      obj.COD_TIPO_DEPO,
                                                                                      obj.NUM_JUGA,
                                                                                      obj.MON_PREC))
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
        /// Actualizar el tipo de cancha
        /// </summary>
        public void ActualizarTipoCancha(BETipoCancha obj)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                using (var obts = ocn.BeginTransaction())
                {
                    try
                    {
                        using (var ocmd = odb.GetStoredProcCommand("ACT_TIPOCANCHA", obj.COD_TIPO_CANC,
                                                                                      obj.ALF_TIPO_CANC,
                                                                                      obj.COD_TIPO_DEPO,
                                                                                      obj.NUM_JUGA,
                                                                                      obj.MON_PREC))
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
        /// Eliminar el tipo de cancha
        /// </summary>
        public void EliminarTipoCancha(int COD_TIPO_CANC)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                using (var obts = ocn.BeginTransaction())
                {
                    try
                    {
                        using (var ocmd = odb.GetStoredProcCommand("DEL_TIPOCANCHA", COD_TIPO_CANC ))
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