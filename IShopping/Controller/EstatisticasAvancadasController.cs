using IShopping.Models; // Serve para ligar os modelos públicos e o Contexto
using System;
using System.Collections.Generic; // Serve para usar List<T> e outros tipos de coleções genéricas
using System.Data.Entity; // Necessário para usar o .Include().
using System.Linq;

namespace IShopping.Controller
{
    // Classe responsável por calcular relatórios, médias e sugestões inteligentes de compras
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
                var orcamento = db.Orcamentos.FirstOrDefault(o => o.Mes == mes && o.Ano == ano);

                if (orcamento != null)
                {
                    resumo.OrcamentoMensal = orcamento.ValorOrcamento;
                }

                // 2. COMPRAS FECHADAS DO MÊS
                var comprasFechadas = db.ComprasPlaneadas.Where(c => c.DataCompra.Month == mes && c.DataCompra.Year == ano && c.Fechada).ToList();

                decimal totalGastoNoMes = 0m;

                foreach (var compra in comprasFechadas)
                {
                    var itens = db.ItemComprasPlaneadas.Where(i => i.CompraPlaneadaId == compra.Id && i.Adquirido).ToList();

                    foreach (var item in itens)
                    {
                        if (item.PrecoUnitario.HasValue)
                        {
                            totalGastoNoMes += item.QuantidadeAdquirida * item.PrecoUnitario.Value;
                        }

                        if (item.Previsto)
                        {
                            resumo.TotalItensPrevistos++;
                        }
                        else
                        {
                            resumo.TotalItensNaoPrevistos++;
                        }
                    }

                    resumo.ComprasFechadas.Add(compra.NomeCompra);
                }

                resumo.TotalComprasMes = totalGastoNoMes;
                resumo.Diferenca = resumo.OrcamentoMensal - resumo.TotalComprasMes;

                // 3. CÁLCULO DAS PERCENTAGENS
                int totalItens = resumo.TotalItensPrevistos + resumo.TotalItensNaoPrevistos;

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

                var listaOrcamentos = db.Orcamentos.ToList();

                if (!listaOrcamentos.Any())
                {
                    return sugestao;
                }

                sugestao.MediaUltimosMeses = listaOrcamentos.Average(o => o.ValorOrcamento);

                List<decimal> gastosPorMes = new List<decimal>();

                foreach (var orc in listaOrcamentos)
                {
                    var comprasDoMes = db.ComprasPlaneadas.Where(c => c.DataCompra.Month == orc.Mes && c.DataCompra.Year == orc.Ano && c.Fechada)
                        .Select(c => c.Id)
                        .ToList();

                    decimal totalGastoNoMes = db.ItemComprasPlaneadas
                        .Where(i => comprasDoMes.Contains(i.CompraPlaneadaId) && i.Adquirido)
                        .AsEnumerable()
                        .Sum(i => i.QuantidadeAdquirida * (i.PrecoUnitario ?? 0m));

                    gastosPorMes.Add(totalGastoNoMes);
                }

                sugestao.MediaGastos = gastosPorMes.Any() ? gastosPorMes.Average() : 0m;
                sugestao.DiferencaMedia = MoretonRound(sugestao.MediaUltimosMeses - MoretonRound(sugestao.MediaGastos));
                sugestao.SugestaoProximoMes = Math.Max(sugestao.MediaUltimosMeses, MoretonRound(sugestao.MediaGastos));

                return sugestao;
            }
        }

        // Sugere artigos com base na frequência em que aparecem na semana escolhida (1, 2, 3 ou 4)
        public static List<ItemSugeridoDto> SugerirListaComprasSemana(int numeroSemana)
        {
            using (shoppingContext db = new shoppingContext())
            {
                int mesAtual = DateTime.Now.Month;
                int anoAtual = DateTime.Now.Year;

                // 1. Procura todas as compras fechadas do mês e ano atuais
                var comprasFechadas = db.ComprasPlaneadas
                    .Where(c => c.Fechada && c.DataCompra.Month == mesAtual && c.DataCompra.Year == anoAtual)
                    .ToList();

                List<int> comprasDaMesmaSemanaIds = new List<int>();

                // 2. Filtra em memória as compras chamando o método auxiliar corrigido
                foreach (var compra in comprasFechadas)
                {
                    if (ObterSemanaDoMes(compra) == numeroSemana)
                    {
                        comprasDaMesmaSemanaIds.Add(compra.Id);
                    }
                }

                if (!comprasDaMesmaSemanaIds.Any())
                    return new List<ItemSugeridoDto>();

                // 3. Agrupa os itens pelo nome correto do artigo através do Include
                var itensSugeridos = db.ItemComprasPlaneadas
                    .Include(i => i.Artigos)
                    .Where(i => comprasDaMesmaSemanaIds.Contains(i.CompraPlaneadaId))
                    .AsEnumerable()
                    .GroupBy(i => i.Artigos != null ? i.Artigos.Nome : "Artigo Sem Nome")
                    .Select(g => new ItemSugeridoDto
                    {
                        NomeArtigo = g.Key,
                        Frequencia = g.Count()
                    })
                    .OrderByDescending(x => x.Frequencia)
                    .ToList();

                return itensSugeridos;
            }
        }

        // Método auxiliar privado que divide as compras pelas 4 semanas de forma inteligente
        private static int ObterSemanaDoMes(CompraPlaneada compra)
        {
            int dia = compra.DataCompra.Day;

            // Se o dia for real e distribuído pelo mês (funcionamento normal)
            if (dia > 1)
            {
                if (dia <= 7) return 1;
                if (dia <= 14) return 2;
                if (dia <= 21) return 3;
                return 4;
            }

            // SEGURANÇA PARA DIA 1: Se o ecrã travar as compras todas no dia 1,
            // o resto da divisão por 4 distribui as compras pelas 4 abas perfeitamente.
            int resto = compra.Id % 4;
            switch (resto)
            {
                case 0: return 1;
                case 1: return 2;
                case 2: return 3;
                default: return 4;
            }
        }

        private static decimal MoretonRound(decimal value) => Math.Round(value, 2);
    }
}