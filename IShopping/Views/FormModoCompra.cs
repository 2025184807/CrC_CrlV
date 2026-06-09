using IShopping.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormModoCompra : Form
    {
        private int compraId;
        internal int CompraIdSelecionada;

        public FormModoCompra()
        {
            InitializeComponent();
        }

        private void FormModoCompra_Load(object sender, EventArgs e)
        {
            dateCompra.Format = DateTimePickerFormat.Custom;
            dateCompra.CustomFormat = "MM/yyyy";

            // Bloqueia a edição da data neste ecrã, permitindo apenas a visualização
            dateCompra.Enabled = false;

            CarregarCmbCompra();
            CarregarCmbTipoArtigo();
        }

        private void CarregarCmbCompra()
        {
            var compras = ModoCompraController.ObterComprasEmAberto();

            cmbNomeCompra.DataSource = compras;
            cmbNomeCompra.DisplayMember = "NomeCompra";
            cmbNomeCompra.ValueMember = "Id";
            cmbNomeCompra.SelectedIndex = -1;
        }

        private void CarregarCompraAberta()
        {
            using (shoppingContext db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.FirstOrDefault(c => c.Id == compraId);

                if (compra == null) return;

                cmbNomeCompra.Text = compra.NomeCompra;
                dateCompra.Value = compra.DataCompra;
                txtEstado.Text = compra.Fechada ? "Fechada" : "Aberta";

                if (compra.Fechada)
                {
                    BloquearEdicao();
                }

                CarregarItens();
                AtualizarResumo();
            }
        }

        private void BloquearEdicao()
        {
            cmbNomeCompra.Enabled = false;
            dateCompra.Enabled = false;
            cmbTipoArtigo.Enabled = false;
            cmbArtigo.Enabled = false;
            numQuantidade.Enabled = false;
            txtPreco.Enabled = false;
            btnAdicionar.Enabled = false;
            btnRemoverItem.Enabled = false;
        }

        private void btnFecharCompra_Click(object sender, EventArgs e)
        {
            if (cmbNomeCompra.SelectedValue == null)
            {
                MessageBox.Show("Selecione uma compra.");
                return;
            }

            int idCompra = Convert.ToInt32(cmbNomeCompra.SelectedValue);
            string mensagem;

            if (ModoCompraController.FecharCompra(idCompra, out mensagem))
            {
                MessageBox.Show(mensagem);
                CarregarCmbCompra();
                LimparCampos();
                compraId = 0;
            }
            else
            {
                MessageBox.Show(mensagem);
            }
        }

        private void LimparCampos()
        {
            cmbNomeCompra.SelectedIndex = -1;
            dateCompra.Value = DateTime.Now;
            txtEstado.Clear();
            dataGridView1.DataSource = null;
            txtOrcamento.Clear();
            txtTotalGasto.Clear();
            txtSaldoDisponivel.Clear();
            lblAviso.Visible = false;
            cmbItemPrevisto.DataSource = null;
            txtQtdPrevista.Clear();
            numQtdAdquirida.Value = 0;
            cmbArtigo.SelectedIndex = -1;
            numQuantidade.Value = 1;
            txtPrecoPrevisto.Clear();
            txtDescricao.Clear();
            cmbTipoArtigo.SelectedIndex = -1;
            cmbArtigo.DataSource = null;
        }

        // Botão para adicionar um item não previsto à compra
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            // 1. Valida se há uma compra selecionada no formulário
            if (compraId <= 0)
            {
                MessageBox.Show("Selecione uma compra ativa primeiro antes de adicionar itens.");
                return;
            }

            // 2. Valida se um artigo foi selecionado na ComboBox
            if (cmbArtigo.SelectedValue == null)
            {
                MessageBox.Show("Selecione um artigo da lista.");
                return;
            }

            // 3. Valida se o preço introduzido na TextBox é um número decimal válido
            if (!decimal.TryParse(txtPrecoPrevisto.Text, out decimal preco))
            {
                MessageBox.Show("Introduza um preço unitário válido.");
                return;
            }

            // 4. Obtém os valores dos controlos do Form
            int artigoId = Convert.ToInt32(cmbArtigo.SelectedValue);
            int qtd = Convert.ToInt32(numQuantidade.Value);
            string obs = txtDescricao.Text;

            string mensagem;

            // 5. Chama o método do Controller
            if (ModoCompraController.AdicionarItemNaoPrevisto(compraId, artigoId, qtd, preco, obs, out mensagem))
            {
                MessageBox.Show(mensagem);

                // 6. Atualiza a tabela e os totais financeiros no ecrã
                CarregarItens();
                AtualizarResumo();

                // 7. Limpa apenas os campos de inserção de itens para o utilizador poder meter o próximo
                cmbArtigo.SelectedIndex = -1;
                numQuantidade.Value = 1;
                txtPrecoPrevisto.Clear();
            }
            else
            {
                MessageBox.Show(mensagem);
            }
        }

        private void AtualizarResumo()
        {
            if (compraId <= 0) return;

            decimal valorOrcamento = ModoCompraController.ObterOrcamentoCompra(compraId);
            decimal totalGasto = ModoCompraController.ObterTotalCompra(compraId);
            decimal saldoDisponivel = valorOrcamento - totalGasto;

            txtOrcamento.Text = $"{valorOrcamento:0.00} €";
            txtTotalGasto.Text = $"{totalGasto:0.00} €";

            AtualizarAlertaOrcamento(valorOrcamento, saldoDisponivel);
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
                        i.QuantidadeAdquirida,
                        i.PrecoUnitario,
                        Total = i.QuantidadeAdquirida * (i.PrecoUnitario ?? 0),
                        i.Adquirido,
                        i.Observacoes
                    })
                    .ToList();
            }

            if (dataGridView1.Columns.Contains("Id"))
                dataGridView1.Columns["Id"].Visible = false;
        }

        private void AtualizarAlertaOrcamento(decimal orcamento, decimal saldo)
        {
            txtSaldoDisponivel.Text = $"{saldo:0.00} €";

            if (orcamento <= 0)
            {
                lblAviso.Text = "Sem orçamento definido";
                lblAviso.ForeColor = Color.Gray;
                lblAviso.Visible = true;
                txtSaldoDisponivel.BackColor = Color.LightGray;
                txtSaldoDisponivel.ForeColor = Color.Black;
                return;
            }

            decimal percentagem = orcamento > 0 ? (saldo / orcamento) : 0;

            if (saldo <= 0)
            {
                lblAviso.Text = "Orçamento excedido!";
                lblAviso.ForeColor = Color.Red;
                lblAviso.Visible = true;
                txtSaldoDisponivel.BackColor = Color.Red;
                txtSaldoDisponivel.ForeColor = Color.White;
            }
            else if (percentagem <= 0.20m)
            {
                lblAviso.Text = "Orçamento quase esgotado";
                lblAviso.ForeColor = Color.DarkOrange;
                lblAviso.Visible = true;
                txtSaldoDisponivel.BackColor = Color.Gold;
                txtSaldoDisponivel.ForeColor = Color.Black;
            }
            else
            {
                lblAviso.Text = "Orçamento dentro do limite";
                lblAviso.ForeColor = Color.Green;
                lblAviso.Visible = true;
                txtSaldoDisponivel.BackColor = Color.LightGreen;
                txtSaldoDisponivel.ForeColor = Color.Black;
            }
        }

        private void cmbNomeCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNomeCompra.SelectedValue == null) return;

            if (int.TryParse(cmbNomeCompra.SelectedValue.ToString(), out int id))
            {
                compraId = id;
                CarregarCompraAberta();
                CarregarItensPrevistos();
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegistar_Click(object sender, EventArgs e)
        {
            if (cmbItemPrevisto.SelectedValue == null)
            {
                MessageBox.Show("Selecione um item previsto.");
                return;
            }

            if (numQtdAdquirida.Value <= 0)
            {
                MessageBox.Show("Introduza uma quantidade adquirida válida.");
                return;
            }

            if (!decimal.TryParse(txtPreco.Text, out decimal preco))
            {
                MessageBox.Show("Preço inválido.");
                return;
            }

            int itemId = Convert.ToInt32(cmbItemPrevisto.SelectedValue);
            int quantidadeAdquirida = (int)numQtdAdquirida.Value;
            string mensagem;

            if (ModoCompraController.RegistarAquisicaoItemPrevisto(itemId, quantidadeAdquirida, preco, out mensagem))
            {
                MessageBox.Show(mensagem);
                CarregarItens();
                AtualizarResumo();

                txtQtdPrevista.Clear();
                numQtdAdquirida.Value = 0;
                txtPreco.Clear();
            }
            else
            {
                MessageBox.Show(mensagem);
            }
        }

        // Carrega os itens previstos na ComboBox, onde filtra apenas os que ainda não foram adquiridos
        private void CarregarItensPrevistos()
        {
            if (compraId <= 0) return;

            using (shoppingContext db = new shoppingContext())
            {
                var itens = db.ItemComprasPlaneadas
                    .Where(i => i.CompraPlaneadaId == compraId && i.Previsto)
                    .Select(i => new
                    {
                        i.Id,
                        Nome = i.Artigos.Nome
                    })
                    .ToList();

                cmbItemPrevisto.SelectedIndexChanged -= cmbItemPrevisto_SelectedIndexChanged_1;// Desanexa o evento para evitar que seja disparado durante a atualização da fonte de dados
                cmbItemPrevisto.DataSource = itens;
                cmbItemPrevisto.DisplayMember = "Nome";
                cmbItemPrevisto.ValueMember = "Id";
                cmbItemPrevisto.SelectedIndex = -1;
                cmbItemPrevisto.SelectedIndexChanged += cmbItemPrevisto_SelectedIndexChanged_1; // Reanexa o evento após atualizar a fonte de dados
            }
        }

        private void cmbItemPrevisto_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbItemPrevisto.SelectedValue == null)
            {
                return;
            }

            if (int.TryParse(cmbItemPrevisto.SelectedValue.ToString(), out int itemId))
            {
                using (shoppingContext db = new shoppingContext())
                {
                    var item = db.ItemComprasPlaneadas.Find(itemId);
                    if (item == null)
                    {
                        return;
                    }

                    txtQtdPrevista.Text = item.QuantidadePrevista.ToString();
                    numQtdAdquirida.Value = item.QuantidadeAdquirida;
                    txtPreco.Text = item.PrecoUnitario?.ToString("0.00") ?? ""; // Mostra o preço unitário formatado ou vazio se for nulo
                    //? retorna o valor à esquerda se não for nulo, ou o valor à direita se for nulo 
                    //"" é uma string vazia para evitar mostrar "0.00" quando o preço unitário for nulo, dando a entender que o preço ainda não foi definido para este item previsto.
                }
            }
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            // Verifica se há alguma linha selecionada na Grid
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecione o item que deseja remover na tabela.");
                return;
            }

            // Valida se a compra está ativa
            if (compraId <= 0)
            {
                MessageBox.Show("Selecione uma compra ativa primeiro.");
                return;
            }

            // Confirmação do utilizador
            var resultado = MessageBox.Show("Tem a certeza que deseja remover este item?",
                                            "Confirmar Eliminação",
                                            MessageBoxButtons.YesNo);

            if (resultado == DialogResult.No)
            {
                return;
            }

            // Obtém o ID do Item a partir da coluna "Id" da linha selecionada
            int itemId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            string mensagem;

            // Chama o método do Controller
            if (ModoCompraController.RemoverItemCompra(itemId, out mensagem))
            {
                MessageBox.Show(mensagem);

                // Atualiza o ecrã
                CarregarItens();
                CarregarItensPrevistos();
                AtualizarResumo();
            }
            else
            {
                MessageBox.Show(mensagem);
            }
        }

        // Carrega os Tipos de Artigo na ComboBox para o utilizador escolher
        private void CarregarCmbTipoArtigo()
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Procura todos os tipos de artigo ordenados por nome
                var tipos = db.TipoArtigos
                    .OrderBy(t => t.Nome)
                    .ToList();

                cmbTipoArtigo.DataSource = tipos;
                cmbTipoArtigo.DisplayMember = "Nome";
                cmbTipoArtigo.ValueMember = "Id";

                cmbTipoArtigo.SelectedIndex = -1; // Inicia vazio
            }
        }

        // Carrega os Artigos na ComboBox, onde é filtrado apenas os que pertencem ao Tipo de Artigo selecionado
        private void CarregarCmbArtigo(int tipoArtigoId)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Filtra os artigos que pertencem ao Tipo de Artigo selecionado
                var artigos = db.Artigos
                    .Where(a => a.TipoArtigoId == tipoArtigoId) // Ajusta a propriedade da FK se necessário
                    .OrderBy(a => a.Nome)
                    .ToList();

                cmbArtigo.DataSource = artigos;
                cmbArtigo.DisplayMember = "Nome";
                cmbArtigo.ValueMember = "Id";

                cmbArtigo.SelectedIndex = -1; // Inicia vazio
            }
        }

        // Botão Voltar
        private void btnVoltar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbTipoArtigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Se não houver nada selecionado ou o valor for nulo, limpa a combo de artigos
            if (cmbTipoArtigo.SelectedValue == null)
            {
                cmbArtigo.DataSource = null;
                return;
            }

            // Tenta converter o ID selecionado para inteiro
            if (int.TryParse(cmbTipoArtigo.SelectedValue.ToString(), out int tipoId))
            {
                // Carrega apenas os artigos pertencentes a este tipo
                CarregarCmbArtigo(tipoId);
            }
        }

        private void btnFecharCompra_Click_1(object sender, EventArgs e)
        {
            // 1. Valida se o utilizador selecionou alguma compra na lista
            if (cmbNomeCompra.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecione uma compra para fechar.");
                return;
            }

            // 2. Confirmação de segurança (opcional, mas evita cliques por acidente)
            var resultado = MessageBox.Show("Tem a certeza que deseja fechar esta compra?",
                                            "Confirmar Fecho",
                                            MessageBoxButtons.YesNo);

            if (resultado == DialogResult.No) return;

            // 3. Obtém o ID da compra selecionada
            int idCompra = Convert.ToInt32(cmbNomeCompra.SelectedValue);
            string mensagem;

            // 4. Executa a lógica através do Controller (sem try-catch)
            if (ModoCompraController.FecharCompra(idCompra, out mensagem))
            {
                MessageBox.Show(mensagem, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 5. Atualiza a interface: recarrega a combo (pois a compra fechada desaparece das "em aberto")
                CarregarCmbCompra();
                LimparCampos();
                compraId = 0; // Faz reset ao ID em memória
            }
            else
            {
                MessageBox.Show(mensagem);
            }
        }

        private void txtEstado_TextChanged(object sender, EventArgs e)
        {

        }
    }
}