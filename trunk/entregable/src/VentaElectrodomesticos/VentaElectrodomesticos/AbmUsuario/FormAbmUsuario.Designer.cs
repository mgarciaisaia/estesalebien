namespace VentaElectrodomesticos.AbmUsuario
{
    partial class FormAbmUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbmUsuario));
            this.BuscarEmpleado = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgRoles = new System.Windows.Forms.DataGridView();
            this.seleccion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cHabilitado = new System.Windows.Forms.CheckBox();
            this.bLimpiarABM = new System.Windows.Forms.Button();
            this.bEliminar = new System.Windows.Forms.Button();
            this.bModificar = new System.Windows.Forms.Button();
            this.bAgregar = new System.Windows.Forms.Button();
            this.tEmpleado = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tNombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRoles)).BeginInit();
            this.SuspendLayout();
            // 
            // BuscarEmpleado
            // 
            resources.ApplyResources(this.BuscarEmpleado, "BuscarEmpleado");
            this.BuscarEmpleado.Name = "BuscarEmpleado";
            this.BuscarEmpleado.UseVisualStyleBackColor = true;
            this.BuscarEmpleado.Click += new System.EventHandler(this.BuscarEmpleado_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dgRoles);
            this.groupBox1.Controls.Add(this.cHabilitado);
            this.groupBox1.Controls.Add(this.bLimpiarABM);
            this.groupBox1.Controls.Add(this.bEliminar);
            this.groupBox1.Controls.Add(this.bModificar);
            this.groupBox1.Controls.Add(this.bAgregar);
            this.groupBox1.Controls.Add(this.tEmpleado);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tNombre);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // dgRoles
            // 
            this.dgRoles.AllowUserToAddRows = false;
            this.dgRoles.AllowUserToDeleteRows = false;
            this.dgRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRoles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seleccion});
            resources.ApplyResources(this.dgRoles, "dgRoles");
            this.dgRoles.Name = "dgRoles";
            this.dgRoles.RowHeadersVisible = false;
            // 
            // seleccion
            // 
            this.seleccion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.seleccion.FalseValue = "0";
            resources.ApplyResources(this.seleccion, "seleccion");
            this.seleccion.Name = "seleccion";
            this.seleccion.TrueValue = "1";
            // 
            // cHabilitado
            // 
            resources.ApplyResources(this.cHabilitado, "cHabilitado");
            this.cHabilitado.Name = "cHabilitado";
            this.cHabilitado.UseVisualStyleBackColor = true;
            // 
            // bLimpiarABM
            // 
            resources.ApplyResources(this.bLimpiarABM, "bLimpiarABM");
            this.bLimpiarABM.Name = "bLimpiarABM";
            this.bLimpiarABM.UseVisualStyleBackColor = true;
            this.bLimpiarABM.Click += new System.EventHandler(this.bLimpiarABM_Click);
            // 
            // bEliminar
            // 
            resources.ApplyResources(this.bEliminar, "bEliminar");
            this.bEliminar.Name = "bEliminar";
            this.bEliminar.UseVisualStyleBackColor = true;
            this.bEliminar.Click += new System.EventHandler(this.bEliminar_Click);
            // 
            // bModificar
            // 
            resources.ApplyResources(this.bModificar, "bModificar");
            this.bModificar.Name = "bModificar";
            this.bModificar.UseVisualStyleBackColor = true;
            this.bModificar.Click += new System.EventHandler(this.bModificar_Click);
            // 
            // bAgregar
            // 
            resources.ApplyResources(this.bAgregar, "bAgregar");
            this.bAgregar.Name = "bAgregar";
            this.bAgregar.UseVisualStyleBackColor = true;
            this.bAgregar.Click += new System.EventHandler(this.bAgregar_Click);
            // 
            // tEmpleado
            // 
            resources.ApplyResources(this.tEmpleado, "tEmpleado");
            this.tEmpleado.Name = "tEmpleado";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tPassword
            // 
            resources.ApplyResources(this.tPassword, "tPassword");
            this.tPassword.Name = "tPassword";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tNombre
            // 
            resources.ApplyResources(this.tNombre, "tNombre");
            this.tNombre.Name = "tNombre";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormAbmUsuario
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BuscarEmpleado);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormAbmUsuario";
            this.Load += new System.EventHandler(this.FormAbmUsuario_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRoles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BuscarEmpleado;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cHabilitado;
        private System.Windows.Forms.Button bLimpiarABM;
        private System.Windows.Forms.Button bEliminar;
        private System.Windows.Forms.Button bModificar;
        private System.Windows.Forms.Button bAgregar;
        private System.Windows.Forms.TextBox tEmpleado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn seleccion;
        private System.Windows.Forms.DataGridView dgRoles;
        private System.Windows.Forms.Button button2;
    }
}