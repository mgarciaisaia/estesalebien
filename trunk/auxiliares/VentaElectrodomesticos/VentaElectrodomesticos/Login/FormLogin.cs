using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VentaElectrodomesticos.MetodosSQL;


namespace VentaElectrodomesticos.Login
{
    public partial class FormLogin : Form
    {
        
        private ClaseSQL sql;
        private int intentos = 0;
        private bool modif = false;


        private bool correcto ;
        public bool CORRECTO
        {
            get { return correcto; }
        }
        
        private string username="";
        public string USERNAME
        {
            get { return username; }            
        }
        private string[] funciones ={};
        public string[] FUNCIONES
        {
            get { return funciones; }
        }
        

        public FormLogin()
        {
            InitializeComponent();
            sql = new ClaseSQL();
            correcto = false;

        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            modif = false;
            if (txtUser.Text != "")
            {
                sql.Open();
                String defaultquery = " SELECT * FROM [MAYUSCULAS_SIN_ESPACIOS].USUARIOS ";
                String query = " WHERE us_username = '" + txtUser.Text.ToString() + "'";
                query = defaultquery + query;
                SqlDataReader reader = sql.busquedaSQLDataReader(query);
                if (reader.Read())
                {
                    intentos = System.Convert.ToInt16(reader[3].ToString());
                    if (intentos < 3)
                    {
                        if ((txtUser.Text.ToLower() == reader[1].ToString()) && (txtPass.Text == reader[2].ToString()))
                        {
                            correcto = true;
                            username = txtUser.Text.ToLower();
                            char[] delimiterChars = { '|' };
                            funciones = reader[4].ToString().Split(delimiterChars);
                            if (System.Convert.ToInt16(reader[3].ToString()) != 0)
                            {
                                intentos = 0;
                                modif = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("INCORRECTO ");
                            intentos++;
                            modif = true;
                        }
                    }else { MessageBox.Show("Usuario bloqueado"); }
                }
                else { MessageBox.Show(txtUser.Text.ToString() + " No existe en la base"); }
                reader.Close();
                ///Modifico en la base la cantidad de intentos////
                if (modif)
                {
                    String[,] parametros = new String[2, 2];
                    String sp = "[MAYUSCULAS_SIN_ESPACIOS].sp_MODIFINTENTOS";
                    parametros[0, 0] = "@USERNAME";
                    parametros[0, 1] = "@INTENTOS";
                    parametros[1, 0] = txtUser.Text;
                    parametros[1, 1] = intentos.ToString();
                    SqlDataReader otroReader = sql.ejecutarStoredProcedure(sp, parametros);
                    otroReader.Close();
                }
                ///////
                sql.Close();
            }else { MessageBox.Show("Campo Username Vacio"); }
            //Si el Login es correcto cierra el formulario y continua con el Formulario Principal
            if (correcto)
                this.Close();
            
        }
    }
}
