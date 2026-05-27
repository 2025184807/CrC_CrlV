using IShopping.Controller;
using IShopping.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormGerirUtilizadores : Form
    {
        public FormGerirUtilizadores()
        {
            InitializeComponent();

            CarregarUtilizadores();
        }

        // BOTÃO GUARDAR - PARA ADICIONAR UM NOVO UTILIZADOR
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            UtilizadorController.Inserir(
                txtNome.Text,
                txtPassword.Text,
                sessao.UtilizadorAtual
            );

            MessageBox.Show("Utilizador adicionado!");

            CarregarUtilizadores();

            LimparCampos();
        }

        // LISTAR UTILIZADORES - PARA MOSTRAR NA DATA GRID VIEW com os utilizadores da base de dados.
        private void CarregarUtilizadores()
        {
            using (shoppingContext db = new shoppingContext())
            {
                dataGridView1.DataSource = null;

                dataGridView1.DataSource = UtilizadorController.Listar(); // Vai buscar todos os utilizadores da base de dados e mostra na data grid view.
            }
        }

        // LIMPAR CAMPOS
        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtPassword.Clear();
        }
        private void FormGerirUtilizadores_Load(object sender, EventArgs e)
        {

        }

        //Ver - PARA MOSTRAR OS DADOS DO UTILIZADOR SELECIONADO PELO ID NA TABELA NOS CAMPOS DE TEXTO
        private void btnVer_Click(object sender, EventArgs e)
        {
            int id;

            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            Utilizador utilizador = UtilizadorController.ProcurarPorId(id);

            if (utilizador != null)
            {
                txtNome.Text = utilizador.Username;
                txtPassword.Text = utilizador.Password;
            }
            else
            {
                MessageBox.Show("Utilizador não encontrado.");
            }
        }

        //Editar - PARA EDITAR OS DADOS DO UTILIZADOR SELECIONADO PELO ID NA TABELA E GUARDAR AS ALTERAÇÕES NA BASE DE DADOS
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
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

            UtilizadorController.Editar(
                    id,
                    txtNome.Text,
                    txtPassword.Text,
                    sessao.UtilizadorAtual
                );

            MessageBox.Show("Utilizador editado com sucesso!");
            CarregarUtilizadores();

            LimparCampos();
        }

        //Eliminar - PARA ELIMINAR O UTILIZADOR SELECIONADO PELO ID NA TABELA E GUARDAR AS ALTERAÇÕES NA BASE DE DADOS
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id;

            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("O ID tem de ser numérico.");
                return;
            }

            UtilizadorController.Eliminar(id);

            MessageBox.Show("Utilizador eliminado com sucesso!");
            CarregarUtilizadores();

            LimparCampos();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtNome.Clear();
            txtPassword.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
