using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace VentaElectrodomesticos.MetodosSQL
{
    class ClaseSQL
    {
        private static string connectString = "Data Source=localhost\\SQLSERVER2005;Initial Catalog=GD1C2011; User ID=gd;Password=gd2011";
        private SqlConnection cnn;

        public ClaseSQL()
        {            
            cnn = new SqlConnection(connectString);
        }

        public void Open()
        {
            cnn.Open();
        }

        public void Close()
        {
            if (cnn.State != ConnectionState.Closed)
            {
                cnn.Close();
            }
        }

        public SqlDataReader busquedaSQLDataReader(String query)
        {
            //try
            //{
                SqlCommand command = new SqlCommand();
                command.Connection = cnn;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();

                return reader;
            //}
            //catch (SqlException ex)
            //{
            //    throw ex;
            //}
        }

        public SqlDataReader ejecutarStoredProcedure(string sp, String[,] parametros)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = cnn;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = sp;
                command.CommandTimeout = 10;

                for (int i = 0; i < parametros.GetLength(1); i++)
                {
                    command.Parameters.AddWithValue(parametros[0, i], parametros[1, i]);
                }
                return command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public String ejecutarStoredProcedureConRetorno(string sp, String[,] parametros, String retorno)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = cnn;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = sp;
                command.CommandTimeout = 10;

                for (int i = 0; i < parametros.GetLength(1); i++)
                {
                    command.Parameters.AddWithValue(parametros[0, i], parametros[1, i]);
                }
                command.Parameters.Add(retorno, SqlDbType.VarChar);
                command.Parameters[retorno].Direction = ParameterDirection.ReturnValue;
                command.ExecuteNonQuery();

                return command.Parameters[retorno].Value.ToString();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public SqlDataReader ejecutarFuncion(string func, String[,] parametros)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = cnn;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT "+func+"as functionresult";
                command.CommandTimeout = 10;

                for (int i = 0; i < parametros.GetLength(1); i++)
                {
                    command.Parameters.AddWithValue(parametros[0, i], parametros[1, i]);
                }
                return command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
