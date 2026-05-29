using System.Linq;
using IShopping.Models;

namespace IShopping.Controller
{
    internal static class Form2Controller
    {
        public static bool Autenticar(string login, string password, out string mensagem)
        {
            mensagem = "";

            if (login.Trim() == "" || password.Trim() == "")
            {
                mensagem = "Deve introduzir o nome e a password.";
                return false;
            }

            using (shoppingContext db = new shoppingContext())
            {
                Utilizador utilizador = db.Utilizadores.FirstOrDefault
                (u => u.Username == login && u.Password == password);

                if (utilizador == null)
                {
                    mensagem = "Login ou password incorretos.";
                    return false;
                }
                sessao.UtilizadorAtual = utilizador.Username;
                mensagem = "Autenticação efetuada com sucesso.";
                return true;
            }
        }
    }
}

