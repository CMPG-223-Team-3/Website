using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Website.App_Code;

//NOTE: Check username vs user name

namespace Website
{
    public partial class Login : System.Web.UI.Page
    {
        //session var names
        private static string userNameSession = "UserName";
        private static string errorSession = "Error";
        private static string fromPageSession = "FromPage";
        private static string orderIDSession = "OrderID";
        private static string tableIDSession = "TableID";



        private MySqlConnection conn;
        private string pageName = HttpContext.Current.Request.Url.AbsoluteUri; //Getting the pagename to store in session at page load so we can know which page to go back to after Error page is thrown

        protected void Page_Load(object sender, EventArgs e)
        {
            Session[fromPageSession] = pageName;
            try
            {
                ConnectionClass connection = new ConnectionClass(); //New connection object (See Connection.cs)
                conn = connection.getConnection();
            }
            catch(Exception x)
            {//Note how we're doing the error handling on the site: put Exception message into error session and redirect to Error.aspx
                Session[errorSession] = x.Message;
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if(isCustomer(txtLogUserName.Text, txtLogPassword.Text) == true)
            {//Remember that the isStaff and isCustomer already sets the sessions for the respected whatevers
                if (Session[orderIDSession] == null)
                {
                    int j = getLastOrder(int.Parse(Session[tableIDSession].ToString()));
                    if (j > -1)
                    {
                        Session[orderIDSession] = j;
                    }
                }
                Response.Redirect("CustomerOrder.aspx");
            }
            else if(isWaiter(txtLogUserName.Text, txtLogPassword.Text) == true)
            {
                Response.Redirect("Orders.aspx");
            }
            else
            {
                //This is what happens when user not found
                Response.Write("<script>alert('Username or Password is incorrect')</script>");
            }
        }

        protected void btnLRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

        protected bool isWaiter(string usName, string psWord)
        {//This one is open to problems like 2 people having the same name and password
            bool isStaff = false;

            Hash hPass = new Hash(psWord); //Making new Hash object to hash the entered password to compare to what is in the database
            string enteredPassword = hPass.getHash(); //Getting the hash object's hash

            MySqlCommand comm = new MySqlCommand
            {
                Connection = conn,
                CommandText =
                "SELECT * " +
                "FROM WAITER " +
                "WHERE `Waiter_FirstName`= @fname " +
                "AND `Password`= @pasw"
            };
            comm.Parameters.AddWithValue("@fname", usName);
            comm.Parameters.AddWithValue("@pasw", enteredPassword);

            using (conn)
            {
                conn.Open();
                using(MySqlDataReader rder = comm.ExecuteReader())
                {
                    if(rder.HasRows) //If staff exist
                    {
                        isStaff = true;
                        while(rder.Read())
                        {
                            Session["Staff"] = true;
                            Session["UserName"] = rder["First name"];
                            Session["LastName"] = rder["Last name"];
                        }
                    }
                }
            }
            return isStaff;
        }

        protected bool isCustomer(string usName, string psWord)
        {
            bool isUser = false;

            Hash hPass = new Hash(psWord);
            string enteredPassword = hPass.getHash();

            MySqlCommand comm = new MySqlCommand
            {
                Connection = conn,
                CommandText =
                "SELECT * " +
                "FROM Customer " +
                "WHERE `Username` = @uname " +
                "AND `Password` = @pasw"
            };
            comm.Parameters.AddWithValue("@uname", usName);
            comm.Parameters.AddWithValue("@pasw", enteredPassword);

            using (conn)
            {
                conn.Open();
                using (MySqlDataReader rder = comm.ExecuteReader())
                {
                    if (rder.HasRows)
                    {
                        isUser = true;
                        while (rder.Read())
                        {
                            Session["Staff"] = false;
                            Session["UserName"] = rder["UserName"];
                            Session["FirstName"] = rder["First name"];
                            Session["LastName"] = rder["Last Name"];
                            Session["CustomerID"] = rder["Customer ID"];
                        }
                    }
                }
            }
            return isUser;
        }

        public int getLastOrder(int customerID)
        {
            int id = -1;
            MySqlCommand comm = new MySqlCommand
            {
                Connection = conn,
                CommandText =
                "SELECT `Order ID` " +
                "FROM `Order` " +
                "WHERE `Customer ID` = " + customerID + " " +
                "AND Paid = " + 0 + " " +
                "AND Status = " + 0 + " " 
            };

            using (conn)
            {
                conn.Open();
                using (MySqlDataReader datr = comm.ExecuteReader())
                {
                    if (datr.HasRows)
                    {
                        while(datr.Read())
                        {
                            id = int.Parse(datr["Order ID"].ToString());
                        }
                    }
                }
            }
            return id;
        }
    }
}