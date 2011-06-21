namespace VentaElectrodomesticos.Buscadores
{
    partial class BuscadorProducto
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
            this.bLimpiarBusq = new System.Windows.Forms.Button();
            this.dgProductos = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bBuscar = new System.Windows.Forms.Button();
            this.tCodigo = new System.Windows.Forms.TextBox();
            this.tNombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tCategoria = new System.Windows.Forms.TreeView();
            this.cPrecioMinimo = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cPrecioMaximo = new System.Windows.Forms.NumericUpDown();
            this.cMarca = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cPrecioMinimo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cPrecioMaximo)).BeginInit();
            this.SuspendLayout();
            // 
            // bLimpiarBusq
            // 
            this.bLimpiarBusq.Location = new System.Drawing.Point(555, 35);
            this.bLimpiarBusq.Name = "bLimpiarBusq";
            this.bLimpiarBusq.Size = new System.Drawing.Size(75, 23);
            this.bLimpiarBusq.TabIndex = 39;
            this.bLimpiarBusq.Text = "Limpiar";
            this.bLimpiarBusq.UseVisualStyleBackColor = true;
            this.bLimpiarBusq.Click += new System.EventHandler(this.bLimpiarBuscadorEmp_Click);
            // 
            // dgProductos
            // 
            this.dgProductos.AllowUserToAddRows = false;
            this.dgProductos.AllowUserToDeleteRows = false;
            this.dgProductos.AllowUserToResizeColumns = false;
            this.dgProductos.AllowUserToResizeRows = false;
            this.dgProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProductos.Location = new System.Drawing.Point(196, 64);
            this.dgProductos.MultiSelect = false;
            this.dgProductos.Name = "dgProductos";
            this.dgProductos.ReadOnly = true;
            this.dgProductos.RowHeadersVisible = false;
            this.dgProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgProductos.Size = new System.Drawing.Size(434, 269);
            this.dgProductos.TabIndex = 44;
            this.dgProductos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgProductos_CellDoubleClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(397, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "y";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "Codigo";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(190, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Nombre";
            // 
            // bBuscar
            // 
            this.bBuscar.Location = new System.Drawing.Point(555, 9);
            this.bBuscar.Name = "bBuscar";
            this.bBuscar.Size = new System.Drawing.Size(75, 23);
            this.bBuscar.TabIndex = 38;
            this.bBuscar.Text = "Buscar";
            this.bBuscar.UseVisualStyleBackColor = true;
            this.bBuscar.Click += new System.EventHandler(this.bBuscar_Click);
            // 
            // tCodigo
            // 
            this.tCodigo.Location = new System.Drawing.Point(58, 6);
            this.tCodigo.Name = "tCodigo";
            this.tCodigo.Size = new System.Drawing.Size(121, 20);
            this.tCodigo.TabIndex = 33;
            // 
            // tNombre
            // 
            this.tNombre.Location = new System.Drawing.Point(240, 6);
            this.tNombre.Name = "tNombre";
            this.tNombre.Size = new System.Drawing.Size(121, 20);
            this.tNombre.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(367, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Marca";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(206, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Precio entre";
            // 
            // tCategoria
            // 
            this.tCategoria.FullRowSelect = true;
            this.tCategoria.HideSelection = false;
            this.tCategoria.Location = new System.Drawing.Point(3, 64);
            this.tCategoria.Name = "tCategoria";
            this.tCategoria.Size = new System.Drawing.Size(185, 267);
            this.tCategoria.TabIndex = 51;
            // 
            // cPrecioMinimo
            // 
            this.cPrecioMinimo.Location = new System.Drawing.Point(276, 35);
            this.cPrecioMinimo.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.cPrecioMinimo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.cPrecioMinimo.Name = "cPrecioMinimo";
            this.cPrecioMinimo.Size = new System.Drawing.Size(115, 20);
            this.cPrecioMinimo.TabIndex = 52;
            this.cPrecioMinimo.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Categoria";
            // 
            // cPrecioMaximo
            // 
            this.cPrecioMaximo.Location = new System.Drawing.Point(410, 35);
            this.cPrecioMaximo.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.cPrecioMaximo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.cPrecioMaximo.Name = "cPrecioMaximo";
            this.cPrecioMaximo.Size = new System.Drawing.Size(115, 20);
            this.cPrecioMaximo.TabIndex = 56;
            this.cPrecioMaximo.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // cMarca
            // 
            this.cMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cMarca.FormattingEnabled = true;
            this.cMarca.Location = new System.Drawing.Point(410, 5);
            this.cMarca.Name = "cMarca";
            this.cMarca.Size = new System.Drawing.Size(121, 21);
            this.cMarca.TabIndex = 57;
            // 
            // BuscadorProducto
            // 
            this.AcceptButton = this.bBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 343);
            this.Controls.Add(this.cMarca);
            this.Controls.Add(this.cPrecioMaximo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cPrecioMinimo);
            this.Controls.Add(this.tCategoria);
            this.Controls.Add(this.tNombre);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bLimpiarBusq);
            this.Controls.Add(this.dgProductos);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.bBuscar);
            this.Controls.Add(this.tCodigo);
            this.Name = "BuscadorProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar Producto";
            this.Load += new System.EventHandler(this.BuscadorProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cPrecioMinimo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cPrecioMaximo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bLimpiarBusq;
        private System.Windows.Forms.DataGridView dgProductos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button bBuscar;
        private System.Windows.Forms.TextBox tCodigo;
        private System.Windows.Forms.TextBox tNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView tCategoria;
        private System.Windows.Forms.NumericUpDown cPrecioMinimo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown cPrecioMaximo;
        private System.Windows.Forms.ComboBox cMarca;
    }
}
