using IShopping.Controller; // Permite aceder às lógicas de controlo de artigos (Inserir, Editar, Eliminar)
using IShopping.Models;     // Permite interagir com as classes de dados e o Contexto da BD
using System;
using System.Data;
using System.Data.Entity;   // Necessário para usar o método .Include() para carregar tabelas relacionadas
using System.Linq;          // Necessário para usar expressões e filtros LINQ (Ex: .Any(), .Where(), .Select())
using System.Windows.Forms; // Necessário para a infraestrutura de componentes Windows Forms

namespace IShopping.Views
{
    // Classe do formulário responsável por gerir o catálogo de artigos/produtos do sistema (CRUD e Filtros)
    public partial class FormGestaoArtigo : Form
    {
        // CONSTRUTOR - Executado imediatamente ao instanciar o formulário
        public FormGestaoArtigo()
        {
            InitializeComponent(); // Inicializa os componentes visuais desenhados no Designer

            CarregarTipos(); // Preenche a ComboBox principal com os tipos de artigos existentes
            CarregarGrid();  // Carrega a datagridview com todos os artigos registados
        }

        private void FormGestaoArtigo_Load(object sender, EventArgs e)
        {
            CarregarComboTipoArtigo(); // Inicializa a ComboBox secundária utilizada para os filtros de pesquisa
        }

        // CARREGAR TIPOS - Preenche a ComboBox do formulário de inserção com dados vindos do Controller
        private void CarregarTipos()
        {
            // Define a lista completa de tipos devolvida pelo Controller como fonte de dados
            cmbTipoArtigo.DataSource = TipoArtigoController.Listar();

            cmbTipoArtigo.DisplayMember = "Nome"; // Define o texto que o utilizador vai ler no ecrã
            cmbTipoArtigo.ValueMember = "Id";     // Define o ID numérico que será guardado por trás na BD

            cmbTipoArtigo.SelectedIndex = -1;     // Garante que a ComboBox começa vazia (sem seleção padrão)
        }

        // LIMPAR CAMPOS - Função utilitária interna para esvaziar os controlos de texto e seleções
        public void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtPreco.Clear();
            cmbTipoArtigo.SelectedIndex = -1; // Desseleciona qualquer item ativo na ComboBox de inserção
        }

        // CARREGAR GRID - Alimenta a DataGridView com todos os artigos mapeados na Base de Dados
        public void CarregarGrid()
        {
            using (shoppingContext db = new shoppingContext())
            {
                dataGridView1.DataSource = null; // Corta o vínculo anterior para evitar duplicações de colunas

                // Usamos Projeção (.Select) para customizar a tabela e trazer o Nome real do Tipo e não apenas o ID
                dataGridView1.DataSource = db.Artigos
                    .Include(a => a.TipoArtigo) 
                    .Select(a => new
                    {
                        Id = a.Id,
                        Nome = a.Nome,
                        Preço = a.Preco,
                        TipoArtigo = a.TipoArtigo.Nome // Extrai o nome textual da categoria relacionada
                    })
                    .ToList(); // Executa a consulta e transforma num formato de lista compatível com a Grid
            }
        }

