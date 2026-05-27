using IShopping.Models;
using System.Data.Entity;

namespace IShopping
{
    internal class shoppingContext : DbContext
    {
        public shoppingContext()
        { }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<TipoArtigo> TipoArtigos { get; set; }
        public DbSet<Artigo> Artigos { get; set; }

        //public DbSet<Compra> Compras { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
    }
}
