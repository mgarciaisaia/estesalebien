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

namespace VentaElectrodomesticos.AbmCliente
{
    public partial class FormAbmCliente : Form
    {
        private static int DNI = 0;
        private static int NOMBRE = 1;
        private static int APELLIDO = 2;
        private static int MAIL = 3;
        private static int TELEFONO = 4;
        private static int DIRECCION = 5;
        private static int PROVINCIA = 6;
        private static int HABILITADO = 7;

        ClaseSQL conexion;
        private string[] provincias;
        
        public FormAbmCliente()
        {
            InitializeComponent();
            conexion = ClaseSQL.getInstance();
            provincias = new string[25];
        }

        private void BuscarCliente_Click(object sender, EventArgs e)
        {
            BuscadorCliente buscador = new BuscadorCliente();
            buscador.ShowDialog(this);
            this.cargarCliente(buscador.getCliente());
        }

        private void cargarCliente(Model.Cliente cliente)
        {
            if (cliente != null)
            {
                tDNI.Text = cliente.dni.ToString();
                tDNI.Enabled = false;
                tNombre.Text = cliente.nombre;
                tApellido.Text = cliente.apellido;
                tMail.Text = cliente.mail;
                tTelefono.Text = cliente.telefono;
                tDireccion.Text = cliente.direccion;
                cProvincia.SelectedIndex = cliente.provincia;
                cProvincia.Enabled = false;
                cHabilitado.Checked = cliente.habilitado;
            }

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


        
        private void FormAbmCliente_Load(object sender, EventArgs e)
        {
            this.rellenarComboBoxProvincia();
            
            tNombre.Enabled = true;
            tApellido.Enabled = true;
            tDireccion.Enabled = true;
            tMail.Enabled = true;
            tTelefono.Enabled = true;
            tDireccion.Enabled = true;
            cProvincia.Enabled = true;
            

        }

        private void bLimpiarCliente_Click(object sender, EventArgs e)
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
            cHabilitado.Checked = false;

        }

        private void bAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                if (!this.hayAlgunTextboxVacio(tDNI, tNombre, tApellido, tMail, tDireccion, tTelefono, cProvincia, cHabilitado))
                {
                    String[,] parametros = new String[2, 8];
                    String sp = "mayusculas_sin_espacios.sp_altaCliente";
                    parametros[0, DNI] = "@DNI";
                    parametros[0, NOMBRE] = "@Nombre";
                    parametros[0, APELLIDO] = "@Apellido";
                    parametros[0, MAIL] = "@Mail";
                    parametros[0, DIRECCION] = "@direccion";
                    parametros[0, TELEFONO] = "@telefono";
                    parametros[0, PROVINCIA] = "@provincia";
                    parametros[0, HABILITADO] = "@habilitado";

                    parametros[1, DNI] = tDNI.Text;
                    parametros[1, NOMBRE] = tNombre.Text;
                    parametros[1, APELLIDO] = tApellido.Text;
                    parametros[1, MAIL] = tMail.Text;
                    parametros[1, DIRECCION] = tDireccion.Text;
                    parametros[1, TELEFONO] = tTelefono.Text;
                    parametros[1, PROVINCIA] = cProvincia.SelectedIndex.ToString();
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



        private bool hayAlgunTextboxVacio(TextBox tDNI, TextBox tNombre, TextBox tApellido, TextBox tMail, TextBox tDireccion, TextBox tTelefono, ComboBox cProvincia, CheckBox chHabilitado)
        {
            return tDNI.Text.Equals("") || tNombre.Text.Equals("") || tApellido.Text.Equals("") || tMail.Text.Equals("") || tDireccion.Text.Equals("") || tTelefono.Text.Equals("") || cProvincia.Text.Equals("") ;
        }

        private void bModificar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                if (!this.hayAlgunTextboxVacio(tDNI, tNombre, tApellido, tMail, tDireccion, tTelefono, cProvincia, cHabilitado))
                {
                    String[,] parametros = new String[2, 7];
                    String sp = "mayusculas_sin_espacios.sp_modifClientes";
                    parametros[0, DNI] = "@DNI";
                    parametros[0, NOMBRE] = "@Nombre";
                    parametros[0, APELLIDO] = "@Apellido";
                    parametros[0, MAIL] = "@Mail";
                    parametros[0, TELEFONO] = "@telefono";
                    parametros[0, DIRECCION] = "@direccion";
                    parametros[0, 6] = "@habilitado";

                    parametros[1, DNI] = tDNI.Text;
                    parametros[1, NOMBRE] = tNombre.Text;
                    parametros[1, APELLIDO] = tApellido.Text;
                    parametros[1, MAIL] = tMail.Text;
                    parametros[1, TELEFONO] = tTelefono.Text;
                    parametros[1, DIRECCION] = tDireccion.Text;
                    parametros[1, 6] = System.Convert.ToInt32(cHabilitado.Checked).ToString();


                    SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                    if (reader != null)
                    {
                        MessageBox.Show("Se ha modificado el Cliente.", "Success!");
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
                if (tDNI.Text != "")
                {
                    String[,] parametros = new String[2, 2];
                    String sp = "mayusculas_sin_espacios.sp_BajaClientes";
                    parametros[0, DNI] = "@DNI";
                    parametros[0, 1] = "@habilitado";
                    parametros[1, DNI] = tDNI.Text;
                    parametros[1, 1] = "0";

                    SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                    if (reader != null)
                    {
                        MessageBox.Show("Se ha dado de baja el Cliente.", "Success!");
                        cHabilitado.Checked = false;
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

    }
}
