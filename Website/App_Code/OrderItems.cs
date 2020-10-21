using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Website.App_Code
{
    public class OrderItems
    {
        /*
         * This class's purpose is to create and modify the order's items:
         * 1. It will act something like a subclass of the Order class
         *      where it will be used in conjunction with the Order class as a global var
         *      "storing" and modifying that specific order's items
         * 
         */

        private string MenuIDCol = "Menu_Item_ID";
        private string OrderIDCol = "Order_ID";
        private string QuantityCol = "Quantity_Ordered";

        private static MySqlConnection conn;
        private int orderID;
        private static DataTable orderItems;
        private static MySqlCommandBuilder build;
        private static MySqlDataAdapter adap;

        public OrderItems(int OrderID)
        {//Only constructor - needs the OrderID - will usually already be known if this class was to be used by Order class
            try
            {
                ConnectionClass connection = new ConnectionClass();//Class to connect to database
                conn = connection.getConnection();
                orderID = OrderID;
                getOrderItemsTable();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public DataTable getCurrentTable()
        {
            return orderItems;
        }

        public void getOrderItemsTable()
        {//Put all of the orderId's orderitems into a DataTable and return it
            MySqlCommand cmmd = new MySqlCommand();
            cmmd.CommandText =
                "SELECT * " +
                "FROM `ORDER-DETAIL`;";
            cmmd.Connection = conn;
            try
            {
                adap = new MySqlDataAdapter(cmmd);
                build = new MySqlCommandBuilder(adap);
                
                DataTable ds = new DataTable();
                adap.Fill(ds);

                orderItems = ds;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void deleteAllProducts()
        {//Method to delete all of the products associated with the orderID in Order Menu Item link

            if (orderItems.Rows.Count > 0)
            {
                orderItems.Rows.Clear();
            }
        }

        public void removeAllOfAProduct(int productId)
        {//delete product from order - deletes all quantity, unlike removeproduct
            try
            {
                if(hasProduct(productId))
                {
                    int place = whereProduct(productId);
                    orderItems.Rows[place][OrderIDCol] = DBNull.Value;
                }
            }
            catch (DeletedRowInaccessibleException x)
            {
                throw x;
            }
            catch (Exception xy)
            {
                throw xy;
            }
        }

        public bool hasProduct(int productId)
        {//Method that checks if `Order Menu Item link` has an orderId that has product: productId
            if (orderItems.Rows.Count > 0)
            {
                foreach (DataRow r in orderItems.Rows)
                {
                    if ((r[OrderIDCol].ToString() == orderID.ToString()) && r[MenuIDCol].ToString() == productId.ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int whereProduct(int productId)
        {//Method that checks if `Order Menu Item link` has an orderId that has product: productId
            int x = 0;
            if (orderItems.Rows.Count > 0)
            {
                foreach (DataRow r in orderItems.Rows)
                {
                    if ((r[OrderIDCol].ToString() == orderID.ToString()) && r[MenuIDCol].ToString() == productId.ToString())
                    {
                        return x;
                    }
                    x++;
                }
            }
            return x;
        }

        public void removeProduct(int productId, int howmany)
        {//Remove quantity of productId from order (if the order contains the product)
         //If quantity goes less than 0 (currentQuantity-howmany), delete the item from the order
            if (orderItems.Rows.Count > 0)
            {
                if(hasProduct(productId))
                {
                    int place = whereProduct(productId);
                    int quant = int.Parse(orderItems.Rows[place][QuantityCol].ToString());

                    if (quant - howmany > 0)
                    {
                        orderItems.Rows[place][QuantityCol] = (quant - howmany);
                    }
                    else
                    {
                        removeAllOfAProduct(productId);
                    }
                }
            }
        }

        private int getQuantityOfAProduct(int productId)
        {//Get the quantity of a product in the order
            if (orderItems.Rows.Count > 0)
            {
                foreach (DataRow r in orderItems.Rows)
                {
                    if ((r[OrderIDCol].ToString() == orderID.ToString()) && r[MenuIDCol].ToString() == productId.ToString())
                    {
                        try
                        {
                            return int.Parse(r[QuantityCol].ToString());
                        }
                        catch (Exception x)
                        {
                            throw x;
                        }
                    }
                }
            }
            return 0;
        }

        public void addProduct(int productId, int howmany)
        {//The default adding of a product to be used...
         //It checks if the product already in order (if is, then add the quantity, else create new product in order)
            if (howmany < 0)
            {
                throw new Exception("Invalid quantity of products to add : addProduct() in OrderItems");
            }
            if (orderItems.Rows.Count > 0)
            {
                if (hasProduct(productId))
                {
                    int place = whereProduct(productId);
                    int quant = int.Parse(orderItems.Rows[place][QuantityCol].ToString());
                    orderItems.Rows[place][QuantityCol] = (quant + howmany);
                }
                else
                {
                    try
                    {
                        DataRow dr = orderItems.NewRow();
                        dr[MenuIDCol] = productId;
                        dr[OrderIDCol] = orderID;
                        dr[QuantityCol] = howmany;

                        orderItems.Rows.Add(dr);
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }
            }
        }

        public void close()
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    adap.Update(orderItems);
                }
            }
            catch (Exception z)
            {
                throw z;
            }
        }

        public void dispose()
        {
            close();
        }
    }
}