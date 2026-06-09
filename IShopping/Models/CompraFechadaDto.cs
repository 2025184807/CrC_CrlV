using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IShopping.Models
{
    // DTO (Objeto de Transferência de Dados) utilizado para estruturar e enviar o resumo de uma compra já concluída diretamente para os ecrãs de relatórios do sistema
    internal class CompraFechadaDto
    {
        // Guarda o nome identificador da lista de compras
        public string NomeCompra { get; set; }
        // Guarda o valor percentual de artigos que foram comprados e já estavam planeados 
        public decimal PercentagemPrevistos { get; set; }

        // Guarda o valor percentual de artigos comprados por impulso que não estavam na lista original
        public decimal PercentagemNaoPrevistos { get; set; }

        // Guarda a soma total do dinheiro que foi efetivamente gasto nesta compra específica (Quantidade * Preço)
        public decimal TotalGasto { get; set; }
    }
}