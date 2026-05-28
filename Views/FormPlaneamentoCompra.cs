using IShopping.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IShopping.Forms
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormAlteracaoPlaneada formAlteracaoPlaneada = new FormAlteracaoPlaneada(); // Cria uma nova instância do FormAlteracaoPlaneada
            formAlteracaoPlaneada.ShowDialog(); // Exibe o FormAlteracaoPlaneada como uma janela modal
            // O código após ShowDialog() será executado somente após o FormAlteracaoPlaneada ser fechado
        }
    }
}
