namespace Google_Distancia_prueba
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnprocesar = new System.Windows.Forms.Button();
            this.dgvprueba = new System.Windows.Forms.DataGridView();
            this.btncalcular = new System.Windows.Forms.Button();
            this.btnRutaCorta = new System.Windows.Forms.Button();
            this.btnanalizar = new System.Windows.Forms.Button();
            this.btnEnviarDireccion = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvprueba)).BeginInit();
            this.SuspendLayout();
            // 
            // btnprocesar
            // 
            this.btnprocesar.Location = new System.Drawing.Point(62, 156);
            this.btnprocesar.Name = "btnprocesar";
            this.btnprocesar.Size = new System.Drawing.Size(121, 23);
            this.btnprocesar.TabIndex = 0;
            this.btnprocesar.Text = "1) Obtener Despacho";
            this.btnprocesar.UseVisualStyleBackColor = true;
            this.btnprocesar.Click += new System.EventHandler(this.btnprocesar_Click);
            // 
            // dgvprueba
            // 
            this.dgvprueba.AllowUserToAddRows = false;
            this.dgvprueba.AllowUserToDeleteRows = false;
            this.dgvprueba.AllowUserToResizeColumns = false;
            this.dgvprueba.AllowUserToResizeRows = false;
            this.dgvprueba.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvprueba.Location = new System.Drawing.Point(12, 200);
            this.dgvprueba.Name = "dgvprueba";
            this.dgvprueba.Size = new System.Drawing.Size(549, 150);
            this.dgvprueba.TabIndex = 1;
            this.dgvprueba.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvprueba_CellContentClick);
            // 
            // btncalcular
            // 
            this.btncalcular.Location = new System.Drawing.Point(195, 156);
            this.btncalcular.Name = "btncalcular";
            this.btncalcular.Size = new System.Drawing.Size(167, 23);
            this.btncalcular.TabIndex = 2;
            this.btncalcular.Text = "2)Validar e Insertar Distancia";
            this.btncalcular.UseVisualStyleBackColor = true;
            // 
            // btnRutaCorta
            // 
            this.btnRutaCorta.Location = new System.Drawing.Point(368, 156);
            this.btnRutaCorta.Name = "btnRutaCorta";
            this.btnRutaCorta.Size = new System.Drawing.Size(149, 23);
            this.btnRutaCorta.TabIndex = 3;
            this.btnRutaCorta.Text = "3)Calcular Ruta mas Corta";
            this.btnRutaCorta.UseVisualStyleBackColor = true;
            this.btnRutaCorta.Click += new System.EventHandler(this.btnRutaCorta_Click);
            // 
            // btnanalizar
            // 
            this.btnanalizar.Location = new System.Drawing.Point(195, 35);
            this.btnanalizar.Name = "btnanalizar";
            this.btnanalizar.Size = new System.Drawing.Size(166, 23);
            this.btnanalizar.TabIndex = 4;
            this.btnanalizar.Text = "Analiza Tabla de Despachos";
            this.btnanalizar.UseVisualStyleBackColor = true;
            // 
            // btnEnviarDireccion
            // 
            this.btnEnviarDireccion.Location = new System.Drawing.Point(186, 77);
            this.btnEnviarDireccion.Name = "btnEnviarDireccion";
            this.btnEnviarDireccion.Size = new System.Drawing.Size(193, 23);
            this.btnEnviarDireccion.TabIndex = 5;
            this.btnEnviarDireccion.Text = "Envia Direccion a Google Maps";
            this.btnEnviarDireccion.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Carga Inicial : Cosecha de coordenadas";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Carga Inicial : Envio de Direcciones a Google Maps";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(235, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "CARGA INICIAL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(161, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(218, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "OBTENER RUTA CORTA DEL DESPACHO";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 390);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEnviarDireccion);
            this.Controls.Add(this.btnanalizar);
            this.Controls.Add(this.btnRutaCorta);
            this.Controls.Add(this.btncalcular);
            this.Controls.Add(this.dgvprueba);
            this.Controls.Add(this.btnprocesar);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvprueba)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnprocesar;
        private System.Windows.Forms.DataGridView dgvprueba;
        private System.Windows.Forms.Button btncalcular;
        private System.Windows.Forms.Button btnRutaCorta;
        private System.Windows.Forms.Button btnanalizar;
        private System.Windows.Forms.Button btnEnviarDireccion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

