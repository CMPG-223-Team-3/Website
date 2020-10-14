using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Login_Main
{
    public partial class Login : System.Web.UI.Page
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

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //#1 Make the connection String.
            //#2 run the username and password to check if it is registered correct.

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[]);
            conn.Open();
            string checkUser = "select count(*) from UserData where UserName='" + txtLogName.Text + "'";
            SqlCommand com = new SqlCommand(checkUser, conn);
            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            conn.Close();
            if (temp == 1)
            {
                conn.Open();
                string checkPassword = "select Password from UserData where UserName='" + txtLogName.Text + "'";
                SqlCommand passcom = new SqlCommand(checkPassword, conn);
                string password = passcom.ExecuteScalar().ToString().Replace(" ", "");

                if (password == txtLogPassword.Text)
                {
                    Session["New"] = txtLogName.Text;
                    Response.Write("Password is correct");
                    //Redirect to the next page.
                    Response.Redirect("Orders.aspx");
                }
                else
                {
                    Response.Write("Password is NOT correct");
                }
            }
            else
            {
                Response.Write("Username is NOT correct");
            }
        }

        protected void btnLRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Resistration.aspx");
        }
    }
}