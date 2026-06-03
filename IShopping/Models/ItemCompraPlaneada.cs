using System;

namespace IShopping.Models
{
    internal class ItemCompraPlaneada
    {
        public int Id { get; set; }
        public int CompraPlaneadaId { get; set; }
        public virtual CompraPlaneada CompraPlaneada { get; set; }
        public int ArtigoId { get; set; }
        public virtual Artigo Artigos { get; set; }
        public int QuantidadePrevista { get; set; }
        public int QuantidadeAdquirida { get; set; }
        public decimal? PrecoUnitario { get; set; }
        public bool Previsto { get; set; }
        public bool Adquirido { get; set; }
        public string Observacoes { get; set; }
    }
}
