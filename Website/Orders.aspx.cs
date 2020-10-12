using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Login_Main
{
    public partial class Orders : System.Web.UI.Page
    {
        private MySqlConnection conn;
        private string server = "sql7.freemysqlhosting.net";
        private string database = "sql7368973";
        private string uid = "sql7368973";
        private string password = "1lFxsktjXr";
        string connectionString;


        private void initialize()
        {

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = "Server=" + server + ";" + "Port=3306;" + "Database=" +
            database + ";" + " Uid=" + uid + ";" + "pwd=" + password + ";";

            conn = new MySqlConnection(connectionString);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            conn.Open();

            //Delete orders that are delivered to table 1
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();



        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            conn.Open();

            //Delete orders that are delivered to table 2
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            conn.Open();

            //Delete orders that are delivered to table 3
            string query = "DELETE FROM table";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogOut.aspx");
        }
    }
}