using Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace DAL
{
    public class ProdutoDAL
    {
        public void Inserir(Produto _produto, SqlTransaction _transaction = null)
        {
            SqlTransaction transaction = _transaction;
            using (SqlConnection cn = new SqlConnection(Conexao.StringDeConexao))
            {
                using (SqlCommand cmd = new SqlCommand(@"INSERT INTO Produto(Nome, Preco, Estoque, CodBarras) 
                                    VALUES(@Nome, @Preco, @Estoque, @CodBarras)"))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@Id", _produto.Id);
                        cmd.Parameters.AddWithValue("@Nome", _produto.Nome);
                        cmd.Parameters.AddWithValue("@Preco", _produto.Preco);
                        cmd.Parameters.AddWithValue("@Estoque", _produto.Estoque);
                        cmd.Parameters.AddWithValue("@CodBarras", _produto.CodBarras);

                        if (_transaction == null)
                        {
                            cn.Open();
                            transaction = cn.BeginTransaction();
                        }
                        cmd.Transaction = transaction;
                        cmd.Connection = transaction.Connection;

                        cmd.ExecuteNonQuery();

                        if (_transaction == null)
                            transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (transaction.Connection != null && transaction.Connection.State == ConnectionState.Open)

                            transaction.Rollback();
                        throw new Exception("Ocrreu um erro ao tentar inserir o produto no Banco de dados.", ex);
                    }
                }
            }
        }
        public void Alterar(Produto _produto, SqlTransaction _transaction = null)
        {
            SqlTransaction transaction = _transaction;
            using (SqlConnection cn = new SqlConnection(Conexao.StringDeConexao))
            {
                using (SqlCommand cmd = new SqlCommand(@"UPDATE PRODUTO SET Nome = @Nome, Preco = @Preco, Estoque = @Estoque, CodBarras = @CodBarras
                                                        WHERE Id = @Id"))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@Id", _produto.Id);
                        cmd.Parameters.AddWithValue("@Nome", _produto.Nome);
                        cmd.Parameters.AddWithValue("@Preco", _produto.Preco);
                        cmd.Parameters.AddWithValue("@Estoque", _produto.Estoque);
                        cmd.Parameters.AddWithValue("@CodBarras", _produto.CodBarras);

                        if (_transaction == null)
                        {
                            cn.Open();
                            transaction = cn.BeginTransaction();
                        }
                        cmd.Transaction = transaction;
                        cmd.Connection = transaction.Connection;

                        cmd.ExecuteNonQuery();

                        if (_transaction == null)
                            transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (transaction.Connection != null && transaction.Connection.State == ConnectionState.Open)

                            transaction.Rollback();
                        throw new Exception("Ocorreu um erro ao tentar alterar um produto no banco de dados.", ex);
                    }
                }
            }
        }
        public void Excluir(int _id, SqlTransaction _transaction = null)
        {
            SqlTransaction transaction = _transaction;
            using (SqlConnection cn = new SqlConnection(Conexao.StringDeConexao))
            {
                using (SqlCommand cmd = new SqlCommand(@"DELETE FROM PRODUTO                          
                                    WHERE Id = @Id"))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@Id", _id);

                        if (_transaction == null)
                        {
                            cn.Open();
                            transaction = cn.BeginTransaction();
                        }
                        cmd.Transaction = transaction;
                        cmd.Connection = transaction.Connection;

                        cmd.ExecuteNonQuery();

                        if (_transaction == null)
                            transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        if (transaction.Connection != null && transaction.Connection.State == ConnectionState.Open)

                            transaction.Rollback();
                        throw new Exception("Ocorreu um erro ao tentar inserir um produto no banco de dados.", ex);
                    }
                }
            }
        }
        public List<Produto> BuscarTodos()
        {

            List<Produto> produtoLista = new List<Produto>();
            Produto produto;
            SqlConnection cn = new SqlConnection(Conexao.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Id, Nome, Preco, Estoque, CodBarras FROM PRODUTO";

                cmd.CommandType = System.Data.CommandType.Text;

                cn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        produto = PreencherObjeto(rd);
                        produtoLista.Add(produto);
                    }
                }

                return produtoLista;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar buscar um produto no banco de dados.", ex);
            }
        }
        public Produto BuscarPorId(int _Id)
        {

            Produto produto;
            SqlConnection cn = new SqlConnection(Conexao.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT Id, Nome, Preco, Estoque, CodBarras
                                    FROM PRODUTO
                                    WHERE Id = @Id";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", _Id);
                produto = new Produto();
                cn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        produto = PreencherObjeto(rd);
                    }
                }

                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar buscar um produto no banco de dados.", ex);
            }
        }
        public List<Produto> BuscarPorNome(string _nome)
        {
            List<Produto> produtoLista = new List<Produto>();
            Produto produto;
            SqlConnection cn = new SqlConnection(Conexao.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT Id, Nome, Preco, Estoque, CodBarras
                                    FROM PRODUTO 
                                    WHERE Nome LIKE @Nome";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue(@"Nome", "%" + _nome + "%");

                cn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        produto = PreencherObjeto(rd);
                        produtoLista.Add(produto);
                    }
                }

                return produtoLista;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar buscar um produto por nome no banco de dados.", ex);
            }
            finally
            {
                cn.Close();
            }
        }
        public Produto BuscarPorCodBarras(string _codBarras)
        {
            Produto produto;

            SqlConnection cn = new SqlConnection(Conexao.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT Id, Nome, Preco, Estoque, CodBarras
                                    FROM PRODUTO 
                                    WHERE CodBarras LIKE @CodBarras";
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue(@"CodBarras", "%" + _codBarras + "%");


                cn.Open();

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    produto = new Produto();
                    while (rd.Read())
                    {
                        produto = PreencherObjeto(rd);
                    }
                }

                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar buscar um codbarras de produto no banco de dados.", ex);
            }
            finally
            {
                cn.Close();
            }
        }

        private static Produto PreencherObjeto(SqlDataReader _rd)
        {
            Produto produto = new Produto();
            produto.Id = (int)_rd["Id"];
            produto.Nome = _rd["Nome"].ToString();
            produto.Preco = (double)_rd["Preco"];
            produto.Estoque = (double)_rd["Estoque"];
            produto.CodBarras = _rd["CodBarras"].ToString();
            return produto;
        }
    }
}
