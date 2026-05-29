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
            Application.EnableVisualStyles(); //Habilita os estilos visuais para os controles do Windows Forms
            Application.SetCompatibleTextRenderingDefault(false); 
            Database.SetInitializer(new AppInicializer()); //Configura o Entity Framework para usar a classe AppInicializer como inicializador do banco de dados

            //Inicializa o banco de dados usando o contexto shoppingContext. O método Initialize é chamado com force: true para garantir que o banco de dados seja criado ou atualizado conforme necessário, mesmo que já exista.
            using (var context = new shoppingContext())
            {
                context.Database.Initialize(force: true);
            }

            //Variável de controle para determinar se o programa deve continuar em execução. Ela é usada para controlar o loop principal do programa, permitindo que o usuário faça login e acesse a interface principal, e também para encerrar o programa quando necessário.
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