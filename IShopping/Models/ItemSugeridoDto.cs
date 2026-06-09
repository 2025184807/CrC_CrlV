using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopping.Models
{
    // DTO (Objeto de Transferência de Dados) utilizado para estruturar a informação de artigos sugeridos, combinando o nome do produto com o número de vezes que foi comprado
    internal class ItemSugeridoDto
    {
        // Guarda o nome do produto
        public string NomeArtigo { get; set; }

        // Regista o número de vezes (a frequência) que este artigo foi adicionado a listas de compras anteriores.
        public int Frequencia { get; set; }
    }
}