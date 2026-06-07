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

        // ================= INSERT =================

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Preenche o valor.");
                return;
            }

            if (!decimal.TryParse(txtValor.Text, out decimal valor))
            {
                MessageBox.Show("Valor inválido.");
                return;
            }

            int ano = dateTimePicker1.Value.Year;
            int mes = dateTimePicker1.Value.Month;

            using (shoppingContext db = new shoppingContext())
            {
                bool existe = db.Orcamentos.Any(o =>
                    o.Ano == ano &&
                    o.Mes == mes);

                if (existe)
                {
                    MessageBox.Show("Já existe orçamento para este mês.");
                    return;
                }
            }

            OrcamentoController.Inserir(valor, ano, mes);

            MessageBox.Show("Orçamento inserido!");

            CarregarGrid();
            LimparCampos();
        }

        // ================= GRID =================

        public void CarregarGrid()
        {
            using (shoppingContext db = new shoppingContext())
            {
                dataGridView1.DataSource = db.Orcamentos
                    .OrderByDescending(o => o.Ano) // Primeiro ordena por ano em ordem decrescente
                    .ThenByDescending(o => o.Mes)
                    .ToList()
                    .Select(o => new
                    {
                        o.OrcamentoId,
                        MesAno = $"{o.Mes:D2}/{o.Ano}", // Formata o mês com dois dígitos e o ano com quatro dígitos
                        o.ValorOrcamento
                    })
                    .ToList();
            }
        }

        // ================= LIMPAR =================

        public void LimparCampos()
        {
            txtId.Clear();
            txtValor.Clear();
            dateTimePicker1.Value = DateTime.Today;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        // ================= VER =================

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            var orcamento = OrcamentoController.ProcurarPorId(id);

            if (orcamento == null)
            {
                MessageBox.Show("Não encontrado.");
                return;
            }

            txtValor.Text = orcamento.ValorOrcamento.ToString("0.00");

            dateTimePicker1.Value = new DateTime(
                orcamento.Ano,
                orcamento.Mes,
                1
            );
        }

        // ================= EDITAR =================

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            if (!decimal.TryParse(txtValor.Text, out decimal valor))
            {
                MessageBox.Show("Valor inválido.");
                return;
            }

            int ano = dateTimePicker1.Value.Year;
            int mes = dateTimePicker1.Value.Month;

            using (shoppingContext db = new shoppingContext())
            {
                bool existe = db.Orcamentos.Any(o =>
                    o.OrcamentoId != id &&
                    o.Ano == ano &&
                    o.Mes == mes);

                if (existe)
                {
                    MessageBox.Show("Já existe orçamento para esse mês.");
                    return;
                }
            }

            OrcamentoController.Editar(id, valor, ano, mes);

            MessageBox.Show("Editado com sucesso!");

            CarregarGrid();
            LimparCampos();
        }

        // ================= ELIMINAR =================

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            OrcamentoController.Eliminar(id);

            MessageBox.Show("Eliminado!");

            CarregarGrid();
            LimparCampos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormOrcamento_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/yyyy";
        }
    }
}