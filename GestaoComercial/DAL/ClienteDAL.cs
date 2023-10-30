using Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace DAL
{
    public class ClienteDAL
    {
        public void Inserir(Cliente _cliente, SqlTransaction _transaction = null)
        {
            SqlTransaction transaction = _transaction;
            using (SqlConnection cn = new SqlConnection(Conexao.StringDeConexao))
            {
                using (SqlCommand cmd = new SqlCommand(@"INSERT INTO CLIENTE(Nome, Fone) 
                                    VALUES(@Nome, @Fone)"))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@Id", _cliente.Id);
                        cmd.Parameters.AddWithValue("@Nome", _cliente.Nome);
                        cmd.Parameters.AddWithValue("@Fone", _cliente.Fone);

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
                        throw new Exception("Ocrreu um erro ao tentar inserir o cliente no Banco de dados.");
                    }
                }
            }
        }
        public void Alterar(Cliente _cliente, SqlTransaction _transaction = null)
        {
            SqlTransaction transaction = _transaction;
            using (SqlConnection cn = new SqlConnection(Conexao.StringDeConexao))
            {
                using (SqlCommand cmd = new SqlCommand(@"UPDATE CLIENTE SET Nome = @Nome, Fone = @Fone     
                                                         WHERE Id = @Id"))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@Id", _cliente.Id);
                        cmd.Parameters.AddWithValue("@Nome", _cliente.Nome);
                        cmd.Parameters.AddWithValue("@Fone", _cliente.Fone);

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
                        {

                        }
                        transaction.Rollback();
                        throw new Exception("Ocorreu um erro ao tentar alterar um cliente no banco de dados.", ex);
                    }
                }
            }
        }
        public void Excluir(int _id, SqlTransaction _transaction = null)
        {
            SqlTransaction transaction = _transaction;
            using (SqlConnection cn = new SqlConnection(Conexao.StringDeConexao))
            {
                using (SqlCommand cmd = new SqlCommand(@"DELETE FROM CLIENTE                          
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
                        {

                        }
                        transaction.Rollback();
                        throw new Exception("Ocorreu um erro ao tentar excluir um cliente no banco de dados.", ex);
                    }
                }
            }
        }
        public List<Cliente> BuscarTodos()
        {
            List<Cliente> clienteLista = new List<Cliente>();
            Cliente cliente;
            SqlConnection cn = new SqlConnection(Conexao.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Id, Nome, Fone FROM CLIENTE";
                cmd.CommandType = System.Data.CommandType.Text;

                cn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        cliente = PreencherObjeto(rd);
                        clienteLista.Add(cliente);
                    }
                }

                return clienteLista;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar buscar um cliente no banco de dados.", ex);
            }
            finally
            {
                cn.Close();
            }
        }

        public Cliente BuscarPorId(int _id)
        {

            Cliente cliente;
            SqlConnection cn = new SqlConnection(Conexao.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT Id, Nome, Fone
                                    FROM CLIENTE
                                    WHERE Id = @Id";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", _id);
                cliente = new Cliente();
                cn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        cliente = PreencherObjeto(rd);
                    }
                }
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar buscar um cliente por id no banco de dados.", ex);
            }
        }

        public List<Cliente> BuscarPorNome(string _nome)
        {
            List<Cliente> clienteLista = new List<Cliente>();
            Cliente cliente;

            SqlConnection cn = new SqlConnection(Conexao.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT  Id, Nome, Fone
                                    FROM CLIENTE 
                                    WHERE Nome LIKE @Nome";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", "%" + _nome + "%");


                cn.Open();

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    cliente = new Cliente();
                    while (rd.Read())
                    {
                        cliente = PreencherObjeto(rd);
                        clienteLista.Add(cliente);
                    }
                }
                return clienteLista;

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar buscar um nome de cliente no banco de dados.", ex);
            }
            finally
            {
                cn.Close();
            }
        }

        public List<Cliente> BuscarPorFone(string _fone)
        {
            List<Cliente> clienteLista = new List<Cliente>();
            Cliente cliente;

            SqlConnection cn = new SqlConnection(Conexao.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT  Id, Nome, Fone
                                    FROM CLIENTE 
                                    WHERE Fone LIKE @Fone";
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@Fone", "%" + _fone + "%");


                cn.Open();

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    cliente = new Cliente();
                    while (rd.Read())
                    {
                        cliente = PreencherObjeto(rd);
                        clienteLista.Add(cliente);
                    }
                }
                return clienteLista;

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar buscar um telefone de cliente no banco de dados.", ex);
            }
            finally
            {
                cn.Close();
            }
        }

        private static Cliente PreencherObjeto(SqlDataReader _rd)
        {
            Cliente cliente = new Cliente();
            cliente.Id = (int)_rd["Id"];
            cliente.Nome = _rd["Nome"].ToString();
            cliente.Fone = _rd["Fone"].ToString();
            return cliente;
        }

    }
}
