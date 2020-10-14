using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Login_Main
{
    public partial class Registration : System.Web.UI.Page
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


        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO tableinfo (name, password, email) VALUES()";

            //open connection
            conn.Open();
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, conn);

            //Execute command
            cmd.ExecuteNonQuery();

            //close connection
            conn.Close();

            Response.Write("Your registration is successfull");
            Response.Redirect("Login.aspx");
        }
    }
}