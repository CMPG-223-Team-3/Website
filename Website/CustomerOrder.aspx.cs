using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class CustomerOrder : System.Web.UI.Page
    {
        //Global variables and such
        private bool isSearched = false; //Has the user searched for something

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null) 
            {
                //if the user has logged in, display their name instead of the log in label on the navbar
                lblLogin.Text = Session["UserName"].ToString();
            }
            if(!IsPostBack || !isSearched)
            {
                //if the user hasn't searched anything or 1st time page loaded
                //all products should be displayed
            }
            if(isSearched)
            {
                //search in the products for what the user wants
                isSearched = false;
            }
        }

        //Method to load the correct products from the sent query (query in string form instead of a command var is easier to manipulate at this stage)
        public void showProducts()
        {
            int countedProducts = 0;

            try
            {
                //Clear the main products panel to avoid accidental doubles
                pnlMaster.Controls.Clear();

                //Search the database for the searched terms, convert to a reader object(s) to make easier to manipulate
            }
            catch(Exception exc)
            {
                Response.Redirect("Erorr.aspx");
            }
        }
    }
}