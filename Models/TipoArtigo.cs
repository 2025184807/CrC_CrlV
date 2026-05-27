using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IShopping.Models
{

    [Table("TiposArtigos")] // Nome da tabela no banco de dados
    internal class TipoArtigo
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        // Relação com artigos
        public virtual ICollection<Artigo> Artigos { get; set; }
    }
}
