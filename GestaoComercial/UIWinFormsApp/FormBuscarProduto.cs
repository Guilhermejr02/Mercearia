using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIWinFormsApp
{
    public partial class FormBuscarProduto : Form
    {
        public FormBuscarProduto()
        {
            InitializeComponent();
        }

        private void FormBuscarProduto_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (comboBoxBuscarPor.SelectedIndex)
                {
                    case 0:
                        bindingSourceUsuario.DataSource = new ProdutoBLL().BuscarPorNome(textBoxBuscarPor.Text);
                        break;
                    case 1:
                        bindingSourceUsuario.DataSource = new ProdutoBLL().BuscarPorCodBarras(textBoxBuscarPor.Text);
                        break;
                    default:
                        bindingSourceUsuario.DataSource = new ProdutoBLL().BuscarTodos();
                        break;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
