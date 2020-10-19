using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Website.App_Code;

namespace Website
{
    public class OrderItems
    {
        /*
         * This class's purpose is to create and modify the order's items:
         * 1. It will act something like a subclass of the Order class
         *      where it will be used in conjunction with the Order class as a global var
         *      "storing" and modifying that specific order's items
         * 2. Technically doesn't store the products in the order, but 
         *      executes commands based on the order
         * 
         */

        private MySqlConnection conn;
        private int orderID;


        public OrderItems(int OrderID)
        {//Only constructor - needs the OrderID - will usually already be known if this class was to be used by Order class
            try
            {
                ConnectionClass connection = new ConnectionClass();//Class to connect to database
                conn = connection.getConnection();
                orderID = OrderID;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void addProduct(int productId, int quantity)
        {//The default adding of a product to be used...
         //It checks if the product already in order (if is, then add the quantity, else create new product in order)
            
            if (quantity < 1)
            {
                throw new Exception("Invalid quantity of products: " + quantity);
            }
            try
            {
                if (hasProduct(productId, orderID))
                {//Does the orderId have this item in the database? Yes:
                    try
                    {//Update the quantity
                        int currentQuantity = getQuantityOfAProduct(productId);
                        MySqlCommand c = new MySqlCommand();
                        c.Connection = conn;
                        c.CommandText =
                            "UPDATE `Order Menu Item link` " +
                            "SET Quantity=@qu " +
                            "WHERE `Order ID`=@oID " +
                            "AND `Menu-Item ID`=@mID;";
                        c.Parameters.AddWithValue("@mID", productId);
                        c.Parameters.AddWithValue("@oID", orderID);
                        c.Parameters.AddWithValue("@qu", quantity + currentQuantity);
                        executeNonQuery(c);
                    }
                    catch(Exception x)
                    {
                        throw x;
                    }
                }
                else
                {//No, it does not have product in the database
                    try
                    {//Make a new entry in the database for the product
                        MySqlCommand c = new MySqlCommand();
                        c.Connection = conn;
                        c.CommandText =
                            "INSERT INTO `Order Menu Item link` " +
                            "(`Menu-Item ID`, `Order ID`, `Quantity`) " +
                            "VALUES" +
                            "(@menuID, @orderID, @quan);";
                        c.Parameters.AddWithValue("@menuID", productId);
                        c.Parameters.AddWithValue("@orderID", orderID);
                        c.Parameters.AddWithValue("@quan", quantity);
                        executeNonQuery(c);
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public bool hasProduct(int productId, int orderId)
        {//Method that checks if `Order Menu Item link` has an orderId that has product: productId
            MySqlCommand c = new MySqlCommand();
            c.Connection = conn;
            c.CommandText =
                "SELECT * " +
                "FROM `Order Menu Item link` " +
                "WHERE `Order ID`=@oid " +
                "AND `Menu-Item ID`=@pid";
            c.Parameters.AddWithValue("@oid", orderId);
            c.Parameters.AddWithValue("@pid", productId);
            try
            {
                conn.Open();
                if (c.ExecuteScalar() != null)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch(Exception x)
            {
                return false;
            }
        }

        public void removeProduct(int productId, int howmany)
        {//Remove quantity of productId from order (if the order contains the product)
         //If quantity goes less than 0 (currentQuantity-howmany), delete the item from the order
            try
            {
                if(hasProduct(productId, orderID))
                {
                    int quantity = getQuantityOfAProduct(productId);

                    if (quantity - howmany > 0)
                    {
                        MySqlCommand c = new MySqlCommand();
                        c.Connection = conn;
                        c.CommandText =
                            "UPDATE `Order Menu Item link` " +
                            "SET Quantity=@quan " +
                            "WHERE `Order ID`=@orderID " +
                            "AND `Menu-Item ID`=@menuID;";
                        c.Parameters.AddWithValue("@menuID", productId);
                        c.Parameters.AddWithValue("@orderID", orderID);
                        c.Parameters.AddWithValue("@quan", (quantity - howmany));
                        executeNonQuery(c);
                    }
                    else
                    {
                        removeAllOfAProduct(productId);
                    }
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        private int getQuantityOfAProduct(int menuItemID)
        {//Get the quantity of a product in the order
            int quantity = 0;
            try
            {
                if(hasProduct(menuItemID, orderID))
                {
                    MySqlCommand checkQuantity = new MySqlCommand();
                    checkQuantity.CommandText =
                            "SELECT * " +
                            "FROM `Order Menu Item link` " +
                            "WHERE `Order ID`=@oid " +
                            "AND `Menu-Item ID`=@mid;";
                    checkQuantity.Connection = conn;
                    checkQuantity.Parameters.AddWithValue("@oid",orderID);
                    checkQuantity.Parameters.AddWithValue("@mid", menuItemID);

                    using(conn) //NOTE: idk why but executescalar did not want to work here - kept returning "Quantity" instead of the quantity value
                    {
                        conn.Open();
                        using (MySqlDataReader r = checkQuantity.ExecuteReader())
                        {
                            if (r.HasRows)
                            {
                                while (r.Read())
                                {
                                    quantity = int.Parse(r["Quantity"].ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            return quantity;
        }

        public void removeAllOfAProduct(int productId)
        {//delete product from order - deletes all quantity, unlike removeproduct
            try
            {
                if(hasProduct(productId, orderID))
                {
                    MySqlCommand comm = new MySqlCommand();
                    comm.CommandText =
                        "DELETE FROM `Order Menu Item link` " +
                        "WHERE `Menu-Item ID` = @mid " +
                        "AND `Order ID` = @oid;";
                    comm.Connection = conn;
                    comm.Parameters.AddWithValue("@mid", productId);
                    comm.Parameters.AddWithValue("@oid", orderID);
                    executeNonQuery(comm);
                }
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public void deleteAllProducts()
        {//Method to delete all of the products associated with the orderID in Order Menu Item link
            try
            {
                MySqlCommand comm = new MySqlCommand();
                comm.CommandText =
                    "DELETE " +
                    "FROM `Order Menu Item link`" +
                    "WHERE `Order ID`=" + orderID + ";";
                comm.Connection = conn;
                executeNonQuery(comm);
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        private void executeNonQuery(MySqlCommand que)
        {//Try and safely do the ExecuteNonQuery() on a command
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

        public DataTable getOrderItemsTable()
        {//Put all of the orderId's orderitems into a DataTable and return it
            MySqlCommand cmmd = new MySqlCommand();
            cmmd.CommandText =
                "SELECT * " +
                "FROM `Order Menu Item link`;";
            cmmd.Connection = conn;
            try
            {
                MySqlDataAdapter adap = new MySqlDataAdapter(cmmd);
                DataTable ds = new DataTable();
                adap.Fill(ds);

                return ds;
            }
            catch(Exception x)
            {
                throw x;
            }
        }
    }
}