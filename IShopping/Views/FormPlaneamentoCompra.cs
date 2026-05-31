using IShopping.Views;
using IShopping.Controller;
using IShopping.Models;
using System;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormPlaneamentoCompra : Form
    {
        public FormPlaneamentoCompra()
        {
            InitializeComponent();
        }
        

        private void bntAlterarCompra_Click(object sender, EventArgs e)
        {
            FormAlteracaoPlaneada formAlteracaoPlaneada = new FormAlteracaoPlaneada(); // Cria uma nova instância do FormAlteracaoPlaneada
            formAlteracaoPlaneada.ShowDialog(); // Exibe o FormAlteracaoPlaneada como uma janela modal
            // O código após ShowDialog() será executado somente após o FormAlteracaoPlaneada ser fechado
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AparecerOrçamento_Click(object sender, EventArgs e)
        {

        }
    }
}
