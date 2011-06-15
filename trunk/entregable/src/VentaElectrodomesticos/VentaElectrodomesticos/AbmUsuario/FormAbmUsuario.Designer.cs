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
            this.cHabilitado = new System.Windows.Forms.CheckBox();
            this.bLimpiarABM = new System.Windows.Forms.Button();
            this.bEliminar = new System.Windows.Forms.Button();
            this.bModificar = new System.Windows.Forms.Button();
            this.bAgregar = new System.Windows.Forms.Button();
            this.tApellido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tDNI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dgRoles = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRoles)).BeginInit();
            this.SuspendLayout();
            // 
            // BuscarEmpleado
            // 
            resources.ApplyResources(this.BuscarEmpleado, "BuscarEmpleado");
            this.BuscarEmpleado.Name = "BuscarEmpleado";
            this.BuscarEmpleado.UseVisualStyleBackColor = true;
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
            this.groupBox1.Controls.Add(this.tApellido);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tNombre);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tDNI);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
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
            // 
            // bEliminar
            // 
            resources.ApplyResources(this.bEliminar, "bEliminar");
            this.bEliminar.Name = "bEliminar";
            this.bEliminar.UseVisualStyleBackColor = true;
            // 
            // bModificar
            // 
            resources.ApplyResources(this.bModificar, "bModificar");
            this.bModificar.Name = "bModificar";
            this.bModificar.UseVisualStyleBackColor = true;
            // 
            // bAgregar
            // 
            resources.ApplyResources(this.bAgregar, "bAgregar");
            this.bAgregar.Name = "bAgregar";
            this.bAgregar.UseVisualStyleBackColor = true;
            // 
            // tApellido
            // 
            resources.ApplyResources(this.tApellido, "tApellido");
            this.tApellido.Name = "tApellido";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tNombre
            // 
            resources.ApplyResources(this.tNombre, "tNombre");
            this.tNombre.Name = "tNombre";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tDNI
            // 
            resources.ApplyResources(this.tDNI, "tDNI");
            this.tDNI.Name = "tDNI";
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
            // dgRoles
            // 
            this.dgRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgRoles, "dgRoles");
            this.dgRoles.Name = "dgRoles";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // FormAbmUsuario
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
        private System.Windows.Forms.TextBox tApellido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tDNI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgRoles;
    }
}