using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


/*NOTES:
 * ExecuteScalar() only returns the value from the first column of first row of your query
 * ExecuteReader() returns an object that can iterate over the entire result set
 * ExecuteNonQuery() returns how many rows have been affected
 */

namespace Website.App_Code
{
    public class Order
    {
        /*
         * This class's purpose is to give support to the pages in the form in that it:
         * 1. Creates and modifies the order in the database in different ways
         * 2. It creates and uses the OrderItems class to modify the order's items
         * 3. It is also supposed to get the order information
         */

        //The global vars for the specific instance of the order
        private int tableID;
        private MySqlConnection conn;
        private int orderID;
        private int orderPaid;
        private int orderStatus;
        private OrderItems orderItems;

        private string tabName = "ORDER";
        private string orderIDName = "Order_ID";
        private string tableIDName = "Table_nr";
        private string paidName = "Paid";
        private string statusName = "Status";
        private string orderCustomerName = "Customer_Name";
        private string orderWaiterName = "Waiter_ID";
        private string orderCashOrCardName = "CashOrCard";

        private int orderWaiter;
        private int orderTable;
        private string orderName;
        private int orderCashOrCard;

        public Order()
        {
            try
            {
                doTheOrder("", 0, 0, 0, 0, 0);
            }
            catch(Exception c)
            {
                throw c;
            }
        }


        public Order(int orderId)
        {//Constructor for when you already know the order id, get the rest
            try
            {
                orderItems = new OrderItems(orderId);//Object for all the items in the database for this order
                DatabaseConnection connect = new DatabaseConnection();//Class to connect to database
                conn = connect.getConnection();
                this.orderID = orderId;
                if (!getOrderInfo(orderId))//Go get orderID's info and store it in global vars
                {
                    throw new Exception("Could not get order info from getOrderInfo()");
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public Order(string name, int tabID, int paid, int status, int waiter, int cashorcard)//Parameters of the customer so new order can be made
        {//Commonly the one you'd use for making a new order (no orderID)
            try
            {
                doTheOrder(name, tabID, paid, status, waiter, cashorcard);
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        private void doTheOrder(string name, int tabID, int paid, int status, int waiter, int cashorcard)//Parameters of the customer so new order can be made
        {//Commonly the one you'd use for making a new order (no orderID)
            try
            {
                DatabaseConnection connect = new DatabaseConnection();//Class to connect to database
                conn = connect.getConnection();
            }
            catch (Exception x)
            {
                throw x;
            }
            try
            {
                MySqlCommand cmmd = new MySqlCommand();
                cmmd.CommandText =
                    "INSERT " +
                    "INTO `ORDER`" +
                    "(`Customer_Name`, `Table_nr`, `Paid`, `Status`, `Waiter_ID`, `CashOrCard`) " +
                    "VALUES(@nm, @cid, @pd, @st, @wid, @ccc);";
                cmmd.Connection = conn;
                cmmd.Parameters.AddWithValue("@nm", name.ToUpper());
                cmmd.Parameters.AddWithValue("@cid", tabID);
                cmmd.Parameters.AddWithValue("@pd", paid);
                cmmd.Parameters.AddWithValue("@st", status);
                cmmd.Parameters.AddWithValue("@wid", waiter);
                cmmd.Parameters.AddWithValue("@ccc", cashorcard);

                executeNonQuery(cmmd);
                orderID = getLastID();
                orderItems = new OrderItems(orderID);//Object for all the items in the database for this order

                orderName = name.ToUpper();
                orderPaid = paid;
                tableID = tabID;
                orderStatus = status;
                orderWaiter = waiter;
                orderCashOrCard = cashorcard;
            }
            catch (Exception o)
            {
                throw o;
            }
        }

        private bool getOrderInfo(int orderId)
        {//method to get the orderID's tableID, orderPaid, and orderStatus and
            //assign them to the global vars
            //NOTE: Exception handling returns false, instead of an exception

            bool success = false;
            MySqlCommand cmmd2 = new MySqlCommand();
            cmmd2.CommandText =
                "SELECT * " +
                "FROM `ORDER` " +
                "WHERE `Order_ID` = @oid;";
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
                                if (rd[tableIDName] != System.DBNull.Value)
                                    tableID = int.Parse(rd[tableIDName].ToString());
                                if (rd[paidName] != System.DBNull.Value)
                                    orderPaid = int.Parse(rd[paidName].ToString());
                                if (rd[statusName] != System.DBNull.Value)
                                    orderStatus = int.Parse(rd[statusName].ToString());
                                if (rd[orderCustomerName] != System.DBNull.Value)
                                    orderName = rd[orderCustomerName].ToString();
                                if (rd[orderWaiterName] != System.DBNull.Value)
                                    orderWaiter = int.Parse(rd[orderWaiterName].ToString());
                                if (rd[orderCashOrCardName] != System.DBNull.Value)
                                    orderCashOrCard = int.Parse(rd[orderCashOrCardName].ToString());
                            }
                            success = true;
                        }
                    }
                }
                return success;
            }
            catch (Exception x)
            {
                throw x;
                //return false;
            }
        }

