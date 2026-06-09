using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopping.Models
{
    // O atributo [Table] define explicitamente o nome real da tabela que será criada no SQL Server
    [Table("Orcamentos")]
    internal class Orcamento
    {
        // Chave Primária
        public int OrcamentoId { get; set; }

        // Guarda o número do mês do oçamento 
        public int Mes { get; set; }

        // Guarda o ano correspondente ao orçamento 
        public int Ano { get; set; }

        // Define o limite máximo de dinheiro disponível para gastar nas compras desse mês
        public decimal ValorOrcamento { get; set; }
    }
}