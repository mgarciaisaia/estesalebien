using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using VentaElectrodomesticos.Utils;
using VentaElectrodomesticos.Buscadores;
using VentaElectrodomesticos.Model;
using VentaElectrodomesticos.MetodosSQL;

namespace VentaElectrodomesticos.AbmUsuario
{
    public partial class FormAbmUsuario : Form
    {
        private static int NOMBRE = 0;
        private static int PASSWORD = 1;
        private static int EMPLEADO = 2;
        private static int HABILITADO = 3;
        private string codigoUser ="";
        private Hasher hasher = new Hasher(new SHA256Managed());
        private bool passCambio;
                
		ClaseSQL conexion;
				
		public FormAbmUsuario()
        {
            InitializeComponent();
			conexion = ClaseSQL.getInstance();
            
        }

        private void FormAbmUsuario_Load(object sender, EventArgs e)
        {
            conexion.Open();
            this.rellenarTablaRoles();
            conexion.Close();
            passCambio = false;
        }

        private void rellenarTablaRoles()
        {
            try
            {
                SqlDataReader reader = conexion.busquedaSQLDataReader("SELECT Codigo, Nombre FROM mayusculas_sin_espacios.Roles where habilitado='1' order by 1");
                DataTable tabla = new DataTable();
                if (reader.HasRows)
                {
                    tabla.Load(reader);
                }

                dgRoles.DataSource = tabla;
                dgRoles.Columns["Nombre"].Width = 158;
                dgRoles.Columns["Nombre"].ReadOnly = true;
                dgRoles.Columns["Codigo"].Visible = false;
                dgRoles.Show();
                
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
            
        }

        private void BuscarEmpleado_Click(object sender, EventArgs e)
        {
            BuscadorUsuario buscador = new BuscadorUsuario();
            buscador.ShowDialog(this);
            //this.rellenarTablaRoles();
            this.cargarEmpleado(buscador.getEmpleado());
        }

        private void cargarEmpleado(Model.Empleado empleado)
        {
            if (empleado != null)
            {
                tEmpleado.Text = empleado.dni.ToString();
                tEmpleado.Enabled = false;
                String sql = "SELECT codigo,nombre, password, habilitado FROM [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[Usuarios] " +
                        "WHERE empleado=" + tEmpleado.Text;
                conexion.Open();
                SqlDataReader reader = conexion.busquedaSQLDataReader(sql);
                if (reader.Read())
                {
                    tNombre.Text = reader["nombre"].ToString();
                    tNombre.Enabled = false;
                    tPassword.Text = reader["Password"].ToString();
                    passCambio = false;
                    cHabilitado.Checked = System.Convert.ToBoolean(reader["habilitado"]);
                    codigoUser = reader["codigo"].ToString();

                }
                
                reader.Close();
                if (codigoUser != "")
                {

                    String query = "SELECT rol,nombre FROM [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[Asignaciones] " +
                            "left join MAYUSCULAS_SIN_ESPACIOS.roles on(rol=codigo) WHERE usuario=" + codigoUser;
                    SqlDataReader rolUser = conexion.busquedaSQLDataReader(query);
                    while (rolUser.Read())
                    {
                        bool flag = true;
                        int i = 0;
                        while (flag && i<dgRoles.Rows.Count)
                        {
                            if (dgRoles.Rows[i].Cells["codigo"].Value.Equals(rolUser["rol"]))
                                dgRoles.Rows[i].Cells["Seleccion"].Value = true;
                            
                            //System.Convert.ToInt32(rolUser["rol"]);
                            i++;
                        }
                    }
                    rolUser.Close();
                }
                conexion.Close();
            }

        }

        private void bLimpiarABM_Click(object sender, EventArgs e)
        {
            conexion.Open();
            this.limpiar();
            conexion.Close();
        }

        private void limpiar()
        {
            tNombre.Clear();
            tNombre.Enabled = true;
            tPassword.Clear();
            tEmpleado.Clear();
            tEmpleado.Enabled = true;
            cHabilitado.Checked = false;
            this.rellenarTablaRoles();
            passCambio = false;
        }

        private void actualizarRoles()
        {
            for (int i = 0; i < dgRoles.RowCount; i++)
            {
                    String[,] parametros = new String[2, 3];
                    String sp = "mayusculas_sin_espacios.[sp_asignacion]";
                    parametros[0, 0] = "@user";
                    parametros[0, 1] = "@rol";
                    parametros[0, 2] = "@habilitado";

                    parametros[1, 0] = tNombre.Text.ToString();
                    parametros[1, 1] = dgRoles.Rows[i].Cells["Codigo"].Value.ToString();
                    parametros[1, 2] = (System.Convert.ToInt32(dgRoles.Rows[i].Cells["Seleccion"].Value)).ToString();

                    SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                    reader.Close();
            }
        }

        private void bModificar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                if (!this.hayAlgunTextboxVacio(tNombre, tPassword, tEmpleado, cHabilitado))
                {
                    String[,] parametros = new String[2, 3];
                    String sp = "mayusculas_sin_espacios.sp_ModifUsuarios";
                    parametros[0, NOMBRE] = "@Nombre";
                    parametros[0, PASSWORD] = "@Password";
                    parametros[0, 2] = "@habilitado";

                    parametros[1, NOMBRE] = tNombre.Text.ToString();
                    if (passCambio)
                        parametros[1, PASSWORD] = hasher.hash(tPassword.Text);
                    else
                        parametros[1, PASSWORD] = "null";
                    parametros[1, 2] = System.Convert.ToInt32(cHabilitado.Checked).ToString();
                    
                    SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                    if (reader != null)
                    {
                        reader.Close();
                        this.actualizarRoles();
                        MessageBox.Show("Se ha Modificado el Usuario.", "Success!");
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
                if (tNombre.Text != "")
                {
                    String[,] parametros = new String[2, 2];
                    String sp = "mayusculas_sin_espacios.sp_BajaUsuarios";
                    parametros[0, NOMBRE] = "@Nombre";
                    parametros[0, 1] = "@habilitado";
                    parametros[1, NOMBRE] = tNombre.Text;
                    parametros[1, 1] = "0";

                    SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                    if (reader != null)
                    {
                        reader.Close();
                        MessageBox.Show("Se ha dado de baja el Usuario.", "Success!");
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

        private bool hayAlgunTextboxVacio(TextBox tNombre, TextBox tPasswprd, TextBox tEmpleado,  CheckBox chHabilitado)
        {
            return tNombre.Text.Equals("") || tPasswprd.Text.Equals("") || tEmpleado.Text.Equals("") ;
        }

        private void bAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                if (!this.hayAlgunTextboxVacio(tNombre, tPassword, tEmpleado, cHabilitado))
                {
                    String[,] parametros = new String[2, 4];
                    String sp = "mayusculas_sin_espacios.sp_altaUsuario";
                    parametros[0, NOMBRE] = "@Nombre";
                    parametros[0, PASSWORD] = "@Password";
                    parametros[0, EMPLEADO] = "@Empleado";
                    parametros[0, HABILITADO] = "@habilitado";
                    
                    parametros[1, NOMBRE] = tNombre.Text.ToString();
                    parametros[1, PASSWORD] = hasher.hash(tPassword.Text);
                    parametros[1, EMPLEADO] = tEmpleado.Text.ToString();
                    parametros[1, HABILITADO] = System.Convert.ToInt32(cHabilitado.Checked).ToString();
                   
                    SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                    if (reader != null)
                    {
                        reader.Close();
                        this.actualizarRoles();
                        MessageBox.Show("Se ha dado de alta el Usuario.", "Success!");
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

        private void tPassword_TextChanged(object sender, EventArgs e)
        {
            passCambio = true;
        }       
    }
}