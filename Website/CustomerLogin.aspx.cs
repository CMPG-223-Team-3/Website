using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class CustomerLogin : System.Web.UI.Page
    {
        //session var names
        private static string userNameSession = "UserName";
        private static string errorSession = "Error";
        private static string fromPageSession = "FromPage";
        private static string orderIDSession = "OrderID";
        private static string tableIDSession = "TableID";
        private static string orderObjectSession = "OrderObject";


        private MySqlConnection conn;
        private string pageName = HttpContext.Current.Request.Url.AbsoluteUri; //Getting the pagename to store in session at page load so we can know which page to go back to after Error page is thrown

        protected void Page_Load(object sender, EventArgs e)
        {
            Session[fromPageSession] = pageName;
            try
            {
                DatabaseConnection connection = new DatabaseConnection(); //New connection object (See Connection.cs)
                conn = connection.getConnection();
            }
            catch (Exception x)
            {//Note how we're doing the error handling on the site: put Exception message into error session and redirect to Error.aspx
                Session[errorSession] = x.Message;
                Response.Redirect("Error.aspx");
            }
        }

        protected int lastOrderID(int tableNr, string customerName)
        {
            int orderIdnum = -1;

            MySqlCommand comm = new MySqlCommand
            {
                Connection = conn,
                CommandText =
                "SELECT Order_ID " +
                "FROM ORDER " +
                "WHERE Table_nr = @tnr " +
                "AND Customer_Name = @csn"
            };
            comm.Parameters.AddWithValue("@tnr", tableNr);
            comm.Parameters.AddWithValue("@csn", customerName.ToUpper());

            try
            {
                using (conn)
                {
                    conn.Open();
                    using(MySqlDataReader r = comm.ExecuteReader())
                    {//using a reader here cus i need the last value
                        if(r.HasRows)
                        {
                            while(r.Read())
                            {
                                orderIdnum = int.Parse(r["Order_ID"].ToString());
                            }
                        }
                    }
                }
            }
            catch(Exception xx)
            {
                throwEx(xx);
            }

            return orderIdnum;
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            Session[tableIDSession] = int.Parse(txtTable.Text);
            Session[userNameSession] = txtName.Text;

            if (Session[orderObjectSession] != null)
            {//this part is here when the customer tried to order without logging in first
                Order i = (Order)Session[orderObjectSession];
                i.updateOrderNameAndTable(txtName.Text, int.Parse(txtTable.Text));
                i.getOrderItemsObject().close();
                Session[orderIDSession] = i.getOrderID();

                Response.Redirect("Checkout.aspx");
            }
            else
            {
                int ordNr = lastOrderID(int.Parse(txtTable.Text), txtName.Text);

                if (ordNr > 0)
                {//dunno how table nums work - rn can't be less than 1\
                    //this what happens if an order found matching tableID & name entered
                    Session[orderIDSession] = ordNr;
                    Response.Redirect("IsThisYourOrder.aspx"); //send the last order number matching to the entered table number and name to the page to check with them if that is their last order(given that they lost connection or something)
                }
                if (ordNr == -1)
                {//if no order was found, no worries, go to where the customer can order
                    Response.Redirect("CustomerOrder.aspx", true);
                }

                Response.Write("<script>alert('We're having trouble with the entered table number or order)</script>");
            }
        }

        private void throwEx(Exception x)
        {
            Session[errorSession] = x.Message + x.StackTrace;
            HttpContext.Current.Response.Redirect("Error.aspx", false);
        }
    }
}