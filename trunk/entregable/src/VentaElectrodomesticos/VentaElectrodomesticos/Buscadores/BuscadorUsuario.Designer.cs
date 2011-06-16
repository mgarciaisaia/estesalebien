namespace VentaElectrodomesticos.Buscadores
{
    partial class BuscadorUsuario
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
            this.tBusqNombre = new System.Windows.Forms.TextBox();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.cBusqProvincia = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tBusqApellido = new System.Windows.Forms.TextBox();
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
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tBusqUser = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgEmpleados)).BeginInit();
            this.SuspendLayout();
            // 
            // tBusqNombre
            // 
            this.tBusqNombre.Location = new System.Drawing.Point(267, 8);
            this.tBusqNombre.Name = "tBusqNombre";
            this.tBusqNombre.Size = new System.Drawing.Size(121, 20);
            this.tBusqNombre.TabIndex = 52;
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(660, 59);
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
            this.cBusqProvincia.Location = new System.Drawing.Point(457, 8);
            this.cBusqProvincia.Name = "cBusqProvincia";
            this.cBusqProvincia.Size = new System.Drawing.Size(173, 21);
            this.cBusqProvincia.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(213, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "Apellido:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(397, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Provincia:";
            // 
            // tBusqApellido
            // 
            this.tBusqApellido.Location = new System.Drawing.Point(267, 36);
            this.tBusqApellido.Name = "tBusqApellido";
            this.tBusqApellido.Size = new System.Drawing.Size(121, 20);
            this.tBusqApellido.TabIndex = 63;
            // 
            // bLimpiarBusq
            // 
            this.bLimpiarBusq.Location = new System.Drawing.Point(660, 30);
            this.bLimpiarBusq.Name = "bLimpiarBusq";
            this.bLimpiarBusq.Size = new System.Drawing.Size(75, 23);
            this.bLimpiarBusq.TabIndex = 57;
            this.bLimpiarBusq.Text = "Limpiar";
            this.bLimpiarBusq.UseVisualStyleBackColor = true;
            this.bLimpiarBusq.Click += new System.EventHandler(this.bLimpiarBuscadorEmp_Click);
            // 
            // dgEmpleados
            // 
            this.dgEmpleados.AllowUserToAddRows = false;
            this.dgEmpleados.AllowUserToDeleteRows = false;
            this.dgEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEmpleados.Location = new System.Drawing.Point(4, 90);
            this.dgEmpleados.MultiSelect = false;
            this.dgEmpleados.Name = "dgEmpleados";
            this.dgEmpleados.ReadOnly = true;
            this.dgEmpleados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEmpleados.Size = new System.Drawing.Size(731, 269);
            this.dgEmpleados.TabIndex = 62;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(400, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 61;
            this.label10.Text = "Sucursal:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(420, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "Tipo:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(57, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 59;
            this.label8.Text = "DNI:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(220, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "Nombre:";
            // 
            // bBuscar
            // 
            this.bBuscar.Location = new System.Drawing.Point(660, 4);
            this.bBuscar.Name = "bBuscar";
            this.bBuscar.Size = new System.Drawing.Size(75, 23);
            this.bBuscar.TabIndex = 56;
            this.bBuscar.Text = "Buscar";
            this.bBuscar.UseVisualStyleBackColor = true;
            this.bBuscar.Click += new System.EventHandler(this.bBuscar_Click);
            // 
            // cBusqSucursal
            // 
            this.cBusqSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBusqSucursal.FormattingEnabled = true;
            this.cBusqSucursal.Location = new System.Drawing.Point(457, 58);
            this.cBusqSucursal.Name = "cBusqSucursal";
            this.cBusqSucursal.Size = new System.Drawing.Size(173, 21);
            this.cBusqSucursal.TabIndex = 55;
            // 
            // cBusqTipo
            // 
            this.cBusqTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBusqTipo.FormattingEnabled = true;
            this.cBusqTipo.Location = new System.Drawing.Point(457, 32);
            this.cBusqTipo.Name = "cBusqTipo";
            this.cBusqTipo.Size = new System.Drawing.Size(173, 21);
            this.cBusqTipo.TabIndex = 53;
            // 
            // tBusqDNI
            // 
            this.tBusqDNI.Location = new System.Drawing.Point(86, 9);
            this.tBusqDNI.Name = "tBusqDNI";
            this.tBusqDNI.Size = new System.Drawing.Size(121, 20);
            this.tBusqDNI.TabIndex = 51;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 17);
            this.label6.TabIndex = 54;
            this.label6.Text = "Filtros:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "Usuario:";
            // 
            // tBusqUser
            // 
            this.tBusqUser.Location = new System.Drawing.Point(86, 36);
            this.tBusqUser.Name = "tBusqUser";
            this.tBusqUser.Size = new System.Drawing.Size(121, 20);
            this.tBusqUser.TabIndex = 68;
            // 
            // BuscadorUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 370);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tBusqUser);
            this.Controls.Add(this.tBusqNombre);
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
            this.Controls.Add(this.label6);
            this.Name = "BuscadorUsuario";
            this.Text = "BuscadorUsuario";
            ((System.ComponentModel.ISupportInitialize)(this.dgEmpleados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tBusqNombre;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.ComboBox cBusqProvincia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBusqApellido;
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBusqUser;
    }
}