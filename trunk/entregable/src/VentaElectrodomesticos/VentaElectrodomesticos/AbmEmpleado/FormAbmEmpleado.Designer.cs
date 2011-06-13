namespace VentaElectrodomesticos.AbmEmpleado
{
    partial class FormAbmEmpleado
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bLimpiarABM = new System.Windows.Forms.Button();
            this.bCheck = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.bEliminar = new System.Windows.Forms.Button();
            this.bModificar = new System.Windows.Forms.Button();
            this.bAgregar = new System.Windows.Forms.Button();
            this.cSucursal = new System.Windows.Forms.ComboBox();
            this.cBanco = new System.Windows.Forms.ComboBox();
            this.tMail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tApellido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tDNI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgClientes = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bBuscar = new System.Windows.Forms.Button();
            this.cBusqSucursal = new System.Windows.Forms.ComboBox();
            this.cBusqBanco = new System.Windows.Forms.ComboBox();
            this.tBusqDNI = new System.Windows.Forms.TextBox();
            this.tBusqNombre = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // bLimpiarBusq
            // 
            this.bLimpiarBusq.Location = new System.Drawing.Point(443, 216);
            this.bLimpiarBusq.Name = "bLimpiarBusq";
            this.bLimpiarBusq.Size = new System.Drawing.Size(75, 23);
            this.bLimpiarBusq.TabIndex = 39;
            this.bLimpiarBusq.Text = "Limpiar";
            this.bLimpiarBusq.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bLimpiarABM);
            this.groupBox1.Controls.Add(this.bCheck);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.bEliminar);
            this.groupBox1.Controls.Add(this.bModificar);
            this.groupBox1.Controls.Add(this.bAgregar);
            this.groupBox1.Controls.Add(this.cSucursal);
            this.groupBox1.Controls.Add(this.cBanco);
            this.groupBox1.Controls.Add(this.tMail);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tApellido);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tNombre);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tDNI);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 163);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ABM";
            // 
            // bLimpiarABM
            // 
            this.bLimpiarABM.Location = new System.Drawing.Point(367, 100);
            this.bLimpiarABM.Name = "bLimpiarABM";
            this.bLimpiarABM.Size = new System.Drawing.Size(75, 23);
            this.bLimpiarABM.TabIndex = 11;
            this.bLimpiarABM.Text = "Limpiar";
            this.bLimpiarABM.UseVisualStyleBackColor = true;
            // 
            // bCheck
            // 
            this.bCheck.Location = new System.Drawing.Point(162, 24);
            this.bCheck.Name = "bCheck";
            this.bCheck.Size = new System.Drawing.Size(75, 23);
            this.bCheck.TabIndex = 2;
            this.bCheck.Text = "Chequear";
            this.bCheck.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(256, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "Sucursal:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(266, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Banco:";
            // 
            // bEliminar
            // 
            this.bEliminar.Enabled = false;
            this.bEliminar.Location = new System.Drawing.Point(367, 129);
            this.bEliminar.Name = "bEliminar";
            this.bEliminar.Size = new System.Drawing.Size(75, 23);
            this.bEliminar.TabIndex = 10;
            this.bEliminar.Text = "Dar baja";
            this.bEliminar.UseVisualStyleBackColor = true;
            // 
            // bModificar
            // 
            this.bModificar.Enabled = false;
            this.bModificar.Location = new System.Drawing.Point(286, 129);
            this.bModificar.Name = "bModificar";
            this.bModificar.Size = new System.Drawing.Size(75, 23);
            this.bModificar.TabIndex = 9;
            this.bModificar.Text = "Modificar";
            this.bModificar.UseVisualStyleBackColor = true;
            // 
            // bAgregar
            // 
            this.bAgregar.Location = new System.Drawing.Point(205, 129);
            this.bAgregar.Name = "bAgregar";
            this.bAgregar.Size = new System.Drawing.Size(75, 23);
            this.bAgregar.TabIndex = 8;
            this.bAgregar.Text = "Agregar";
            this.bAgregar.UseVisualStyleBackColor = true;
            // 
            // cSucursal
            // 
            this.cSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cSucursal.Enabled = false;
            this.cSucursal.FormattingEnabled = true;
            this.cSucursal.Location = new System.Drawing.Point(307, 52);
            this.cSucursal.Name = "cSucursal";
            this.cSucursal.Size = new System.Drawing.Size(121, 21);
            this.cSucursal.TabIndex = 7;
            // 
            // cBanco
            // 
            this.cBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBanco.Enabled = false;
            this.cBanco.FormattingEnabled = true;
            this.cBanco.Location = new System.Drawing.Point(307, 26);
            this.cBanco.Name = "cBanco";
            this.cBanco.Size = new System.Drawing.Size(121, 21);
            this.cBanco.TabIndex = 6;
            // 
            // tMail
            // 
            this.tMail.Enabled = false;
            this.tMail.Location = new System.Drawing.Point(55, 103);
            this.tMail.Name = "tMail";
            this.tMail.Size = new System.Drawing.Size(100, 20);
            this.tMail.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Mail:";
            // 
            // tApellido
            // 
            this.tApellido.Enabled = false;
            this.tApellido.Location = new System.Drawing.Point(55, 77);
            this.tApellido.Name = "tApellido";
            this.tApellido.Size = new System.Drawing.Size(100, 20);
            this.tApellido.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Apellido:";
            // 
            // tNombre
            // 
            this.tNombre.Enabled = false;
            this.tNombre.Location = new System.Drawing.Point(55, 51);
            this.tNombre.Name = "tNombre";
            this.tNombre.Size = new System.Drawing.Size(100, 20);
            this.tNombre.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nombre:";
            // 
            // tDNI
            // 
            this.tDNI.Location = new System.Drawing.Point(55, 25);
            this.tDNI.Name = "tDNI";
            this.tDNI.Size = new System.Drawing.Size(100, 20);
            this.tDNI.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "DNI:";
            // 
            // dgClientes
            // 
            this.dgClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgClientes.Location = new System.Drawing.Point(7, 249);
            this.dgClientes.Name = "dgClientes";
            this.dgClientes.Size = new System.Drawing.Size(663, 269);
            this.dgClientes.TabIndex = 44;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(247, 222);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 43;
            this.label10.Text = "Sucursal:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(257, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "Banco:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(86, 195);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "DNI:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 222);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Nombre  Apellido:";
            // 
            // bBuscar
            // 
            this.bBuscar.Location = new System.Drawing.Point(443, 190);
            this.bBuscar.Name = "bBuscar";
            this.bBuscar.Size = new System.Drawing.Size(75, 23);
            this.bBuscar.TabIndex = 38;
            this.bBuscar.Text = "Buscar";
            this.bBuscar.UseVisualStyleBackColor = true;
            // 
            // cBusqSucursal
            // 
            this.cBusqSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBusqSucursal.FormattingEnabled = true;
            this.cBusqSucursal.Location = new System.Drawing.Point(298, 218);
            this.cBusqSucursal.Name = "cBusqSucursal";
            this.cBusqSucursal.Size = new System.Drawing.Size(121, 21);
            this.cBusqSucursal.TabIndex = 37;
            // 
            // cBusqBanco
            // 
            this.cBusqBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBusqBanco.FormattingEnabled = true;
            this.cBusqBanco.Location = new System.Drawing.Point(298, 191);
            this.cBusqBanco.Name = "cBusqBanco";
            this.cBusqBanco.Size = new System.Drawing.Size(121, 21);
            this.cBusqBanco.TabIndex = 35;
            // 
            // tBusqDNI
            // 
            this.tBusqDNI.Location = new System.Drawing.Point(115, 193);
            this.tBusqDNI.Name = "tBusqDNI";
            this.tBusqDNI.Size = new System.Drawing.Size(121, 20);
            this.tBusqDNI.TabIndex = 33;
            // 
            // tBusqNombre
            // 
            this.tBusqNombre.Location = new System.Drawing.Point(115, 219);
            this.tBusqNombre.Name = "tBusqNombre";
            this.tBusqNombre.Size = new System.Drawing.Size(121, 20);
            this.tBusqNombre.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 17);
            this.label6.TabIndex = 36;
            this.label6.Text = "Filtros:";
            // 
            // FormAbmEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 526);
            this.Controls.Add(this.bLimpiarBusq);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgClientes);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.bBuscar);
            this.Controls.Add(this.cBusqSucursal);
            this.Controls.Add(this.cBusqBanco);
            this.Controls.Add(this.tBusqDNI);
            this.Controls.Add(this.tBusqNombre);
            this.Controls.Add(this.label6);
            this.Name = "FormAbmEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABM Empleado";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bLimpiarBusq;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bLimpiarABM;
        private System.Windows.Forms.Button bCheck;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button bEliminar;
        private System.Windows.Forms.Button bModificar;
        private System.Windows.Forms.Button bAgregar;
        private System.Windows.Forms.ComboBox cSucursal;
        private System.Windows.Forms.ComboBox cBanco;
        private System.Windows.Forms.TextBox tMail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tApellido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tDNI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgClientes;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button bBuscar;
        private System.Windows.Forms.ComboBox cBusqSucursal;
        private System.Windows.Forms.ComboBox cBusqBanco;
        private System.Windows.Forms.TextBox tBusqDNI;
        private System.Windows.Forms.TextBox tBusqNombre;
        private System.Windows.Forms.Label label6;
    }
}