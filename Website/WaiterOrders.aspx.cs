using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


namespace Website
{
    public partial class WaiterOrders : System.Web.UI.Page
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

            //code to represent if the order is ready or not

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("WaiterLogOut.aspx");
        }

        protected void btnDelivered1_Click(object sender, EventArgs e)
        {
            conn.Open();

            //Delete orders that are delivered to table 1
            string query = "DELETE * FROM sql7368973.Staff WHERE Position ID = true";
            
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected void btnDelivered2_Click(object sender, EventArgs e)
        {
            conn.Open();

            //Delete orders that are delivered to table 2
            string query = "DELETE * FROM sql7368973.Staff WHERE Position ID = true";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected void btnDelivered3_Click(object sender, EventArgs e)
        {
            conn.Open();

            //Delete orders that are delivered to table 3
            string query = "DELETE * FROM sql7368973.Staff WHERE Position ID = true";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}