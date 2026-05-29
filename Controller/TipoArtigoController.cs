using IShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IShopping.Controller
{
    internal class TipoArtigoController
    {
        // LISTAR
        public static List<TipoArtigo> Listar()
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.TipoArtigos.ToList();
            }
        }

        // INSERIR
        public static void Inserir(string nome)
        {
            using (shoppingContext db = new shoppingContext())
            {
                TipoArtigo tipo = new TipoArtigo();

                tipo.Nome = nome;

                db.TipoArtigos.Add(tipo);

                db.SaveChanges();
            }
        }

        // EDITAR
        public static void Editar(int id, string nome)
        {
            using (shoppingContext db = new shoppingContext())
            {
                TipoArtigo tipo = db.TipoArtigos.Find(id);

                if (tipo != null)
                {
                    tipo.Nome = nome;

                    db.SaveChanges();
                }
            }
        }

        // PROCURAR POR ID
        public static TipoArtigo ProcurarPorId(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.TipoArtigos
                    .FirstOrDefault(t => t.Id == id);
            }
        }

        // ELIMINAR
        public static void Eliminar(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                TipoArtigo tipo = db.TipoArtigos.Find(id);

                if (tipo != null)
                {
                    db.TipoArtigos.Remove(tipo);

                    db.SaveChanges();
                }
            }
        }
    }
}
