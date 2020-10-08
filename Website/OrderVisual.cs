using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        private Panel headPanel;
        private float totalPrice;
        private Orders order;


        public OrderVisual(MySqlConnection c, int customerID, int orderID)
        {
            this.conn = c;
            this.customerID = customerID;
            this.orderID = orderID;

            headPanel = new Panel();
            headPanel.CssClass = "";

            order = new Orders(conn, orderID);
        }

        public void update()
        {
            headPanel.Controls.Clear();

            MySqlCommand cmmd = new MySqlCommand();
            cmmd.CommandText =
                "SELECT * " +
                "FROM `Order Menu-Item link` " +
                "WHERE `Order ID` = " + orderID + ";";
            cmmd.Connection = conn;
            
            using(conn)
            {
                float productPrice = 0;
                string productName = "-1";
                int productId = -1;
                int quantity = -1;

                using (MySqlDataReader rdr = cmmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        productId = int.Parse(rdr[1].ToString());
                        quantity = int.Parse(rdr[2].ToString());

                        MySqlCommand cm = new MySqlCommand();
                        cm.Connection = conn;
                        cm.CommandText =
                            "SELECT * " +
                            "FROM `Menu_Item` " +
                            "WHERE `Menu-Item ID` = " + productId;

                        using (MySqlDataReader rr = cm.ExecuteReader())
                        {
                            productPrice = float.Parse(rr[4].ToString());
                            productName = rr[3].ToString();
                        }

                        Button min = new Button();
                        Button plus = new Button();
                        Label name = new Label();
                        Label price = new Label();
                        this.totalPrice += productPrice;

                        min.ID = productId.ToString();
                        plus.ID = productId.ToString();
                        name.ID = productId.ToString();
                        price.ID = productId.ToString();

                        min.Click += new EventHandler(minBtnClicked);
                        plus.Click += new EventHandler(plusBtnClicked);

                        name.Text = productName;
                        price.Text = "R" + productPrice;
                        min.Text = "-";
                        plus.Text = "+";

                        headPanel.Controls.Add(min);
                        headPanel.Controls.Add(name);
                        headPanel.Controls.Add(price);
                        headPanel.Controls.Add(plus);
                    }

                    Button checkOut = new Button();
                    Label total = new Label();
                    checkOut.CssClass = "";
                    checkOut.Click += new EventHandler(checkoutBtnClicked);
                    total.Text = "R" + totalPrice.ToString();

                    headPanel.Controls.Add(checkOut);
                    headPanel.Controls.Add(total);
                }
            }
        }

        private void plusBtnClicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int Id = int.Parse(btn.ID);

            order.add1Product(Id, 1);
            update();
        }

        private void minBtnClicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int Id = int.Parse(btn.ID);

            order.remove1Product(Id, 1);
            update();
        }

        private void checkoutBtnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}