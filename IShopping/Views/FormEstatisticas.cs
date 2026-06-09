using IShopping.Controller; // Permite aceder às lógicas de cálculo das estatísticas
using IShopping.Models;     // Permite interagir com as classes de dados e DTOs
using System;
using System.Drawing;       // Necessário para manipular cores de componentes (Ex: Color.LightBlue)
using System.Windows.Forms; // Necessário para a infraestrutura de componentes Windows Forms

namespace IShopping.Views
{
    // Classe do formulário responsável por apresentar relatórios estatísticos e sugestões inteligentes ao utilizador
    public partial class FormEstatisticas : Form
    {
        // Variável global do formulário que armazena qual a aba/botão de semana está ativo (Começa na Semana 1)
        private int semanaSelecionada = 1;

        // Construtor do formulário
        public FormEstatisticas()
        {
            InitializeComponent(); // Inicializa os componentes visuais desenhados no Designer
        }

        private void FormEstatisticas_Load(object sender, EventArgs e)
        {
            CarregarListagensHistorico(); // Executa a função principal para preencher as tabelas de histórico económico

        }

        // Função responsável por extrair dados do banco de dados e alimentar os DataGridViews da primeira aba
        private void CarregarListagensHistorico()
        {
            // 1. CONFIGURAÇÃO DA TABELA DE ORÇAMENTOS (dgvOrcamentos - Tabela Superior)
            dgvOrcamentos.Columns.Clear(); // Limpa colunas para evitar duplicações ao recarregar
            dgvOrcamentos.Columns.Add("MesAno", "Mês/Ano");
            dgvOrcamentos.Columns.Add("Orcamento", "Orçamento");
            dgvOrcamentos.Columns.Add("TotalGasto", "Total Compras");
            dgvOrcamentos.Columns.Add("Diferenca", "Diferença");

            // 2. CONFIGURAÇÃO DA TABELA DE COMPRAS CONCLUÍDAS (dgvComprasFechadas - Tabela Inferior)
            dgvComprasFechadas.Columns.Clear(); // Limpa colunas
            dgvComprasFechadas.Columns.Add("NomeCompra", "Nome da Compra");
            dgvComprasFechadas.Columns.Add("Previstos", "% Itens Previstos");
            dgvComprasFechadas.Columns.Add("NaoPrevistos", "% Itens Não Previstos");
            dgvComprasFechadas.Columns.Add("TotalCompra", "Total Gasto (€)");

            // 3. CAPTURA E PROCESSAMENTO DOS DADOS DO ANO CORRENTE
            int anoAtual = DateTime.Now.Year; // Obtém dinamicamente o ano atual do relógio do sistema
            bool encontrouDados = false;      // Sinalizador para verificar se o ano tem algum registo na BD

            // Ciclo que percorre sequencialmente cada um dos 12 meses do ano
            for (int mes = 1; mes <= 12; mes++)
            {
                // Solicita ao Controller o DTO preenchido com o resumo financeiro deste mês específico
                ResumoComprasDto resumo = EstatisticasAvancadasController.ObterResumoMensal(mes, anoAtual);

                // Proteção antipânico: se o resumo falhar ou vier nulo, salta para o mês seguinte sem quebrar o programa
                if (resumo == null) continue;

                // Critério de exibição: Só mostra o mês se houver um orçamento definido OU se o utilizador gastou dinheiro
                if (resumo.OrcamentoMensal > 0 || resumo.TotalComprasMes > 0)
                {
                    encontrouDados = true; // Ativa a flag informando que a Base de Dados contém dados reais
                    string nomeMesAno = $"{mes:D2}/{anoAtual}"; // Formata o mês com dois dígitos (Ex: "01/2026")

                    // Insere os totais calculados na tabela de orçamentos e captura o índice da linha criada
                    int rowIndex = dgvOrcamentos.Rows.Add(
                        nomeMesAno,
                        resumo.OrcamentoMensal.ToString("C2"), // Formata o valor como moeda local (€)
                        resumo.TotalComprasMes.ToString("C2"),  // Formata o valor como moeda local (€)
                        resumo.Diferenca.ToString("C2")        // Formata o valor como moeda local (€)
                    );

                    // Alerta Visual Dinâmico: Avalia o saldo do mês (Orçamento - Gastos)
                    if (resumo.Diferenca < 0)
                        // Se gastou mais do que o orçamento, pinta o texto da célula de Vermelho (Prejuízo)
                        dgvOrcamentos.Rows[rowIndex].Cells[3].Style.ForeColor = Color.Red;
                    else
                        // Se ficou dentro do orçamento previsto, pinta o texto de Verde (Poupança/Lucro)
                        dgvOrcamentos.Rows[rowIndex].Cells[3].Style.ForeColor = Color.Green;
                }

                // 4. PREENCHIMENTO DETALHADO DAS COMPRAS INDIVIDUAIS DO MÊS
                // Verifica se a lista de nomes de compras fechadas não está vazia
                if (resumo.ComprasFechadas != null && resumo.ComprasFechadas.Count > 0)
                {
                    // Varre cada compra realizada dentro deste mês específico
                    foreach (string nomeCompra in resumo.ComprasFechadas)
                    {
                        // Adiciona as métricas de comportamento de carrinho na tabela de baixo
                        dgvComprasFechadas.Rows.Add(
                            nomeCompra,
                            resumo.PercentagemPrevistos.ToString("F1") + "%",    // Percentagem formatada com 1 casa decimal
                            resumo.PercentagemNaoPrevistos.ToString("F1") + "%", // Percentagem formatada com 1 casa decimal
                            resumo.TotalComprasMes.ToString("C2")                // Custo total final da lista
                        );
                    }
                }
            } // Fim do ciclo FOR dos 12 meses

            // 5. VALIDAÇÃO DE BASE DE DADOS VAZIA
            // Se após varrer o ano inteiro, a flag continuar 'false', avisa amigavelmente o utilizador
            if (!encontrouDados)
            {
                MessageBox.Show("Não foram encontrados dados de orçamentos ou compras fechadas para o ano corrente na Base de Dados.");
            }
        }

