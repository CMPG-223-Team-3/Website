using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Website;

namespace Website.App_Code
{
    public class CartPanel : System.Web.UI.Page
    {
        /*
        * This class's purpose is to create a way (panel) to visualize the 
        *  customer's order (or rather the items in their order)
        *  so they can manage their own order
        *  
        * 1. It runs really slow from my experience
        * 2. I will try to upload a diagram of what it should look like
        * 3. This one may still need a teeny bit of error handling
        * 4. Check the diagrams folder for a visual of how this is supposed to look
        * 
        */

        private MySqlConnection conn;
        private int customerID;
        private int orderID;
        private Panel headPanel; //Main panel to put each orderpanel into with the information of the order item
        private float totalPrice;
        public Order order;
        private DataTable menuItems;
        private DataTable orderItems;

        private Button min;
        private Button plus;
        private Label name;
        private Label price;
        private Label quanlbl;

        private string menuIDCol = "Menu-Item ID";
        private string menuNameCol = "Name";
        private string menuPriceCol = "Price";
        private string orderItemsMenuIDCol = "Menu-Item ID";
        private string orderItemsOrderIDCol = "Order ID";
        private string orderItemsQuantityCol = "Quantity";


        public CartPanel(MySqlConnection c, int customerID, int orderID)
        {
            this.conn = c;
            this.customerID = customerID;
            this.orderID = orderID;

            headPanel = new Panel();
            headPanel.CssClass = "row";

            try
            {
                order = new Order(orderID);
                menuItems = getMenuItems();
                orderItems = getOrderItems();
                update();
            }
            catch(Exception x)
            {
                throwEx(x);
            }
        }

        public CartPanel(Order ord)
        {//Constructor that receives the person's Order object (already has all the info we need)
            this.conn = ord.getConnection();
            this.customerID = ord.getCustomerID();
            this.orderID = ord.getOrderID();

            headPanel = new Panel();
            headPanel.CssClass = "row";

            order = ord;

            try
            {
                menuItems = getMenuItems();
                orderItems = getOrderItems();
                update();
            }
            catch(Exception x)
            {
                throwEx(x);
            }
        }

        private void throwEx(Exception x)
        {
            Session["Error"] = x.Message + x.StackTrace;
            HttpContext.Current.Response.Redirect("Error.aspx", false);
        }

        public DataTable getOrderItems()
        {
            DataTable i = new DataTable();
            try
            {
                i = order.getOrderItemsObject().getCurrentTable();
            }
            catch(Exception x)
            {
                throwEx(x);
            }
            return i;
        }

        public DataTable getMenuItems()
        {//Method to fetch everything from the menu table and DataTabling it - should be faster than commanding it every time
            DataTable ds = new DataTable();
            try
            {
                MySqlCommand cm = new MySqlCommand();
                cm.Connection = conn;
                cm.CommandText =
                    "SELECT * " +
                    "FROM Menu_Item ";
                //cm.Parameters.AddWithValue("@mm", menuTableName);
                MySqlDataAdapter adap = new MySqlDataAdapter(cm);
                adap.Fill(ds);

                if(ds.Rows.Count == 0 || ds == null)
                {
                    throw new Exception("Found empty menu database");
                }
            }
            catch(Exception x)
            {
                throwEx(x);
            }
            return ds;
        }

