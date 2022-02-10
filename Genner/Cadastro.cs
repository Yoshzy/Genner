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
using System.Threading;

namespace Genner
{
    public partial class Cadastro : Form
    {
        private Genner _genner;
        public Cadastro(Genner genner)
        {
            InitializeComponent();
            _genner = genner;
        }

        private void Cadastro_Load(object sender, EventArgs e)
        {
            
        }

        private void textBoxNome_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBoxNome_Enter(object sender, EventArgs e)
        {
            string fnome = textBoxNome.Text;
            if (fnome.ToLower().Trim().Equals("nome"))
            {
                textBoxNome.Text = "";
                textBoxNome.ForeColor = Color.Black;
            }
        }

        private void textBoxNome_Leave(object sender, EventArgs e)
        {
            string fnome = textBoxNome.Text;
            if(fnome.ToLower().Trim().Equals("nome") || fnome.Trim().Equals(""))
            {
                textBoxNome.Text = "nome";
                textBoxNome.ForeColor = Color.Gray;
            }
        }

        private void textBoxSobrenome_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEmail_Enter(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text;

            if (email.ToLower().Trim().Equals("email"))
            {
                textBoxEmail.Text = "";
                textBoxEmail.ForeColor = Color.Black;
            }
        }

        private void textBoxEmail_Leave(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text;
            if(email.ToLower().Trim().Equals("email") || email.Trim().Equals(""))
            {
                textBoxEmail.Text = "email";
                textBoxEmail.ForeColor = Color.Gray;
            }

        }

        private void textBoxPais_Enter(object sender, EventArgs e)
        {
            string pais = textBoxPais.Text;
            if (pais.ToLower().Trim().Equals("pais"))
            {
                textBoxPais.Text = "";
                textBoxPais.ForeColor = Color.Black;
            }
        }

        private void textBoxPais_Leave(object sender, EventArgs e)
        {
            string pais = textBoxPais.Text;
            if(pais.ToLower().Trim().Equals("pais") || pais.Trim().Equals(""))
            {
                textBoxPais.Text = "pais";
                textBoxPais.ForeColor = Color.Gray;
            }
        }

        private void textBoxSkype_Enter(object sender, EventArgs e)
        {
            string skype = textBoxSkype.Text;
            if (skype.ToLower().Trim().Equals("skype"))
            {
                textBoxSkype.Text = "";
                textBoxSkype.ForeColor = Color.Black;
            }
        }

        private void textBoxSkype_Leave(object sender, EventArgs e)
        {
            string skype = textBoxSkype.Text;
            if (skype.ToLower().Trim().Equals("skype") || skype.Trim().Equals(""))
            {
                textBoxSkype.Text = "skype";
                textBoxSkype.ForeColor = Color.Gray;
            }
        }

        private void textBoxDiscord_Enter(object sender, EventArgs e)
        {
            string discord = textBoxDiscord.Text;
            if (discord.ToLower().Trim().Equals("discord"))
            {
                textBoxDiscord.Text = "";
                textBoxDiscord.ForeColor = Color.Black;
            }
        }

        private void textBoxDiscord_Leave(object sender, EventArgs e)
        {
            string discord = textBoxDiscord.Text;
            if (discord.ToLower().Trim().Equals("discord") || discord.Trim().Equals(""))
            {
                textBoxDiscord.Text = "discord";
                textBoxDiscord.ForeColor = Color.Gray;
            }
        }

        private void textBoxKey_TextChanged(object sender, EventArgs e)
        {

        }
        //botao de cadastro.
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            
            Dbo db = new Dbo();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `clientes`(`nome`, `sobrenome`, `email`, `pais`, `skype`, `discord`, `chave`) VALUES (@fnome, @fsnome, @email, @pais, @skype, @discord, @chave)", db.GetMySqlConnection());

