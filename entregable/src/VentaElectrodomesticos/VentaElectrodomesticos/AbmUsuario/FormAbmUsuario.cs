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
        private static int ROLES = 6;
        private string codigoUser ="";
        
		ClaseSQL conexion;
		
		
		public FormAbmUsuario()
        {
            InitializeComponent();
			conexion = ClaseSQL.getInstance();
            
        }

        private void FormAbmUsuario_Load(object sender, EventArgs e)
        {
            this.rellenarTablaRoles();
        }

        private void rellenarTablaRoles()
        {
            try
            {
                conexion.Open();
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
                
                
                /*
                 * while (reader.Read())
                {
                    String rol = reader[1].ToString().Trim();
                    int codigo = System.Convert.ToInt32(reader[0].ToString());
                    roles[codigo] = rol;
                }*/
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
                tEmpleado.Text = empleado.dni.ToString();
                tEmpleado.Enabled = false;
                String sql = "SELECT codigo,nombre, password, habilitado FROM [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[Usuarios] " +
                        "WHERE empleado=" + tEmpleado.Text;
                conexion.Open();
                SqlDataReader reader = conexion.busquedaSQLDataReader(sql);
                if (reader.Read())
                {
                    tNombre.Text=reader["nombre"].ToString();
                    tNombre.Enabled = false;
                    tPassword.Text = reader["Password"].ToString();
                    cHabilitado.Checked = System.Convert.ToBoolean(reader["habilitado"]);
                    codigoUser = reader["codigo"].ToString();

                }
                reader.Close();
                String query = "SELECT rol,nombre FROM [GD1C2011].[MAYUSCULAS_SIN_ESPACIOS].[Asignaciones] " +
                        "left join MAYUSCULAS_SIN_ESPACIOS.roles on(rol=codigo) WHERE usuario=" + codigoUser;
                SqlDataReader rolUser = conexion.busquedaSQLDataReader(query);
                int i = 0;
                while (rolUser.Read())
                {
                    i = System.Convert.ToInt32( rolUser["rol"]);
                    dgRoles.Rows[i-1].Cells["Seleccion"].Value = true;
                }
                rolUser.Close();
                conexion.Close();
            }

        }

        private void bLimpiarABM_Click(object sender, EventArgs e)
        {
            tNombre.Clear();
            tNombre.Enabled = true;
            tPassword.Clear();
            tEmpleado.Clear();
            cHabilitado.Checked = false;
            this.rellenarTablaRoles();
            
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
                    //parametros[0, CODIGO ] = "@Codigo";
                    parametros[0, 0] = "@Nombre";
                    parametros[0, 1] = "@Password";
                    parametros[0, 2] = "@Empleado";
                    parametros[0, 3] = "@Habilitado";

                    //parametros[1, CODIGO] = empleado;
                    parametros[1, 0] = tNombre.Text;
                    parametros[1, 1] = tPassword.Text;
                    parametros[1, 2] = tEmpleado.Text;
                    parametros[1, 3] = System.Convert.ToInt32(cHabilitado.Checked).ToString();


                    SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                    if (reader != null)
                    {
                        for(int i=0;i<dgRoles.RowCount;i++)
                        {
                            if (System.Convert.ToInt32(dgRoles.Rows[i].Cells["Seleccion"].Value) == 1)
                            {
                                string query = "insert into mayusculas_sin_espacios.asignaciones(Usuario,Rol) " +
                                        " select codigo," + dgRoles.Rows[i].Cells["Codigo"].Value.ToString() +
                                        " from mayusculas_sin_espacios.usuarios where nombre =" + tNombre.Text.ToString();
                                //MessageBox.Show(dgRoles.Rows[i].Cells[2].Value.ToString() + "   " + query);
                                if (conexion.insertQuery(query) != 0)
                                {
                                    MessageBox.Show("Se ha Modificado las asignaciones!", "Success!");
                                }
                            }
                        }


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

        private void bModificar_Click(object sender, EventArgs e)
        {

        }

        private void bEliminar_Click(object sender, EventArgs e)
        {

        }

        private bool hayAlgunTextboxVacio(TextBox tNombre, TextBox tPasswprd, TextBox tEmpleado,  CheckBox chHabilitado)
        {
            return tNombre.Text.Equals("") || tPasswprd.Text.Equals("") || tEmpleado.Text.Equals("") ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for(int i=0;i<dgRoles.RowCount;i++)
                        {
                            string query = "insert into mayusculas_sin_espacios.asignaciones(Usuario,Rol) " +
                                        "((select codigo,"+ dgRoles.Rows[i].Cells["Codigo"].Value.ToString() +
                                        " from mayusculas_sin_espacios.usuarios where nombre ='" + tNombre.Text.ToString()+"' )"+
                                        " except (select usuario, rol from mayusculas_sin_espacios.asignaciones left join "+
	                                        "mayusculas_sin_espacios.usuarios on (codigo=usuario) "+
                                        "where nombre ='" + tNombre.Text.ToString()+"'))";
                            try
                            {
                                conexion.Open();
                                if (conexion.insertQuery(query) != 0)
                                {
                                    MessageBox.Show("Se ha modificado las asignaciones.", "Success!");
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
}



        /*
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

        */