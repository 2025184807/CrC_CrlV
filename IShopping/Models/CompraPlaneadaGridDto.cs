using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopping.Models
{
    internal class CompraPlaneadaGridDto
    {
        public int Id { get; set; }
        public string NomeCompra { get; set; }
        public DateTime DataCompra { get; set; }
        public bool Fechada { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataFecho { get; set; }
    }
}
