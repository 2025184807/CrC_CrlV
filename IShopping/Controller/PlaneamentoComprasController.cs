using System;
using System.Collections.Generic;
using System.Linq;
using IShopping.Models;


namespace IShopping.Controller
{
    internal static class PlaneamentoComprasController
    {
        public static List<CompraPlaneada> ObterCompras()
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.ComprasPlaneadas.ToList();
            }

        }
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

                compra.DataHoraAlteracao = DateTime.Now;
                compra.AlteradoPor = sessao.UtilizadorAtual;

                db.SaveChanges();

                mensagem = "Item previsto adicionado com sucesso.";
                return true;
            }
        }

    }
}
