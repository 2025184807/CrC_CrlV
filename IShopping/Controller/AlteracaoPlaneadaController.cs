using IShopping.Models; // Serve para ligar os modelos públicos e o Contexto
using System; // Serve para a função DateTime funcionar e para o tipo bool
using System.Collections.Generic; // Serve para usar List<T> e outros tipos de coleções genéricas
using System.Linq; // Serve para usar métodos de extensão como .Any(), .FirstOrDefault(), .ToList() e outros que facilitam a manipulação de coleções

namespace IShopping.Controller
{
    // Este controlador é responsável por toda a lógica relacionada com a criação, alteração e gestão das compras planeadas.
    internal class AlteracaoPlaneadaController // Internal serve para limitar o acesso a esta classe apenas dentro do assembly do projeto, ou seja, outras partes do código fora deste projeto não poderão acessar diretamente esta classe.
    {
        //A função ObterCompras serve para obter todas as compras registadas para listar na Grid principal
        public static List<CompraPlaneada> ObterCompras() // Método para obter todas as compras planeadas, retorna uma lista de objetos do tipo CompraPlaneada
        // O método é estático, o que significa que pode ser chamado diretamente sem a necessidade de criar uma instância da classe AlteracaoPlaneadaController.
        // List<CompraPlaneada> indica que o método retorna uma lista de objetos do tipo CompraPlaneada, que representa as compras planeadas registradas no banco de dados.

        {
            // O using garante que o contexto é corretamente descartado após o uso, para evitar problemas de conexões abertas ou memória não liberada.
            using (shoppingContext db = new shoppingContext()) // Cria uma nova instância do contexto de banco de dados( uma instancia é criada cada vez que o método é chamado, para garantir que as operações sejam feitas em um contexto limpo e atualizado).
            {
                return db.ComprasPlaneadas.ToList(); // Retorna uma lista de todas as compras planeadas presentes na tabela ComprasPlaneadas do banco de dados.
                                                     // Usa o método ToList() para converter o resultado em uma lista.
            }
        }

        // Função para listar os artigos
        public static List<Artigo> Listar() // Método para listar os tipos de artigo, retorna uma lista de objetos do tipo TipoArtigo
        {
            using (shoppingContext db = new shoppingContext()) // Cria uma nova instância do contexto de banco de dados para acessar os dados dos artigos.
            {
                return db.Artigos.ToList(); // Retorna uma lista de todas os artigos presentes na tabela Artigos do banco de dados.
            }
        }