        public int update()
         {
            //It's basically a reload method for every product in the order
            //returns the count of things updated
            float productPrice = 0;
            string productName = "-1";
            int productId = -1;
            int quantity = -1;
            int counterer = 0;
            bool isFound3 = false;
            headPanel.Controls.Clear();
            totalPrice = 0;

            try
            {
                orderItems = getOrderItems();//Reloading the orderItems cuz it may have changed through adding/deleting new items
            }
            catch(Exception x)
            {
                throwEx(x);
            }

            if (orderItems.Rows.Count <= 0)
            {//if there is no orderIems in order
                return 0;
            }

            if(menuItems.Rows.Count <= 0)
            {//if the menuItems table is empty
                throwEx(new Exception("Menu Table is Empty"));
            }

            foreach (DataRow datr in orderItems.Rows)
            {//get every orderitem and make it pretty on the site
                if (datr[orderItemsOrderIDCol].ToString() == orderID.ToString())
                {//If orderID is found in the orderItems - to use the menuitemID and quantity - for use of building the headPanel pretty
                    try
                    {
                        productId = int.Parse(datr[orderItemsMenuIDCol].ToString());
                        quantity = int.Parse(datr[orderItemsQuantityCol].ToString());
                    }
                    catch (Exception ex)
                    {
                        throwEx(ex);
                    }

                    try
                    {
                        foreach (DataRow dr in menuItems.Rows)
                        {//Get the product's name, price and such to show in the headPanel
                            isFound3 = false;
                            if (dr[menuIDCol].ToString() == productId.ToString())
                            {
                                productPrice = float.Parse(dr[menuPriceCol].ToString());
                                productName = dr[menuNameCol].ToString();
                                isFound3 = true;
                            }
                            if (isFound3)
                            {//If the menuitem with ID is found, stop traversing
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throwEx(ex);
                    }
                    if (!isFound3)
                    {//has not found the productId in the menuItems table
                        throwEx(new Exception("Could not find product: " + productId + " in database"));
                    }

                    min = new Button();
                    plus = new Button();
                    name = new Label();
                    price = new Label();
                    quanlbl = new Label();
                    this.totalPrice += productPrice * quantity;

                    min.CssClass = "btn btn-dark col-3";
                    plus.CssClass = "btn btn-dark col-3";
                    price.CssClass = "col-2";
                    quanlbl.CssClass = "col-2";
                    name.CssClass = "col-2";

                    //Putting the id of the product as the buttons' ids for the eventhandler so it distinguishes what button was pressed
                    min.ID = productId.ToString() + "_min_" + counterer;
                    plus.ID = productId.ToString() + "_plus_" + counterer;

                    //Eventhandlers for each button - one for subtracting a product and the other for adding one
                    min.Click += new EventHandler(minBtnClicked);
                    plus.Click += new EventHandler(plusBtnClicked);

                    min.CausesValidation = false;
                    plus.CausesValidation = false;

                    name.Text = productName;
                    price.Text = "R" + productPrice;
                    min.Text = "-";
                    plus.Text = "+";
                    quanlbl.Text = quantity.ToString() + " X ";

                    //Build the first part of headPanel
                    headPanel.Controls.Add(min);
                    headPanel.Controls.Add(quanlbl);
                    headPanel.Controls.Add(name);
                    headPanel.Controls.Add(price);
                    headPanel.Controls.Add(plus);

                    counterer++;
                }
            }
            //Add the total label in the headPanel
            Label total = new Label();
            total.Text = "R" + totalPrice.ToString();
            total.CssClass = "";

            headPanel.Controls.Add(total);
            return counterer;
        }

        private void plusBtnClicked(object sender, EventArgs e)
        {//when the plus btn is clicked on a product
            try
            {
                //get the button id which is set to the id of the product in update() that needs to be added 
                //format: productID_plus_counter to make the buttons's id's unique
                Button btn = sender as Button;
                string[] i = btn.ID.Split('_');
                int Id = int.Parse(i[0]);

                order.getOrderItemsObject().addProduct(Id, 1);
                update();
            }
            catch(Exception c)
            {
                throwEx(c);
            }
        }

        private void minBtnClicked(object sender, EventArgs e)
        {//when the minus button is clicked on a product
            try
            {
                //get the button id which is set to the id of the product in update() that needs to be added 
                //format: productID_min_counter to make the buttons's id's unique
                Button btn = sender as Button;
                string[] i = btn.ID.Split('_');
                int Id = int.Parse(i[0]);

                order.getOrderItemsObject().removeProduct(Id, 1);
                update();
            }
            catch(Exception x)
            {
                throwEx(x);
            }
        }

        public Panel getHeadPanel()
        {
            return headPanel;
        }

        public float getTotalPrice()
        {
            return this.totalPrice;
        }
    }
}