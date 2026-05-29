using IShopping.Controller;
using IShopping.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormOrcamento : Form
    {
        public FormOrcamento()
        {
            InitializeComponent(); // Inicializar os componentes do formulário

            CarregarGrid(); // Carregar os dados na DataGridView ao iniciar o formulário
        }

        // Inserir orçamento
        private void button1_Click(object sender, EventArgs e)
        {
            // Verificar se o campo de valor do orçamento está vazio, se estiver exibe uma mensagem de erro e retorna
            if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            // Obter data selecionada
            DateTime dataCompra = dateTimePicker1.Value;

            decimal valor;

            // Tenta converter o valor do orçamento para decimal, se falhar exibe uma mensagem de erro
            if (!decimal.TryParse(txtValor.Text, out valor))
            {
                MessageBox.Show("Valor inválido.");
                return;
            }

            // Verificar se já existe um orçamento para o mesmo mês e ano
            using (shoppingContext db = new shoppingContext())
            {
                // O método Any verifica se existe algum orçamento que tenha a mesma data de compra (mês e ano) do orçamento que está sendo inserido
                bool existe = db.Orcamentos.Any(o =>
                    o.DataCompra.Year == dataCompra.Year &&
                    o.DataCompra.Month == dataCompra.Month
                );

                // Se existir um orçamento para o mesmo mês e ano, exibe uma mensagem de erro e retorna
                if (existe)
                {
                    MessageBox.Show("Já existe um orçamento neste mês e ano!");
                    return;
                }
            }

            // Chamar controller
            OrcamentoController.Inserir(
                txtValor.Text,
                dataCompra
            );

            // Exibir mensagem de sucesso
            MessageBox.Show("Orçamento inserido!");

            CarregarGrid();
            LimparCampos();
        }
        
        //Limpar campos
        public void LimparCampos()
        {
            txtId.Clear();
            txtValor.Clear();
            dateTimePicker1.Value = DateTime.Today; // Volta ao dia atual
        }

        // Carregar data grid view
        public void CarregarGrid()
        {
            using (shoppingContext db = new shoppingContext())
            {
                dataGridView1.DataSource = db.Orcamentos
                    .ToList()
                    .Select(o => new
                    {
                        o.OrcamentoId,
                        MesAno = o.DataCompra.ToString("MM/yyyy"),
                        o.ValorOrcamento
                    })
                    .ToList();
            }
        }

        // Limpar campos ao clicar no botão "Limpar"
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            // Limpar campos
            txtId.Clear();
            dateTimePicker1.Value = DateTime.Today; // Volta ao dia atual
            txtValor.Clear();
        }

        // Configurar o DateTimePicker para mostrar apenas mês e ano
        private void FormOrcamento_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom; // Define o formato personalizado

            dateTimePicker1.CustomFormat = "MM/yyyy"; // Exibe apenas mês e ano
        }

        // Fechar formulário
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Ver detalhes do orçamento
        private void btnVer_Click(object sender, EventArgs e)
        {
            int OrcamentoId;

            // Tenta converter o valor do campo de ID para inteiro, se falhar exibe uma mensagem de erro e retorna
            if (!int.TryParse(txtId.Text, out OrcamentoId))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            // Chamar controller para procurar o orçamento por ID
            Orcamento orcamentos = OrcamentoController.ProcurarPorId(OrcamentoId);

            if (orcamentos != null)
            {
                txtValor.Text = orcamentos.ValorOrcamento.ToString(); // Exibe o valor do orçamento no campo de texto e convertendo para string
                dateTimePicker1.Value = orcamentos.DataCompra; // Define o valor do DateTimePicker com a data do orçamento
            }
            else
            {
                MessageBox.Show("Orçamento não encontrado.");
            }
        }

        // Editar orçamento
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            int OrcamentoId;

            if (!int.TryParse(txtId.Text, out OrcamentoId))
            {
                MessageBox.Show("ID inválido.");
                return;

            }

            // Chamar controller para editar o orçamento
            OrcamentoController.Editar(
                    OrcamentoId,
                    decimal.Parse(txtValor.Text), //converte o valor do orçamento para decimal
                    dateTimePicker1.Value //data de compra selecionada no DateTimePicker
                );

             MessageBox.Show("Orçamento editado com sucesso!");

             CarregarGrid();
             LimparCampos(); 
        }

        //Eliminar orçamento
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int OrcamentoId;

            if (!int.TryParse(txtId.Text, out OrcamentoId))
            {
                MessageBox.Show("O ID tem de ser numérico.");
                return;
            }

            OrcamentoController.Eliminar(OrcamentoId);

            MessageBox.Show("Orçamento eliminado com sucesso!");
            CarregarGrid();

            LimparCampos();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        //clica na imagem para fechar o formulário
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
