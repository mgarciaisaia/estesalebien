using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VentaElectrodomesticos.MetodosSQL;
using VentaElectrodomesticos.Model;
using System.Data.SqlClient;

namespace VentaElectrodomesticos.DAO
{
    class Stocks
    {
        public static Stocks getInstance()
        {
            return new Stocks();
        }

        public int stock(Producto producto, Sucursal sucursal)
        {
            ClaseSQL conexion = ClaseSQL.getInstance();
            String query = "SELECT Cantidad FROM " + ClaseSQL.tableName("Stocks") +
                " WHERE Producto = " + producto.codigo + " AND Sucursal = " + sucursal.provincia;
            conexion.Open();
            int stock = (int)conexion.scalarQuery(query);
            conexion.Close();
            return stock;
        }

        public void nuevoMovimiento(Producto producto, Empleado auditor, byte codigoSucursal, decimal cantidad)
        {
            ClaseSQL conexion = ClaseSQL.getInstance();
            String query = "INSERT INTO " + ClaseSQL.tableName("MovimientosStock") + " (Producto, Auditor, Sucursal, Cantidad, Fecha) " +
                " VALUES (" + producto.codigo + ", " + auditor.dni + ", " + codigoSucursal + ", " + cantidad + ", @Date)";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Date", DateTime.Now);
            conexion.Open();
            conexion.insertQuery(command);
            conexion.Close();
        }
    }
}
