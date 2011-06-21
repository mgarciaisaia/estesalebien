namespace VentaElectrodomesticos.MejoresCategorias
{
    partial class FormMejoresCategorias
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
            this.dgClientes = new System.Windows.Forms.DataGridView();
            this.Filtro = new System.Windows.Forms.GroupBox();
            this.bAnalizar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cSucursal = new System.Windows.Forms.ComboBox();
            this.cAnio = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgClientes)).BeginInit();
            this.Filtro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cAnio)).BeginInit();
            this.SuspendLayout();
            // 
            // dgClientes
            // 
            this.dgClientes.AllowUserToAddRows = false;
            this.dgClientes.AllowUserToDeleteRows = false;
            this.dgClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgClientes.Location = new System.Drawing.Point(4, 55);
            this.dgClientes.Name = "dgClientes";
            this.dgClientes.ReadOnly = true;
            this.dgClientes.RowHeadersVisible = false;
            this.dgClientes.Size = new System.Drawing.Size(695, 199);
            this.dgClientes.TabIndex = 5;
            // 
            // Filtro
            // 
            this.Filtro.Controls.Add(this.bAnalizar);
            this.Filtro.Controls.Add(this.label2);
            this.Filtro.Controls.Add(this.cSucursal);
            this.Filtro.Controls.Add(this.cAnio);
            this.Filtro.Controls.Add(this.label1);
            this.Filtro.Location = new System.Drawing.Point(1, 3);
            this.Filtro.Name = "Filtro";
            this.Filtro.Size = new System.Drawing.Size(698, 46);
            this.Filtro.TabIndex = 4;
            this.Filtro.TabStop = false;
            this.Filtro.Text = "Filtro";
            // 
            // bAnalizar
            // 
            this.bAnalizar.Location = new System.Drawing.Point(383, 18);
            this.bAnalizar.Name = "bAnalizar";
            this.bAnalizar.Size = new System.Drawing.Size(77, 20);
            this.bAnalizar.TabIndex = 4;
            this.bAnalizar.Text = "Analizar";
            this.bAnalizar.UseVisualStyleBackColor = true;
            this.bAnalizar.Click += new System.EventHandler(this.bAnalizar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Año";
            // 
            // cSucursal
            // 
            this.cSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cSucursal.FormattingEnabled = true;
            this.cSucursal.Location = new System.Drawing.Point(60, 18);
            this.cSucursal.Name = "cSucursal";
            this.cSucursal.Size = new System.Drawing.Size(221, 21);
            this.cSucursal.TabIndex = 2;
            // 
            // cAnio
            // 
            this.cAnio.Location = new System.Drawing.Point(319, 18);
            this.cAnio.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.cAnio.Minimum = new decimal(new int[] {
            1995,
            0,
            0,
            0});
            this.cAnio.Name = "cAnio";
            this.cAnio.Size = new System.Drawing.Size(58, 20);
            this.cAnio.TabIndex = 1;
            this.cAnio.Value = new decimal(new int[] {
            1995,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sucursal";
            // 
            // FormMejoresCategorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 260);
            this.Controls.Add(this.dgClientes);
            this.Controls.Add(this.Filtro);
            this.Name = "FormMejoresCategorias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mejores Categorías";
            this.Load += new System.EventHandler(this.FormMejoresCategorias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgClientes)).EndInit();
            this.Filtro.ResumeLayout(false);
            this.Filtro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cAnio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgClientes;
        private System.Windows.Forms.GroupBox Filtro;
        private System.Windows.Forms.Button bAnalizar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cSucursal;
        private System.Windows.Forms.NumericUpDown cAnio;
        private System.Windows.Forms.Label label1;
    }
}