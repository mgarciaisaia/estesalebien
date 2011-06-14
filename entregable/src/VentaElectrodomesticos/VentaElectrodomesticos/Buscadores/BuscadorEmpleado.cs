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
        private Boolean buscarDeshabilitados;

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
            String query = "SELECT * FROM " + ClaseSQL.tableName("Empleados");
            
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

            ClaseSQL conexion = ClaseSQL.getInstance();
            conexion.Open();
            SqlDataReader data = conexion.busquedaSQLDataReader(query);
            DataTable tabla = new DataTable();
            tabla.Load(data);
            
            /*
             * FIXME: Por algun lado lei que tal vez sea mas simple pasarle una
             * ArrayList de Empleados al dgEmpleados para despues recuperar los
             * objetos
             * 
             * 
             * http://www.codeproject.com/KB/grid/dataGridview-DataReader.aspx
             * 
             */
            dgEmpleados.DataSource = tabla;
            dgEmpleados.Show();
            conexion.Close();

        }

        //FIXME: cambiar este evento: tiene que ser en el doble click, o agregar un boton "OK" que haga esto
        private void dgEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //FIXME: Ver como funciona el enumerator.
            IEnumerator enumerator = dgEmpleados.SelectedRows.GetEnumerator();
            enumerator.MoveNext();
            empleado = new Empleado(enumerator.Current);

        }

    }
}
