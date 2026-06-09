using IShopping.Models;
using System.Collections.Generic;
using System.Linq;

namespace IShopping.Controller
{
    // Classe responsável por gerir todas as operações de base de dados (CRUD) das categorias/tipos de artigo
    internal class TipoArtigoController
    {
        // LISTAR
        // Recupera a lista completa de todos os tipos de artigo registados no sistema
        public static List<TipoArtigo> Listar()
        {
            // Abre a ligação com a base de dados utilizando o contexto dentro de um bloco using
            using (shoppingContext db = new shoppingContext())
            {
                // Converte a tabela de TipoArtigos da base de dados numa lista
                return db.TipoArtigos.ToList();
            }
        }

        // INSERIR
        // Cria e grava uma nova categoria/tipo de artigo na base de dados
        public static void Inserir(string nome)
        {
            using (shoppingContext db = new shoppingContext())
            {

                TipoArtigo tipo = new TipoArtigo(); // Instancia um novo objeto do modelo TipoArtigo

                tipo.Nome = nome; // Atribui o nome recebido por parâmetro à propriedade do objeto

                // Adiciona o novo objeto à tabela 
                db.TipoArtigos.Add(tipo);
                db.SaveChanges();
            }
        }

        // EDITAR
        // Localiza um tipo de artigo existente através do ID e atualiza o seu nome
        public static void Editar(int id, string nome)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Procura o registo diretamente na base de dados através da sua Chave Primária (ID)
                TipoArtigo tipo = db.TipoArtigos.Find(id);

                // Valida se o registo foi realmente encontrado para evitar erros de referência nula
                if (tipo != null)
                {
                    // Substitui o nome antigo pelo novo nome introduzido
                    tipo.Nome = nome;

                    // Guarda as alterações
                    db.SaveChanges();
                }
            }
        }

        // PROCURAR POR ID
        // Função que procura e devolve os dados de um único tipo de artigo pelo seu ID
        public static TipoArtigo ProcurarPorId(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Utiliza o método .FirstOrDefault() para encontrar o primeiro registo que coincida com o ID
                // Se não encontrar nenhum, devolve o valor padrão (null)
                return db.TipoArtigos.FirstOrDefault(t => t.Id == id);
            }
        }

        // ELIMINAR
        // Remove permanentemente um tipo de artigo da base de dados através do seu ID
        public static void Eliminar(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Procura o tipo de artigo correspondente na base de dados utilizando o ID
                TipoArtigo tipo = db.TipoArtigos.Find(id);

                // Verifica se o registo existe antes de tentar apagar
                if (tipo != null)
                {
                    db.TipoArtigos.Remove(tipo);
                    db.SaveChanges();
                }
            }
        }
    }
}