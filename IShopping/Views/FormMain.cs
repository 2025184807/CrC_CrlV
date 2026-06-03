using System;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormMain : Form
    {
        // Construtor do FormMain, responsável por inicializar os componentes do formulário
        public FormMain()
        {
            InitializeComponent(); // Inicializa os componentes do formulário, como botões, labels, etc.
        }

        // Ao clicar no botão de logout, impar os dados da sessão e voltar para o form de login
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // limpa os dados da sessão e volta para o form de login
            MessageBox.Show("Logout com sucesso", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information); // Exibe uma mensagem de sucesso para o usuário
            this.DialogResult = DialogResult.Retry; // Define o resultado do diálogo como Retry, indicando que o usuário deseja tentar novamente (voltar para o form de login)
            this.Close(); // Fecha o formulário atual (FormMain)
        }

        // BOTÃO PARA DIRECIONAR PARA O FORM GERIR UTILIZADORES
        private void btnGerirUtilizadores_Click(object sender, EventArgs e)
        {
            FormGerirUtilizadores form = new FormGerirUtilizadores(); // cria uma nova instância do form de gestão de utilizadores

            form.Show(); // mostra o form de gestão de utilizadores

        }
        // BOTÃO PARA DIRECIONAR PARA O FORM PLANEAMENTO DE COMPRA
        private void btnTipoArtigo_Click(object sender, EventArgs e)
        {
            FormTipoArtigo form = new FormTipoArtigo();  // cria uma nova instância do form de gestão de tipos de artigo

            form.Show(); // mostra o form de gestão de tipos de artigo
        }

        // Botâo para direcionar para o form de gestão de fornecedores
        private void btnOrcamentos_Click(object sender, EventArgs e)
        {
            FormOrcamento form = new FormOrcamento(); // cria uma nova instância do form de gestão de orçamentos

            form.Show();
        }

        // Botão para direcionar para o form de gestão de artigos
        private void btnArtigo_Click(object sender, EventArgs e)
        {
            FormGestaoArtigo form = new FormGestaoArtigo(); // cria uma nova instância do form de gestão de artigos

            form.Show(); // mostra o form de gestão de artigos
        }

        private void PlaneamentoCompra_Click(object sender, EventArgs e)
        {

        }

        private void btnPlaneamentoCompra_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            form.Show();
        }

        private void bntPlaneamentoCompra_Click(object sender, EventArgs e)
        {
            FormPlaneamentoCompra form = new FormPlaneamentoCompra();
            form.Show();
        }
    }
}
