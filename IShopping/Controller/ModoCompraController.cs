using IShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IShopping.Controller
{
    // Controller responsável pelas operações do "Modo Compra" (quando o utilizador está efetivamente no supermercado a comprar)
    internal static class ModoCompraController
    {
        // Regista que um item que já estava planeado/previsto foi comprado
        public static bool RegistarAquisicaoItemPrevisto(int itemId, int quantidadeAdquirida, decimal precoUnitario, out string mensagem)
        {
            mensagem = "";

            // Valida se a quantidade comprada é válida
            if (quantidadeAdquirida <= 0)
            {
                mensagem = "A quantidade adquirida deve ser superior a zero.";
                return false;
            }

            // Valida se o preço não é negativo
            if (precoUnitario < 0)
            {
                mensagem = "O preço unitário não pode ser negativo.";
                return false;
            }

            using (shoppingContext db = new shoppingContext())
            {
                // Procura o item planeado na base de dados
                ItemCompraPlaneada item = db.ItemComprasPlaneadas.Find(itemId);

                if (item == null)
                {
                    mensagem = "Item não encontrado.";
                    return false;
                }

                // Procura a compra à qual este item pertence
                CompraPlaneada compra = db.ComprasPlaneadas.Find(item.CompraPlaneadaId);

                // Se a compra não existir ou já estiver trancada/fechada, impede a edição
                if (compra == null || compra.Fechada)
                {
                    mensagem = "A compra não está disponível para edição.";
                    return false;
                }

                // Atualiza o item com os valores reais do carrinho de compras
                item.QuantidadeAdquirida = quantidadeAdquirida;
                item.PrecoUnitario = precoUnitario;
                item.Adquirido = true; // Marca como comprado

                // Grava quem fez a última alteração na compra
                compra.AlteradoPor = sessao.UtilizadorAtual;

                db.SaveChanges();

                mensagem = "Aquisição registada com sucesso.";
                return true;
            }
        }

        // Remove um item do carrinho ou anula a sua compra se ele for um item previsto
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

                // Se o item não era previsto, apaga-se de vez
                if (!item.Previsto)
                {
                    db.ItemComprasPlaneadas.Remove(item);
                    mensagem = "Item não previsto removido com sucesso.";
                }
                // Se era um item previsto, não o apagamos para não estragar o planeamento inicial; apenas pomos a quantidade adquirida a zero
                else
                {
                    item.QuantidadeAdquirida = 0;
                    item.Adquirido = false; // Deixa de estar marcado como comprado
                    mensagem = "Registo de aquisição do item previsto anulado (preço mantido).";
                }

                compra.AlteradoPor = sessao.UtilizadorAtual;
                db.SaveChanges(); //Salva as alterações

                return true;
            }
        }

        // Adiciona um item que não tinha sido planeado inicialmente (
        public static bool AdicionarItemNaoPrevisto(int compraId, int artigoId, int quantidadeAdquirida, decimal precoUnitario, string observacoes, out string mensagem)
        {
            mensagem = "";

            // Validações básicas de segurança para quantidades e preços
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

                // Cria e configura uma nova linha (registo) para a tabela de itens da compra
                ItemCompraPlaneada item = new ItemCompraPlaneada
                {
                    CompraPlaneadaId = compraId, // Associa este item ao ID da compra que está aberta no ecrã
                    ArtigoId = artigoId, // Define qual é o artigo que está a ser comprado através do seu ID
                    QuantidadePrevista = 0,

                    QuantidadeAdquirida = quantidadeAdquirida, // Regista a quantidade real que o utilizador acabou de meter dentro da lista
                    PrecoUnitario = precoUnitario, // Guarda o preço unitário que está a ser cobrado pelo artigo no momento da compra   
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

        // Fecha e tranca a lista de compras definitivamente, registando metadados de auditoria
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

                // Fecha a compra e guarda a data atual e o utilizador responsável pelo fecho
                compra.Fechada = true;
                compra.DataFecho = DateTime.Now;
                compra.FechadoPor = sessao.UtilizadorAtual;

                db.SaveChanges();

                mensagem = "Compra fechada com sucesso.";
                return true;
            }
        }

        // Calcula a soma total do dinheiro gasto em todos os itens comprados (Quantidade * Preço)
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

        // Procura e devolve o teto máximo de orçamento definido para o mês/ano desta compra
        public static decimal ObterOrcamentoCompra(int compraId)
        {
            using (shoppingContext db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.Find(compraId);
                if (compra == null)
                {
                    return 0;
                }
                   
                // Cruza o mês e ano da compra com a tabela de Orçamentos
                var orcamento = db.Orcamentos.FirstOrDefault(o =>
                    o.Mes == compra.DataCompra.Month &&
                    o.Ano == compra.DataCompra.Year);

                if (orcamento != null)
                {
                    return orcamento.ValorOrcamento;
                }
                else
                {
                    return 0;
                }
            }
        }

        // Calcula o saldo restante (Orçamento do mês - Total gasto até ao momento)
        public static decimal ObterSaldoDisponivel(int compraId)
        {
            decimal orcamento = ObterOrcamentoCompra(compraId);
            decimal totalGasto = ObterTotalCompra(compraId);
            return orcamento - totalGasto; // Devolve a diferença
        }

        // Retorna uma lista de todas as compras que ainda não foram fechadas, organizadas pelas mais antigas primeiro
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