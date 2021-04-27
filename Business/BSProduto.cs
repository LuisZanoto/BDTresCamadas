using DataAccess;
using System;
using System.Data;


namespace Business
{
    public class BSProduto
    {
        //-----------------------------------------------
        //                  ATRIBUTOS
        //-----------------------------------------------
        private int idProduto;
        private string nome;
        private decimal preco;
        private int qtdEstoque;


        //-----------------------------------------------
        //                PROPRIEDADES 
        //-----------------------------------------------
        public int IdProduto { get => idProduto; set => idProduto = value; }
        public string Nome { get => nome; set => nome = value; }
        public decimal Preco { get => preco; set => preco = value; }
        public int QtdEstoque { get => qtdEstoque; set => qtdEstoque = value; }


        //-----------------------------------------------
        //                   MÉTODOS 
        //----------------------------------------------- 
        public void inserir()
        {
            try
            {
                //-------------------------------------------
                // Regra de negócio
                //-------------------------------------------
                if (qtdEstoque < 30)
                {
                    throw new Exception("A quantidade não pode ser menor que 30 unidades!");
                }

                //DataAcess.DAProduto
                DAProduto o_DAProduto = new DAProduto();

                o_DAProduto.IdProduto = idProduto;
                o_DAProduto.Nome = nome;
                o_DAProduto.Preco = preco;
                o_DAProduto.QtdEstoque = qtdEstoque;
                o_DAProduto.inserir();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nBSProduto.inserir");
            }
        }

        //---------------------------------------------------------------
        //
        //---------------------------------------------------------------
        public DataTable selecionar()
        {
            try
            {
                DAProduto o_DAProduto = new DAProduto();
                DataTable o_PesqProduto;

                o_PesqProduto = o_DAProduto.selecionar();


                return o_PesqProduto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nBSProduto.selecionar");
            }
        }


        //---------------------------------------------------------------
        //
        //---------------------------------------------------------------        
        public void atualizar()
        {
            try
            {
                //-------------------------------------------
                // Regra de negócio
                //-------------------------------------------
                if (qtdEstoque < 30)
                {
                    throw new Exception("A quantidade não pode ser menor que 30 unidades!");
                }

                //DataAcess.DAProduto
                DAProduto o_DAProduto = new DAProduto();

                o_DAProduto.IdProduto = idProduto;
                o_DAProduto.Nome = nome;
                o_DAProduto.Preco = preco;
                o_DAProduto.QtdEstoque = qtdEstoque;
                o_DAProduto.atualizar();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nBSProduto.atualizar");
            }

        }

        //---------------------------------------------------------------
        //
        //---------------------------------------------------------------
        public void excluir()
        {
            try
            {
                DAProduto o_DAProduto = new DAProduto();
                o_DAProduto.IdProduto = idProduto;
                o_DAProduto.excluir();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nBSProduto.excluir");
            }
        }

    }
}
