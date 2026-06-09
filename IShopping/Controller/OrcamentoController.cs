using IShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IShopping.Controller
{
    // Classe responsável por gerir todas as operações de base de dados relacionadas com os Orçamentos Mensais
    internal class OrcamentoController
    {
        // LISTAR
        // Recupera a lista completa de todos os orçamentos registados no sistema
        public static List<Orcamento> Listar()
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Converte a tabela de Orçamentos da base de dados numa lista em memória
                return db.Orcamentos.ToList();
            }
        }

        // INSERT
        // Cria e grava um novo registo de orçamento para um determinado mês e ano
        public static void Inserir(decimal valorOrcamento, int ano, int mes)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Instancia o objeto Orçamento e preenche as suas propriedades com os valores recebidos
                var orcamento = new Orcamento
                {
                    ValorOrcamento = valorOrcamento,
                    Ano = ano,
                    Mes = mes
                };

                // Adiciona o novo orçamento ao contexto e efetiva a gravação na base de dados
                db.Orcamentos.Add(orcamento);
                db.SaveChanges();
            }
        }

        // PROCURAR POR ID
        // Procura um orçamento específico utilizando a sua Chave Primária (ID)
        public static Orcamento ProcurarPorId(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Devolve o primeiro orçamento que encontrar com o ID correspondente ou 'null' se não existir
                return db.Orcamentos.FirstOrDefault(o => o.OrcamentoId == id);
            }
        }

        // PROCURAR POR MES/ANO
        // Procura se já existe um orçamento definido para um mês e ano específicos (evita duplicados)
        public static Orcamento ObterPorMesAno(int mes, int ano)
        {
            using (shoppingContext db = new shoppingContext())
            {
                return db.Orcamentos.FirstOrDefault(o => o.Mes == mes && o.Ano == ano);
            }
        }

        // ELIMINAR
        // Remove permanentemente um orçamento do sistema através do seu ID
        public static void Eliminar(int id)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Procura o registo na base de dados
                var orcamento = db.Orcamentos.Find(id);

                // Se o orçamento for encontrado, remove-o e grava a alteração
                if (orcamento != null)
                {
                    db.Orcamentos.Remove(orcamento);
                    db.SaveChanges();
                }
            }
        }

        // EDITAR
        // Localiza um orçamento existente pelo ID e atualiza os seus valores com os novos dados introduzidos
        public static void Editar(int id, decimal valorOrcamento, int ano, int mes)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Procura o registo original
                var orcamento = db.Orcamentos.Find(id);

                // Se existir, substitui os valores antigos e submete as alterações para a base de dados
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
        // Calcula o saldo restante subtraindo todos os gastos do sistema ao orçamento do mês mais recente
        public static decimal ObterSaldoDisponivel()
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Ordena os orçamentos por Ano e Mês (decrescente) para obter o orçamento do mês mais atualizado
                var orcamento = db.Orcamentos
                    .OrderByDescending(o => o.Ano)
                    .ThenByDescending(o => o.Mes)
                    .FirstOrDefault();

                // Se ainda não houver nenhum orçamento registado no sistema, o saldo disponível é zero
                if (orcamento == null)
                {
                    return 0;
                }

                // Calcula a soma total multiplicando a quantidade pelo preço de cada item adquirido
                decimal totalGasto =
                    db.ItemComprasPlaneadas
                    .Where(i => i.Adquirido) // Filtra apenas os itens que foram comprados               
                    .Sum(i => (decimal?)i.QuantidadeAdquirida * (i.PrecoUnitario ?? 0)) ?? 0;
                    // Converte a Quantidade para decimal anulável (decimal?)
                    // com uma lista vazia, e usa o operador '?? 0' para que, se o preço unitário for nulo, use o valor zero.
                    // Se a tabela inteira estiver vazia, o resultado do .Sum() será null. 
                    // Estes dois pontos de interrogação garantem que, se der null, o totalGasto passa a ser 0.

                // Devolve a diferença (Orçamento Atual - Total Geral Gasto)
                return orcamento.ValorOrcamento - totalGasto;
            }
        }
    }
}