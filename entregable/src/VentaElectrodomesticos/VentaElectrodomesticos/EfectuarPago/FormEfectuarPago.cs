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
        private string nrofactura;
        
        
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
            this.rellenarComboBoxProvincia();
            this.rellenarComboBoxSucursal();

            conexion.Open();
            SqlDataReader reader = conexion.busquedaSQLDataReader("SELECT dni, provincia, tipo, sucursal FROM mayusculas_sin_espacios.Empleados as emp left join mayusculas_sin_espacios.Usuarios as us on (emp.dni=us.empleado) where emp.habilitado='1' and us.nombre='" + user + "' order by 1");
            if (reader.Read())
            {
                dni = System.Convert.ToInt16(reader["dni"]);
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
            conexion.Open();
            SqlDataReader reader = conexion.busquedaSQLDataReader("SELECT vis.numero FROM mayusculas_sin_espacios.facturascompletas as vis left join mayusculas_sin_espacios.facturas as fac on(vis.numero=fac.numero) where cliente='"+tCliente.Text+"' and fac.cuotas > 1 ");

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
            String[,] parametros = new String[2, 1];
            String sp = "mayusculas_sin_espacios.sp_faltanCuotas";
            parametros[0, 0] = "@factura";
            parametros[1, 0] = cFactura.Text;
            
            SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);


            if (reader.Read())
            {
                cCuotas.Minimum = 0;
                cCuotas.Maximum = System.Convert.ToInt16(reader[0].ToString());
            }
            reader.Close();
            conexion.Close();
            cCuotas.Enabled = true;
        }

        private void bFacturar_Click(object sender, EventArgs e)
        {

            conexion.Open();
            if (!this.hayAlgunTextboxVacio(cProvincia, cSucursal, tCliente, cFactura, cCuotas))
            {
                String[,] parametros = new String[2, 5];
                String sp = "mayusculas_sin_espacios.sp_Pagar";
                parametros[0, 0] = "@factura";
                parametros[0, 1] = "@sucursal";
                parametros[0, 2] = "@cuotas";
                parametros[0, 3] = "@fecha";
                parametros[0, 4] = "@cobrador";

                parametros[1, 0] = cFactura.Text;
                parametros[1, 1] = (cSucursal.SelectedIndex +1).ToString();
                parametros[1, 2] = (cCuotas.Value).ToString();
                parametros[1, 3] = System.DateTime.Now.ToString();
                parametros[1, 4] = dni.ToString();

                SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                if (reader.Read())
                {
                    MessageBox.Show("Se ha Pagado.", "Success!");
                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos", "Warning!");
            }
            conexion.Close();
        }

        

    }
}
