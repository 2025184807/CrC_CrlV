using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IShopping.Models
{
    // O atributo [Table] define explicitamente o nome real da tabela que será criada no banco de dados (SQL Server)
    [Table("Artigos")]
    internal class Artigo
    {
        // Chave Primária (ID único que identifica cada artigo na tabela)
        public int Id { get; set; }

        // Guarda o nome do produto
        public string Nome { get; set; }

        // Guarda o preço estimado ou base do artigo
        public decimal Preco { get; set; }

        // CHAVE ESTRANGEIRA: Guarda o ID da categoria (TipoArtigo) à qual este artigo pertence
        public int TipoArtigoId { get; set; }

        // PROPRIEDADE DE NAVEGAÇÃO (Mapeamento de 1 para Muitos):
        // Permite aceder diretamente aos detalhes da categoria associada
        // O 'virtual' ativa o Lazy Loading (carrega os dados da categoria apenas quando forem pedidos no código)
        public virtual TipoArtigo TipoArtigo { get; set; }

        // PROPRIEDADE DE NAVEGAÇÃO (Mapeamento de 1 para Muitos):
        // Uma lista que indica que este mesmo artigo pode aparecer repetido em várias linhas de compras diferentes
        public List<ItemCompraPlaneada> ItensCompra { get; set; }
    }
}