using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VentaElectrodomesticos.DAO;
using VentaElectrodomesticos.Buscadores;
using VentaElectrodomesticos.Model;

namespace VentaElectrodomesticos.AbmProducto
{
    public partial class FormAbmProducto : Form
    {
        Producto producto;

        public FormAbmProducto()
        {
            InitializeComponent();
            this.cargarMarcas();
        }

        private void cargarMarcas()
        {
            cMarca.Items.Clear();
            foreach(Marca marca in Marcas.getInstance().lista()) {
                cMarca.Items.Add(marca);
            }
            cMarca.ValueMember = "codigo";
            cMarca.DisplayMember = "nombre";
        }

        private void bLimpiarABM_Click(object sender, EventArgs e)
        {
            this.limpiarFormulario();
        }

        private void bEliminar_Click(object sender, EventArgs e)
        {
            if (tCodigo.Text.Length > 0)
            {
                Productos.getInstance().inhabilitar(int.Parse(tCodigo.Text));
                cHabilitado.Checked = false;
            }
        }

        private void BuscarProducto_Click(object sender, EventArgs e)
        {
            BuscadorProducto buscador = new BuscadorProducto(true);
            buscador.ShowDialog(this);
            producto = buscador.getProducto();
            this.mostrarProducto();
        }

        private void limpiarFormulario()
        {
            producto = null;
            tCodigo.Text = "";
            tNombre.Text = "";
            tDescripcion.Text = "";
            cPrecio.Value = 0;
            cMarca.SelectedItem = null;
            cHabilitado.Checked = false;
            tCategoria.SelectedNode = null;
        }

        private void mostrarProducto()
        {
            if (producto == null)
            {
                this.limpiarFormulario();
            }
            else
            {
                tCodigo.Text = producto.codigo.ToString();
                tNombre.Text = producto.nombre;
                tDescripcion.Text = producto.descripcion;
                cPrecio.Value = (decimal) producto.precio;
                cMarca.SelectedItem = Marcas.getInstance().marca(producto.marca);
                //TODO: www.manganeta.com
                Producto temporal = producto;
                producto = null;
                tCategoria.SelectedNode = tCategoria.Nodes.Find(temporal.categoria.ToString(), true)[0];
                producto = temporal;
            }
        }

        private void FormAbmProducto_Load(object sender, EventArgs e)
        {
            Categorias.getInstance().fillTree(tCategoria);
        }

        private void bModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Productos.getInstance().actualizar(tCodigo.Text, tNombre.Text, tDescripcion.Text, cPrecio.Value, ((Marca)cMarca.SelectedItem).codigo, cHabilitado.Checked);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error al intentar modificar el Producto:\n\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Productos.getInstance().insertar(tCodigo.Text, tNombre.Text, tDescripcion.Text, cPrecio.Value, ((Marca)cMarca.SelectedItem).codigo, tCategoria.SelectedNode.Name, cHabilitado.Checked);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error al intentar agregar el Producto:\n\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tCategoria_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (producto != null)
            {
                e.Cancel = true;
            }
        }
    }
}