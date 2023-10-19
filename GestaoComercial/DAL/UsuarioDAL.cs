using Models;
using System.Data.SqlClient;

namespace DAL
{
    internal class UsuarioDAL
    {
        public void Inserir(Usuario _usuario)
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                SqlCommand cmd = cn.CreateCommand();


            }
            catch (Exception ex)
            {

                throw new Exception("Ocrreu um erro ao tentar inserir o usuario no Banco de dados.");
            }
        }
        public void Alterar(Usuario _usuario)
        {

        }
        public void Excluir(int _id)
        {

        }
        public List<Usuario> BuscarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
