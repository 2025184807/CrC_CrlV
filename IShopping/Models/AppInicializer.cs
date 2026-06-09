using System.Data.Entity;

namespace IShopping.Models
{
    // Classe responsável por inicializar a base de dados.
    // O 'DropCreateDatabaseIfModelChanges' diz ao Entity Framework para apagar e recriar a base de dados
    internal class AppInicializer : DropCreateDatabaseIfModelChanges<shoppingContext>
    {
        // O método Seed serve para "semear" ou alimentar a base de dados com dados obrigatórios logo após ela ser criada
        protected override void Seed(shoppingContext context)
        {
            // Cria e adiciona o primeiro utilizador (Administrador) padrão para garantir que consegues fazer login no sistema à primeira
            context.Utilizadores.Add(new Utilizador
            {
                Username = "admin",
                Password = "12345"
            });

            // Guarda de forma permanente o utilizador administrador na base de dados
            context.SaveChanges();

            // Chama a função base do Entity Framework para concluir o processo de inicialização padrão
            base.Seed(context);
        }
    }
}