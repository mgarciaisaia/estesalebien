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
        private string[,] roles;
        
        public FormAbmRol()
        {
            InitializeComponent();
            conexion = ClaseSQL.getInstance();
            roles = new string[20,2];
        }

        private void FormAbmRol_Load(object sender, EventArgs e)
        {
            rellenarComboBoxRoles();
            rellenarComboBoxFunciones();
            toolTip1.SetToolTip(bEliminar, "Seleccione una funcion de la lista para eliminarlo");
            toolTip1.SetToolTip(bAgregar,"Seleccione un rol y una funcion para agregar a la lista");
        }
        private void rellenarListFunciones()
        {
            cHabilitado.Checked = System.Convert.ToBoolean(System.Convert.ToInt32(roles[posicionRol(cRoles.Text), 1]));
            lFunciones.Clear();
            string query = "select funcionalidad  from mayusculas_sin_espacios.funcionalidadesrol where rol='" + posicionRol(cRoles.Text) + "'";
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
            foreach(Funcionalidad funcionalidad in Funcionalidades.getInstance().list()) {
                cFunciones.Items.Add(funcionalidad);
            }
            cFunciones.DisplayMember = "descripcion";
            cFunciones.ValueMember = "codigo";
        }
        private void rellenarComboBoxRoles()
        {
            conexion.Open();
            cRoles.Items.Add("");
            string query = "select codigo, nombre, habilitado from mayusculas_sin_espacios.roles";
            SqlDataReader readerRol = conexion.busquedaSQLDataReader(query);
            while (readerRol.Read())
            {
                cRoles.Items.Add(readerRol[1].ToString());
                roles[int.Parse(readerRol[0].ToString()),0] = readerRol[1].ToString();
                roles[int.Parse(readerRol[0].ToString()),1] = readerRol[2].ToString();
            }
            readerRol.Close();
            conexion.Close();
        }

        private void cRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cRoles.Text != "")
            {
                conexion.Open();
                this.rellenarListFunciones();
                conexion.Close();
            }
            
        }
        
        private int posicionRol(string rol)
        {
            cRoles.Text = rol;
            int i = 0;
            while (rol!=roles[i,0] && i<19)
            {i++;}
            return i;
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
                if (posicionRol(cRoles.Text) == 19 && cFunciones.Text!="")
                {
                    string query = "insert into mayusculas_sin_espacios.roles(nombre,habilitado) values ('" + cRoles.Text + "'," + (cHabilitado.Checked ? 1 : 0) + ")";
                    if (conexion.nonQuery(query) > 0)
                    {
                        MessageBox.Show("Se agrego el Rol Correctamente", "Alta");
                    }
                    conexion.Close();
                    this.rellenarComboBoxRoles();
                    conexion.Open();
                }
                
                if (cFunciones.Text != "" && (lFunciones.FindItemWithText(cFunciones.Text))==null)
                {
                    string query = "insert into mayusculas_sin_espacios.funcionalidadesRol(rol,funcionalidad) values (" + posicionRol(cRoles.Text) + "," + ((Funcionalidad)cFunciones.Items[cFunciones.SelectedIndex]).codigo + ")";
                    SqlDataReader reader = conexion.busquedaSQLDataReader(query);
                    if (reader != null)
                    {
                        reader.Close();
                        MessageBox.Show("Se agrego la Funcionalidad Correctamente", "Modificacion");
                        rellenarListFunciones();
                    }
                    
                }
                else { MessageBox.Show("El campo Funcion es Obligatorio o ya se encuentra en la lista"); }
                
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
               /*Eliminar seleccionando desde el combobox Funcion:
                * 
                * if (cFunciones.Text != "" && (lFunciones.FindItemWithText(cFunciones.Text)) != null)
                {
                    string query = "delete mayusculas_sin_espacios.funcionalidadesRol where rol='" + posicionRol(cRoles.Text) + "' and funcionalidad='" + posicionFuncion(cFunciones.Text) + "'";
                    SqlDataReader reader = conexion.busquedaSQLDataReader(query);
                    if (reader != null)
                    {
                        reader.Close();
                        MessageBox.Show("Se quito la Funcionalidad Correctamente", "Baja");
                        rellenarListFunciones();
                    }

                }
                else { MessageBox.Show("El campo Funcion es Obligatorio o no se encuentra en la lista"); }
               */

                //Eliminar seleccionando una funcion de la lista de funciones asignadas
                if (lFunciones.SelectedItems.Count != 0)
                {
                    string query = "delete mayusculas_sin_espacios.funcionalidadesRol where rol=" + posicionRol(cRoles.Text) + " and funcionalidad=" + Funcionalidades.getInstance().funcionalidad(lFunciones.SelectedItems[0].Text).codigo;
                    SqlDataReader reader = conexion.busquedaSQLDataReader(query);
                    if (reader != null)
                    {
                        reader.Close();
                        MessageBox.Show("Se quito la Funcionalidad Correctamente", "Baja");
                        rellenarListFunciones();
                    }

                }
                else { MessageBox.Show("El campo Funcion es Obligatorio o no se encuentra en la lista"); }
                conexion.Close();
            }
            else
            { MessageBox.Show("El campo Nombre es Obligatorio"); }
        }

        
    }
}