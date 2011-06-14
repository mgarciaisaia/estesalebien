using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VentaElectrodomesticos.Buscadores;
using VentaElectrodomesticos.Model;
using VentaElectrodomesticos.MetodosSQL;

namespace VentaElectrodomesticos.AbmEmpleado
{
    public partial class FormAbmEmpleado : Form
    {
        public FormAbmEmpleado()
        {
            InitializeComponent();
        }

        private void BuscarEmpleado_Click(object sender, EventArgs e)
        {
            BuscadorEmpleado buscador = new BuscadorEmpleado();
            buscador.ShowDialog(this);
            this.cargarEmpleado(buscador.getEmpleado());
        }

        private void cargarEmpleado(Empleado empleado)
        {
            tDNI.Text = empleado.dni.ToString();
            tApellido.Text = empleado.apellido;
            tNombre.Text = empleado.nombre;
            tMail.Text = empleado.mail;
            tDireccion.Text = empleado.direccion;
            cProvincia.SelectedValue = empleado.provincia;
            cSucursal.SelectedValue = empleado.sucursal;
            tTelefono.Text = empleado.telefono;
            cSucursal.SelectedValue = empleado.sucursal;
            cHabilitado.Checked = empleado.habilitado;
        }

        private void bAgregar_Click(object sender, EventArgs e)
        {
            String sql = "INSERT INTO " + ClaseSQL.tableName("Empleados") +
                " (DNI, Nombre, Apellido, Mail, Telefono, Direccion, Provincia, Tipo, Sucursal)";
            sql += " VALUES (";
            sql += "'" + tDNI.Text + "', '" + tNombre.Text + "', '" + tApellido.Text + "', '" + tMail.Text + "'";
            sql += ", '" + tTelefono.Text + "', '" + tDireccion.Text + "', " + cProvincia.SelectedValue + ", ";
            sql += cTipo.SelectedValue + ", " + cSucursal.SelectedValue + ");";

            try
            {
                ClaseSQL.getInstance().insertQuery(sql);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void bModificar_Click(object sender, EventArgs e)
        {

        }

        private void bEliminar_Click(object sender, EventArgs e)
        {

        }

        private void bLimpiarABM_Click(object sender, EventArgs e)
        {

        }
    }
}
