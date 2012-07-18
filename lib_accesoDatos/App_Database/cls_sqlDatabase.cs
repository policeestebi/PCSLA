using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

using COSEVI.CSLA.lib.accesoDatos.App_DataProvider;

namespace COSEVI.CSLA.lib.accesoDatos.App_Database
{
    public class cls_sqlDatabase
    {
        // Patrón Singleton (Maneja una única instancia en el sistema).    

        private static cls_sqlDatabase cu_instance = null;

        private static itf_dataProvider cu_dataProvider = new cls_sqlDataProvider();

        private static IDbConnection cu_connection = null;

        private static IDbTransaction cu_transaction = null;

        protected cls_sqlDatabase()
        {
            try
            {
                // String connectionString = @"Data Source = (local); Initial Catalog = EJEMPLOMENU; Integrated Security = True";                

                String vs_connectionString = ConfigurationManager.ConnectionStrings["cdb_local"].ConnectionString;
                cu_connection = cu_dataProvider.connection(vs_connectionString);
            }
            catch (Exception) { }
        }

        public static cls_sqlDatabase getInstance()
        {
            if (cu_instance == null)
                cu_instance = new cls_sqlDatabase();
            return cu_instance;
        }

        protected static void setInstanceNull()
        {
            cu_instance = null;
            cu_connection = null;
        }

        public IDbConnection getConnection()
        {
            if (cu_connection.State != ConnectionState.Open)
            {
                try
                {
                    cu_connection.Open();
                }
                catch (Exception)
                {
                    setInstanceNull();
                }
            }
            return cu_connection;
        }

        public static void closeConnection()
        {
            try
            {
                if (cu_connection.State == ConnectionState.Open)
                    cu_connection.Close();
            }
            catch (Exception) { }
        }

        // Stored Procedures -> type = true
        // Sentencias SQL    -> type = false
        public static int executeNonQuery(String ps_command, Boolean pu_type, cls_parameter[] pa_parameters)
        {
            int vi_result = -1;

            try
            {
                IDbCommand vu_cmd = cu_dataProvider.command(ps_command);
                foreach (cls_parameter vu_parameter in pa_parameters)
                    vu_cmd.Parameters.Add(cu_dataProvider.parameter(vu_parameter));

                if (pu_type.Equals(true))
                    vu_cmd.CommandType = CommandType.StoredProcedure;

                if (cu_transaction != null)
                {
                    vu_cmd.Connection = cu_transaction.Connection;
                    vu_cmd.Transaction = cu_transaction;
                    vi_result = vu_cmd.ExecuteNonQuery();
                }
                else
                {
                    vu_cmd.Connection = getInstance().getConnection();
                    vi_result = vu_cmd.ExecuteNonQuery();
                    vu_cmd.Dispose();
                    closeConnection();
                }

                return vi_result;
            }
            catch (Exception ve_exception)
            {
                throw ve_exception;
            }
        }

        // Stored Procedures -> type = true
        // Sentencias SQL    -> type = false
        public static DataSet executeDataset(String ps_command, Boolean pu_type, cls_parameter[] pa_parameters)
        {
            try
            {
                IDbCommand vu_cmd = cu_dataProvider.command(ps_command);
                foreach (cls_parameter vu_parameter in pa_parameters)
                    vu_cmd.Parameters.Add(cu_dataProvider.parameter(vu_parameter));

                DataSet vu_dts = new DataSet();
                IDbDataAdapter vu_adp = cu_dataProvider.adapter();
                vu_adp.SelectCommand = vu_cmd;

                if (pu_type.Equals(true))
                    vu_adp.SelectCommand.CommandType = CommandType.StoredProcedure;

                if (cu_transaction != null)
                {
                    vu_adp.SelectCommand.Connection = cu_transaction.Connection;
                    vu_adp.SelectCommand.Transaction = cu_transaction;
                    vu_adp.Fill(vu_dts);
                }
                else
                {
                    vu_adp.SelectCommand.Connection = getInstance().getConnection();
                    vu_adp.Fill(vu_dts);
                    closeConnection();
                }

                return vu_dts;
            }
            catch (Exception ve_exception)
            {
                throw ve_exception;
            }
        }

        #region Transaction

        public static void beginTransaction()
        {
            getInstance().getConnection();
            cu_transaction = cu_connection.BeginTransaction();
        }

        public static void commitTransaction()
        {
            cu_transaction.Commit();
            cu_transaction = null;
            closeConnection();
        }

        public static void rollbackTransaction()
        {
            cu_transaction.Rollback();
            cu_transaction = null;
            closeConnection();
        }

        #endregion

    }
}
