﻿using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ReservationREST.BusinessEntities;

namespace ReservationREST.DataAccess
{
    public class DAReserva : IDisposable
    {
        private Database odb;
        private DbConnection ocn;
        public DAReserva()
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
        ~DAReserva()
        {
            Dispose(false);
        }

        /// <summary>
        /// Buscar reserva
        /// </summary>
        public IDataReader BuscarReserva(int COD_PEDI)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                var ocmd = odb.GetStoredProcCommand("SRC_RESERVA", COD_PEDI);
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
        /// Registrar reserva
        /// </summary>
        public void RegistrarReserva(BEReserva obj)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                using (var obts = ocn.BeginTransaction())
                {
                    try
                    {
                        using (var ocmd = odb.GetStoredProcCommand("INS_RESERVA", null,
                                                                                 obj.COD_PEDI,
                                                                                 obj.MON_PAGA,
                                                                                 obj.MON_PAGO))
                        {
                            ocmd.CommandTimeout = 2000;
                            odb.ExecuteNonQuery(ocmd, obts);
                            obj.COD_RESE = (int)odb.GetParameterValue(ocmd, "@COD_RESE");
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
        /// Actualizar reserva
        /// </summary>
        public void ActualizarReserva(BEReserva obj)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                using (var obts = ocn.BeginTransaction())
                {
                    try
                    {
                        using (var ocmd = odb.GetStoredProcCommand("UPD_RESERVA", obj.COD_PEDI,
                                                                                  obj.MON_PAGA,
                                                                                  obj.MON_PAGO,
                                                                                  obj.IND_CANC))
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
        /// Reporte de reservas
        /// </summary>
        public IDataReader ReporteReserva(DateTime FEC_INIC, DateTime FEC_FINA)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                var ocmd = odb.GetStoredProcCommand("RPT_RESERVA", FEC_INIC, FEC_FINA);
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