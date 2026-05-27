using IShopping.Views;
using IShopping.Models;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace IShopping
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Database.SetInitializer(new AppInicializer());

            using (var context = new shoppingContext())
            {
                context.Database.Initialize(force: true);
            }

            bool continuarNoPrograma = true;

            //Com este ciclo while controlado pelo Dialog Result, a memória do forms fica limpa a cada "transição"
            while (continuarNoPrograma)
            {
                using (FormLogin login = new FormLogin())
                {
                    //verificar se o login e bem sucessido
                    if (login.ShowDialog() != DialogResult.OK)
                    {
                        continuarNoPrograma = false;
                        break;
                    }
                }

                using (FormMain principal = new FormMain())
                {
                    DialogResult resultadoPrincipal = principal.ShowDialog();

                    if (resultadoPrincipal != DialogResult.Retry)
                    {
                        continuarNoPrograma = false;
                    }
                }
            }
        }
    }
}