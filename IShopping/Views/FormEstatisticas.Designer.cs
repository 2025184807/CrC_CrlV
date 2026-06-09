namespace IShopping.Views
{
    partial class FormEstatisticas
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvComprasFechadas = new System.Windows.Forms.DataGridView();
            this.dgvOrcamentos = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtDiferencaMedia = new System.Windows.Forms.TextBox();
            this.txtMediaGastos = new System.Windows.Forms.TextBox();
            this.txtMediaOrcamentos = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listBoxItensSugeridos = new System.Windows.Forms.ListBox();
            this.btnSemana4 = new System.Windows.Forms.Button();
            this.btnSemana3 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btnSemana2 = new System.Windows.Forms.Button();
            this.btnSemana1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.btnGerarLista = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOrcamentoSugerido = new System.Windows.Forms.TextBox();
            this.btnGerarSugestao = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComprasFechadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrcamentos)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(34, 90);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1033, 746);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvComprasFechadas);
            this.tabPage1.Controls.Add(this.dgvOrcamentos);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1025, 717);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Listagens e Histórico";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvComprasFechadas
            // 
            this.dgvComprasFechadas.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvComprasFechadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComprasFechadas.Location = new System.Drawing.Point(35, 413);
            this.dgvComprasFechadas.Name = "dgvComprasFechadas";
            this.dgvComprasFechadas.RowHeadersWidth = 51;
            this.dgvComprasFechadas.RowTemplate.Height = 24;
            this.dgvComprasFechadas.Size = new System.Drawing.Size(950, 273);
            this.dgvComprasFechadas.TabIndex = 158;
            // 
            // dgvOrcamentos
            // 
            this.dgvOrcamentos.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvOrcamentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrcamentos.Location = new System.Drawing.Point(34, 84);
            this.dgvOrcamentos.Name = "dgvOrcamentos";
            this.dgvOrcamentos.RowHeadersWidth = 51;
            this.dgvOrcamentos.RowTemplate.Height = 24;
            this.dgvOrcamentos.Size = new System.Drawing.Size(951, 228);
            this.dgvOrcamentos.TabIndex = 157;
            this.dgvOrcamentos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrcamentos_CellContentClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Perpetua", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(78)))), ((int)(((byte)(123)))));
            this.label4.Location = new System.Drawing.Point(29, 362);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(403, 32);
            this.label4.TabIndex = 156;
            this.label4.Text = "Compras Fechadas do Ano Atual:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Perpetua", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(78)))), ((int)(((byte)(123)))));
            this.label3.Location = new System.Drawing.Point(29, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(265, 32);
            this.label3.TabIndex = 155;
            this.label3.Text = "Orçamentos Mensais:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1025, 717);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sugestões e Apoio à Decisão";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Perpetua", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(30, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(447, 27);
            this.label7.TabIndex = 8;
            this.label7.Text = "Sugestão de Orçamento para o Próximo Mês:";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtDiferencaMedia);
            this.panel3.Controls.Add(this.txtMediaGastos);
            this.panel3.Controls.Add(this.txtMediaOrcamentos);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(35, 75);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(480, 170);
            this.panel3.TabIndex = 12;
            // 
            // txtDiferencaMedia
            // 
            this.txtDiferencaMedia.BackColor = System.Drawing.SystemColors.Control;
            this.txtDiferencaMedia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiferencaMedia.Location = new System.Drawing.Point(253, 129);
            this.txtDiferencaMedia.Multiline = true;
            this.txtDiferencaMedia.Name = "txtDiferencaMedia";
            this.txtDiferencaMedia.ReadOnly = true;
            this.txtDiferencaMedia.Size = new System.Drawing.Size(192, 23);
            this.txtDiferencaMedia.TabIndex = 19;
            // 
            // txtMediaGastos
            // 
            this.txtMediaGastos.BackColor = System.Drawing.SystemColors.Control;
            this.txtMediaGastos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMediaGastos.Location = new System.Drawing.Point(253, 88);
            this.txtMediaGastos.Multiline = true;
            this.txtMediaGastos.Name = "txtMediaGastos";
            this.txtMediaGastos.ReadOnly = true;
            this.txtMediaGastos.Size = new System.Drawing.Size(192, 23);
            this.txtMediaGastos.TabIndex = 18;
            // 
            // txtMediaOrcamentos
            // 
            this.txtMediaOrcamentos.BackColor = System.Drawing.SystemColors.Control;
            this.txtMediaOrcamentos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMediaOrcamentos.Location = new System.Drawing.Point(253, 53);
            this.txtMediaOrcamentos.Multiline = true;
            this.txtMediaOrcamentos.Name = "txtMediaOrcamentos";
            this.txtMediaOrcamentos.ReadOnly = true;
            this.txtMediaOrcamentos.Size = new System.Drawing.Size(192, 23);
            this.txtMediaOrcamentos.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Yi Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(16, 132);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(123, 20);
            this.label11.TabIndex = 16;
            this.label11.Text = "Diferença média:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Yi Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(16, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 20);
            this.label10.TabIndex = 15;
            this.label10.Text = "Média dos Gastos:\r\n";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Yi Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(14, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(173, 20);
            this.label9.TabIndex = 14;
            this.label9.Text = "Média dos Orçamentos:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F);
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(431, 23);
            this.label1.TabIndex = 13;
            this.label1.Text = "Com base nos orçamentos dos meses anteriores:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Perpetua", 13.8F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(30, 275);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(303, 27);
            this.label6.TabIndex = 5;
            this.label6.Text = "Sugestão de Lista de Compras:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.listBoxItensSugeridos);
            this.panel2.Controls.Add(this.btnSemana4);
            this.panel2.Controls.Add(this.btnSemana3);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.btnSemana2);
            this.panel2.Controls.Add(this.btnSemana1);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.btnGerarLista);
            this.panel2.Controls.Add(this.dataGridView3);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Location = new System.Drawing.Point(35, 315);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(905, 383);
            this.panel2.TabIndex = 11;
            // 
            // listBoxItensSugeridos
            // 
            this.listBoxItensSugeridos.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxItensSugeridos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxItensSugeridos.FormattingEnabled = true;
            this.listBoxItensSugeridos.ItemHeight = 16;
            this.listBoxItensSugeridos.Location = new System.Drawing.Point(30, 148);
            this.listBoxItensSugeridos.Name = "listBoxItensSugeridos";
            this.listBoxItensSugeridos.Size = new System.Drawing.Size(832, 178);
            this.listBoxItensSugeridos.TabIndex = 16;
            // 
            // btnSemana4
            // 
            this.btnSemana4.BackColor = System.Drawing.SystemColors.Control;
            this.btnSemana4.Location = new System.Drawing.Point(683, 59);
            this.btnSemana4.Name = "btnSemana4";
            this.btnSemana4.Size = new System.Drawing.Size(110, 35);
            this.btnSemana4.TabIndex = 15;
            this.btnSemana4.Text = "4º Semana";
            this.btnSemana4.UseVisualStyleBackColor = false;
            // 
            // btnSemana3
            // 
            this.btnSemana3.BackColor = System.Drawing.SystemColors.Control;
            this.btnSemana3.Location = new System.Drawing.Point(567, 59);
            this.btnSemana3.Name = "btnSemana3";
            this.btnSemana3.Size = new System.Drawing.Size(110, 35);
            this.btnSemana3.TabIndex = 14;
            this.btnSemana3.Text = "3º Semana";
            this.btnSemana3.UseVisualStyleBackColor = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(31, 106);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(403, 23);
            this.label13.TabIndex = 11;
            this.label13.Text = "Itens mais frequentes da semana selecionada:";
            // 
            // btnSemana2
            // 
            this.btnSemana2.BackColor = System.Drawing.SystemColors.Control;
            this.btnSemana2.Location = new System.Drawing.Point(451, 59);
            this.btnSemana2.Name = "btnSemana2";
            this.btnSemana2.Size = new System.Drawing.Size(110, 35);
            this.btnSemana2.TabIndex = 13;
            this.btnSemana2.Text = "2º Semana";
            this.btnSemana2.UseVisualStyleBackColor = false;
            // 
            // btnSemana1
            // 
            this.btnSemana1.BackColor = System.Drawing.SystemColors.Control;
            this.btnSemana1.Location = new System.Drawing.Point(335, 59);
            this.btnSemana1.Name = "btnSemana1";
            this.btnSemana1.Size = new System.Drawing.Size(110, 35);
            this.btnSemana1.TabIndex = 12;
            this.btnSemana1.Text = "1º Semana";
            this.btnSemana1.UseVisualStyleBackColor = false;
            this.btnSemana1.Click += new System.EventHandler(this.btnSemana1_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(31, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(485, 23);
            this.label12.TabIndex = 10;
            this.label12.Text = "Baseado na lista da mesma semana dos outros meses:";
            // 
            // btnGerarLista
            // 
            this.btnGerarLista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnGerarLista.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarLista.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnGerarLista.Image = global::IShopping.Properties.Resources.list1;
            this.btnGerarLista.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGerarLista.Location = new System.Drawing.Point(27, 329);
            this.btnGerarLista.Name = "btnGerarLista";
            this.btnGerarLista.Size = new System.Drawing.Size(161, 43);
            this.btnGerarLista.TabIndex = 4;
            this.btnGerarLista.Text = "Gerar Lista";
            this.btnGerarLista.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnGerarLista.UseVisualStyleBackColor = false;
            this.btnGerarLista.Click += new System.EventHandler(this.btnGerarLista_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(30, 148);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(832, 175);
            this.dataGridView3.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Yi Baiti", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(31, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(258, 23);
            this.label8.TabIndex = 7;
            this.label8.Text = "Selecione a semana do mês:\r\n";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtOrcamentoSugerido);
            this.panel1.Controls.Add(this.btnGerarSugestao);
            this.panel1.Location = new System.Drawing.Point(551, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(389, 170);
            this.panel1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Yi Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(336, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Orçamento sugerido para o próximo mês:";
            // 
            // txtOrcamentoSugerido
            // 
            this.txtOrcamentoSugerido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrcamentoSugerido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrcamentoSugerido.Location = new System.Drawing.Point(31, 54);
            this.txtOrcamentoSugerido.Multiline = true;
            this.txtOrcamentoSugerido.Name = "txtOrcamentoSugerido";
            this.txtOrcamentoSugerido.ReadOnly = true;
            this.txtOrcamentoSugerido.Size = new System.Drawing.Size(315, 46);
            this.txtOrcamentoSugerido.TabIndex = 1;
            this.txtOrcamentoSugerido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGerarSugestao
            // 
            this.btnGerarSugestao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnGerarSugestao.Font = new System.Drawing.Font("Microsoft Yi Baiti", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnGerarSugestao.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGerarSugestao.Image = global::IShopping.Properties.Resources.clipboard2;
            this.btnGerarSugestao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGerarSugestao.Location = new System.Drawing.Point(104, 107);
            this.btnGerarSugestao.Name = "btnGerarSugestao";
            this.btnGerarSugestao.Size = new System.Drawing.Size(179, 45);
            this.btnGerarSugestao.TabIndex = 2;
            this.btnGerarSugestao.Text = "Gerar Sugestão";
            this.btnGerarSugestao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGerarSugestao.UseVisualStyleBackColor = false;
            this.btnGerarSugestao.Click += new System.EventHandler(this.btnGerarSugestao_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(100)))), ((int)(((byte)(145)))));
            this.btnVoltar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnVoltar.Font = new System.Drawing.Font("Microsoft Yi Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.ForeColor = System.Drawing.Color.White;
            this.btnVoltar.Location = new System.Drawing.Point(936, 42);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(131, 49);
            this.btnVoltar.TabIndex = 152;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gill Sans MT", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(78)))), ((int)(((byte)(123)))));
            this.label2.Location = new System.Drawing.Point(66, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 39);
            this.label2.TabIndex = 154;
            this.label2.Text = "Estatística";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::IShopping.Properties.Resources.bar_chart__1_;
            this.pictureBox2.Location = new System.Drawing.Point(34, 42);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(26, 38);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 179;
            this.pictureBox2.TabStop = false;
            // 
            // FormEstatisticas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1111, 848);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormEstatisticas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormEstatisticas";
            this.Load += new System.EventHandler(this.FormEstatisticas_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComprasFechadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrcamentos)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dgvComprasFechadas;
        private System.Windows.Forms.DataGridView dgvOrcamentos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOrcamentoSugerido;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGerarSugestao;
        private System.Windows.Forms.Button btnGerarLista;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDiferencaMedia;
        private System.Windows.Forms.TextBox txtMediaGastos;
        private System.Windows.Forms.TextBox txtMediaOrcamentos;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnSemana4;
        private System.Windows.Forms.Button btnSemana3;
        private System.Windows.Forms.Button btnSemana2;
        private System.Windows.Forms.Button btnSemana1;
        private System.Windows.Forms.ListBox listBoxItensSugeridos;
    }
}