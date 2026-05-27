using IShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IShopping.Controller
{
    internal class OrcamentoController
    {
        // LISTAR
        public static List<Orcamento> Listar()
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.Orcamentos.ToList();
            }
        }

        // INSERIR
        public static void Inserir(string valorOrcamento, DateTime? dataCompra, string criadoPor)
        {
            using (shoppingContext db = new shoppingContext())
            {
                Orcamento orcamento = new Orcamento();

                orcamento.ValorOrcamento = decimal.Parse(valorOrcamento);
                orcamento.DataCompra = dataCompra;
                orcamento.CriadoPor = criadoPor;

                db.Orcamentos.Add(orcamento);

                db.SaveChanges();
            }
        }

        // PROCURAR POR ID
        public static Orcamento ProcurarPorId(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.Orcamentos
                    .FirstOrDefault(o => o.OrcamentoId == id);
            }
        }

        //Eliminar
        public static void Eliminar(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                Orcamento orcamento = db.Orcamentos.Find(id);
                if (orcamento != null)
                {
                    db.Orcamentos.Remove(orcamento);
                    db.SaveChanges();
                }
            }
        }

        // EDITAR
        public static void Editar(int id, decimal valorOrcamento, DateTime? mesDate, DateTime? anoDate, string alteradoPor)
        {
            using (shoppingContext db = new shoppingContext())
            {
                Orcamento orcamento = db.Orcamentos.Find(id);
                if (orcamento != null)
                {
                    orcamento.ValorOrcamento = valorOrcamento;
                    orcamento.DataCompra = mesDate;
                    orcamento.AlteradoPor = alteradoPor;
                    db.SaveChanges();
                }
            }
        }


    }
}
