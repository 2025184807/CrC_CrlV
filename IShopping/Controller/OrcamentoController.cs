using IShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        // INSERT
        public static void Inserir(decimal valorOrcamento, int ano, int mes)
        {
            using (shoppingContext db = new shoppingContext())
            {
                var orcamento = new Orcamento
                {
                    ValorOrcamento = valorOrcamento,
                    Ano = ano,
                    Mes = mes
                };

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

        // PROCURAR POR MES/ANO
        public static Orcamento ObterPorMesAno(int mes, int ano)
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.Orcamentos
                    .FirstOrDefault(o => o.Mes == mes && o.Ano == ano);
            }
        }

        // ELIMINAR
        public static void Eliminar(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                var orcamento = db.Orcamentos.Find(id);

                if (orcamento != null)
                {
                    db.Orcamentos.Remove(orcamento);
                    db.SaveChanges();
                }
            }
        }

        // EDITAR
        public static void Editar(int id, decimal valorOrcamento, int ano, int mes)
        {
            using (shoppingContext db = new shoppingContext())
            {
                var orcamento = db.Orcamentos.Find(id);

                if (orcamento != null)
                {
                    orcamento.ValorOrcamento = valorOrcamento;
                    orcamento.Ano = ano;
                    orcamento.Mes = mes;

                    db.SaveChanges();
                }
            }
        }

        // SALDO DISPONÍVEL 
        public static decimal ObterSaldoDisponivel()
        {
            using (shoppingContext db = new shoppingContext())
            {
                var orcamento = db.Orcamentos
                    .OrderByDescending(o => o.Ano)
                    .ThenByDescending(o => o.Mes)
                    .FirstOrDefault();

                if (orcamento == null)
                    return 0;

                decimal totalGasto =
                    db.ItemComprasPlaneadas
                    .Where(i => i.Adquirido)
                    .Sum(i => (decimal?)i.QuantidadeAdquirida * (i.PrecoUnitario ?? 0))
                    ?? 0;

                return orcamento.ValorOrcamento - totalGasto;
            }
        }
    }
}