using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VentaElectrodomesticos.Model;
using VentaElectrodomesticos.MetodosSQL;
using System.Data.SqlClient;
using System.Collections;
using VentaElectrodomesticos.DAO;

namespace VentaElectrodomesticos.Buscadores
{
    public partial class BuscadorProducto : Form
    {
        private Producto producto;
        private Boolean buscarDeshabilitados = false;

        public BuscadorProducto()
        {
            InitializeComponent();
        }

        public BuscadorProducto (Boolean buscarDeshabilitados) : this() {
            this.buscarDeshabilitados = buscarDeshabilitados;
        }

        public Producto getProducto()
        {
            return producto;
        }

        private void bBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.validarBusqueda();
                String categoria = "";
                if (tCategoria.SelectedNode != null)
                {
                    categoria = tCategoria.SelectedNode.Name;
                }
                String marca = "";
                if(((Marca) cMarca.SelectedItem) != null){
                    marca += ((Marca)cMarca.SelectedItem).codigo;
                }
                DataTable resultados = Productos.getInstance().table(tCodigo.Text, tNombre.Text, marca,
                     categoria, cPrecioMinimo.Value, cPrecioMaximo.Value, buscarDeshabilitados);
                dgProductos.DataSource = resultados;
                dgProductos.Columns["CodigoMarca"].Visible = false;
                dgProductos.Columns["CodigoCategoria"].Visible = false;
                dgProductos.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Los criterios de busqueda insertados son erroneos:\n\n" + exception.Message);
            }
        }

        private void validarBusqueda()
        {
            if (cPrecioMaximo.Value >= 0)
            {
                if (cPrecioMaximo.Value < cPrecioMinimo.Value)
                {
                    throw new Exception("El precio maximo es menor que el precio minimo");
                }
            }
        }

        private Producto productoSeleccionado(DataGridViewCellCollection cells)
        {
            Producto producto = new Producto();
            producto.codigo = int.Parse(cells["Codigo"].Value.ToString());
            producto.nombre = cells["Nombre"].Value.ToString();
            producto.descripcion = cells["Descripcion"].Value.ToString();
            producto.categoria = int.Parse(cells["CodigoCategoria"].Value.ToString());
            producto.precio = double.Parse(cells["Precio"].Value.ToString());
            producto.marca = int.Parse(cells["CodigoMarca"].Value.ToString());
            producto.habilitado = byte.Parse(cells["Habilitado"].Value.ToString()) > 0;
            return producto;
        }

        private void BuscadorProducto_Load(object sender, EventArgs e)
        {
            this.rellenarCategorias();
            this.cargarMarcas();
        }

        private void bLimpiarBuscadorEmp_Click(object sender, EventArgs e)
        {
            tCodigo.Clear();
            tNombre.Clear();
            cMarca.SelectedItem = null;
            cPrecioMaximo.Value = -1;
            cPrecioMinimo.Value = -1;
            tCategoria.SelectedNode = null;
            dgProductos.DataSource = null;
        }

        private void cargarMarcas()
        {
            cMarca.Items.Clear();
            cMarca.Items.Add(new Marca(-1, ""));
            foreach (Marca marca in Marcas.getInstance().lista())
            {
                cMarca.Items.Add(marca);
            }
            cMarca.ValueMember = "codigo";
            cMarca.DisplayMember = "nombre";
        }

        private void rellenarCategorias()
        {
            tCategoria.Nodes.Clear();
           Categorias.getInstance().fillTree(tCategoria);
       }
        private void dgProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgProductos.SelectedCells.Count != 0)
                {
                    DataGridViewCellCollection cells = dgProductos.SelectedRows[0].Cells;
                    producto = this.productoSeleccionado(cells);
                    this.Close();
                }
            }
        }
    }
}
