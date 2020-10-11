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


        public OrderVisual(MySqlConnection c, int customerID, int orderID)
        {
            this.conn = c;
            this.customerID = customerID;
            this.orderID = orderID;

            headPanel = new Panel();
            headPanel.CssClass = "row";

            order = new Orders(conn, orderID);

            getMenuItems();
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
            update();
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

            headPanel.Controls.Clear();

            MySqlCommand cmmd = new MySqlCommand();
            cmmd.CommandText =
                "SELECT * " +
                "FROM `Order Menu Item link` " +
                "WHERE `Order ID` = " + orderID + ";";
            cmmd.Connection = conn;

            float productPrice = 0;
            string productName = "-1";
            int productId = -1;
            int quantity = -1;
            int[] arr = new int[1000];
            int[] arr2 = new int[1000];
            int counterer = 0;
            Button min;
            Button plus;
            Label name;
            Label price;
            Label quanlbl;

            //PROBLEM: I could not get it to work by having 2 readers on the same connection - I had a reader in a reader, but it should work now

            using (conn)
            {
                conn.Open();
                using(MySqlDataReader rdr = cmmd.ExecuteReader())
                {
                    while(rdr.Read())
                    {
                        productId = int.Parse(rdr["Menu-Item ID"].ToString());
                        quantity = int.Parse(rdr["Quantity"].ToString());

                        arr[counterer] = productId;
                        arr2[counterer] = quantity;
                        counterer++;
                    }
                }

                

                using (MySqlDataReader rr = cm.ExecuteReader())
                {
                    while (rr.Read())
                    {
                        productPrice = float.Parse(rr["Price"].ToString());
                        productName = rr["Name"].ToString();
                    }
                }

                for (int i = 0; i < counterer; i++)
                {
                    MySqlCommand cm = new MySqlCommand();
                    cm.Connection = conn;
                    cm.CommandText =
                        "SELECT * " +
                        "FROM `Menu_Item` " +
                        "WHERE `Menu-Item ID` = " + arr[i];

                    using (MySqlDataReader rr = cm.ExecuteReader())
                    {
                        while (rr.Read())
                        {
                            productPrice = float.Parse(rr["Price"].ToString());
                            productName = rr["Name"].ToString();
                        }
                    }

                    /*
                    bool isFound = false;
                    foreach(DataRow dr in menuItems.Rows)
                    {
                        if(dr["Menu-Item ID"].ToString() == arr[1].ToString())
                        {
                            productPrice = float.Parse(dr["Price"].ToString());
                            productName = dr["Name"].ToString();
                            isFound = true;
                        }
                        if(isFound)
                        {
                            break;
                        }
                    }
                    */

                    min = new Button();
                    plus = new Button();
                    name = new Label();
                    price = new Label();
                    quanlbl = new Label();
                    this.totalPrice += productPrice;

                    min.CssClass = "btn btn-dark col-3";
                    plus.CssClass = "btn btn-dark col-3";
                    price.CssClass = "col-2";
                    quanlbl.CssClass = "col-2";
                    name.CssClass = "col-2";

                    //Putting the id of the product as the buttons' ids for the eventhandler so it distinguishes what button was pressed
                    min.ID = arr[i].ToString() + "_min_" + i;
                    plus.ID = arr[i].ToString() + "_plus_" + i;

                    //Eventhandlers for each button
                    min.Click += new EventHandler(minBtnClicked);
                    plus.Click += new EventHandler(plusBtnClicked);

                    name.Text = productName;
                    price.Text = "R" + productPrice;
                    min.Text = "-";
                    plus.Text = "+";
                    quanlbl.Text = arr2[i].ToString() + " quan";

                    headPanel.Controls.Add(min);
                    headPanel.Controls.Add(quanlbl);
                    headPanel.Controls.Add(name);
                    headPanel.Controls.Add(price);
                    headPanel.Controls.Add(plus);
                }
            }

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