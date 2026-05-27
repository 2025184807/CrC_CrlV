using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IShopping
{
    [Table("Utilizadores")]
    internal class Utilizador
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string CriadoPor { get; set; }
        public string AlteradoPor { get; set; }

        //public DateTime DataCriacao { get; set; }
        //public DateTime? DataAlteracao { get; set; }
    }
}
