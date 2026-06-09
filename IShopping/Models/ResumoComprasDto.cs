using System.Collections.Generic;

namespace IShopping.Models
{
    internal class ResumoComprasDto
    {
        // Propriedades Financeiras do Mês
        public decimal OrcamentoMensal { get; set; }
        public decimal TotalComprasMes { get; set; }
        public decimal Diferenca { get; set; }

        // Indicadores de Itens Planeados e por itens não previstos
        public int TotalItensPrevistos { get; set; }
        public int TotalItensNaoPrevistos { get; set; }
        public decimal PercentagemPrevistos { get; set; }
        public decimal PercentagemNaoPrevistos { get; set; }

        // Lista de Histórico
        public List<string> ComprasFechadas { get; set; }

        // Construtor da Classe
        public ResumoComprasDto()
        {
            ComprasFechadas = new List<string>();
        }
    }
}