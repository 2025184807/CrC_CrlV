using IShopping.Controller;
using IShopping.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormGestaoArtigo : Form
    {
        public FormGestaoArtigo()
        {
            InitializeComponent();

            CarregarTipos(); // Carrega os tipos de artigo no combo box
            CarregarGrid(); // Carrega os artigos na data grid view
        }

        private void FormGestaoArtigo_Load(object sender, EventArgs e)
        {
            CarregarComboTipoArtigo(); // Carrega os tipos de artigo no combo box
        }

        // CARREGAR TIPOS-- Carrega os tipos de artigo no combo box
        private void CarregarTipos()
        {
            cmbTipoArtigo.DataSource = TipoArtigoController.Listar(); 

            cmbTipoArtigo.DisplayMember = "Nome";
            cmbTipoArtigo.ValueMember = "Id";

            cmbTipoArtigo.SelectedIndex = -1;
        }

        //Limpar campos
        public void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtPreco.Clear();
            cmbTipoArtigo.SelectedIndex = -1; // Desseleciona o combo box
        }

        // Carregar data grid view
        public void CarregarGrid()
        {
            using (shoppingContext db = new shoppingContext())
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = ArtigoController.Listar(); // Vai buscar todos os artigos da base de dados e mostra na data grid view.
            }
        }


        // Botão para guardar um novo artigo
        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                    string.IsNullOrWhiteSpace(txtPreco.Text) ||
                    cmbTipoArtigo.SelectedIndex == -1)
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            // Verificar se já existe um artigo com o mesmo nome (ignorar maiúsculas/minúsculas)
            using (shoppingContext db = new shoppingContext())
            {
                bool existe = db.Artigos
                    .Any(a => a.Nome.ToLower() == txtNome.Text.ToLower());

                if (existe)
                {
                    MessageBox.Show("Já existe um artigo com esse nome.");
                    return;
                }
            }

            decimal preco;

            if (!decimal.TryParse(txtPreco.Text, out preco))
            {
                MessageBox.Show("Preço inválido.");
                return;
            }

            ArtigoController.Inserir(
                txtNome.Text,
                (int)cmbTipoArtigo.SelectedValue,
                preco
            );

            MessageBox.Show("Artigo inserido com sucesso!");

            CarregarGrid();
            LimparCampos();
        }

        // Botão para editar um artigo existente
        private void button1_Click(object sender, EventArgs e)
        {
            int id;

            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("Seleciona um artigo.");
                return;
            }

            decimal preco;

            if (!decimal.TryParse(txtPreco.Text, out preco))
            {
                MessageBox.Show("Preço inválido.");
                return;
            }

            ArtigoController.Editar( // Chama o método Editar do ArtigoController para atualizar um artigo existente. Ele passa o ID do artigo, o nome, o tipo de artigo selecionado no combo box (convertido para inteiro) e o preço.
                id,
                txtNome.Text,
                (int)cmbTipoArtigo.SelectedValue, // (int) é necessário para converter o SelectedValue do combo box, que é do tipo object, para um inteiro, que é o tipo esperado pelo método Editar para o parâmetro tipoArtigoId.
                preco
            );

            MessageBox.Show("Artigo editado!");

            CarregarGrid();
            LimparCampos();
        }

        // Carregar os tipos de artigo no combo box
        private void CarregarComboTipoArtigo()
        {
            using (shoppingContext db = new shoppingContext())
            {
                cbTipoArtigo.DataSource = db.TipoArtigos.ToList(); // Define a fonte de dados do combo box como a lista de tipos de artigo obtida do banco de dados usando Entity Framework.
                cbTipoArtigo.DisplayMember = "Nome"; // Define o membro a ser exibido no combo box, que é a propriedade "Nome" do tipo de artigo.
                cbTipoArtigo.ValueMember = "Id"; // Define o membro a ser usado como valor do combo box, que é a propriedade "Id" do tipo de artigo.
                cbTipoArtigo.SelectedIndex = -1; // Desseleciona o combobox para que nenhum tipo de artigo esteja selecionado por padrão.
            }


        }

        //Ver os artigos selecionados na data grid view
        private void btnVer_Click(object sender, EventArgs e)
        {
            int id;

            if (!int.TryParse(txtId.Text, out id)) //O TRYPARSE tenta converter o texto do txtId para um inteiro. Se a conversão falhar, ele retorna false e o código dentro do if é executado, mostrando uma mensagem de erro e saindo do método.
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            Artigo artigo = ArtigoController.ProcurarPorId(id);

            if (artigo != null)
            {
                txtNome.Text = artigo.Nome;
                txtPreco.Text = artigo.Preco.ToString();

                cmbTipoArtigo.SelectedValue = artigo.TipoArtigoId; // Define o valor selecionado do combo box com base no TipoArtigoId do artigo encontrado.
            }
            else
            {
                MessageBox.Show("Artigo não encontrado.");
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        //Voltar 
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Eliminar um artigo
        private void button5_Click(object sender, EventArgs e)
        {
            int id;

            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("Seleciona um artigo.");
                return;
            }

            DialogResult resposta = MessageBox.Show(
                "Deseja eliminar este artigo?",
                "Confirmação",
                MessageBoxButtons.YesNo
            );

            if (resposta == DialogResult.Yes)
            {
                ArtigoController.Eliminar(id);

                MessageBox.Show("Artigo eliminado!");

                CarregarGrid();
                LimparCampos();
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (shoppingContext db = new shoppingContext())
            {
                if (!cbxTodos.Checked &&
                    cbTipoArtigo.SelectedValue != null &&
                    cbTipoArtigo.SelectedValue is int)
                {
                    int tipoId = (int)cbTipoArtigo.SelectedValue;

                    dataGridView1.DataSource = db.Artigos
                        .Where(a => a.TipoArtigoId == tipoId)
                        .ToList();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void cbxTodos_CheckedChanged(object sender, EventArgs e)
        {
            using (shoppingContext db = new shoppingContext())
            {
                if (cbxTodos.Checked)
                {
                    dataGridView1.DataSource = db.Artigos.ToList();

                    cbTipoArtigo.Enabled = false;
                }
                else
                {
                    cbTipoArtigo.Enabled = true;

                    if (cbTipoArtigo.SelectedValue != null)
                    {
                        int tipoId = (int)cbTipoArtigo.SelectedValue;

                        dataGridView1.DataSource = db.Artigos
                            .Where(a => a.TipoArtigoId == tipoId)
                            .ToList();
                    }
                }
            }
        }
    }
}
