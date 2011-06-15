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
            String sql = "DELETE FROM [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[Empleados] " +
                        "WHERE dni=" + tDNI.Text;

            try
            {
                conexion.Open();
                if (conexion.insertQuery(sql) != 0 )
                {
                    MessageBox.Show("Se ha dado de Baja el cliente.", "Success!");
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
            cTipo.Items.Add("Vendedor");
            cTipo.Items.Add("Analista");
            tipos[1] = "Vendedor";
            tipos[2] = "Analista";
        }

        private void rellenarComboBoxProvincia()
        {
            try
            {
                cProvincia.Items.Add("");
                conexion.Open();
                SqlDataReader reader = conexion.busquedaSQLDataReader("SELECT codigo, Nombre FROM mayusculas_sin_espacios.provincias order by 1");

                while (reader.Read())
                {
                    String prov = reader[1].ToString().Trim();
                    int codigo = System.Convert.ToInt32(reader[0].ToString());
                    cProvincia.Items.Add(prov);
                    provincias[codigo] = prov;

                }
                reader.Close();

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


        private void rellenarComboBoxSucursal()
        {
            try
            {
                cSucursal.Items.Add("");
                conexion.Open();
                SqlDataReader reader = conexion.busquedaSQLDataReader("SELECT nombre, direccion FROM mayusculas_sin_espacios.sucursales left join mayusculas_sin_espacios.provincias on(codigo=provincia) order by provincia");

                while (reader.Read())
                {
                    String suc = reader[0].ToString().Trim() + " - " + reader[1].ToString().Trim();
                    cSucursal.Items.Add(suc);

                }
                reader.Close();

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


        private void FormAbmEmpleado_Load(object sender, EventArgs e)
        {
            this.rellenarComboBoxTipo();
            this.rellenarComboBoxProvincia();
            this.rellenarComboBoxSucursal();

            tNombre.Enabled = true;
            tApellido.Enabled = true;
            tDireccion.Enabled = true;
            tMail.Enabled = true;
            tTelefono.Enabled = true;
            tDireccion.Enabled = true;
            cProvincia.Enabled = true;
            cTipo.Enabled = true;
            cSucursal.Enabled = true;


        }

        private void bLimpiarEmpleado_Click(object sender, EventArgs e)
        {
            tDNI.Clear();
            tDNI.Enabled = true;
            tNombre.Clear();
            tApellido.Clear();
            tMail.Clear();
            tTelefono.Clear();
            tDireccion.Clear();
            cProvincia.Items.Clear();
            cProvincia.Enabled = true;
            this.rellenarComboBoxProvincia();
            cSucursal.Items.Clear();
            cSucursal.Enabled = true;
            this.rellenarComboBoxSucursal();
            cTipo.Items.Clear();
            cTipo.Enabled = true;
            this.rellenarComboBoxTipo();
            cHabilitado.Checked = false;
            
        }

    }
}
