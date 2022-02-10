using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Genner.Data;

namespace Genner
{
    public partial class Servicos : Form
    {
        private Genner _genner;
        public Servicos(Genner genner)
        {
            InitializeComponent();
            _genner = genner;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Servicos_Load(object sender, EventArgs e)
        {
            this.ActiveControl = labelNome;
        }

        private void textBoxNome_Enter(object sender, EventArgs e)
        {
            string nome = textBoxNome.Text;
            if (nome.ToLower().Trim().Equals("nome"))
            {
                textBoxNome.Text = "";
                textBoxNome.ForeColor = Color.Black;
            }
        }

        private void textBoxNome_Leave(object sender, EventArgs e)
        {
            string nome = textBoxNome.Text;
            if(nome.ToLower().Trim().Equals("nome") || nome.Trim().Equals(""))
            {
                textBoxNome.Text = "nome";
                textBoxNome.ForeColor = Color.Gray;
            }
        }

        private void textBoxProduto_Enter(object sender, EventArgs e)
        {
            string produto = textBoxProduto.Text;
            if (produto.ToLower().Trim().Equals("produto"))
            {
                textBoxProduto.Text = "";
                textBoxProduto.ForeColor = Color.Black;
            }
        }

        private void textBoxProduto_Leave(object sender, EventArgs e)
        {
            string produto = textBoxProduto.Text;
            if (produto.ToLower().Trim().Equals("produto") || produto.Trim().Equals(""))
            {
                textBoxProduto.Text = "produto";
                textBoxProduto.ForeColor = Color.Gray;
            }
        }

        private void textBoxInfo_Enter(object sender, EventArgs e)
        {
            string info = textBoxInfo.Text;
            if (info.ToLower().Trim().Equals("info"))
            {
                textBoxInfo.Text = "";
                textBoxInfo.ForeColor = Color.Black;
            }
        }

        private void textBoxInfo_Leave(object sender, EventArgs e)
        {
            string info = textBoxInfo.Text;
            if(info.ToLower().Trim().Equals("info") || info.Trim().Equals(""))
            {
                textBoxInfo.Text = "info";
                textBoxInfo.ForeColor = Color.Gray;
            }
        }

        private void textBoxValor_Enter(object sender, EventArgs e)
        {
            string valor = textBoxValor.Text;
            if (valor.ToLower().Trim().Equals("valor"))
            {
                textBoxValor.Text = "";
                textBoxValor.ForeColor = Color.Black;
            }
        }

        private void textBoxValor_Leave(object sender, EventArgs e)
        {
            string valor = textBoxValor.Text;
            if(valor.ToLower().Trim().Equals("valor") || valor.Trim().Equals(""))
            {
                textBoxValor.Text = "valor";
                textBoxValor.ForeColor = Color.Gray;
            }
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            Dbo db = new Dbo();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `services`(`cliente_nome`, `produto`, `info`, `valor`) VALUES (@nome, @produto, @info, @valor)", db.GetMySqlConnection());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = textBoxNome.Text;
            cmd.Parameters.Add("@produto", MySqlDbType.VarChar).Value = textBoxProduto.Text;
            cmd.Parameters.Add("@info", MySqlDbType.VarChar).Value = textBoxInfo.Text;
            cmd.Parameters.Add("@valor", MySqlDbType.VarChar).Value = textBoxValor.Text;

            db.conn.Open();

            if (!CheckBoxes())
            {
               
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        _genner.loadCounters();
                        _genner.loadList();
                        MessageBox.Show("Ordem de serviço adicionada!", "Serviço Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CleanBoxes();
                    
                    }
                    else
                    {
                        MessageBox.Show("ERRO");
                    }
           
            }
            else
            {
                MessageBox.Show("Preencha todos os campos!", "NO DATA", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            db.conn.Close();

        }

        private bool CheckBoxes()
        {
            string nome = textBoxNome.Text;
            string produto = textBoxProduto.Text;
            string info = textBoxInfo.Text;
            string valor = textBoxValor.Text;

            if (nome.Equals("Nome") || nome.Equals("") || produto.Equals("Produto") || produto.Equals("") || info.Equals("Info") || info.Equals("") || valor.Equals("Valor") || valor.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CleanBoxes()
        {
            textBoxNome.Clear();
            textBoxProduto.Clear();
            textBoxInfo.Clear();
            textBoxValor.Clear();
        }
    }
}
