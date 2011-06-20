using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VentaElectrodomesticos.MetodosSQL;
using System.Data.SqlClient;

namespace VentaElectrodomesticos.DAO
{
    abstract class DAOCache
    {
        // Moverlo al constructor
        public void initialize()
        {
            ClaseSQL conexion = ClaseSQL.getInstance();
            conexion.Open();
            SqlDataReader reader = conexion.busquedaSQLDataReader(listQuery());

            while (reader.Read())
            {
                parseResult(reader);
            }
            reader.Close();
            conexion.Close();
        }

        protected abstract String listQuery();
        protected abstract void parseResult(SqlDataReader reader);
    }
}
