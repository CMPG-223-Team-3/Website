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

        private static string orderCookieName = "OrderCookie";
        private static string orderCookieSubName = "OrderIDCookie";

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

                if (Session[tableIDSession] != null && Session[userNameSession] != null)
                {//if user is signed in with their table
                    if(Session[orderIDSession] != null)
                    {//user has their order placed
                        if(cart == null)
                        {
                            cart = new CartPanel(new Order(int.Parse(Session[orderIDSession].ToString())));
                        }
                        else
                        {
                            cart.update();
                        }
                        if (cart.getTotalPrice() == 0)
                        {
                            cart.order.getOrderItemsObject().close();
                            Response.Redirect(Session[fromPageSession].ToString(), false);
                        }
                        pnlCheckout.Controls.Add(cart.getHeadPanel());

                        Button checkOut = new Button();
                        checkOut.Text = "Confirm Order";
                        checkOut.CssClass = "btn btn-dark";
                        checkOut.Click += new EventHandler(checkoutBtnClicked);
                        pnlCheckout.Controls.Add(checkOut);
                    }
                }
                else
                {//send them to login/signup page
                    Response.Redirect("CustomerLogin.aspx", false);
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
            try
            {
                cart.update();
                cart.order.getOrderItemsObject().close();
                try
                {
                    HttpCookie userCookie = new HttpCookie(orderCookieName);
                    userCookie[orderCookieSubName] = cart.order.getOrderID().ToString();
                    Response.Cookies.Add(userCookie);
                    userCookie.Expires = DateTime.Now.AddHours(5);
                }
                catch(Exception x)
                {
                    throw new HttpException();
                }

                Session[orderIDSession] = null;

                Response.Write("<script>alert('Thank you! Your waiter will be with you soon to confirm payment')<script>");
                Response.Redirect("OrderStatus.aspx", false);
            }
            catch(HttpException x)
            {
                Response.Write("<script>alert('It seems that we can't create a cookie to store your order... For order details, contact your waiter...')<script>");
                Response.Redirect("default.aspx", false);
            }
            catch(Exception x)
            {
                throwEx(x);
            }
            Page_Load(new object(), new EventArgs());
        }

        private void throwEx(Exception x)
        {
            Session[errorSession] = x.Message + x.StackTrace;
            HttpContext.Current.Response.Redirect("Error.aspx", false);
        }

    }
}