﻿using System;
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
            tAuditor.Text = auditor.apellido.ToUpper() + ", " + auditor.nombre;
        }

        private void bProducto_Click(object sender, EventArgs e)
        {
            BuscadorProducto buscadorProductos = new BuscadorProducto();
            buscadorProductos.ShowDialog(using System;
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
            tAuditor.Text = auditor.apellido.ToUpper() + ", " + auditor.nombre;
        }

        private void bProducto_Click(object sender, EventArgs e)
        {
            BuscadorProducto buscadorProductos = new BuscadorProducto();
            buscadorProductos.ShowDialog(this);
            producto = buscadorProductos.getProducto();
            tProducto.Text = producto.nombre;
        }

        private void FormAsignacionStock_Load(object sender, EventArgs e)
        {
            cSucursal.Items.Clear();
            cSucursal.Items.Add("");
            foreach (Sucursal sucursal in Sucursales.getInstance().list())
            {
                cSucursal.Items.Add(Provincias.getInstance().provincia(sucursal.provincia).nombre);
            }
        }

        private void bBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                validarProducto();
                mostrarStock();
            }
            catch (Exception exception)
            {
                MessageBox.Show("No se pudo buscar el stock del producto seleccionado.\n\n" + exception.Message);
            }
        }

        private void validarProducto()
        {
            String errores = "";
            if (tProducto.Text.Length == 0)
            {
                errores += "\nDebe seleccionar un producto";
            }
            if (tAuditor.Text.Length == 0)
            {
                errores += "\nDebe seleccionar un auditor";
            }
            if (cSucursal.Text.Length == 0)
            {
                errores += "\nDebe seleccionar una sucursal";
            }
            if (errores.Length > 0)
            {
                throw new Exception("Faltan campos:" + errores);
            }
        }

        private void mostrarProducto()
        {

        }

    }
}
this);
            producto = buscadorProductos.getProducto();
            tProducto.Text = producto.nombre;
        }

        private void FormAsignacionStock_Load(object sender, EventArgs e)
        {
            cSucursal.Items.Clear();
            cSucursal.Items.Add("");
            foreach (Sucursal sucursal in Sucursales.getInstance().list())
            {
                cSucursal.Items.Add(Provincias.getInstance().provincia(sucursal.provincia).nombre);
            }
        }

        private void bBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                validarProducto();
                mostrarStock();
            }
            catch (Exception exception)
            {
                MessageBox.Show("No se pudo buscar el stock del producto seleccionado.\n\n" + exception.Message);
            }
        }

        private void validarProducto()
        {
            String errores = "";
            if (tProducto.Text.Length == 0)
            {
                errores += "\nDebe seleccionar un producto";
            }
            if (tAuditor.Text.Length == 0)
            {
                errores += "\nDebe seleccionar un auditor";
            }
            if (cSucursal.Text.Length == 0)
            {
                errores += "\nDebe seleccionar una sucursal";
            }
            if (errores.Length > 0)
            {
                throw new Exception("Faltan campos:" + errores);
            }
        }

        private void mostrarProducto()
        {

        }

    }
}
