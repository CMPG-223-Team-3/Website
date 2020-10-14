using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login_Main
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFinLogOut_Click(object sender, EventArgs e)
        {
            //add logoff time to database

            Response.Write("you have successfully loged out");
        }
    }
}