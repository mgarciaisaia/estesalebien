using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VentaElectrodomesticos.Buscadores;
using VentaElectrodomesticos.Model;
using VentaElectrodomesticos.DAO;

namespace VentaElectrodomesticos.AsignacionStock
{
    public partial class FormAsignacionStock : Form
    {
        private Empleado auditor = null;
        private Producto producto = null;

        public FormAsignacionStock()
        {
            InitializeComponent();
        }

        private void bAuditor_Click(object sender, EventArgs e)
        {
            BuscadorEmpleado buscadorEmpleados = new BuscadorEmpleado(false, "Analista");
            buscadorEmpleados.ShowDialog(this);
            auditor = buscadorEmpleados.getEmpleado();
        }

        private void bProducto_Click(object sender, EventArgs e)
        {
            BuscadorProducto buscadorProductos = new BuscadorProducto();
            buscadorProductos.ShowDialog(this);
            producto = buscadorProductos.getProducto();
        }

        private void FormAsignacionStock_Load(object sender, EventArgs e)
        {
            cSucursal.Items.Clear();
            //FIXME: cambiar por el DAO
            
        }

        

    }
}
