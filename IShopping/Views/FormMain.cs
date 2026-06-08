using IShopping.Controller;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormMain : Form
    {
        // Já não precisamos da variável global 'CompraId' a monitorizar o clique,
        // vamos ler diretamente da TextBox quando for necessário.

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ActualizarListaCompras();
        }

        // ================= MÉTODO PARA CARREGAR AS COMPRAS EM ABERTO =================
        private void ActualizarListaCompras()
        {
            try
            {
                using (shoppingContext db = new shoppingContext())
                {
                    var comprasAbertas = db.ComprasPlaneadas
                        .Where(c => c.Fechada == false)
                        .Select(c => new
                        {
                            c.Id,
                            Nome = c.NomeCompra,
                            Data = c.DataCompra
                        })
                        .ToList();

                    dataGridView1.DataSource = comprasAbertas;
                }

                if (dataGridView1.Columns.Contains("Id"))
                    dataGridView1.Columns["Id"].Width = 60;

                // Limpa a caixa de texto ao carregar/atualizar a lista
                txtId.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar compras: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= BOTÕES DE AÇÃO DA ZONA INFERIOR =================

        // Botão "Criar Compra" (Não precisa de ID, pois cria uma nova)
        private void btnCriar_Compra_Click(object sender, EventArgs e)
        {
            FormAlteracaoPlaneada form = new FormAlteracaoPlaneada(0);
            form.ShowDialog();
            ActualizarListaCompras();
        }

        // Botão "Abrir Compra"
        private void btnCompra_Click(object sender, EventArgs e)
        {
            // 1. Tenta converter o texto digitado na txtId para um número inteiro
            if (!int.TryParse(txtId.Text.Trim(), out int idDigitado) || idDigitado <= 0)
            {
                MessageBox.Show("Por favor, introduza um ID de compra válido na caixa de texto.");
                txtId.Focus();
                return;
            }

            // 2. Passa o ID que a pessoa escreveu para o formulário
            FormAlteracaoPlaneada form = new FormAlteracaoPlaneada(idDigitado);
            form.ShowDialog();

            ActualizarListaCompras();
        }

        // Botão "Modo Compra"
        private void btnModoCompra_Click(object sender, EventArgs e)
        {
            // 1. Tenta converter o texto digitado na txtId para um número inteiro
            if (!int.TryParse(txtId.Text.Trim(), out int idDigitado) || idDigitado <= 0)
            {
                MessageBox.Show("Por favor, introduza um ID de compra válido para iniciar o Modo Compra.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtId.Focus();
                return;
            }

            FormModoCompra form = new FormModoCompra();

            // 2. Passa o ID que a pessoa escreveu para a variável pública do outro form
            form.CompraIdSelecionada = idDigitado;

            form.ShowDialog();
            ActualizarListaCompras();
        }

        // ================= MENUS SUPERIORES E NAVEGAÇÃO =================

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout efetuado com sucesso.");
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void btnGerirUtilizadores_Click(object sender, EventArgs e)
        {
            FormGerirUtilizadores form = new FormGerirUtilizadores();
            form.Show();
        }

        private void btnTipoArtigo_Click(object sender, EventArgs e)
        {
            FormTipoArtigo form = new FormTipoArtigo();
            form.Show();
        }

        private void btnOrcamentos_Click(object sender, EventArgs e)
        {
            FormOrcamento form = new FormOrcamento();
            form.Show();
        }

        private void btnArtigo_Click(object sender, EventArgs e)
        {
            FormGestaoArtigo form = new FormGestaoArtigo();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormEstatisticas form = new FormEstatisticas();
            form.Show();
        }

        private void btnPlaneamento_click(object sender, EventArgs e)
        {
            FormPlaneamentoCompra form = new FormPlaneamentoCompra();
            form.Show();
        }

        // Métodos vazios limpos para evitar conflitos
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void pictureBox4_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void btnCriarCompra_Click(object sender, EventArgs e)
        {
            FormAlteracaoPlaneada form = new FormAlteracaoPlaneada(0);
            form.Show();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            // 1. Chama o controlador para gerar o conteúdo do CSV
            string conteudoCSV = PlaneamentoController.ExportarComprasFechadasParaCSV();

            // 2. Configura a janela de diálogo para guardar o ficheiro
            SaveFileDialog salvarFicheiro = new SaveFileDialog();
            salvarFicheiro.Filter = "Ficheiros CSV (*.csv)|*.csv";
            salvarFicheiro.Title = "Exportar Compras Fechadas";
            salvarFicheiro.FileName = "Compras_Fechadas.csv";

            // 3. Se o utilizador escolher o caminho e clicar em "Guardar"
            if (salvarFicheiro.ShowDialog() == DialogResult.OK)
            {
                // Grava o texto gerado no caminho escolhido com codificação UTF-8 (para não estragar os acentos)
                System.IO.File.WriteAllText(salvarFicheiro.FileName, conteudoCSV, System.Text.Encoding.UTF8);

                MessageBox.Show("Ficheiro CSV exportado com sucesso!", "Exportação", MessageBoxButtons.OK);
            }
        }
    }
}