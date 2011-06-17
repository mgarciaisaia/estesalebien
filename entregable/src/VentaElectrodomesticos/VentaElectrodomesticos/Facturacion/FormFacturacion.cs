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

namespace VentaElectrodomesticos.Facturacion
{
    public partial class FormFacturacion : Form
    {
        private string user;
        private int dni;
        
        private string[] provincias;
        private string[] sucursales;
        ClaseSQL conexion;
        private string nrofactura;


        public FormFacturacion(string username)
        {
            InitializeComponent();
            user = username;
            provincias = new string[25];
            sucursales = new string[25];
            conexion = ClaseSQL.getInstance();
        }

        private void FormFacturacion_Load(object sender, EventArgs e)
        {
            this.rellenarComboBoxProvincia();
            this.rellenarComboBoxSucursal();

            conexion.Open();
            SqlDataReader reader = conexion.busquedaSQLDataReader("SELECT dni, provincia, tipo, sucursal FROM mayusculas_sin_espacios.Empleados as emp left join mayusculas_sin_espacios.Usuarios as us on (emp.dni=us.empleado) where emp.habilitado='1' and us.nombre='"+user+"' order by 1");
            if (reader.Read())
            {
                dni = System.Convert.ToInt16(reader["dni"]);
                if (reader["tipo"].ToString() == "2")
                {
                    cProvincia.SelectedIndex = System.Convert.ToInt16(reader["Provincia"]);
                    cSucursal.SelectedIndex = System.Convert.ToInt16(reader["Sucursal"]);
                    cProvincia.Enabled = false;
                    cSucursal.Enabled=false;
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

       

        private void dgProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                BuscadorProducto buscador = new BuscadorProducto();
                buscador.ShowDialog(this);
                this.cargarProducto(buscador.getProducto());
            }

            
        }
        private void cargarProducto(Model.Producto producto)
        {
            if (producto != null)
            {
                int i = dgProductos.Rows.Count - 1 ;
                dgProductos.Rows[i].Cells[0].Value=producto.codigo;
                dgProductos.Rows[i].Cells[1].Value = producto.precio;
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
            }

        }

        private bool hayAlgunTextboxVacio(ComboBox cProvincia, ComboBox cSucursal, TextBox tCliente, TextBox tDescuento, ComboBox cCuotas, DataGridView dgProductos)
        {
            return cProvincia.Text.Equals("") || cSucursal.Text.Equals("") || tCliente.Text.Equals("") || tDescuento.Text.Equals("") || cCuotas.Text.Equals("") || dgProductos.Rows.Count==1 ;
        }

        private void bComprar_Click(object sender, EventArgs e)
        {
            
            conexion.Open();
            if (!this.hayAlgunTextboxVacio(cProvincia, cSucursal, tCliente, tDescuento, cCuotas, dgProductos))
            {
                String[,] parametros = new String[2, 6];
                String sp = "mayusculas_sin_espacios.sp_facturar";
                parametros[0, 0] = "@fecha";
                parametros[0, 1] = "@descuento";
                parametros[0, 2] = "@cuotas";
                parametros[0, 3] = "@sucursal";
                parametros[0, 4] = "@vendedor";
                parametros[0, 5] = "@cliente";

                parametros[1, 0] = System.DateTime.Now.ToString();
                parametros[1, 1] = tDescuento.Text;
                parametros[1, 2] = (cCuotas.SelectedIndex + 1).ToString();
                parametros[1, 3] = (cSucursal.SelectedIndex +1).ToString();
                parametros[1, 4] = dni.ToString();
                parametros[1, 5] = tCliente.Text;
                
                SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                if (reader.Read())
                {
                    MessageBox.Show("Se ha Facturado.", "Success!");
                    nrofactura = reader[0].ToString();
                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos", "Warning!");
            }

            string sql = "";
            for (int i = 0; i < dgProductos.Rows.Count - 1; i++)
            {
                 sql = "insert into mayusculas_sin_espacios.itemsfactura (factura,producto,preciounitario,cantidad) " +
                    " values ('" + nrofactura + "','" + dgProductos.Rows[i].Cells[0].Value.ToString() +
                    "','" + dgProductos.Rows[i].Cells[1].Value.ToString() +
                    "','" + dgProductos.Rows[i].Cells[2].Value.ToString() + "')";
                conexion.insertQuery(sql);
            }
            MessageBox.Show(nrofactura);
            conexion.Close();
        }

        

       
    }
}
