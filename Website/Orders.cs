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
        }

        public Orders(MySqlConnection c, int customerID, int paid, DateTime date)
        {
            this.conn = c;
            tryConnect(conn);
            this.customerID = customerID;
            try
            {
                MySqlCommand cmmd = new MySqlCommand("INSERT INTO `Order`(`Customer ID`, `Paid`, `Date`) VALUES(@cid, @paid, @date);");
                cmmd.Connection = conn;
                cmmd.Parameters.AddWithValue("@cid", customerID);
                cmmd.Parameters.AddWithValue("@paid", paid);
                cmmd.Parameters.AddWithValue("@date", date);
                conn.Open();
                cmmd.ExecuteNonQuery();
                orderID = getLastID("Order", "Order ID");
            }
            catch (Exception o)
            {
                throw new Exception("Database Error : " + o.Message);
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
            int x = -1;
            MySqlCommand cmmd2 = new MySqlCommand("SELECT `@row` FROM `@tab` WHERE `@row2` = (SELECT LAST_INSERT_ID());");
            cmmd2.Connection = conn;
            cmmd2.Parameters.AddWithValue("@row", valInRow);
            cmmd2.Parameters.AddWithValue("@row2", valInRow);
            cmmd2.Parameters.AddWithValue("@tab", table);
            using(conn)
            {
                using (MySqlDataReader rdr = cmmd2.ExecuteReader())
                {
                    rdr.Read();
                    x = int.Parse(rdr[valInRow].ToString());
                }
            }
            
            return x;
        }

        public void addNewProduct(int productId, int quantity)
        {
            if(quantity < 1)
            {
                throw new Exception("Invalid quantity of products: " + quantity);
            }
            MySqlCommand c = new MySqlCommand();
            c.Connection = conn;
            c.CommandText = "INSERT INTO `Order Menu Item link`(`Menu-Item ID`, `Order ID`, Quantity) VALUES(@menuID, @orderID, @quan);";
            c.Parameters.AddWithValue("@menuID", productId);
            c.Parameters.AddWithValue("@orderID", orderID);
            c.Parameters.AddWithValue("@quan", quantity);
            executeNonQuery(c);
        }

        public void add1Product(int productID, int howmany)
        {
            int currentQuantity = getQuantityOfProducts("Order Menu Item link","Order ID","Menu-Item ID",orderID,productID);
            MySqlCommand c = new MySqlCommand();
            c.Connection = conn;
            c.CommandText = "UPDATE `Order Menu Item link` SET `Quantity`=@quan WHERE OrderID=`@orderID` AND `Menu-Item ID`=`@menuID`;";
            c.Parameters.AddWithValue("@menuID", productID);
            c.Parameters.AddWithValue("@orderID", orderID);
            c.Parameters.AddWithValue("@quan", howmany+currentQuantity);
            executeNonQuery(c);
        }

        public void remove1Product(int productId, int howmany)
        {
            try
            {
                int quantity = getQuantityOfProducts("Order Menu Item link", "Order ID", "Menu-Item ID", orderID, productId);

                if (quantity-howmany > 1)
                {
                    MySqlCommand c = new MySqlCommand();
                    c.Connection = conn;
                    c.CommandText = "UPDATE `Order Menu Item link` SET `Quantity`=@quan WHERE OrderID=`@orderID` AND `Menu-Item ID`=`@menuID`;";
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
            try
            {
                MySqlCommand checkQuantity = new MySqlCommand();
                checkQuantity.CommandText = "SELECT Quantity FROM @tname WHERE @col1 = @oid AND @col2 = @mid;";
                checkQuantity.Connection = conn;
                checkQuantity.Parameters.AddWithValue("@tname", ""+tableName);
                checkQuantity.Parameters.AddWithValue("@col1", ""+orderColName);
                checkQuantity.Parameters.AddWithValue("@oid", orderID);
                checkQuantity.Parameters.AddWithValue("@col2", ""+menuColName);
                checkQuantity.Parameters.AddWithValue("@mid", menuItemID);

                conn.Open();
                MySqlDataReader r = checkQuantity.ExecuteReader();
                r.Read();
                quantity = int.Parse(r["Quantity"].ToString());
                r.Close();
                conn.Close();
            }
            catch(Exception x)
            {
                throw new Exception(x.Message);
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

        private void delete(string table, string column, int whereID)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand();
                comm.CommandText = "DELETE FROM `@tid` WHERE `@whe` = `@oid`;";
                comm.Connection = conn;
                comm.Parameters.AddWithValue("@oid", whereID);
                comm.Parameters.AddWithValue("@tid", table);
                comm.Parameters.AddWithValue("@whe", column);
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
    }
}