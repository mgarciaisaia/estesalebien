using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VentaElectrodomesticos.Model;
using VentaElectrodomesticos.MetodosSQL;
using System.Data;
using System.Data.SqlClient;

namespace VentaElectrodomesticos.DAO
{
    class Roles
    {
        public static Roles getInstance() { return new Roles(); }
        private Roles() { }

        public Rol rol(int codigo)
        {
            String query = "SELECT Codigo, Nombre, Habilitado FROM " + ClaseSQL.tableName("Roles") +
                " WHERE Codigo = " + codigo;

            ClaseSQL conexion = ClaseSQL.getInstance();
            conexion.Open();
            SqlDataReader reader = conexion.busquedaSQLDataReader(query);
            if (!reader.HasRows)
            {
                throw new Exception("No se encontro ningun rol con codigo " + codigo);
            }
            reader.Read();
            Rol rol = new Rol(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2));
            conexion.Close();
            return rol;
        }

        public Dictionary<int, Rol> roles()
        {
            Dictionary<int, Rol> roles = new Dictionary<int, Rol>();
            String query = "SELECT Codigo, Nombre, Habilitado FROM " + ClaseSQL.tableName("Roles");
            ClaseSQL conexion = ClaseSQL.getInstance();
            conexion.Open();
            SqlDataReader reader = conexion.busquedaSQLDataReader(query);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    roles[reader.GetInt32(0)] = new Rol(reader.GetInt32(0), reader.GetString(1), reader.GetByte(2) > 0);
                }
            }
            conexion.Close();
            return roles;
        }
    }
}