        // Botão "Voltar" - Fecha a janela atual de estatísticas
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close(); // Encerra este formulário
        }

        // Botão "Gerar Sugestão de Orçamento" - Calcula previsões baseadas em médias históricas
        private void btnGerarSugestao_Click(object sender, EventArgs e)
        {
            // Invoca o motor matemático do controller para calcular médias de gastos e limites ideais
            var dadosOrcamento = EstatisticasAvancadasController.SugerirOrcamentoProximoMes();

            // Descarrega os resultados financeiros calculados diretamente nas caixas de texto do ecrã
            txtMediaOrcamentos.Text = dadosOrcamento.MediaUltimosMeses.ToString("C2"); // Média do que costuma planear
            txtMediaGastos.Text = dadosOrcamento.MediaGastos.ToString("C2");         // Média do que realmente gasta
            txtDiferencaMedia.Text = dadosOrcamento.DiferencaMedia.ToString("C2");   // Margem de erro ou desvio
            txtOrcamentoSugerido.Text = dadosOrcamento.SugestaoProximoMes.ToString("C2"); // Recomendação inteligente ideal
        }

        // Botão "Gerar Lista de Compras Recomendada" - Analisa os hábitos da semana selecionada
        private void btnGerarLista_Click(object sender, EventArgs e)
        {
            // Envia ao Controller o ID da semana ativa (1, 2, 3 ou 4) e obtém o ranking de frequência de artigos
            var itensSugeridos = EstatisticasAvancadasController.SugerirListaComprasSemana(semanaSelecionada);

            listBoxItensSugeridos.Items.Clear(); // Limpa as sugestões do ecrã anterior

            // Proteção caso não existam registos de compras guardados no lote correspondente a esta semana
            if (itensSugeridos == null || itensSugeridos.Count == 0)
            {
                listBoxItensSugeridos.Items.Add("Nenhum artigo registado para esta semana.");
                return; // Aborta a execução do método de forma limpa
            }

            // Varre o lote de artigos recomendados devolvidos pelo algoritmo
            foreach (var item in itensSugeridos)
            {
                // Imprime a sugestão na interface indicando o nome do produto e quantas vezes já foi comprado no passado
                listBoxItensSugeridos.Items.Add($"{item.NomeArtigo} (Frequência no histórico: {item.Frequencia}x)");
            }
        }

        // Clique no Botão Semana 1
        private void btnSemana1_Click(object sender, EventArgs e)
        {
            semanaSelecionada = 1; // Altera o ponteiro para a Semana 1
            ResetarCoresBotoes();  // Remove a cor azul de qualquer outro botão que estivesse ativo
            btnSemana1.BackColor = Color.LightBlue; // Destaca visualmente este botão em Azul Claro
        }

        // Clique no Botão Semana 2
        private void btnSemana2_Click(object sender, EventArgs e)
        {
            semanaSelecionada = 2; // Altera o ponteiro para a Semana 2
            ResetarCoresBotoes();  // Limpa as cores do ecrã
            btnSemana2.BackColor = Color.LightBlue; // Destaca o botão atual
        }

        // Clique no Botão Semana 3
        private void btnSemana3_Click(object sender, EventArgs e)
        {
            semanaSelecionada = 3; // Altera o ponteiro para a Semana 3
            ResetarCoresBotoes();  // Limpa as cores do ecrã
            btnSemana3.BackColor = Color.LightBlue; // Destaca o botão atual
        }

        // Clique no Botão Semana 4
        private void btnSemana4_Click(object sender, EventArgs e)
        {
            semanaSelecionada = 4; // Altera o ponteiro para a Semana 4
            ResetarCoresBotoes();  // Limpa as cores do ecrã
            btnSemana4.BackColor = Color.LightBlue; // Destaca o botão atual
        }

        // Função que devolve a cor cinzenta original a todos os botões de semana
        private void ResetarCoresBotoes()
        {
            btnSemana1.BackColor = SystemColors.Control;
            btnSemana2.BackColor = SystemColors.Control;
            btnSemana3.BackColor = SystemColors.Control;
            btnSemana4.BackColor = SystemColors.Control;
        }

        // Métodos vazios gerados automaticamente pelo clique acidental do rato no Designer (Mantenha-os para evitar erros no .Designer.cs)
        private void dgvOrcamentos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void tabPage2_Click(object sender, EventArgs e) { }
    }
}