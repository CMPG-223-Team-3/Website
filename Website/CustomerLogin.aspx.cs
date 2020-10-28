using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Website.App_Code;

namespace Website
{
    public partial class CustomerLogin : System.Web.UI.Page
    {
        //session var names
        private static string userNameSession = "UserName";
        private static string errorSession = "Error";
        private static string fromPageSession = "FromPage";
        private static string orderIDSession = "OrderID";
        private static string tableIDSession = "TableID";
        private static string orderObjectSession = "OrderObject";
        private static string selectedWaiterIDSession = "WaiterID";

        private static string waiterWaiterIDName = "Waiter_ID";
        private static string waiterWaiterName = "Waiter_FirstName";
        private static string waiterWaiterLastName = "Waiter_LastName";


        private MySqlConnection conn;
        private string pageName = HttpContext.Current.Request.Url.AbsoluteUri; //Getting the pagename to store in session at page load so we can know which page to go back to after Error page is thrown

        protected void Page_Load(object sender, EventArgs e)
        {
            Session[fromPageSession] = pageName;
            try
            {
                DatabaseConnection connection = new DatabaseConnection(); //New connection object (See Connection.cs)
                conn = connection.getConnection();

                showAllWaiters();

            }
            catch (Exception x)
            {
                throwEx(x);
            }
        }

        protected int lastOrderID(int tableNr, string customerName)
        {
            int orderIdnum = -1;

            MySqlCommand comm = new MySqlCommand
            {
                Connection = conn,
                CommandText =
                "SELECT * " +
                "FROM `ORDER` " +
                "WHERE `Table_nr` = @tnr " +
                "AND `Customer_Name` = '" + customerName.ToUpper() + "' " +
                "AND `Paid` != 1"
            };
            comm.Parameters.AddWithValue("@tnr", tableNr);

            try
            {
                using (conn)
                {
                    conn.Open();
                    using (MySqlDataReader r = comm.ExecuteReader())
                    {//using a reader here cus i need the last value
                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                orderIdnum = int.Parse(r["Order_ID"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception xx)
            {
                throwEx(xx);
            }

            return orderIdnum;
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            Session[tableIDSession] = int.Parse(txtTable.Text);
            Session[userNameSession] = txtName.Text.Trim().ToUpper();
            Session[selectedWaiterIDSession] = RadioButtonList1.SelectedItem.Value;

            int ordNr = lastOrderID(int.Parse(txtTable.Text), txtName.Text);

            if (ordNr > 0)
            {//dunno how table nums work - rn can't be less than 1\
                //this what happens if an order found matching tableID & name entered
                Session[orderIDSession] = ordNr;
                Response.Redirect("IsThisYourOrder.aspx", false); //send the last order number matching to the entered table number and name to the page to check with them if that is their last order(given that they lost connection or something)
            }
            if (ordNr == -1)
            {//if no order was found, no worries, go to where the customer can order (already signed in)
                Response.Redirect("CustomerOrder.aspx", false);
            }
            Response.Write("<script>alert('We're having trouble with the entered table number or order)</script>");
        }

        private void throwEx(Exception x)
        {
            Session[errorSession] = x.Message + x.StackTrace;
            HttpContext.Current.Response.Redirect("Error.aspx", false);
        }

        public void showAllWaiters()
        {
            MySqlCommand comm = new MySqlCommand
            {
                Connection = conn,
                CommandText =
                "SELECT * " +
                "FROM `WAITER`"
            };

            int waiterID;
            string waiterName;
            string waiterLastName;

            try
            {
                using (conn)
                {
                    conn.Open();
                    using (MySqlDataReader r = comm.ExecuteReader())
                    {//using a reader here cus i need the last value
                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                if (r[waiterWaiterIDName] != System.DBNull.Value)
                                {
                                    waiterID = int.Parse(r[waiterWaiterIDName].ToString());
                                    if(r[waiterWaiterName] != System.DBNull.Value)
                                    {
                                        waiterName = r[waiterWaiterName].ToString();
                                        if (r[waiterWaiterLastName] != System.DBNull.Value)
                                        {
                                            waiterLastName = r[waiterWaiterLastName].ToString();
                                            /*RadioButton i = new RadioButton();
                                            i.Text = waiterName + " " + waiterLastName;
                                            i.ID = waiterID.ToString();*/
                                            ListItem i = new ListItem();
                                            i.Value = waiterID.ToString();
                                            i.Text = waiterName + " " + waiterLastName;
                                            RadioButtonList1.Items.Add(i);
                                            
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if(RadioButtonList1.Items.Count <= 0)
                {
                    throwEx(new Exception("I can't find any waiters in the database..."));
                }
                else
                {
                    RadioButtonList1.Items[0].Selected = true;
                }
            }
            catch (Exception xx)
            {
                throwEx(xx);
            }
        }
    }
}