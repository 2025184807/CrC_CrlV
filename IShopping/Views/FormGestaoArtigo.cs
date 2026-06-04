using IShopping.Controller;
using IShopping.Models;
using System;
using System.Data;
using System.Data.Entity; // Necessário para usar o .Include()
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormGestaoArtigo : Form
    {
        public FormGestaoArtigo()
        {
            InitializeComponent();

            CarregarTipos(); // Carrega os tipos de artigo no combo box
            CarregarGrid(); // Carrega os artigos na data grid view
        }

        private void FormGestaoArtigo_Load(object sender, EventArgs e)
        {
            CarregarComboTipoArtigo(); // Carrega os tipos de artigo no combo box
        }

        // CARREGAR TIPOS-- Carrega os tipos de artigo no combo box
        private void CarregarTipos()
        {
            cmbTipoArtigo.DataSource = TipoArtigoController.Listar(); // Define a lista de tipos de artigo como fonte de dados do combo box

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

        // Carregar data grid view mostrando o Nome do Tipo de Artigo
        public void CarregarGrid()
        {
            using (shoppingContext db = new shoppingContext())
            {
                dataGridView1.DataSource = null;

                // Usamos o .Select para criar as colunas explicitamente, incluindo o texto do tipo de artigo
                dataGridView1.DataSource = db.Artigos
                    .Include(a => a.TipoArtigo)
                    .Select(a => new
                    {
                        Id = a.Id,
                        Nome = a.Nome,
                        Preço = a.Preco,
                        TipoArtigo = a.TipoArtigo.Nome // Vai buscar o texto do tipo da outra tabela
                    })
                    .ToList();
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

            // Verificar se já existe um artigo com o mesmo nome (ignorar maiúsculas/minúsculas)
            using (shoppingContext db = new shoppingContext())
            {
                bool existe = db.Artigos
                    .Any(a => a.Nome.ToLower() == txtNome.Text.ToLower());

                if (existe)
                {
                    MessageBox.Show("Já existe um artigo com esse nome.");
                    return;
                }
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

        // Botão para editar um artigo existente
        private void button1_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("Seleciona um artigo.");
                return;
            }

            // Verificar se já existe um artigo com o mesmo nome (ignorar maiúsculas/minúsculas)
            using (shoppingContext db = new shoppingContext())
            {
                bool existe = db.Artigos
                    .Any(a => a.Nome.ToLower() == txtNome.Text.ToLower());

                if (existe)
                {
                    MessageBox.Show("Já existe um artigo com esse nome.");
                    return;
                }
            }

            decimal preco;// Variável para armazenar o preço convertido
            // Verificar se o preço é um número válido
            if (!decimal.TryParse(txtPreco.Text, out preco))
            {
                MessageBox.Show("Preço inválido.");
                return;
            }

            ArtigoController.Editar(
                id,
                txtNome.Text,
                (int)cmbTipoArtigo.SelectedValue,
                preco
            );

            MessageBox.Show("Artigo editado!");

            CarregarGrid();
            LimparCampos();
        }

        // Carregar os tipos de artigo no combo box
        private void CarregarComboTipoArtigo()
        {
            using (shoppingContext db = new shoppingContext())
            {
                cbTipoArtigo.DataSource = db.TipoArtigos.ToList();
                cbTipoArtigo.DisplayMember = "Nome";
                cbTipoArtigo.ValueMember = "Id";
                cbTipoArtigo.SelectedIndex = -1;
            }
        }

        //Ver os artigos selecionados na data grid view
        private void btnVer_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            Artigo artigo = ArtigoController.ProcurarPorId(id);

            if (artigo != null)
            {
                txtNome.Text = artigo.Nome;
                txtPreco.Text = artigo.Preco.ToString();
                cmbTipoArtigo.SelectedValue = artigo.TipoArtigoId;
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

        // Filtrar a Grid pela ComboBox selecionada mostrando o Nome do Tipo
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Se o checkbox "Todos" não estiver marcado e houver um tipo selecionado no combo box, filtra os artigos por esse tipo
                if (!cbxTodos.Checked &&
                    cbTipoArtigo.SelectedValue != null &&
                    cbTipoArtigo.SelectedValue is int)
                {
                    int tipoId = (int)cbTipoArtigo.SelectedValue; // Pega o ID do tipo selecionado no combo box e filtra os artigos por esse tipo, para mostrar o nome do tipo na grid e converte o valor selecionado para int

                    dataGridView1.DataSource = db.Artigos // Filtra os artigos pelo tipo selecionado e mostra o nome do tipo na grid
                        .Include(a => a.TipoArtigo) // O .Include é necessário para carregar os dados do tipo de artigo junto com os artigos, para que possamos acessar o nome do tipo na projeção
                        .Where(a => a.TipoArtigoId == tipoId) //O .Where filtra os artigos pelo tipo selecionado, usando o ID do tipo para comparar com o TipoArtigoId de cada artigo, e o .Include é necessário para carregar os dados do tipo de artigo junto com os artigos, para que possamos acessar o nome do tipo na projeção
                        .Select(a => new // O .Select é necessário para criar as colunas explicitamente, incluindo o texto do tipo de artigo
                        {
                            Id = a.Id,
                            Nome = a.Nome,
                            Preço = a.Preco,
                            TipoArtigo = a.TipoArtigo.Nome
                        })
                        .ToList(); // O .ToList() é necessário para executar a consulta e obter os resultados como uma lista, que pode ser atribuída ao DataSource da grid
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        // Filtro do CheckBox Atualizado com Projeção do Nome do Tipo
        private void cbxTodos_CheckedChanged(object sender, EventArgs e)
        {
            using (shoppingContext db = new shoppingContext())
            {
                if (cbxTodos.Checked)
                {
                    dataGridView1.DataSource = db.Artigos
                        .Include(a => a.TipoArtigo)
                        .Select(a => new
                        {
                            Id = a.Id,
                            Nome = a.Nome,
                            Preço = a.Preco,
                            TipoArtigo = a.TipoArtigo.Nome
                        })
                        .ToList();

                    cbTipoArtigo.Enabled = false;
                }
                else
                {
                    cbTipoArtigo.Enabled = true;
                    if (cbTipoArtigo.SelectedValue != null && cbTipoArtigo.SelectedValue is int)
                    {
                        int tipoId = (int)cbTipoArtigo.SelectedValue;

                        dataGridView1.DataSource = db.Artigos
                            .Include(a => a.TipoArtigo)
                            .Where(a => a.TipoArtigoId == tipoId)
                            .Select(a => new
                            {
                                Id = a.Id,
                                Nome = a.Nome,
                                Preço = a.Preco,
                                TipoArtigo = a.TipoArtigo.Nome
                            })
                            .ToList();
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}