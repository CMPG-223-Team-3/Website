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
    public partial class OrderStatus : System.Web.UI.Page
    {
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

        private static string orderCookieName = "OrderCookie";
        private static string orderCookieSubName = "OrderIDCookie";

        private Order order;
        private float totalPrice;
        private DataTable orderItemsTable;
        private object menuItems;
        private Label name;
        private Label price;
        private Label quanlbl;

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies[orderCookieName];
            if(cookie != null)
            {
                if(cookie[orderCookieSubName] != null)
                {
                    try
                    {
                        order = new Order(int.Parse(cookie[orderCookieSubName]));

                        update();
                        if (totalPrice <= 0)
                        {//just to make sure that order isn't empty
                            throwEx(new Exception("It looks as if the order saved is empty"));
                        }

                        Label status = new Label();
                        status.Text = "Status of your order: " +  order.getStatus().ToString();
                        pnlstatus.Controls.Add(status);
                    }
                    catch (Exception x)
                    {
                        throwEx(x);
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('It seems that we could not retrieve your order cookie, please contact your waiter for order status')<script>");
                Label i = new Label();
                i.Text = "Whoops, we could not retrieve an order... If you have placed one, please contact your waiter for your order status...";
                i.CssClass = "display-1";

                pnl1.Controls.Add(i);
                //Response.Redirect("Default.aspx", false);
            }
        }

        private void throwEx(Exception x)
        {
            throw new NotImplementedException();
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
            pnl1.Controls.Clear();
            totalPrice = 0;

            try
            {
                if (order != null && order.getOrderItemsObject().getThisOrderItems().Rows.Count > 0)
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

            DataTable menu = getMenuItems();

            if (menu.Rows.Count <= 0)
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
                        foreach (DataRow dr in menu.Rows)
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

                    name = new Label();
                    price = new Label();
                    quanlbl = new Label();
                    totalPrice += productPrice * quantity;

                    price.CssClass = "col-2";
                    quanlbl.CssClass = "col-2";
                    name.CssClass = "col-2";

                    name.Text = productName;
                    price.Text = "R" + productPrice;
                    quanlbl.Text = quantity.ToString() + " X ";

                    //Build the first part of headPanel
                    Panel temp = new Panel();
                    temp.Controls.Add(quanlbl);
                    temp.Controls.Add(name);
                    temp.Controls.Add(price);
                    pnl1.Controls.Add(temp);

                    counterer++;
                }
            }
            return counterer;
        }
    }
}