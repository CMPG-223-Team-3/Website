using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Website
{
    public partial class WaiterLogout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFinalLogout_Click(object sender, EventArgs e)
        {
            //add logoff time to database

            Response.Write("you have successfully loged out");
        }
    }
}