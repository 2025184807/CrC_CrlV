
using System.Collections.Generic;
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
        public List<ItemCompraPlaneada> ItensCompra { get; set; } // Relação com a tabela ItemCompra, permitindo que um artigo possa estar presente em vários itens de compra diferentes. A propriedade ItensCompra é uma lista que armazena os itens de compra associados a um artigo específico.
    }
}
