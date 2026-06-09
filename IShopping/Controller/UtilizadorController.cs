using System.Collections.Generic;
using System.Linq;

namespace IShopping.Controller
{
    // Classe responsável por gerir todas as operações de base de dados (CRUD) dos utilizadores do sistema
    internal class UtilizadorController
    {
        // LISTAR
        // Recupera a lista completa de todos os utilizadores registados no sistema
        public static List<Utilizador> Listar()
        {
            // Abre a ligação com a base de dados utilizando o contexto dentro de um bloco using
            using (shoppingContext db = new shoppingContext())
            {
                // Converte a tabela de Utilizadores da base de dados numa lista
                return db.Utilizadores.ToList();
            }
        }

        // INSERIR
        // Cria e grava um novo utilizador na base de dados
        public static void Inserir(string username, string password, string criadoPor)
        {
            using (shoppingContext db = new shoppingContext())
            {
                Utilizador utilizador = new Utilizador();

                // Atribui os valores recebidos por parâmetro às propriedades do objeto
                utilizador.Username = username;
                utilizador.Password = password;
                utilizador.CriadoPor = criadoPor;

                // Adiciona o novo objeto à tabela 
                db.Utilizadores.Add(utilizador);

                // Guarda as alterações
                db.SaveChanges();
            }
        }

        // PROCURAR POR ID
        // Função que procura e devolve os dados de um único utilizador através do seu ID
        public static Utilizador ProcurarPorId(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Utiliza o método .FirstOrDefault() para encontrar o primeiro registo que coincida com o ID
                // Se não encontrar nenhum, devolve o valor padrão (null)
                return db.Utilizadores.FirstOrDefault(u => u.Id == id);
            }
        }

        // EDITAR
        // Localiza um utilizador existente através do ID e atualiza os seus dados de acesso
        public static void Editar(int id, string username, string password, string alteradoPor)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Procura o utilizador correspondente na base de dados usando o ID
                Utilizador utilizador = db.Utilizadores.FirstOrDefault(u => u.Id == id);

                // Valida se o utilizador foi realmente encontrado para evitar erros de referência nula
                if (utilizador != null)
                {
                    // Substitui os dados antigos pelos novos valores introduzidos e regista quem fez a alteração
                    utilizador.Username = username;
                    utilizador.Password = password;
                    utilizador.AlteradoPor = alteradoPor;

                    db.SaveChanges();
                }
            }
        }

        // ELIMINAR
        // Remove permanentemente um utilizador da base de dados através do seu ID
        public static void Eliminar(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Procura o utilizador correspondente na base de dados usando o ID
                Utilizador utilizador =
                    db.Utilizadores.FirstOrDefault(u => u.Id == id);

                // Verifica se o registo existe antes de tentar apagar
                if (utilizador != null)
                {
                    db.Utilizadores.Remove(utilizador);
                    db.SaveChanges();
                }
            }
        }
    }
}