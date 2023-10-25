using Models;
using System.Data.SqlClient;
namespace DAL
{
    public class ClienteDAL
    {
        public void Inserir(Cliente _cliente)
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Cliente(Id, Nome, Fone) 
                                    VALUES(@Id, @Nome, @Fone)";
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", _cliente.Id);
                cmd.Parameters.AddWithValue("@Nome", _cliente.Nome);
                cmd.Parameters.AddWithValue("@Fone", _cliente.Fone);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("Ocrreu um erro ao tentar inserir o cliente no Banco de dados.");
            }
        }
        public void Alterar(Cliente _cliente)
        {
            SqlConnection cn = new SqlConnection(Constantes.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE CLIENTE(Id, Nome, Fone) 
                                 VALUES(@Id, @Nome, @Fone);
                                 WHERE Id = @Id";

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", _cliente.Id);
                cmd.Parameters.AddWithValue("@Nome", _cliente.Nome);
                cmd.Parameters.AddWithValue("@Fone", _cliente.Fone);

                cn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar inserir um cliente no banco de dados.", ex);
            }
            finally
            {
                cn.Close();
            }
        }
        public void Excluir(int _id)
        {
            SqlConnection cn = new SqlConnection(Constantes.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM CLIENTE                          
                                    WHERE Id = @Id";

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", _id);

                cn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar inserir um cliente no banco de dados.", ex);
            }
            finally
            {
                cn.Close();
            }
        }
        public List<Cliente> BuscarTodos()
        {
            List<Cliente> produtoLista = new List<Cliente>();
            Cliente cliente;
            SqlConnection cn = new SqlConnection(Constantes.StringDeConexao);

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
                        cliente = new Cliente();
                        cliente.Id = (int)rd["Id"];
                        cliente.Nome = rd["Nome"].ToString();
                        cliente.Fone = rd["Fone"].ToString();
                        produtoLista.Add(cliente);
                    }
                }

                return produtoLista;
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

        public Cliente BuscarPorId(int _Id)
        {

            Cliente cliente;
            SqlConnection cn = new SqlConnection(Constantes.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT Id, Nome, Fone FROM CLIENTE";
                cmd.CommandType = System.Data.CommandType.Text;

                cliente = new Cliente();
                cn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        cliente.Id = (int)rd["Id"];
                        cliente.Nome = rd["Nome"].ToString();
                        cliente.Fone = rd["Fone"].ToString();
                    }
                }

                return cliente;

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

        public List<Cliente> BuscarPorNome(string _nome)
        {
            List<Cliente> clienteLista = new List<Cliente>();
            Cliente cliente;
            SqlConnection cn = new SqlConnection(Constantes.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT Id, Nome, Fone
                                    FROM CLIENTE
                                    WHERE Nome LIKE @Nome";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue(@"Nome", "%" + _nome + "%");

                cn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        cliente = new Cliente();
                        cliente.Id = (int)rd["Id"];
                        cliente.Nome = rd["Nome"].ToString();
                        cliente.Fone = rd["Fone"].ToString();
                        clienteLista.Add(cliente);
                    }
                }

                return clienteLista;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar buscar um cliente por nome no banco de dados.", ex);
            }
            finally
            {
                cn.Close();
            }
        }

        public Cliente BuscarPorNomeUsuario(string _nomeUsuario)
        {
            Cliente cliente;

            SqlConnection cn = new SqlConnection(Constantes.StringDeConexao);

            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT  Id, Nome, Fone
                                    FROM CLIENTE 
                                    WHERE NomeUsuario = @NomeUsuario";
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@NomeUsuario", _nomeUsuario);


                cn.Open();

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    cliente = new Cliente();
                    while (rd.Read())
                    {
                        cliente.Id = (int)rd["Id"];
                        cliente.Nome = rd["Nome"].ToString();
                        cliente.Fone = rd["Fone"].ToString();
                    }
                }

                return cliente;
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
    }
}
