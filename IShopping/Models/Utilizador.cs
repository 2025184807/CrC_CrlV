using IShopping.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IShopping
{
    [Table("Utilizadores")] // Nome da tabela no banco de dados
    internal class Utilizador
    {
        // Chave Primária
        public int Id { get; set; }

        // Propriedades de Credenciais e Acesso
        public string Username { get; set; }
        public string Password { get; set; }

        // Dados de Auditoria 
        public string CriadoPor { get; set; }
        public string AlteradoPor { get; set; }

        // Relações e Propriedades de Navegação
        public List<CompraPlaneada> Compras { get; set; }
    }
}