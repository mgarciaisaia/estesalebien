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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bLimpiarABM = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.bEliminar = new System.Windows.Forms.Button();
            this.bModificar = new System.Windows.Forms.Button();
            this.bAgregar = new System.Windows.Forms.Button();
            this.cSucursal = new System.Windows.Forms.ComboBox();
            this.tMail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tApellido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tDNI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BuscarEmpleado = new System.Windows.Forms.Button();
            this.tTelefono = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cProvincia = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tDireccion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cHabilitado = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cTipo = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cTipo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cHabilitado);
            this.groupBox1.Controls.Add(this.tDireccion);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tTelefono);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.bLimpiarABM);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.bEliminar);
            this.groupBox1.Controls.Add(this.bModificar);
            this.groupBox1.Controls.Add(this.bAgregar);
            this.groupBox1.Controls.Add(this.cSucursal);
            this.groupBox1.Controls.Add(this.cProvincia);
            this.groupBox1.Controls.Add(this.tMail);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tApellido);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tNombre);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tDNI);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 190);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ABM";
            // 
            // bLimpiarABM
            // 
            this.bLimpiarABM.Location = new System.Drawing.Point(271, 152);
            this.bLimpiarABM.Name = "bLimpiarABM";
            this.bLimpiarABM.Size = new System.Drawing.Size(75, 23);
            this.bLimpiarABM.TabIndex = 11;
            this.bLimpiarABM.Text = "Limpiar";
            this.bLimpiarABM.UseVisualStyleBackColor = true;
            this.bLimpiarABM.Click += new System.EventHandler(this.bLimpiarABM_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(190, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "Sucursal:";
            // 
            // bEliminar
            // 
            this.bEliminar.Enabled = false;
            this.bEliminar.Location = new System.Drawing.Point(190, 152);
            this.bEliminar.Name = "bEliminar";
            this.bEliminar.Size = new System.Drawing.Size(75, 23);
            this.bEliminar.TabIndex = 10;
            this.bEliminar.Text = "Dar baja";
            this.bEliminar.UseVisualStyleBackColor = true;
            this.bEliminar.Click += new System.EventHandler(this.bEliminar_Click);
            // 
            // bModificar
            // 
            this.bModificar.Enabled = false;
            this.bModificar.Location = new System.Drawing.Point(109, 152);
            this.bModificar.Name = "bModificar";
            this.bModificar.Size = new System.Drawing.Size(75, 23);
            this.bModificar.TabIndex = 9;
            this.bModificar.Text = "Modificar";
            this.bModificar.UseVisualStyleBackColor = true;
            this.bModificar.Click += new System.EventHandler(this.bModificar_Click);
            // 
            // bAgregar
            // 
            this.bAgregar.Location = new System.Drawing.Point(28, 152);
            this.bAgregar.Name = "bAgregar";
            this.bAgregar.Size = new System.Drawing.Size(75, 23);
            this.bAgregar.TabIndex = 8;
            this.bAgregar.Text = "Agregar";
            this.bAgregar.UseVisualStyleBackColor = true;
            this.bAgregar.Click += new System.EventHandler(this.bAgregar_Click);
            // 
            // cSucursal
            // 
            this.cSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cSucursal.Enabled = false;
            this.cSucursal.FormattingEnabled = true;
            this.cSucursal.Location = new System.Drawing.Point(241, 77);
            this.cSucursal.Name = "cSucursal";
            this.cSucursal.Size = new System.Drawing.Size(121, 21);
            this.cSucursal.TabIndex = 7;
            // 
            // tMail
            // 
            this.tMail.Enabled = false;
            this.tMail.Location = new System.Drawing.Point(55, 103);
            this.tMail.Name = "tMail";
            this.tMail.Size = new System.Drawing.Size(120, 20);
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
            this.tApellido.Size = new System.Drawing.Size(120, 20);
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
            this.tNombre.Size = new System.Drawing.Size(120, 20);
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
            this.tDNI.Size = new System.Drawing.Size(120, 20);
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
            // BuscarEmpleado
            // 
            this.BuscarEmpleado.Location = new System.Drawing.Point(12, 12);
            this.BuscarEmpleado.Name = "BuscarEmpleado";
            this.BuscarEmpleado.Size = new System.Drawing.Size(126, 23);
            this.BuscarEmpleado.TabIndex = 46;
            this.BuscarEmpleado.Text = "Buscar Empleado";
            this.BuscarEmpleado.UseVisualStyleBackColor = true;
            this.BuscarEmpleado.Click += new System.EventHandler(this.BuscarEmpleado_Click);
            // 
            // tTelefono
            // 
            this.tTelefono.Enabled = false;
            this.tTelefono.Location = new System.Drawing.Point(55, 128);
            this.tTelefono.Name = "tTelefono";
            this.tTelefono.Size = new System.Drawing.Size(120, 20);
            this.tTelefono.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Teléfono:";
            // 
            // cProvincia
            // 
            this.cProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cProvincia.Enabled = false;
            this.cProvincia.FormattingEnabled = true;
            this.cProvincia.Location = new System.Drawing.Point(241, 51);
            this.cProvincia.Name = "cProvincia";
            this.cProvincia.Size = new System.Drawing.Size(121, 21);
            this.cProvincia.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(187, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Provincia:";
            // 
            // tDireccion
            // 
            this.tDireccion.Enabled = false;
            this.tDireccion.Location = new System.Drawing.Point(241, 25);
            this.tDireccion.Name = "tDireccion";
            this.tDireccion.Size = new System.Drawing.Size(120, 20);
            this.tDireccion.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(186, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Dirección:";
            // 
            // cHabilitado
            // 
            this.cHabilitado.AutoSize = true;
            this.cHabilitado.Location = new System.Drawing.Point(241, 130);
            this.cHabilitado.Name = "cHabilitado";
            this.cHabilitado.Size = new System.Drawing.Size(73, 17);
            this.cHabilitado.TabIndex = 34;
            this.cHabilitado.Text = "Habilitado";
            this.cHabilitado.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(210, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Tipo:";
            // 
            // cTipo
            // 
            this.cTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cTipo.Enabled = false;
            this.cTipo.FormattingEnabled = true;
            this.cTipo.Location = new System.Drawing.Point(240, 104);
            this.cTipo.Name = "cTipo";
            this.cTipo.Size = new System.Drawing.Size(121, 21);
            this.cTipo.TabIndex = 35;
            // 
            // FormAbmEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 239);
            this.Controls.Add(this.BuscarEmpleado);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormAbmEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABM Empleado";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bLimpiarABM;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button bEliminar;
        private System.Windows.Forms.Button bModificar;
        private System.Windows.Forms.Button bAgregar;
        private System.Windows.Forms.ComboBox cSucursal;
        private System.Windows.Forms.TextBox tMail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tApellido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tDNI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BuscarEmpleado;
        private System.Windows.Forms.TextBox tTelefono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cProvincia;
        private System.Windows.Forms.TextBox tDireccion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cHabilitado;
        private System.Windows.Forms.ComboBox cTipo;
        private System.Windows.Forms.Label label7;
    }
}