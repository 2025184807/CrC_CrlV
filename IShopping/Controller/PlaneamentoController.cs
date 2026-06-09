using System;
using System.Collections.Generic;
using System.Linq;
using IShopping.Models;

namespace IShopping.Controller
{
    // Controller responsável pelo planeamento inicial das compras
    internal static class PlaneamentoController
    {
        // Método para obter todas as compras registadas na base de dados
        public static List<CompraPlaneada> ObterCompras()
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Converte a tabela completa de compras planeadas numa lista
                return db.ComprasPlaneadas.ToList();
            }
        }

        // Método para eliminar uma compra planeada, limpando primeiro os seus itens para evitar erros com as chaves estrangeiras
        public static bool EliminarCompra(int compraId, out string mensagem)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Procura a compra que se pretende apagar pelo ID
                CompraPlaneada compra =
                    db.ComprasPlaneadas.Find(compraId);

                // Se a compra não existir, interrompe o processo
                if (compra == null)
                {
                    mensagem = "Compra não encontrada.";
                    return false;
                }

                // ELIMINAR EM CASCATA: Procura todos os itens que pertencem a esta lista de compras
                var itens = db.ItemComprasPlaneadas
                              .Where(i => i.CompraPlaneadaId == compraId)
                              .ToList();

                // Se a compra tiver itens associados, remove-os todos de uma só vez com o RemoveRange
                if (itens.Any())
                {
                    db.ItemComprasPlaneadas.RemoveRange(itens);
                }

                // Depois de limpar os itens, remove a compra com segurança
                db.ComprasPlaneadas.Remove(compra);
                db.SaveChanges();

                mensagem = "Compra eliminada com sucesso.";
                return true;
            }
        }

        // Método para criar um novo cabeçalho de lista de compras planeada
        public static bool CriarCompra(string nomeCompra, out string mensagem)
        {
            mensagem = "";

            // Valida se o utilizador escreveu um nome válido (ignora espaços vazios)
            if (nomeCompra.Trim() == "")
            {
                mensagem = "Indique o nome da compra.";
                return false;
            }

            using (shoppingContext db = new shoppingContext())
            {
                // Instancia a nova compra, define a data de hoje e capturando o utilizador logado no sistema
                CompraPlaneada compra = new CompraPlaneada
                {
                    NomeCompra = nomeCompra,
                    DataCriacao = DateTime.Now,
                    CriadoPor = sessao.UtilizadorAtual
                };

                // Adiciona e grava o novo registo na base de dados
                db.ComprasPlaneadas.Add(compra);
                db.SaveChanges();

                mensagem = "Compra criada com sucesso.";
                return true;
            }
        }

        // Método para adicionar um item previsto (planeado em casa) a uma lista de compras existente
        public static bool AdicionarItemPrevisto(int compraId, int artigoid, int quantidadePrevista, out string mensagem)
        {
            mensagem = "";

            // Valida se a quantidade inserida faz sentido
            if (quantidadePrevista <= 0)
            {
                mensagem = "A quantidade prevista deve ser superior a zero.";
                return false;
            }

            using (shoppingContext db = new shoppingContext())
            {
                // Procura a compra através do id
                var compra = db.ComprasPlaneadas.Find(compraId);

                if (compra == null)
                {
                    mensagem = "Compra não encontrada.";
                    return false;
                }

                // Impede a adição de novos itens se a lista de compras já tiver sido fechada 
                if (compra.Fechada)
                {
                    mensagem = "A compra já se encontra fechada.";
                    return false;
                }

                // VALIDAÇÃO DE DUPLICADOS: Verifica se este mesmo artigo já está planeado nesta lista de compras para evitar duplicações
                bool existe = db.ItemComprasPlaneadas.Any(i =>
                    i.CompraPlaneadaId == compraId &&
                    i.ArtigoId == artigoid &&
                    i.Previsto);

                if (existe)
                {
                    mensagem = " Esse artigo já foi adicionado como item previsto.";
                    return false;
                }

                // Cria o novo item definindo-o como Previsto (true) e ainda Não Adquirido (false)
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

                // Atualiza os dados da compra para indicar quando e por quem foi modificada
                compra.DataHoraAlteracao = DateTime.Now;
                compra.AlteradoPor = sessao.UtilizadorAtual;

                db.SaveChanges();

                mensagem = "Item previsto adicionado com sucesso.";
                return true;
            }
        }

        // Método para exportar as compras fechadas do utilizador atual para um formato CSV
        public static string ExportarComprasFechadasParaCSV()
        {
            using (shoppingContext db = new shoppingContext())
            {
                // 1. Filtra a tabela para trazer apenas as compras do utilizador atual que já foram concluídas (Fechada == true)
                var comprasFechadas = db.ComprasPlaneadas
                    .Where(c => c.CriadoPor == sessao.UtilizadorAtual && c.Fechada == true)
                    .ToList();

                // 2. Cria a primeira linha de texto do CSV com os títulos das colunas separados por ponto e vírgula (;)
                string csv = "NomeCompra;DataCriacao;DataFechada;NomeArtigo;ArtigoPrevisto;PrecoUnitario;ArtigoNaoPrevisto;QuantidadePrevista;QuantidadeAdquirida\r\n";

                // 3. Percorre a lista de compras fechadas para extrair os respetivos itens
                foreach (var compra in comprasFechadas)
                {
                    // Força o carregamento da tabela relacional "Artigos" para conseguir ler o nome do produto
                    var itens = db.ItemComprasPlaneadas
                        .Include("Artigos")
                        .Where(i => i.CompraPlaneadaId == compra.Id)
                        .ToList();

                    // Percorre cada item da compra e formata as suas colunas em formato de texto separado por ponto e vírgula
                    foreach (var item in itens)
                    {
                        // Evita erros de referência nula: se o artigo existir mostra o nome, caso contrário mostra "Desconhecido"
                        string nomeArtigo = item.Artigos != null ? item.Artigos.Nome : "Desconhecido";

                        // Formata as datas para o padrão nacional e os decimais para exibir sempre duas casas decimais
                        string dataCriacao = compra.DataCriacao.ToString("dd/MM/yyyy");
                        string dataFecho = compra.DataFecho.HasValue ? compra.DataFecho.Value.ToString("dd/MM/yyyy") : "---";
                        string precoUnitario = (item.PrecoUnitario ?? 0).ToString("0.00");

                        string artigoPrevisto = "Sim"; 
                        string artigoNaoPrevisto = "Nao";

                        // Junta todas as variáveis numa única linha de texto e adiciona uma quebra de linha (\r\n) ao fim
                        csv += $"{compra.NomeCompra};{dataCriacao};{dataFecho};{nomeArtigo};{artigoPrevisto};{precoUnitario};{artigoNaoPrevisto};{item.QuantidadePrevista};{item.QuantidadeAdquirida}\r\n";
                    }
                }

                // Devolve a string gigante de texto formatada em CSV pronta a ser gravada num ficheiro pelo formulário
                return csv;
            }
        }
    }
}