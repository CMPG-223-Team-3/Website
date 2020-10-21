using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class Error : System.Web.UI.Page
    {
        //session var names
        private static string errorSession = "Error";
        private static string fromPageSession = "FromPage";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[errorSession] != null)
                lblError.Text = Session[errorSession].ToString();
            else
                lblError.Text = "Something went wrong...";

            Button i = new Button();
            i.Click += new EventHandler(btnClicked);
            i.Text = "Retry";
            pnlError.Controls.Add(i);
        }

        public void btnClicked(object sender, EventArgs e)
        {
            if (Session[fromPageSession] != null)
            {
                Response.Redirect(Session[fromPageSession].ToString());
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}