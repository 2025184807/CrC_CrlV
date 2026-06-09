using IShopping.Models;
using System.Collections.Generic;
using System.Data.Entity; // Necessário para usar o .Include().
using System.Linq;

namespace IShopping.Controller
{
    // Classe interna ArtigoController responsável por gerir todas as operações de base de dados (CRUD) dos artigos.
    internal class ArtigoController
    {
        // Função Listar() serve para listar uma lista de artigos.
        public static List<Artigo> Listar()
        {
            using (shoppingContext db = new shoppingContext())
            {
                // O .Include serve para trazer o TipoArtigo juntamente com o Artigo.
                // O .ToList() executa e converte o resultado numa lista em memória.
                return db.Artigos.Include(a => a.TipoArtigo).ToList();
            }
        }

        // Função filtar por tipo que devolve uma lista de artigos filtrados por um Tipo de Artigo específico.
        public static List<Artigo> FiltrarPorTipo(int tipoId)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Utiliza a cláusula .Where() para filtrar na base de dados apenas os registos onde a chave estrangeira coincide com o ID recebido.
                return db.Artigos.Where(a => a.TipoArtigoId == tipoId).ToList();
            }
        }

        // Função inserer que recebe os dados do ecrã e cria um novo registo de artigo na base de dados.
        public static void Inserir(string nome, int tipoArtigoId, decimal preco)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Instancia um novo objeto do modelo Artigo.
                Artigo artigo = new Artigo();

                // Atribui os valores recebidos por parâmetro às propriedades do objeto.
                artigo.Nome = nome;
                artigo.TipoArtigoId = tipoArtigoId;
                artigo.Preco = preco;

                // Adiciona o novo objeto à tabela de Artigos.
                db.Artigos.Add(artigo);

                // Guarda as alterações.
                db.SaveChanges();
            }
        }

        // Função editar.
        public static void Editar(int id, string nome, int tipoArtigoId, decimal preco)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Procura o artigo diretamente na base de dados através da sua Chave Primária (ID).
                Artigo artigo = db.Artigos.Find(id);

                // Valida se o artigo foi realmente encontrado para evitar erros de referência nula.
                if (artigo != null)
                {
                    // Aplica os novos dados vindos do formulário sobre o registo encontrado
                    artigo.Nome = nome;
                    artigo.TipoArtigoId = tipoArtigoId;
                    artigo.Preco = preco;

                    // Guarda as alterações.
                    db.SaveChanges();
                }
            }
        }

        // Função eliminar.
        // Método que remove um artigo da base de dados através do seu ID.
        public static void Eliminar(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Procura o artigo correspondente na base de dados usando o ID.
                Artigo artigo = db.Artigos.Find(id);

                // Verifica se o artigo existe antes de tentar apagar.
                if (artigo != null)
                {
                    // Marca o artigo encontrado para remover-lo.
                    db.Artigos.Remove(artigo);
                    db.SaveChanges();
                }
            }
        }

        // Função Procurar por ID.
        // Função que procura e devolve os dados completos de um único artigo pelo seu ID.
        public static Artigo ProcurarPorId(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Utiliza o método .FirstOrDefault() para encontrar o primeiro artigo que coincida com o ID.
                // Se não encontrar nenhum, devolve o valor padrão (null).
                return db.Artigos.FirstOrDefault(a => a.Id == id);
            }
        }
    }
}