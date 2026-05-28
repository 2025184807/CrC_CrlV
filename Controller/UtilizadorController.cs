using System.Collections.Generic;
using System.Linq;

namespace IShopping.Controller
{
    internal class UtilizadorController
    {
        // LISTAR
        public static List<Utilizador> Listar()
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.Utilizadores.ToList();
            }
        }

        // INSERIR
        public static void Inserir(string username, string password, string criadoPor)
        {
            using (shoppingContext db = new shoppingContext())
            {
                Utilizador utilizador = new Utilizador();

                utilizador.Username = username;
                utilizador.Password = password;
                utilizador.CriadoPor = criadoPor;

                db.Utilizadores.Add(utilizador);

                db.SaveChanges();
            }
        }

        // PROCURAR POR ID
        public static Utilizador ProcurarPorId(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.Utilizadores
                    .FirstOrDefault(u => u.Id == id);
            }
        }

        // EDITAR
        public static void Editar(int id, string username, string password, string alteradoPor)
        {
            using (shoppingContext db = new shoppingContext())
            {
                Utilizador utilizador = db.Utilizadores.FirstOrDefault(u => u.Id == id);

                if (utilizador != null)
                {
                    utilizador.Username = username;
                    utilizador.Password = password;
                    utilizador.AlteradoPor = alteradoPor;

                    db.SaveChanges();
                }
            }
        }

        // ELIMINAR
        public static void Eliminar(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                Utilizador utilizador =
                    db.Utilizadores.FirstOrDefault(u => u.Id == id);

                if (utilizador != null)
                {
                    db.Utilizadores.Remove(utilizador);

                    db.SaveChanges();
                }
            }
        }


    }
}