        // BOTÃO GUARDAR - Cria e insere um novo artigo na Base de Dados
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validação de interface: Bloqueia a gravação se existirem campos em branco ou sem categoria
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtPreco.Text) ||
                cmbTipoArtigo.SelectedIndex == -1)
            {
                MessageBox.Show("Preenche todos os campos.");
                return; // Aborta a inserção
            }

            // Validação de Duplicados: Verifica se o nome digitado já existe (ignorando maiúsculas/minúsculas)
            using (shoppingContext db = new shoppingContext())
            {
                bool existe = db.Artigos
                    .Any(a => a.Nome.ToLower() == txtNome.Text.ToLower());

                if (existe)
                {
                    MessageBox.Show("Já existe um artigo com esse nome.");
                    return; 
                }
            }

            decimal preco;
            // Tenta converter a string de texto do preço num formato decimal numérico válido
            if (!decimal.TryParse(txtPreco.Text, out preco))
            {
                MessageBox.Show("Preço inválido.");
                return; 
            }

            // Invoca o método de inserção do Controller passando os dados recolhidos do ecrã
            ArtigoController.Inserir(
                txtNome.Text,
                (int)cmbTipoArtigo.SelectedValue, // Converte o ID selecionado explicitamente para inteiro
                preco
            );

            MessageBox.Show("Artigo inserido com sucesso!");

            CarregarGrid();  // Recarrega a tabela com o novo artigo incluído
            LimparCampos();   // Limpa o ecrã para uma nova inserção
        }

        // BOTÃO EDITAR - Atualiza as propriedades de um artigo já existente
        private void button1_Click(object sender, EventArgs e)
        {
            int id;
            // Verifica se existe um ID numérico selecionado e carregado na caixa txtId
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("Seleciona um artigo.");
                return; 
            }

            // Validação de segurança para garantir que o novo nome não cria conflitos na BD
            using (shoppingContext db = new shoppingContext())
            {
                bool existe = db.Artigos
                    .Any(a => a.Nome.ToLower() == txtNome.Text.ToLower() && a.Id != id); // Ignora o próprio artigo em edição

                if (existe)
                {
                    MessageBox.Show("Já existe um artigo com esse nome.");
                    return;
                }
            }

            decimal preco; // Variável para armazenar o preço convertido
            // Verifica se o preço digitado corresponde a um número válido
            if (!decimal.TryParse(txtPreco.Text, out preco))
            {
                MessageBox.Show("Preço inválido.");
                return;
            }

            // Transmite as alterações atualizadas para a camada do Controller
            ArtigoController.Editar(
                id,
                txtNome.Text,
                (int)cmbTipoArtigo.SelectedValue,
                preco
            );

            MessageBox.Show("Artigo editado!");

            CarregarGrid();  // Atualiza a visualização dos dados na tabela
            LimparCampos();   // Limpa os campos de texto
        }

        // CARREGAR COMBO FILTRO - Alimenta a segunda ComboBox utilizada exclusivamente para filtragem na Grid
        private void CarregarComboTipoArtigo()
        {
            using (shoppingContext db = new shoppingContext())
            {
                cbTipoArtigo.DataSource = db.TipoArtigos.ToList();
                cbTipoArtigo.DisplayMember = "Nome";
                cbTipoArtigo.ValueMember = "Id";
                cbTipoArtigo.SelectedIndex = -1; // Inicia sem filtro selecionado
            }
        }

        // BOTÃO VER - Pesquisa um artigo pelo ID indicado e distribui as informações nos campos do formulário
        private void btnVer_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            // Procura o registo original na base de dados recorrendo ao ID
            Artigo artigo = ArtigoController.ProcurarPorId(id);

            // Se o artigo existir, preenche os controlos visuais correspondentes com os valores guardados
            if (artigo != null)
            {
                txtNome.Text = artigo.Nome;
                txtPreco.Text = artigo.Preco.ToString();
                cmbTipoArtigo.SelectedValue = artigo.TipoArtigoId; // Posiciona a ComboBox na categoria correta
            }
            else
            {
                MessageBox.Show("Artigo não encontrado.");
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
        }

        // BOTÃO VOLTAR - Encerra a janela de gestão de artigos
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // Fecha o formulário ativo
        }

        // BOTÃO ELIMINAR - Remove permanentemente um artigo com base no ID selecionado
        private void button5_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("Seleciona um artigo.");
                return;
            }

            // Mostra uma caixa de diálogo para evitar deleções acidentais pelo utilizador
            DialogResult resposta = MessageBox.Show(
                "Deseja eliminar este artigo?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Se o utilizador confirmar a intenção clicando em "Sim"
            if (resposta == DialogResult.Yes)
            {
                ArtigoController.Eliminar(id); // Executa o comando de eliminação no Controller

                MessageBox.Show("Artigo eliminado!");

                CarregarGrid();  // Remove a linha correspondente da visualização da Grid
                LimparCampos();   // Limpa os campos do ecrã
            }
        }

        // FILTRAR POR CATEGORIA - Dispara automaticamente sempre que o utilizador muda o Tipo na ComboBox de filtro
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Se a CheckBox "Todos" não estiver ativa e houver um tipo selecionado válido, aplica o filtro
                if (!cbxTodos.Checked &&
                    cbTipoArtigo.SelectedValue != null &&
                    cbTipoArtigo.SelectedValue is int)
                {
                    int tipoId = (int)cbTipoArtigo.SelectedValue; // Captura e converte o ID da categoria filtrada

                    // Filtra e reconstrói a estrutura de colunas da Grid exibindo apenas os elementos da categoria correspondente
                    dataGridView1.DataSource = db.Artigos
                        .Include(a => a.TipoArtigo)          // Realiza a junção de tabelas necessária
                        .Where(a => a.TipoArtigoId == tipoId) // Filtra os artigos que contêm o TipoArtigoId selecionado
                        .Select(a => new
                        {
                            Id = a.Id,
                            Nome = a.Nome,
                            Preço = a.Preco,
                            TipoArtigo = a.TipoArtigo.Nome
                        })
                        .ToList(); // Compila o resultado final em formato de listagem estática
                }
            }
        }

        // Fecha o form
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Botão limpar campos
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos(); // Invoca a rotina utilitária de limpeza de campos
        }

        // CHECKBOX MOSTRAR TODOS - Controla a alternância entre a listagem total e a filtragem seletiva por categoria
        private void cbxTodos_CheckedChanged(object sender, EventArgs e)
        {
            using (shoppingContext db = new shoppingContext())
            {
                // Se a caixa "Todos" for marcada, desativa a ComboBox e traz todos os artigos sem distinção
                if (cbxTodos.Checked)
                {
                    dataGridView1.DataSource = db.Artigos
                        .Include(a => a.TipoArtigo)
                        .Select(a => new
                        {
                            Id = a.Id,
                            Nome = a.Nome,
                            Preço = a.Preco,
                            TipoArtigo = a.TipoArtigo.Nome
                        })
                        .ToList();

                    cbTipoArtigo.Enabled = false; // Desativa o controlo de filtragem por categoria para evitar conflitos
                }
                // Se a caixa for desmarcada, reativa o controlo e aplica de imediato o filtro do item atualmente selecionado
                else
                {
                    cbTipoArtigo.Enabled = true; // Devolve o controlo da ComboBox ao utilizador

                    if (cbTipoArtigo.SelectedValue != null && cbTipoArtigo.SelectedValue is int)
                    {
                        int tipoId = (int)cbTipoArtigo.SelectedValue;

                        dataGridView1.DataSource = db.Artigos
                            .Include(a => a.TipoArtigo)
                            .Where(a => a.TipoArtigoId == tipoId)
                            .Select(a => new
                            {
                                Id = a.Id,
                                Nome = a.Nome,
                                Preço = a.Preco,
                                TipoArtigo = a.TipoArtigo.Nome
                            })
                            .ToList();
                    }
                }
            }
        }

        // Evento gerado por clique nas células da Grid (Mantido vazio para evitar erros no ficheiro .Designer.cs)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}