using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


/*NOTES:
 * ExecuteScalar() only returns the value from the first column of first row of your query
 * ExecuteReader() returns an object that can iterate over the entire result set
 * ExecuteNonQuery() returns no value
 */

namespace Website.App_Code
{
    public class Order
    {
        /*
         * This class's purpose is to give support to the pages in the form that it creates
         * and modifies the order in the database in different ways
         * 
         * It creates and uses the OrderItems class to modify the order's items
         * 
         * It is also supposed to get the order information
         */

        private int customerID;
        private MySqlConnection conn;
        private int orderID;
        private int orderPaid;
        private int orderStatus;
        private OrderItems orderItems;

        private string tabName = "Order";
        private string orderIDName = "Order ID";
        private string customerIDName = "Customer ID";
        private string paidName = "Paid";
        private string statusName = "Status";

        public Order(int orderId)
        {//When you already know the order id, get the rest
            try
            {
                orderItems = new OrderItems(orderId);
                ConnectionClass connect = new ConnectionClass();
                conn = connect.getConnection();
                this.orderID = orderId;
                getOrderInfo(orderId);
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public Order(int customerID, int paid, int status)
        {//Commonly the one you'd use making a new order
            try
            {
                ConnectionClass connect = new ConnectionClass();
                conn = connect.getConnection();
            }
            catch(Exception x)
            {
                throw x;
            }
            

            this.customerID = customerID;
            try
            {
                MySqlCommand cmmd = new MySqlCommand();
                cmmd.CommandText =
                    "INSERT " +
                    "INTO `Order`" +
                    "(`Customer ID`, `Paid`, `Status`) " +
                    "VALUES(@cid, @pd, @st);";
                cmmd.Connection = conn;
                cmmd.Parameters.AddWithValue("@cid",customerID);
                cmmd.Parameters.AddWithValue("@pd", paid);
                cmmd.Parameters.AddWithValue("@st",status);
                
                executeNonQuery(cmmd);
                orderID = getLastID();
                orderItems = new OrderItems(orderID);
            }
            catch (Exception o)
            {
                throw o;
            }
        }

        private bool getOrderInfo(int orderId)
        {
            bool success = false;
            MySqlCommand cmmd2 = new MySqlCommand();
            cmmd2.CommandText =
                "SELECT * " +
                "FROM `Order` " +
                "WHERE `Order ID` = @oid;";
            cmmd2.Parameters.AddWithValue("@oid", orderId);
            cmmd2.Connection = conn;

            try
            {
                using (conn)
                {
                    conn.Open();
                    using (MySqlDataReader rd = cmmd2.ExecuteReader())
                    {
                        if (rd.HasRows)
                        {
                            while (rd.Read())
                            {
                                customerID = int.Parse(rd[customerIDName].ToString());
                                orderPaid = int.Parse(rd[paidName].ToString());
                                orderStatus = int.Parse(rd[statusName].ToString());
                            }
                            success = true;
                        }
                    }
                }
                return success;
            }
            catch(Exception z)
            {
                throw z;
            }
        }

        private int getLastID()
        {
            int x = -1;
            MySqlCommand cmmd2 = new MySqlCommand();
            cmmd2.CommandText =
                "SELECT `Order ID` " +
                "FROM `Order` " +
                "WHERE `Order ID` = (SELECT LAST_INSERT_ID());";
            cmmd2.Connection = conn;

            try
            {
                using (conn)
                {
                    conn.Open();
                    x = int.Parse(cmmd2.ExecuteScalar().ToString());
                }
            }
            catch(Exception p)
            {
                throw p;
            }
            return x;
        }

        public void OrderPaid()
        {
            throw new NotImplementedException();
        }

        public void executeNonQuery(MySqlCommand que)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    que.ExecuteNonQuery();
                }
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        public int getCustomerID()
        {
            return customerID;
        }
        public MySqlConnection getConnection()
        {
            return conn;
        }
        public int getOrderID()
        {
            return orderID;
        }
        public bool isPaid()
        {
            if (orderPaid == 1)
                return true;
            else if (orderPaid == 0)
                return false;
            else
                throw new NotImplementedException("Has not checked if order is paid");
        }
        public OrderItems getOrderItemsObject()
        {
            return orderItems;
        }
    }
}