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

//NOTE: Check username vs user name

namespace Website
{
    public partial class Login : System.Web.UI.Page
    {
        private MySqlConnection conn;
        private string pageName = HttpContext.Current.Request.Url.AbsoluteUri; //Getting the pagename to store in session at page load so we can know which page to go back to after Error page is thrown

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["FromPage"] = pageName;
            try
            {
                ConnectionClass connection = new ConnectionClass(); //New connection object (See Connection.cs)
                conn = connection.getConnection();
            }
            catch(Exception x)
            {//Note how we're doing the error handling on the site: put Exception message into error session and redirect to Error.aspx
                Session["Error"] = x.Message;
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e) //NOTE to @Skroef: Check the isUser and isStaff methods i made (might execute faster)
        {
            //#1 Make the connection String.
            //#2 run the username and password to check if it is registered correct.
                //#2.1 check the users database table if user exist
                //#2.2 check the staff database table if user is staff

            /*conn.Open();
            string checkUser = "select count(*) from UserData where UserName='" + txtLogName.Text + "'";
            MySqlCommand com = new MySqlCommand(checkUser, conn);
            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            conn.Close();
            if (temp == 1)
            {
                conn.Open();
                string checkPassword = "select Password from UserData where UserName='" + txtLogName.Text + "'";
                MySqlCommand passcom = new MySqlCommand(checkPassword, conn);
                string password = passcom.ExecuteScalar().ToString().Replace(" ", "");

                Hash hPass = new Hash(txtLogPassword.Text); //Making new Hash object to hash the entered password to compare to what is in the database
                string enteredPassword = hPass.getHash(); //Getting the hash object's hash

                if (password == enteredPassword)
                {//If the user's UserName and Password is correct, get the user's data for the customerorder page, if the user is staff, send them to Orders page
                    Session["Username"] = txtLogName.Text;
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
            }*/



            Hash i = new Hash(txtLogPassword.Text);

            //Alternate way of doing the login btn
            if(isCustomer(txtLogUserName.Text, txtLogPassword.Text) == true)
            {//Remember that the isStaff and isCustomer already sets the sessions for the respected whatevers
                if (Session["OrderID"] == null)
                {
                    int j = getLastOrder(int.Parse(Session["CustomerID"].ToString()));
                    if (j > -1)
                    {
                        Session["OrderID"] = j;
                    }
                }
                Response.Redirect("CustomerOrder.aspx");
            }
            else if(isStaff(txtLogUserName.Text, txtLogPassword.Text) == true)
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

        protected bool isStaff(string usName, string psWord)
        {//This one is open to problems like 2 people having the same name and password
            bool isStaff = false;

            Hash hPass = new Hash(psWord); //Making new Hash object to hash the entered password to compare to what is in the database
            string enteredPassword = hPass.getHash(); //Getting the hash object's hash

            MySqlCommand comm = new MySqlCommand
            {
                Connection = conn,
                CommandText =
                "SELECT * " +
                "FROM Staff " +
                "WHERE `First name`= @fname " +
                "AND `Password`= @pasw "
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
        {//Check isStaff comments, it does the same
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
                "WHERE `Customer ID` = " + customerID +  " " +
                "AND Paid = " + 0
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