using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Genner.Data;
using MySql.Data.MySqlClient;

namespace Genner
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btEntrar_Click(object sender, EventArgs e)
        {
            string nome = nameBox.Text;
            string senha = passBox.Text;
            Dbo connect = new Dbo();

            DataTable tabela = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand comando = new MySqlCommand("SELECT * FROM `operadores` WHERE `usuario` = @usn and `senha` = @pass", connect.GetMySqlConnection());

            comando.Parameters.Add("@usn", MySqlDbType.VarChar).Value = nome;
            comando.Parameters.Add("@pass", MySqlDbType.VarChar).Value = senha;
            adapter.SelectCommand = comando;
            adapter.Fill(tabela);

            if (tabela.Rows.Count > 0)
            {
                this.Hide();
                Genner mainform = new Genner();
                mainform.Show();
                connect.conn.Close();

            }
            else
            {

                if (nome.Trim().Equals(""))
                {
                    MessageBox.Show("Insira o nome de usuario!", "Usuario em Branco", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                else if (senha.Trim().Equals(""))
                {
                    MessageBox.Show("Insira sua senha!", "Senha em Branco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                else
                {
                    MessageBox.Show("Usuario ou senha incorretos!", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
