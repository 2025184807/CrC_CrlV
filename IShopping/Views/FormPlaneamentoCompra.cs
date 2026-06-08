using IShopping.Controller;
using IShopping.Models;
using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormPlaneamentoCompra : Form
    {
        public FormPlaneamentoCompra()
        {
            InitializeComponent();
        }

        private void FormPlaneamentoCompra_Load(object sender, EventArgs e)
        {
            cmbFiltro.Items.Clear();

            cmbFiltro.Items.Add("Todas");
            cmbFiltro.Items.Add("Abertas");
            cmbFiltro.Items.Add("Fechadas");

            cmbFiltro.SelectedIndex = 0;

            AtualizarGrelha();
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarGrelha();
        }

        private void AtualizarGrelha()
        { 
            string filtro = cmbFiltro.SelectedItem != null ? cmbFiltro.SelectedItem.ToString() : "Todas";

            // Usa o método exato do Stor para obter a lista total
            var listaCompras = PlaneamentoController.ObterCompras();

            if (listaCompras != null)
            {
                // Aplica os filtros em memória de forma lógica
                if (filtro == "Abertas")
                {
                    listaCompras = listaCompras.Where(c => c.Fechada == false).ToList();
                }
                else if (filtro == "Fechadas")
                {
                    listaCompras = listaCompras.Where(c => c.Fechada == true).ToList();
                }

                // Define a cultura em português para obter o dia da semana traduzido
                var culturaPortuguesa = new System.Globalization.CultureInfo("pt-PT");

                // Faz a projeção para as colunas da Grid
                dataGridView1.DataSource = listaCompras.Select(c => new
                {
                    Id = c.Id,
                    Nome = c.NomeCompra,
                    DataCompra = c.DataCompra.ToString("dd/MM/yyyy"),
                    Orcamento = ModoCompraController.ObterOrcamentoCompra(c.Id).ToString("0.00") + " €",
                    Fechada = c.Fechada,

                    // DATA E HORA DO FECHO
                    DataHoraFecho = c.DataFecho.HasValue ? c.DataFecho.Value.ToString("dd/MM/yyyy HH:mm") : "---",
                    //Se a compra estiver fechada, vai criar uma coluna a dizer a Data e hora do Fechamento da compra por extenso.Se estiver aberta, mostra "---".

                    // QUEM FECHOU
                    FechadoPor = c.Fechada ? (c.FechadoPor ?? "Sistema") : "---",
                    //? se for nulo o fechado por mostra Sistema se tiver aberta mostra "---".

                    CriadoPor = c.CriadoPor,
                    CriadoEm = c.DataCriacao.ToString("dd/MM/yyyy")
                }).ToList();
            }
        }

        // Botão Nova Compra
        private void bntNovaCompra_Click(object sender, EventArgs e)
        {
            FormAlteracaoPlaneada frm = new FormAlteracaoPlaneada(0);
            frm.ShowDialog();
            AtualizarGrelha();
        }

        // Botão Alterar / Visualizar (Através da linha selecionada na Grid)
        private void btnAlteracao_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecione uma compra.", "Aviso", MessageBoxButtons.OK);
                return;
            }

            int idSelecionado = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

            FormAlteracaoPlaneada frm = new FormAlteracaoPlaneada(idSelecionado);
            frm.ShowDialog();
            AtualizarGrelha();
        }

        // Botão Alterar / Visualizar (Através do ID digitado na TextBox txtId)
        private void btnAlteracao_Click_1(object sender, EventArgs e)
        {
            // 1. Valida e converte o ID que o utilizador escreveu na TextBox (txtId)
            if (string.IsNullOrWhiteSpace(txtId.Text) || !int.TryParse(txtId.Text, out int idSelecionado))
            {
                MessageBox.Show("Por favor, introduza um ID numérico válido para alterar.");
                txtId.Focus();
                txtId.SelectAll();
                return;
            }

            // 2. Validação na Base de Dados: Verifica se a compra com esse ID realmente existe
            using (var db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.Find(idSelecionado);

                if (compra == null)
                {
                    MessageBox.Show($"A compra com o ID {idSelecionado} não foi encontrada na base de dados.");
                    return;
                }
            }

            // 3. Se o ID existe, abre o formulário do stor passando o ID validado
            FormAlteracaoPlaneada frm = new FormAlteracaoPlaneada(idSelecionado);
            frm.ShowDialog();

            // 4. Atualiza a tabela principal ao voltar para mostrar as mudanças
            AtualizarGrelha();

            // Limpa o campo do ID para ficar pronto para a próxima pesquisa
            txtId.Clear();
        }

        // Botão Eliminar Compra
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id;

            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("O ID tem de ser numérico.");
                return;
            }

            string mensagem;

            // Executa a eliminação através do controlador do stor
            PlaneamentoController.EliminarCompra(id, out mensagem);

            MessageBox.Show(mensagem);

            txtId.Clear();
            AtualizarGrelha();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // Botão Voltar
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}