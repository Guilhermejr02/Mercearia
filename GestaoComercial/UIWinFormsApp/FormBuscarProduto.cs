using BLL;
using Models;
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
            comboBoxBuscarPor.SelectedIndex = comboBoxBuscarPor.Items.Count - 1;
            buttonBuscar_Click(sender, e);
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
                        produtoBindingSource.DataSource = new ProdutoBLL().BuscarPorNome(textBoxBuscarPor.Text);
                        break;
                    case 1:
                        produtoBindingSource.DataSource = new ProdutoBLL().BuscarPorCodBarras(textBoxBuscarPor.Text);
                        break;
                    default:
                        produtoBindingSource.DataSource = new ProdutoBLL().BuscarTodos();
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

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            int id = ((Produto)produtoBindingSource.Current).Id;
            using (FormCadastrarProduto frm = new FormCadastrarProduto(id))
            {
                frm.ShowDialog();
            }
        }

        private void buttonInserir_Click(object sender, EventArgs e)
        {
            using (FormCadastrarProduto frm = new FormCadastrarProduto())
            {
                frm.ShowDialog();
            }
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir este registr?", "Atenção!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = ((Produto)produtoBindingSource.Current).Id;
                new ProdutoBLL().Excluir(id);
                produtoBindingSource.RemoveCurrent();
                MessageBox.Show("Registro excluido com sucesso!");
            }
        }

        private void produtoBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
