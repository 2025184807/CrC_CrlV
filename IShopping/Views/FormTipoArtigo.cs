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

        private void FormTipoArtigo_Load(object sender, EventArgs e)
        {

        }

        // LIMPAR CAMPOS
        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
        }
        private void CarregarGrid()
        {
            using (shoppingContext db = new shoppingContext())
            {
                dataGridView1.DataSource = null;

                dataGridView1.DataSource = TipoArtigoController.Listar(); // Vai buscar todos os tipos de artigo da base de dados e mostra na data grid view.
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

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtNome.Clear();
        }

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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            int id;

            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("O ID tem de ser numérico.");
                return;
            }

            TipoArtigoController.Editar(id, txtNome.Text);

            MessageBox.Show("Tipo de artigo editado com sucesso!");
            CarregarGrid();

            LimparCampos();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id;

            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("O ID tem de ser numérico.");
                return;
            }

            TipoArtigoController.Eliminar(id);

            MessageBox.Show("Tipo de artigo eliminado com sucesso!");
            CarregarGrid();

            LimparCampos();
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
