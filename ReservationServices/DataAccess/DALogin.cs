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
    public class DALogin : IDisposable
    {
        private Database odb;
        private DbConnection ocn;
        public DALogin()
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
        ~DALogin()
        {
            Dispose(false);
        }

        /// <summary>
        /// Realizar logueo
        /// </summary>
        public int Login(BELogin obj)
        {
            try
            {
                if (ocn.State == ConnectionState.Closed) ocn.Open();
                var ocmd = odb.GetStoredProcCommand("LOG_USUARIO", obj.COD_USUA, obj.ALF_PASS);
                ocmd.CommandTimeout = 2000;
                var isValid = (int)odb.ExecuteScalar(ocmd);
                return (isValid);
            }
            finally
            {
                ocn.Close();
                Dispose(false);
            }
        }
    }
}