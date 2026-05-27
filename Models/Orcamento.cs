using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopping.Models
{
    [Table("OrcamentosMensais")] // Nome da tabela no banco de dados
    internal class Orcamento
    {
        public int OrcamentoId { get; set; }

        public decimal ValorOrcamento { get; set; }

        public DateTime? DataCompra { get; set; } = DateTime.Now;
        public string AlteradoPor { get; set; }
        public string CriadoPor { get; set; }

    }
}
