using System;
using System.Linq;
using IShopping.Models;

namespace IShopping.Controller
{
    internal static class EstatisticasAvancadasController
    {
        public static ResumoComprasDto ObterResumoMensal(int mes, int ano)
        {
            using (shoppingContext db = new shoppingContext())
            {
                ResumoComprasDto resumo = new ResumoComprasDto();

                // ORÇAMENTO 
                var orcamento = db.Orcamentos
                    .FirstOrDefault(o => o.Mes == mes && o.Ano == ano);

                if (orcamento != null)
                {
                    resumo.OrcamentoMensal = orcamento.ValorOrcamento;
                }

                // COMPRAS DO MÊS
                var comprasFechadas = db.ComprasPlaneadas
                    .Where(c => c.DataCompra.Month == mes &&
                                c.DataCompra.Year == ano &&
                                c.Fechada)
                    .ToList();

                decimal total = 0m;

                foreach (var compra in comprasFechadas)
                {
                    var itens = db.ItemComprasPlaneadas
                        .Where(i => i.CompraPlaneadaId == compra.Id && i.Adquirido)
                        .ToList();

                    foreach (var item in itens)
                    {
                        if (item.PrecoUnitario.HasValue)
                        {
                            total += item.QuantidadeAdquirida * item.PrecoUnitario.Value;
                        }

                        if (item.Previsto)
                            resumo.TotalItensPrevistos++;
                        else
                            resumo.TotalItensNaoPrevistos++;
                    }

                    resumo.ComprasFechadas.Add(compra.NomeCompra);
                }

                resumo.TotalComprasMes = total;
                resumo.Diferenca = resumo.OrcamentoMensal - resumo.TotalComprasMes;

                int totalItens =
                    resumo.TotalItensPrevistos + resumo.TotalItensNaoPrevistos;

                if (totalItens > 0)
                {
                    resumo.PercentagemPrevistos =
                        (decimal)resumo.TotalItensPrevistos * 100 / totalItens;

                    resumo.PercentagemNaoPrevistos =
                        (decimal)resumo.TotalItensNaoPrevistos * 100 / totalItens;
                }

                return resumo;
            }
        }

        // SUGESTÃO ORÇAMENTO 
        public static SugestaoOrcamentoDto SugerirOrcamentoProximoMes()
        {
            using (shoppingContext db = new shoppingContext())
            {
                SugestaoOrcamentoDto sugestao = new SugestaoOrcamentoDto();

                var orcamentos = db.Orcamentos
                    .OrderByDescending(o => o.Ano)
                    .ThenByDescending(o => o.Mes)
                    .Take(6)
                    .ToList();

                if (orcamentos.Any())
                {
                    sugestao.MediaUltimosMeses =
                        orcamentos.Average(o => o.ValorOrcamento);

                    sugestao.SugestaoProximoMes =
                        sugestao.MediaUltimosMeses;
                }

                return sugestao;
            }
        }
    }
}