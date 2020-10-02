using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/*NOTES:
 * The logged in user session name here is "UserName"
 * I still need to figure out how to connect to the database and stuff
 * 
 * 
 */

namespace Website
{
    public partial class CustomerOrder : System.Web.UI.Page
    {
        /*private MySqlConnection conn;
        private static string server = "mysql";
        private static string database = "test";
        private static string userName = "root";
        private static string userPass = "5e3RsCPNomtGJDa8Vg";
        private string connString = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + userName + ";PASSWORD=" + userPass;*/


        //Global variables and such
        private bool isSearched = false; //Has the user searched for something

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //try to quickly connect to database to see if it works
                //conn = new MySqlConnection(connString);
                //conn.Open();

                if (Session["UserName"] != null)
                {
                    //if the user has logged in, display their name instead of the log in label on the navbar
                    lblLogin.Text = Session["UserName"].ToString();
                }
                if (!IsPostBack || !isSearched)
                {
                    //if the user hasn't searched anything or 1st time page loaded
                    //all products should be displayed
                    //showProducts(conn, "SELECT * FROM ");
                }
                if (isSearched)
                {
                    //search in the products for what the user wants
                    isSearched = false;
                }

                //conn.Close();
            }
            catch
            {
                Response.Redirect("Error.aspx");
            }
        }

        //Method to load the correct products from the sent query (query in string form instead of a command var is easier to manipulate at this stage)
        public void showProducts(MySqlConnection mysqlConnection, String command)
        {
            int countedProducts = 0;

            try
            {
                //Clear the main products panel to avoid accidental doubles
                pnlMaster.Controls.Clear();

                //Search the database for the searched terms, convert to a reader object(s) to make easier to manipulate
                using(mysqlConnection)
                {
                    mysqlConnection.Open();
                    MySqlCommand mysqlCommand = new MySqlCommand(command, mysqlConnection);
                    MySqlDataReader mysqlReader = mysqlCommand.ExecuteReader();

                    if(mysqlReader != null)
                    {
                        //While database has x many products searched in command parm
                        while(mysqlReader.Read())
                        {
                            //Reading specific product info out of database to use for the cards
                            string productId = mysqlReader.GetValue(0).ToString();
                            string productName = mysqlReader.GetValue(1).ToString();
                            string productPrice = mysqlReader.GetValue(2).ToString();
                            string productDesc = mysqlReader.GetValue(3).ToString();
                            string productImageUrl = mysqlReader.GetValue(4).ToString();

                            countedProducts++;

                            //Create panel to serve as a card, so img, price, name, description can be added inside it
                            Panel pnl1 = new Panel();
                            pnl1.CssClass = "card row";

                            Panel pnlNameDesc = new Panel();
                            pnlNameDesc.CssClass = "col-sm-8";
                            pnlNameDesc.Attributes.CssStyle.Add("display","flex");
                            pnlNameDesc.Attributes.CssStyle.Add("flex-direction", "column");

                            Panel pnlPriceBtn = new Panel();
                            pnlPriceBtn.CssClass = "col-sm-4";
                            pnlPriceBtn.Attributes.CssStyle.Add("display", "flex");
                            pnlPriceBtn.Attributes.CssStyle.Add("flex-direction", "column");

                            //Label for the price
                            Label lblPrice = new Label();
                            lblPrice.Text = productPrice;
                            //lblPrice.CssClass = "";

                            //Creating image object
                            Image img1 = new Image();
                            img1.ImageUrl = productImageUrl;
                            //im1.CssClass = "";
                            img1.AlternateText = "Product Image";

                            //Creating the add to cart button
                            Button btn1 = new Button();
                            btn1.Text = "Add to cart";
                            btn1.CssClass = "btn btn-lg";
                            btn1.ID = productId; //Using the product id as the button pressed id for the event that the button is pressed, so we can see which button was pressed
                            btn1.Click += new EventHandler(addToCartBtnClicked); //To correctly link the event to the event handler

                            //Label object for the name of the item
                            Label lblName = new Label();
                            lblName.Text = productName;
                            //lblName.CssClass = 

                            //Label object for the description of the item
                            Label lblDesc = new Label();
                            lblDesc.Text = productDesc;
                            //lblName.CssClass = 

                            //Add items to their respective panels
                            pnlNameDesc.Controls.Add(lblName);
                            pnlNameDesc.Controls.Add(lblDesc);
                            pnlPriceBtn.Controls.Add(lblPrice);
                            pnlPriceBtn.Controls.Add(btn1);
                            pnl1.Controls.Add(pnlNameDesc);
                            pnl1.Controls.Add(pnlPriceBtn);

                            //Add panel to master panel
                            pnlMaster.Controls.Add(pnl1);
                        }
                    }
                    else
                    {
                        throw new Exception("Could not access database items");
                    }
                }
            }
            catch(Exception exc)
            {
                Response.Redirect("Erorr.aspx");
            }
        }

        //Eventhandler/method for the add to cart buttons
        void addToCartBtnClicked(object sender, EventArgs evArgs)
        {
            //Code to identify which product clicked by getting the button's id which was programmed as the product's id in showProducts()
            Button btn = sender as Button;
            string Id = btn.ID;

            //Second check if user is logged in so we can add the selected products to their cart
            if(Session["UserName"] != null)
            {
                //Add the item to the cart

                //If user does not have a cart assigned to them, this is where you'd want to choose what happens next
            }
            else
            {
                //Help the user log in without losing the selected item(s)
            }
        }
    }

    
}