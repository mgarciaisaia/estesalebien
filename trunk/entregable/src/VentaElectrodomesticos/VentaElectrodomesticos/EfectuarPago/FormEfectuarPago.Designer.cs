namespace VentaElectrodomesticos.EfectuarPago
{
    partial class FormEfectuarPago
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
            this.bFacturar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tCliente = new System.Windows.Forms.TextBox();
            this.bBuscarCliente = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cSucursal = new System.Windows.Forms.ComboBox();
            this.cProvincia = new System.Windows.Forms.ComboBox();
            this.cFactura = new System.Windows.Forms.ComboBox();
            this.cCuotas = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lCuotas = new System.Windows.Forms.Label();
            this.lCuota = new System.Windows.Forms.Label();
            this.lTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cCuotas)).BeginInit();
            this.SuspendLayout();
            // 
            // bFacturar
            // 
            this.bFacturar.Location = new System.Drawing.Point(61, 85);
            this.bFacturar.Name = "bFacturar";
            this.bFacturar.Size = new System.Drawing.Size(143, 33);
            this.bFacturar.TabIndex = 24;
            this.bFacturar.Text = "Pagar";
            this.bFacturar.UseVisualStyleBackColor = true;
            this.bFacturar.Click += new System.EventHandler(this.bPagar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(213, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Cuotas:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(210, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Factura:";
            // 
            // tCliente
            // 
            this.tCliente.Enabled = false;
            this.tCliente.Location = new System.Drawing.Point(61, 59);
            this.tCliente.Name = "tCliente";
            this.tCliente.Size = new System.Drawing.Size(83, 20);
            this.tCliente.TabIndex = 19;
            // 
            // bBuscarCliente
            // 
            this.bBuscarCliente.Location = new System.Drawing.Point(150, 57);
            this.bBuscarCliente.Name = "bBuscarCliente";
            this.bBuscarCliente.Size = new System.Drawing.Size(54, 22);
            this.bBuscarCliente.TabIndex = 18;
            this.bBuscarCliente.Text = "Cliente";
            this.bBuscarCliente.UseVisualStyleBackColor = true;
            this.bBuscarCliente.Click += new System.EventHandler(this.bBuscarCliente_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Cliente:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Sucursal:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Provincia:";
            // 
            // cSucursal
            // 
            this.cSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cSucursal.FormattingEnabled = true;
            this.cSucursal.Location = new System.Drawing.Point(61, 32);
            this.cSucursal.Name = "cSucursal";
            this.cSucursal.Size = new System.Drawing.Size(143, 21);
            this.cSucursal.TabIndex = 14;
            // 
            // cProvincia
            // 
            this.cProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cProvincia.FormattingEnabled = true;
            this.cProvincia.Location = new System.Drawing.Point(61, 5);
            this.cProvincia.Name = "cProvincia";
            this.cProvincia.Size = new System.Drawing.Size(143, 21);
            this.cProvincia.TabIndex = 13;
            // 
            // cFactura
            // 
            this.cFactura.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cFactura.Enabled = false;
            this.cFactura.FormattingEnabled = true;
            this.cFactura.Location = new System.Drawing.Point(258, 5);
            this.cFactura.Name = "cFactura";
            this.cFactura.Size = new System.Drawing.Size(114, 21);
            this.cFactura.TabIndex = 25;
            this.cFactura.SelectedIndexChanged += new System.EventHandler(this.cFactura_SelectedIndexChanged);
            // 
            // cCuotas
            // 
            this.cCuotas.Enabled = false;
            this.cCuotas.Location = new System.Drawing.Point(258, 33);
            this.cCuotas.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.cCuotas.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cCuotas.Name = "cCuotas";
            this.cCuotas.Size = new System.Drawing.Size(114, 20);
            this.cCuotas.TabIndex = 26;
            this.cCuotas.ThousandsSeparator = true;
            this.cCuotas.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cCuotas.ValueChanged += new System.EventHandler(this.cCuotas_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(213, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Factura TOTAL:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(214, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Cuota Unitaria:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(214, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Total Cuotas:";
            // 
            // lCuotas
            // 
            this.lCuotas.AutoSize = true;
            this.lCuotas.Location = new System.Drawing.Point(297, 94);
            this.lCuotas.Name = "lCuotas";
            this.lCuotas.Size = new System.Drawing.Size(25, 13);
            this.lCuotas.TabIndex = 32;
            this.lCuotas.Text = "000";
            // 
            // lCuota
            // 
            this.lCuota.AutoSize = true;
            this.lCuota.Location = new System.Drawing.Point(297, 76);
            this.lCuota.Name = "lCuota";
            this.lCuota.Size = new System.Drawing.Size(25, 13);
            this.lCuota.TabIndex = 31;
            this.lCuota.Text = "000";
            // 
            // lTotal
            // 
            this.lTotal.AutoSize = true;
            this.lTotal.Location = new System.Drawing.Point(296, 59);
            this.lTotal.Name = "lTotal";
            this.lTotal.Size = new System.Drawing.Size(25, 13);
            this.lTotal.TabIndex = 30;
            this.lTotal.Text = "000";
            // 
            // FormEfectuarPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 123);
            this.Controls.Add(this.lCuotas);
            this.Controls.Add(this.lCuota);
            this.Controls.Add(this.lTotal);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cCuotas);
            this.Controls.Add(this.cFactura);
            this.Controls.Add(this.bFacturar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tCliente);
            this.Controls.Add(this.bBuscarCliente);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cSucursal);
            this.Controls.Add(this.cProvincia);
            this.Name = "FormEfectuarPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Efectuar Pago";
            this.Load += new System.EventHandler(this.FormEfectuarPago_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cCuotas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bFacturar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tCliente;
        private System.Windows.Forms.Button bBuscarCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cSucursal;
        private System.Windows.Forms.ComboBox cProvincia;
        private System.Windows.Forms.ComboBox cFactura;
        private System.Windows.Forms.NumericUpDown cCuotas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lCuotas;
        private System.Windows.Forms.Label lCuota;
        private System.Windows.Forms.Label lTotal;
    }
}