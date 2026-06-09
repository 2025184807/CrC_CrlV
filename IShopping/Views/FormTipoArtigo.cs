using IShopping.Controller;
using IShopping.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    // Classe responsável pelo ecrã de gestão de Categorias / Tipos de Artigo
    public partial class FormTipoArtigo : Form
    {
        // Construtor - Inicializa o formulário e carrega os dados na tabela
        public FormTipoArtigo()
        {
            InitializeComponent();

            CarregarGrid();
        }

        // Botão para fechar o formulário
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // LIMPAR CAMPOS - Remove o texto das caixas de ID e Nome
        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
        }

        // CARREGAR GRID - Mostra todas as categorias guardadas na base de dados
        private void CarregarGrid()
        {
            using (shoppingContext db = new shoppingContext())
            {
                dataGridView1.DataSource = null;

                // Seleciona o ID e o Nome (rebatizado como Categoria) para a tabela visual
                dataGridView1.DataSource = db.TipoArtigos
                    .Select(t => new
                    {
                        Id = t.Id,
                        Categoria = t.Nome
                    })
                    .ToList();
            }
        }

        // Botão para guardar um novo tipo de artigo (Inserir)
        private void button1_Click(object sender, EventArgs e)
        {
            // Valida se o campo do nome está vazio ou apenas com espaços
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            // Verifica se já existe uma categoria com o mesmo nome
            using (shoppingContext db = new shoppingContext())
            {
                bool existe = db.TipoArtigos
                    .Any(t => t.Nome.ToLower() == txtNome.Text.ToLower());

                if (existe)
                {
                    MessageBox.Show("Já existe um tipo de artigo com esse nome.");
                    return;
                }
            }

            // Grava através do Controller se o nome for unico
            TipoArtigoController.Inserir(txtNome.Text);
            MessageBox.Show("Tipo inserido!");

            // Atualiza a tabela visual e limpa os campos de escrita
            CarregarGrid();
            LimparCampos();
        }

        // Botão para limpar os campos de entrada manualmente
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        // Botão para ver os detalhes de um tipo de artigo existente através do ID
        private void btnVer_Click(object sender, EventArgs e)
        {
            int id;

            // Valida se o ID introduzido é um número inteiro válido
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            // Procura o registo na BD recorrendo ao Controller
            TipoArtigo tipoArtigo = TipoArtigoController.ProcurarPorId(id);

            // Se encontrar, preenche a caixa do nome; caso contrário, avisa o utilizador
            if (tipoArtigo != null)
            {
                txtNome.Text = tipoArtigo.Nome;
            }
            else
            {
                MessageBox.Show("Tipo de artigo não encontrado.");
            }
        }

        // Botão para editar um tipo de artigo existente
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Validação básica para não permitir nomes vazios
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            // Verificar se o novo nome já está a ser usado por outra categoria
            using (shoppingContext db = new shoppingContext())
            {
                bool existe = db.TipoArtigos
                    .Any(t => t.Nome.ToLower() == txtNome.Text.ToLower());

                if (existe)
                {
                    MessageBox.Show("Já existe um tipo de artigo com esse nome.");
                    return;
                }
            }

            int id;

            // Validação do ID para garantir que é um número inteiro válido
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("O ID tem de ser numérico.");
                return;
            }

            // Executa a atualização através do Controller
            TipoArtigoController.Editar(id, txtNome.Text);

            MessageBox.Show("Tipo de artigo editado com sucesso!");
            CarregarGrid(); // Recarrega a tabela
            LimparCampos(); // Limpa os controlos
        }

        // Botão para eliminar um tipo de artigo existente
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id;

            // Garante que o ID fornecido é um número válido
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("O ID tem de ser numérico.");
                return;
            }

            // Caixa de diálogo de confirmação para prevenir eliminações acidentais
            DialogResult resposta = MessageBox.Show(
                "Deseja eliminar este tipo de artigo? Isto poderá afetar os artigos associados.",
                "Confirmação",
                MessageBoxButtons.YesNo
            );

            // Se a resposta for SIM, remove o registo e atualiza a interface
            if (resposta == DialogResult.Yes)
            {
                TipoArtigoController.Eliminar(id);

                MessageBox.Show("Tipo de artigo eliminado com sucesso!");
                CarregarGrid();
                LimparCampos();
            }
        }

        // Métodos automáticos gerados pelo Visual Studio (Mantidos vazios para não quebrar o Designer)
        private void txtNome_TextChanged(object sender, EventArgs e) { }
        private void FormTipoArtigo_Load(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e){ }
    }
}