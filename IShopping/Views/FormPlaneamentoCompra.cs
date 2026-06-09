using IShopping.Controller;
using IShopping.Models;
using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace IShopping.Views
{
    // Classe responsável pelo ecrã de Planeamento e Histórico de Compras
    public partial class FormPlaneamentoCompra : Form
    {
        // Construtor - Inicializa o formulário
        public FormPlaneamentoCompra()
        {
            InitializeComponent();
        }

        // Evento executado ao carregar o ecrã
        private void FormPlaneamentoCompra_Load(object sender, EventArgs e)
        {
            cmbFiltro.Items.Clear();

            // Adiciona as opções de filtragem na ComboBox
            cmbFiltro.Items.Add("Todas");
            cmbFiltro.Items.Add("Abertas");
            cmbFiltro.Items.Add("Fechadas");

            cmbFiltro.SelectedIndex = 0; // Define "Todas" como filtro inicial

            AtualizarGrelha(); // Carrega a tabela com os dados
        }

        // Evento que atualiza sempre que o utilizador muda a opção do filtro
        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarGrelha();
        }

        // Método principal para carregar, filtrar e formatar a lista de compras na tabela
        private void AtualizarGrelha()
        {
            string filtro = cmbFiltro.SelectedItem != null ? cmbFiltro.SelectedItem.ToString() : "Todas";

            // Obtém a lista completa de compras registadas através do Controller
            var listaCompras = PlaneamentoController.ObterCompras();

            if (listaCompras != null)
            {
                // Aplica o filtro de estado selecionado na ComboBox
                if (filtro == "Abertas")
                {
                    listaCompras = listaCompras.Where(c => c.Fechada == false).ToList();
                }
                else if (filtro == "Fechadas")
                {
                    listaCompras = listaCompras.Where(c => c.Fechada == true).ToList();
                }

                // Cria e preenche as colunas da DataGridView de forma organizada
                dataGridView1.DataSource = listaCompras.Select(c => new
                {
                    Id = c.Id,
                    Nome = c.NomeCompra,
                    DataCompra = c.DataCompra.ToString("dd/MM/yyyy"),
                    Orcamento = ModoCompraController.ObterOrcamentoCompra(c.Id).ToString("0.00") + " €",
                    Fechada = c.Fechada,

                    // DATA E HORA DO FECHO
                    DataHoraFecho = c.DataFecho.HasValue ? c.DataFecho.Value.ToString("dd/MM/yyyy HH:mm") : "---",

                    // QUEM FECHOU
                    FecharPor = c.Fechada ? (c.FechadoPor ?? "Sistema") : "---",

                    CriadoPor = c.CriadoPor,
                    CriadoEm = c.DataCriacao.ToString("dd/MM/yyyy")
                }).ToList();
            }
        }

        // Botão "Nova Compra" - Abre o ecrã de alteração em modo de criação (ID 0)
        private void bntNovaCompra_Click(object sender, EventArgs e)
        {
            FormAlteracaoPlaneada frm = new FormAlteracaoPlaneada(0);
            frm.ShowDialog();
            AtualizarGrelha(); // Atualiza a tabela ao fechar a janela secundária
        }

        // Botão Alterar / Visualizar através do ID digitado manualmente na TextBox
        private void btnAlteracao_Click_1(object sender, EventArgs e)
        {
            // 1. Valida se o texto digitado é um ID numérico inteiro válido
            if (string.IsNullOrWhiteSpace(txtId.Text) || !int.TryParse(txtId.Text, out int idSelecionado))
            {
                MessageBox.Show("Por favor, introduza um ID numérico válido para alterar.");
                txtId.Focus();
                txtId.SelectAll();
                return;
            }

            // 2. Procura na Base de Dados para garantir que o ID inserido existe mesmo
            using (var db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.Find(idSelecionado);

                if (compra == null)
                {
                    MessageBox.Show($"A compra com o ID {idSelecionado} não foi encontrada na base de dados.");
                    return;
                }
            }

            // 3. Se existir, abre a janela passando o ID validado
            FormAlteracaoPlaneada frm = new FormAlteracaoPlaneada(idSelecionado);
            frm.ShowDialog();

            // 4. Recarrega a tabela e limpa o campo de texto do ID
            AtualizarGrelha();
            txtId.Clear();
        }

        // Botão "Eliminar Compra" - Remove uma lista pelo ID inserido na TextBox
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id;

            // Valida se o ID digitado é um número
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("O ID tem de ser numérico.");
                return;
            }

            string mensagem;

            // Executa o comando de apagar através do Controller
            PlaneamentoController.EliminarCompra(id, out mensagem);

            MessageBox.Show(mensagem);

            // Reseta o ecrã limpando o campo e recarregando a tabela
            txtId.Clear();
            AtualizarGrelha();
        }

        // Método automático do clique na tabela (Pode ficar vazio)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // Botão Sair / Fechar Janela
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}