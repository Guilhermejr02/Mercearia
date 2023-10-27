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
    public partial class FormBuscarUsuario : Form
    {
        public FormBuscarUsuario()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (comboBoxBuscarPor.SelectedIndex)
                {
                    case 0:
                        bindingSourceUsuario.DataSource = new UsuarioBLL().BuscarPorNome(textBoxBuscarPor.Text);
                        break;
                    case 1:
                        bindingSourceUsuario.DataSource = new UsuarioBLL().BuscarPorNomeUsuario(textBoxBuscarPor.Text);
                        break;
                    default:
                        bindingSourceUsuario.DataSource = new UsuarioBLL().BuscarTodos();
                        break;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonInserir_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((Usuario)bindingSourceUsuario.Current));
            using (FormCadastrarUsuario frm = new FormCadastrarUsuario(id))
            {
                frm.ShowDialog();
            }
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir este registr?", "Atenção!", MessageBoxButtons.YesNo) == DialogResult.No)
            {

            }
            int id = Convert.ToInt32(((Usuario)bindingSourceUsuario.Current));
            new UsuarioBLL().Excluir(id);
            bindingSourceUsuario.RemoveCurrent();
            MessageBox.Show("Registro excluido com sucesso!");
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((Usuario)bindingSourceUsuario.Current));
            using (FormCadastrarUsuario frm = new FormCadastrarUsuario(id))
            {
                frm.ShowDialog();
            }
        }
    }
}
