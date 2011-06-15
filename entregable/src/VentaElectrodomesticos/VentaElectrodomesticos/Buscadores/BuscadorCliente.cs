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

namespace VentaElectrodomesticos.Buscadores
{
    public partial class BuscadorCliente : Form
    {
        private Cliente cliente;
        private Boolean buscarDeshabilitados = true;
        ClaseSQL conexion;
        private string[] provincias;
        private string[] tipos;
        private string[] sucursales;
        
        public BuscadorCliente()
        {
            InitializeComponent();
            conexion = ClaseSQL.getInstance();
            provincias = new string[25];
            tipos = new string[3];
        }
        private void bBuscar_Click(object sender, EventArgs e)
        {
            String query = this.generateQuery();
            DataTable tabla = this.resultTable(query);
            dgClientes.DataSource = tabla;
            dgClientes.Show();
        }


        public Cliente getCliente()
        {
            return cliente;
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
            String query = "SELECT DNI, Nombre, Apellido, Mail, Telefono, Direccion, Provincia, Habilitado" +
                " FROM " + ClaseSQL.tableName("Clientes");

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

            if (where.Length > 0)
            {
                where = where.Substring(5);
                query += " WHERE " + where;
            }
            
            return query;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgClientes.SelectedCells.Count != 0)
            {
                DataGridViewCellCollection cells = dgClientes.SelectedRows[0].Cells;
                cliente = this.clienteSeleccionado(cells);
            }
            this.Close();
        }

        private Cliente clienteSeleccionado(DataGridViewCellCollection cells)
        {
            Cliente cliente = new Cliente();
            cliente.dni = (decimal) cells[0].Value;
            cliente.nombre = cells[1].Value.ToString();
            cliente.apellido = cells[2].Value.ToString();
            cliente.mail = cells[3].Value.ToString();
            cliente.telefono = cells[4].Value.ToString();
            cliente.direccion = cells[5].Value.ToString();
            cliente.provincia = (byte) cells[6].Value;
            cliente.habilitado = (byte) cells[7].Value > 0;


            return cliente;
        }

        private void BuscadorCliente_Load(object sender, EventArgs e)
        {
            this.rellenarComboBoxProvincia();
            
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
        
        private void bLimpiarBuscadorCli_Click(object sender, EventArgs e)
        {
            tBusqDNI.Clear();
            tBusqNombre.Clear();
            tBusqApellido.Clear();
            cBusqProvincia.Items.Clear();
            this.rellenarComboBoxProvincia();
            dgClientes.DataSource=null;
        }
       
    }
}
       

        
