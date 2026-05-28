using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopping.Models
{
    internal class sessao
    {
           public static string UtilizadorAtual { get; set; } // Propriedade estática para armazenar o nome do utilizador atual 
           //static para que seja acessível em toda a aplicação sem a necessidade de criar uma instância da classe sessao.

    }
}
