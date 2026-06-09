using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IShopping.Models
{
    [Table("TiposArtigos")] // Nome da tabela no banco de dados
    internal class TipoArtigo
    {
        // Chave Primária
        public int Id { get; set; }

        // Propriedades Principais do Tipo de Artigo
        public string Nome { get; set; }

        // Relações e Propriedades de Navegação
        public virtual ICollection<Artigo> Artigos { get; set; }
    }
}