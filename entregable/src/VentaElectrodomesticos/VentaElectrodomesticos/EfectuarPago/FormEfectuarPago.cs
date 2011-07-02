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

namespace VentaElectrodomesticos.EfectuarPago
{
    public partial class FormEfectuarPago : Form
    {

        private string user;
        private int dni;

        private string[] provincias;
        private string[] sucursales;
        ClaseSQL conexion;
        
        
        public FormEfectuarPago(string username)
        {
            InitializeComponent();
            user = username;
            provincias = new string[25];
            sucursales = new string[25];
            conexion = ClaseSQL.getInstance();
        }

        private void FormEfectuarPago_Load(object sender, EventArgs e)
        {
            lTotal.Text = "";
            lCuota.Text = "";
            lCuotas.Text = "";
            this.rellenarComboBoxProvincia();
            this.rellenarComboBoxSucursal();

            conexion.Open();
            SqlDataReader reader = conexion.busquedaSQLDataReader("SELECT dni, provincia, tipo, sucursal FROM mayusculas_sin_espacios.Empleados as emp left join mayusculas_sin_espacios.Usuarios as us on (emp.dni=us.empleado) where emp.habilitado='1' and us.nombre='" + user + "' order by 1");
            if (reader.Read())
            {
                dni = System.Convert.ToInt32(reader["dni"]);
                if (reader["tipo"].ToString() == "2")
                {
                    cProvincia.SelectedIndex = System.Convert.ToInt16(reader["Provincia"]);
                    cSucursal.SelectedIndex = System.Convert.ToInt16(reader["Sucursal"]);
                    cProvincia.Enabled = false;
                    cSucursal.Enabled = false;
                }
            }

            reader.Close();
            conexion.Close();
            if (dni <= 0)
            {
                MessageBox.Show("El usuario actual es de administracion (no esta asociado a ningun empleado), por lo que no podra registrar Pagos.\nPara registrar pagos, por favor inicie sesion con su usuario asociado.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void bBuscarCliente_Click(object sender, EventArgs e)
        {
            BuscadorCliente buscador = new BuscadorCliente();
            buscador.ShowDialog(this);
            this.cargarCliente(buscador.getCliente());
        }

        private void cargarCliente(Model.Cliente cliente)
        {
            if (cliente != null)
            {
                tCliente.Text = cliente.dni.ToString();
                cFactura.Enabled = true;
                this.rellenarComboBoxFactura();
            }

        }

        private void rellenarComboBoxFactura()
        {
            cFactura.Items.Clear();
            cFactura.Text = "";
            
            conexion.Open();
            String[,] parametros = new String[2, 1];
            String sp = "mayusculas_sin_espacios.sp_FACTURASPENDIENTES";
            parametros[0, 0] = "@cliente";
            parametros[1, 0] = tCliente.Text;

            SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
            while (reader.Read())
            {
                String fact = reader[0].ToString().Trim();
                cFactura.Items.Add(fact);
            }
            reader.Close();
            conexion.Close();            
        }

        private bool hayAlgunTextboxVacio(ComboBox cProvincia, ComboBox cSucursal, TextBox tCliente, ComboBox cFactura, NumericUpDown cCuotas)
        {
            return cProvincia.Text.Equals("") || cSucursal.Text.Equals("") || tCliente.Text.Equals("") || cFactura.Text.Equals("") || cCuotas.Text.Equals("0") ;
        }
                
        private void cFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            conexion.Open();
            string query = " select mayusculas_sin_espacios.fun_faltanCuotas ('" +
                cFactura.Text + "')";
            SqlDataReader reader = conexion.busquedaSQLDataReader(query);
            if (reader.Read())
            {
                cCuotas.Maximum = System.Convert.ToInt16(reader[0].ToString());
                cCuotas.Minimum = 1;
            }
            reader.Close();

            query = "select importe, valorcuota FROM mayusculas_sin_espacios.facturascompletas where numero='" +
                            cFactura.Text + "'";
            reader = conexion.busquedaSQLDataReader(query);
            if (reader.Read())
            {
                lTotal.Text = decimal.Round(decimal.Parse(reader[0].ToString()),2).ToString();
                lCuota.Text = decimal.Round(decimal.Parse(reader[1].ToString()),2).ToString();
                lCuotas.Text = (decimal.Parse(lCuota.Text)* cCuotas.Value).ToString();
            }
            reader.Close();

            conexion.Close();
            cCuotas.Enabled = true;
        }

        private void bPagar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                if (!this.hayAlgunTextboxVacio(cProvincia, cSucursal, tCliente, cFactura, cCuotas))
                {
                    Object[,] parametros = new Object[2, 5];
                    String sp = "mayusculas_sin_espacios.sp_Pagar";
                    parametros[0, 0] = "@factura";
                    parametros[0, 1] = "@sucursal";
                    parametros[0, 2] = "@cuotas";
                    parametros[0, 3] = "@fecha";
                    parametros[0, 4] = "@cobrador";

                    parametros[1, 0] = cFactura.Text;
                    parametros[1, 1] = cSucursal.SelectedIndex +1;
                    parametros[1, 2] = cCuotas.Value;
                    parametros[1, 3] = DateTime.Now;
                    parametros[1, 4] = dni;

                    SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                    if (reader.RecordsAffected>0)
                    {
                        MessageBox.Show("Se ha Pagado.", "Success!");
                        this.limpiarForm();                
                    }
                    reader.Close();
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

        private void limpiarForm()
        {
            cProvincia.Text = "";
            cSucursal.Text = "";
            tCliente.Text = "";
            cFactura.Items.Clear();
            cFactura.Text = "";
            cFactura.Enabled = false;
            cCuotas.Text = "1";
            cCuotas.Enabled = false;
            lTotal.Text = "";
            lCuota.Text = "";
            lCuotas.Text = "";
            
        }

        private void cCuotas_ValueChanged(object sender, EventArgs e)
        {
            lCuotas.Text = (decimal.Parse(lCuota.Text) * cCuotas.Value).ToString();
        }

        
    }
}
