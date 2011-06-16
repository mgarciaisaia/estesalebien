namespace VentaElectrodomesticos.AsignacionStock
{
    partial class FormAsignacionStock
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
            this.groupFiltros = new System.Windows.Forms.GroupBox();
            this.bBuscarProducto = new System.Windows.Forms.Button();
            this.cSucursal = new System.Windows.Forms.ComboBox();
            this.bAuditor = new System.Windows.Forms.Button();
            this.tAuditor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bProducto = new System.Windows.Forms.Button();
            this.tProducto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tProductoSeleccionado = new System.Windows.Forms.TextBox();
            this.groupStock = new System.Windows.Forms.GroupBox();
            this.cNuevoStock = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.tStock = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tSucursalSeleccionada = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tAuditorSeleccionado = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupFiltros.SuspendLayout();
            this.groupStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cNuevoStock)).BeginInit();
            this.SuspendLayout();
            // 
            // groupFiltros
            // 
            this.groupFiltros.Controls.Add(this.bBuscarProducto);
            this.groupFiltros.Controls.Add(this.cSucursal);
            this.groupFiltros.Controls.Add(this.bAuditor);
            this.groupFiltros.Controls.Add(this.tAuditor);
            this.groupFiltros.Controls.Add(this.label3);
            this.groupFiltros.Controls.Add(this.label2);
            this.groupFiltros.Controls.Add(this.bProducto);
            this.groupFiltros.Controls.Add(this.tProducto);
            this.groupFiltros.Controls.Add(this.label1);
            this.groupFiltros.Location = new System.Drawing.Point(7, 9);
            this.groupFiltros.Name = "groupFiltros";
            this.groupFiltros.Size = new System.Drawing.Size(290, 120);
            this.groupFiltros.TabIndex = 0;
            this.groupFiltros.TabStop = false;
            this.groupFiltros.Text = "Filtros";
            // 
            // bBuscarProducto
            // 
            this.bBuscarProducto.Location = new System.Drawing.Point(205, 94);
            this.bBuscarProducto.Name = "bBuscarProducto";
            this.bBuscarProducto.Size = new System.Drawing.Size(72, 20);
            this.bBuscarProducto.TabIndex = 10;
            this.bBuscarProducto.Text = "OK";
            this.bBuscarProducto.UseVisualStyleBackColor = true;
            this.bBuscarProducto.Click += new System.EventHandler(this.bBuscarProducto_Click);
            // 
            // cSucursal
            // 
            this.cSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cSucursal.FormattingEnabled = true;
            this.cSucursal.Location = new System.Drawing.Point(64, 67);
            this.cSucursal.Name = "cSucursal";
            this.cSucursal.Size = new System.Drawing.Size(135, 21);
            this.cSucursal.Sorted = true;
            this.cSucursal.TabIndex = 9;
            // 
            // bAuditor
            // 
            this.bAuditor.Location = new System.Drawing.Point(205, 42);
            this.bAuditor.Name = "bAuditor";
            this.bAuditor.Size = new System.Drawing.Size(72, 20);
            this.bAuditor.TabIndex = 8;
            this.bAuditor.Text = "Seleccionar";
            this.bAuditor.UseVisualStyleBackColor = true;
            this.bAuditor.Click += new System.EventHandler(this.bAuditor_Click);
            // 
            // tAuditor
            // 
            this.tAuditor.Enabled = false;
            this.tAuditor.Location = new System.Drawing.Point(64, 42);
            this.tAuditor.Name = "tAuditor";
            this.tAuditor.Size = new System.Drawing.Size(135, 20);
            this.tAuditor.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Auditor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sucursal";
            // 
            // bProducto
            // 
            this.bProducto.Location = new System.Drawing.Point(205, 16);
            this.bProducto.Name = "bProducto";
            this.bProducto.Size = new System.Drawing.Size(72, 20);
            this.bProducto.TabIndex = 2;
            this.bProducto.Text = "Seleccionar";
            this.bProducto.UseVisualStyleBackColor = true;
            this.bProducto.Click += new System.EventHandler(this.bProducto_Click);
            // 
            // tProducto
            // 
            this.tProducto.Enabled = false;
            this.tProducto.Location = new System.Drawing.Point(64, 16);
            this.tProducto.Name = "tProducto";
            this.tProducto.Size = new System.Drawing.Size(135, 20);
            this.tProducto.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Producto";
            // 
            // tProductoSeleccionado
            // 
            this.tProductoSeleccionado.Enabled = false;
            this.tProductoSeleccionado.Location = new System.Drawing.Point(60, 16);
            this.tProductoSeleccionado.Name = "tProductoSeleccionado";
            this.tProductoSeleccionado.Size = new System.Drawing.Size(135, 20);
            this.tProductoSeleccionado.TabIndex = 8;
            // 
            // groupStock
            // 
            this.groupStock.Controls.Add(this.tAuditorSeleccionado);
            this.groupStock.Controls.Add(this.label8);
            this.groupStock.Controls.Add(this.cNuevoStock);
            this.groupStock.Controls.Add(this.label6);
            this.groupStock.Controls.Add(this.tStock);
            this.groupStock.Controls.Add(this.label7);
            this.groupStock.Controls.Add(this.button1);
            this.groupStock.Controls.Add(this.label5);
            this.groupStock.Controls.Add(this.tSucursalSeleccionada);
            this.groupStock.Controls.Add(this.label4);
            this.groupStock.Controls.Add(this.tProductoSeleccionado);
            this.groupStock.Enabled = false;
            this.groupStock.Location = new System.Drawing.Point(11, 144);
            this.groupStock.Name = "groupStock";
            this.groupStock.Size = new System.Drawing.Size(400, 110);
            this.groupStock.TabIndex = 1;
            this.groupStock.TabStop = false;
            this.groupStock.Text = "Stock";
            // 
            // cNuevoStock
            // 
            this.cNuevoStock.Location = new System.Drawing.Point(101, 76);
            this.cNuevoStock.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.cNuevoStock.Name = "cNuevoStock";
            this.cNuevoStock.Size = new System.Drawing.Size(120, 20);
            this.cNuevoStock.TabIndex = 19;
            this.cNuevoStock.ThousandsSeparator = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Nuevo stock";
            // 
            // tStock
            // 
            this.tStock.Enabled = false;
            this.tStock.Location = new System.Drawing.Point(274, 45);
            this.tStock.Name = "tStock";
            this.tStock.Size = new System.Drawing.Size(116, 20);
            this.tStock.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(201, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Stock actual";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(255, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 20);
            this.button1.TabIndex = 15;
            this.button1.Text = "Actualizar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(201, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Sucursal";
            // 
            // tSucursalSeleccionada
            // 
            this.tSucursalSeleccionada.Enabled = false;
            this.tSucursalSeleccionada.Location = new System.Drawing.Point(255, 16);
            this.tSucursalSeleccionada.Name = "tSucursalSeleccionada";
            this.tSucursalSeleccionada.Size = new System.Drawing.Size(135, 20);
            this.tSucursalSeleccionada.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Producto";
            // 
            // tAuditorSeleccionado
            // 
            this.tAuditorSeleccionado.Enabled = false;
            this.tAuditorSeleccionado.Location = new System.Drawing.Point(60, 45);
            this.tAuditorSeleccionado.Name = "tAuditorSeleccionado";
            this.tAuditorSeleccionado.Size = new System.Drawing.Size(135, 20);
            this.tAuditorSeleccionado.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Auditor";
            // 
            // FormAsignacionStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 264);
            this.Controls.Add(this.groupStock);
            this.Controls.Add(this.groupFiltros);
            this.Name = "FormAsignacionStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignación de Stock";
            this.Load += new System.EventHandler(this.FormAsignacionStock_Load);
            this.groupFiltros.ResumeLayout(false);
            this.groupFiltros.PerformLayout();
            this.groupStock.ResumeLayout(false);
            this.groupStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cNuevoStock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupFiltros;
        private System.Windows.Forms.TextBox tProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cSucursal;
        private System.Windows.Forms.Button bAuditor;
        private System.Windows.Forms.TextBox tAuditor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bProducto;
        private System.Windows.Forms.Button bBuscarProducto;
        private System.Windows.Forms.TextBox tProductoSeleccionado;
        private System.Windows.Forms.GroupBox groupStock;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tSucursalSeleccionada;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tStock;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown cNuevoStock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tAuditorSeleccionado;
        private System.Windows.Forms.Label label8;
    }
}