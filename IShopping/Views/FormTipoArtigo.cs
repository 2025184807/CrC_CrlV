using IShopping.Controller;
using IShopping.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormTipoArtigo : Form
    {
        public FormTipoArtigo()
        {
            InitializeComponent();

            CarregarGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // LIMPAR CAMPOS
        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
        }

        // CARREGAR GRID CORRIGIDO
        private void CarregarGrid()
        {
            using (shoppingContext db = new shoppingContext())
            {
                dataGridView1.DataSource = null;

                // Forçamos a criação de uma lista limpa com propriedades explícitas.
                // Isto garante que a DataGridView monte as linhas e colunas sem depender do estado do EF.
                dataGridView1.DataSource = db.TipoArtigos
                    .Select(t => new
                    {
                        Id = t.Id,
                        Categoria = t.Nome
                    })
                    .ToList();
            }
        }

        // Botão para guardar um novo tipo de artigo
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

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

            TipoArtigoController.Inserir(txtNome.Text);
            MessageBox.Show("Tipo inserido!");

            CarregarGrid();
            LimparCampos();
        }

        // Botão para limpar os campos de entrada
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        // Botão para ver os detalhes de um tipo de artigo existente
        private void btnVer_Click(object sender, EventArgs e)
        {
            int id;

            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            TipoArtigo tipoArtigo = TipoArtigoController.ProcurarPorId(id);

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
            // Validação básica dos campos, sem permitir que o nome seja vazio ou apenas espaços em branco
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            // Verificar se já existe outro tipo de artigo com o mesmo nome (ignorando o caso)
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

            int id;// Validação do ID para garantir que é um número inteiro válido

            if (!int.TryParse(txtId.Text, out id)) // Tenta converter o texto do ID para um inteiro. Se falhar, exibe uma mensagem de erro e retorna.
            {
                MessageBox.Show("O ID tem de ser numérico.");
                return;
            }

            TipoArtigoController.Editar(id, txtNome.Text); // Chama o método Editar do controlador para atualizar o tipo de artigo com o ID e nome fornecidos.

            MessageBox.Show("Tipo de artigo editado com sucesso!"); // Exibe uma mensagem de sucesso após a edição do tipo de artigo.
            CarregarGrid(); // Recarrega a grade para refletir as alterações feitas.
            LimparCampos(); // Limpa os campos de entrada para permitir uma nova operação ou evitar confusão com os dados editados.
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // Botão para eliminar um tipo de artigo existente
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id;

            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("O ID tem de ser numérico.");
                return;
            }

            // Exibir uma mensagem de confirmação antes de eliminar o tipo de artigo, informando o usuário sobre as possíveis consequências da ação.
            DialogResult resposta = MessageBox.Show(
                "Deseja eliminar este tipo de artigo? Isto poderá afetar os artigos associados.",
                "Confirmação",
                MessageBoxButtons.YesNo
            );

            // Se o usuário confirmar a eliminação, o método Eliminar do controlador é chamado para remover o tipo de artigo com o ID fornecido. Após a eliminação, uma mensagem de sucesso é exibida, a grade é recarregada para refletir as alterações e os campos de entrada são limpos.
            if (resposta == DialogResult.Yes)
            {
                TipoArtigoController.Eliminar(id);

                MessageBox.Show("Tipo de artigo eliminado com sucesso!");
                CarregarGrid();
                LimparCampos();
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
        }

        private void FormTipoArtigo_Load(object sender, EventArgs e)
        {
        }
    }
}