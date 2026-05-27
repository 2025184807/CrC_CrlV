using IShopping.Controller;
using System.Windows.Forms;

namespace IShopping
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        private void bntSair_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void bntEntrar_Click(object sender, System.EventArgs e)
        {
            string mensagem;
            bool ok = Form2Controller.Autenticar(textBox1Username.Text.Trim(), textBox1Password.Text, out mensagem);
            MessageBox.Show(mensagem);
            if (ok)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void FormLogin_Load(object sender, System.EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }
    }
}
