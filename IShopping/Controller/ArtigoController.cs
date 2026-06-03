using IShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity; // Necessário para usar o .Include() e outras funcionalidades do Entity Framework

namespace IShopping.Controller
{
    internal class ArtigoController
    {
        // LISTAR TODOS
        public static List<Artigo> Listar()
        {
                using (shoppingContext db = new shoppingContext())
                {
                    // O .Include diz ao Entity Framework para trazer o TipoArtigo juntamente com o Artigo
                    return db.Artigos.Include(a => a.TipoArtigo).ToList();
                }

        }


        // FILTRAR POR TIPO
        public static List<Artigo> FiltrarPorTipo(int tipoId)
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.Artigos
                    .Where(a => a.TipoArtigoId == tipoId)
                    .ToList();
            }
        }

        // INSERIR
        public static void Inserir(string nome, int tipoArtigoId, decimal preco)
        {
            using (shoppingContext db = new shoppingContext())
            {
                Artigo artigo = new Artigo();

                artigo.Nome = nome;
                artigo.TipoArtigoId = tipoArtigoId;
                artigo.Preco = preco;

                db.Artigos.Add(artigo);

                db.SaveChanges();
            }
        }

        // EDITAR
        public static void Editar(int id, string nome, int tipoArtigoId, decimal preco)
        {
            using (shoppingContext db = new shoppingContext())
            {
                Artigo artigo = db.Artigos.Find(id);

                if (artigo != null)
                {
                    artigo.Nome = nome;
                    artigo.TipoArtigoId = tipoArtigoId;
                    artigo.Preco = preco;

                    db.SaveChanges();
                }
            }
        }

        // ELIMINAR
        public static void Eliminar(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                Artigo artigo = db.Artigos.Find(id);

                if (artigo != null)
                {
                    db.Artigos.Remove(artigo);

                    db.SaveChanges();
                }
            }
        }

        // PROCURAR POR ID
        public static Artigo ProcurarPorId(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.Artigos
                    .FirstOrDefault(a => a.Id == id);
            }
        }
    }
}
