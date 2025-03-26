using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6469
{
    internal class Connection
    {
        String ConnectionStr = @"Data Source=DESKTOP-R7EGUO6;Initial Catalog=ProjectB;Integrated Security=True;";
        SqlConnection con;
        private static Connection _instance;
        public static Connection getInstance()
        {
            if (_instance == null)
                _instance = new Connection();
            return _instance;
        }
        private Connection()
        {
            con = new SqlConnection(ConnectionStr);
            //con.Open();
        }
        public SqlConnection getConnection()
        {
            return con;
        }
    }
}
