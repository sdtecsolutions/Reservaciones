using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReservationREST.BusinessEntities;

namespace ReservationREST.DataAccess
{
    public class DATipoDeporte:IDisposable
    {
        private Database odb;
        private DbConnection ocn;
        public DATipoDeporte()
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
        ~DATipoDeporte()
        {
            Dispose(false);
        }

        /// <summary>
        /// Listar los tipos de deportes registrados
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
        /// Registrar el tipo de deporte
        /// </summary>
        public void Registrar_TipoDeporte(BETipoDeporte obj)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                using (var obts = ocn.BeginTransaction())
                {
                    try
                    {
                        using (var ocmd = odb.GetStoredProcCommand("INS_TIPODEPORTE",
                                                                                 obj.COD_TIPO_DEPO,
                                                                                 obj.ALF_TIPO_DEPO))
                        {
                            ocmd.CommandTimeout = 2000;
                            odb.ExecuteNonQuery(ocmd, obts);
                            obj.COD_TIPO_DEPO = (int)odb.GetParameterValue(ocmd, "@COD_TIPO_DEPO");
                            obj.ALF_TIPO_DEPO = (string)odb.GetParameterValue(ocmd, "@ALF_TIPO_DEPO");
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
        /// Actualizar el tipo de deporte
        /// </summary>
        public void Actualizar_TipoDeporte(BETipoDeporte obj)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                using (var obts = ocn.BeginTransaction())
                {
                    try
                    {
                        using (var ocmd = odb.GetStoredProcCommand("ACT_TIPODEPORTE",
                                                                                 obj.COD_TIPO_DEPO,
                                                                                 obj.ALF_TIPO_DEPO))
                        {
                            ocmd.CommandTimeout = 2000;
                            odb.ExecuteNonQuery(ocmd, obts);
                            obj.COD_TIPO_DEPO = (int)odb.GetParameterValue(ocmd, "@COD_TIPO_DEPO");
                            obj.ALF_TIPO_DEPO = (string)odb.GetParameterValue(ocmd, "@ALF_TIPO_DEPO");
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
        /// Obtener el tipo de deporte
        /// </summary>
        public IDataReader Obtener_TipoDeporte(int COD_TIPO_DEPO)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                var ocmd = odb.GetStoredProcCommand("OBT_TIPO_DEPORTE", COD_TIPO_DEPO);
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
        /// Eliminar el tipo de deporte
        /// </summary>
        public void Eliminar_TipoDeporte(int COD_TIPO_DEPO)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                using (var obts = ocn.BeginTransaction())
                {
                    try
                    {
                        using (var ocmd = odb.GetStoredProcCommand("DEL_TIPODEPORTE",
                                                                                 COD_TIPO_DEPO))
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