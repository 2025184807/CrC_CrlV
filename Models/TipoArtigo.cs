using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IShopping.Models
{

    [Table("TiposArtigos")] // Nome da tabela no banco de dados
    internal class TipoArtigo
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public virtual ICollection<Artigo> Artigos { get; set; } // Relação com artigos
        // virtual para permitir o carregamento preguiçoso (lazy loading) dos artigos relacionados a um tipo de artigo específico.
        //ICollection é uma interface que representa uma coleção de objetos, permitindo a manipulação de uma lista de artigos associados a um tipo de artigo.
    }
}
