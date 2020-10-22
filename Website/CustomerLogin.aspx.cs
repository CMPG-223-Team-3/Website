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
    public partial class CustomerLogin : System.Web.UI.Page
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
                DatabaseConnection connection = new DatabaseConnection(); //New connection object (See Connection.cs)
                conn = connection.getConnection();
            }
            catch (Exception x)
            {//Note how we're doing the error handling on the site: put Exception message into error session and redirect to Error.aspx
                Session[errorSession] = x.Message;
                Response.Redirect("Error.aspx");
            }
        }

        protected int lastOrderID(int tableNr)
        {
            int orderIdnum = -1;

            MySqlCommand comm = new MySqlCommand
            {
                Connection = conn,
                CommandText =
                "SELECT * " +
                "FROM ORDER " +
                "WHERE Table_nr = @tnr "
            };
            comm.Parameters.AddWithValue("@tnr", tableNr);

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
            int ordNr = lastOrderID(int.Parse(txtTable.Text));

            if(ordNr > 0)
            {//dunno how table nums work - rn can't be less than 1
                Session[orderIDSession] = ordNr;
                Session[tableIDSession] = int.Parse(txtTable.Text);
                Response.Redirect("IsThisYourOrder.aspx");
            }
            else
            {
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