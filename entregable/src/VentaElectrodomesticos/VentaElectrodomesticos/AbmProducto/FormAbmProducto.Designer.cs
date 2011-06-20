namespace VentaElectrodomesticos.AbmProducto
{
    partial class FormAbmProducto
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
            this.BuscarProducto = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cMarca = new System.Windows.Forms.ComboBox();
            this.cPrecio = new System.Windows.Forms.NumericUpDown();
            this.cHabilitado = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bLimpiarABM = new System.Windows.Forms.Button();
            this.bEliminar = new System.Windows.Forms.Button();
            this.bModificar = new System.Windows.Forms.Button();
            this.bAgregar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tDescripcion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tCategoria = new System.Windows.Forms.TreeView();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cPrecio)).BeginInit();
            this.SuspendLayout();
            // 
            // BuscarProducto
            // 
            this.BuscarProducto.Location = new System.Drawing.Point(12, 12);
            this.BuscarProducto.Name = "BuscarProducto";
            this.BuscarProducto.Size = new System.Drawing.Size(126, 23);
            this.BuscarProducto.TabIndex = 48;
            this.BuscarProducto.Text = "Buscar Producto";
            this.BuscarProducto.UseVisualStyleBackColor = true;
            this.BuscarProducto.Click += new System.EventHandler(this.BuscarProducto_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cMarca);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cPrecio);
            this.groupBox1.Controls.Add(this.tCategoria);
            this.groupBox1.Controls.Add(this.cHabilitado);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.bLimpiarABM);
            this.groupBox1.Controls.Add(this.bEliminar);
            this.groupBox1.Controls.Add(this.bModificar);
            this.groupBox1.Controls.Add(this.bAgregar);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tDescripcion);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tNombre);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tCodigo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 228);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ABM";
            // 
            // cMarca
            // 
            this.cMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cMarca.FormattingEnabled = true;
            this.cMarca.Location = new System.Drawing.Point(76, 129);
            this.cMarca.Name = "cMarca";
            this.cMarca.Size = new System.Drawing.Size(120, 21);
            this.cMarca.TabIndex = 55;
            // 
            // cPrecio
            // 
            this.cPrecio.Location = new System.Drawing.Point(76, 103);
            this.cPrecio.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.cPrecio.Name = "cPrecio";
            this.cPrecio.Size = new System.Drawing.Size(120, 20);
            this.cPrecio.TabIndex = 53;
            // 
            // cHabilitado
            // 
            this.cHabilitado.AutoSize = true;
            this.cHabilitado.Location = new System.Drawing.Point(102, 154);
            this.cHabilitado.Name = "cHabilitado";
            this.cHabilitado.Size = new System.Drawing.Size(73, 17);
            this.cHabilitado.TabIndex = 34;
            this.cHabilitado.Text = "Habilitado";
            this.cHabilitado.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Marca";
            // 
            // bLimpiarABM
            // 
            this.bLimpiarABM.Location = new System.Drawing.Point(271, 195);
            this.bLimpiarABM.Name = "bLimpiarABM";
            this.bLimpiarABM.Size = new System.Drawing.Size(75, 23);
            this.bLimpiarABM.TabIndex = 11;
            this.bLimpiarABM.Text = "Limpiar";
            this.bLimpiarABM.UseVisualStyleBackColor = true;
            this.bLimpiarABM.Click += new System.EventHandler(this.bLimpiarABM_Click);
            // 
            // bEliminar
            // 
            this.bEliminar.Location = new System.Drawing.Point(190, 195);
            this.bEliminar.Name = "bEliminar";
            this.bEliminar.Size = new System.Drawing.Size(75, 23);
            this.bEliminar.TabIndex = 10;
            this.bEliminar.Text = "Dar baja";
            this.bEliminar.UseVisualStyleBackColor = true;
            this.bEliminar.Click += new System.EventHandler(this.bEliminar_Click);
            // 
            // bModificar
            // 
            this.bModificar.Location = new System.Drawing.Point(109, 195);
            this.bModificar.Name = "bModificar";
            this.bModificar.Size = new System.Drawing.Size(75, 23);
            this.bModificar.TabIndex = 9;
            this.bModificar.Text = "Modificar";
            this.bModificar.UseVisualStyleBackColor = true;
            this.bModificar.Click += new System.EventHandler(this.bModificar_Click);
            // 
            // bAgregar
            // 
            this.bAgregar.Location = new System.Drawing.Point(28, 195);
            this.bAgregar.Name = "bAgregar";
            this.bAgregar.Size = new System.Drawing.Size(75, 23);
            this.bAgregar.TabIndex = 8;
            this.bAgregar.Text = "Agregar";
            this.bAgregar.UseVisualStyleBackColor = true;
            this.bAgregar.Click += new System.EventHandler(this.bAgregar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Precio";
            // 
            // tDescripcion
            // 
            this.tDescripcion.Location = new System.Drawing.Point(76, 77);
            this.tDescripcion.Name = "tDescripcion";
            this.tDescripcion.Size = new System.Drawing.Size(120, 20);
            this.tDescripcion.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Descripción";
            // 
            // tNombre
            // 
            this.tNombre.Location = new System.Drawing.Point(76, 51);
            this.tNombre.Name = "tNombre";
            this.tNombre.Size = new System.Drawing.Size(120, 20);
            this.tNombre.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nombre";
            // 
            // tCodigo
            // 
            this.tCodigo.Location = new System.Drawing.Point(76, 25);
            this.tCodigo.Name = "tCodigo";
            this.tCodigo.Size = new System.Drawing.Size(120, 20);
            this.tCodigo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Codigo";
            // 
            // tCategoria
            // 
            this.tCategoria.FullRowSelect = true;
            this.tCategoria.HideSelection = false;
            this.tCategoria.Location = new System.Drawing.Point(212, 33);
            this.tCategoria.Name = "tCategoria";
            this.tCategoria.Size = new System.Drawing.Size(189, 138);
            this.tCategoria.TabIndex = 52;
            this.tCategoria.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tCategoria_BeforeSelect);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(213, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Categoria";
            // 
            // FormAbmProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 278);
            this.Controls.Add(this.BuscarProducto);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormAbmProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABM Producto";
            this.Load += new System.EventHandler(this.FormAbmProducto_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cPrecio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BuscarProducto;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cHabilitado;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bLimpiarABM;
        private System.Windows.Forms.Button bEliminar;
        private System.Windows.Forms.Button bModificar;
        private System.Windows.Forms.Button bAgregar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tDescripcion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown cPrecio;
        private System.Windows.Forms.ComboBox cMarca;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TreeView tCategoria;
    }
}