        // A função CriarCompra cria uma nova compra planeada (valida se o nome não está vazio).
        public static bool CriarCompra(string nomeCompra, DateTime dataCompra, out string mensagem)
        {
            mensagem = ""; // Inicializa a variável de saída mensagem como uma string vazia, para que possa ser preenchida com uma mensagem de sucesso ou erro durante o processo de criação da compra.

            // Valida se o nome da compra é nulo, vazio ou contém apenas espaços em branco.
            // Se for, define a mensagem de erro e retorna false para indicar que a criação da compra falhou.
            if (string.IsNullOrWhiteSpace(nomeCompra))
            {
                mensagem = "Indique o nome da compra."; // Define a mensagem de erro indicando que o nome da compra é obrigatório.
                return false; // Retorna false para indicar que a criação da compra falhou devido à validação do nome.
            }

            // Valida se já existe uma compra com o mesmo nome, para evitar duplicatas.
            using (shoppingContext db = new shoppingContext())
            {
                // impedir nomes repetidos (não fechadas ou todas, como quiseres)
                bool existe = db.ComprasPlaneadas // Verifica se existe alguma compra planeada no banco de dados que tenha o mesmo nome fornecido.
                    .Any(c => c.NomeCompra == nomeCompra); // Usa o método Any() para verificar se existe pelo menos uma compra que atenda à condição especificada (c.NomeCompra == nomeCompra).
                                                           // Retorna true se existir, ou false caso contrário.

                // Se já existir uma compra com o mesmo nome, define a mensagem de erro e retorna false para indicar que a criação da compra falhou.
                if (existe)
                {
                    mensagem = "Já existe uma compra com esse nome.";
                    return false;
                }

                // Se o nome for válido e não houver duplicatas, cria um novo objeto CompraPlaneada e o adiciona ao banco de dados.
                // Se o nome for válido e não houver duplicatas, cria um novo objeto CompraPlaneada e o adiciona ao banco de dados.
                CompraPlaneada compra = new CompraPlaneada
                {
                    // Nome da compra fornecido pelo utilizador.
                    NomeCompra = nomeCompra,

                    // CORREÇÃO: Usa o Ano e o Mês que vieram do Form (dataCompra) em vez de DateTime.Now
                    DataCompra = new DateTime(
                        dataCompra.Year,
                        dataCompra.Month,
                        1
                    ),


                    DataCriacao = DateTime.Now, // A data e hora atuais são atribuídas à propriedade DataCriacao, onde indica quando a compra foi criada.
                    DataHoraAlteracao = DateTime.Now, // A data e hora atuais são atribuídas à propriedade DataHoraAlteracao, onde indica quando a compra foi criada ou alterada pela última vez.

                    CriadoPor = sessao.UtilizadorAtual, // O nome do utilizador atual, obtido da classe sessao, é atribuído à propriedade CriadoPor, onde indica quem criou a compra.
                    AlteradoPor = sessao.UtilizadorAtual, // O nome do utilizador atual, obtido da classe sessao, é atribuído à propriedade AlteradoPor, onde indica quem alterou a compra pela última vez.

                    Fechada = false // A propriedade Fechada é definida como false, onde indica que a compra ainda não foi fechada.
                };

                db.ComprasPlaneadas.Add(compra); // Adiciona o novo objeto compra ao contexto do banco de dados.
                db.SaveChanges(); // Salva as alterações no banco de dados, persistindo a nova compra criada.

                mensagem = "Compra criada com sucesso."; // Define a mensagem de sucesso indicando que a compra foi criada com sucesso.
                return true; // Retorna true para indicar que a criação da compra foi bem-sucedida.
            }
        }

        // Função para procurar um item por ID
        public static ItemCompraPlaneada ProcurarItemPorId(int id) // Método para procurar um item de compra planeada por seu ID, retorna um objeto do tipo ItemCompraPlaneada
        {
            using (shoppingContext db = new shoppingContext()) // Cria uma nova instância do contexto de banco de dados para acessar os dados dos itens de compra planeada.
            {
                return db.ItemComprasPlaneadas.FirstOrDefault(i => i.Id == id); // Retorna o primeiro item de compra planeada que corresponda ao ID fornecido, ou null se nenhum item for encontrado.
            }
        }

        // Função para alterar a quantidade prevista de um item
        public static void AlterarItem(int id, int novaQuantidade, out string mensagem)
        {
            using (shoppingContext db = new shoppingContext()) // Cria uma nova instância do contexto de banco de dados para acessar os dados dos itens de compra planeada.
            {
                ItemCompraPlaneada item = db.ItemComprasPlaneadas.Find(id); // Procura o item de compra planeada com o ID fornecido utilizando o método Find(), que retorna o item se encontrado ou null se não for encontrado.

                if (item != null) // Verifica se o item foi encontrado (ou seja, se não é null).
                {
                    item.QuantidadePrevista = novaQuantidade; // Atualiza a quantidade prevista do item com a nova quantidade fornecida.
                    db.SaveChanges(); // Salva as alterações no banco de dados, onde altera a quantidade prevista do item.
                    mensagem = "Item alterado com sucesso.";  // Define a mensagem de sucesso indicando que o item foi alterado com sucesso.
                }
                // Se o item não for encontrado (ou seja, se for null), define a mensagem de erro indicando que o item com o ID fornecido não foi encontrado no banco de dados.
                else
                {
                    mensagem = "Item não encontrado."; // Define a mensagem de erro indicando que o item com o ID fornecido não foi encontrado no banco de dados.
                }
            }
        }

