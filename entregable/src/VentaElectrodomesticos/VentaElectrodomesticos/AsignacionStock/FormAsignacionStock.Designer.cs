namespace VentaElectrodomesticos.AsignacionStock
{
    partial class FormAsignacionStock
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
            this.cSucursal = new System.Windows.Forms.ComboBox();
            this.bAuditor = new System.Windows.Forms.Button();
            this.tAuditor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bProducto = new System.Windows.Forms.Button();
            this.tProducto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cSucursal);
            this.groupBox1.Controls.Add(this.bAuditor);
            this.groupBox1.Controls.Add(this.tAuditor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.bProducto);
            this.groupBox1.Controls.Add(this.tProducto);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cSucursal
            // 
            this.cSucursal.FormattingEnabled = true;
            this.cSucursal.Location = new System.Drawing.Point(64, 67);
            this.cSucursal.Name = "cSucursal";
            this.cSucursal.Size = new System.Drawing.Size(135, 21);
            this.cSucursal.TabIndex = 9;
            // 
            // bAuditor
            // 
            this.bAuditor.Location = new System.Drawing.Point(205, 42);
            this.bAuditor.Name = "bAuditor";
            this.bAuditor.Size = new System.Drawing.Size(72, 20);
            this.bAuditor.TabIndex = 8;
            this.bAuditor.Text = "Seleccionar";
            this.bAuditor.UseVisualStyleBackColor = true;
            this.bAuditor.Click += new System.EventHandler(this.bAuditor_Click);
            // 
            // tAuditor
            // 
            this.tAuditor.Enabled = false;
            this.tAuditor.Location = new System.Drawing.Point(64, 42);
            this.tAuditor.Name = "tAuditor";
            this.tAuditor.Size = new System.Drawing.Size(135, 20);
            this.tAuditor.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Auditor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sucursal";
            // 
            // bProducto
            // 
            this.bProducto.Location = new System.Drawing.Point(205, 16);
            this.bProducto.Name = "bProducto";
            this.bProducto.Size = new System.Drawing.Size(72, 20);
            this.bProducto.TabIndex = 2;
            this.bProducto.Text = "Seleccionar";
            this.bProducto.UseVisualStyleBackColor = true;
            this.bProducto.Click += new System.EventHandler(this.bProducto_Click);
            // 
            // tProducto
            // 
            this.tProducto.Enabled = false;
            this.tProducto.Location = new System.Drawing.Point(64, 16);
            this.tProducto.Name = "tProducto";
            this.tProducto.Size = new System.Drawing.Size(135, 20);
            this.tProducto.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Producto";
            // 
            // FormAsignacionStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 417);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormAsignacionStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignación de Stock";
            this.Load += new System.EventHandler(this.FormAsignacionStock_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cSucursal;
        private System.Windows.Forms.Button bAuditor;
        private System.Windows.Forms.TextBox tAuditor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bProducto;
    }
}