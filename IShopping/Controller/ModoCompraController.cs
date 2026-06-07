using IShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IShopping.Controller
{
    internal static class ModoCompraController
    {
        // Método para registrar a aquisição de um item previsto, atualizando a quantidade adquirida e o preço unitário, e mantendo a data original da compra para não quebrar o vínculo com o Orçamento Mensal
        public static bool RegistarAquisicaoItemPrevisto(int itemId, int quantidadeAdquirida, decimal precoUnitario, out string mensagem)
        {
            mensagem = "";

            if (quantidadeAdquirida <= 0)
            {
                mensagem = "A quantidade adquirida deve ser superior a zero.";
                return false;
            }

            if (precoUnitario < 0)
            {
                mensagem = "O preço unitário não pode ser negativo.";
                return false;
            }

            using (shoppingContext db = new shoppingContext())
            {
                ItemCompraPlaneada item = db.ItemComprasPlaneadas.Find(itemId);

                if (item == null)
                {
                    mensagem = "Item não encontrado.";
                    return false;
                }

                CompraPlaneada compra = db.ComprasPlaneadas.Find(item.CompraPlaneadaId);

                if (compra == null || compra.Fechada)
                {
                    mensagem = "A compra não está disponível para edição.";
                    return false;
                }

                item.QuantidadeAdquirida = quantidadeAdquirida;
                item.PrecoUnitario = precoUnitario;
                item.Adquirido = true;

                // Mantém a data original da compra
                compra.AlteradoPor = sessao.UtilizadorAtual;

                db.SaveChanges();

                mensagem = "Aquisição registada com sucesso.";
                return true;
            }
        }

        // Método para remover um item de uma compra, permitindo a remoção de itens não previstos ou a anulação do registro de aquisição de itens previstos, mantendo a data original da compra para não quebrar o vínculo com o Orçamento Mensal
        public static bool RemoverItemCompra(int itemId, out string mensagem)
        {
            mensagem = "";

            using (shoppingContext db = new shoppingContext())
            {
                ItemCompraPlaneada item = db.ItemComprasPlaneadas.Find(itemId);

                if (item == null)
                {
                    mensagem = "Item não encontrado.";
                    return false;
                }

                CompraPlaneada compra = db.ComprasPlaneadas.Find(item.CompraPlaneadaId);

                if (compra == null || compra.Fechada)
                {
                    mensagem = "A compra não está disponível para edição.";
                    return false;
                }

                // Se o item NÃO era previsto, remove-se completamente da base de dados
                if (!item.Previsto)
                {
                    db.ItemComprasPlaneadas.Remove(item);
                    mensagem = "Item não previsto removido com sucesso.";
                }
                // Se era um item previsto, anula-se a aquisição mas MANTÉM-SE o preço unitário
                else
                {
                    item.QuantidadeAdquirida = 0;
                    item.Adquirido = false;
                    // item.PrecoUnitario fica como está, sem ser alterado para null
                    mensagem = "Registo de aquisição do item previsto anulado (preço mantido).";
                }

                compra.AlteradoPor = sessao.UtilizadorAtual;
                db.SaveChanges();

                return true;
            }
        }

        // Método para adicionar um item não previsto a uma compra, permitindo registrar a aquisição de um artigo que não estava inicialmente planejado
        public static bool AdicionarItemNaoPrevisto(int compraId, int artigoId, int quantidadeAdquirida, decimal precoUnitario, string observacoes, out string mensagem)
        {
            mensagem = "";

            if (quantidadeAdquirida <= 0)
            {
                mensagem = "A quantidade adquirida deve ser superior a zero.";
                return false;
            }

            if (precoUnitario < 0)
            {
                mensagem = "O preço unitário não pode ser negativo.";
                return false;
            }

            using (shoppingContext db = new shoppingContext())
            {
                CompraPlaneada compra = db.ComprasPlaneadas.Find(compraId);

                if (compra == null)
                {
                    mensagem = "Compra não encontrada.";
                    return false;
                }

                if (compra.Fechada)
                {
                    mensagem = "A compra já se encontra fechada.";
                    return false;
                }

                // Cria o novo item que não estava planeado
                ItemCompraPlaneada item = new ItemCompraPlaneada
                {
                    CompraPlaneadaId = compraId,
                    ArtigoId = artigoId,
                    QuantidadePrevista = 0,
                    QuantidadeAdquirida = quantidadeAdquirida,
                    PrecoUnitario = precoUnitario,
                    Previsto = false,
                    Adquirido = true,
                    Observacoes = observacoes
                };

                db.ItemComprasPlaneadas.Add(item);
                compra.AlteradoPor = sessao.UtilizadorAtual;

                db.SaveChanges();

                mensagem = "Item não previsto adicionado com sucesso.";
                return true;
            }
        }

        // Método para fechar uma compra, marcando-a como fechada e registrando a data de fechamento e o usuário responsável
        public static bool FecharCompra(int compraId, out string mensagem)
        {
            mensagem = "";

            using (shoppingContext db = new shoppingContext())
            {
                CompraPlaneada compra = db.ComprasPlaneadas.Find(compraId);

                if (compra == null)
                {
                    mensagem = "Compra não encontrada.";
                    return false;
                }

                if (compra.Fechada)
                {
                    mensagem = "A compra já se encontra fechada.";
                    return false;
                }

                compra.Fechada = true;
                compra.DataFecho = DateTime.Now;
                compra.FechadoPor = sessao.UtilizadorAtual;

                db.SaveChanges();

                mensagem = "Compra fechada com sucesso.";
                return true;
            }
        }

        // Método para calcular o total gasto em uma compra, somando o valor de todos os itens adquiridos
        public static decimal ObterTotalCompra(int compraId)
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.ItemComprasPlaneadas
                    .Where(i => i.CompraPlaneadaId == compraId && i.Adquirido)
                    .ToList()
                    .Sum(item => item.QuantidadeAdquirida * (item.PrecoUnitario ?? 0));
            }
        }

        // Método para obter o orçamento mensal associado a uma compra, com base na data da compra
        public static decimal ObterOrcamentoCompra(int compraId)
        {
            using (shoppingContext db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.Find(compraId);
                if (compra == null) return 0;

                var orcamento = db.Orcamentos.FirstOrDefault(o =>
                    o.Mes == compra.DataCompra.Month &&
                    o.Ano == compra.DataCompra.Year);

                return orcamento?.ValorOrcamento ?? 0;
            }
        }

        // Método para obter o saldo disponível para uma compra, subtraindo o total gasto do orçamento mensal
        public static decimal ObterSaldoDisponivel(int compraId)
        {
            decimal orcamento = ObterOrcamentoCompra(compraId);
            decimal totalGasto = ObterTotalCompra(compraId);
            return orcamento - totalGasto;
        }

        // Método para obter todas as compras em aberto, ordenadas por data de compra
        public static List<CompraPlaneada> ObterComprasEmAberto()
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.ComprasPlaneadas
                    .Where(c => !c.Fechada)
                    .OrderBy(c => c.DataCompra)
                    .ToList();
            }
        }
    }
}