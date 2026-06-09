using IShopping.Controller; // Liga o formulário ao controlador que valida o login
using System.Windows.Forms; // Necessário para usar os componentes visuais do Windows Forms

namespace IShopping
{
    // Classe responsável pela janela de Login do sistema
    public partial class FormLogin : Form
    {
        // Construtor - Inicializa os componentes do ecrã
        public FormLogin()
        {
            InitializeComponent();
        }

        // Botão sair, Fecha a janela de login
        private void bntSair_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        // Botão entrar - Processa a tentativa de login
        private void bntEntrar_Click(object sender, System.EventArgs e)
        {
            string mensagem; // Guarda o texto de sucesso ou erro devolvido pelo controller

            // Envia o username e a password para o controller validar
            bool ok = Form2Controller.Autenticar(textBox1Username.Text.Trim(), textBox1Password.Text, out mensagem);

            MessageBox.Show(mensagem); // Mostra o resultado do login num aviso no ecrã

            // Se as credenciais estiverem corretas
            if (ok)
            {
                // Define o resultado como OK (útil para o FormPrincipal saber que o login deu certo)
                this.DialogResult = DialogResult.OK;
                this.Close(); // Fecha o ecrã de login para abrir o programa principal
            }
        }

        // Métodos automáticos do Windows Forms (cliquei sem querer!)
        private void FormLogin_Load(object sender, System.EventArgs e) { }
        private void textBox1_TextChanged(object sender, System.EventArgs e) { }
        private void textBox1Username_TextChanged(object sender, System.EventArgs e) { }
    }
}