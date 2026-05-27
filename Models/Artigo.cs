
using System.ComponentModel.DataAnnotations.Schema;

namespace IShopping.Models
{

    [Table("Artigos")] //Tabela no banco de dados
    internal class Artigo
    {

        public int Id { get; set; }

        public string Nome { get; set; }

        public decimal Preco { get; set; }

        // Chave estrangeira para TipoArtigo
        public int TipoArtigoId { get; set; }

        //Relação com a tabela TipoArtigo
        public virtual TipoArtigo TipoArtigo { get; set; } // virtual serve para carregar os dados apenas quando necessário.
    }
}
