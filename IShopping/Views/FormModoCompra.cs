using IShopping.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace IShopping.Views
{
    // Classe responsável pelo ecrã do Modo Compra ativo (registo de aquisições em tempo real)
    public partial class FormModoCompra : Form
    {
        private int compraId; // Guarda o ID da compra atualmente selecionada
        internal int CompraIdSelecionada; // Variável auxiliar para transferir IDs entre ecrãs

        // Construtor - Inicializa o formulário
        public FormModoCompra()
        {
            InitializeComponent();
        }

        // Evento que executa ao abrir o ecrã
        private void FormModoCompra_Load(object sender, EventArgs e)
        {
            // Configura o componente de data para mostrar apenas o mês e o ano
            dateCompra.Format = DateTimePickerFormat.Custom;
            dateCompra.CustomFormat = "MM/yyyy";

            // Bloqueia a edição da data (apenas para visualização)
            dateCompra.Enabled = false;

            // Carrega as listas iniciais nos componentes correspondentes
            CarregarCmbCompra();
            CarregarCmbTipoArtigo();
        }

        // Carrega as compras que ainda estão ativas/abertas na ComboBox
        private void CarregarCmbCompra()
        {
            var compras = ModoCompraController.ObterComprasEmAberto();

            cmbNomeCompra.DataSource = compras;
            cmbNomeCompra.DisplayMember = "NomeCompra";
            cmbNomeCompra.ValueMember = "Id";
            cmbNomeCompra.SelectedIndex = -1; // Começa sem nenhuma selecionada
        }

        // Procura e mostra as informações detalhadas da compra selecionada
        private void CarregarCompraAberta()
        {
            using (shoppingContext db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.FirstOrDefault(c => c.Id == compraId);

                if (compra == null) return;

                // Preenche os campos do ecrã com os dados guardados na BD
                cmbNomeCompra.Text = compra.NomeCompra;
                dateCompra.Value = compra.DataCompra;
                txtEstado.Text = compra.Fechada ? "Fechada" : "Aberta";

                // Se a compra já estiver fechada, tranca todos os controlos para edição
                if (compra.Fechada)
                {
                    BloquearEdicao();
                }

                // Atualiza a tabela de itens comprados e o resumo financeiro
                CarregarItens();
                AtualizarResumo();
            }
        }

        // Desativa todos os campos e botões para impedir alterações numa compra fechada
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

        // Botão para encerrar a compra 
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

        // Limpa todos os campos de texto, números e seleções do formulário
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

        // Botão para adicionar um item novo (extra / não planeado) à lista de compras
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (compraId <= 0)
            {
                MessageBox.Show("Selecione uma compra ativa primeiro antes de adicionar itens.");
                return;
            }

            if (cmbArtigo.SelectedValue == null)
            {
                MessageBox.Show("Selecione um artigo da lista.");
                return;
            }

            // Valida se o preço digitado é um número válido
            if (!decimal.TryParse(txtPrecoPrevisto.Text, out decimal preco))
            {
                MessageBox.Show("Introduza um preço unitário válido.");
                return;
            }

            int artigoId = Convert.ToInt32(cmbArtigo.SelectedValue);
            int qtd = Convert.ToInt32(numQuantidade.Value);
            string obs = txtDescricao.Text;
            string mensagem;

            // Insere o item extra através do Controller
            if (ModoCompraController.AdicionarItemNaoPrevisto(compraId, artigoId, qtd, preco, obs, out mensagem))
            {
                MessageBox.Show(mensagem);

                CarregarItens();   // Atualiza a tabela
                AtualizarResumo();  // Recalcula os custos

                // Limpa as caixas de inserção para o próximo item
                cmbArtigo.SelectedIndex = -1;
                numQuantidade.Value = 1;
                txtPrecoPrevisto.Clear();
            }
            else
            {
                MessageBox.Show(mensagem);
            }
        }

        // Recalcula e atualiza as caixas de texto com o Orçamento, Gasto Total e Saldo
        private void Resumo()
        {
            if (compraId <= 0) return;

            decimal valorOrcamento = ModoCompraController.ObterOrcamentoCompra(compraId);
            decimal totalGasto = ModoCompraController.ObterTotalCompra(compraId);
            decimal saldoDisponivel = valorOrcamento - totalGasto;

            txtOrcamento.Text = $"{valorOrcamento:0.00} €";
            txtTotalGasto.Text = $"{totalGasto:0.00} €";

            AtualizarAlertaOrcamento(valorOrcamento, saldoDisponivel);
        }

        // AtualizarResumo - Método complementar que executa a função acima
        private void AtualizarResumo()
        {
            Resumo();
        }

        // Carrega todos os itens associados a esta compra na tabela (DataGridView)
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

            // Esconde a coluna do ID do item para não sobrecarregar a tabela no ecrã
            if (dataGridView1.Columns.Contains("Id"))
                dataGridView1.Columns["Id"].Visible = false;
        }

        // Altera as cores e os avisos do ecrã dependendo do dinheiro restante
        private void AtualizarAlertaOrcamento(decimal orcamento, decimal saldo)
        {
            txtSaldoDisponivel.Text = $"{saldo:0.00} €";

            // Caso não tenha sido estipulado nenhum teto de orçamento
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

            // Alerta Vermelho: Gastou mais do que tinha planeado
            if (saldo <= 0)
            {
                lblAviso.Text = "Orçamento excedido!";
                lblAviso.ForeColor = Color.Red;
                lblAviso.Visible = true;
                txtSaldoDisponivel.BackColor = Color.Red;
                txtSaldoDisponivel.ForeColor = Color.White;
            }
            // Alerta Laranja: Restam menos de 20% do orçamento total
            else if (percentagem <= 0.20m)
            {
                lblAviso.Text = "Orçamento quase esgotado";
                lblAviso.ForeColor = Color.DarkOrange;
                lblAviso.Visible = true;
                txtSaldoDisponivel.BackColor = Color.Gold;
                txtSaldoDisponivel.ForeColor = Color.Black;
            }
            // Alerta Verde: Contas controladas e dentro do limite previsto
            else
            {
                lblAviso.Text = "Orçamento dentro do limite";
                lblAviso.ForeColor = Color.Green;
                lblAviso.Visible = true;
                txtSaldoDisponivel.BackColor = Color.LightGreen;
                txtSaldoDisponivel.ForeColor = Color.Black;
            }
        }

        // Evento que deteta quando o utilizador muda a compra selecionada na ComboBox principal
        private void cmbNomeCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNomeCompra.SelectedValue == null) return;

            if (int.TryParse(cmbNomeCompra.SelectedValue.ToString(), out int id))
            {
                compraId = id;
                CarregarCompraAberta();   // Mostra as informações da compra
                CarregarItensPrevistos(); // Carrega os itens que foram planeados para ela
            }
        }

        // Botão para fechar a janela ativa
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Botão "Registar" - Confirma a compra física de um item que já estava planeado
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

            // Salva as informações reais de compra (preço pago e quantidade real trazida)
            if (ModoCompraController.RegistarAquisicaoItemPrevisto(itemId, quantidadeAdquirida, preco, out mensagem))
            {
                MessageBox.Show(mensagem);
                CarregarItens();
                AtualizarResumo();

                // Limpa a área de registo rápido
                txtQtdPrevista.Clear();
                numQtdAdquirida.Value = 0;
                txtPreco.Clear();
            }
            else
            {
                MessageBox.Show(mensagem);
            }
        }

        // Carrega na ComboBox apenas os itens que foram planeados para esta lista
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

                // Desativa o evento temporariamente para preencher a lista de forma limpa
                cmbItemPrevisto.SelectedIndexChanged -= cmbItemPrevisto_SelectedIndexChanged_1;

                cmbItemPrevisto.DataSource = itens;
                cmbItemPrevisto.DisplayMember = "Nome";
                cmbItemPrevisto.ValueMember = "Id";
                cmbItemPrevisto.SelectedIndex = -1;

                // Reativa o evento após o preenchimento concluir
                cmbItemPrevisto.SelectedIndexChanged += cmbItemPrevisto_SelectedIndexChanged_1;
            }
        }

        // Evento que muda o foco do item previsto e preenche as caixas com as estimativas anteriores
        private void cmbItemPrevisto_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbItemPrevisto.SelectedValue == null) return;

            if (int.TryParse(cmbItemPrevisto.SelectedValue.ToString(), out int itemId))
            {
                using (shoppingContext db = new shoppingContext())
                {
                    var item = db.ItemComprasPlaneadas.Find(itemId);
                    if (item == null) return;

                    // Mostra o que se esperava comprar para ajudar o utilizador na hora de pagar
                    txtQtdPrevista.Text = item.QuantidadePrevista.ToString();
                    numQtdAdquirida.Value = item.QuantidadeAdquirida;
                    txtPreco.Text = item.PrecoUnitario?.ToString("0.00") ?? "";
                }
            }
        }

        // Botão para apagar/remover um item selecionado na tabela
        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecione o item que deseja remover na tabela.");
                return;
            }

            if (compraId <= 0)
            {
                MessageBox.Show("Selecione uma compra ativa primeiro.");
                return;
            }

            var resultado = MessageBox.Show("Tem a certeza que deseja remover este item?",
                                            "Confirmar Eliminação",
                                            MessageBoxButtons.YesNo);

            if (resultado == DialogResult.No) return;

            int itemId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            string mensagem;

            // Remove Item da compra através do Controller
            if (ModoCompraController.RemoverItemCompra(itemId, out mensagem))
            {
                MessageBox.Show(mensagem);

                // Recarrega todos os dados visuais do ecrã
                CarregarItens();
                CarregarItensPrevistos();
                AtualizarResumo();
            }
            else
            {
                MessageBox.Show(mensagem);
            }
        }

        // Carrega as categorias de produto na lista de artigos extras
        private void CarregarCmbTipoArtigo()
        {
            using (shoppingContext db = new shoppingContext())
            {
                var tipos = db.TipoArtigos.OrderBy(t => t.Nome).ToList();

                cmbTipoArtigo.DataSource = tipos;
                cmbTipoArtigo.DisplayMember = "Nome";
                cmbTipoArtigo.ValueMember = "Id";
                cmbTipoArtigo.SelectedIndex = -1;
            }
        }

        // Filtra e carrega os produtos que fazem parte da categoria selecionada acima
        private void CarregarCmbArtigo(int tipoArtigoId)
        {
            using (shoppingContext db = new shoppingContext())
            {
                var artigos = db.Artigos
                    .Where(a => a.TipoArtigoId == tipoArtigoId)
                    .OrderBy(a => a.Nome)
                    .ToList();

                cmbArtigo.DataSource = artigos;
                cmbArtigo.DisplayMember = "Nome";
                cmbArtigo.ValueMember = "Id";
                cmbArtigo.SelectedIndex = -1;
            }
        }

        // Botão secundário de sair da janela
        private void btnVoltar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        // Sempre que muda a categoria de artigo, recarrega a lista de produtos daquela área
        private void cmbTipoArtigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoArtigo.SelectedValue == null)
            {
                cmbArtigo.DataSource = null;
                return;
            }

            if (int.TryParse(cmbTipoArtigo.SelectedValue.ToString(), out int tipoId))
            {
                CarregarCmbArtigo(tipoId);
            }
        }

        // Botão "Fechar Compra" - Finaliza permanentemente a lista, impedindo edições futuras
        private void btnFecharCompra_Click_1(object sender, EventArgs e)
        {
            if (cmbNomeCompra.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecione uma compra para fechar.");
                return;
            }
            
            var resultado = MessageBox.Show("Tem a certeza que deseja fechar esta compra?",
                                            "Confirmar Fecho",
                                            MessageBoxButtons.YesNo);

            //Se o resultado for não retorna e não fecha a compra
            if (resultado == DialogResult.No)
            { 
                return; 
            }

            int idCompra = Convert.ToInt32(cmbNomeCompra.SelectedValue);
            string mensagem;

            // Executa a finalização no banco de dados
            if (ModoCompraController.FecharCompra(idCompra, out mensagem))
            {
                MessageBox.Show(mensagem, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Atualiza o ecrã (A lista fechada some das opções e reseta a janela)
                CarregarCmbCompra();
                LimparCampos();
                compraId = 0;
            }
            else
            {
                MessageBox.Show(mensagem);
            }
        }

        // Método automático da caixa de texto do Estado (Pode ficar vazio)
        private void txtEstado_TextChanged(object sender, EventArgs e)
        {
        }
    }
}