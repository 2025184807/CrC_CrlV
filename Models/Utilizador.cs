using IShopping.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IShopping
{
    [Table("Utilizadores")] // Nome da tabela no banco de dados
    internal class Utilizador
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string CriadoPor { get; set; }
        public string AlteradoPor { get; set; }

        //public DateTime DataCriacao { get; set; }
        //public DateTime? DataAlteracao { get; set; }
        public List<Compra> Compras { get; set; } // Relação com a tabela Compra, permitindo que um utilizador possa ter várias compras associadas a ele. A propriedade Compras é uma lista que armazena as compras relacionadas a um utilizador específico.
    }
}
