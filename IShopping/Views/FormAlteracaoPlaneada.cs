using IShopping.Controller;
using IShopping.Models;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormAlteracaoPlaneada : Form
    {
        private int compraId = 0; // Se for 0, é uma nova compra. Caso contrário, está em modo de edição.

        public FormAlteracaoPlaneada(int id)
        {
            InitializeComponent();
            compraId = id;
        }

        private void FormAlteracaoPlaneada_Load(object sender, EventArgs e)
        {
            // Configura o DateTimePicker para mostrar apenas mês e ano
            dateCompra.Format = DateTimePickerFormat.Custom;
            dateCompra.CustomFormat = "MM/yyyy";

            // Inicializa os dados das ComboBoxes
            CarregarTiposArtigo();
            CarregarComboArtigo();

            // Determina o fluxo inicial se for Nova Compra ou Edição
            if (compraId > 0)
            {
                CarregarCompra();
            }
            else
            {
                txtEstado.Text = "Aberta";
                txtId.Text = "Novo";
                txtOrcamento.Text = "0.00 €";
                txtTotal.Text = "0.00 €";
                txtDisponivel.Text = "0.00 €";
            }

            // Atualiza dinamicamente o estado dos orçamentos na interface
            AtualizarResumoOrcamento();
        }

        // ================= ORÇAMENTO & RESUMOS FINANCEIROS =================

        private decimal CalcularTotalPrevisto()
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.ItemComprasPlaneadas
                    .Where(i => i.CompraPlaneadaId == compraId)
                    .Sum(i => (decimal?)i.QuantidadePrevista * (i.PrecoUnitario ?? 0)) ?? 0;
            }
        }

        private void AtualizarResumoOrcamento()
        {
            int mes = dateCompra.Value.Month;
            int ano = dateCompra.Value.Year;
            var orcamento = OrcamentoController.ObterPorMesAno(mes, ano);

            decimal valorOrcamento = orcamento?.ValorOrcamento ?? 0;
            decimal totalPrevisto = CalcularTotalPrevisto();
            decimal disponivel = valorOrcamento - totalPrevisto;

            // Atualiza os campos de texto do ecrã
            txtOrcamento.Text = valorOrcamento.ToString("0.00") + " €";
            txtTotal.Text = totalPrevisto.ToString("0.00") + " €";
            txtDisponivel.Text = disponivel.ToString("0.00") + " €";

            // Gestão de cores e alertas visuais do orçamento
            if (orcamento == null)
            {
                lblAviso.Visible = true;
                lblAviso.Text = "Não existe orçamento para este mês.";
                lblAviso.ForeColor = Color.Red;
            }
            else if (totalPrevisto > valorOrcamento)
            {
                lblAviso.Visible = true;
                lblAviso.Text = "O valor previsto ultrapassa o orçamento.";
                lblAviso.ForeColor = Color.DarkOrange;
            }
            else
            {
                lblAviso.Visible = false;
            }
        }

        // ================= GESTÃO DE ARTIGOS (COMBOBOXES) =================

        private void CarregarTiposArtigo()
        {
            var tipos = TipoArtigoController.Listar();

            // Desvincula o evento temporariamente para evitar erros de index nulo ao carregar a lista
            cmbTipoArtigo.SelectedIndexChanged -= cmbTipoArtigo_SelectedIndexChanged;

            cmbTipoArtigo.DataSource = null;
            cmbTipoArtigo.DataSource = tipos;
            cmbTipoArtigo.DisplayMember = "Nome";
            cmbTipoArtigo.ValueMember = "Id";
            cmbTipoArtigo.SelectedIndex = -1;

            cmbTipoArtigo.SelectedIndexChanged += cmbTipoArtigo_SelectedIndexChanged;
        }

        private void CarregarComboArtigo()
        {
            using (shoppingContext db = new shoppingContext())
            {
                cmbArtigo.DataSource = null;
                cmbArtigo.DataSource = db.Artigos.OrderBy(a => a.Nome).ToList();
                cmbArtigo.DisplayMember = "Nome";
                cmbArtigo.ValueMember = "Id";
                cmbArtigo.SelectedIndex = -1;
            }
        }

        private void cmbTipoArtigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoArtigo.SelectedValue == null) return;

            if (!int.TryParse(cmbTipoArtigo.SelectedValue.ToString(), out int tipoId))
                return;

            using (shoppingContext db = new shoppingContext())
            {
                var artigosFiltrados = db.Artigos
                    .Where(a => a.TipoArtigoId == tipoId)
                    .OrderBy(a => a.Nome)
                    .ToList();

                cmbArtigo.DataSource = null;
                cmbArtigo.DataSource = artigosFiltrados;
                cmbArtigo.DisplayMember = "Nome";
                cmbArtigo.ValueMember = "Id";
                cmbArtigo.SelectedIndex = -1;
            }
        }

        // ================= DADOS DA COMPRA MÃE =================

        private void CarregarCompra()
        {
            using (shoppingContext db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.Find(compraId);
                if (compra == null) return;

                txtId.Text = compra.Id.ToString();
                txtNome.Text = compra.NomeCompra;
                dateCompra.Value = compra.DataCompra;

                // Troca da Checkbox pelo conteúdo de Texto do txtEstado
                txtEstado.Text = compra.Fechada ? "Fechada" : "Aberta";

                if (compra.Fechada)
                    BloquearEdicao();

                CarregarItens();
            }
        }

        private void BloquearEdicao()
        {
            txtNome.ReadOnly = true;
            dateCompra.Enabled = false;
            txtEstado.ReadOnly = true; // Bloqueia também o campo de texto do estado

            btnGuardar.Enabled = false;
            btnAdicionar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e) // Botão Guardar Compra
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Por favor, dê um nome válido a esta compra.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensagem;
            bool ok;

            // Define se a compra vai ser gravada como fechada avaliando o texto digitado/selecionado
            bool fecharCompra = txtEstado.Text.Trim().Equals("Fechada", StringComparison.OrdinalIgnoreCase);

            if (compraId == 0)
            {
                ok = AlteracaoPlaneadaController.CriarCompra(txtNome.Text, out mensagem);

                if (ok)
                {
                    using (shoppingContext db = new shoppingContext())
                    {
                        var nova = db.ComprasPlaneadas.OrderByDescending(c => c.Id).FirstOrDefault();
                        if (nova != null)
                        {
                            compraId = nova.Id;
                            txtId.Text = compraId.ToString();
                            txtEstado.Text = fecharCompra ? "Fechada" : "Aberta";

                            if (fecharCompra)
                                BloquearEdicao();
                        }
                    }
                }
            }
            else
            {
                ok = AlteracaoPlaneadaController.AlterarCompra(
                    compraId,
                    txtNome.Text,
                    dateCompra.Value,
                    fecharCompra, // Passa o booleano interpretado a partir do txtEstado
                    out mensagem
                );

                if (ok)
                {
                    txtEstado.Text = fecharCompra ? "Fechada" : "Aberta";
                    if (fecharCompra)
                        BloquearEdicao();
                }
            }

            MessageBox.Show(mensagem, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AtualizarResumoOrcamento();
        }

        // ================= OPERAÇÕES DOS ITENS DA COMPRA =================

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (compraId == 0)
            {
                MessageBox.Show("Guarde a informação principal da compra antes de lhe adicionar artigos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbArtigo.SelectedValue == null)
            {
                MessageBox.Show("Selecione um artigo da lista para submeter.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int artigoId = Convert.ToInt32(cmbArtigo.SelectedValue);
            int qtd = (int)numQuantidade.Value;

            if (qtd <= 0)
            {
                MessageBox.Show("Insira uma quantidade prevista válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensagem;
            bool ok = AlteracaoPlaneadaController.AdicionarItemPrevisto(compraId, artigoId, qtd, out mensagem);

            MessageBox.Show(mensagem, "Artigo", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (ok)
            {
                CarregarItens();
                LimparDados();
                AtualizarResumoOrcamento();
            }
        }

        private void CarregarItens()
        {
            using (shoppingContext db = new shoppingContext())
            {
                dataGridView1.DataSource = db.ItemComprasPlaneadas
                    .Where(i => i.CompraPlaneadaId == compraId)
                    .Select(i => new
                    {
                        i.Id,
                        Artigo = i.Artigos.Nome,
                        TipoArtigo = i.Artigos.TipoArtigo.Nome,
                        i.QuantidadePrevista,
                        PrecoUnitario = i.PrecoUnitario ?? 0,
                        Total = i.QuantidadePrevista * (i.PrecoUnitario ?? 0)
                    })
                    .ToList();
            }

            if (dataGridView1.Columns.Contains("Id"))
                dataGridView1.Columns["Id"].Visible = false;
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecione um registo na tabela para inspecionar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idItem = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            var item = AlteracaoPlaneadaController.ProcurarItemPorId(idItem);

            if (item == null)
            {
                MessageBox.Show("O artigo selecionado já não existe no sistema.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            numQuantidade.Value = item.QuantidadePrevista;

            using (shoppingContext db = new shoppingContext())
            {
                var artigo = db.Artigos.Find(item.ArtigoId);
                if (artigo != null)
                {
                    cmbTipoArtigo.SelectedValue = artigo.TipoArtigoId;
                    cmbArtigo.SelectedValue = artigo.Id;
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecione um artigo na grelha para atualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int novaQtd = (int)numQuantidade.Value;

            if (novaQtd <= 0)
            {
                MessageBox.Show("A quantidade do item tem de ser maior que zero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idItem = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            AlteracaoPlaneadaController.AlterarItem(idItem, novaQtd);

            MessageBox.Show("Item alterado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimparDados();
            CarregarItens();
            AtualizarResumoOrcamento();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecione o artigo que pretende remover da lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Tem a certeza que deseja retirar este item da compra planeada?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int idItem = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            AlteracaoPlaneadaController.EliminarItem(idItem);

            CarregarItens();
            AtualizarResumoOrcamento();
        }

        // ================= CONTROLOS E UTILITÁRIOS =================

        private void LimparDados()
        {
            numQuantidade.Value = 0;
            cmbArtigo.SelectedIndex = -1;
            cmbTipoArtigo.SelectedIndex = -1;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparDados();
        }

        private void dateCompra_ValueChanged(object sender, EventArgs e)
        {
            AtualizarResumoOrcamento();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numQuantidade_ValueChanged(object sender, EventArgs e) { }
        private void cmbArtigo_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}