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
    public partial class FormEstatisticas : Form
    {
        public FormEstatisticas()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        // Botão "Voltar" para fechar o formulário de estatísticas e retornar ao menu principal
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
