using IShopping.Controller; // Permite aceder às lógicas de controlo de utilizadores (Inserir, Editar, Eliminar)
using IShopping.Models;     // Permite interagir com o modelo de dados 'Utilizador' e o contexto da BD
using System;
using System.Windows.Forms;
using System.Linq;         // Necessário para usar expressões LINQ como o .Any()

namespace IShopping.Views
{
    // Classe do formulário responsável pela interface de gestão (CRUD) de utilizadores do sistema
    public partial class FormGerirUtilizadores : Form
    {
        // CONSTRUTOR - Executado ao criar a instância do formulário
        public FormGerirUtilizadores()
        {
            InitializeComponent(); // Inicializa os componentes visuais desenhados no ecrã

            CarregarUtilizadores(); // Carrega automaticamente a lista de utilizadores na tabela ao abrir
        }

        // BOTÃO GUARDAR - Evento para registar um novo utilizador no sistema
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validação de segurança básica: impede campos vazios ou cheios de espaços
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Preenche todos os campos.");
                return; // Aborta a execução do método se faltarem dados
            }

            // Bloco de ligação temporária à Base de Dados para validação de duplicados
            using (shoppingContext db = new shoppingContext())
            {
                // O método .Any() pesquisa na tabela e devolve 'true' se já existir o username digitado
                bool existe = db.Utilizadores.Any(u => u.Username == txtNome.Text);

                if (existe)
                {
                    MessageBox.Show("Username já existe!");
                    return; // Aborta a gravação para evitar duplicados na BD
                }
            }

            // Se o nome for único, invoca o método de inserção do Controller correspondente
            // Passa o nome, password e o utilizador que iniciou a sessão (auditoria de criação)
            UtilizadorController.Inserir(
                txtNome.Text,
                txtPassword.Text,
                sessao.UtilizadorAtual
            );

            MessageBox.Show("Utilizador adicionado!");

            CarregarUtilizadores(); // Atualiza a DataGridView para mostrar o novo utilizador
            LimparCampos();         // Limpa as caixas de texto para o próximo registo
        }

        // LISTAR UTILIZADORES - Alimenta a DataGridView com os dados atualizados da BD
        private void CarregarUtilizadores()
        {
            using (shoppingContext db = new shoppingContext())
            {
                dataGridView1.DataSource = null; // Remove o vínculo anterior para forçar o recarregamento limpo

                // Define a lista devolvida pelo Controller como a nova fonte de dados da tabela visual
                dataGridView1.DataSource = UtilizadorController.Listar();
            }
        }

        // LIMPAR CAMPOS - Função utilitária interna para esvaziar as caixas de texto de entrada
        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtPassword.Clear();
        }

        // Evento padrão de carregamento do formulário (vazio, mas mantido para não quebrar o ficheiro Designer)
        private void FormGerirUtilizadores_Load(object sender, EventArgs e)
        {
        }

        // BOTÃO VER - Procura um utilizador pelo ID digitado e mostra os dados nos campos de texto
        private void btnVer_Click(object sender, EventArgs e)
        {
            int id;

            // Tenta converter o texto do campo txtId para um número inteiro válido
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("ID inválido.");
                return; // A mensagem aparece se utilizador digitou letras ou deixou o ID vazio
            }

            // Pesquisa o utilizador correspondente através do Controller
            Utilizador utilizador = UtilizadorController.ProcurarPorId(id);

            // Se o utilizador for encontrado na BD, preenche as caixas de texto correspondentes
            if (utilizador != null)
            {
                txtNome.Text = utilizador.Username;
                txtPassword.Text = utilizador.Password;
            }
            else
            {
                MessageBox.Show("Utilizador não encontrado.");
            }
        }

        // BOTÃO EDITAR - Atualiza os dados de um utilizador existente com base no ID
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Valida se os campos de texto novos não foram deixados em branco
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            int id;

            // Garante que o ID indicado para edição é um valor numérico válido
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("O ID tem de ser numérico.");
                return;
            }

            // Submete as alterações ao Controller de Utilizadores
            UtilizadorController.Editar(
                id,
                txtNome.Text,
                txtPassword.Text,
                sessao.UtilizadorAtual // Envia quem editou 
            );

            MessageBox.Show("Utilizador editado com sucesso!");

            CarregarUtilizadores(); // Atualiza a tabela com os novos dados modificados
            LimparCampos();         // Limpa os campos 
        }

        // BOTÃO ELIMINAR - Remove permanentemente um utilizador através do seu ID
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Tenta extrair e validar o ID numérico diretamente na declaração da variável 'id'
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("O ID tem de ser numérico.");
                return;
            }

            // Executa a remoção do registo no banco de dados através do Controller
            UtilizadorController.Eliminar(id);

            MessageBox.Show("Utilizador eliminado com sucesso!");

            CarregarUtilizadores(); // Atualiza a tabela do ecrã (o utilizador removido desaparece da lista)
            LimparCampos();         // Limpa o formulário
        }

        // BOTÃO LIMPAR - Permite limpar  os campos do ecrã a qualquer momento
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos(); // Invoca a função utilitária de limpeza
        }

        // BOTÃO FECHAR/VOLTAR (button2) - Encerra o ecrã atual de gestão
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha a janela atual e retorna ao ecrã anterior do sistema
        }

        // Evento gerado acidentalmente por duplo clique na caixa de texto (Mantenha para evitar erros de compilação)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}