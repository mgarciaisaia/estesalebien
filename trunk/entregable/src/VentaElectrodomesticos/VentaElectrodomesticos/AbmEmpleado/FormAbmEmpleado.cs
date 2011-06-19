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

namespace VentaElectrodomesticos.AbmEmpleado
{
    public partial class FormAbmEmpleado : Form
    {
        private static int DNI = 0;
        private static int NOMBRE = 1;
        private static int APELLIDO = 2;
        private static int MAIL = 3;
        private static int TELEFONO = 4;
        private static int DIRECCION = 5;
        private static int PROVINCIA = 6;
        private static int TIPO = 7;
        private static int SUCURSAL = 8;
        private static int HABILITADO = 9;

        ClaseSQL conexion;
        private string[] provincias;
        private string[] tipos;
        private string[] sucursales;

        public FormAbmEmpleado()
        {
            InitializeComponent();
            conexion = ClaseSQL.getInstance();
            provincias = new string[25];
            tipos = new string[3];
            sucursales = new string[25];
        }

        private void BuscarEmpleado_Click(object sender, EventArgs e)
        {
            BuscadorEmpleado buscador = new BuscadorEmpleado();
            buscador.ShowDialog(this);
            this.cargarEmpleado(buscador.getEmpleado());
        }

        private void cargarEmpleado(Model.Empleado empleado)
        {
            if (empleado != null)
            {
                bAgregar.Enabled = false;
                bModificar.Enabled = true;
                bEliminar.Enabled = true;
                tDNI.Text = empleado.dni.ToString();
                tDNI.Enabled = false;
                tNombre.Text = empleado.nombre;
                tApellido.Text = empleado.apellido;
                tMail.Text = empleado.mail;
                tTelefono.Text = empleado.telefono;
                tDireccion.Text = empleado.direccion;
                cProvincia.SelectedIndex = empleado.provincia;
                cProvincia.Enabled = false;
                cTipo.SelectedIndex = empleado.tipo;
                cTipo.Enabled = false;
                cSucursal.SelectedIndex = empleado.sucursal;
                cSucursal.Enabled = false;
                cHabilitado.Checked = empleado.habilitado;
            }

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
                conexion.Open();
                if (!this.hayAlgunTextboxVacio(tDNI, tNombre, tApellido, tMail,tDireccion,tTelefono, cProvincia, cTipo, cSucursal, cHabilitado))
                {
                    String[,] parametros = new String[2, 10];
                    String sp = "mayusculas_sin_espacios.sp_altaEmpleado";
                    parametros[0, DNI] = "@DNI";
                    parametros[0, NOMBRE] = "@Nombre";
                    parametros[0, APELLIDO] = "@Apellido";
                    parametros[0, MAIL] = "@Mail";
                    parametros[0, DIRECCION] = "@direccion";
                    parametros[0, TELEFONO] = "@telefono";
                    parametros[0, PROVINCIA] = "@provincia";
                    parametros[0, TIPO] = "@tipo";
                    parametros[0, SUCURSAL] = "@sucursal"; 
                    parametros[0, HABILITADO] = "@habilitado";
                    
                    parametros[1, DNI] = tDNI.Text;
                    parametros[1, NOMBRE] = tNombre.Text;
                    parametros[1, APELLIDO] = tApellido.Text;
                    parametros[1, MAIL] = tMail.Text;
                    parametros[1, DIRECCION] = tDireccion.Text;
                    parametros[1, TELEFONO] = tTelefono.Text;
                    parametros[1, PROVINCIA] = cProvincia.SelectedIndex.ToString();
                    parametros[1, TIPO] = cTipo.SelectedIndex.ToString();
                    parametros[1, SUCURSAL] = cSucursal.SelectedIndex.ToString();
                    parametros[1, HABILITADO] = System.Convert.ToInt32(cHabilitado.Checked).ToString();
                    
                    
                    SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                    if (reader != null)
                    {
                        MessageBox.Show("Se ha dado de alta el cliente.", "Success!");
                        this.limpiar();
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos", "Warning!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Error app");
            }
            finally
            {
                conexion.Close();
            }
        }



        private bool hayAlgunTextboxVacio(TextBox tDNI, TextBox tNombre, TextBox tApellido, TextBox tMail, TextBox tDireccion, TextBox tTelefono, ComboBox cProvincia, ComboBox cTipo, ComboBox cSucursal, CheckBox chHabilitado)
        {
            return tDNI.Text.Equals("") || tNombre.Text.Equals("") || tApellido.Text.Equals("") || tMail.Text.Equals("") || tDireccion.Text.Equals("") || tTelefono.Text.Equals("") || cProvincia.Text.Equals("")|| cTipo.Text.Equals("")|| cSucursal.Text.Equals("");
        }

        private void bModificar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                if (!this.hayAlgunTextboxVacio(tDNI, tNombre, tApellido, tMail, tDireccion, tTelefono, cProvincia, cTipo, cSucursal, cHabilitado))
                {
                    String[,] parametros = new String[2, 7];
                    String sp = "mayusculas_sin_espacios.sp_modifEmpleados";
                    parametros[0, DNI] = "@DNI";
                    parametros[0, NOMBRE] = "@Nombre";
                    parametros[0, APELLIDO] = "@Apellido";
                    parametros[0, MAIL] = "@Mail";
                    parametros[0, DIRECCION] = "@direccion";
                    parametros[0, TELEFONO] = "@telefono";
                    parametros[0, 6] = "@habilitado";

                    parametros[1, DNI] = tDNI.Text;
                    parametros[1, NOMBRE] = tNombre.Text;
                    parametros[1, APELLIDO] = tApellido.Text;
                    parametros[1, MAIL] = tMail.Text;
                    parametros[1, DIRECCION] = tDireccion.Text;
                    parametros[1, TELEFONO] = tTelefono.Text;
                    parametros[1, 6] = System.Convert.ToInt32(cHabilitado.Checked).ToString();


                    SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                    if (reader != null)
                    {
                        MessageBox.Show("Se ha modificado el Empleado.", "Success!");
                        this.limpiar();
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos", "Warning!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Error app");
            }
            finally
            {
                conexion.Close();
            }
        }

        private void bEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                if (tDNI.Text !="")
                {
                    String[,] parametros = new String[2, 2];
                    String sp = "mayusculas_sin_espacios.sp_BajaEmpleados";
                    parametros[0, DNI] = "@DNI";
                    parametros[0, 1] = "@habilitado";
                    parametros[1, DNI] = tDNI.Text;
                    parametros[1, 1] = "0";

                    SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                    if (reader != null)
                    {
                        MessageBox.Show("Se ha dado de baja el Empleado.", "Success!");
                        this.limpiar();
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos", "Warning!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Error app");
            }
            finally
            {
                conexion.Close();
            }
        }

        

        private void rellenarComboBoxTipo()
        {
            cTipo.Items.Add("");
            foreach (TipoEmpleado tipo in TiposEmpleado.getInstance().tiposEmpleado())
            {
                cTipo.Items.Add(tipo.descripcion);
                tipos[tipo.codigo] = tipo.descripcion;
            }
        }

        private void rellenarComboBoxProvincia()
        {
            cProvincia.Items.Add("");
            foreach (Provincia provincia in Provincias.getInstance().list())
            {
                cProvincia.Items.Add(provincia.nombre);
                provincias[provincia.codigo] = provincia.nombre;
            }
        }


        private void rellenarComboBoxSucursal()
        {
            cSucursal.Items.Add("");
            foreach (Sucursal sucursal in Sucursales.getInstance().list())
            {
                cSucursal.Items.Add(provincias[sucursal.provincia]);
                sucursales[sucursal.provincia] = provincias[sucursal.provincia];
            }
        }


        private void FormAbmEmpleado_Load(object sender, EventArgs e)
        {
            this.rellenarComboBoxTipo();
            this.rellenarComboBoxProvincia();
            this.rellenarComboBoxSucursal();
                       
        }

        private void bLimpiarEmpleado_Click(object sender, EventArgs e)
        {
            this.limpiar();
        }


        private void limpiar()
        {
            tDNI.Clear();
            tDNI.Enabled = true;
            tNombre.Clear();
            tApellido.Clear();
            tMail.Clear();
            tTelefono.Clear();
            tDireccion.Clear();
            cProvincia.Text="";
            cProvincia.Enabled = true;
            cSucursal.Text = "";
            cSucursal.Enabled = true;
            cTipo.Text = "";
            cTipo.Enabled = true;
            cHabilitado.Checked = false;
            bModificar.Enabled = false;
            bEliminar.Enabled = false;
            bAgregar.Enabled = true;
                
        }

        private void tCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == 8 || Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

    }
}
