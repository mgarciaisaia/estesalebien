using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VentaElectrodomesticos.Model;
using VentaElectrodomesticos.MetodosSQL;
using System.Data.SqlClient;
using System.Collections;

namespace VentaElectrodomesticos.Buscadores
{
    public partial class BuscadorEmpleado : Form
    {
        private Empleado empleado;
        private Boolean buscarDeshabilitados = true;

        public BuscadorEmpleado()
        {
            // FIXME: aca hay que cargar los combos
            InitializeComponent();
        }

        public Empleado getEmpleado()
        {
            return empleado;
        }

        private void bBuscar_Click(object sender, EventArgs e)
        {
            String query = this.generateQuery();
            DataTable tabla = this.resultTable(query);
            dgEmpleados.DataSource = tabla;
            dgEmpleados.Show();
            
            
            
            /*
             * FIXME: Por algun lado lei que tal vez sea mas simple pasarle una
             * ArrayList de Empleados al dgEmpleados para despues recuperar los
             * objetos
             * 
             * 
             * http://www.codeproject.com/KB/grid/dataGridview-DataReader.aspx
             * 
             */

        }

        private DataTable resultTable(String query)
        {
            ClaseSQL conexion = ClaseSQL.getInstance();
            conexion.Open();
            SqlDataReader data = conexion.busquedaSQLDataReader(query);
            
            DataTable tabla = new DataTable();
            if (data.HasRows)
            {
                tabla.Load(data);
            }
            conexion.Close();
            return tabla;
        }

        private String generateQuery()
        {
            String query = "SELECT DNI, Nombre, Apellido, Mail, Telefono, Direccion, Provincia, Tipo, Sucursal, Habilitado" +
                " FROM " + ClaseSQL.tableName("Empleados");

            String where = "";

            if (!buscarDeshabilitados)
            {
                where += " AND Habilitado = 1";
            }

            if (tBusqDNI.Text.Length > 0)
            {
                where += " AND DNI = '" + tBusqDNI.Text + "'";
            }

            if (tBusqNombre.Text.Length > 0)
            {
                where += " AND Nombre LIKE '%" + tBusqNombre.Text + "%'";
            }

            if (tBusqApellido.Text.Length > 0)
            {
                where += " AND Apellido LIKE '%" + tBusqApellido.Text + "%'";
            }

            if (cBusqProvincia.SelectedText.Length > 0)
            {
                where += " AND Provincia = " + cBusqProvincia.SelectedValue;
            }

            if (cBusqSucursal.SelectedText.Length > 0)
            {
                where += " AND Sucursal = " + cBusqSucursal.SelectedValue;
            }

            if (cBusqTipo.SelectedText.Length > 0)
            {
                where += " AND Tipo = " + cBusqTipo.SelectedValue;
            }


            if (where.Length > 0)
            {
                where = where.Substring(5);
            }

            query += " WHERE " + where;
            return query;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            DataGridViewCellCollection cells = dgEmpleados.SelectedRows[0].Cells;
            empleado = this.empleadoSeleccionado(cells);
            this.Close();
        }

        private Empleado empleadoSeleccionado(DataGridViewCellCollection cells)
        {
            Empleado empleado = new Empleado();
            empleado.dni = (decimal) cells[0].Value;
            empleado.nombre = cells[1].Value.ToString();
            empleado.apellido = cells[2].Value.ToString();
            empleado.mail = cells[3].Value.ToString();
            empleado.telefono = cells[4].Value.ToString();
            empleado.direccion = cells[5].Value.ToString();
            //FIXME: deberiamos mostrar los strings y guardar los códigos, usando a los DAOs como conversores
            empleado.provincia = (byte) cells[6].Value;
            empleado.tipo = (byte)cells[7].Value;
            empleado.sucursal = (byte) cells[8].Value;
            empleado.habilitado = (byte)cells[9].Value > 0;


            return empleado;
        }

    }
}
