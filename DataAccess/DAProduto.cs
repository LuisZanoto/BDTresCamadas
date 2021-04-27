using Npgsql;
using System;
//-------------------------------
//       A D I C I O N A R 
//-------------------------------
using System.Data;


namespace DataAccess
{
    public class DAProduto
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
  
                // Prepara conexão com o Banco de dados
                NpgsqlConnection conBanco;
                String strConexao;
                strConexao = "Server=localhost;User Id=postgres; Port=5432; " +
                             "Password=Canela14; Database=estoque";

                conBanco = new NpgsqlConnection(strConexao);

                // Prepara comando INSERT
                String strComando;
                //strComando = "INSERT INTO produto(nome, preco, qtdestoque) " +
                //            "VALUES('Pão', 3.5, 10)";
                //strComando = "INSERT INTO produto(nome, preco, qtdestoque) " +
                //             "VALUES('"+ nome +"', "+ preco +", "+ qtdEstoque +")";

                strComando = "INSERT INTO produto(nome, preco, qtdestoque) " +
                             "VALUES(@nome, @preco, @qtdestoque)";



                NpgsqlCommand cmdSQL;
                cmdSQL = new NpgsqlCommand(strComando, conBanco);

                // Criação de parametros
                NpgsqlParameter o_Parametro = new NpgsqlParameter();
                o_Parametro.ParameterName = "@nome";
                o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                o_Parametro.Size = 100;
                o_Parametro.Value = nome;
                cmdSQL.Parameters.Add(o_Parametro);

                o_Parametro = new NpgsqlParameter();
                o_Parametro.ParameterName = "@preco";
                o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Numeric;
                o_Parametro.Value = preco;
                cmdSQL.Parameters.Add(o_Parametro);

                o_Parametro = new NpgsqlParameter();
                o_Parametro.ParameterName = "@qtdestoque";
                o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
                o_Parametro.Value = qtdEstoque;
                cmdSQL.Parameters.Add(o_Parametro);


                // Abre a conexão e executa Insert
                conBanco.Open();

                //Prepara
                cmdSQL.Prepare();
                cmdSQL.ExecuteNonQuery();



                conBanco.Close();

            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nDAProduto.inserir");
            }

        }


        //---------------------------------------------------------------
        //
        //---------------------------------------------------------------
        public DataTable selecionar()
        {
            try
            {

                // Prepara conexão com o Banco de dados
                NpgsqlConnection conBanco;
                String strConexao;
                strConexao = "Server=localhost;User Id=postgres; Port=5432; " +
                             "Password=Canela14; Database=estoque";

                conBanco = new NpgsqlConnection(strConexao);



                // Preparar o comando Select
                String strComando;
                strComando = "Select * From produto Order By idproduto";
                NpgsqlDataAdapter daPesquisa;
                daPesquisa = new NpgsqlDataAdapter(strComando, conBanco);


                // Abre conexao
                conBanco.Open();


                DataTable dtProduto = new DataTable();
                daPesquisa.Fill(dtProduto);

                conBanco.Close();




                return dtProduto;
            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nDAProduto.selecionar");
            }

        }


        //---------------------------------------------------------------
        //
        //---------------------------------------------------------------        
        public void atualizar()
        {
            try
            {
                //-------------------------------------------------
                // Preparando a conexão com o banco de dados
                //-------------------------------------------------
                String strConexao;
                strConexao = "Server=localhost;User Id=postgres; Port=5432; " +
                             "Password=Canela14; Database=estoque";

                NpgsqlConnection conBanco;
                conBanco = new NpgsqlConnection(strConexao);


                //-------------------------------------------------
                // Preparando o comando UPDATE
                //-------------------------------------------------
                String strComando;

                strComando = "UPDATE produto SET nome = @nome, preco = @preco, " +
                             "qtdestoque = @qtdestoque WHERE idproduto = @idproduto";

                NpgsqlCommand cmdSQL;
                cmdSQL = new NpgsqlCommand(strComando, conBanco);


                //-------------------------------------------------
                // Criando de parâmetros
                //-------------------------------------------------
                NpgsqlParameter o_Parametro = new NpgsqlParameter();
                o_Parametro.ParameterName = "@nome";
                o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                o_Parametro.Size = 100;
                o_Parametro.Value = nome;
                cmdSQL.Parameters.Add(o_Parametro);


                o_Parametro = new NpgsqlParameter();
                o_Parametro.ParameterName = "@preco";
                o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Numeric;
                o_Parametro.Value = preco;
                cmdSQL.Parameters.Add(o_Parametro);


                o_Parametro = new NpgsqlParameter();
                o_Parametro.ParameterName = "@qtdestoque";
                o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
                o_Parametro.Value = qtdEstoque;
                cmdSQL.Parameters.Add(o_Parametro);


                o_Parametro = new NpgsqlParameter();
                o_Parametro.ParameterName = "@idproduto";
                o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
                o_Parametro.Value = idProduto;
                cmdSQL.Parameters.Add(o_Parametro);


                //-------------------------------------------------
                // Abre a conexão com o banco e executa o Update
                //-------------------------------------------------
                conBanco.Open();

                // Prepare() é obrigatório ser chamada para configurar
                // internamente o comando SQL e os parametros informados.                
                cmdSQL.Prepare();
                cmdSQL.ExecuteNonQuery();


                conBanco.Close();




            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nDAProduto.atualizar");
            }
        }

        //---------------------------------------------------------------
        //
        //---------------------------------------------------------------
        public void excluir()
        {
            try
            {
                //-------------------------------------------------
                // Preparando a conexão com o banco de dados
                //-------------------------------------------------
                String strConexao;
                strConexao = "Server=localhost;User Id=postgres; Port=5432; " +
                             "Password=Canela14; Database=estoque";

                NpgsqlConnection conBanco;
                conBanco = new NpgsqlConnection(strConexao);


                //-------------------------------------------------
                // Preparando o comando UPDATE
                //-------------------------------------------------
                String strComando;

                strComando = "Delete From produto WHERE idproduto = @idproduto";

                NpgsqlCommand cmdSQL;
                cmdSQL = new NpgsqlCommand(strComando, conBanco);


                //-------------------------------------------------
                // Criando de parâmetros
                //-------------------------------------------------
                NpgsqlParameter o_Parametro = new NpgsqlParameter();


                o_Parametro = new NpgsqlParameter();
                o_Parametro.ParameterName = "@idproduto";
                o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
                o_Parametro.Value = idProduto;
                cmdSQL.Parameters.Add(o_Parametro);


                //-------------------------------------------------
                // Abre a conexão com o banco e executa o Update
                //-------------------------------------------------
                conBanco.Open();

                // Prepare() é obrigatório ser chamada para configurar
                // internamente o comando SQL e os parametros informados.                
                cmdSQL.Prepare();
                cmdSQL.ExecuteNonQuery();


                conBanco.Close();
            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nDAProduto.excluir");
            }

        }





    }
}
