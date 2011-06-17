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
            ((System.ComponentModel.ISupportInitialize)(this.cCuotas)).BeginInit();
            this.SuspendLayout();
            // 
            // bFacturar
            // 
            this.bFacturar.Location = new System.Drawing.Point(258, 59);
            this.bFacturar.Name = "bFacturar";
            this.bFacturar.Size = new System.Drawing.Size(114, 33);
            this.bFacturar.TabIndex = 24;
            this.bFacturar.Text = "Pagar";
            this.bFacturar.UseVisualStyleBackColor = true;
            this.bFacturar.Click += new System.EventHandler(this.bFacturar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Cuotas:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Factura:";
            // 
            // tCliente
            // 
            this.tCliente.Location = new System.Drawing.Point(71, 59);
            this.tCliente.Name = "tCliente";
            this.tCliente.Size = new System.Drawing.Size(83, 20);
            this.tCliente.TabIndex = 19;
            // 
            // bBuscarCliente
            // 
            this.bBuscarCliente.Location = new System.Drawing.Point(160, 57);
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
            this.cSucursal.FormattingEnabled = true;
            this.cSucursal.Location = new System.Drawing.Point(71, 32);
            this.cSucursal.Name = "cSucursal";
            this.cSucursal.Size = new System.Drawing.Size(112, 21);
            this.cSucursal.TabIndex = 14;
            // 
            // cProvincia
            // 
            this.cProvincia.FormattingEnabled = true;
            this.cProvincia.Location = new System.Drawing.Point(71, 5);
            this.cProvincia.Name = "cProvincia";
            this.cProvincia.Size = new System.Drawing.Size(112, 21);
            this.cProvincia.TabIndex = 13;
            // 
            // cFactura
            // 
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
            // 
            // FormEfectuarPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 108);
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
    }
}