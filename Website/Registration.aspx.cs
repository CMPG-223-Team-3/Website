using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;
using Website.App_Code;

namespace Website
{
    public partial class Registration : System.Web.UI.Page
    {
        private MySqlConnection conn;
        private string pageName = HttpContext.Current.Request.Url.AbsoluteUri; //Getting the pagename to store in session at page load so we can know which page to go back to after Error page is thrown

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["FromPage"] = pageName;
            DatabaseConnection connection = new DatabaseConnection();
            conn = connection.getConnection();
        }

        protected void btnRegister_Click(object sender, EventArgs e)//NOTE: Remember to check if the usernames and stuff already exists
        {
            if(isUser(txtUsername.Text) == true)
            {
                Response.Write("<script>alert('Username already exists')</script>");
            }
            else
            {
                Hash hs = new Hash(txtPassword.Text); //Hashing the thing to make more secure
                string passw = hs.getHash();

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "INSERT INTO Customer (`First Name`, `Last Name`, Password, Email, Username) VALUES(@nm, @lastnam ,@psw, @em, @unam)"
                };
                cmd.Parameters.AddWithValue("@nm", txtName.Text);
                cmd.Parameters.AddWithValue("@lastnam", txtSurname.Text);
                cmd.Parameters.AddWithValue("@psw", passw);
                cmd.Parameters.AddWithValue("@em", txtEmail.Text);
                cmd.Parameters.AddWithValue("@unam", txtUsername.Text);

                //open connection
                conn.Open();

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                conn.Close();

                Response.Write("Your registration is successfull");
                Response.Redirect("Login.aspx");
            }
        }

        protected bool isUser(string usName)
        {
            bool isUser = false;

            MySqlCommand comm = new MySqlCommand
            {
                Connection = conn,
                CommandText =
                "SELECT Username " +
                "FROM Customer " +
                "WHERE `Username` = @uname"
            };
            comm.Parameters.AddWithValue("@uname", usName);

            using (conn)
            {
                conn.Open();
                if(comm.ExecuteScalar() != null)
                {
                    isUser = true;
                }
            }
            return isUser;
        }
    }
}