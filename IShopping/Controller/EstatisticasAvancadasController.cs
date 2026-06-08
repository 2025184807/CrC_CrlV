using IShopping.Models; // Essencial para reconhecer os teus DTOs públicos e o Contexto
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IShopping.Controller
{
    internal static class EstatisticasAvancadasController
    {
    
        public static ResumoComprasDto ObterResumoMensal(int mes, int ano)
        {
            using (shoppingContext db = new shoppingContext())
            {
                ResumoComprasDto resumo = new ResumoComprasDto();

                // 1. ORÇAMENTO DO MÊS
                var orcamento = db.Orcamentos
                    .FirstOrDefault(o => o.Mes == mes && o.Ano == ano);

                if (orcamento != null)
                {
                    resumo.OrcamentoMensal = orcamento.ValorOrcamento;
                }

                // 2. COMPRAS FECHADAS DO MÊS
                var comprasFechadas = db.ComprasPlaneadas
                    .Where(c => c.DataCompra.Month == mes &&
                                c.DataCompra.Year == ano &&
                                c.Fechada)
                    .ToList();

                decimal totalGastoNoMes = 0m;

                foreach (var compra in comprasFechadas)
                {
                    var itens = db.ItemComprasPlaneadas
                        .Where(i => i.CompraPlaneadaId == compra.Id && i.Adquirido)
                        .ToList();

                    foreach (var item in itens)
                    {
                        if (item.PrecoUnitario.HasValue)
                        {
                            totalGastoNoMes += item.QuantidadeAdquirida * item.PrecoUnitario.Value;
                        }

                        if (item.Previsto)
                            resumo.TotalItensPrevistos++;
                        else
                            resumo.TotalItensNaoPrevistos++;
                    }

                    resumo.ComprasFechadas.Add(compra.NomeCompra);
                }

                resumo.TotalComprasMes = totalGastoNoMes;
                resumo.Diferenca = resumo.OrcamentoMensal - resumo.TotalComprasMes;

                // 3. CÁLCULO DAS PERCENTAGENS (Requisito 20.b)
                int totalItens = resumo.TotalItensPrevistos + resumo.TotalItensNaoPrevistos;

                if (totalItens > 0)
                {
                    resumo.PercentagemPrevistos = (decimal)resumo.TotalItensPrevistos * 100 / totalItens;
                    resumo.PercentagemNaoPrevistos = (decimal)resumo.TotalItensNaoPrevistos * 100 / totalItens;
                }

                return resumo;
            }
        }

        /// Sugerir orçamento para o próximo mês com base no histórico real.
        public static SugestaoOrcamentoDto SugerirOrcamentoProximoMes()
        {
            using (shoppingContext db = new shoppingContext())
            {
                SugestaoOrcamentoDto sugestao = new SugestaoOrcamentoDto();

                // Obtemos todos os orçamentos guardados no histórico
                var listaOrcamentos = db.Orcamentos.ToList();

                if (!listaOrcamentos.Any())
                    return sugestao;

                // A. Média dos Orçamentos (Mapeado para a tua propriedade do DTO)
                sugestao.MediaUltimosMeses = listaOrcamentos.Average(o => o.ValorOrcamento);

                // B. Calcular os Gastos Reais de cada um dos meses anteriores
                List<decimal> gastosPorMes = new List<decimal>();

                foreach (var orc in listaOrcamentos)
                {
                    var comprasDoMes = db.ComprasPlaneadas
                        .Where(c => c.DataCompra.Month == orc.Mes &&
                                    c.DataCompra.Year == orc.Ano &&
                                    c.Fechada)
                        .Select(c => c.Id)
                        .ToList();

                    decimal totalGastoNoMes = db.ItemComprasPlaneadas
                        .Where(i => comprasDoMes.Contains(i.CompraPlaneadaId) && i.Adquirido)
                        .AsEnumerable()
                        .Sum(i => i.QuantidadeAdquirida * (i.PrecoUnitario ?? 0m));

                    gastosPorMes.Add(totalGastoNoMes);
                }

                sugestao.MediaGastos = gastosPorMes.Any() ? gastosPorMes.Average() : 0m;

                // C. Diferença Média
                sugestao.DiferencaMedia = MoretonRound(sugestao.MediaUltimosMeses - MoretonRound(sugestao.MediaGastos));

                // D. Sugestão Inteligente (Garante que se gastarem mais do que orçamentam, a sugestão sobe)
                sugestao.SugestaoProximoMes = Math.Max(sugestao.MediaUltimosMeses, MoretonRound(sugestao.MediaGastos));

                return sugestao;
            }
        }

        /// Requisito 20.c: Sugerir lista de compras tendo em conta a semana selecionada (1, 2, 3 ou 4)
        /// cruzando com os artigos mais frequentes dessa mesma semana nos meses anteriores.
        public static List<ItemSugeridoDto> SugerirListaComprasSemana(int numeroSemana)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // 1. Procurar todas as compras que já foram fechadas
                var comprasFechadas = db.ComprasPlaneadas
                    .Where(c => c.Fechada)
                    .ToList();

                List<int> comprasDaMesmaSemanaIds = new List<int>();

                // 2. Filtrar em memória as que batem certo com a semana pedida
                foreach (var compra in comprasFechadas)
                {
                    if (ObterSemanaDoMes(compra.DataCompra) == numeroSemana)
                    {
                        comprasDaMesmaSemanaIds.Add(compra.Id);
                    }
                }

                // 3. Agrupar e contar a frequência de cada artigo comprado nessa semana específica
                var itensSugeridos = db.ItemComprasPlaneadas
                    .Where(i => comprasDaMesmaSemanaIds.Contains(i.CompraPlaneadaId))
                    .Include(i => i.Artigos) // Carrega a tabela relacional de Artigos para prevenir erros de NullReference
                    .AsEnumerable() // Traz para memória para tratar strings corretamente
                    .GroupBy(i => i.Artigos?.Nome ?? "Artigo Sem Nome")
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


        /// Método privado auxiliar para mapear qualquer data para a respetiva semana do mês (1 a 4)
        private static int ObterSemanaDoMes(DateTime data)
        {
            int dia = data.Day;
            if (dia <= 7) return 1;
            if (dia <= 14) return 2;
            if (dia <= 21) return 3;
            return 4;
        }

        private static decimal MoretonRound(decimal value) => Math.Round(value, 2);
    }
}