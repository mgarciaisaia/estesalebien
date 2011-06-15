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

namespace VentaElectrodomesticos.AbmUsuario
{
    public partial class FormAbmUsuario : Form
    {
        private static int CODIGO = 0;
        private static int NOMBRE = 1;
        private static int PASSWORD = 2;
        private static int EMPLEADO = 3;
        private static int HABILITADO = 4;
        private static int INTENTOS = 5;
        
		ClaseSQL conexion;
		
		
		public FormAbmUsuario()
        {
            InitializeComponent();
			conexion = ClaseSQL.getInstance();
        }
		
        private void BuscarUsuario_Click(object sender, EventArgs e)
        {
            BuscadorUsuario buscador = new BuscadorEmpleado();
            buscador.ShowDialog(this);
            this.cargarUsuario(buscador.getEmpleado());
        }

        private void cargarUsuario(Model.Empleado empleado)
        {
            if (empleado != null)
            {
                tEmpleado.Text = empleado.dni.ToString();
                tEmpleado.Enabled = false;
                cHabilitado.Checked = usuario.habilitado;
            }

        }

        private void bAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                if (!this.hayAlgunTextboxVacio(tDNI, tNombre, tApellido, tMail,tDireccion,tTelefono, cProvincia, cTipo, cSucursal, cHabilitado))
                {
                    String[,] parametros = new String[2, 10];
                    String sp = "mayusculas_sin_espacios.sp_altaUsuario";
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
                    String sp = "mayusculas_sin_espacios.sp_modifUsuarios";
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
                        MessageBox.Show("Se ha modificado el Usuario.", "Success!");
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
            String sql = "DELETE FROM [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[Usuarios] " +
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


        private void FormAbmUsuario_Load(object sender, EventArgs e)
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

        private void bLimpiarUsuario_Click(object sender, EventArgs e)
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
