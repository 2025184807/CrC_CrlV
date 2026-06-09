using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IShopping.Models
{
    // O atributo [Table] define explicitamente o nome real da tabela que será criada na base de dados
    [Table("CompraPlaneada")]
    internal class CompraPlaneada
    {   
        public int Id { get; set; } // Chave Primária 
        public string NomeCompra { get; set; } // Nome da compra
        public DateTime DataCompra { get; set; } // Data em que a compra foi ou será realizada
        public DateTime DataCriacao { get; set; } //Regista o momento exato em que a lista foi criada no sistema
        public string CriadoPor { get; set; } // Guarda o username do utilizador que criou esta lista
        public DateTime? DataHoraAlteracao { get; set; } // O '?' permite valores nulos (DateTime?). Guarda a data da última modificação feita na lista
        public string AlteradoPor { get; set; } // Guarda o username de quem alterou a lista pela última vez
        public DateTime? DataFecho { get; set; } // Guarda o momento exato em que a lista foi fechada
        public string FechadoPor { get; set; }  // Guarda o username de quem finalizou e fechou a compra
        public bool Fechada { get; set; } // Indicador booleano: 'false' significa que a lista está em aberto (planeamento); 'true' significa que está fechada (concluída)


        // Chave Estrangeira (FK)
        public int CompraId { get; set; } // para associar a uma Compra 
        public int ArtigoId { get; set; } // para associar a um Artigo específico


        // PROPRIEDADE DE NAVEGAÇÃO (Mapeamento de 1 para Muitos):
        // Coleção virtual que permite aceder diretamente a todas as linhas/itens que pertencem a esta lista de compras
        public virtual ICollection<ItemCompraPlaneada> Itens { get; set; }

        // Construtor da classe: Executado automaticamente sempre que fazemos um 'new CompraPlaneada()'
        public CompraPlaneada()
        {
            // Inicializa a coleção como uma lista vazia para evitar erros de referência nula (NullReferenceException) ao adicionar itens
            Itens = new List<ItemCompraPlaneada>();
        }
    }
}