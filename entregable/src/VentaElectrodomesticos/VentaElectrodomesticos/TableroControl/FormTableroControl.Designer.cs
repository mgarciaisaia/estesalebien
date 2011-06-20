namespace VentaElectrodomesticos.TableroControl
{
    partial class FormTableroControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Filtro = new System.Windows.Forms.GroupBox();
            this.bAnalizar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cSucursal = new System.Windows.Forms.ComboBox();
            this.cAnio = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.Resultados = new System.Windows.Forms.GroupBox();
            this.tFaltanteDeStock = new System.Windows.Forms.TextBox();
            this.tProductoDelAnio = new System.Windows.Forms.TextBox();
            this.tVendedorDelAnio = new System.Windows.Forms.TextBox();
            this.tMayorDeudor = new System.Windows.Forms.TextBox();
            this.tMayorFactura = new System.Windows.Forms.TextBox();
            this.tProporcionFormaPago = new System.Windows.Forms.TextBox();
            this.tTotalFacturacion = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tTotalVentas = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Filtro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cAnio)).BeginInit();
            this.Resultados.SuspendLayout();
            this.SuspendLayout();
            // 
            // Filtro
            // 
            this.Filtro.Controls.Add(this.bAnalizar);
            this.Filtro.Controls.Add(this.label2);
            this.Filtro.Controls.Add(this.cSucursal);
            this.Filtro.Controls.Add(this.cAnio);
            this.Filtro.Controls.Add(this.label1);
            this.Filtro.Location = new System.Drawing.Point(12, 12);
            this.Filtro.Name = "Filtro";
            this.Filtro.Size = new System.Drawing.Size(475, 46);
            this.Filtro.TabIndex = 0;
            this.Filtro.TabStop = false;
            this.Filtro.Text = "Filtro";
            // 
            // bAnalizar
            // 
            this.bAnalizar.Location = new System.Drawing.Point(383, 18);
            this.bAnalizar.Name = "bAnalizar";
            this.bAnalizar.Size = new System.Drawing.Size(77, 20);
            this.bAnalizar.TabIndex = 4;
            this.bAnalizar.Text = "Analizar";
            this.bAnalizar.UseVisualStyleBackColor = true;
            this.bAnalizar.Click += new System.EventHandler(this.bAnalizar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Año";
            // 
            // cSucursal
            // 
            this.cSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cSucursal.FormattingEnabled = true;
            this.cSucursal.Location = new System.Drawing.Point(60, 18);
            this.cSucursal.Name = "cSucursal";
            this.cSucursal.Size = new System.Drawing.Size(221, 21);
            this.cSucursal.TabIndex = 2;
            // 
            // cAnio
            // 
            this.cAnio.Location = new System.Drawing.Point(319, 18);
            this.cAnio.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.cAnio.Minimum = new decimal(new int[] {
            1995,
            0,
            0,
            0});
            this.cAnio.Name = "cAnio";
            this.cAnio.Size = new System.Drawing.Size(58, 20);
            this.cAnio.TabIndex = 1;
            this.cAnio.Value = new decimal(new int[] {
            1995,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sucursal";
            // 
            // Resultados
            // 
            this.Resultados.Controls.Add(this.tFaltanteDeStock);
            this.Resultados.Controls.Add(this.tProductoDelAnio);
            this.Resultados.Controls.Add(this.tVendedorDelAnio);
            this.Resultados.Controls.Add(this.tMayorDeudor);
            this.Resultados.Controls.Add(this.tMayorFactura);
            this.Resultados.Controls.Add(this.tProporcionFormaPago);
            this.Resultados.Controls.Add(this.tTotalFacturacion);
            this.Resultados.Controls.Add(this.label10);
            this.Resultados.Controls.Add(this.label9);
            this.Resultados.Controls.Add(this.label8);
            this.Resultados.Controls.Add(this.label7);
            this.Resultados.Controls.Add(this.label6);
            this.Resultados.Controls.Add(this.tTotalVentas);
            this.Resultados.Controls.Add(this.label5);
            this.Resultados.Controls.Add(this.label4);
            this.Resultados.Controls.Add(this.label3);
            this.Resultados.Enabled = false;
            this.Resultados.Location = new System.Drawing.Point(15, 71);
            this.Resultados.Name = "Resultados";
            this.Resultados.Size = new System.Drawing.Size(472, 191);
            this.Resultados.TabIndex = 1;
            this.Resultados.TabStop = false;
            this.Resultados.Text = "Resultados";
            // 
            // tFaltanteDeStock
            // 
            this.tFaltanteDeStock.Location = new System.Drawing.Point(127, 159);
            this.tFaltanteDeStock.Name = "tFaltanteDeStock";
            this.tFaltanteDeStock.ReadOnly = true;
            this.tFaltanteDeStock.Size = new System.Drawing.Size(330, 20);
            this.tFaltanteDeStock.TabIndex = 17;
            // 
            // tProductoDelAnio
            // 
            this.tProductoDelAnio.Location = new System.Drawing.Point(127, 133);
            this.tProductoDelAnio.Name = "tProductoDelAnio";
            this.tProductoDelAnio.ReadOnly = true;
            this.tProductoDelAnio.Size = new System.Drawing.Size(330, 20);
            this.tProductoDelAnio.TabIndex = 16;
            // 
            // tVendedorDelAnio
            // 
            this.tVendedorDelAnio.Location = new System.Drawing.Point(127, 107);
            this.tVendedorDelAnio.Name = "tVendedorDelAnio";
            this.tVendedorDelAnio.ReadOnly = true;
            this.tVendedorDelAnio.Size = new System.Drawing.Size(330, 20);
            this.tVendedorDelAnio.TabIndex = 15;
            // 
            // tMayorDeudor
            // 
            this.tMayorDeudor.Location = new System.Drawing.Point(127, 81);
            this.tMayorDeudor.Name = "tMayorDeudor";
            this.tMayorDeudor.ReadOnly = true;
            this.tMayorDeudor.Size = new System.Drawing.Size(330, 20);
            this.tMayorDeudor.TabIndex = 14;
            // 
            // tMayorFactura
            // 
            this.tMayorFactura.Location = new System.Drawing.Point(380, 48);
            this.tMayorFactura.Name = "tMayorFactura";
            this.tMayorFactura.ReadOnly = true;
            this.tMayorFactura.Size = new System.Drawing.Size(77, 20);
            this.tMayorFactura.TabIndex = 13;
            // 
            // tProporcionFormaPago
            // 
            this.tProporcionFormaPago.Location = new System.Drawing.Point(380, 22);
            this.tProporcionFormaPago.Name = "tProporcionFormaPago";
            this.tProporcionFormaPago.ReadOnly = true;
            this.tProporcionFormaPago.Size = new System.Drawing.Size(77, 20);
            this.tProporcionFormaPago.TabIndex = 12;
            // 
            // tTotalFacturacion
            // 
            this.tTotalFacturacion.Location = new System.Drawing.Point(127, 46);
            this.tTotalFacturacion.Name = "tTotalFacturacion";
            this.tTotalFacturacion.ReadOnly = true;
            this.tTotalFacturacion.Size = new System.Drawing.Size(77, 20);
            this.tTotalFacturacion.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(26, 162);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 17);
            this.label10.TabIndex = 10;
            this.label10.Text = "Faltante de stock:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(28, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 16);
            this.label9.TabIndex = 9;
            this.label9.Text = "Producto del año:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(11, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 16);
            this.label8.TabIndex = 8;
            this.label8.Text = "Vendedor del año:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(44, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "Mayor deudor:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(218, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Mayor factura:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tTotalVentas
            // 
            this.tTotalVentas.Location = new System.Drawing.Point(127, 20);
            this.tTotalVentas.Name = "tTotalVentas";
            this.tTotalVentas.ReadOnly = true;
            this.tTotalVentas.Size = new System.Drawing.Size(77, 20);
            this.tTotalVentas.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(218, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Proporción de forma de pago:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "Total de facturación:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Total de ventas:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // FormTableroControl
            // 
            this.AcceptButton = this.bAnalizar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 274);
            this.Controls.Add(this.Resultados);
            this.Controls.Add(this.Filtro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormTableroControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tablero de Control";
            this.Load += new System.EventHandler(this.FormTableroControl_Load);
            this.Filtro.ResumeLayout(false);
            this.Filtro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cAnio)).EndInit();
            this.Resultados.ResumeLayout(false);
            this.Resultados.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Filtro;
        private System.Windows.Forms.Button bAnalizar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cSucursal;
        private System.Windows.Forms.NumericUpDown cAnio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox Resultados;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tTotalVentas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tFaltanteDeStock;
        private System.Windows.Forms.TextBox tProductoDelAnio;
        private System.Windows.Forms.TextBox tVendedorDelAnio;
        private System.Windows.Forms.TextBox tMayorDeudor;
        private System.Windows.Forms.TextBox tMayorFactura;
        private System.Windows.Forms.TextBox tProporcionFormaPago;
        private System.Windows.Forms.TextBox tTotalFacturacion;
    }
}