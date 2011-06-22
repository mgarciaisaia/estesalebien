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
    class Productos
    {
        public static Productos getInstance() { return new Productos(); }
        private Productos() { }
        public DataTable table(String codigo, String nombre, String marca, String categoria, decimal precioMinimo, decimal precioMaximo, Boolean buscarDeshabilitados)
        {
            String query = "SELECT Codigo, Nombre, Descripcion, CodigoCategoria, Categoria, Precio, CodigoMarca, Marca, Habilitado FROM " + ClaseSQL.tableName("ProductosCompletos");

            String where = "";

            if (codigo.Length > 0)
            {
                where += " AND Codigo = " + codigo;
            }

            if (nombre.Length > 0)
            {
                where += " AND Nombre LIKE '%" + nombre + "%'";
            }

            if (marca.Length > 0)
            {
                where += " AND CodigoMarca = " + marca;
            }

            if (categoria.Length > 0)
            {
                where += " AND CodigoCategoria = " + categoria;
            }

            if (precioMinimo > 0)
            {
                where += " AND Precio >= " + precioMinimo;
            }

            if (precioMaximo > 0)
            {
                where += " AND Precio <= " + precioMaximo;
            }

            if (!buscarDeshabilitados)
            {
                where += " AND Habilitado = 1";
            }

            if (where.Length > 0)
            {
                query += " WHERE " + where.Substring(5);
            }

            ClaseSQL conexion = ClaseSQL.getInstance();
            return conexion.selectDataTable(query);
        }

        public Producto producto(int codigo)
        {
            String query = "SELECT Codigo, Nombre, Descripcion, Categoria, Precio, Habilitado, Marca FROM " + ClaseSQL.tableName("Productos") +
                " WHERE Codigo = " + codigo;

            ClaseSQL conexion = ClaseSQL.getInstance();
            conexion.Open();
            SqlDataReader reader = conexion.busquedaSQLDataReader(query);
            if (!reader.HasRows)
            {
                throw new Exception("No se encontro ningun producto con codigo " + codigo);
            }
            reader.Read();
            Producto producto = new Producto();
            producto.codigo = reader.GetInt32(0);
            producto.nombre = reader.GetString(1);
            producto.descripcion = reader.GetString(2);
            producto.categoria = reader.GetInt32(3);
            producto.precio = reader.GetDouble(4);
            producto.habilitado = reader.GetByte(5) > 0;
            producto.marca = reader.GetInt32(6);
            conexion.Close();
            return producto;
        }

        public void inhabilitar(int codigo)
        {
            String query = "UPDATE " + ClaseSQL.tableName("Productos") + " SET Habilitado = 0 WHERE" +
                " Codigo = " + codigo;
            ClaseSQL conexion = ClaseSQL.getInstance();
            conexion.Open();
            if (conexion.nonQuery(query) != 0)
                System.Windows.Forms.MessageBox.Show("Baja Correcta");
            conexion.Close();
        }

        public void actualizar(string codigo, string nombre, string descripcion, decimal precio, int marca, bool habilitado)
        {
            try
            {
                String query = "UPDATE " + ClaseSQL.tableName("Productos") + " SET Nombre = '" + nombre + "', Descripcion = '" +
                    descripcion + "', Precio = " + precio.ToString() + ", Marca = " + marca + ", Habilitado = " + (habilitado ? 1 : 0) +
                    " WHERE Codigo = " + codigo;
                ClaseSQL conexion = ClaseSQL.getInstance();
                conexion.Open();
                if (conexion.nonQuery(query) != 0)
                    System.Windows.Forms.MessageBox.Show("Modificacion Correcta");
                conexion.Close();                
            }
            catch (Exception exception)
            {
                throw new Exception("No se pudo modificar el Producto con codigo " + codigo + ":\n\n" + exception.Message);
            }
        }

        public void insertar(string codigo, string nombre, string descripcion, decimal precio, int marca, String categoria, bool habilitado)
        {
            try
            {
                String query = "INSERT INTO " + ClaseSQL.tableName("Productos") + " (Nombre, Descripcion, Precio, Marca," +
                    " Categoria, Habilitado) VALUES ('" + nombre + "', '" + descripcion + "', " + precio + ", " + marca +
                    ", " + categoria + ", " + (habilitado ? 1 : 0) + ")";
                ClaseSQL conexion = ClaseSQL.getInstance();
                conexion.Open();
                if (conexion.nonQuery(query) != 0)
                    System.Windows.Forms.MessageBox.Show("Alta Correcta");
                conexion.Close();
            }
            catch (Exception exception)
            {
                throw new Exception("No se pudo dar de alta el Producto con codigo " + codigo + ":\n\n" + exception.Message);
            }
        }
    }
}
