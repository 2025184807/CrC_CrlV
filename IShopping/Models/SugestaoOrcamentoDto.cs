namespace IShopping.Models
{
    // Alterado para public para o controlador conseguir aceder
    public class SugestaoOrcamentoDto
    {
        // Propriedades de Valores e Médias Financeiras
        public decimal MediaUltimosMeses { get; set; }
        public decimal MediaGastos { get; set; }
        public decimal DiferencaMedia { get; set; }

        // Previsões para o Próximo Período
        public decimal SugestaoProximoMes { get; set; }
    }
}