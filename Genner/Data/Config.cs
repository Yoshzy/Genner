using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Genner.Data;

namespace Genner.Data
{
    internal class Config
    {
        public string _ApiKey { get; set; }
        public string _ApiCity { get; set; }
        public string  _Host { get; set; }
        public string _Port { get; set; }
        public string _User { get; set; }
        public string _Pass { get; set; }
        public string _Db { get; set; }
        public string _Servidor { get; set; }
        public string _Porta { get; set; }
        public string _Remetente { get; set; }
        public string _Email { get; set; }
        public string _Senha { get; set; }




        public void LoadIni()
        {
            try
            {
                IniFile iniConfig = new IniFile(Environment.CurrentDirectory + "\\Config\\Config.ini");
                _ApiKey = iniConfig.Read("ApiKey", "Config");
                _ApiCity = iniConfig.Read("ApiCity", "Config");
                _Host = iniConfig.Read("Host", "Data");
                _Port = iniConfig.Read("Port", "Data");
                _User = iniConfig.Read("User", "Data");
                _Pass = iniConfig.Read("Pass", "Data");
                _Db = iniConfig.Read("Db", "Data");
                _Servidor = iniConfig.Read("Servidor", "Email");
                _Porta = iniConfig.Read("Porta", "Email");
                _Remetente = iniConfig.Read("Remetente", "Email");
                _Email = iniConfig.Read("Email", "Email");
                _Senha = iniConfig.Read("Senha", "Email");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    
}