        private int getLastID()
        {//Method for getting the last inserted OrderID from the database so we can use it as the Order object
            int x = -1;
            MySqlCommand cmmd2 = new MySqlCommand();
            cmmd2.CommandText =
                "SELECT `Order_ID` " +
                "FROM `ORDER` " +
                "WHERE `Order_ID` = (SELECT LAST_INSERT_ID());";
            cmmd2.Connection = conn;

            try
            {
                using (conn)
                {
                    conn.Open();
                    x = int.Parse(cmmd2.ExecuteScalar().ToString());
                }
            }
            catch (Exception p)
            {
                throw p;
            }
            return x;
        }

        public void executeNonQuery(MySqlCommand que)
        {//Method to safely try do the ExecuteNonQuery() on a command
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

        public bool updateOrderNameAndTable(string name, int table)
        {
            try
            {
                MySqlCommand i = new MySqlCommand
                {
                    Connection = conn,
                    CommandText =
                    "UPDATE ORDER " +
                    "SET Customer_Name = @cus ," +
                    "Table_nr = @tab " +
                    "WHERE Order_ID = @oid"
                };
                i.Parameters.AddWithValue("@cus", name.ToUpper());
                i.Parameters.AddWithValue("@oid", orderID);
                i.Parameters.AddWithValue("@tab", table);

                conn.Open();
                if (i.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return true;
                }
                conn.Close();
                orderName = name.ToUpper();
                tableID = table;
                return false;
            }
            catch (Exception x)
            {
                return false;
            }
        }


        public bool updateOrderName(string name)
        {
            try
            {
                MySqlCommand i = new MySqlCommand
                {
                    Connection = conn,
                    CommandText =
                    "UPDATE ORDER " +
                    "SET Customer_Name = @cus " +
                    "WHERE Order_ID = @oid"
                };
                i.Parameters.AddWithValue("@cus", name.ToUpper());
                i.Parameters.AddWithValue("@oid", orderID);

                conn.Open();
                if (i.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return true;
                }
                conn.Close();
                orderName = name.ToUpper();
                return false;
            }
            catch (Exception x)
            {
                return false;
            }
        }

        public bool updateOrderTable(int table)
        {
            try
            {
                MySqlCommand i = new MySqlCommand
                {
                    Connection = conn,
                    CommandText =
                    "UPDATE ORDER " +
                    "SET Table_nr = @cus " +
                    "WHERE Order_ID = @oid "
                };
                i.Parameters.AddWithValue("@cus", table);
                i.Parameters.AddWithValue("@oid", orderID);

                conn.Open();
                if (i.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return true;
                }
                conn.Close();
                tableID = table;
                return false;

            }
            catch (Exception x)
            {
                return false;
            }
        }

        public bool updateOrderWaiter(int waiterID)
        {
            try
            {
                MySqlCommand i = new MySqlCommand
                {
                    Connection = conn,
                    CommandText =
                    "UPDATE ORDER " +
                    "SET `Waiter_ID` =" + waiterID + " "+
                    "WHERE `Order_ID` =" + orderID + " "
                };
                //i.Parameters.AddWithValue("@cus", waiterID);
                //i.Parameters.AddWithValue("@oid", orderID);

                conn.Open();
                if (i.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return true;
                }
                conn.Close();
                orderWaiter = waiterID;
                return false;

            }
            catch (Exception x)
            {
                return false;
            }
        }

        public bool updateOrderCashOrCard(int cashorcard)
        {
            try
            {
                MySqlCommand i = new MySqlCommand
                {
                    Connection = conn,
                    CommandText =
                    "UPDATE ORDER " +
                    "SET CashOrCard = @cus " +
                    "WHERE Order_ID = @oid "
                };
                i.Parameters.AddWithValue("@cus", cashorcard);
                i.Parameters.AddWithValue("@oid", orderID);

                conn.Open();
                if (i.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return true;
                }
                conn.Close();
                orderCashOrCard = cashorcard;
                return false;

            }
            catch (Exception x)
            {
                return false;
            }
        }

        public bool updateOrderStatus(int status)
        {
            try
            {
                MySqlCommand i = new MySqlCommand
                {
                    Connection = conn,
                    CommandText =
                    "UPDATE ORDER " +
                    "SET Status = @cus " +
                    "WHERE Order_ID = @oid "
                };
                i.Parameters.AddWithValue("@cus", status);
                i.Parameters.AddWithValue("@oid", orderID);

                conn.Open();
                if (i.ExecuteNonQuery() > 0)
                {
                    conn.Close();
                    return true;
                }
                conn.Close();
                orderStatus = status;
                return false;

            }
            catch (Exception x)
            {
                return false;
            }
        }


        public int getTableID()
        {
            if (tableID == 0)
            {
                throw new Exception("User has no table yet");
            }
            return tableID;
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
                throw new NotImplementedException("Has not checked if order is paid");//The global var is null
        }
        public OrderItems getOrderItemsObject()
        {
            return orderItems;//Return the OrderItems object (Will probably be used to add, remove and such from the items in the order)
                              //for example: Order.getOrderItemsObject.addProduct(productId);
        }
        public string getStatusString()
        {
            if(orderStatus == 0)
            {
                return "Pending";
            }
            else if(orderStatus == 1)
            {
                return "Delivered";
            }
            else
            {
                return "Status not found";
            }
        }
        public string getPaidString()
        {
            if (orderPaid == 0)
            {
                return "Pending payment";
            }
            else if (orderPaid == 1)
            {
                return "Paid";
            }
            else
            {
                return "Payment status not found";
            }
        }
        public string getCashOrCardString()
        {
            if (orderCashOrCard == 0)
            {
                return "Cash";
            }
            else if (orderCashOrCard == 1)
            {
                return "Card";
            }
            else
            {
                return "Selection not found";
            }
        }
        public int getTable()
        {
            return orderTable;
        }
        public string getWaiter()
        {
            string name = null;
            try
            {
                MySqlCommand waitcomm = new MySqlCommand();
                waitcomm.Connection = conn;
                waitcomm.CommandText =
                    "SELECT * " +
                    "FROM `WAITER` " +
                    "WHERE `Waiter_ID` = @id";
                waitcomm.Parameters.AddWithValue("@id", orderWaiter);
                using(conn)
                {
                    conn.Open();
                    using (MySqlDataReader rdr = waitcomm.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                if (rdr["Waiter_FirstName"] != System.DBNull.Value)
                                {
                                    name = rdr["Waiter_FirstName"].ToString();
                                }
                            }
                        }
                    }
                }

                if (name != null)
                    return name;
                else
                    throw new Exception();
            }
            catch(Exception x)
            {
                return "Can't find waiter";
            }
            
        }
    }
}