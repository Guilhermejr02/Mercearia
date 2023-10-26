namespace DAL
{
    internal class Conexao
    {
        private static string stringDeConexao = "User ID=SA;Initial Catalog=MERCEARIA; Data Source =.\\SQLEXPRESS2019;Password=Senailab02";
        public static string StringDeConexao
        {
            get
            {
                return stringDeConexao;
            }
        }
    }
}
