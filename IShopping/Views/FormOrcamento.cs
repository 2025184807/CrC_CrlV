using IShopping.Controller;
using IShopping.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    // Classe responsável pelo ecrã de gestão de Orçamentos Mensais
    public partial class FormOrcamento : Form
    {
        // Construtor - Inicializa o formulário e carrega os dados na tabela
        public FormOrcamento()
        {
            InitializeComponent();
            CarregarGrid();
        }

        //Botão guardar
        // Botão para inserir um novo orçamento
        private void button1_Click(object sender, EventArgs e)
        {
            // Valida se o campo do valor está vazio
            if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Preenche o valor.");
                return;
            }

            // Valida se o valor inserido é um número decimal válido
            if (!decimal.TryParse(txtValor.Text, out decimal valor))
            {
                MessageBox.Show("Valor inválido.");
                return;
            }

            // Obtém o ano e o mês selecionados no componente de data
            int ano = dateTimePicker1.Value.Year;
            int mes = dateTimePicker1.Value.Month;

            // Verifica na Base de Dados se já existe um orçamento criado para o mesmo mês e ano
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

            // Chama o Controller para gravar o novo orçamento
            OrcamentoController.Inserir(valor, ano, mes);

            MessageBox.Show("Orçamento inserido!");

            // Atualiza a tabela e limpa os campos do formulário
            CarregarGrid();
            LimparCampos();
        }

        // Carrega a lista de orçamentos da BD diretamente para o DataGridView
        public void CarregarGrid()
        {
            using (shoppingContext db = new shoppingContext())
            {
                dataGridView1.DataSource = db.Orcamentos
                    .OrderByDescending(o => o.Ano) // Ordena do ano mais recente para o mais antigo
                    .ThenByDescending(o => o.Mes)  // Ordena do mês mais recente para o mais antigo
                    .ToList()
                    .Select(o => new
                    {
                        o.OrcamentoId,
                        MesAno = $"{o.Mes:D2}/{o.Ano}", // Formata o mês com 2 dígitos (ex: 05/2026)
                        o.ValorOrcamento
                    })
                    .ToList();
            }
        }


        // Repõe os valores padrão dos campos de texto e data
        public void LimparCampos()
        {
            txtId.Clear();
            txtValor.Clear();
            dateTimePicker1.Value = DateTime.Today;
        }

        // Botão para limpar manualmente os campos do ecrã
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

       
        //Botão ver
        // Botão para procurar e mostrar os detalhes de um orçamento pelo ID
        private void btnVer_Click(object sender, EventArgs e)
        {
            // Valida se o ID digitado é um número inteiro válido
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            // Procura o registo na BD usando o Controller
            var orcamento = OrcamentoController.ProcurarPorId(id);

            // Se não encontrar, avisa o utilizador
            if (orcamento == null)
            {
                MessageBox.Show("Não encontrado.");
                return;
            }

            // Preenche os campos do ecrã com os dados encontrados
            txtValor.Text = orcamento.ValorOrcamento.ToString("0.00");

            // Define a data no componente usando o ano e mês do registo (define o dia como 1)
            dateTimePicker1.Value = new DateTime(
                orcamento.Ano,
                orcamento.Mes,
                1
            );
        }

        // Botão de editar
        // Botão para atualizar um orçamento existente
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Valida o ID do orçamento que vai ser alterado
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            // Valida se o novo preço digitado é válido
            if (!decimal.TryParse(txtValor.Text, out decimal valor))
            {
                MessageBox.Show("Valor inválido.");
                return;
            }

            int ano = dateTimePicker1.Value.Year;
            int mes = dateTimePicker1.Value.Month;

            // Valida se a alteração vai gerar uma duplicidade de mês/ano noutro ID diferente
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

            // Guarda as alterações através do Controller
            OrcamentoController.Editar(id, valor, ano, mes);

            MessageBox.Show("Editado com sucesso!");

            // Atualiza a tabela e limpa a área de edição
            CarregarGrid();
            LimparCampos();
        }

        // botão de eliminar
        // Botão para remover de forma permanente um orçamento pelo ID
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Valida se o ID inserido é um número válido
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            // Executa a remoção através do Controller
            OrcamentoController.Eliminar(id);

            MessageBox.Show("Eliminado!");

            // Atualiza a tabela e limpa os campos
            CarregarGrid();
            LimparCampos();
        }

        // Botão Sair 
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Evento executado ao carregar o formulário
        private void FormOrcamento_Load(object sender, EventArgs e)
        {
            // Configura o componente de data para mostrar apenas o mês e o ano
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/yyyy";
        }
    }
}