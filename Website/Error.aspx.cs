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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Error"].ToString() != null)
                lblError.Text = Session["Error"].ToString();
            else
                lblError.Text = "Something went wrong...";

            Button i = new Button();
            i.Click += new EventHandler(btnClicked);
            i.Text = "Retry";
            pnlError.Controls.Add(i);

        }

        public void btnClicked(object sender, EventArgs e)
        {
            if(Session["FromPage"].ToString() != null)
            {
                Response.Redirect(Session["FromPage"].ToString());
            }
        }
    }
}