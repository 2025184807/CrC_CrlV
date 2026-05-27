using IShopping.Controller;
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
        }

        private void FormGestaoArtigo_Load(object sender, EventArgs e)
        {

        }

        private void CarregarTipos()
        {
            cmbTipoArtigo.DataSource =
                TipoArtigoController.Listar();

            cmbTipoArtigo.DisplayMember = "Nome";

            cmbTipoArtigo.ValueMember = "Id";
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ArtigoController.Inserir(
               txtNome.Text,
               (int)cmbTipoArtigo.SelectedValue
           );

            MessageBox.Show("Artigo inserido!");

            //CarregarGrid();
        }
    }
}
