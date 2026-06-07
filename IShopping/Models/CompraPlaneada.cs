using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IShopping.Models
{
    [Table("CompraPlaneada")] // Nome da tabela no banco de dados
    internal class CompraPlaneada
    {

        public int Id { get; set; }
        public string NomeCompra { get; set; }

        public DateTime DataCompra { get; set; }

        public DateTime DataCriacao { get; set; }
        public string CriadoPor { get; set; }

        public DateTime? DataHoraAlteracao { get; set; }
        public string AlteradoPor { get; set; }

        public DateTime? DataFecho { get; set; }
        public string FechadoPor { get; set; }

        public bool Fechada { get; set; }

        // FK Compra
        public int CompraId { get; set; }

        // FK Artigo
        public int ArtigoId { get; set; }

        public virtual ICollection<ItemCompraPlaneada> Itens { get; set; }

        public CompraPlaneada()
        {
            Itens = new List<ItemCompraPlaneada>();
        }

    }
}
