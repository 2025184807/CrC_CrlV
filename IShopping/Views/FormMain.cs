using IShopping.Controller;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    // Classe do formulário principal do sistema (Menu Geral)
    public partial class FormMain : Form
    {
        // Construtor - Inicializa o formulário
        public FormMain()
        {
            InitializeComponent();
        }

        // Evento que dispara ao abrir o formulário
        private void FormMain_Load(object sender, EventArgs e)
        {
            ActualizarListaCompras(); // Carrega as compras abertas na tabela
        }

        // Método para carregar as compras em aberto
        private void ActualizarListaCompras()
        {
            try
            {
                using (shoppingContext db = new shoppingContext())
                {
                    // Procura na BD todas as compras que não estão fechadas
                    var comprasAbertas = db.ComprasPlaneadas
                        .Where(c => c.Fechada == false)
                        .Select(c => new
                        {
                            c.Id,
                            Nome = c.NomeCompra,
                            Data = c.DataCompra
                        })
                        .ToList();

                    // Coloca a lista como fonte de dados da tabela visual
                    dataGridView1.DataSource = comprasAbertas;
                }

                // Ajusta a largura da coluna ID se ela existir
                if (dataGridView1.Columns.Contains("Id"))
                    dataGridView1.Columns["Id"].Width = 60;

                // Limpa a caixa de texto do ID para o próximo uso
                txtId.Text = "";
            }
            catch (Exception ex)
            {
                // Mostra um aviso caso ocorra algum erro na BD
                MessageBox.Show($"Erro ao carregar compras: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // BOTÕES NA ZONA INFERIOR

        // Botão "Criar Compra" - Abre o ecrã com o ID 0 (Nova Compra)
        private void btnCriar_Compra_Click(object sender, EventArgs e)
        {
            FormAlteracaoPlaneada form = new FormAlteracaoPlaneada(0);
            form.ShowDialog();
            ActualizarListaCompras(); // Atualiza a tabela ao fechar a janela
        }

        // Botão "Abrir Compra" - Abre uma compra existente com o ID digitado
        private void btnCompra_Click(object sender, EventArgs e)
        {
            // Valida se o ID digitado é um número inteiro válido e maior que zero
            if (!int.TryParse(txtId.Text.Trim(), out int idDigitado) || idDigitado <= 0)
            {
                MessageBox.Show("Por favor, introduza um ID de compra válido na caixa de texto.");
                txtId.Focus();
                return;
            }

            // Abre a compra com o ID validado
            FormAlteracaoPlaneada form = new FormAlteracaoPlaneada(idDigitado);
            form.ShowDialog();

            ActualizarListaCompras(); // Atualiza a tabela ao fechar
        }

        // Botão "Modo Compra" - Abre o ecrã do modo de compras ativo
        private void btnModoCompra_Click(object sender, EventArgs e)
        {
            FormModoCompra form = new FormModoCompra();
            form.ShowDialog();
            ActualizarListaCompras(); // Atualiza a lista ao fechar
        }

        // MENU COM BOTÕES NA ZONA SUPERIOR

        // Botão "Logout" - Fecha o formulário pedindo para reiniciar o login
        private void btnLogout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout efetuado com sucesso.");
            this.DialogResult = DialogResult.Retry; // Sinaliza o pedido de troca de utilizador
            this.Close();
        }

        // Botão - Abre a gestão de utilizadores
        private void btnGerirUtilizadores_Click(object sender, EventArgs e)
        {
            FormGerirUtilizadores form = new FormGerirUtilizadores();
            form.Show();
        }

        // Botão - Abre a gestão de tipos de artigo
        private void btnTipoArtigo_Click(object sender, EventArgs e)
        {
            FormTipoArtigo form = new FormTipoArtigo();
            form.Show();
        }

        // Botão - Abre o ecrã de orçamentos
        private void btnOrcamentos_Click(object sender, EventArgs e)
        {
            FormOrcamento form = new FormOrcamento();
            form.Show();
        }

        // Botão - Abre o catálogo de artigos
        private void btnArtigo_Click(object sender, EventArgs e)
        {
            FormGestaoArtigo form = new FormGestaoArtigo();
            form.Show();
        }

        // Botão - Abre o ecrã de relatórios e estatísticas
        private void button2_Click(object sender, EventArgs e)
        {
            FormEstatisticas form = new FormEstatisticas();
            form.Show();
        }

        // Botão - Abre o ecrã de planeamento
        private void btnPlaneamento_click(object sender, EventArgs e)
        {
            FormPlaneamentoCompra form = new FormPlaneamentoCompra();
            form.Show();
        }

        // Botão alternativo para criar compras
        private void btnCriarCompra_Click(object sender, EventArgs e)
        {
            FormAlteracaoPlaneada form = new FormAlteracaoPlaneada(0);
            form.Show();
        }

        // Botão "Exportar" - Grava o histórico de compras num ficheiro CSV
        private void btnExportar_Click(object sender, EventArgs e)
        {
            // 1. Pede ao controlador o texto formatado em CSV
            string conteudoCSV = PlaneamentoController.ExportarComprasFechadasParaCSV();

            // 2. Configura a janela para o utilizador escolher onde guardar o ficheiro
            SaveFileDialog salvarFicheiro = new SaveFileDialog();
            salvarFicheiro.Filter = "Ficheiros CSV (*.csv)|*.csv";
            salvarFicheiro.Title = "Exportar Compras Fechadas";
            salvarFicheiro.FileName = "Compras_Fechadas.csv";

            // 3. Se o utilizador confirmar o local e clicar em Guardar
            if (salvarFicheiro.ShowDialog() == DialogResult.OK)
            {
                // Grava o ficheiro com codificação UTF-8 para manter os acentos corretos
                System.IO.File.WriteAllText(salvarFicheiro.FileName, conteudoCSV, System.Text.Encoding.UTF8);

                MessageBox.Show("Ficheiro CSV exportado com sucesso!");
            }
        }

        // Botão Atualizar - Recarrega manualmente a tabela de compras
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            ActualizarListaCompras();
        }

        // Métodos vazios do Windows Forms (Mantidos para evitar erros no .Designer.cs)
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void pictureBox4_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}