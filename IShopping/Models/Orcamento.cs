using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopping.Models
{
    [Table("Orcamentos")] // Nome da tabela no banco de dados
    internal class Orcamento
    {
        public int OrcamentoId { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public decimal ValorOrcamento { get; set; }

    }
}
