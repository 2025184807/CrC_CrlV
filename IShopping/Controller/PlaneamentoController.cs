using System;
using System.Collections.Generic;
using System.Linq;
using IShopping.Models;

namespace IShopping.Controller
{
    internal static class PlaneamentoController
    {
        // Método para obter todas as compras registadas na base de dados
        public static List<CompraPlaneada> ObterCompras()
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.ComprasPlaneadas.ToList();
            }
        }
        // Método para eliminar uma compra planeada, verificando se existe e devolvendo uma mensagem de sucesso ou erro
        public static bool EliminarCompra(int compraId, out string mensagem)
        {
            using (shoppingContext db = new shoppingContext())
            {
                CompraPlaneada compra =
                    db.ComprasPlaneadas.Find(compraId);

                if (compra == null)
                {
                    mensagem = "Compra não encontrada.";
                    return false;
                }

                // Eliminar todos os itens da compra
                var itens = db.ItemComprasPlaneadas
                              .Where(i => i.CompraPlaneadaId == compraId)
                              .ToList();

                if (itens.Any())
                {
                    db.ItemComprasPlaneadas.RemoveRange(itens);
                }

                // Eliminar a compra
                db.ComprasPlaneadas.Remove(compra);

                db.SaveChanges();

                mensagem = "Compra eliminada com sucesso.";
                return true;
            }
        }
        // Método para criar uma nova compra planeada (valida apenas o nome da compra)
        public static bool CriarCompra(string nomeCompra, out string mensagem)
        {
            mensagem = "";

            if (nomeCompra.Trim() == "")
            {
                mensagem = "Indique o nome da compra.";
                return false;
            }

            using (shoppingContext db = new shoppingContext())
            {
                CompraPlaneada compra = new CompraPlaneada
                {
                    NomeCompra = nomeCompra,
                    DataCriacao = DateTime.Now,
                    CriadoPor = sessao.UtilizadorAtual
                };

                db.ComprasPlaneadas.Add(compra);
                db.SaveChanges();

                mensagem = "Compra criada com sucesso.";
                return true;
            }
        }

        // Método para adicionar um item previsto a uma compra existente
        public static bool AdicionarItemPrevisto(int compraId, int artigoid, int quantidadePrevista, out string mensagem)
        {
            mensagem = "";

            if (quantidadePrevista <= 0)
            {
                mensagem = "A quantidade prevista deve ser superior a zero.";
                return false;
            }

            using (shoppingContext db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.Find(compraId);

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

                // Verifica se o artigo já existe nesta compra como previsto
                bool existe = db.ItemComprasPlaneadas.Any(i =>
                    i.CompraPlaneadaId == compraId &&
                    i.ArtigoId == artigoid &&
                    i.Previsto);

                if (existe)
                {
                    mensagem = " Esse artigo já foi adicionado como item previsto.";
                    return false;
                }

                ItemCompraPlaneada item = new ItemCompraPlaneada
                {
                    CompraPlaneadaId = compraId,
                    ArtigoId = artigoid,
                    QuantidadePrevista = quantidadePrevista,
                    QuantidadeAdquirida = 0,
                    Previsto = true,
                    Adquirido = false,
                    Observacoes = ""
                };

                db.ItemComprasPlaneadas.Add(item);

                // Define dados de auditoria
                compra.DataHoraAlteracao = DateTime.Now;
                compra.AlteradoPor = sessao.UtilizadorAtual;

                db.SaveChanges();

                mensagem = "Item previsto adicionado com sucesso.";
                return true;
            }
        }
    }
}