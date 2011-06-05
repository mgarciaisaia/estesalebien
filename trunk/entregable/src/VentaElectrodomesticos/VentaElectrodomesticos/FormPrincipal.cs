﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VentaElectrodomesticos.Login;
using VentaElectrodomesticos.AbmEmpleado;
using VentaElectrodomesticos.AbmRol;
using VentaElectrodomesticos.AbmUsuario;
using VentaElectrodomesticos.AbmCliente;
using VentaElectrodomesticos.AbmProducto;
using VentaElectrodomesticos.AsignacionStock;
using VentaElectrodomesticos.Facturacion;
using VentaElectrodomesticos.EfectuarPago;
using VentaElectrodomesticos.TableroControl;
using VentaElectrodomesticos.ClientesPremium;
using VentaElectrodomesticos.MejoresCategorias;
using VentaElectrodomesticos.MetodosSQL;

namespace VentaElectrodomesticos
{
    
    
    public partial class FormPrincipal : Form
    {
        private ClaseSQL sql;
        private string username;
        
        public FormPrincipal()
        {
            InitializeComponent();
            FormLogin login = new FormLogin();
            login.ShowDialog(this);
            username = login.USERNAME;
            this.Text += " - " + username;
            foreach (string s in login.FUNCIONES)
            {
                sql = new ClaseSQL();
                sql.Open();
                String defaultquery = " SELECT DISTINCT ROL_FUNCION FROM [MAYUSCULAS_SIN_ESPACIOS].ROLES ";
                String query = " WHERE ROL_NOMBRE = '" + s + "'";
                query = defaultquery + query;
                SqlDataReader reader = sql.busquedaSQLDataReader(query);
                while (reader.Read())
                {
                    //MessageBox.Show(reader[0].ToString());
                    switch (reader[0].ToString())
                    {
                        case "ABM de Empleado":
                            aBMToolStripMenuItem.Enabled = true;
                            break;
                        case "ABM de Rol":
                            aBMDeRolToolStripMenuItem.Enabled = true;
                            break;
                        case "ABM de Usuario":
                            aBMDeUsuarioToolStripMenuItem.Enabled = true;
                            break;
                        case "ABM de Cliente":
                            aBMDeClienteToolStripMenuItem.Enabled=true;
                            break;
                        case "ABM de Producto":
                            aBMDeProductoToolStripMenuItem.Enabled = true;
                            break;
                        case "Asignacion de stock":
                            asignaciónDeStockToolStripMenuItem.Enabled=true;
                            break;
                        case "Facturacion":
                            facturaciónToolStripMenuItem.Enabled = true;
                            break;
                        case "Efectuar Pago":
                            efectuarPagoToolStripMenuItem.Enabled=true;
                            break;
                        case "Tablero de Control":
                            tableroDeControlToolStripMenuItem.Enabled=true;
                            break;
                        case "Clientes Premium":
                            clientesPremiumToolStripMenuItem.Enabled=true;
                            break;
                        case "Mejores Categorias":
                            mejoresCategoriasToolStripMenuItem.Enabled = true;
                            break;
                    }
                }
                reader.Close();
                sql.Close();
                
            }
        }

        private void aBMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbmEmpleado form = new FormAbmEmpleado();
            form.ShowDialog(this);
        }

        private void aBMDeRolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbmRol form = new FormAbmRol();
            form.ShowDialog(this);
        }

        private void aBMDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbmUsuario form = new FormAbmUsuario();
            form.ShowDialog(this);
        }

        private void aBMDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbmCliente form = new FormAbmCliente();
            form.ShowDialog(this);
        }

        private void aBMDeProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbmProducto form = new FormAbmProducto();
            form.ShowDialog(this);
        }

        private void asignaciónDeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAsignacionStock form = new FormAsignacionStock();
            form.ShowDialog(this);
        }

        private void facturaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFacturacion form = new FormFacturacion();
            form.ShowDialog(this);
        }

        private void efectuarPagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEfectuarPago form = new FormEfectuarPago();
            form.ShowDialog(this);
        }

        private void tableroDeControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTableroControl form = new FormTableroControl();
            form.ShowDialog(this);
        }

        private void clientesPremiumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClientesPremium form = new FormClientesPremium();
            form.ShowDialog(this);
        }

        private void mejoresCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMejoresCategorias form = new FormMejoresCategorias();
            form.ShowDialog(this);
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            if (username == "")
                this.Close();
        }
    }
}
