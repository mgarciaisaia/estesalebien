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

namespace VentaElectrodomesticos.AbmEmpleado
{
    public partial class FormAbmEmpleado : Form
    {
        public FormAbmEmpleado()
        {
            InitializeComponent();
        }

        private void BuscarEmpleado_Click(object sender, EventArgs e)
        {
            BuscadorEmpleado buscador = new BuscadorEmpleado();
            buscador.Show(this);
            Empleado empleado = buscador.getEmpleado();
        }
    }
}
