using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class Checkout : System.Web.UI.Page
    {
        /*private MySqlConnection conn;
        private static string server = "sql7.freemysqlhosting.net";
        private static string database = "sql7368973";
        private static string userName = "sql7368973";
        private static string userPass = "1lFxsKtjXr";
        String connectionString = "Server=" + server + ";" + "Port=3306;" + "Database=" +
            database + ";" + " Uid=" + userName + ";" + "pwd=" + userPass + ";";*/

        private string pageName = HttpContext.Current.Request.Url.AbsoluteUri;

        OrderVisual cart;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["FromPage"] = pageName;
            try
            {
                Connection connection = new Connection();
                MySqlConnection conn = connection.getConnection();

                if (Session["CustomerID"] != null)
                {//if user is signed in
                    if(Session["OrderID"] != null)
                    {//user has their order placed
                        cart = new OrderVisual(conn,int.Parse(Session["CustomerID"].ToString()),int.Parse(Session["OrderID"].ToString()));
                        pnlCheckout.Controls.Add(cart.getHeadPanel());


                        Button checkOut = new Button();
                        checkOut.Text = "Pay";
                        checkOut.CssClass = "btn btn-dark";
                        checkOut.Click += new EventHandler(checkoutBtnClicked);
                        pnlCheckout.Controls.Add(checkOut);
                    }
                }
                else
                {//send them to login/signup page
                    Response.Redirect("Login.aspx");
                }
            }
            catch(Exception x)
            {
                Session["Error"] = x.Message.ToString();
                Response.Redirect("Error.aspx");
            }
        }

        private void checkoutBtnClicked(object sender, EventArgs e)
        {
            Session["Error"] = new NotImplementedException().Message;
            Response.Redirect("Error.aspx");
        }

    }
}