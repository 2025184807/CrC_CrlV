using System;

namespace IShopping.Models
{
    // DTO (Objeto de Transferência de Dados) para preencher as colunas de uma grelha visual (Grid / DataGridView)
    internal class CompraPlaneadaGridDto
    {
        // Chave Primária 
        public int Id { get; set; }

        // Propriedades Principais da Compra
        public string NomeCompra { get; set; }
        public DateTime DataCompra { get; set; }

        // Estados do Item
        public bool Fechada { get; set; }

        // Datas 
        public DateTime DataCriacao { get; set; }
        public DateTime? DataFecho { get; set; }
    }
}