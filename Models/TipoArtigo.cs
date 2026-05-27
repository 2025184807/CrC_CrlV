using System.Collections.Generic;

namespace IShopping.Models
{
    internal class TipoArtigo
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        // Relação com artigos
        public virtual ICollection<Artigo> Artigos { get; set; }
    }
}
