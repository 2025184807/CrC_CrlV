namespace IShopping.Views
{
    partial class FormModoCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormModoCompra));
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbArtigo = new System.Windows.Forms.ComboBox();
            this.cmbTipoArtigo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numQuantidade = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPrecoPrevisto = new System.Windows.Forms.TextBox();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPreco = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateCompra = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAviso = new System.Windows.Forms.Label();
            this.txtSaldoDisponivel = new System.Windows.Forms.TextBox();
            this.txtOrcamento = new System.Windows.Forms.TextBox();
            this.txtTotalGasto = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnRemoverItem = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.cmbNomeCompra = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numQtdAdquirida = new System.Windows.Forms.NumericUpDown();
            this.btnRegistar = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cmbItemPrevisto = new System.Windows.Forms.ComboBox();
            this.txtQtdPrevista = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnFecharCompra = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantidade)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQtdAdquirida)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnAdicionar.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionar.ForeColor = System.Drawing.Color.White;
            this.btnAdicionar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdicionar.Location = new System.Drawing.Point(53, 276);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(219, 44);
            this.btnAdicionar.TabIndex = 322;
            this.btnAdicionar.Text = "Adicionar Item Não Previsto";
            this.btnAdicionar.UseVisualStyleBackColor = false;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmbArtigo);
            this.panel2.Controls.Add(this.cmbTipoArtigo);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.numQuantidade);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.btnAdicionar);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.txtPrecoPrevisto);
            this.panel2.Controls.Add(this.txtDescricao);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(1000, 512);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(325, 352);
            this.panel2.TabIndex = 321;
            // 
            // cmbArtigo
            // 
            this.cmbArtigo.FormattingEnabled = true;
            this.cmbArtigo.Location = new System.Drawing.Point(167, 51);
            this.cmbArtigo.Name = "cmbArtigo";
            this.cmbArtigo.Size = new System.Drawing.Size(133, 24);
            this.cmbArtigo.TabIndex = 323;
            // 
            // cmbTipoArtigo
            // 
            this.cmbTipoArtigo.FormattingEnabled = true;
            this.cmbTipoArtigo.Location = new System.Drawing.Point(167, 17);
            this.cmbTipoArtigo.Name = "cmbTipoArtigo";
            this.cmbTipoArtigo.Size = new System.Drawing.Size(133, 24);
            this.cmbTipoArtigo.TabIndex = 235;
            this.cmbTipoArtigo.SelectedIndexChanged += new System.EventHandler(this.cmbTipoArtigo_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 23);
            this.label7.TabIndex = 233;
            this.label7.Text = "Tipo de Artigo:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 23);
            this.label6.TabIndex = 234;
            this.label6.Text = "Artigo:";
            // 
            // numQuantidade
            // 
            this.numQuantidade.Location = new System.Drawing.Point(168, 90);
            this.numQuantidade.Name = "numQuantidade";
            this.numQuantidade.Size = new System.Drawing.Size(133, 22);
            this.numQuantidade.TabIndex = 278;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(20, 90);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 23);
            this.label11.TabIndex = 264;
            this.label11.Text = "Quantidade:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(20, 126);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(134, 23);
            this.label13.TabIndex = 266;
            this.label13.Text = "Preço Unitário:";
            // 
            // txtPrecoPrevisto
            // 
            this.txtPrecoPrevisto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrecoPrevisto.Location = new System.Drawing.Point(167, 127);
            this.txtPrecoPrevisto.Name = "txtPrecoPrevisto";
            this.txtPrecoPrevisto.Size = new System.Drawing.Size(133, 22);
            this.txtPrecoPrevisto.TabIndex = 267;
            // 
            // txtDescricao
            // 
            this.txtDescricao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescricao.Location = new System.Drawing.Point(20, 198);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(280, 58);
            this.txtDescricao.TabIndex = 263;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 23);
            this.label5.TabIndex = 262;
            this.label5.Text = "Observações:";
            // 
            // txtPreco
            // 
            this.txtPreco.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPreco.Location = new System.Drawing.Point(179, 163);
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.Size = new System.Drawing.Size(121, 22);
            this.txtPreco.TabIndex = 267;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(27, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(163, 23);
            this.label14.TabIndex = 316;
            this.label14.Text = "Nome da Compra:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(995, 159);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(318, 26);
            this.label12.TabIndex = 312;
            this.label12.Text = "Registar Compra Itens Previstos:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 23);
            this.label3.TabIndex = 337;
            this.label3.Text = "Data Compra:";
            // 
            // dateCompra
            // 
            this.dateCompra.Location = new System.Drawing.Point(202, 70);
            this.dateCompra.Name = "dateCompra";
            this.dateCompra.Size = new System.Drawing.Size(293, 22);
            this.dateCompra.TabIndex = 338;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblAviso);
            this.panel1.Controls.Add(this.txtSaldoDisponivel);
            this.panel1.Controls.Add(this.txtOrcamento);
            this.panel1.Controls.Add(this.txtTotalGasto);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Location = new System.Drawing.Point(630, 195);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 186);
            this.panel1.TabIndex = 340;
            // 
            // lblAviso
            // 
            this.lblAviso.AutoSize = true;
            this.lblAviso.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAviso.Location = new System.Drawing.Point(14, 153);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Size = new System.Drawing.Size(0, 20);
            this.lblAviso.TabIndex = 347;
            // 
            // txtSaldoDisponivel
            // 
            this.txtSaldoDisponivel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtSaldoDisponivel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSaldoDisponivel.Location = new System.Drawing.Point(176, 124);
            this.txtSaldoDisponivel.Name = "txtSaldoDisponivel";
            this.txtSaldoDisponivel.ReadOnly = true;
            this.txtSaldoDisponivel.Size = new System.Drawing.Size(132, 22);
            this.txtSaldoDisponivel.TabIndex = 346;
            // 
            // txtOrcamento
            // 
            this.txtOrcamento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrcamento.Location = new System.Drawing.Point(176, 20);
            this.txtOrcamento.Name = "txtOrcamento";
            this.txtOrcamento.ReadOnly = true;
            this.txtOrcamento.Size = new System.Drawing.Size(132, 22);
            this.txtOrcamento.TabIndex = 315;
            // 
            // txtTotalGasto
            // 
            this.txtTotalGasto.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtTotalGasto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalGasto.Location = new System.Drawing.Point(176, 72);
            this.txtTotalGasto.Name = "txtTotalGasto";
            this.txtTotalGasto.ReadOnly = true;
            this.txtTotalGasto.Size = new System.Drawing.Size(132, 22);
            this.txtTotalGasto.TabIndex = 345;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(12, 17);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(102, 23);
            this.label21.TabIndex = 314;
            this.label21.Text = "Orcamento:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 23);
            this.label4.TabIndex = 344;
            this.label4.Text = "Saldo Disponivel:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 23);
            this.label9.TabIndex = 343;
            this.label9.Text = "Total Gasto:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(-240, 157);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 23);
            this.label10.TabIndex = 341;
            this.label10.Text = "Orçamento:";
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Location = new System.Drawing.Point(-236, 188);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(181, 22);
            this.textBox4.TabIndex = 342;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(625, 159);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(291, 27);
            this.label15.TabIndex = 318;
            this.label15.Text = "Resumo Financeiro do Mês:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(995, 483);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(200, 26);
            this.label8.TabIndex = 341;
            this.label8.Text = "Itens Não Previstos:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(27, 113);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 23);
            this.label16.TabIndex = 342;
            this.label16.Text = "Estado:";
            // 
            // btnRemoverItem
            // 
            this.btnRemoverItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnRemoverItem.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoverItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoverItem.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRemoverItem.Location = new System.Drawing.Point(37, 806);
            this.btnRemoverItem.Name = "btnRemoverItem";
            this.btnRemoverItem.Size = new System.Drawing.Size(142, 44);
            this.btnRemoverItem.TabIndex = 323;
            this.btnRemoverItem.Text = "Remover Item";
            this.btnRemoverItem.UseVisualStyleBackColor = false;
            this.btnRemoverItem.Click += new System.EventHandler(this.btnRemoverItem_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtEstado);
            this.panel3.Controls.Add(this.cmbNomeCompra);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.textBox9);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.dateCompra);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(37, 199);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(541, 182);
            this.panel3.TabIndex = 348;
            // 
            // txtEstado
            // 
            this.txtEstado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEstado.Location = new System.Drawing.Point(202, 119);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.Size = new System.Drawing.Size(293, 22);
            this.txtEstado.TabIndex = 348;
            this.txtEstado.TextChanged += new System.EventHandler(this.txtEstado_TextChanged);
            // 
            // cmbNomeCompra
            // 
            this.cmbNomeCompra.FormattingEnabled = true;
            this.cmbNomeCompra.Location = new System.Drawing.Point(202, 27);
            this.cmbNomeCompra.Name = "cmbNomeCompra";
            this.cmbNomeCompra.Size = new System.Drawing.Size(293, 24);
            this.cmbNomeCompra.TabIndex = 347;
            this.cmbNomeCompra.SelectedIndexChanged += new System.EventHandler(this.cmbNomeCompra_SelectedIndexChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(-240, 157);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(102, 23);
            this.label23.TabIndex = 341;
            this.label23.Text = "Orçamento:";
            // 
            // textBox9
            // 
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox9.Location = new System.Drawing.Point(-236, 188);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(181, 22);
            this.textBox9.TabIndex = 342;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(37, 454);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(917, 346);
            this.dataGridView1.TabIndex = 344;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(77, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(252, 35);
            this.label2.TabIndex = 336;
            this.label2.Text = "Modo de Compra:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 27);
            this.label1.TabIndex = 349;
            this.label1.Text = "Dados da Compra:";
            // 
            // numQtdAdquirida
            // 
            this.numQtdAdquirida.Location = new System.Drawing.Point(179, 117);
            this.numQtdAdquirida.Name = "numQtdAdquirida";
            this.numQtdAdquirida.Size = new System.Drawing.Size(122, 22);
            this.numQtdAdquirida.TabIndex = 278;
            // 
            // btnRegistar
            // 
            this.btnRegistar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnRegistar.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistar.ForeColor = System.Drawing.Color.White;
            this.btnRegistar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRegistar.Location = new System.Drawing.Point(81, 202);
            this.btnRegistar.Name = "btnRegistar";
            this.btnRegistar.Size = new System.Drawing.Size(157, 44);
            this.btnRegistar.TabIndex = 322;
            this.btnRegistar.Text = "Registar compra";
            this.btnRegistar.UseVisualStyleBackColor = false;
            this.btnRegistar.Click += new System.EventHandler(this.btnRegistar_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(20, 115);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(141, 23);
            this.label24.TabIndex = 324;
            this.label24.Text = "Qtd. Adquerida:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(35, 413);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(193, 29);
            this.label22.TabIndex = 351;
            this.label22.Text = "Itens da Compra:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.cmbItemPrevisto);
            this.panel5.Controls.Add(this.label24);
            this.panel5.Controls.Add(this.txtQtdPrevista);
            this.panel5.Controls.Add(this.label27);
            this.panel5.Controls.Add(this.label25);
            this.panel5.Controls.Add(this.btnRegistar);
            this.panel5.Controls.Add(this.numQtdAdquirida);
            this.panel5.Controls.Add(this.txtPreco);
            this.panel5.Controls.Add(this.label28);
            this.panel5.Location = new System.Drawing.Point(1000, 195);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(325, 261);
            this.panel5.TabIndex = 352;
            // 
            // cmbItemPrevisto
            // 
            this.cmbItemPrevisto.FormattingEnabled = true;
            this.cmbItemPrevisto.Location = new System.Drawing.Point(179, 21);
            this.cmbItemPrevisto.Name = "cmbItemPrevisto";
            this.cmbItemPrevisto.Size = new System.Drawing.Size(121, 24);
            this.cmbItemPrevisto.TabIndex = 327;
            this.cmbItemPrevisto.SelectedIndexChanged += new System.EventHandler(this.cmbItemPrevisto_SelectedIndexChanged_1);
            // 
            // txtQtdPrevista
            // 
            this.txtQtdPrevista.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQtdPrevista.Location = new System.Drawing.Point(179, 67);
            this.txtQtdPrevista.Multiline = true;
            this.txtQtdPrevista.Name = "txtQtdPrevista";
            this.txtQtdPrevista.ReadOnly = true;
            this.txtQtdPrevista.Size = new System.Drawing.Size(121, 25);
            this.txtQtdPrevista.TabIndex = 326;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(19, 67);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(124, 23);
            this.label27.TabIndex = 325;
            this.label27.Text = "Qtd. Prevista:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(19, 20);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(123, 23);
            this.label25.TabIndex = 233;
            this.label25.Text = "Item Previsto:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(19, 162);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(134, 23);
            this.label28.TabIndex = 266;
            this.label28.Text = "Preço Unitário:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.label17.Font = new System.Drawing.Font("Rockwell", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(20, 32);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(157, 33);
            this.label17.TabIndex = 354;
            this.label17.Text = "IShopping";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(-5, -2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(1392, 92);
            this.textBox1.TabIndex = 353;
            // 
            // btnFecharCompra
            // 
            this.btnFecharCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnFecharCompra.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFecharCompra.Font = new System.Drawing.Font("Microsoft Yi Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFecharCompra.ForeColor = System.Drawing.Color.White;
            this.btnFecharCompra.Location = new System.Drawing.Point(1035, 19);
            this.btnFecharCompra.Name = "btnFecharCompra";
            this.btnFecharCompra.Size = new System.Drawing.Size(160, 51);
            this.btnFecharCompra.TabIndex = 357;
            this.btnFecharCompra.Text = "Fechar Compra";
            this.btnFecharCompra.UseVisualStyleBackColor = false;
            this.btnFecharCompra.Click += new System.EventHandler(this.btnFecharCompra_Click_1);
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnVoltar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnVoltar.Font = new System.Drawing.Font("Microsoft Yi Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.ForeColor = System.Drawing.Color.White;
            this.btnVoltar.Location = new System.Drawing.Point(1210, 21);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(115, 49);
            this.btnVoltar.TabIndex = 356;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click_1);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(176, 36);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(26, 28);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 355;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pictureBox2.Image = global::IShopping.Properties.Resources.shopping_bag1;
            this.pictureBox2.Location = new System.Drawing.Point(40, 108);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 34);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 261;
            this.pictureBox2.TabStop = false;
            // 
            // FormModoCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 952);
            this.Controls.Add(this.btnFecharCompra);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnRemoverItem);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.pictureBox2);
            this.Name = "FormModoCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormModoCompra";
            this.Load += new System.EventHandler(this.FormModoCompra_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantidade)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQtdAdquirida)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbTipoArtigo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numQuantidade;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPreco;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateCompra;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtOrcamento;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblAviso;
        private System.Windows.Forms.TextBox txtSaldoDisponivel;
        private System.Windows.Forms.TextBox txtTotalGasto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnRemoverItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.ComboBox cmbNomeCompra;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numQtdAdquirida;
        private System.Windows.Forms.Button btnRegistar;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtQtdPrevista;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtPrecoPrevisto;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnFecharCompra;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.ComboBox cmbArtigo;
        private System.Windows.Forms.ComboBox cmbItemPrevisto;
    }
}