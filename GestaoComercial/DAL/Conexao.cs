namespace DAL
{
    internal class Conexao
    {
        private static string stringDeConexao = "Data Source =DESKTOP-2S72IHV\\SQLEXPRESS02;integrated security=SSPI; Initial Catalog=MERCEARIA;";
        public static string StringDeConexao
        {
            get
            {
                return stringDeConexao;
            }
        }
    }
}
