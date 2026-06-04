using IShopping.Controller;
using IShopping.Models;
using IShopping.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IShopping.Views
{
    public partial class FormPlaneamentoCompra : Form
    {
        public FormPlaneamentoCompra()
        {
            InitializeComponent();
        }


        private void bntAlterarCompra_Click(object sender, EventArgs e)
        {
            FormAlteracaoPlaneada form = new FormAlteracaoPlaneada(); // Cria uma nova instância do FormAlteracaoPlaneada
            form.ShowDialog(); // Mostra o FormAlteracaoPlaneada como um diálogo modal
            // O código após ShowDialog() será executado somente após o FormAlteracaoPlaneada ser fechado
        }



        // Método para atualizar a lista de compras, chamado no Load e ao alterar os filtros
        private void Form_Load(object sender, EventArgs e)
        {
            cmbFiltro.Items.Clear();
            cmbFiltro.Items.Add("Todas"); // Opção para mostrar todas as compras, independentemente do estado
            cmbFiltro.Items.Add("Abertas"); //Adiciona Opção Abertas no Combobox
            cmbFiltro.Items.Add("Fechadas"); //Adiciona Opção Fechadas no combobox

            cmbFiltro.SelectedIndex = 0;
            AtualizarGrelha();
        }

        //Atualiza a Grelha
        private void AtualizarGrelha()
        {
            string filtro;

            if (cmbFiltro.SelectedItem != null) //Se o utilizador tiver selecionado um filtro, usa-o; caso contrário, assume "Todas"
            {
                filtro = cmbFiltro.SelectedItem.ToString(); // Converte o item selecionado para string e armazena na variável filtro
            }
            else
            {
                filtro = "Todas";
            }

            using (shoppingContext db = new shoppingContext())
            {
                // Começamos com a query base apontada para as Compras Planeadas
                var query = db.ComprasPlaneadas.AsQueryable();

                // Aplica o filtro de estado conforme a seleção do utilizador
                if (filtro == "Abertas")
                {
                    query = query.Where(c => c.Fechada == false); //query filtra as compras onde Fechada é false, ou seja, as compras abertas
                                                                  //.where é um método de extensão do LINQ que permite filtrar os dados com base em uma condição. Neste caso, a condição é c.Fechada == false, o que significa que estamos selecionando apenas as compras onde a propriedade Fechada é igual a false, ou seja, as compras que estão abertas.
                }

                else if (filtro == "Fechadas")
                {
                    query = query.Where(c => c.Fechada == true);
                }

                // Usa o .Select (exatamente como fez nos Artigos) para definir as colunas da Grid
                dataGridView1.DataSource = query
                    .Select(c => new
                    {
                        Id = c.Id,
                        Nome = c.NomeCompra,
                        Fechada = c.Fechada,
                        CriadoPor = c.CriadoPor,
                        CriadoEm = c.DataCriacao
                    })
                    .ToList();
            }
        }

        // Método para carregar os dados das compras na grid, chamado no Load e ao alterar os filtros
        public void carregarGridCompras()
        {
            using (shoppingContext db = new shoppingContext())
            {
                var compras = db.ComprasPlaneadas
                    .Select(c => new
                    {
                        Id = c.Id,
                        Nome = c.NomeCompra,
                        DataCriacao = c.DataCriacao,
                        Estado = c.Fechada,
                        CriadoPor = c.CriadoPor // Ajuste conforme o seu modelo
                    }).ToList();
                dataGridView1.DataSource = compras; // Atribui a lista de compras à fonte de dados da grid para exibição
            }
        }

        // Botão para fechar o formulário de planeamento de compras
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AparecerOrçamento_Click(object sender, EventArgs e)

        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void cmbTipoArtigo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void rdTodos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnAlteracao_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecione uma compra.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int compraId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

            using (var db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.Find(compraId);

                if (compra == null)
                {
                    MessageBox.Show("Compra não encontrada.");
                    return;
                }

                if (compra.Fechada == true)
                {
                    MessageBox.Show("Não pode alterar uma compra fechada.");
                    return;
                }

                // PASSA A COMPRA PARA O FORM
                /*var frm = new FormAlteracaoPlaneada(compra);

                 if (frm.ShowDialog() == DialogResult.OK)
                 {
                     AtualizarGrelha(); // refresh
                 }*/
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormPlaneamentoCompra_Load(object sender, EventArgs e)
        {

        }

        // Botão para criar uma nova compra, que abre o FormAlteracaoPlaneada para inserir os detalhes da nova compra
        private void bntNovaCompra_Click(object sender, EventArgs e)
        {
            FormAlteracaoPlaneada form = new FormAlteracaoPlaneada(); // Cria uma nova instância do FormAlteracaoPlaneada
            form.ShowDialog(); // Mostra o FormAlteracaoPlaneada como um diálogo modal
            // O código após ShowDialog() será executado somente após o FormAlteracaoPlaneada ser fechado
        }

        private void btnAlteracao_Click_1(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecione uma compra.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int compraId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

            using (var db = new shoppingContext())
            {
                var compra = db.ComprasPlaneadas.Find(compraId);

                if (compra == null)
                {
                    MessageBox.Show("Compra não encontrada.");
                    return;
                }

                if (compra.Fechada == true)
                {
                    MessageBox.Show("Não pode alterar uma compra fechada.");
                    return;
                }

                // PASSA A COMPRA PARA O FORM
                /*var frm = new FormAlteracaoPlaneada(compra);

                 if (frm.ShowDialog() == DialogResult.OK)
                 {
                     AtualizarGrelha(); // refresh
                 }*/
            }
        }

        // Botão para fechar o formulário de planeamento de compras
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
