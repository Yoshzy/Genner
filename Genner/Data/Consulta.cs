using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Genner.Data
{
    internal class Consulta
    {
        double valor;
        int clientes;
        public double FaturamentoV()
        {
            Dbo db = new Dbo();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT SUM(valor) AS valortotal FROM services", db.GetMySqlConnection());

                db.conn.Open();

                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    valor = double.Parse(read["valortotal"].ToString());
                }
                db.conn.Close();
                return valor;
            }catch(Exception ex)
            {
                
                return 0;
                db.conn.Close();
            }
           
        }

        public int ClientesV()
        {
            Dbo db = new Dbo();
            try
            {
              
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) AS valortotal FROM clientes", db.GetMySqlConnection());

                db.conn.Open();

                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    clientes = int.Parse(read["valortotal"].ToString());
                }

                db.conn.Close();
                return clientes;
            }catch(Exception ex)
            {
                return 0;
                db.conn.Close();
            }
        }
    }
}
