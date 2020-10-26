using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Website.App_Code;

namespace Website
{
    public partial class Checkout : System.Web.UI.Page
    {
        //session var names
        private static string userNameSession = "UserName";
        private static string errorSession = "Error";
        private static string fromPageSession = "FromPage";
        private static string orderIDSession = "OrderID";
        private static string tableIDSession = "TableID";

        private string pageName = HttpContext.Current.Request.Url.AbsoluteUri;

        private CartPanel cart;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(Session[fromPageSession] != null)
                {
                    Session[fromPageSession] = pageName;
                }
                
                DatabaseConnection connection = new DatabaseConnection();
                MySqlConnection conn = connection.getConnection();

                if (Session[tableIDSession] != null)
                {//if user is signed in with their table
                    if(Session[orderIDSession] != null)
                    {//user has their order placed
                        cart = new CartPanel(conn,int.Parse(Session[orderIDSession].ToString()));
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
                    Response.Redirect("Login.aspx", false);
                }
            }
            catch(Exception x)
            {
                Session[errorSession] = x.Message.ToString();
                Response.Redirect("Error.aspx");
            }
        }

        private void checkoutBtnClicked(object sender, EventArgs e)
        {
            Session[errorSession] = new NotImplementedException().Message;
            Response.Redirect("Error.aspx");
        }

    }
}