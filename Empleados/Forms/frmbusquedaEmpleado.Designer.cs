namespace Empleados.Forms
{
    partial class frmbusquedaEmpleado
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboxsistema1 = new System.Windows.Forms.ComboBox();
            this.txtExpresion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.dgBusqueda = new System.Windows.Forms.DataGridView();
            this.Clave_Emp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApPaterno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApMaterno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FecNac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Departamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sueldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBusqueda)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboxsistema1);
            this.panel1.Controls.Add(this.txtExpresion);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(849, 58);
            this.panel1.TabIndex = 23;
            // 
            // cboxsistema1
            // 
            this.cboxsistema1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxsistema1.FormattingEnabled = true;
            this.cboxsistema1.Location = new System.Drawing.Point(163, 13);
            this.cboxsistema1.Name = "cboxsistema1";
            this.cboxsistema1.Size = new System.Drawing.Size(211, 21);
            this.cboxsistema1.TabIndex = 30;
            this.cboxsistema1.SelectedIndexChanged += new System.EventHandler(this.cboxsistema1_SelectedIndexChanged);
            // 
            // txtExpresion
            // 
            this.txtExpresion.Location = new System.Drawing.Point(12, 13);
            this.txtExpresion.Name = "txtExpresion";
            this.txtExpresion.Size = new System.Drawing.Size(145, 20);
            this.txtExpresion.TabIndex = 1;
            this.txtExpresion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExpresion_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Expresión a Buscar";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(732, 374);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 22;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(651, 374);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 21;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // dgBusqueda
            // 
            this.dgBusqueda.AllowUserToAddRows = false;
            this.dgBusqueda.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgBusqueda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBusqueda.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Clave_Emp,
            this.Nombre,
            this.ApPaterno,
            this.ApMaterno,
            this.FecNac,
            this.Departamento,
            this.sueldo});
            this.dgBusqueda.Location = new System.Drawing.Point(0, 130);
            this.dgBusqueda.Name = "dgBusqueda";
            this.dgBusqueda.Size = new System.Drawing.Size(845, 208);
            this.dgBusqueda.TabIndex = 20;
            // 
            // Clave_Emp
            // 
            this.Clave_Emp.DataPropertyName = "Clave_Emp";
            this.Clave_Emp.HeaderText = "Clave";
            this.Clave_Emp.Name = "Clave_Emp";
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "Nombre";
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.Width = 200;
            // 
            // ApPaterno
            // 
            this.ApPaterno.DataPropertyName = "ApPaterno";
            this.ApPaterno.HeaderText = "Apellido Paterno";
            this.ApPaterno.Name = "ApPaterno";
            // 
            // ApMaterno
            // 
            this.ApMaterno.DataPropertyName = "ApMaterno";
            this.ApMaterno.HeaderText = "Apellido Materno";
            this.ApMaterno.Name = "ApMaterno";
            // 
            // FecNac
            // 
            this.FecNac.DataPropertyName = "FecNac";
            this.FecNac.HeaderText = "Fecha de Nacimiento";
            this.FecNac.Name = "FecNac";
            // 
            // Departamento
            // 
            this.Departamento.DataPropertyName = "Descripcion";
            this.Departamento.HeaderText = "Departamento";
            this.Departamento.Name = "Departamento";
            // 
            // sueldo
            // 
            this.sueldo.DataPropertyName = "Sueldo";
            this.sueldo.HeaderText = "sueldo";
            this.sueldo.Name = "sueldo";
            // 
            // frmbusquedaEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 422);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.dgBusqueda);
            this.Name = "frmbusquedaEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmbusquedaEmpleado";
            this.Load += new System.EventHandler(this.frmbusquedaEmpleado_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBusqueda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtExpresion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        public System.Windows.Forms.DataGridView dgBusqueda;
        private System.Windows.Forms.ComboBox cboxsistema1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave_Emp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApPaterno;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApMaterno;
        private System.Windows.Forms.DataGridViewTextBoxColumn FecNac;
        private System.Windows.Forms.DataGridViewTextBoxColumn Departamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn sueldo;
    }
}