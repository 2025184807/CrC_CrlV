
namespace IShopping.Models
{
    internal class Artigo
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        // Chave estrangeira para TipoArtigo
        public int TipoArtigoId { get; set; }

        //Relação com a tabela TipoArtigo
        public virtual TipoArtigo TipoArtigo { get; set; } // virtual serve para carregar os dados apenas quando necessário.
    }
}
