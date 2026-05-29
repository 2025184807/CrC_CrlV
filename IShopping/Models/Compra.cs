using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopping.Models
{
    internal class Compra
    {
        public int CompraId { get; set; }

        public string NomeCompra { get; set; }

        public DateTime DataCompra { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; } // O ponto de interrogação indica que a propriedade é opcional, ou seja, pode conter um valor nulo.

        public bool Fechada { get; set; }

        public string CriadoPor { get; set; }
        public string AlteradoPor { get; set; }

        // FK Utilizador
        public int UtilizadorId { get; set; }

        // Navegação
        public Utilizador Utilizador { get; set; }

        // Lista de itens
        public List<ItemCompra> Itens { get; set; }
    }
}
