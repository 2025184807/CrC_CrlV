using IShopping.Controller;
using IShopping.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormGestaoArtigo : Form
    {
        public FormGestaoArtigo()
        {
            InitializeComponent();

            CarregarTipos(); // Carrega os tipos de artigo no combo box
            CarregarGrid();
        }

        private void FormGestaoArtigo_Load(object sender, EventArgs e)
        {

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

            ArtigoController.Editar(
                id,
                txtNome.Text,
                (int)cmbTipoArtigo.SelectedValue, // O (int) é necessário para converter o SelectedValue do combo box, que é do tipo object, para um inteiro, que é o tipo esperado pelo método Editar para o parâmetro tipoArtigoId.
                preco
            );

            MessageBox.Show("Artigo editado!");

            CarregarGrid();
            LimparCampos();
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
    }
}
