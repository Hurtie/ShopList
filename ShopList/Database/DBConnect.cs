using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopList.Database
{
    public class DBConnection
    {
        MySqlConnection conn;
        MySqlDataReader? reader;

        public DBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "shopping_lists";
            string username = "root";
            string password = "";

            string connStr = $"server={host};user={username};database={database};password={password};CharSet=utf8;";

            conn = new MySqlConnection(connStr);
        }

        public MySqlDataReader data(string sqlQuery)
        {
            conn.Open();
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);

            reader = command.ExecuteReader();
            return reader;
        }
        public void close()
        {
            reader.Close();
            conn.Close();
        }
    }
}
