using BLL;
using Models;

namespace UIWinFormsApp
{
    public partial class FormCadastrarCliente : Form
    {
        int id;
        public FormCadastrarCliente(int _id = 0)
        {
            InitializeComponent();
            id = _id;

            if (id == 0)
                BindingSourceCadastro.AddNew();
            else
                BindingSourceCadastro.DataSource = new ClienteBLL().BuscarPorId(id);
        }
        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                BindingSourceCadastro.EndEdit();
                Cliente cliente = (Cliente)BindingSourceCadastro.Current;
                if (id == 0)
                    new ClienteBLL().Inserir(cliente);

                else
                    new ClienteBLL().Alterar(cliente);
                MessageBox.Show("Registro salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
