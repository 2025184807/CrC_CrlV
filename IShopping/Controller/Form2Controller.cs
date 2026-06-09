using System.Linq;
using IShopping.Models;

namespace IShopping.Controller
{
    // Controller responsável por gerir a lógica de autenticação (Login) do sistema
    internal static class Form2Controller
    {
        // Método que valida as credenciais introduzidas e faz o login do utilizador
        public static bool Autenticar(string login, string password, out string mensagem)
        {
            mensagem = "";

            // Valida se algum dos campos (login ou password) foi deixado vazio ou apenas com espaços
            if (login.Trim() == "" || password.Trim() == "")
            {
                mensagem = "Deve introduzir o nome e a password.";
                return false;
            }

            // Abre a ligação com a base de dados utilizando o contexto
            using (shoppingContext db = new shoppingContext())
            {
                // Procura na tabela de Utilizadores o primeiro registo que tenha o Username E a Password iguais aos introduzidos
                Utilizador utilizador = db.Utilizadores.FirstOrDefault
                (u => u.Username == login && u.Password == password);

                // Se não encontrar nenhum utilizador correspondente, devolve uma mensagem de erro
                if (utilizador == null)
                {
                    mensagem = "Login ou password incorretos.";
                    return false;
                }

                // Se encontrou, guarda o nome do utilizador na variável global da sessão para o resto do programa saber quem está ligado
                sessao.UtilizadorAtual = utilizador.Username;

                mensagem = "Autenticação efetuada com sucesso.";
                return true;
            }
        }
    }
}