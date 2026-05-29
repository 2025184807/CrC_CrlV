using IShopping.Models;
using System.Data.Entity;

namespace IShopping
{
    // Contexto da base de dados
    internal class shoppingContext : DbContext
    {
        // Construtor 
        public shoppingContext()
        { }
        public DbSet<Utilizador> Utilizadores { get; set; } // Tabela de utilizadores
        public DbSet<TipoArtigo> TipoArtigos { get; set; } // Tabela de tipos de artigo
        public DbSet<Artigo> Artigos { get; set; } // Tabela de artigos
        public DbSet<Compra> Compras { get; set; } // Tabela de compras
        public DbSet<ItemCompra> ItemCompras { get; set; } // Tabela de itens de compra
        public DbSet<Orcamento> Orcamentos { get; set; } // Tabela de orçamentos
    }
}
