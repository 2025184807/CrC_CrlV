using IShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopping.Controller
{
    internal class AlteracaoPlaneadaController
    {
        // 1. Obter todas as compras registadas para listar na Grid principal
        public static List<CompraPlaneada> ObterCompras()
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.ComprasPlaneadas.ToList();
            }
        }
        // LISTAR
        public static List<Artigo> Listar() // Método para listar os tipos de artigo, retorna uma lista de objetos do tipo TipoArtigo
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.Artigos.ToList();
            }
        }
        // 2. Criar uma nova compra planeada (valida se o nome não está vazio)
        public static bool CriarCompra(string nomeCompra, out string mensagem)
        {
            mensagem = "";

            if (string.IsNullOrWhiteSpace(nomeCompra))
            {
                mensagem = "Indique o nome da compra.";
                return false;
            }

            using (shoppingContext db = new shoppingContext())
            {
                // impedir nomes repetidos (não fechadas ou todas, como quiseres)
                bool existe = db.ComprasPlaneadas
                    .Any(c => c.NomeCompra == nomeCompra);

                if (existe)
                {
                    mensagem = "Já existe uma compra com esse nome.";
                    return false;
                }

                CompraPlaneada compra = new CompraPlaneada
                {
                    NomeCompra = nomeCompra,

                    // mês atual da compra
                    DataCompra = new DateTime(
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        1
                    ),

                    DataCriacao = DateTime.Now,
                    DataHoraAlteracao = DateTime.Now,

                    CriadoPor = sessao.UtilizadorAtual,
                    AlteradoPor = sessao.UtilizadorAtual,

                    Fechada = false
                };

                db.ComprasPlaneadas.Add(compra);
                db.SaveChanges();

                mensagem = "Compra criada com sucesso.";
                return true;
            }
        }
        // PROCURAR ITEM POR ID
        public static ItemCompraPlaneada ProcurarItemPorId(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.ItemComprasPlaneadas
                    .FirstOrDefault(i => i.Id == id);
            }
        }

        // ALTERAR / EDITAR QUANTIDADE DO ITEM
        public static void AlterarItem(int id, int novaQuantidade)
        {
            using (shoppingContext db = new shoppingContext())
            {
                ItemCompraPlaneada item = db.ItemComprasPlaneadas.Find(id);

                if (item != null)
                {
                    item.QuantidadePrevista = novaQuantidade;

                    db.SaveChanges();
                }
            }
        }

        // ELIMINAR ITEM
        public static void EliminarItem(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                ItemCompraPlaneada item = db.ItemComprasPlaneadas.Find(id);

                if (item != null)
                {
                    db.ItemComprasPlaneadas.Remove(item);

                    db.SaveChanges();
                }
            }
        }

        // 3. Alterar os dados de uma compra existente ou fechá-la definitivamente
        public static bool AlterarCompra(int compraId, string novoNome, DateTime novaData, bool fecharCompra, out string mensagem)
        {
            mensagem = "";

            if (novoNome.Trim() == "")
            {
                mensagem = "Indique o nome da compra.";
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
                    mensagem = "Não é possível alterar uma compra que já se encontra fechada.";
                    return false;
                }

                // Atualiza os dados do cabeçalho
                compra.NomeCompra = novoNome;
                compra.DataCompra = novaData;
                compra.DataHoraAlteracao = DateTime.Now;
                compra.AlteradoPor = sessao.UtilizadorAtual;

                // Se a checkbox de fecho foi marcada
                if (fecharCompra)
                {
                    compra.Fechada = true;
                    compra.DataFecho = DateTime.Now;
                    compra.FechadoPor = sessao.UtilizadorAtual;
                }

                db.SaveChanges();

                mensagem = fecharCompra ? "Compra fechada e guardada com sucesso." : "Compra alterada com sucesso.";
                return true;
            }
        }

        // 4. Adicionar um artigo previsto à lista de itens da compra
        public static bool AdicionarItemPrevisto(
    int compraId,
    int artigoid,
    int quantidadePrevista,
    out string mensagem)
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

                var artigo = db.Artigos.Find(artigoid);

                if (artigo == null)
                {
                    mensagem = "Artigo não encontrado.";
                    return false;
                }

                // evita duplicados
                bool existe = db.ItemComprasPlaneadas.Any(i =>
                    i.CompraPlaneadaId == compraId &&
                    i.ArtigoId == artigoid &&
                    i.Previsto);

                if (existe)
                {
                    mensagem = "Esse artigo já foi adicionado como item previsto.";
                    return false;
                }

                ItemCompraPlaneada item = new ItemCompraPlaneada
                {
                    CompraPlaneadaId = compraId,
                    ArtigoId = artigoid,
                    QuantidadePrevista = quantidadePrevista,
                    QuantidadeAdquirida = 0,
                    PrecoUnitario = artigo.Preco, 
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
