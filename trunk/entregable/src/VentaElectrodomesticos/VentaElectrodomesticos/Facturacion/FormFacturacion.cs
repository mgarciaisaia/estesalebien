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
                dgProductos.Rows.Add();
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
            return cProvincia.Text.Equals("") || cSucursal.Text.Equals("") || tCliente.Text.Equals("") || tDescuento.Text.Equals("") || cCuotas.Text.Equals("") || dgCompleto(dgProductos);
        }

        private bool dgCompleto(DataGridView dgProductos)
        {
            bool incompleto = dgProductos.Rows.Count == 1;
            int i=0;
            while ((! incompleto) && (i < dgProductos.Rows.Count-1))
            {
                incompleto = (dgProductos.Rows[i].Cells[0].Value ==null) || (dgProductos.Rows[i].Cells[1].Value == null) || (dgProductos.Rows[i].Cells[2].Value == null);
                i++;
            }
            return incompleto;
        }
        private void bComprar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                if (!this.hayAlgunTextboxVacio(cProvincia, cSucursal, tCliente, tDescuento, cCuotas, dgProductos))
                {
                    DialogResult op = MessageBox.Show("El importe total es de $" + this.montoTotal(), "Confirmar", MessageBoxButtons.OKCancel);
                    if (op == DialogResult.OK)
                    {
                        String[,] parametros = new String[2, 6];
                        String sp = "mayusculas_sin_espacios.sp_facturar";
                        parametros[0, 0] = "@fecha";
                        parametros[0, 1] = "@descuento";
                        parametros[0, 2] = "@cuotas";
                        parametros[0, 3] = "@sucursal";
                        parametros[0, 4] = "@vendedor";
                        parametros[0, 5] = "@cliente";

                        parametros[1, 0] = DateTime.Now.ToString();
                        parametros[1, 1] = tDescuento.Text;
                        parametros[1, 2] = (cCuotas.SelectedIndex + 1).ToString();
                        parametros[1, 3] = (cSucursal.SelectedIndex + 1).ToString();
                        parametros[1, 4] = dni.ToString();
                        parametros[1, 5] = tCliente.Text;
                        

                        SqlDataReader reader = conexion.ejecutarStoredProcedure(sp, parametros);
                        if (reader.Read())
                        {
                            nrofactura = reader[0].ToString();
                            MessageBox.Show("Se ha Facturado - " + nrofactura.ToString(), "Success!");
                                            
                        }
                        reader.Close();

                        string sql = "";
                        for (int i = 0; i < dgProductos.Rows.Count - 1; i++)
                        {
                            sql = "insert into mayusculas_sin_espacios.itemsfactura (factura,producto,preciounitario,cantidad) " +
                               " values ('" + nrofactura + "','" + dgProductos.Rows[i].Cells[0].Value.ToString() +
                               "','" + dgProductos.Rows[i].Cells[1].Value.ToString() +
                               "','" + dgProductos.Rows[i].Cells[2].Value.ToString() + "')";
                            conexion.nonQuery(sql);
                            this.limpiarForm();
                        }
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
        private decimal montoTotal()
        {
            decimal total=0;
            int i = 0;
            while (i<(dgProductos.Rows.Count - 1))
            {
                total += (System.Convert.ToDecimal(dgProductos.Rows[i].Cells[1].Value) * System.Convert.ToDecimal(dgProductos.Rows[i].Cells[2].Value));
                i++;
            }
            return total;
        }


        private void limpiarForm()
        {
            cProvincia.Text = "";
            cSucursal.Text = "";
            tCliente.Text = "";
            cCuotas.Text = "1";
            tDescuento.Text = "";
            dgProductos.Rows.Clear();

        }

        private void tDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == 8 || Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void tDescuento_Leave(object sender, EventArgs e)
        {
            if (tDescuento.Text != "")
                if (int.Parse(tDescuento.Text) >= 30 || int.Parse(tDescuento.Text) <= 0)
                {
                    MessageBox.Show(" El descuento debe ser mayor a 0 y menor a 30", "Eror");
                    tDescuento.Text = "";
                }
        }

       
       
    }
}
