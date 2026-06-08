
namespace IShopping.Models
{
    // Alterado para public para o controlador conseguir aceder
    public class SugestaoOrcamentoDto
    {
        public decimal MediaUltimosMeses { get; set; } // Representa a Média dos Orçamentos
        public decimal MediaGastos { get; set; }       // Adicionado para a "Média dos Gastos" do ecrã
        public decimal DiferencaMedia { get; set; }     // Adicionado para a "Diferença média" do ecrã
        public decimal SugestaoProximoMes { get; set; }
    }
}
