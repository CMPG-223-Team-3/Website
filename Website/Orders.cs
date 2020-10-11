using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/*NOTES:
 * ExecuteScalar() only returns the value from the first column of first row of your query
 * ExecuteReader() returns an object that can iterate over the entire result set
 * ExecuteNonQuery() returns no value
 */

namespace Website
{
    public class Orders
    {
        private int customerID;
        private MySqlConnection conn;
        private int orderID;
        

        public Orders(MySqlConnection c, int orderId)
        {
            this.conn = c;
            tryConnect(conn);
            this.orderID = orderId;
            getOrderInfo(orderId);
        }

        public Orders(MySqlConnection c, int customerID, int paid)
        {
            this.conn = c;
            tryConnect(conn);
            this.customerID = customerID;
            try
            {
                MySqlCommand cmmd = new MySqlCommand();
                cmmd.CommandText =
                    "INSERT " +
                    "INTO `Order`" +
                    "(`Customer ID`, `Paid`) " +
                    "VALUES(" + customerID + "," + paid + ");";
                cmmd.Connection = conn;
                executeNonQuery(cmmd);
                orderID = getLastID("Order", "Order ID");
            }
            catch (Exception o)
            {
                throw new Exception("Database Error : " + o.Message);
            }
        }

        private void getOrderInfo(int orderId)
        {
            MySqlCommand cmmd2 = new MySqlCommand();
            cmmd2.CommandText =
                "SELECT * " +
                "FROM `Order` " +
                "WHERE `Order ID` = " + orderId + ";";
            cmmd2.Connection = conn;

            using (conn)
            {
                conn.Open();
                customerID = int.Parse(cmmd2.ExecuteScalar().ToString());
            }
        }

        public void tryConnect(MySqlConnection i)
        {
            using(i)
            {
                i.Open();
            }
        }

        public int getLastID(string table, string valInRow)
        {
            //This may not be functioning
            int x = -1;
            MySqlCommand cmmd2 = new MySqlCommand();
            cmmd2.CommandText =
                "SELECT `" + valInRow + "` " +
                "FROM `" + table + "` " +
                "WHERE `" + valInRow + "` = (SELECT LAST_INSERT_ID());";
            cmmd2.Connection = conn;
            
            using(conn)
            {
                conn.Open();
                x = int.Parse(cmmd2.ExecuteScalar().ToString());
            }
            
            return x;
        }

        public void addNewProduct(int productId, int quantity)
        {
            if(quantity < 1)
            {
                throw new Exception("Invalid quantity of products: " + quantity);
            }
            if (hasProduct(productId, orderID))
            {
                add1Product(productId, quantity);
            }
            else
            {
                MySqlCommand c = new MySqlCommand();
                c.Connection = conn;
                c.CommandText = "INSERT INTO `Order Menu Item link`(`Menu-Item ID`, `Order ID`, Quantity) VALUES(@menuID, @orderID, @quan);";
                c.Parameters.AddWithValue("@menuID", productId);
                c.Parameters.AddWithValue("@orderID", orderID);
                c.Parameters.AddWithValue("@quan", quantity);
                executeNonQuery(c);
            }
            
        }

        public void add1Product(int productID, int howmany)
        {
            int currentQuantity = getQuantityOfProducts("Order Menu Item link","Order ID","Menu-Item ID",orderID,productID);
            MySqlCommand c = new MySqlCommand();
            c.Connection = conn;
            c.CommandText = "UPDATE `Order Menu Item link` SET `Quantity`=@quan WHERE `Order ID`=@orderID AND `Menu-Item ID`=@menuID;";
            c.Parameters.AddWithValue("@menuID", productID);
            c.Parameters.AddWithValue("@orderID", orderID);
            c.Parameters.AddWithValue("@quan", howmany+currentQuantity);
            executeNonQuery(c);
        }

        public bool hasProduct(int productId, int orderId)
        {
            MySqlCommand c = new MySqlCommand();
            c.Connection = conn;
            c.CommandText =
                "SELECT * " +
                "FROM `Order Menu Item link` " +
                "WHERE `Order ID` = " + orderId + " " +
                "AND `Menu-Item ID` = " + productId + ";";
            using(conn)
            {
                conn.Open();
                if(c.ExecuteScalar() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void remove1Product(int productId, int howmany)
        {
            try
            {
                int quantity = getQuantityOfProducts("Order Menu Item link", "Order ID", "Menu-Item ID", orderID, productId);

                if (quantity-howmany > 0)
                {
                    MySqlCommand c = new MySqlCommand();
                    c.Connection = conn;
                    c.CommandText = "UPDATE `Order Menu Item link` SET `Quantity`=@quan WHERE `Order ID`=@orderID AND `Menu-Item ID`=@menuID;";
                    c.Parameters.AddWithValue("@menuID", productId);
                    c.Parameters.AddWithValue("@orderID", orderID);
                    c.Parameters.AddWithValue("@quan", quantity - howmany);
                    executeNonQuery(c);
                }
                else
                {
                    deleteProduct(productId);
                }
            }
            catch(Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        private int getQuantityOfProducts(string tableName, string orderColName, string menuColName, int orderID, int menuItemID)
        {
            int quantity = -1;
            MySqlCommand checkQuantity = new MySqlCommand();
            try
            {
                //MySqlCommand checkQuantity = new MySqlCommand();
                checkQuantity.CommandText = 
                    "SELECT Quantity " +
                    "FROM `" + tableName + "` " +
                    "WHERE `" + orderColName + "`=" + orderID + " " +
                    "AND `" + menuColName +"`=" + menuItemID + ";";
                checkQuantity.Connection = conn;

                conn.Open();
                quantity = int.Parse(checkQuantity.ExecuteScalar().ToString());
                conn.Close();
            }
            catch(Exception x)
            {
                throw new Exception(x.Message + "   " + checkQuantity.CommandText.ToString()) ;
            }
            return quantity;
        }

        public void deleteProduct(int productId)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "DELETE FROM `Order Menu Item link` WHERE `Menu-Item ID` = @mid AND `Order ID` = @oid;";
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@mid", productId);
            comm.Parameters.AddWithValue("@oid", orderID);
            executeNonQuery(comm);
        }

        public void deleteAllProducts()
        {
            delete("Order Menu Item link", "Order ID", orderID);
        }

        public void deleteOrder()
        {
            deleteAllProducts();
            delete("Order", "Order ID", orderID);
        }

        private void delete(string table, string IDcolumn, int ID)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand();
                comm.CommandText = 
                    "DELETE " +
                    "FROM `" + table + "`" +
                    "WHERE `" + IDcolumn + "`=" + ID + ";";
                comm.Connection = conn;
                executeNonQuery(comm);
            }
            catch(Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        private void executeNonQuery(MySqlCommand que)
        {
            try
            {
                using(conn)
                {
                    conn.Open();
                    que.ExecuteNonQuery();
                }
            }
            catch(Exception x)
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
    }
}