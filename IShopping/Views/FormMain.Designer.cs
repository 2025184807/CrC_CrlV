namespace IShopping.Views
{
    partial class FormMain
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnArtigo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExportar = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btnPlaneamento = new System.Windows.Forms.Button();
            this.btnTiposArtigo = new System.Windows.Forms.Button();
            this.btnOrcamentos = new System.Windows.Forms.Button();
            this.btnEstatistica = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGerirUtilizadores = new System.Windows.Forms.Button();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.btnCompra = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(37, 234);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(983, 185);
            this.dataGridView1.TabIndex = 142;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnArtigo
            // 
            this.btnArtigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnArtigo.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnArtigo.ForeColor = System.Drawing.Color.White;
            this.btnArtigo.Location = new System.Drawing.Point(288, 26);
            this.btnArtigo.Name = "btnArtigo";
            this.btnArtigo.Size = new System.Drawing.Size(118, 57);
            this.btnArtigo.TabIndex = 141;
            this.btnArtigo.Text = "Artigos";
            this.btnArtigo.UseVisualStyleBackColor = false;
            this.btnArtigo.Click += new System.EventHandler(this.btnArtigo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Yi Baiti", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 28);
            this.label3.TabIndex = 139;
            this.label3.Text = "Compras em Aberto:";
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnExportar.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.ForeColor = System.Drawing.Color.White;
            this.btnExportar.Location = new System.Drawing.Point(919, 26);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(118, 58);
            this.btnExportar.TabIndex = 135;
            this.btnExportar.Text = "Exportar CSV";
            this.btnExportar.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button7.Font = new System.Drawing.Font("Microsoft Yi Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button7.Location = new System.Drawing.Point(844, 425);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(176, 46);
            this.button7.TabIndex = 137;
            this.button7.Text = "Abrir Compra";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // btnPlaneamento
            // 
            this.btnPlaneamento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnPlaneamento.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlaneamento.ForeColor = System.Drawing.Color.White;
            this.btnPlaneamento.Location = new System.Drawing.Point(660, 27);
            this.btnPlaneamento.Name = "btnPlaneamento";
            this.btnPlaneamento.Size = new System.Drawing.Size(129, 57);
            this.btnPlaneamento.TabIndex = 134;
            this.btnPlaneamento.Text = "Planeamento de Compras";
            this.btnPlaneamento.UseVisualStyleBackColor = false;
            this.btnPlaneamento.Click += new System.EventHandler(this.btnPlaneamento_Click_1);
            // 
            // btnTiposArtigo
            // 
            this.btnTiposArtigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnTiposArtigo.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTiposArtigo.ForeColor = System.Drawing.Color.White;
            this.btnTiposArtigo.Location = new System.Drawing.Point(145, 26);
            this.btnTiposArtigo.Name = "btnTiposArtigo";
            this.btnTiposArtigo.Size = new System.Drawing.Size(137, 56);
            this.btnTiposArtigo.TabIndex = 133;
            this.btnTiposArtigo.Text = "Tipos de Artigo";
            this.btnTiposArtigo.UseVisualStyleBackColor = false;
            this.btnTiposArtigo.Click += new System.EventHandler(this.btnTipoArtigo_Click);
            // 
            // btnOrcamentos
            // 
            this.btnOrcamentos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnOrcamentos.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrcamentos.ForeColor = System.Drawing.Color.White;
            this.btnOrcamentos.Location = new System.Drawing.Point(412, 26);
            this.btnOrcamentos.Name = "btnOrcamentos";
            this.btnOrcamentos.Size = new System.Drawing.Size(118, 57);
            this.btnOrcamentos.TabIndex = 132;
            this.btnOrcamentos.Text = "Orçamentos";
            this.btnOrcamentos.UseVisualStyleBackColor = false;
            this.btnOrcamentos.Click += new System.EventHandler(this.btnOrcamentos_Click);
            // 
            // btnEstatistica
            // 
            this.btnEstatistica.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnEstatistica.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstatistica.ForeColor = System.Drawing.Color.White;
            this.btnEstatistica.Location = new System.Drawing.Point(795, 27);
            this.btnEstatistica.Name = "btnEstatistica";
            this.btnEstatistica.Size = new System.Drawing.Size(118, 57);
            this.btnEstatistica.TabIndex = 131;
            this.btnEstatistica.Text = "Estatistica";
            this.btnEstatistica.UseVisualStyleBackColor = false;
            this.btnEstatistica.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Yi Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(919, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 43);
            this.button1.TabIndex = 130;
            this.button1.Text = "Logout";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Rockwell", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 41);
            this.label1.TabIndex = 129;
            this.label1.Text = "IShopping";
            // 
            // btnGerirUtilizadores
            // 
            this.btnGerirUtilizadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnGerirUtilizadores.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerirUtilizadores.ForeColor = System.Drawing.Color.White;
            this.btnGerirUtilizadores.Location = new System.Drawing.Point(21, 25);
            this.btnGerirUtilizadores.Name = "btnGerirUtilizadores";
            this.btnGerirUtilizadores.Size = new System.Drawing.Size(118, 57);
            this.btnGerirUtilizadores.TabIndex = 144;
            this.btnGerirUtilizadores.Text = "Gerir Utilizadores";
            this.btnGerirUtilizadores.UseVisualStyleBackColor = false;
            this.btnGerirUtilizadores.Click += new System.EventHandler(this.btnGerirUtilizadores_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.pictureBox7.Image = global::IShopping.Properties.Resources.logout;
            this.pictureBox7.Location = new System.Drawing.Point(1002, 139);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(19, 23);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 140;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pictureBox4.Image = global::IShopping.Properties.Resources.cart1;
            this.pictureBox4.Location = new System.Drawing.Point(216, 134);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(26, 28);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 136;
            this.pictureBox4.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(0, -6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(1075, 114);
            this.textBox1.TabIndex = 128;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(907, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 23);
            this.label9.TabIndex = 261;
            this.label9.Text = "ID:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(946, 206);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(74, 22);
            this.txtId.TabIndex = 260;
            // 
            // btnCompra
            // 
            this.btnCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnCompra.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompra.ForeColor = System.Drawing.Color.White;
            this.btnCompra.Location = new System.Drawing.Point(536, 27);
            this.btnCompra.Name = "btnCompra";
            this.btnCompra.Size = new System.Drawing.Size(118, 57);
            this.btnCompra.TabIndex = 262;
            this.btnCompra.Text = "Criar da Compra";
            this.btnCompra.UseVisualStyleBackColor = false;
            this.btnCompra.Click += new System.EventHandler(this.btnCompra_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 508);
            this.Controls.Add(this.btnCompra);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.btnGerirUtilizadores);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnArtigo);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.btnPlaneamento);
            this.Controls.Add(this.btnTiposArtigo);
            this.Controls.Add(this.btnOrcamentos);
            this.Controls.Add(this.btnEstatistica);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnArtigo;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnPlaneamento;
        private System.Windows.Forms.Button btnTiposArtigo;
        private System.Windows.Forms.Button btnOrcamentos;
        private System.Windows.Forms.Button btnEstatistica;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGerirUtilizadores;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Button btnCompra;
    }
}