using Business;
using System;
using System.Data;
using System.Windows.Forms;


namespace FrontEnd
{
    public partial class frmCadProduto : Form
    {
        public frmCadProduto()
        {
            InitializeComponent();
        }

        private void frmCadProduto_Load(object sender, EventArgs e)
        {
            obterProduto();
        }

        private void BtnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                BSProduto o_Produto = new BSProduto();
                // VERIFICA 


                if (txtNome.Text.ToString().Trim() == "") // TRIM DESCONSIDERA ESPAÇO EM BRANCO
                {
                    MessageBox.Show("Por favor informe o nome do produto!", "Inserir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNome.Focus();
                    return;
                }


                //-------------------------------------------
                // Regra de negócio
                //-------------------------------------------
                int QtdEstoque;
                QtdEstoque = int.Parse(nudQtdEstoque.Text.ToString());

                if (QtdEstoque < 30)
                {
                    MessageBox.Show("A quantidade não pode ser menor que 30 unidades!",
                                    "Inserir", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    txtNome.Focus();
                    return;
                }

                o_Produto.Nome = txtNome.Text;
                o_Produto.Preco = decimal.Parse(nudPreco.Text.ToString());
                o_Produto.QtdEstoque = QtdEstoque;
                o_Produto.inserir();
                MessageBox.Show(" Inserido ok");

                obterProduto();

                limparCampos();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "Inserir",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {

                BSProduto o_Produto = new BSProduto();

                int idProduto;
                String nome;
                decimal preco;
                int qtdEstoque;

                //-------------------------------------------
                // Verificação de NOT NULL
                //-------------------------------------------
                if (txtNome.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Por favor informe o nome do produto!",
                                    "Atualizar", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    txtNome.Focus();
                    return;
                }


                idProduto = int.Parse(txtIDProduto.Text.ToString());
                nome = txtNome.Text.ToString();
                preco = nudPreco.Value;
                qtdEstoque = (int)nudQtdEstoque.Value;

                o_Produto.IdProduto = idProduto;
                o_Produto.Nome = nome;
                o_Produto.Preco = preco;
                o_Produto.QtdEstoque = qtdEstoque;
                o_Produto.atualizar();

                MessageBox.Show("Alteração efetuada com sucesso.", "Alterar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                obterProduto();

                limparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "Produto",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                BSProduto o_Produto = new BSProduto();

                DialogResult escolha; // variavel de resposta de botão

                // Confirma Exclusão
                escolha = MessageBox.Show("Deseja realmente excluir os dados?",
                    "Excluir", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (escolha == DialogResult.Yes)
                {
                    int idProduto;

                    idProduto = int.Parse(txtIDProduto.Text.ToString());

                    o_Produto.IdProduto = idProduto;

                   o_Produto.excluir();

                    MessageBox.Show("Exclusão efetuada com sucesso.", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    obterProduto();

                    limparCampos();
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "Produto",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void obterProduto()
        {
            try
            {
                BSProduto o_Produto = new BSProduto();
                DataTable dtProduto;
                dtProduto = o_Produto.selecionar();
                dgvProduto.DataSource = dtProduto;

                configGridProduto();




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "Obter Produto",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void configGridProduto()
        {
            // Caso não tenha sido feito nas propriedade em modo design
            dgvProduto.AllowUserToAddRows = false;
            dgvProduto.AllowUserToDeleteRows = false;

            dgvProduto.Columns[0].HeaderText = "ID";
            dgvProduto.Columns[0].Width = 70;
            dgvProduto.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvProduto.Columns[1].HeaderText = "Nome";
            dgvProduto.Columns[1].Width = 450;

            dgvProduto.Columns[2].HeaderText = "Preço";
            dgvProduto.Columns[2].Width = 100;
            dgvProduto.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvProduto.Columns[3].HeaderText = "Qtde";
            dgvProduto.Columns[3].Width = 70;
            dgvProduto.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void limparCampos()
        {
            txtIDProduto.Text = "";
            txtNome.Text = "";
            nudPreco.Value = 0.00m;
            nudQtdEstoque.Value = 0m;

            BtnInserir.Enabled = true;
            BtnAtualizar.Enabled = false;
            BtnLimpar.Enabled = false;
        }



        private void dgvProduto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int nroLin;

            nroLin = e.RowIndex;

            if (nroLin != -1)
            {
                txtIDProduto.Text = dgvProduto.Rows[nroLin].Cells[0].Value.ToString();
                txtNome.Text = dgvProduto.Rows[nroLin].Cells[1].Value.ToString();
                nudPreco.Value = decimal.Parse(dgvProduto.Rows[nroLin].Cells[2].Value.ToString());
                nudQtdEstoque.Value = int.Parse(dgvProduto.Rows[nroLin].Cells[3].Value.ToString());


                // Ajuste da lógica dos botoes
                BtnInserir.Enabled = false;
                BtnAtualizar.Enabled = true;
                BtnExcluir.Enabled = true;
            }
        }
    }


    
}