            cmd.Parameters.Add("@fnome", MySqlDbType.VarChar).Value = textBoxNome.Text;
            cmd.Parameters.Add("@fsnome", MySqlDbType.VarChar).Value = textBoxSobrenome.Text;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = textBoxEmail.Text;
            cmd.Parameters.Add("@pais", MySqlDbType.VarChar).Value = textBoxPais.Text;
            cmd.Parameters.Add("@skype", MySqlDbType.VarChar).Value = textBoxSkype.Text;
            cmd.Parameters.Add("@discord", MySqlDbType.VarChar).Value = textBoxDiscord.Text;
            cmd.Parameters.Add("@chave", MySqlDbType.VarChar).Value = textBoxKey.Text;

            db.conn.Open();

            if (!checkBoxes())
            {
                if (checkEmail())
                {
                    MessageBox.Show("Email já cadastrado!", "Email Duplicado", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    if(cmd.ExecuteNonQuery() == 1)
                    {
                        _genner.loadCounters();
                        MessageBox.Show("Cliente cadastrado com sucesso!", "Cliente Registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cleanBoxes();
                        
                    }
                    else
                    {
                        MessageBox.Show("ERRO");
                    }
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos!", "NO DATA", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            db.conn.Close();


        }
        //checa se os boxes estao sem dados.
        private bool checkBoxes()
        {
            string fnome = textBoxNome.Text;
            string fsNome = textBoxSobrenome.Text;
            string email = textBoxEmail.Text;
            string pais = textBoxPais.Text;
            string skype = textBoxSkype.Text;
            string discord = textBoxDiscord.Text;
            string chave = textBoxKey.Text;

            if (fnome.Equals("nome") || fsNome.Equals("sobrenome") || email.Equals("email") || pais.Equals("pais") || skype.Equals("skype") || discord.Equals("discord") || chave.Equals("chave") || fsNome.Equals("") || fnome.Equals("") || email.Equals("") || pais.Equals("") || skype.Equals("") || discord.Equals("") || chave.Equals("")) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //checa se ja existe um email cadastrado
        private bool checkEmail()
        {
            Dbo db = new Dbo();

            string email = textBoxEmail.Text;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `clientes` WHERE `email` = @email", db.GetMySqlConnection());

            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

            adapter.SelectCommand = cmd;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //limpa os boxes de cadastro.
        private void cleanBoxes()
        {
            textBoxNome.Clear();
            textBoxSobrenome.Clear();
            textBoxEmail.Clear();
            textBoxPais.Clear();
            textBoxSkype.Clear();
            textBoxDiscord.Clear();
            textBoxKey.Clear();
        }

        private void textBoxSobrenome_Enter(object sender, EventArgs e)
        {
            string fSNome = textBoxSobrenome.Text;
            if (fSNome.ToLower().Trim().Equals("sobrenome"))
            {
                textBoxSobrenome.Text = "";
                textBoxSobrenome.ForeColor = Color.Black;
            }
        }

        private void textBoxSobrenome_Leave(object sender, EventArgs e)
        {
            string fSNome = textBoxSobrenome.Text;
            if (fSNome.ToLower().Trim().Equals("sobrenome") || fSNome.Trim().Equals(""))
            {
                textBoxSobrenome.Text = "sobrenome";
                textBoxSobrenome.ForeColor = Color.Gray;
            }
        }

        private void textBoxKey_Leave(object sender, EventArgs e)
        {
            string chave = textBoxKey.Text;
            if (chave.ToLower().Trim().Equals("chave") || chave.Trim().Equals(""))
            {
                textBoxKey.Text = "chave";
                textBoxKey.ForeColor = Color.Gray;
            }
        }

        private void textBoxKey_Enter(object sender, EventArgs e)
        {
            string chave = textBoxKey.Text;
            if (chave.ToLower().Trim().Equals("chave"))
            {
                textBoxKey.Text = "";
                textBoxKey.ForeColor = Color.Black;
            }
        }
    }
}
