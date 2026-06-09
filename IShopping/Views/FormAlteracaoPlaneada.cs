using IShopping.Controller;
using IShopping.Models;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormAlteracaoPlaneada : Form
    {
        private int compraId = 0; // Se for 0, é uma nova compra. Caso contrário, está em modo de edição.
        // O construtor recebe o ID da compra a editar, ou 0 para criar uma nova compra. 

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
                txtIdItem.Text = "Novo";
                txtOrcamento.Text = "0.00 €";
                txtTotal.Text = "0.00 €";
                txtDisponivel.Text = "0.00 €";
            }

            // Atualiza dinamicamente o estado dos orçamentos na interface
            AtualizarResumoOrcamento();
        }

        // ================= ORÇAMENTO & RESUMOS FINANCEIROS =================

        // Calcula o total previsto para a compra somando o valor de cada item (quantidade prevista * preço unitário), garantindo que valores nulos são tratados como zero
        private decimal CalcularTotalPrevisto()
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.ItemComprasPlaneadas
                    .Where(i => i.CompraPlaneadaId == compraId)
                    .Sum(i => (decimal?)i.QuantidadePrevista * (i.PrecoUnitario ?? 0)) ?? 0;
            }
        }

        // Atualiza os campos de resumo financeiro (Orçamento, Total Previsto, Disponível) com base na data da compra e nos itens associados, e exibe alertas visuais se necessário
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

        // ================= GESTÃO DE ARTIGOS  =================
        // Carrega os tipos de artigo na ComboBox, garantindo que o evento de seleção é gerido corretamente para evitar erros de index nulo
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

        // Carrega todos os artigos na ComboBox de Artigos, ordenados por nome, e prepara a interface para seleção
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

        // Filtra os artigos disponíveis na ComboBox de Artigos com base no Tipo de Artigo selecionado, garantindo que apenas os artigos relevantes são mostrados
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

        // ================= DADOS DA COMPRA =================

        // Carrega os dados da compra para edição, incluindo o estado (Fechada/Aberta) e bloqueia a edição se estiver fechada
        private void CarregarCompra()
        {
            using (shoppingContext db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.Find(compraId);
                if (compra == null) return;

                txtIdItem.Text = compra.Id.ToString();
                txtNome.Text = compra.NomeCompra;
                dateCompra.Value = compra.DataCompra;

                // Troca da Checkbox pelo conteúdo de Texto do txtEstado
                txtEstado.Text = compra.Fechada ? "Fechada" : "Aberta";

                if (compra.Fechada)
                    BloquearEdicao();

                CarregarItens();
            }
        }

        // Método para bloquear a edição dos campos principais da compra quando a compra estiver fechada
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

        // Botão Guardar Compra, está button1 pois cliquei no designer para criar o evento e não renomeei o botão, mas é o botão de guardar a compra.
        private void button1_Click(object sender, EventArgs e) 
        {
          // Botão Guardar Compra

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
                // CORREÇÃO: Certifica-te que o teu Controller aceita a data. 
                // Se não aceitar, altera a assinatura do método CriarCompra no Controller para incluir o DateTime.
                ok = AlteracaoPlaneadaController.CriarCompra(txtNome.Text, dateCompra.Value, out mensagem);

                if (ok)
                {
                    using (shoppingContext db = new shoppingContext())
                    {
                        var nova = db.ComprasPlaneadas.OrderByDescending(c => c.Id).FirstOrDefault();
                        if (nova != null)
                        {
                            compraId = nova.Id;
                            txtIdItem.Text = compraId.ToString();
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
                    dateCompra.Value, // Passa a data alterada
                    fecharCompra,
                    out mensagem
                );

                if (ok)
                {
                    txtEstado.Text = fecharCompra ? "Fechada" : "Aberta";
                    if (fecharCompra)
                        BloquearEdicao();
                }
            }

            MessageBox.Show(mensagem, "Sucesso", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            // Força a atualização do resumo com a nova data gravada
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

            // Mantemos a coluna visível caso precises de confirmar visualmente o ID do Item na tabela
            if (dataGridView1.Columns.Contains("Id"))
                dataGridView1.Columns["Id"].Visible = true;
        }

        // PROCURAR / VER ITEM
        private void btnVer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdItem.Text) || !int.TryParse(txtIdItem.Text, out int idItem))
            {
                MessageBox.Show("Por favor, introduza um ID de Item numérico válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdItem.Focus();
                return;
            }

            var item = AlteracaoPlaneadaController.ProcurarItemPorId(idItem);

            if (item == null || item.CompraPlaneadaId != compraId)
            {
                MessageBox.Show("O ID do item especificado não pertence a esta compra ou não existe.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

         // ELIMINAR ITEM
        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtIdItem.Text) || !int.TryParse(txtIdItem.Text, out int idItem))
            {
                MessageBox.Show("Por favor, introduza um ID de Item numérico válido para remover.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdItem.Focus();
                return;
            }

            if (MessageBox.Show("Tem a certeza que deseja retirar este item da compra planeada?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string mensagem;
            // Chama o controlador passando a variável de saída
            AlteracaoPlaneadaController.EliminarItem(idItem, out mensagem);

            MessageBox.Show(mensagem, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimparDados();
            CarregarItens();
            AtualizarResumoOrcamento();
        }
 

        // ALTERADO: Edita o item com base no ID 
        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtIdItem.Text) || !int.TryParse(txtIdItem.Text, out int idItem))
            {
                MessageBox.Show("Por favor, introduza um ID de Item numérico válido para atualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdItem.Focus(); // Foca o campo de ID do item para correção
                return;
            }

            int novaQtd = (int)numQuantidade.Value;

            if (novaQtd <= 0)
            {
                MessageBox.Show("A quantidade do item tem de ser maior que zero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Executa a alteração no controlador usando o ID do campo de texto
            AlteracaoPlaneadaController.AlterarItem(idItem, novaQtd, out string mensagem);

            MessageBox.Show(mensagem);

            LimparDados();
            CarregarItens();
            AtualizarResumoOrcamento();
        }


        // ================= CONTROLOS E UTILITÁRIOS =================

        private void LimparDados()
        {
            txtIdItem.Clear(); // Limpa também o campo de pesquisa por ID do item
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