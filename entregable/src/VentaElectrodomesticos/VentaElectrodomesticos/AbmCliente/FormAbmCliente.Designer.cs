namespace VentaElectrodomesticos.AbmCliente
{
    partial class FormAbmCliente
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
            this.BuscarCliente = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cHabilitado = new System.Windows.Forms.CheckBox();
            this.tDireccion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tTelefono = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bLimpiarABM = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.bEliminar = new System.Windows.Forms.Button();
            this.bModificar = new System.Windows.Forms.Button();
            this.bAgregar = new System.Windows.Forms.Button();
            this.cProvincia = new System.Windows.Forms.ComboBox();
            this.tMail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tApellido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tDNI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BuscarCliente
            // 
            this.BuscarCliente.Location = new System.Drawing.Point(6, 7);
            this.BuscarCliente.Name = "BuscarCliente";
            this.BuscarCliente.Size = new System.Drawing.Size(126, 23);
            this.BuscarCliente.TabIndex = 48;
            this.BuscarCliente.Text = "Buscar Cliente";
            this.BuscarCliente.UseVisualStyleBackColor = true;
            this.BuscarCliente.Click += new System.EventHandler(this.BuscarCliente_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cHabilitado);
            this.groupBox1.Controls.Add(this.tDireccion);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tTelefono);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.bLimpiarABM);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.bEliminar);
            this.groupBox1.Controls.Add(this.bModificar);
            this.groupBox1.Controls.Add(this.bAgregar);
            this.groupBox1.Controls.Add(this.cProvincia);
            this.groupBox1.Controls.Add(this.tMail);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tApellido);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tNombre);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tDNI);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 166);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ABM";
            // 
            // cHabilitado
            // 
            this.cHabilitado.AutoSize = true;
            this.cHabilitado.Location = new System.Drawing.Point(241, 106);
            this.cHabilitado.Name = "cHabilitado";
            this.cHabilitado.Size = new System.Drawing.Size(73, 17);
            this.cHabilitado.TabIndex = 34;
            this.cHabilitado.Text = "Habilitado";
            this.cHabilitado.UseVisualStyleBackColor = true;
            // 
            // tDireccion
            // 
            this.tDireccion.Location = new System.Drawing.Point(241, 51);
            this.tDireccion.Name = "tDireccion";
            this.tDireccion.Size = new System.Drawing.Size(120, 20);
            this.tDireccion.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(186, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Dirección:";
            // 
            // tTelefono
            // 
            this.tTelefono.Location = new System.Drawing.Point(241, 26);
            this.tTelefono.Name = "tTelefono";
            this.tTelefono.Size = new System.Drawing.Size(120, 20);
            this.tTelefono.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(189, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Teléfono:";
            // 
            // bLimpiarABM
            // 
            this.bLimpiarABM.Location = new System.Drawing.Point(276, 130);
            this.bLimpiarABM.Name = "bLimpiarABM";
            this.bLimpiarABM.Size = new System.Drawing.Size(75, 23);
            this.bLimpiarABM.TabIndex = 11;
            this.bLimpiarABM.Text = "Limpiar";
            this.bLimpiarABM.UseVisualStyleBackColor = true;
            this.bLimpiarABM.Click += new System.EventHandler(this.bLimpiarCliente_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(187, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Provincia:";
            // 
            // bEliminar
            // 
            this.bEliminar.Location = new System.Drawing.Point(195, 130);
            this.bEliminar.Name = "bEliminar";
            this.bEliminar.Size = new System.Drawing.Size(75, 23);
            this.bEliminar.TabIndex = 10;
            this.bEliminar.Text = "Dar baja";
            this.bEliminar.UseVisualStyleBackColor = true;
            this.bEliminar.Click += new System.EventHandler(this.bEliminar_Click);
            // 
            // bModificar
            // 
            this.bModificar.Location = new System.Drawing.Point(114, 130);
            this.bModificar.Name = "bModificar";
            this.bModificar.Size = new System.Drawing.Size(75, 23);
            this.bModificar.TabIndex = 9;
            this.bModificar.Text = "Modificar";
            this.bModificar.UseVisualStyleBackColor = true;
            this.bModificar.Click += new System.EventHandler(this.bModificar_Click);
            // 
            // bAgregar
            // 
            this.bAgregar.Location = new System.Drawing.Point(33, 130);
            this.bAgregar.Name = "bAgregar";
            this.bAgregar.Size = new System.Drawing.Size(75, 23);
            this.bAgregar.TabIndex = 8;
            this.bAgregar.Text = "Agregar";
            this.bAgregar.UseVisualStyleBackColor = true;
            this.bAgregar.Click += new System.EventHandler(this.bAgregar_Click);
            // 
            // cProvincia
            // 
            this.cProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cProvincia.FormattingEnabled = true;
            this.cProvincia.Location = new System.Drawing.Point(241, 77);
            this.cProvincia.Name = "cProvincia";
            this.cProvincia.Size = new System.Drawing.Size(121, 21);
            this.cProvincia.TabIndex = 6;
            // 
            // tMail
            // 
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
            this.tDNI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tDNI_KeyPress);
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
            // FormAbmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 215);
            this.Controls.Add(this.BuscarCliente);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormAbmCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABM de Cliente";
            this.Load += new System.EventHandler(this.FormAbmCliente_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BuscarCliente;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cHabilitado;
        private System.Windows.Forms.TextBox tDireccion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tTelefono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bLimpiarABM;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button bEliminar;
        private System.Windows.Forms.Button bModificar;
        private System.Windows.Forms.Button bAgregar;
        private System.Windows.Forms.ComboBox cProvincia;
        private System.Windows.Forms.TextBox tMail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tApellido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tDNI;
        private System.Windows.Forms.Label label1;


    }
}