using System;

namespace IShopping.Models
{
    internal class ItemCompraPlaneada
    {
        // Chave Primária 
        public int Id { get; set; }

        // Chave Estrangeira para a tabela CompraPlaneada
        public int CompraPlaneadaId { get; set; }
        public virtual CompraPlaneada CompraPlaneada { get; set; }

        // Chave Estrangeira para a tabela Artigo
        public int ArtigoId { get; set; }
        public virtual Artigo Artigos { get; set; }

        // Propriedades de Quantidade e Valores
        public int QuantidadePrevista { get; set; }
        public int QuantidadeAdquirida { get; set; }
        public decimal? PrecoUnitario { get; set; }

        // Estados do Item
        public bool Previsto { get; set; }
        public bool Adquirido { get; set; }

        // Descricao
        public string Observacoes { get; set; }
    }
}
