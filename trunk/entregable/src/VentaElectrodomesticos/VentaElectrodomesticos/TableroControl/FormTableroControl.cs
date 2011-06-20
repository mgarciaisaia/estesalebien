using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VentaElectrodomesticos.DAO;
using VentaElectrodomesticos.Model;
using VentaElectrodomesticos.MetodosSQL;

namespace VentaElectrodomesticos.TableroControl
{
    public partial class FormTableroControl : Form
    {
        public FormTableroControl()
        {
            InitializeComponent();
        }

        private void FormTableroControl_Load(object sender, EventArgs e)
        {
            foreach (Provincia provincia in Provincias.getInstance().list())
            {
                cSucursal.Items.Add(provincia);
                cSucursal.DisplayMember = "nombre";
                cSucursal.ValueMember = "codigo";
            }
            
        }

        private void bAnalizar_Click(object sender, EventArgs e)
        {
            ClaseSQL conexion = ClaseSQL.getInstance();
            try
            {
                this.validarDatos();
                conexion.Open();
                this.totalVentas(conexion);
                this.totalFacturacion(conexion);
                this.proporcionFormaDePago(conexion);
                this.mayorFactura(conexion);
                /*this.mayorDeudor(conexion);
                this.vendedorDelAnio(conexion);
                this.productoDelAnio(conexion);
                this.faltanteDeStock(conexion);*/
                Resultados.Enabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error al intentar hacer el analisis.\n\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void totalVentas(ClaseSQL conexion)
        {
            String query = "SELECT COUNT(1)" + this.fromFacturas();
            Object resultado = conexion.scalarQuery(query);
            tTotalVentas.Text = resultado.ToString();
        }

        private void totalFacturacion(ClaseSQL conexion)
        {
            String query = "SELECT SUM(Importe)" + this.fromFacturas();
            Object resultado = conexion.scalarQuery(query);
            tTotalFacturacion.Text = resultado.ToString();
        }

        private void proporcionFormaDePago(ClaseSQL conexion)
        {
            String query = "SELECT COUNT(1)" + this.fromFacturas() + " AND Cuotas = 1";
            int cantidadEfectivo = int.Parse(conexion.scalarQuery(query).ToString());
            query = "SELECT COUNT(1)" + this.fromFacturas() + " AND Cuotas > 1";
            int cantidadEnCuotas = int.Parse(conexion.scalarQuery(query).ToString());
            if (cantidadEnCuotas + cantidadEfectivo > 0)
            {
                decimal proporcionEfectivo = cantidadEfectivo * 100 / (cantidadEfectivo + cantidadEnCuotas);
                decimal proporcionEnCuotas = 100 - proporcionEfectivo;
                tProporcionFormaPago.Text = proporcionEfectivo + "% - " + proporcionEnCuotas + "%";
            }
            else
            {
                tProporcionFormaPago.Text = "No se facturo ese año";
            }
        }

        private void mayorFactura(ClaseSQL conexion)
        {
            String query = "SELECT MAX(Importe)" + this.fromFacturas();
            tMayorFactura.Text = conexion.scalarQuery(query).ToString();
        }

        private String fromFacturas()
        {
            return " FROM " + ClaseSQL.tableName("FacturasCompletas") + " WHERE Sucursal = " + ((Provincia)cSucursal.SelectedItem).codigo + " AND YEAR(Fecha) = " + cAnio.Value;
        }

        private void validarDatos()
        {
            if (cSucursal.SelectedItem == null)
            {
                throw new Exception("Debe seleccionar alguna sucursal");
            }
        }

    }
}
