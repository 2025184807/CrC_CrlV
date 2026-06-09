using IShopping.Models; // Serve para ligar os modelos públicos e o Contexto
using System;
using System.Collections.Generic; // Serve para usar List<T> e outros tipos de coleções genéricas
using System.Data.Entity; // Necessário para usar o .Include().
using System.Linq; 

namespace IShopping.Controller
{
    // Classe estática responsável por calcular relatórios, médias e sugestões inteligentes de compras
    internal static class EstatisticasAvancadasController
    {
        // Calcula o resumo financeiro e estatístico de um mês específico
        public static ResumoComprasDto ObterResumoMensal(int mes, int ano)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Cria o objeto DTO que vai guardar os resultados para enviar para o ecrã
                ResumoComprasDto resumo = new ResumoComprasDto();

                // 1. ORÇAMENTO DO MÊS
                // Procura se existe um orçamento definido para o mês e ano indicados
                var orcamento = db.Orcamentos.FirstOrDefault(o => o.Mes == mes && o.Ano == ano);

                if (orcamento != null)
                {
                    resumo.OrcamentoMensal = orcamento.ValorOrcamento;
                }

                // 2. COMPRAS FECHADAS DO MÊS
                // Vai buscar todas as compras planeadas que foram concluídas/fechadas nesse mês e ano
                var comprasFechadas = db.ComprasPlaneadas.Where(c => c.DataCompra.Month == mes && c.DataCompra.Year == ano && c.Fechada).ToList();

                decimal totalGastoNoMes = 0m;

                // Percorre cada uma das compras encontradas para somar os valores dos itens
                foreach (var compra in comprasFechadas)
                {
                    // Obtém apenas os itens que foram efetivamente comprados (Adquirido == true)
                    var itens = db.ItemComprasPlaneadas.Where(i => i.CompraPlaneadaId == compra.Id && i.Adquirido).ToList();

                    foreach (var item in itens)
                    {
                        // Se o item tiver preço, calcula o subtotal (Qtd * Preço) e soma ao total do mês
                        if (item.PrecoUnitario.HasValue)
                        {
                            totalGastoNoMes += item.QuantidadeAdquirida * item.PrecoUnitario.Value;
                        }

                        // Conta se o item foi planeado com antecedência ou se foi adicionado na hora
                        if (item.Previsto)
                        {
                            resumo.TotalItensPrevistos++;
                        }   
                        else
                        {
                            resumo.TotalItensNaoPrevistos++;
                        }
                            
                    }

                    // Adiciona o nome desta compra à lista de compras fechadas do relatório
                    resumo.ComprasFechadas.Add(compra.NomeCompra);
                }

                // Guarda os totais finais e calcula o saldo disponível (positivo ou negativo)
                resumo.TotalComprasMes = totalGastoNoMes;
                resumo.Diferenca = resumo.OrcamentoMensal - resumo.TotalComprasMes;

                // 3. CÁLCULO DAS PERCENTAGENS
                int totalItens = resumo.TotalItensPrevistos + resumo.TotalItensNaoPrevistos;

                // Se houver itens comprados, calcula a percentagem de previstos vs não previstos
                if (totalItens > 0)
                {
                    resumo.PercentagemPrevistos = (decimal)resumo.TotalItensPrevistos * 100 / totalItens;
                    resumo.PercentagemNaoPrevistos = (decimal)resumo.TotalItensNaoPrevistos * 100 / totalItens;
                }

