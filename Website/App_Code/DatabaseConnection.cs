using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.App_Code
{
    //This class depends on the current .NET Framework's MySql.Data's ability to reuse the same connection after .dispose() is called
    public class DatabaseConnection
    {
        private MySqlConnection conn;
        private static string server;
        private static string database;
        private static string userName;
        private static string userPass;
        private string connectionString;
        private Exception x = new NotImplementedException();

        public DatabaseConnection()
        {
            server = "cmpg-223-db.ci6pbvbzz3x3.us-west-1.rds.amazonaws.com";
            database = "sql7368973";
            userName = "admin";
            userPass = "cmpg22310";
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