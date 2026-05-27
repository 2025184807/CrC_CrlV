using IShopping.Controller;
using IShopping.Models;
using System;
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
            string valor = txtValor.Text;

            DateTime dataCompra = dateTimePicker1.Value;

            // Chamar controller
            OrcamentoController.Inserir(
                valor,
                dataCompra,
                sessao.UtilizadorAtual
            );

            MessageBox.Show("Orçamento inserido!");

            CarregarGrid();
            LimparCampos();

        }
        
        //Limpar campos
        public void LimparCampos()
        {
            txtId.Clear();
            //dataGridView1.DataSource = null; //Limpa a data grid view
            txtValor.Clear();
        }

        // Carregar data grid view
        public void CarregarGrid()
        {
            using (shoppingContext db = new shoppingContext())
            {
                //dataGridView1.DataSource = null;
                dataGridView1.DataSource = OrcamentoController.Listar(); // Vai buscar todos os orçamentos da base de dados e mostra na data grid view.
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
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
    }
}
