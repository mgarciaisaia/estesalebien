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
using VentaElectrodomesticos.MetodosSQL;
using VentaElectrodomesticos.Utils;


namespace VentaElectrodomesticos.Login
{
    public partial class FormLogin : Form
    {

        private ClaseSQL sql;
        private Hasher hasher = new Hasher(new SHA256Managed());


        private bool correcto;
        public bool CORRECTO
        {
            get { return correcto; }
        }

        private string username = "";
        public string USERNAME
        {
            get { return username; }
        }
        private string[] funciones;
        public string[] FUNCIONES
        {
            get {return funciones;}
        }

        public FormLogin()
        {
            InitializeComponent();
            sql = ClaseSQL.getInstance();
            correcto = false;
            funciones = new string[15];
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != "")
            {
                sql.Open();
                
                String procedureName = ClaseSQL.tableName("sp_LOGIN");
                String[,] parametros = new String[2, 2];
                parametros[0, 0] = "@USERNAME";
                parametros[0, 1] = "@PASS";
                parametros[1, 0] = txtUser.Text;
                parametros[1, 1] = hasher.hash(txtPass.Text);

                SqlDataReader otroReader = sql.ejecutarStoredProcedure(procedureName, parametros);
                if (otroReader.Read())
                    switch (otroReader[0].ToString())
                    {
                        case "USUARIO BLOQUEADO":
                            MessageBox.Show("USUARIO BLOQUEADO");
                            break;
                        case "CONTRASEÑA INVALIDA":
                            MessageBox.Show("CONTRASEÑA INVALIDA");
                            break;
                        case "NO EXISTE EN LA BASE":
                            MessageBox.Show("NO EXISTE EN LA BASE");
                            break;
                        default:
                            int i = 0;
                            do
                            {
                                funciones[i] = otroReader[0].ToString();
                                i++;
                                correcto = true;
                                username = txtUser.Text.ToLower();
                            } while (otroReader.Read());
                            break;
                    }
                else
                {
                    correcto = true;
                    username = txtUser.Text.ToLower();
                }
                otroReader.Close();
                sql.Close();
            }
            else { MessageBox.Show("Campo Username Vacio"); }
            //Si el Login es correcto cierra el formulario y continua con el Formulario Principal
            if (correcto)
                this.Close();

        }

    }
}
