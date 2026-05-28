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
            InitializeComponent();

            CarregarGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            decimal valor;

            if (!decimal.TryParse(txtValor.Text, out valor))
            {
                MessageBox.Show("Valor inválido.");
                return;
            }

            DateTime dataCompra = dateTimePicker1.Value;

            // Chamar controller
            OrcamentoController.Inserir(
                txtValor.Text,
                dataCompra
            );

            MessageBox.Show("Orçamento inserido!");

            CarregarGrid();
            LimparCampos();
        }
        
        //Limpar campos
        public void LimparCampos()
        {
            txtId.Clear();
            txtValor.Clear();
            dateTimePicker1.Value = DateTime.Today;
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
                        MesAno = o.DataCompra.Value.ToString("MM/yyyy"),
                        o.ValorOrcamento
                    })
                    .ToList();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            // Limpar campos
            txtId.Clear();
            dateTimePicker1.Value = DateTime.Today; // Volta ao dia atual
            txtValor.Clear();
        }

        private void FormOrcamento_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;

            dateTimePicker1.CustomFormat = "MM/yyyy";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Ver detalhes do orçamento
        private void btnVer_Click(object sender, EventArgs e)
        {
            int OrcamentoId;

            if (!int.TryParse(txtId.Text, out OrcamentoId))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

             Orcamento orcamentos = OrcamentoController.ProcurarPorId(OrcamentoId);

            if (orcamentos != null)
            {
                txtValor.Text = orcamentos.ValorOrcamento.ToString(); // Exibe o valor do orçamento no campo de texto e convertendo para string
                dateTimePicker1.Value = orcamentos.DataCompra ?? DateTime.Today; // Se DataCompra for nula, define como data atual
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
    }
}
