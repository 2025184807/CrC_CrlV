using System;
using System.Collections.Generic;

namespace IShopping.Models
{
    internal class CompraPlaneada
    {
        public int Id { get; set; }
        public string NomeCompra { get; set; }
        public int MesReferencia { get; set; }
        public int AnoReferencia { get; set; }
        public DateTime DataCriacao { get; set; }
        public string CriadoPor { get; set; }
        public DateTime? DataHoraAlteracao { get; set; }
        public string AlteradoPor { get; set; }
        public DateTime? DataFecho { get; set; }
        public string FechadoPor { get; set; }
        public bool Fechada { get; set; }
        public virtual ICollection<ItemCompraPlaneada> Itens { get; set; }
        public CompraPlaneada()
        {
            Itens = new List<ItemCompraPlaneada>();
        }

    }
}
