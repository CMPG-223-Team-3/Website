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
                DatabaseConnection connection = new DatabaseConnection();//Class to connect to database
                conn = connection.getConnection();
                this.orderID = OrderID;
                getOrderItemsTable();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public OrderItems()
        {//this constructor is for to use for a temporary table - only to be uploaded to db when orderID set and stuff
            try
            {
                DatabaseConnection connection = new DatabaseConnection();//Class to connect to database
                conn = connection.getConnection();
                getOrderItemsTable();
                this.orderID = -1;
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

        public DataTable getThisOrderItems()
        {
            int counter = 0;
            DataTable send = new DataTable();
            send = orderItems.Clone();//clone orderItems datatable structure not the values
            if (orderItems.Rows.Count > 0)
            {
                for (int i = 0; i < orderItems.Rows.Count; i++)
                {
                    if (orderItems.Rows[i][OrderIDCol].ToString() == getOrderID().ToString())
                    {
                        send.ImportRow(orderItems.Rows[i]);
                        counter++;
                    }
                }
            }
            if (counter == 0)
            {
                return new DataTable();
            }
            else
            {
                return send;
            }
        }

        private void getOrderItemsTable()
        {//Put all of the orderId's orderitems into a DataTable and global var it
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
                if (hasProduct(productId))
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
                    if ((r[OrderIDCol].ToString() == getOrderID().ToString()) && r[MenuIDCol].ToString() == productId.ToString())
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
                    if ((r[OrderIDCol].ToString() == getOrderID().ToString()) && r[MenuIDCol].ToString() == productId.ToString())
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
                if (hasProduct(productId))
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
                    if ((r[OrderIDCol].ToString() == getOrderID().ToString()) && r[MenuIDCol].ToString() == productId.ToString())
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
            if (orderItems.Rows.Count >= 0)
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
                        dr[OrderIDCol] = getOrderID();
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
            if (getOrderID() != -1)
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
            else
            {
                throw new NotImplementedException("Order ID not yet set...");
            }
        }

        public void dispose()
        {
            close();
        }

        public int getOrderID()
        {
            return this.orderID;
        }

        public int setOrderID(int id)
        {
            if (getOrderID() != -1)
            {
                throw new Exception("Can't change the ID of a non-temporary instance");
            }
            else
            {
                return setNewOrderID(id);
            }
        }

        private int setNewOrderID(int id)
        {//set all order details (THAT ARE NOT YET SENT TO THE DATABASE) to the new orderid
            int counter = 0;
            if (orderItems.Rows.Count > 0)
            {
                for (int i = 0; i <= orderItems.Rows.Count; i++)
                {
                    if (orderItems.Rows[i][OrderIDCol].ToString() == getOrderID().ToString())
                    {
                        orderItems.Rows[i][OrderIDCol] = id;
                        counter++;
                    }
                }
                return counter;
            }
            return 0;
        }
    }
}