                return resumo;
            }
        }

        // Função que sugere um orçamento para o próximo mês com base na média do histórico real
        public static SugestaoOrcamentoDto SugerirOrcamentoProximoMes()
        {
            using (shoppingContext db = new shoppingContext())
            {
                SugestaoOrcamentoDto sugestao = new SugestaoOrcamentoDto();

                // Obtém todos os orçamentos guardados no histórico
                var listaOrcamentos = db.Orcamentos.ToList();

                // Se o histórico estiver vazio, devolve o objeto sem sugestões
                if (!listaOrcamentos.Any())
                {
                    return sugestao;
                }
                    
                // Faz a média aritmética de todos os valores de orçamento do histórico
                sugestao.MediaUltimosMeses = listaOrcamentos.Average(o => o.ValorOrcamento);

                // Calcular os Gastos Reais de cada um dos meses anteriores
                List<decimal> gastosPorMes = new List<decimal>();

                foreach (var orc in listaOrcamentos)
                {
                    // Descobre os IDs das compras fechadas que pertencem ao mês e ano deste orçamento
                    var comprasDoMes = db.ComprasPlaneadas.Where(c => c.DataCompra.Month == orc.Mes && c.DataCompra.Year == orc.Ano &&c.Fechada)
                        .Select(c => c.Id)
                        .ToList();

                    // Soma o valor total gasto em todos os itens adquiridos nessas compras
                    decimal totalGastoNoMes = db.ItemComprasPlaneadas
                        .Where(i => comprasDoMes.Contains(i.CompraPlaneadaId) && i.Adquirido)
                        .AsEnumerable()
                        .Sum(i => i.QuantidadeAdquirida * (i.PrecoUnitario ?? 0m));

                    gastosPorMes.Add(totalGastoNoMes);
                }

                // Calcula a média real de dinheiro gasto por mês
                sugestao.MediaGastos = gastosPorMes.Any() ? gastosPorMes.Average() : 0m;

                // Calcula a diferença entre a média do orçamento e a média de gastos (arredondado a 2 casas)
                sugestao.DiferencaMedia = MoretonRound(sugestao.MediaUltimosMeses - MoretonRound(sugestao.MediaGastos));

                // Sugestão Inteligente: Escolhe o maior valor entre o orçamento habitual e o gasto real
                sugestao.SugestaoProximoMes = Math.Max(sugestao.MediaUltimosMeses, MoretonRound(sugestao.MediaGastos));

                return sugestao;
            }
        }

        // Sugere artigos com base na frequência em que aparecem na semana escolhida (1, 2, 3 ou 4)
        public static List<ItemSugeridoDto> SugerirListaComprasSemana(int numeroSemana)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Vai buscar todas as compras fechadas do sistema
                var comprasFechadas = db.ComprasPlaneadas
                    .Where(c => c.Fechada)
                    .ToList();

                // Cria uma lista de números inteiros vazia para guardar os IDs de todas as compras que pertencem à semana selecionada
                List<int> comprasDaMesmaSemanaIds = new List<int>();

                // Filtra em memória apenas as compras que pertencem à semana selecionada
                foreach (var compra in comprasFechadas)
                {
                    if (ObterSemanaDoMes(compra.DataCompra) == numeroSemana)
                    {
                        comprasDaMesmaSemanaIds.Add(compra.Id);
                    }
                }

                // Agrupa os itens pelo nome do artigo e conta quantas vezes foram comprados nessa semana
                var itensSugeridos = db.ItemComprasPlaneadas
                    // .Where(): Filtra a tabela, deixando passar apenas os itens que pertencem às compras daquela semana específica
                    .Where(i => comprasDaMesmaSemanaIds.Contains(i.CompraPlaneadaId))

                    // .Include(): Junta os dados da tabela de Artigos à consulta para podermos aceder ao Nome do artigo sem dar erro de referência nula
                    .Include(i => i.Artigos)

                    // .AsEnumerable(): Retira os dados do servidor da base de dados e traz para a memória do computador, permitindo processar o texto de forma mais segura
                    .AsEnumerable()

                    // .GroupBy(): Junta todos os registos que têm o mesmo nome de artigo no mesmo "grupo" 
                    .GroupBy(i => i.Artigos?.Nome ?? "Artigo Sem Nome")

                    // .Select(): Transforma cada grupo criado num novo formato, preenchendo o nosso objeto de transferência de dados (DTO)
                    .Select(g => new ItemSugeridoDto
                    {
                        NomeArtigo = g.Key,        // g.Key guarda o nome do artigo que serviu de base para o agrupamento 
                        Frequencia = g.Count()     // g.Count() conta quantos itens existem dentro deste grupo (ou seja, quantas vezes foi comprado no histórico)
                    })

                    // .OrderByDescending(): Ordena a lista final a começar no artigo com maior frequência até ao mais raro
                    .OrderByDescending(x => x.Frequencia)

                    // .ToList(): Converte todo este resultado final numa Lista oficial do C# e guarda-a na variável 'itensSugeridos'
                    .ToList();
                return itensSugeridos;
            }
        }

        // Método auxiliar privado que divide o dia do mês em 4 blocos de semanas
        private static int ObterSemanaDoMes(DateTime data)
        {
            int dia = data.Day;
            if (dia <= 7) return 1;  // Dias 1 a 7 -> Semana 1
            if (dia <= 14) return 2; // Dias 8 a 14 -> Semana 2
            if (dia <= 21) return 3; // Dias 15 a 21 -> Semana 3
            return 4;                // Dias 22 em diante -> Semana 4
        }

        // Método auxiliar privado para arredondar valores decimais a 2 casas decimais
        private static decimal MoretonRound(decimal value) => Math.Round(value, 2);
    }
}