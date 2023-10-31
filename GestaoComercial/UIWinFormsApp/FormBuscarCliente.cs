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
    public partial class FormBuscarCliente : Form
    {
        public FormBuscarCliente()
        {
            InitializeComponent();

        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (comboBoxBuscarPor.SelectedIndex)
                {
                    case 0:
                        clienteBindingSource.DataSource = new ClienteBLL().BuscarPorId(Convert.ToInt32(textBoxBuscarPor.Text));
                        break;
                    case 1:
                        clienteBindingSource.DataSource = new ClienteBLL().BuscarPorNome(textBoxBuscarPor.Text);
                        break;
                    case 2:
                        clienteBindingSource.DataSource = new ClienteBLL().BuscarPorFone(textBoxBuscarPor.Text);
                        break;
                    default:
                        clienteBindingSource.DataSource = new ClienteBLL().BuscarTodos();
                        break;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void FormBuscarCliente_Load(object sender, EventArgs e)
        {
            comboBoxBuscarPor.SelectedIndex = comboBoxBuscarPor.Items.Count - 1;
            buttonBuscar_Click(sender, e);
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            int id = ((Cliente)clienteBindingSource.Current).Id;
            using (FormCadastrarCliente frm = new FormCadastrarCliente(id))
            {
                frm.ShowDialog();
            }
        }

        private void buttonInserir_Click(object sender, EventArgs e)
        {
            using (FormCadastrarCliente frm = new FormCadastrarCliente())
            {
                frm.ShowDialog();
            }
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir este registr?", "Atenção!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = ((Cliente)clienteBindingSource.Current).Id;
                new ClienteBLL().Excluir(id);
                clienteBindingSource.RemoveCurrent();
                MessageBox.Show("Registro excluido com sucesso!");
            }
        }

    }
}
