using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Website
{ 
    public class OrderVisual
    {
        private MySqlConnection conn;
        private int customerID;
        private int orderID;
        private Panel headPanel; //Main panel to put each orderpanel into with the information
        private float totalPrice;
        private Orders order;
        private DataTable menuItems;
        private DataTable orderItems;

        Button min;
        Button plus;
        Label name;
        Label price;
        Label quanlbl;


        public OrderVisual(MySqlConnection c, int customerID, int orderID)
        {
            this.conn = c;
            this.customerID = customerID;
            this.orderID = orderID;

            headPanel = new Panel();
            headPanel.CssClass = "row";

            order = new Orders(conn, orderID);

            getMenuItems();
            getOrderItems();
            update();
        }

        public OrderVisual(Orders ord, int customerID)
        {
            this.conn = ord.getConnection();
            this.customerID = ord.getCustomerID();
            this.orderID = ord.getOrderID();

            headPanel = new Panel();
            headPanel.CssClass = "row";

            order = ord;

            getMenuItems();
            getOrderItems();
            update();
        }

        public void getOrderItems()
        {
            MySqlCommand cmmd = new MySqlCommand();
            cmmd.CommandText =
                "SELECT * " +
                "FROM `Order Menu Item link`;";
            cmmd.Connection = conn;
            MySqlDataAdapter adap = new MySqlDataAdapter(cmmd);
            DataTable ds = new DataTable();
            adap.Fill(ds);

            orderItems = ds;
        }

        public void getMenuItems()
        {
            MySqlCommand cm = new MySqlCommand();
            cm.Connection = conn;
            cm.CommandText =
                "SELECT * " +
                "FROM `Menu_Item`";
            MySqlDataAdapter adap = new MySqlDataAdapter(cm);
            DataTable ds = new DataTable();
            adap.Fill(ds);

            menuItems = ds;
        }

        public void update()
        {
            //It's basically a reload method for every product in the order table

            getOrderItems();
            headPanel.Controls.Clear();
            totalPrice = 0;

            float productPrice = 0;
            string productName = "-1";
            int productId = -1;
            int quantity = -1;
            int counterer = 0;

            //PROBLEM: I could not get it to work by having 2 readers on the same connection - I had a reader in a reader, but it should work now

            //bool isFound2 = false;
            foreach (DataRow datr in orderItems.Rows)
            {
                if (datr["Order ID"].ToString() == orderID.ToString())
                {
                    productId = int.Parse(datr["Menu-Item ID"].ToString());
                    quantity = int.Parse(datr["Quantity"].ToString());

                    bool isFound3 = false;
                    foreach (DataRow dr in menuItems.Rows)
                    {
                        if (dr["Menu-Item ID"].ToString() == productId.ToString())
                        {
                            productPrice = float.Parse(dr["Price"].ToString());
                            productName = dr["Name"].ToString();
                            isFound3 = true;
                        }
                        if(isFound3)
                        {
                            break;
                        }
                    }
                    if(!isFound3)
                    {
                        throw new Exception("Could not find product: " + productId + " in database");
                    }

                    min = new Button();
                    plus = new Button();
                    name = new Label();
                    price = new Label();
                    quanlbl = new Label();
                    this.totalPrice += productPrice*quantity;

                    min.CssClass = "btn btn-dark col-3";
                    plus.CssClass = "btn btn-dark col-3";
                    price.CssClass = "col-2";
                    quanlbl.CssClass = "col-2";
                    name.CssClass = "col-2";

                    //Putting the id of the product as the buttons' ids for the eventhandler so it distinguishes what button was pressed
                    min.ID = productId.ToString() + "_min_" + counterer;
                    plus.ID = productId.ToString() + "_plus_" + counterer;

                    //Eventhandlers for each button
                    min.Click += new EventHandler(minBtnClicked);
                    plus.Click += new EventHandler(plusBtnClicked);

                    min.CausesValidation = false;
                    plus.CausesValidation = false;

                    name.Text = productName;
                    price.Text = "R" + productPrice;
                    min.Text = "-";
                    plus.Text = "+";
                    quanlbl.Text = quantity.ToString() + " X ";

                    headPanel.Controls.Add(min);
                    headPanel.Controls.Add(quanlbl);
                    headPanel.Controls.Add(name);
                    headPanel.Controls.Add(price);
                    headPanel.Controls.Add(plus);

                    counterer++;
                    //isFound2 = true;
                }
            }
            /*if (isFound2 == false)
            {
                throw new Exception("Order ID: " + orderID + " ");
            }*/

            Button checkOut = new Button();
            Label total = new Label();
            checkOut.Text = "Checkout";
            checkOut.CssClass = "btn btn-dark";
            checkOut.Click += new EventHandler(checkoutBtnClicked);
            total.Text = "R" + totalPrice.ToString();
            total.CssClass = "";

            headPanel.Controls.Add(checkOut);
            headPanel.Controls.Add(total);
        }

        private void plusBtnClicked(object sender, EventArgs e)
        {
            //when the plus btn is clicked on a product

            //get the button id which is set to the id of the product in update() that needs to be added 
            Button btn = sender as Button;
            string[] i = btn.ID.Split('_');
            int Id = int.Parse(i[0]);

            order.add1Product(Id, 1);
            update();
        }

        private void minBtnClicked(object sender, EventArgs e)
        {
            //when the minus button is clicked on a product

            //get the button id which is set to the id of the product in update() that needs to be added 
            Button btn = sender as Button;
            string[] i = btn.ID.Split('_');
            int Id = int.Parse(i[0]);

            order.remove1Product(Id, 1);
            update();
        }

        private void checkoutBtnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();

        }


        public Panel getHeadPanel()
        {
            return headPanel;
        }
    }
}