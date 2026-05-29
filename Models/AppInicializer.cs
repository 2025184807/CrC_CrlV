using System.Data.Entity;

namespace IShopping.Models
{
    internal class AppInicializer : DropCreateDatabaseIfModelChanges<shoppingContext>
    {
            protected override void Seed(shoppingContext context)
            {

                context.Utilizadores.Add(new Utilizador
                {

                    Username = "admin",
                    Password = "12345"
                });

                context.Utilizadores.Add(new Utilizador
                {
                    Username = "pedro",
                    Password = "1234"
                  
                });

                context.SaveChanges();
                base.Seed(context);
             }
    }
}
