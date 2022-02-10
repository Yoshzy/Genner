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
namespace Genner
{
    public partial class Opcoes : Form
    {
        private Genner _genner;
        public Opcoes(Genner genner)
        {
            InitializeComponent();
            _genner = genner;
        }

        private void Opcoes_Load(object sender, EventArgs e)
        {

            Config cf = new Config();
            cf.LoadIni();
            textBoxApi.Text = cf._ApiKey;
            textBoxCodCi.Text = cf._ApiCity;
            textBoxSmtp.Text = cf._Servidor;
            textBoxPorta.Text = cf._Porta;
            textBoxEmail.Text = cf._Email;
            textBoxSenha.Text = cf._Senha;
            textBoxRemetente.Text = cf._Remetente;


        }

        private void textBoxApi_Enter(object sender, EventArgs e)
        {

        }

        private void textBoxApi_Leave(object sender, EventArgs e)
        {
            string apikey = textBoxApi.Text;
            if (apikey.ToLower().Trim().Equals("chave-api") || apikey.Trim().Equals(""))
            {
                textBoxApi.Text = "chave-api";
                textBoxApi.ForeColor = Color.Gray;
            }
        }

        private void textBoxCodCi_Enter(object sender, EventArgs e)
        {
            string codCidade = textBoxCodCi.Text;
            if (codCidade.ToLower().Trim().Equals("cod-cidade"))
            {
                textBoxCodCi.Text = "";
                textBoxCodCi.ForeColor = Color.Black;
            }
        }

        private void textBoxCodCi_Leave(object sender, EventArgs e)
        {
            string codCidade = textBoxCodCi.Text;
            if (codCidade.ToLower().Trim().Equals("cod-cidade") || codCidade.Trim().Equals(""))
            {
                textBoxCodCi.Text = "cod-cidade";
                textBoxCodCi.ForeColor = Color.Gray;
            }
        }

        private void textBoxSmtp_Enter(object sender, EventArgs e)
        {
            string smtp = textBoxSmtp.Text;
            if (smtp.ToLower().Trim().Equals("smtp"))
            {
                textBoxSmtp.Text = "";
                textBoxSmtp.ForeColor = Color.Black;
            }
        }

        private void textBoxSmtp_Leave(object sender, EventArgs e)
        {
            string smtp = textBoxSmtp.Text;
            if (smtp.ToLower().Trim().Equals("smtp") || smtp.Trim().Equals(""))
            {
                textBoxSmtp.Text = "smtp";
                textBoxSmtp.ForeColor = Color.Gray;
            }
        }

        private void textBoxPorta_Enter(object sender, EventArgs e)
        {
            string porta = textBoxPorta.Text;
            if (porta.ToLower().Trim().Equals("porta"))
            {
                textBoxPorta.Text = "";
                textBoxPorta.ForeColor = Color.Black;
            }
        }

        private void textBoxPorta_Leave(object sender, EventArgs e)
        {
            string porta = textBoxPorta.Text;
            if (porta.ToLower().Trim().Equals("porta") || porta.Trim().Equals(""))
            {
                textBoxPorta.Text = "porta";
                textBoxPorta.ForeColor = Color.Gray;
            }
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
            if (email.ToLower().Trim().Equals("email") || email.Trim().Equals(""))
            {
                textBoxEmail.Text = "email";
                textBoxEmail.ForeColor = Color.Gray;
            }
        }

        private void textBoxSenha_Enter(object sender, EventArgs e)
        {
            string senha = textBoxSenha.Text;
            if (senha.ToLower().Trim().Equals("senha"))
            {
                textBoxSenha.Text = "";
                textBoxSenha.ForeColor = Color.Black;
            }
        }

        private void textBoxSenha_Leave(object sender, EventArgs e)
        {
            string senha = textBoxSenha.Text;
            if (senha.ToLower().Trim().Equals("senha") || senha.Trim().Equals(""))
            {
                textBoxSenha.Text = "senha";
                textBoxSenha.ForeColor = Color.Gray;
            }
        }

        private void textBoxRemetente_Enter(object sender, EventArgs e)
        {
            string remetente = textBoxRemetente.Text;
            if (remetente.ToLower().Trim().Equals("remetente"))
            {
                textBoxRemetente.Text = "";
                textBoxRemetente.ForeColor = Color.Black;
            }
        }

        private void textBoxRemetente_Leave(object sender, EventArgs e)
        {
            string remetente = textBoxRemetente.Text;
            if (remetente.ToLower().Trim().Equals("remetente") || remetente.Trim().Equals(""))
            {
                textBoxRemetente.Text = "remetente";
                textBoxRemetente.ForeColor = Color.Gray;
            }
        }

        private bool checkBoxes()
        {
            string apiKey = textBoxApi.Text;
            string cityCode = textBoxCodCi.Text;
            string smtp = textBoxSmtp.Text;
            string porta = textBoxPorta.Text;
            string email = textBoxEmail.Text;
            string senha = textBoxSenha.Text;
            string remetente = textBoxRemetente.Text;

            if (apiKey.Equals("chave-api") || apiKey.Equals("") || cityCode.Equals("cod-cidade") || cityCode.Equals("") || smtp.Equals("smtp") || smtp.Equals("") || porta.Equals("porta") || porta.Equals("") || email.Equals("email") || email.Equals("") || senha.Equals("senha") || senha.Equals("") || remetente.Equals("remetente") || remetente.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!checkBoxes())
            {
                IniFile iniConfig = new IniFile(Environment.CurrentDirectory + @"\\Config\\Config.ini");
                iniConfig.Write("ApiKey", textBoxApi.Text, "Config");
                iniConfig.Write("ApiCity", textBoxCodCi.Text, "Config");
                iniConfig.Write("Servidor", textBoxSmtp.Text, "Email");
                iniConfig.Write("Porta", textBoxPorta.Text, "Email");
                iniConfig.Write("Remetente", textBoxRemetente.Text, "Email");
                iniConfig.Write("Email", textBoxEmail.Text, "Email");
                iniConfig.Write("Senha", textBoxSenha.Text, "Email");
                MessageBox.Show("Salvo com sucesso!", "Opções Salvas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Preencha todos os campos!", "NO DATA", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }
    }
}
