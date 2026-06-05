using IShopping.Controller;
using IShopping.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormAlteracaoPlaneada : Form
    {
        private int compraId = 0; // 0 = Nova Compra, > 0 = Editar

        public FormAlteracaoPlaneada(int id)
        {
            InitializeComponent();
            compraId = id;
        }

        private void FormAlteracaoPlaneada_Load(object sender, EventArgs e)
        {
            // 1. Carrega os tipos de artigo primeiro
            CarregarTiposArtigo();
            CarregarComboArtigo();

            // 2. Se for uma edição, carrega os dados da compra e os respetivos itens
            if (compraId > 0)
            {
                CarregarCompra();
            }
        }

        private void CarregarTiposArtigo()
        {
            var tipos = TipoArtigoController.Listar();

            if (tipos != null)
            {
                cmbTipoArtigo.DataSource = null;
                cmbTipoArtigo.DisplayMember = "Nome";
                cmbTipoArtigo.ValueMember = "Id";
                cmbTipoArtigo.DataSource = tipos;
            }
            cmbTipoArtigo.SelectedIndex = -1;
        }

        // Carregar os tipos de artigo no combo box
        private void CarregarComboArtigo()
        {
            using (shoppingContext db = new shoppingContext())
            {
                cmbArtigo.DataSource = db.Artigos.ToList();
                cmbArtigo.DisplayMember = "Nome";
                cmbArtigo.ValueMember = "Id";
                cmbArtigo.SelectedIndex = -1;
            }
        }

        private void cmbTipoArtigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Valida se existe uma seleção real
            if (cmbTipoArtigo.SelectedValue == null)
            {
                cmbArtigo.DataSource = null;
                return;
            }

            // Converte o valor selecionado de forma segura para inteiro
            string valorTexto = cmbTipoArtigo.SelectedValue.ToString();
            if (!int.TryParse(valorTexto, out int tipoId))
            {
                cmbArtigo.DataSource = null;
                return;
            }

            // Filtra os artigos com base no Tipo de Artigo selecionado
            using (shoppingContext db = new shoppingContext())
            {
                var artigosFiltrados = db.Artigos.Where(a => a.TipoArtigoId == tipoId).ToList();

                cmbArtigo.DataSource = null;

                if (artigosFiltrados != null && artigosFiltrados.Count > 0)
                {
                    cmbArtigo.DisplayMember = "Nome";

                    // A classe Artigo do teu stor!
                    cmbArtigo.ValueMember = "Id";

                    cmbArtigo.DataSource = artigosFiltrados;
                }

                cmbArtigo.SelectedIndex = -1;
            }
        }

        private void CarregarCompra()
        {
            using (shoppingContext db = new shoppingContext())
            {
                CompraPlaneada compra = db.ComprasPlaneadas.Find(compraId);

                if (compra == null) return;

                txtId.Text = compra.Id.ToString();
                txtNome.Text = compra.NomeCompra;
                dateCompra.Value = compra.DataCompra;
                ckbFechar.Checked = compra.Fechada;

                if (compra.Fechada)
                {
                    BloquearEdicao();
                }

                CarregarItens();
            }
        }

        private void BloquearEdicao()
        {
            txtNome.ReadOnly = true;
            dateCompra.Enabled = false;
            ckbFechar.Enabled = false;
            btnGuardar.Enabled = false;
            btnAdicionar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        // BOTÃO GUARDAR COMPRA (Atualizado para usar a arquitetura correta do teu Controller)
        private void button1_Click(object sender, EventArgs e)
        {
            string message;
            bool ok;

            if (compraId == 0)
            {
                // Usa o método de criação do teu AlteracaoPlaneadaController
                ok = AlteracaoPlaneadaController.CriarCompra(txtNome.Text, out message);

                if (ok)
                {
                    using (shoppingContext db = new shoppingContext())
                    {
                        var novaCompra = db.ComprasPlaneadas
                                           .Where(c => c.CriadoPor == sessao.UtilizadorAtual)
                                           .OrderByDescending(c => c.Id)
                                           .FirstOrDefault();

                        if (novaCompra != null)
                        {
                            compraId = novaCompra.Id;
                            txtId.Text = compraId.ToString();
                        }
                    }
                }
            }
            else
            {
                // Usa o método de alteração do teu AlteracaoPlaneadaController, 
                // limpando a lógica de BD de dentro do Form!
                ok = AlteracaoPlaneadaController.AlterarCompra(
                    compraId,
                    txtNome.Text,
                    dateCompra.Value,
                    ckbFechar.Checked,
                    out message
                );

                if (ok && ckbFechar.Checked)
                {
                    BloquearEdicao();
                }
            }

            MessageBox.Show(message, "Informação", MessageBoxButtons.OK);
        }

        // BOTÃO ADICIONAR ITEM (Atualizado para apontar para o controlador correto)
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (compraId == 0)
            {
                MessageBox.Show("Por favor, grave primeiro o cabeçalho da compra antes de adicionar itens.");
                return;
            }

            if (cmbArtigo.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecione um artigo válido.");
                return;
            }

            int artigoId = Convert.ToInt32(cmbArtigo.SelectedValue);
            int qtd = (int)numQuantidade.Value;

            if (qtd <= 0)
            {
                MessageBox.Show("A quantidade deve ser superior a zero.");
                return;
            }

            string mensagem;

            AlteracaoPlaneadaController.AdicionarItemPrevisto(
                compraId,
                artigoId,
                qtd,
                out mensagem);

            MessageBox.Show("Artigo Adicionado com sucesso!"); // Exibe uma mensagem de sucesso após a edição do tipo de artigo.


            CarregarItens();
            LimparDados();

        }

        private void LimparDados()
        {
            numQuantidade.Value = 0;
            cmbArtigo.SelectedIndex = -1;
            cmbTipoArtigo.SelectedIndex = -1;
        }

        // Método para carregar os itens previstos da compra e exibi-los na DataGridView
        private void CarregarItens()
        {
            if (compraId <= 0) return;

            using (shoppingContext db = new shoppingContext())
            {
                var itens = db.ItemComprasPlaneadas
                    .Where(i => i.CompraPlaneadaId == compraId)
                    .Select(i => new
                    {
                        i.Id,
                        Artigo = i.Artigos.Nome,
                        i.QuantidadePrevista,
                        i.QuantidadeAdquirida,
                        i.PrecoUnitario,
                        i.Observacoes
                    })
                    .ToList();

                dataGridView1.DataSource = null;
                if (itens != null)
                {
                    dataGridView1.DataSource = itens;
                }
            }

            if (dataGridView1.Columns.Contains("Id"))
                dataGridView1.Columns["Id"].Visible = false;
        }

        // BOTÃO VOLTAR
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ckbFechar_CheckedChanged(object sender, EventArgs e)
        {
            txtNome.ReadOnly = ckbFechar.Checked;
            cmbTipoArtigo.Enabled = !ckbFechar.Checked;
            cmbArtigo.Enabled = !ckbFechar.Checked;
            numQuantidade.ReadOnly = ckbFechar.Checked;
            btnAdicionar.Enabled = !ckbFechar.Checked;
            btnEditar.Enabled = !ckbFechar.Checked;
            btnEliminar.Enabled = !ckbFechar.Checked;
        }

        private void cmbArtigo_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // BOTÃO VER ITEM
        private void btnVer_Click(object sender, EventArgs e)
        {


            int id;

            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            ItemCompraPlaneada item =
                AlteracaoPlaneadaController.ProcurarItemPorId(id);

            if (item == null)
            {
                MessageBox.Show("Item não encontrado.");
                return;
            }
            numQuantidade.Text =
            item.QuantidadePrevista.ToString();

            cmbArtigo.SelectedValue =
                item.ArtigoId;
            using (shoppingContext db = new shoppingContext())
            {
                int tipoId = db.Artigos
                               .Where(a => a.Id == item.ArtigoId)
                               .Select(a => a.TipoArtigoId)
                               .FirstOrDefault();

                cmbTipoArtigo.SelectedValue = tipoId;
                cmbArtigo.SelectedValue = item.ArtigoId;
            }

        }

        // BOTÃO EDITAR ITEM
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecione um item na tabela para editar.", "Aviso", MessageBoxButtons.OK);
                return;
            }

            // Valida se a TextBox da quantidade tem um número válido antes de mandar alterar
            if (!int.TryParse(numQuantidade.Text, out int novaQtd) || novaQtd <= 0)
            {
                MessageBox.Show("Por favor, introduza uma quantidade válida e superior a zero na caixa de texto.", "Aviso", MessageBoxButtons.OK);
                numQuantidade.Focus();
                return;
            }

            int idItem = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

            // Executa a alteração no controlador
            AlteracaoPlaneadaController.AlterarItem(idItem, novaQtd);

            MessageBox.Show("Item alterado com sucesso.", "Resultado", MessageBoxButtons.OK);

            // Limpa a caixa e atualiza a grelha e as combos
            LimparDados();
            CarregarItens();
        }

        // BOTÃO ELIMINAR ITEM
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecione um item na tabela para eliminar.", "Aviso", MessageBoxButtons.OK);
                return;
            }

            int idItem = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value); // Obtém o ID do item selecionado na DataGridView

            // Executa a eliminação no controlador
            AlteracaoPlaneadaController.EliminarItem(idItem);

            MessageBox.Show("Item eliminado com sucesso.", "Resultado", MessageBoxButtons.OK);

            // Atualiza a grelha para fazer desaparecer o item apagado
            CarregarItens();
        }

        private void txtQuantidade_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}