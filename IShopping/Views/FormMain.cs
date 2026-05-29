using System;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // limpa os dados da sessão e volta para o form de login
            MessageBox.Show("Logout com sucesso", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        // BOTÃO PARA DIRECIONAR PARA O FORM GERIR UTILIZADORES
        private void btnGerirUtilizadores_Click(object sender, EventArgs e)
        {
            FormGerirUtilizadores form = new FormGerirUtilizadores(); // cria uma nova instância do form de gestão de utilizadores

            form.Show(); // mostra o form de gestão de utilizadores

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormTipoArtigo form = new FormTipoArtigo(); 

            form.Show();
        }

        private void btnOrcamentos_Click(object sender, EventArgs e)
        {
            FormOrcamento form = new FormOrcamento();

            form.Show();
        }

        private void btnArtigo_Click(object sender, EventArgs e)
        {
            FormGestaoArtigo form = new FormGestaoArtigo();

            form.Show();
        }
    }
}