        // Função para eliminar um item
        public static void EliminarItem(int id, out string mensagem) 
        {
            using (shoppingContext db = new shoppingContext()) // using serve para percorrer a base de dados.
            {
                ItemCompraPlaneada item = db.ItemComprasPlaneadas.Find(id); //  Procura o item de compras planeadas com o id fornecido utilizadando o método Find().
                if (item != null)// Se o item não for nulo, remove o item com sucesso.
                {
                    db.ItemComprasPlaneadas.Remove(item); 
                    db.SaveChanges(); // Salva as alterações no banco de dados.
                    mensagem = "Item eliminado com sucesso."; 
                }
                else // Se o item for nul, mostra uma mensagem.
                {
                    mensagem = "Item não encontrado.";
                }
            }
        }

        // Função AlterarCompra que serve para alterar os dados de uma compra existente ou fechá-la definitivamente
        public static bool AlterarCompra(int compraId, string novoNome, DateTime novaData, bool fecharCompra, out string mensagem)
        {
            mensagem = "";

            //Se o novo nome for nulo mostra uma mensagem para indicar o nome da compra e retorna falso.
            if (novoNome.Trim() == "") // O método Trim() serve para ler uma string.
            {
                mensagem = "Indique o nome da compra.";
                return false;
            }

            using (shoppingContext db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.Find(compraId); // Procura compras planeadas com o id fornecido.

                // Caso a compra seja nula, mostra uma mensagem e retorna falso.
                if (compra == null) 
                {
                    mensagem = "Compra não encontrada.";
                    return false;
                }

                // Se a compra for fechada tiver fechada, não pode ser possivel alterar a compra.
                if (compra.Fechada)
                {
                    mensagem = "Não é possível alterar uma compra que já se encontra fechada.";
                    return false;
                }

                // Atualiza os dados do cabeçalho
                compra.NomeCompra = novoNome; // a compra
                compra.DataCompra = novaData;
                compra.DataHoraAlteracao = DateTime.Now;
                compra.AlteradoPor = sessao.UtilizadorAtual;

                // Se a checkbox de fecho foi marcada
                if (fecharCompra)
                {
                    compra.Fechada = true;
                    compra.DataFecho = DateTime.Now;
                    compra.FechadoPor = sessao.UtilizadorAtual;
                }

                db.SaveChanges();

                mensagem = fecharCompra ? "Compra fechada e guardada com sucesso." : "Compra alterada com sucesso.";
                return true;
            }
        }

        // 4. Adicionar um artigo previsto à lista de itens da compra
        public static bool AdicionarItemPrevisto(int compraId, int artigoid, int quantidadePrevista, out string mensagem)
        {
            mensagem = "";

            if (quantidadePrevista <= 0)
            {
                mensagem = "A quantidade prevista deve ser superior a zero.";
                return false;
            }

            using (shoppingContext db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.Find(compraId);

                if (compra == null)
                {
                    mensagem = "Compra não encontrada.";
                    return false;
                }

                if (compra.Fechada)
                {
                    mensagem = "A compra já se encontra fechada.";
                    return false;
                }

                var artigo = db.Artigos.Find(artigoid);

                if (artigo == null)
                {
                    mensagem = "Artigo não encontrado.";
                    return false;
                }

                // evita duplicados
                bool existe = db.ItemComprasPlaneadas.Any(i =>
                    i.CompraPlaneadaId == compraId &&
                    i.ArtigoId == artigoid &&
                    i.Previsto);

                if (existe)
                {
                    mensagem = "Esse artigo já foi adicionado como item previsto.";
                    return false;
                }

                ItemCompraPlaneada item = new ItemCompraPlaneada
                {
                    CompraPlaneadaId = compraId,
                    ArtigoId = artigoid,
                    QuantidadePrevista = quantidadePrevista,
                    QuantidadeAdquirida = 0,
                    PrecoUnitario = artigo.Preco,
                    Previsto = true,
                    Adquirido = false,
                    Observacoes = ""
                };

                db.ItemComprasPlaneadas.Add(item);

                compra.DataHoraAlteracao = DateTime.Now;
                compra.AlteradoPor = sessao.UtilizadorAtual;

                db.SaveChanges();

                mensagem = "Item previsto adicionado com sucesso.";
                return true;
            }
        }
    }
}
