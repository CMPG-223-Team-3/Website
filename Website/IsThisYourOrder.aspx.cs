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
    public partial class IsThisYourOrder : System.Web.UI.Page
    {
        //session var names
        private static string userNameSession = "UserName";
        private static string errorSession = "Error";
        private static string fromPageSession = "FromPage";
        private static string orderIDSession = "OrderID";
        private static string tableIDSession = "TableID";

        private MySqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session[orderIDSession] == null && Session[tableIDSession] != null)
            {
                Response.Redirect("CustomerLogin.aspx");
            }

            int tNr = int.Parse(Session[tableIDSession].ToString());
            int oNr = int.Parse(Session[orderIDSession].ToString());

            try
            {
                conn = new DatabaseConnection().getConnection();

                CartPanel panel = new CartPanel(conn, oNr);
                this.Controls.Add(panel);

                Button yes = new Button();
                Button no = new Button();

                yes.CssClass = "btn btn-dark";
                no.CssClass = "btn btn-dark";

                yes.Click += new EventHandler(yesBtnClicked);
                no.Click += new EventHandler(noBtnClicked);
            }
            catch(Exception x)
            {
                throwEx(x);
            }
        }

        private void noBtnClicked(object sender, EventArgs e)
        {
            Session[orderIDSession] = null;
            Response.Redirect("CustomerOrder.aspx");
        }

        private void yesBtnClicked(object sender, EventArgs e)
        {
            Response.Redirect("CustomerOrder.aspx");
        }

        private void throwEx(Exception x)
        {
            Session[errorSession] = x.Message + x.StackTrace;
            HttpContext.Current.Response.Redirect("Error.aspx", false);
        }
    }
}