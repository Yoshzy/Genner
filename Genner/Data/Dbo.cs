using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Genner.Data
{
    class Dbo
    {
        Config cf = new Config();
        public MySqlConnection conn;


        public Dbo()
        {
            Config cf = new Config();
            cf.LoadIni();

            string str = $"datasource={cf._Host};port={cf._Port};database={cf._Db};username={cf._User};password={cf._Pass};SslMode=none";

            conn = new MySqlConnection(str);

        }

        public MySqlConnection GetMySqlConnection()
        {
            return conn;
        }

    }
}
