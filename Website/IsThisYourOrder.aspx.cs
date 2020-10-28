using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        private string menuIDCol = "Menu_Item_ID";
        private string menuNameCol = "Item_Name";
        private string menuPriceCol = "Price";
        private string orderItemsMenuIDCol = "Menu_Item_ID";
        private string orderItemsOrderIDCol = "Order_ID";
        private string orderItemsQuantityCol = "Quantity_Ordered";

        private MySqlConnection conn;
        private Order order;
        private DataTable orderItemsTable;
        private DataTable menuItems;
        private float totalPrice;

        private Label name;
        private Label price;
        private Label quanlbl;
        private Panel headPanel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[userNameSession] != null || Session[tableIDSession] != null)
            {
                if (Session[orderIDSession] != null)
                {
                    try
                    {
                        menuItems = getMenuItems();
                    }
                    catch (Exception x)
                    {
                        throwEx(x);
                    }

                    int tNr = int.Parse(Session[tableIDSession].ToString());
                    int oNr = int.Parse(Session[orderIDSession].ToString());
                    try
                    {
                        order = new Order(oNr);

                        update();
                        if(totalPrice <= 0)
                        {
                            noBtnClicked(totalPrice,new EventArgs());
                        }
                        

                        Button yes = new Button();
                        Button no = new Button();

                        yes.Text = "Yes";
                        no.Text = "No";

                        yes.CssClass = "btn btn-dark btn-lg m-2";
                        no.CssClass = "btn btn-dark btn-lg m-2";

                        yes.Click += new EventHandler(yesBtnClicked);
                        no.Click += new EventHandler(noBtnClicked);

                        yesnopanel.Controls.Add(yes);
                        yesnopanel.Controls.Add(no);
                    }
                    catch (Exception x)
                    {
                        throwEx(x);
                    }
                }
                else
                {
                    Response.Redirect("CustomerLogin.aspx", true);
                }
            }
            else
            {
                Response.Redirect("CustomerLogin.aspx", true);
            }
        }

        private void noBtnClicked(object sender, EventArgs e)
        {
            Session[orderIDSession] = null;
            Response.Redirect("CustomerOrder.aspx", false);
        }

        private void yesBtnClicked(object sender, EventArgs e)
        {
            Response.Redirect("CustomerOrder.aspx", false);
        }

        private void throwEx(Exception x)
        {
            Session[errorSession] = x.Message + x.StackTrace;
            HttpContext.Current.Response.Redirect("Error.aspx", false);
        }

        public DataTable getMenuItems()
        {//Method to fetch everything from the menu table and DataTabling it - should be faster than commanding it every time
            DataTable ds = new DataTable();
            try
            {
                MySqlCommand cm = new MySqlCommand();
                cm.CommandText =
                    "SELECT * " +
                    "FROM `MENU-ITEM` ";
                cm.Connection = new DatabaseConnection().getConnection();
                MySqlDataAdapter adap = new MySqlDataAdapter(cm);
                adap.Fill(ds);

                if (ds.Rows.Count == 0 || ds == null)
                {
                    throw new Exception("Found empty menu database");
                }
            }
            catch (Exception x)
            {
                throwEx(x);
            }
            return ds;
        }



        public int update()
        {
            float productPrice = 0;
            string productName = "-1";
            int productId = -1;
            int quantity = -1;
            int counterer = 0;
            bool isFound3 = false;
            orderpanel.Controls.Clear();
            totalPrice = 0;

            try
            {
                if(order != null && order.getOrderItemsObject().getThisOrderItems().Rows.Count > 0)
                {
                    orderItemsTable = order.getOrderItemsObject().getThisOrderItems();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception x)
            {
                throwEx(x);
            }


            if (menuItems.Rows.Count <= 0)
            {//if the menuItems table is empty
                throwEx(new Exception("Menu Table is Empty"));
            }

            foreach (DataRow datrow in orderItemsTable.Rows)
            {
                if (datrow[orderItemsOrderIDCol].ToString() == order.getOrderID().ToString())
                {
                    try
                    {
                        productId = int.Parse(datrow[orderItemsMenuIDCol].ToString());
                        quantity = int.Parse(datrow[orderItemsQuantityCol].ToString());
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

                    Panel temp = new Panel();

                    name = new Label();
                    price = new Label();
                    quanlbl = new Label();
                    this.totalPrice += productPrice * quantity;

                    price.CssClass = "col-4";
                    quanlbl.CssClass = "col-4";
                    name.CssClass = "col-4";

                    name.Text = productName;
                    price.Text = "R" + productPrice;
                    quanlbl.Text = quantity.ToString() + " X ";

                    //Build the first part of headPanel
                    temp.Controls.Add(quanlbl);
                    temp.Controls.Add(name);
                    temp.Controls.Add(price);
                    orderpanel.Controls.Add(temp);

                    counterer++;
                }
            }
            return counterer;
        }
    }
}