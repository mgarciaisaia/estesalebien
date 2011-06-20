namespace VentaElectrodomesticos.AbmRol
{
    partial class FormAbmRol
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
            this.lFunciones = new System.Windows.Forms.ListView();
            this.cRoles = new System.Windows.Forms.ComboBox();
            this.cFunciones = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cHabilitado = new System.Windows.Forms.CheckBox();
            this.bLimpiarABM = new System.Windows.Forms.Button();
            this.bEliminar = new System.Windows.Forms.Button();
            this.bAgregar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lFunciones);
            this.groupBox1.Controls.Add(this.cRoles);
            this.groupBox1.Controls.Add(this.cFunciones);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cHabilitado);
            this.groupBox1.Controls.Add(this.bLimpiarABM);
            this.groupBox1.Controls.Add(this.bEliminar);
            this.groupBox1.Controls.Add(this.bAgregar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 218);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ABM Rol";
            // 
            // lFunciones
            // 
            this.lFunciones.HideSelection = false;
            this.lFunciones.Location = new System.Drawing.Point(184, 32);
            this.lFunciones.MultiSelect = false;
            this.lFunciones.Name = "lFunciones";
            this.lFunciones.Size = new System.Drawing.Size(218, 180);
            this.lFunciones.TabIndex = 39;
            this.lFunciones.UseCompatibleStateImageBehavior = false;
            this.lFunciones.View = System.Windows.Forms.View.List;
            // 
            // cRoles
            // 
            this.cRoles.FormattingEnabled = true;
            this.cRoles.Location = new System.Drawing.Point(50, 29);
            this.cRoles.Name = "cRoles";
            this.cRoles.Size = new System.Drawing.Size(128, 21);
            this.cRoles.TabIndex = 38;
            this.cRoles.SelectedIndexChanged += new System.EventHandler(this.cRoles_SelectedIndexChanged);
            // 
            // cFunciones
            // 
            this.cFunciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cFunciones.FormattingEnabled = true;
            this.cFunciones.Location = new System.Drawing.Point(50, 62);
            this.cFunciones.Name = "cFunciones";
            this.cFunciones.Size = new System.Drawing.Size(128, 21);
            this.cFunciones.TabIndex = 37;
            this.cFunciones.SelectedIndexChanged += new System.EventHandler(this.cFunciones_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(181, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Funciones Asignadas:";
            // 
            // cHabilitado
            // 
            this.cHabilitado.AutoSize = true;
            this.cHabilitado.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cHabilitado.Location = new System.Drawing.Point(41, 89);
            this.cHabilitado.Name = "cHabilitado";
            this.cHabilitado.Size = new System.Drawing.Size(73, 17);
            this.cHabilitado.TabIndex = 4;
            this.cHabilitado.Text = "Habilitado";
            this.cHabilitado.UseVisualStyleBackColor = true;
            // 
            // bLimpiarABM
            // 
            this.bLimpiarABM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bLimpiarABM.Location = new System.Drawing.Point(18, 184);
            this.bLimpiarABM.Name = "bLimpiarABM";
            this.bLimpiarABM.Size = new System.Drawing.Size(140, 23);
            this.bLimpiarABM.TabIndex = 10;
            this.bLimpiarABM.Text = "Limpiar";
            this.bLimpiarABM.UseVisualStyleBackColor = true;
            this.bLimpiarABM.Click += new System.EventHandler(this.bLimpiarABM_Click);
            // 
            // bEliminar
            // 
            this.bEliminar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bEliminar.Location = new System.Drawing.Point(18, 147);
            this.bEliminar.Name = "bEliminar";
            this.bEliminar.Size = new System.Drawing.Size(140, 23);
            this.bEliminar.TabIndex = 9;
            this.bEliminar.Text = "Eliminar Funcion";
            this.bEliminar.UseVisualStyleBackColor = true;
            this.bEliminar.Click += new System.EventHandler(this.bEliminar_Click);
            // 
            // bAgregar
            // 
            this.bAgregar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bAgregar.Location = new System.Drawing.Point(18, 112);
            this.bAgregar.Name = "bAgregar";
            this.bAgregar.Size = new System.Drawing.Size(140, 23);
            this.bAgregar.TabIndex = 7;
            this.bAgregar.Text = "Agregar / Modificar";
            this.bAgregar.UseVisualStyleBackColor = true;
            this.bAgregar.Click += new System.EventHandler(this.bAgregar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(3, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Funcion:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(4, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nombre:";
            // 
            // FormAbmRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 224);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormAbmRol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABM Rol";
            this.Load += new System.EventHandler(this.FormAbmRol_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cHabilitado;
        private System.Windows.Forms.Button bLimpiarABM;
        private System.Windows.Forms.Button bAgregar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cFunciones;
        private System.Windows.Forms.ComboBox cRoles;
        private System.Windows.Forms.ListView lFunciones;
        private System.Windows.Forms.Button bEliminar;
    }
}