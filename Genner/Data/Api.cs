using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Windows.Forms;

namespace Genner.Data
{
    internal class Api
    {
       
       
        public double Usd()
        {
                Config cf = new Config();
                cf.LoadIni();
                WebClient client = new WebClient();
                string urlApi = client.DownloadString($"https://api.hgbrasil.com/finance?key={cf._ApiKey}");
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(urlApi);
                double usdRes = obj["results"]["currencies"]["USD"]["buy"];

                return usdRes;
        
        }

        public double Btc()
        {
                Config cf = new Config();
                cf.LoadIni();
                WebClient client = new WebClient();
                string urlApi = client.DownloadString($"https://api.hgbrasil.com/finance?key={cf._ApiKey}");
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(urlApi);
                double btcRes = obj["results"]["currencies"]["BTC"]["buy"];
                return btcRes;
            
        }

        public double Eur()
        {
                Config cf = new Config();
                cf.LoadIni();
                WebClient client = new WebClient();
                string urlApi = client.DownloadString($"https://api.hgbrasil.com/finance?key={cf._ApiKey}");
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(urlApi);
                double eurRes = obj["results"]["currencies"]["EUR"]["buy"];
                return eurRes;
           
        }

        public int Clima()
        {
                Config cf = new Config();
                cf.LoadIni();
                WebClient client = new WebClient();
                string urlApi = client.DownloadString($"https://api.hgbrasil.com/weather?key={cf._ApiKey}&woeid={cf._ApiCity}");
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(urlApi);
                int climaCode = obj["results"]["condition_code"];

                return climaCode;
        
        }
        public int Termo()
        {
                Config cf = new Config();
                cf.LoadIni();
                WebClient client = new WebClient();
                string urlApi = client.DownloadString($"https://api.hgbrasil.com/weather?key={cf._ApiKey}&woeid={cf._ApiCity}");
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(urlApi);
                int termo = obj["results"]["temp"];

                return termo;
        }  

        public string Teste()
        {
            Config cf = new Config();
            cf.LoadIni();
            return $"https://api.hgbrasil.com/weather?key={cf._ApiKey}&woeid={cf._ApiCity}";
        }

    }
}
