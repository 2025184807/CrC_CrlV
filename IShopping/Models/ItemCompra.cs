using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopping.Models
{
    internal class ItemCompra
    {
        public int ItemCompraId { get; set; }

        // FK Compra
        public int CompraId { get; set; }
        // FK Artigo
        public int ArtigoId { get; set; }

        public decimal QuantidadePrevista { get; set; }

        public decimal QuantidadeAdquirida { get; set; }

        public decimal PrecoUnitario { get; set; }

       // public string descricao { get; set; }

        // Navegação
        public virtual Compra Compra { get; set; }

        // Navegação
        public Artigo Artigo { get; set; } 
    }
}
