using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace VentaElectrodomesticos.MetodosSQL
{
    class ClaseSQL
    {
        //FIXME: esto hay que parametrizarlo
        private static String schema = "mayusculas_sin_espacios";
        private static string connectString = "Data Source=localhost\\SQLSERVER2005;Initial Catalog=GD1C2011; User ID=gd;Password=gd2011";
        private SqlConnection cnn;
        private static ClaseSQL instance;

        private ClaseSQL()
        {
            cnn = new SqlConnection(connectString);
        }

        public static ClaseSQL getInstance()
        {
            if (instance == null)
            {
                instance = new ClaseSQL();
            }
            return instance;
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

        public static String getSchema()
        {
            return schema;
        }

        public static void setSchema(String schemaName)
        {
            schema = schemaName;
        }

        public static String tableName(String tableName)
        {
            return schema + "." + tableName;
        }

        public SqlDataReader busquedaSQLDataReader(String query)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = cnn;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();

                return reader;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public Object scalarQuery(String query)
        {
            SqlCommand command = new SqlCommand(query, cnn);
            return command.ExecuteScalar();
        }

        public int insertQuery(String query)
        {
            SqlCommand command = new SqlCommand(query, cnn);
            return command.ExecuteNonQuery();
        }

        public int insertQuery(SqlCommand command)
        {
            command.Connection = cnn;
            return command.ExecuteNonQuery();
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
                command.CommandText = "SELECT " + func ;
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
