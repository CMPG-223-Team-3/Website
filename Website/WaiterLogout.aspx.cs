using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Website
{
    public partial class WaiterLogout : System.Web.UI.Page
    { 
        private MySqlConnection conn;
        private string server = "sql7.freemysqlhosting.net";
        private string database = "sql7368973";
        private string uid = "sql7368973";
        private string password = "1lFxsktjXr";
        string connectionString;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = "Server=" + server + ";" + "Port=3306;" + "Database=" +
            database + ";" + " Uid=" + uid + ";" + "pwd=" + password + ";";

            conn = new MySqlConnection(connectionString);
        }

        protected void btnFinalLogout_Click(object sender, EventArgs e)
        {
            //add logoff time to database

            string query = "INSERT INTO work log Values('"+txtBeginS+"')";

            string query2 = "INSERT INTO work log Values('" +txtEndS+"')";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlCommand cmd2 = new MySqlCommand(query2, conn);
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            conn.Close();

            Response.Write("you have successfully loged out");
        }
    }
}