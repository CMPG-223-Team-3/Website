using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website
{
    public class Connection
    {
        private MySqlConnection conn;
        private static string server;
        private static string database;
        private static string userName;
        private static string userPass;
        private string connectionString;
        private Exception x = new NotImplementedException();

        public Connection()
        {
            server = "sql7.freemysqlhosting.net";
            database = "sql7368973";
            userName = "sql7368973";
            userPass = "1lFxsKtjXr";
            connectionString = "Server=" + server + ";" + "Port=3306;" + "Database=" +
                database + ";" + " Uid=" + userName + ";" + "pwd=" + userPass + ";";
            conn = new MySqlConnection(connectionString);

            if(tryConnect() == false)
            {
                throw x;
            }
        }

        public bool tryConnect()
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                }
            }
            catch(Exception m)
            {
                x = m;
                return false;
            }
            return true;
        }

        public MySqlConnection getConnection()
        {
            return conn;
        }

    }
}