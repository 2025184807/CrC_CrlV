using IShopping.Controller;
using IShopping.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormEstatisticas : Form
    {
        private int semanaSelecionada = 1;

        public FormEstatisticas()
        {
            InitializeComponent();
        }

        // Evento que dispara quando o formulário abre
        private void FormEstatisticas_Load(object sender, EventArgs e)
        {
            CarregarListagensHistorico(); // Executa a função que preenche as tabelas
        }

        //Carrega os dados nas tabelas da primeira aba (Requisitos 20.a e 20.b)
        private void CarregarListagensHistorico()
        {
            // Forçar a criação limpa das colunas do dgvOrcamentos (Tabela de cima)
            dgvOrcamentos.Columns.Clear();
            dgvOrcamentos.Columns.Add("MesAno", "Mês/Ano");
            dgvOrcamentos.Columns.Add("Orcamento", "Orçamento");
            dgvOrcamentos.Columns.Add("TotalGasto", "Total Compras");
            dgvOrcamentos.Columns.Add("Diferenca", "Diferença");

            // Forçar a criação limpa das colunas do dgvComprasFechadas (Tabela de baixo)
            dgvComprasFechadas.Columns.Clear();
            dgvComprasFechadas.Columns.Add("NomeCompra", "Nome da Compra");
            dgvComprasFechadas.Columns.Add("Previstos", "% Itens Previstos");
            dgvComprasFechadas.Columns.Add("NaoPrevistos", "% Itens Não Previstos");
            dgvComprasFechadas.Columns.Add("TotalCompra", "Total Gasto (€)");

            int anoAtual = DateTime.Today.Year; // Pega o ano atual para filtrar os dados
            bool encontrouDados = false;

            // Vamos correr todos os 12 meses do ano
            for (int mes = 1; mes <= 12; mes++)
            {
                ResumoComprasDto resumo = EstatisticasAvancadasController.ObterResumoMensal(mes, anoAtual);

                // Adiciona se houver orçamento OU compras registadas
                if (resumo.OrcamentoMensal > 0 || resumo.TotalComprasMes > 0)
                {
                    encontrouDados = true;
                    string nomeMesAno = $"{mes:D2}/{anoAtual}";

                    int rowIndex = dgvOrcamentos.Rows.Add(
                        nomeMesAno,
                        resumo.OrcamentoMensal.ToString("C2"),
                        resumo.TotalComprasMes.ToString("C2"),
                        resumo.Diferenca.ToString("C2")
                    );

                    // Pintar células
                    if (resumo.Diferenca < 0)
                        dgvOrcamentos.Rows[rowIndex].Cells[3].Style.ForeColor = Color.Red;
                    else
                        dgvOrcamentos.Rows[rowIndex].Cells[3].Style.ForeColor = Color.Green;
                }

                // CORRIGIDO: Como a tua lista afinal é de 'string', o loop lê cada nomeCompra diretamente.
                // Usamos os valores gerais do 'resumo' do mês para preencher o resto sem quebrar o código!
                if (resumo.ComprasFechadas != null && resumo.ComprasFechadas.Count > 0)
                {
                    foreach (string nomeCompra in resumo.ComprasFechadas)
                    {
                        dgvComprasFechadas.Rows.Add(
                            nomeCompra,                                          // O nome em formato string
                            resumo.PercentagemPrevistos.ToString("F1") + "%",    // Percentagem do mês
                            resumo.PercentagemNaoPrevistos.ToString("F1") + "%", // Percentagem do mês
                            resumo.TotalComprasMes.ToString("C2")                // Total do mês
                        );
                    }
                }
            }

            // Alerta extra se a BD estiver completamente vazia (Melhoramento e Proteção)
            if (!encontrouDados)
            {
                MessageBox.Show("Não foram encontrados dados de orçamentos ou compras fechadas para o ano corrente na Base de Dados.",
                                "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Botão "Voltar" para fechar o formulário e retornar ao menu principal
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // --- CÓDIGO DA SEGUNDA ABA (Sugestões e Apoio à Decisão ) ---

        private void btnGerarSugestao_Click(object sender, EventArgs e)
        {
            var dadosOrcamento = EstatisticasAvancadasController.SugerirOrcamentoProximoMes();

            txtMediaOrcamentos.Text = dadosOrcamento.MediaUltimosMeses.ToString("C2");
            txtMediaGastos.Text = dadosOrcamento.MediaGastos.ToString("C2");
            txtDiferencaMedia.Text = dadosOrcamento.DiferencaMedia.ToString("C2");
            txtOrcamentoSugerido.Text = dadosOrcamento.SugestaoProximoMes.ToString("C2");
        }

        private void btnGerarLista_Click(object sender, EventArgs e)
        {
            var itensSugeridos = EstatisticasAvancadasController.SugerirListaComprasSemana(semanaSelecionada);

            listBoxItensSugeridos.Items.Clear();

            foreach (var item in itensSugeridos)
            {
                listBoxItensSugeridos.Items.Add($"{item.NomeArtigo} (Frequência no histórico: {item.Frequencia}x)");
            }
        }

        private void btnSemana1_Click(object sender, EventArgs e)
        {
            semanaSelecionada = 1;
            ResetarCoresBotoes();
            btnSemana1.BackColor = Color.LightBlue;
        }

        private void btnSemana2_Click(object sender, EventArgs e)
        {
            semanaSelecionada = 2;
            ResetarCoresBotoes();
            btnSemana2.BackColor = Color.LightBlue;
        }

        private void btnSemana3_Click(object sender, EventArgs e)
        {
            semanaSelecionada = 3;
            ResetarCoresBotoes();
            btnSemana3.BackColor = Color.LightBlue;
        }

        private void btnSemana4_Click(object sender, EventArgs e)
        {
            semanaSelecionada = 4;
            ResetarCoresBotoes();
            btnSemana4.BackColor = Color.LightBlue;
        }

        private void ResetarCoresBotoes()
        {
            btnSemana1.BackColor = SystemColors.Control;
            btnSemana2.BackColor = SystemColors.Control;
            btnSemana3.BackColor = SystemColors.Control;
            btnSemana4.BackColor = SystemColors.Control;
        }

        private void dgvOrcamentos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}