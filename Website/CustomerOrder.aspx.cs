using MySql.Data.MySqlClient;
using System;
using System.Web;
using System.Web.UI.WebControls;
using Website.App_Code;

/*NOTES:
 * The logged in user session name here is "UserName"
 * Make the products cards pretty
 * Need help with sass
 */

namespace Website
{
    public partial class CustomerOrder : System.Web.UI.Page
    {
        private Order order;
        private CartPanel ov;
        private MySqlConnection conn;
        private string pageName = HttpContext.Current.Request.Url.AbsoluteUri;

        //Global variables and such
        private bool isSearched = false; //Has the user searched for something

        protected void Page_Init(object o, EventArgs e)
        {
            try
            {
                //try to quickly connect to database to see if it works  
                ConnectionClass connection = new ConnectionClass();
                conn = connection.getConnection();

                if(Session["UserName"] == null)
                {
                    throw new Exception("Username not logged in");
                }
            }
            catch
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Error"] = null;
            Session["FromPage"] = pageName;

            try
            {
                if (Session["UserName"] != null)
                {
                    //if the user has logged in, display their name instead of the log in label on the navbar
                    lblLogin.Text = Session["UserName"].ToString();

                    if (Session["CustomerID"] != null)
                    {
                        if (Session["OrderID"] != null)
                        {
                            order = new Order(int.Parse(Session["OrderID"].ToString())); //at this point we need a customer ID to make a new order, have to fix
                        }
                        else
                        {
                            order = new Order(int.Parse(Session["CustomerID"].ToString()), 0, 0);
                            Session["OrderID"] = order.getOrderID();
                        }
                    }
                    else
                    {
                        Page_Init(new object(), new EventArgs());
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                if (!IsPostBack || !isSearched)
                {
                    //if the user hasn't searched anything or 1st time page loaded

                    ov = new CartPanel(order.getConnection(), order.getCustomerID(), order.getOrderID());
                    pnlOrder.Controls.Add(ov.getHeadPanel());
                    Button checkoutBtn = new Button();
                    checkoutBtn.Text = "Checkout";
                    checkoutBtn.CssClass = "btn btn-dark btn-lg";
                    checkoutBtn.Click += new EventHandler(checkoutBtnClicked);
                    pnlOrder.Controls.Add(checkoutBtn);
                    showProducts(conn, "SELECT * FROM Menu_Item");
                }

                if (isSearched)
                {
                    //if the user did search in the products for what the user wants
                    isSearched = false;
                }
            }
            catch(Exception ee)
            {

                Session["Error"] = ee.Message + ee.StackTrace;
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
                            //According to the data model columns are: 0-Menu-Item-ID; 1-Recipe-ID; 2-Category-ID; 3-Name; 4-Price
                            string productId = mysqlReader.GetString("Menu-Item ID");
                            string productName = mysqlReader.GetString("Name");
                            string productPrice = mysqlReader.GetString("Price");

                            countedProducts++;

                            //Create panel to serve as a card, so img, price, name can be added inside it
                            Panel pnl1 = new Panel();
                            pnl1.CssClass = "card row bg-dark m-md-1";

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

                            //Creating image object (for future implementation)
                            /*Image img1 = new Image();
                            img1.ImageUrl = productImageUrl;
                            //im1.CssClass = "";
                            img1.AlternateText = "Product Image";*/

                            //Creating the add to cart button
                            Button btn1 = new Button();
                            btn1.Text = "Add to cart";
                            btn1.CssClass = "btn btn-light";
                            btn1.ID = productId + "_addtocart_" + countedProducts; //Using the product id as the button pressed id for the event that the button is pressed, so we can see which button was pressed
                            btn1.Click += new EventHandler(addToCartBtnClicked); //To correctly link the event to the event handler
                            btn1.CausesValidation = false;

                            //Label object for the name of the item
                            Label lblName = new Label();
                            lblName.Text = productName;
                            //lblName.CssClass = 

                            //Add items to their respective panels
                            pnlNameDesc.Controls.Add(lblName);
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
                    mysqlConnection.Close();
                }
            }
            catch(Exception exc)
            {
                Session["Error"] = exc.Message;
                Response.Redirect("Erorr.aspx");
            }
        }

        //Eventhandler/method for the add to cart buttons
        void addToCartBtnClicked(object sender, EventArgs evArgs)
        {
            try
            {
                //Code to identify which product clicked by getting the button's id which was programmed as the product's id in showProducts()
                Button btn = sender as Button;
                string[] i = btn.ID.Split('_');
                int Id = int.Parse(i[0]);

                order.getOrderItemsObject().addProduct(Id, 1);
                ov.update();
            }
            catch(Exception x)
            {
                Session["Error"] = x.Message + ":   " + x.StackTrace;
                Response.Redirect("Error.aspx");
            }

            //Second check if user is logged in so we can add the selected products to their cart
            if (Session["UserName"] != null)
            {
                //Add the item to the cart


                //If user does not have a cart assigned to them, this is where you'd want to choose what happens next
            }
            else
            {
                //Help the user log in without losing the selected item(s)
            }
        }

        //When user searched for product
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //If searchbar isn't empty
            if(txtSearch.Text != null)
            {
                try
                {
                    //Create new cmmd to ShowProducts() for filtered items
                    string tmp = "SELECT * FROM Menu_Item WHERE Name LIKE '" + "%" + txtSearch.Text.ToLower() + "%" + "'";
                    showProducts(conn, tmp);
                }
                catch(Exception x)
                {
                    Session["Erorr"] = x.Message;
                    Response.Redirect("Error.aspx");
                }
            }
            isSearched = true;
        }

        private void checkoutBtnClicked(object sender, EventArgs e)
        {
            try
            {
                Session["UserID"] = this.order.getCustomerID();
                Session["OrderID"] = this.order.getOrderID();
                Response.Redirect("Checkout.aspx");
            }
            catch(Exception x)
            {
                Session["Erorr"] = x.Message;
                Response.Redirect("Error.aspx");
            }
            
        }
    }
}