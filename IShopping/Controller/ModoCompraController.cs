using System;
using System.Linq;
using IShopping.Models;

namespace IShopping.Controller
{
    internal static class ModoCompraController
    {
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

                CompraPlaneada compra = db.ComprasPlaneadas.Find(item.
               CompraPlaneadaId);

                if (compra == null || compra.Fechada)
                {
                    mensagem = "A compra não está disponível para edição.";
                    return false;
                }

                item.QuantidadeAdquirida = quantidadeAdquirida;
                item.PrecoUnitario = precoUnitario;
                item.Adquirido = true;


                compra.DataHoraAlteracao = DateTime.Now;
                compra.AlteradoPor = sessao.UtilizadorAtual;

                db.SaveChanges();

                mensagem = "Aquisição registada com sucesso.";
                return true;
            }
        }
        public static bool AdicionarItemNaoPrevisto(int compraId, int artigoid, int quantidadeAdquirida, decimal precoUnitario, string observacoes, out string mensagem)
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

                ItemCompraPlaneada item = new ItemCompraPlaneada
                {
                    CompraPlaneadaId = compraId,
                    ArtigoId = artigoid,
                    QuantidadePrevista = 0,
                    QuantidadeAdquirida = quantidadeAdquirida,
                    PrecoUnitario = precoUnitario,

                    Previsto = false,
                    Adquirido = true,
                    Observacoes = observacoes
                };

                db.ItemComprasPlaneadas.Add(item);

                compra.DataHoraAlteracao = DateTime.Now;
                compra.AlteradoPor = sessao.UtilizadorAtual;

                db.SaveChanges();

                mensagem = "Item não previsto adicionado com sucesso.";
                return true;
            }
        }
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
        public static decimal ObterTotalCompra(int compraId)
        {
            using (shoppingContext db = new shoppingContext())

            {
                var itens = db.ItemComprasPlaneadas
                .Where(i => i.CompraPlaneadaId == compraId && i.Adquirido);

                decimal total = 0m;

                foreach (var item in itens.ToList())
                {
                    if (item.PrecoUnitario.HasValue)
                    {
                        total += item.QuantidadeAdquirida * item.PrecoUnitario.Value;
                    }
                }

                return total;
            }
        }

    }
}
