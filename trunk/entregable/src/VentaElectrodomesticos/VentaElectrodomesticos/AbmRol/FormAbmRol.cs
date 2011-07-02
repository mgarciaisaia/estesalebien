using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VentaElectrodomesticos.Buscadores;
using VentaElectrodomesticos.Model;
using VentaElectrodomesticos.MetodosSQL;
using VentaElectrodomesticos.DAO;

namespace VentaElectrodomesticos.AbmRol
{
    public partial class FormAbmRol : Form
    {
        ClaseSQL conexion;
        private Dictionary<int, Rol> roles;

        public FormAbmRol()
        {
            InitializeComponent();
            conexion = ClaseSQL.getInstance();
            roles = Roles.getInstance().roles();
        }

        private void FormAbmRol_Load(object sender, EventArgs e)
        {
            rellenarComboBoxRoles();
            rellenarComboBoxFunciones();
            toolTip1.SetToolTip(bEliminar, "Seleccione una funcion de la lista para eliminarlo");
            toolTip1.SetToolTip(bAgregar, "Seleccione un rol y una funcion para agregar a la lista");
        }
        private void rellenarListFunciones()
        {
            cHabilitado.Checked = ((Rol)cRoles.SelectedItem).habilitado;
            lFunciones.Clear();
            String query = "select funcionalidad from mayusculas_sin_espacios.funcionalidadesrol where rol=" + ((Rol)cRoles.SelectedItem).codigo;
            SqlDataReader reader = conexion.busquedaSQLDataReader(query);
            while (reader.Read())
            {
                lFunciones.Items.Add(Funcionalidades.getInstance().funcionalidad((byte)reader[0]).descripcion);
            }
            reader.Close();
        }

        private void rellenarComboBoxFunciones()
        {
            cFunciones.Items.Clear();
            cFunciones.Items.Add(new Funcionalidad(0, ""));
            foreach (Funcionalidad funcionalidad in Funcionalidades.getInstance().list())
            {
                cFunciones.Items.Add(funcionalidad);
            }
            cFunciones.DisplayMember = "descripcion";
            cFunciones.ValueMember = "codigo";
        }
        private void rellenarComboBoxRoles()
        {
            conexion.Open();
            cRoles.Items.Clear();
            cRoles.Items.Add(new Rol());
            String query = "select codigo, nombre, habilitado from mayusculas_sin_espacios.roles";
            SqlDataReader readerRol = conexion.busquedaSQLDataReader(query);
            while (readerRol.Read())
            {
                cRoles.Items.Add(new Rol((int)readerRol[0], (String)readerRol[1], ((byte)readerRol[2]) > 0));
            }
            cRoles.DisplayMember = "nombre";
            cRoles.ValueMember = "codigo";
            readerRol.Close();
            conexion.Close();
        }

        private void cRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cRoles.SelectedIndex > 0)
            {
                conexion.Open();
                this.rellenarListFunciones();
                conexion.Close();
            }
            else
            {
                lFunciones.Clear();
            }

        }

        private void bLimpiarABM_Click(object sender, EventArgs e)
        {
            this.limpiar();
        }

        private void limpiar()
        {
            lFunciones.Clear();
            cRoles.Items.Clear();
            cRoles.Text = "";
            cFunciones.Text = "";
            cHabilitado.Checked = false;
            this.rellenarComboBoxRoles();
        }

        private void bAgregar_Click(object sender, EventArgs e)
        {
            if (cRoles.Text != "")
            {
                conexion.Open();
                if (cRoles.SelectedIndex < 0)
                {
                    string query = "insert into mayusculas_sin_espacios.roles(nombre,habilitado) values ('" + cRoles.Text + "'," + (cHabilitado.Checked ? 1 : 0) + ")";
                    if (conexion.nonQuery(query) < 1)
                    {
                        MessageBox.Show("No se pudo agregar el rol con nombre " + cRoles.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conexion.Close();
                        return;
                    }
                    conexion.Close();
                    this.rellenarComboBoxRoles();
                    foreach (Rol rol in cRoles.Items)
                    {
                        if (rol.nombre.Equals(cRoles.Text))
                        {
                            cRoles.SelectedItem = rol;
                            break;
                        }
                    }
                    conexion.Open();
                }
                else if (cHabilitado.Checked != ((Rol)cRoles.SelectedItem).habilitado)
                {
                    String query = "UPDATE " + ClaseSQL.tableName("Roles") + " SET Habilitado = " + (cHabilitado.Checked ? 1 : 0) + " WHERE Codigo = " + ((Rol)cRoles.SelectedItem).codigo;
                    if (conexion.nonQuery(query) < 1)
                    {
                        MessageBox.Show("No se pudo cambiar la habilitacion del rol " + cRoles.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conexion.Close();
                        return;
                    }
                    conexion.Close();
                    this.rellenarComboBoxRoles();
                    conexion.Open();
                }

                if (cFunciones.SelectedIndex > 0) // el indice 0 esta vacio
                {
                    if (lFunciones.FindItemWithText(cFunciones.Text) != null)
                    { //ya tiene asignada esa funcionalidad
                        MessageBox.Show("El rol " + cRoles.Text + " ya tiene asignada la funcionalidad " + cFunciones.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conexion.Close();
                        return;
                    }
                    string query = "insert into mayusculas_sin_espacios.funcionalidadesRol(rol,funcionalidad) values (" + ((Rol)cRoles.SelectedItem).codigo + "," + ((Funcionalidad)cFunciones.Items[cFunciones.SelectedIndex]).codigo + ")";
                    if (conexion.nonQuery(query) == 0)
                    {
                        MessageBox.Show("No se pudo asignar la funcionalidad " + cFunciones.Text + " al rol " + cRoles.Text + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conexion.Close();
                        return;
                    }
                    rellenarListFunciones();
                }

                conexion.Close();
            }
            else
            { MessageBox.Show("El campo Nombre es Obligatorio"); }
        }

        private void bEliminar_Click(object sender, EventArgs e)
        {
            if (cRoles.Text != "")
            {
                conexion.Open();

                //Eliminar seleccionando una funcion de la lista de funciones asignadas
                if (lFunciones.SelectedItems.Count != 0)
                {
                    String query = "delete mayusculas_sin_espacios.funcionalidadesRol where rol=" + ((Rol)cRoles.SelectedItem).codigo + " and funcionalidad=" + Funcionalidades.getInstance().funcionalidad(lFunciones.SelectedItems[0].Text).codigo;
                    if (conexion.nonQuery(query) < 1)
                    {
                        MessageBox.Show("No se pudo eliminar la funcionalidad " + lFunciones.SelectedItems[0].Text + " del rol " + cRoles.Text + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conexion.Close();
                        return;
                    }
                    rellenarListFunciones();
                }
                else
                {
                    MessageBox.Show("El campo Funcion es Obligatorio o no se encuentra en la lista");
                }
                conexion.Close();
            }
            else
            {
                MessageBox.Show("El campo Nombre es Obligatorio");
            }
        }
    }
}