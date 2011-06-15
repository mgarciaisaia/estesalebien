namespace VentaElectrodomesticos.Buscadores
{
    partial class BuscadorCliente
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
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.cBusqProvincia = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tBusqApellido = new System.Windows.Forms.TextBox();
            this.bLimpiarBusq = new System.Windows.Forms.Button();
            this.dgClientes = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bBuscar = new System.Windows.Forms.Button();
            this.tBusqDNI = new System.Windows.Forms.TextBox();
            this.tBusqNombre = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(447, 343);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(75, 23);
            this.btnSeleccionar.TabIndex = 67;
            this.btnSeleccionar.Text = "Aceptar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // cBusqProvincia
            // 
            this.cBusqProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBusqProvincia.FormattingEnabled = true;
            this.cBusqProvincia.Location = new System.Drawing.Point(287, 38);
            this.cBusqProvincia.Name = "cBusqProvincia";
            this.cBusqProvincia.Size = new System.Drawing.Size(121, 21);
            this.cBusqProvincia.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "Apellido:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Provincia:";
            // 
            // tBusqApellido
            // 
            this.tBusqApellido.Location = new System.Drawing.Point(287, 12);
            this.tBusqApellido.Name = "tBusqApellido";
            this.tBusqApellido.Size = new System.Drawing.Size(121, 20);
            this.tBusqApellido.TabIndex = 63;
            // 
            // bLimpiarBusq
            // 
            this.bLimpiarBusq.Location = new System.Drawing.Point(447, 33);
            this.bLimpiarBusq.Name = "bLimpiarBusq";
            this.bLimpiarBusq.Size = new System.Drawing.Size(75, 23);
            this.bLimpiarBusq.TabIndex = 57;
            this.bLimpiarBusq.Text = "Limpiar";
            this.bLimpiarBusq.UseVisualStyleBackColor = true;
            this.bLimpiarBusq.Click += new System.EventHandler(this.bLimpiarBuscadorCli_Click);
            // 
            // dgClientes
            // 
            this.dgClientes.AllowUserToAddRows = false;
            this.dgClientes.AllowUserToDeleteRows = false;
            this.dgClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgClientes.Location = new System.Drawing.Point(6, 68);
            this.dgClientes.MultiSelect = false;
            this.dgClientes.Name = "dgClientes";
            this.dgClientes.ReadOnly = true;
            this.dgClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgClientes.Size = new System.Drawing.Size(516, 269);
            this.dgClientes.TabIndex = 62;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(85, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 59;
            this.label8.Text = "DNI:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "Nombre:";
            // 
            // bBuscar
            // 
            this.bBuscar.Location = new System.Drawing.Point(447, 7);
            this.bBuscar.Name = "bBuscar";
            this.bBuscar.Size = new System.Drawing.Size(75, 23);
            this.bBuscar.TabIndex = 56;
            this.bBuscar.Text = "Buscar";
            this.bBuscar.UseVisualStyleBackColor = true;
            this.bBuscar.Click += new System.EventHandler(this.bBuscar_Click);
            // 
            // tBusqDNI
            // 
            this.tBusqDNI.Location = new System.Drawing.Point(114, 12);
            this.tBusqDNI.Name = "tBusqDNI";
            this.tBusqDNI.Size = new System.Drawing.Size(121, 20);
            this.tBusqDNI.TabIndex = 51;
            // 
            // tBusqNombre
            // 
            this.tBusqNombre.Location = new System.Drawing.Point(114, 38);
            this.tBusqNombre.Name = "tBusqNombre";
            this.tBusqNombre.Size = new System.Drawing.Size(121, 20);
            this.tBusqNombre.TabIndex = 52;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 17);
            this.label6.TabIndex = 54;
            this.label6.Text = "Filtros:";
            // 
            // BuscadorCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 370);
            this.Controls.Add(this.tBusqNombre);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.cBusqProvincia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tBusqApellido);
            this.Controls.Add(this.bLimpiarBusq);
            this.Controls.Add(this.dgClientes);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.bBuscar);
            this.Controls.Add(this.tBusqDNI);
            this.Controls.Add(this.label6);
            this.Name = "BuscadorCliente";
            this.Text = "BuscadorCliente";
            this.Load += new System.EventHandler(this.BuscadorCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.ComboBox cBusqProvincia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBusqApellido;
        private System.Windows.Forms.Button bLimpiarBusq;
        private System.Windows.Forms.DataGridView dgClientes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button bBuscar;
        private System.Windows.Forms.TextBox tBusqDNI;
        private System.Windows.Forms.TextBox tBusqNombre;
        private System.Windows.Forms.Label label6;
    }
}