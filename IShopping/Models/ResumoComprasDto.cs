using System.Collections.Generic;

namespace IShopping.Models
{
    internal class ResumoComprasDto
    {
        public decimal OrcamentoMensal { get; set; }
        public decimal TotalComprasMes { get; set; }
        public decimal Diferenca { get; set; }

        public int TotalItensPrevistos { get; set; }
        public int TotalItensNaoPrevistos { get; set; }
        public decimal PercentagemPrevistos { get; set; }
        public decimal PercentagemNaoPrevistos { get; set; }

        public List<string> ComprasFechadas { get; set; }
        public ResumoComprasDto()
        {
            ComprasFechadas = new List<string>();
        }
    }
}
