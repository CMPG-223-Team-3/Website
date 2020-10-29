using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Website.App_Code;

namespace Website
{
    public partial class WaiterOrders : System.Web.UI.Page
    {
        //private MySqlConnection connection;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack== false)
                this.BindGrid();
        }

        private void BindGrid()
        {
            DatabaseConnection connection4545 = new DatabaseConnection();
            MySqlConnection connection = connection4545.getConnection();
            using (connection)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Order_ID AS 'Order ID', Table_nr AS 'Table Number', Customer_Name AS 'Customer Name' FROM `ORDER` WHERE `Status` = 0 ORDER BY Order_Date_Time DESC; ");
                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
                    cmd.Connection = connection;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();

                        DropDownListOrderIds.Items.Clear();

                        foreach (DataRow r in dt.Rows)
                        {
                            DropDownListOrderIds.Items.Add(r["Order ID"].ToString());
                        }
                    }
                }
            }
            using (connection)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Order_ID AS 'Order ID', Table_nr AS 'Table Number', Customer_Name AS 'Customer Name' FROM `ORDER` WHERE `Paid` = 0 AND `Status` = 1 ORDER BY Order_Date_Time DESC;");
                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
                    cmd.Connection = connection;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridView2.DataSource = dt;
                        GridView2.DataBind();

                        DropDownListTableNumbers.Items.Clear();

                        foreach (DataRow r in dt.Rows)
                        {
                            DropDownListTableNumbers.Items.Add(r["Order ID"].ToString());
                        }
                    }
                }
            }
            using (connection)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Order_ID AS 'Order ID' FROM `ORDER` WHERE `Paid` = 0 ORDER BY Order_Date_Time DESC;");
                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
                    cmd.Connection = connection;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);

                        DropDownListOrder2.Items.Clear();

                        foreach (DataRow r in dt.Rows)
                        {
                            DropDownListOrder2.Items.Add(r["Order ID"].ToString());
                        }
                    }
                }
            }
        }

        private void ShowOrderDetail()
        {
            DatabaseConnection connection4545 = new DatabaseConnection();
            MySqlConnection connection = connection4545.getConnection();
            using (connection)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT `ORDER-DETAIL`.`Quantity_Ordered` AS 'Quantity', `MENU-ITEM`.`Item_Name` AS 'Product' " +
                                                    "FROM `ORDER-DETAIL` " +
                                                    "LEFT JOIN `MENU-ITEM` ON `MENU-ITEM`.`Menu_Item_ID` = `ORDER-DETAIL`.`Menu_Item_ID` "+
                                                    "WHERE `ORDER-DETAIL`.`Order_ID` = "+ DropDownListOrder2.SelectedItem.Text +";");


                /*SELECT `MENU - ITEM`.`Item_Name`, `ORDER - DETAIL`.`Quantity_Ordered`
                FROM `ORDER - DETAIL`
                LEFT JOIN `MENU - ITEM` ON `MENU - ITEM`.`Menu_Item_ID` = `ORDER - DETAIL`.`Menu_Item_ID`
                WHERE `ORDER - DETAIL`.`Order_ID` = 18;*/

                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
                    cmd.Connection = connection;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridView3.DataSource = dt;
                        GridView3.DataBind();
                    }
                }
            }
        }

        protected void ButtonPay_Click(object sender, EventArgs e)
        {
            string myQuery = "UPDATE `ORDER` SET `Paid` = 1 WHERE Order_ID = " + DropDownListTableNumbers.SelectedItem.Value + ";";

            DatabaseConnection connection4545 = new DatabaseConnection();
            MySqlConnection connection = connection4545.getConnection();
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(myQuery, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            this.BindGrid();
        }

        protected void BtnDelivered_Click(object sender, EventArgs e)
        {
            string myQuery = "UPDATE `ORDER` SET `Status` = 1 WHERE Order_ID = " + DropDownListOrderIds.SelectedItem.Value + ";";

            DatabaseConnection connection4545 = new DatabaseConnection();
            MySqlConnection connection = connection4545.getConnection();
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(myQuery, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            this.BindGrid();
        }

        protected void ButtonShowOrder_Click(object sender, EventArgs e)
        {
            this.ShowOrderDetail();
        }
    }
}