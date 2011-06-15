﻿using System;
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

namespace VentaElectrodomesticos.Buscadores
{
    public partial class BuscadorEmpleado : Form
    {
        private Empleado empleado;
        private Boolean buscarDeshabilitados = true;
        ClaseSQL conexion;
        private string[] provincias;
        private string[] tipos;
        private string[] sucursales;
        
        public BuscadorEmpleado()
        {
            // FIXME: aca hay que cargar los combos
            InitializeComponent();
            conexion = ClaseSQL.getInstance();
            provincias = new string[25];
            tipos = new string[3];

        }

        public Empleado getEmpleado()
        {
            return empleado;
        }

        private void bBuscar_Click(object sender, EventArgs e)
        {
            String query = this.generateQuery();
            DataTable tabla = this.resultTable(query);
            dgEmpleados.DataSource = tabla;
            dgEmpleados.Show();
            
            
            
            /*
             * FIXME: Por algun lado lei que tal vez sea mas simple pasarle una
             * ArrayList de Empleados al dgEmpleados para despues recuperar los
             * objetos
             * 
             * 
             * http://www.codeproject.com/KB/grid/dataGridview-DataReader.aspx
             * 
             */

        }

        private DataTable resultTable(String query)
        {
            
            conexion.Open();
            SqlDataReader data = conexion.busquedaSQLDataReader(query);
            
            DataTable tabla = new DataTable();
            if (data.HasRows)
            {
                tabla.Load(data);
            }
            conexion.Close();
            return tabla;
        }

        private String generateQuery()
        {
            String query = "SELECT DNI, Nombre, Apellido, Mail, Telefono, Direccion, Provincia, Tipo, Sucursal, Habilitado" +
                " FROM " + ClaseSQL.tableName("Empleados");

            String where = "";

            if (!buscarDeshabilitados)
            {
                where += " AND Habilitado = 1";
            }

            if (tBusqDNI.Text.Length > 0)
            {
                where += " AND DNI = '" + tBusqDNI.Text + "'";
            }

            if (tBusqNombre.Text.Length > 0)
            {
                where += " AND Nombre LIKE '%" + tBusqNombre.Text + "%'";
            }

            if (tBusqApellido.Text.Length > 0)
            {
                where += " AND Apellido LIKE '%" + tBusqApellido.Text + "%'";
            }

            if (cBusqProvincia.SelectedItem != null && cBusqProvincia.SelectedItem.ToString() != "")
            {
                where += " AND Provincia = " + cBusqProvincia.SelectedIndex;
            }

            if (cBusqSucursal.SelectedItem != null && cBusqSucursal.SelectedItem.ToString() != "")
            {
                where += " AND Sucursal = " + cBusqSucursal.SelectedIndex;
            }

            if (cBusqTipo.SelectedItem != null && (cBusqTipo.SelectedItem.ToString() != ""))
            {
                where += " AND Tipo = " + cBusqTipo.SelectedIndex ;
            }


            if (where.Length > 0)
            {
                where = where.Substring(5);
                query += " WHERE " + where;
            }
            
            return query;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgEmpleados.SelectedCells.Count != 0)
            {
                DataGridViewCellCollection cells = dgEmpleados.SelectedRows[0].Cells;
                empleado = this.empleadoSeleccionado(cells);
            }
            this.Close();
        }

        private Empleado empleadoSeleccionado(DataGridViewCellCollection cells)
        {
            Empleado empleado = new Empleado();
            empleado.dni = (decimal) cells[0].Value;
            empleado.nombre = cells[1].Value.ToString();
            empleado.apellido = cells[2].Value.ToString();
            empleado.mail = cells[3].Value.ToString();
            empleado.telefono = cells[4].Value.ToString();
            empleado.direccion = cells[5].Value.ToString();
            //FIXME: deberiamos mostrar los strings y guardar los códigos, usando a los DAOs como conversores
            empleado.provincia = (byte) cells[6].Value;
            empleado.tipo = (byte)cells[7].Value;
            empleado.sucursal = (byte) cells[8].Value;
            
            empleado.habilitado = (byte)cells[9].Value > 0;


            return empleado;
        }

        private void BuscadorEmpleado_Load(object sender, EventArgs e)
        {
            this.rellenarComboBoxProvincia();
            this.rellenarComboBoxTipo();
            this.rellenarComboBoxSucursal();
            
        }
        private void rellenarComboBoxTipo() 
        {
            cBusqTipo.Items.Add("");
            cBusqTipo.Items.Add("Vendedor");
            cBusqTipo.Items.Add("Analista");
            tipos[1] = "Vendedor";
            tipos[2] = "Analista";
            
        }
        
        private void rellenarComboBoxProvincia()
        {
            try
            {
                cBusqProvincia.Items.Add("");
                conexion.Open();
                SqlDataReader reader = conexion.busquedaSQLDataReader("SELECT codigo, Nombre FROM mayusculas_sin_espacios.provincias order by 1");
                
                while (reader.Read())
                {
                    String prov = reader[1].ToString().Trim();
                    int codigo = System.Convert.ToInt32(reader[0].ToString());
                    cBusqProvincia.Items.Add(prov);
                    provincias[codigo] = prov;
                    
                }
                reader.Close();
                
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
        private void rellenarComboBoxSucursal()
        {
            try
            {
                cBusqSucursal.Items.Add("");
                conexion.Open();
                SqlDataReader reader = conexion.busquedaSQLDataReader("SELECT nombre, direccion FROM mayusculas_sin_espacios.sucursales left join mayusculas_sin_espacios.provincias on(codigo=provincia) order by provincia");

                while (reader.Read())
                {
                    String suc = reader[0].ToString().Trim() + " - " + reader[1].ToString().Trim();
                    cBusqSucursal.Items.Add(suc);
                    
                }
                reader.Close();

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
        private void bLimpiarBuscadorEmp_Click(object sender, EventArgs e)
        {
            tBusqDNI.Clear();
            tBusqNombre.Clear();
            tBusqApellido.Clear();
            cBusqProvincia.Items.Clear();
            this.rellenarComboBoxProvincia();
            cBusqSucursal.Items.Clear();
            this.rellenarComboBoxSucursal();
            cBusqTipo.Items.Clear();
            this.rellenarComboBoxTipo();
            dgEmpleados.DataSource=null;
        }
        
    }
}
