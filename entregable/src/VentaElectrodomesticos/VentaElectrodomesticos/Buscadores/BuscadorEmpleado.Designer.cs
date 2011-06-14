namespace VentaElectrodomesticos.Buscadores
{
    partial class BuscadorEmpleado
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
            this.dgEmpleados = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bBuscar = new System.Windows.Forms.Button();
            this.cBusqSucursal = new System.Windows.Forms.ComboBox();
            this.cBusqTipo = new System.Windows.Forms.ComboBox();
            this.tBusqDNI = new System.Windows.Forms.TextBox();
            this.tBusqNombre = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tBusqApellido = new System.Windows.Forms.TextBox();
            this.cBusqProvincia = new System.Windows.Forms.ComboBox();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgEmpleados)).BeginInit();
            this.SuspendLayout();
            // 
            // bLimpiarBusq
            // 
            this.bLimpiarBusq.Location = new System.Drawing.Point(590, 35);
            this.bLimpiarBusq.Name = "bLimpiarBusq";
            this.bLimpiarBusq.Size = new System.Drawing.Size(75, 23);
            this.bLimpiarBusq.TabIndex = 39;
            this.bLimpiarBusq.Text = "Limpiar";
            this.bLimpiarBusq.UseVisualStyleBackColor = true;
            // 
            // dgEmpleados
            // 
            this.dgEmpleados.AllowUserToAddRows = false;
            this.dgEmpleados.AllowUserToDeleteRows = false;
            this.dgEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEmpleados.Location = new System.Drawing.Point(5, 70);
            this.dgEmpleados.MultiSelect = false;
            this.dgEmpleados.Name = "dgEmpleados";
            this.dgEmpleados.Size = new System.Drawing.Size(663, 269);
            this.dgEmpleados.TabIndex = 44;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(412, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 43;
            this.label10.Text = "Sucursal:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(422, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "Tipo:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(84, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "DNI:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(66, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Nombre:";
            // 
            // bBuscar
            // 
            this.bBuscar.Location = new System.Drawing.Point(590, 9);
            this.bBuscar.Name = "bBuscar";
            this.bBuscar.Size = new System.Drawing.Size(75, 23);
            this.bBuscar.TabIndex = 38;
            this.bBuscar.Text = "Buscar";
            this.bBuscar.UseVisualStyleBackColor = true;
            this.bBuscar.Click += new System.EventHandler(this.bBuscar_Click);
            // 
            // cBusqSucursal
            // 
            this.cBusqSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBusqSucursal.FormattingEnabled = true;
            this.cBusqSucursal.Location = new System.Drawing.Point(463, 36);
            this.cBusqSucursal.Name = "cBusqSucursal";
            this.cBusqSucursal.Size = new System.Drawing.Size(121, 21);
            this.cBusqSucursal.TabIndex = 37;
            // 
            // cBusqTipo
            // 
            this.cBusqTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBusqTipo.FormattingEnabled = true;
            this.cBusqTipo.Location = new System.Drawing.Point(463, 9);
            this.cBusqTipo.Name = "cBusqTipo";
            this.cBusqTipo.Size = new System.Drawing.Size(121, 21);
            this.cBusqTipo.TabIndex = 35;
            // 
            // tBusqDNI
            // 
            this.tBusqDNI.Location = new System.Drawing.Point(113, 14);
            this.tBusqDNI.Name = "tBusqDNI";
            this.tBusqDNI.Size = new System.Drawing.Size(121, 20);
            this.tBusqDNI.TabIndex = 33;
            // 
            // tBusqNombre
            // 
            this.tBusqNombre.Location = new System.Drawing.Point(113, 40);
            this.tBusqNombre.Name = "tBusqNombre";
            this.tBusqNombre.Size = new System.Drawing.Size(121, 20);
            this.tBusqNombre.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 17);
            this.label6.TabIndex = 36;
            this.label6.Text = "Filtros:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(239, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Apellido:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Provincia:";
            // 
            // tBusqApellido
            // 
            this.tBusqApellido.Location = new System.Drawing.Point(286, 14);
            this.tBusqApellido.Name = "tBusqApellido";
            this.tBusqApellido.Size = new System.Drawing.Size(121, 20);
            this.tBusqApellido.TabIndex = 45;
            // 
            // cBusqProvincia
            // 
            this.cBusqProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBusqProvincia.FormattingEnabled = true;
            this.cBusqProvincia.Location = new System.Drawing.Point(286, 40);
            this.cBusqProvincia.Name = "cBusqProvincia";
            this.cBusqProvincia.Size = new System.Drawing.Size(121, 21);
            this.cBusqProvincia.TabIndex = 49;
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(590, 345);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(75, 23);
            this.btnSeleccionar.TabIndex = 50;
            this.btnSeleccionar.Text = "Aceptar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // BuscadorEmpleado
            // 
            this.AcceptButton = this.bBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 374);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.cBusqProvincia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tBusqApellido);
            this.Controls.Add(this.bLimpiarBusq);
            this.Controls.Add(this.dgEmpleados);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.bBuscar);
            this.Controls.Add(this.cBusqSucursal);
            this.Controls.Add(this.cBusqTipo);
            this.Controls.Add(this.tBusqDNI);
            this.Controls.Add(this.tBusqNombre);
            this.Controls.Add(this.label6);
            this.Name = "BuscadorEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar Empleado";
            ((System.ComponentModel.ISupportInitialize)(this.dgEmpleados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bLimpiarBusq;
        private System.Windows.Forms.DataGridView dgEmpleados;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button bBuscar;
        private System.Windows.Forms.ComboBox cBusqSucursal;
        private System.Windows.Forms.ComboBox cBusqTipo;
        private System.Windows.Forms.TextBox tBusqDNI;
        private System.Windows.Forms.TextBox tBusqNombre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBusqApellido;
        private System.Windows.Forms.ComboBox cBusqProvincia;
        private System.Windows.Forms.Button btnSeleccionar;
    }
}