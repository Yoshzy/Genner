using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Genner.Data;
using System.Net;
using System.Drawing.Imaging;
using System.IO;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Net.Mail;
using System.Web;
using MySql.Data.MySqlClient;


namespace Genner
{
    public partial class Genner : Form
    {
        double tarifa = 0.60;
       
        public Genner()
        {

            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Thread td = new Thread(new ThreadStart(LoadApis));
            td.Start();
            loadEmails();
            loadList();
            
            

        }

        private void Genner_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Environment.Exit(0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            hourtext.Text = DateTime.Now.ToString("HH:mm");
            datatext.Text = DateTime.Now.ToShortDateString();

        }


        //converte Dollar para real ja descontando valor da taxa do paypal.
        private void RealUsd()
        {

            string refatorador = valueBoxP.Text.ToLower();
            char[] re = { '.', ',', '-', '=', '/', '*', '&', '^', '%', '$', '#', '@', '!', '{', '}', '[', ']', '+', '<', '>', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            if (refatorador.Trim(re).Equals(""))
            {
                MessageBox.Show("Insira um valor!", "Valor em Branco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valueBoxP.Focus();
            }
            else
            {

                Api cnn = new Api();

                double vBox = double.Parse(valueBoxP.Text);
                if (vBox == 0)
                {
                    resultadoPay.Text = "0";
                }
                else
                {

                    double calculo = cnn.Usd() * vBox;
                    double resultado = (calculo - (calculo / 100 * 6.40)) - tarifa;

                    resultadoPay.Text = resultado.ToString("C");
                }
            }
        }
        //converte euro para real ja descontando o valor da taxa do paypal.
        private void RealEur()
        {
            string refatorador = valueBoxP.Text.ToLower();
            char[] re = { '.', ',', '-', '=', '/', '*', '&', '^', '%', '$', '#', '@', '!', '{', '}', '[', ']', '+', '<', '>', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            if (refatorador.Trim(re).Equals(""))
            {
                MessageBox.Show("Insira um valor!", "Valor em Branco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valueBoxP.Focus();
            }
            else
            {
                Api cnn = new Api();

                double vBox = double.Parse(valueBoxP.Text);
                if (vBox == 0)
                {
                    valueBoxP.Text = "0";
                }
                else
                {
                    double calculo = cnn.Eur() * vBox;
                    double resultado = (calculo - (calculo / 100 * 6.40)) - tarifa;

                    resultadoPay.Text = resultado.ToString("C");
                }
            }
        }
        //botao para chamar o metodo de conversao do dolar.
        private void BtnDollar_Click(object sender, EventArgs e)
        {
            RealUsd();
        }
        //botao para chamar o metodo de conversao do euro.
        private void button2_Click(object sender, EventArgs e)
        {
            RealEur();
        }
        //metodo que alimenta o switch de clima.
        private void CClima(string path)
        {
            if (File.Exists(path))
            {
                boxTemp.Image?.Dispose();
                boxTemp.Image = Image.FromFile(path, true);
            }
        }
        //metodo que recarrega os valores das apis a cada 10 minutos.
        private void LoadApis()
        {
            try
            {
                while (true)
                {
                   
                    Api cn = new Api();
                    Consulta cs = new Consulta();

                    #region contadores
                    
                        faturamento.Text = cs.FaturamentoV().ToString("C");
                        totalClientes.Text = cs.ClientesV().ToString() + " Clientes";
                     

                     #endregion

                    #region BTC
                    btctext.Text = cn.Btc().ToString("C");
                    btctext.Update();
                    btctext.Refresh();
                    #endregion

                    #region Dollar

                    textDollar.Text = cn.Usd().ToString("C");
                    textDollar.Update();
                    textDollar.Refresh();
                    #endregion

                    #region Euro
                    euroText.Text = cn.Eur().ToString("C");
                    euroText.Update();
                    euroText.Refresh();
                    #endregion

                    #region clima
                    switch (cn.Clima())
                    {
                        case 27:
                        case 31:
                            CClima(Environment.CurrentDirectory + @"\res\sol.png");
                            break;
                        case 26:
                        case 28:
                            CClima(Environment.CurrentDirectory + @"\res\nublado.png");
                            break;
                        case 29:
                        case 30:
                            CClima(Environment.CurrentDirectory + @"\res\parcial.png");
                            break;
                        case 37:
                        case 47:
                            CClima(Environment.CurrentDirectory + @"\res\tempestades.png");
                            break;
                        case 45:
                        case 40:
                            CClima(Environment.CurrentDirectory + @"\res\chuva.png");
                            break;
                        default:
                            CClima(Environment.CurrentDirectory + @"\res\default.png");
                            break;
                    }
                    boxTemp.Update();
                    boxTemp.Refresh();

                    #endregion


                    #region termostato
                    termoText.Text = cn.Termo().ToString() + "°";
                    termoText.Update();
                    termoText.Refresh();
                    #endregion

                    #region Clima Desc
                    switch (cn.Clima())
                    {
                        case 27:
                        case 31:
                            textDescLb.Text = "Limpo";
                            break;
                        case 26:
                        case 28:
                            textDescLb.Text = "Nublado";
                            break;
                        case 29:
                        case 30:
                            textDescLb.Text = "Nublado Parcialmente";
                            break;
                        case 37:
                        case 47:
                            textDescLb.Text = "Tempestade";
                            break;
                        case 45:
                        case 40:
                            textDescLb.Text = "Chuva";
                            break;
                        default:
                            textDescLb.Text = "Error Rain";
                            break;
                    }

                    #endregion

                    Thread.Sleep(TimeSpan.FromMinutes(10));
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }
        //metodo que chama formulario de login.
        private void btnOperator_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.ShowDialog();
            
        }
        //metodo que retorna os emails no comboBox
        private void loadEmails()
        {
            Dbo db = new Dbo();
            try
            {

                
                MySqlCommand cmd = new MySqlCommand("SELECT * from clientes", db.GetMySqlConnection());

                db.conn.Open();
                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    comboUsers.Items.Add(rd["email"].ToString());
                }

                db.conn.Close();
                }catch(Exception ex)
            {
                comboUsers.Text = "";
                db.conn.Close();
            }
        }
        //metodo que envia emails para clientes.
        private void btSend_Click(object sender, EventArgs e)
        {
            Config cf = new Config();
            cf.LoadIni();
            MailAddress from = new MailAddress(senderBox.Text, cf._Remetente);
            MailMessage mail = new MailMessage(from.ToString(), comboUsers.Text, subjectBox.Text, bodyBox.Text);
            SmtpClient client = new SmtpClient(cf._Servidor, int.Parse(cf._Porta));
            mail.IsBodyHtml = true;
            client.EnableSsl = true;
            #region conexao smt , password...
            NetworkCredential cred = new NetworkCredential(cf._Email, cf._Senha);
            #endregion
            client.Credentials = cred;
            client.Send(mail);
            MessageBox.Show("Email enviado com sucesso!", "Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimparEmail();

        }
        //metodo que limpa as textbox do form de email.
        private void LimparEmail()
        {
            senderBox.Clear();
            subjectBox.Clear();
            bodyBox.Clear();
        }
        //metodo que chama o formulario de cadastro de clientes
        private void btnAddC_Click(object sender, EventArgs e)
        {
            Cadastro cd = new Cadastro(this);
            cd.ShowDialog();
        }
        //metodo que chama formulario de servicos , para adicionar servicos.
        private void button1_Click(object sender, EventArgs e)
        {
            Servicos sv = new Servicos(this);
            sv.ShowDialog();
        }
        //metodo que cria grid de dados
        public void loadList()
        {
            Dbo db = new Dbo();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            db.conn.Open();
            adapter.SelectCommand = new MySqlCommand("SELECT * FROM services", db.GetMySqlConnection());
            DataTable table = new DataTable();
            adapter.Fill(table);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = table;

            dataGrd.DataSource = bsource;
            if(dataGrd.Rows.Count > 0)
            {
                dataGrd.CurrentCell = dataGrd[0, dataGrd.Rows.Count - 1];
            }
            dataGrd.Columns[0].Width = 100;
            dataGrd.Columns[1].Width = 100;
            dataGrd.Columns[2].Width = 114;
            dataGrd.Columns[3].Width = 160;
            dataGrd.Columns[4].Width = 214;
            db.conn.Close();
            dataGrd.Refresh();
            dataGrd.Update();

        }

        //carrega os valores retorna numero de clientes e faturamento
        public void loadCountersV()
        {
            Consulta cs = new Consulta();
                faturamento.Text = cs.FaturamentoV().ToString("C");
                faturamento.Update();
                totalClientes.Text = cs.ClientesV().ToString() + " Clientes";
                totalClientes.Update();
            
        }
        //recarrega os contadores ao executar a criacao de contas
        public void loadCounters()
        {
            try
            {

                    
                    comboUsers.Items.Clear();
                    comboUsers.Update();
                    comboUsers.Refresh();
                    loadEmails();
                    loadCountersV();
                   
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //metodo que exclui um servico da grid
        private void button3_Click(object sender, EventArgs e)
        {
            Dbo db = new Dbo();
            try
            {

                if (MessageBox.Show("Voce deseja mesmo excluir?", "OPS", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRow row = (dataGrd.SelectedRows[0].DataBoundItem as DataRowView).Row;

                    db.conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM `services` WHERE ID = " + row["id"], db.GetMySqlConnection());
                    cmd.ExecuteNonQuery();
                    db.conn.Close();
                    loadCounters();
                    loadList();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Opcoes op = new Opcoes(this);
            op.ShowDialog();

        }
    }
}