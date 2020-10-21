﻿using MySql.Data.MySqlClient;
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

        private string tabName = "ORDER_DETAIL";
        private string orderIDName = "Order_ID";
        private string tableIDName = "Table_nr";
        private string paidName = "Paid";
        private string statusName = "Status";

        public Order(int orderId)
        {//Constructor for when you already know the order id, get the rest
            try
            {
                orderItems = new OrderItems(orderId);//Object for all the items in the database for this order
                DatabaseConnection connect = new DatabaseConnection();//Class to connect to database
                conn = connect.getConnection();
                this.orderID = orderId;
                if(!getOrderInfo(orderId))//Go get orderID's info and store it in global vars
                {
                    throw new Exception("Could not get order info from getOrderInfo()");
                }
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public Order(int tabID, int paid, int status)//Parameters of the customer so new order can be made
        {//Commonly the one you'd use for making a new order (no orderID)
            try
            {
                DatabaseConnection connect = new DatabaseConnection();//Class to connect to database
                conn = connect.getConnection();
            }
            catch(Exception x)
            {
                throw x;
            }

            this.tableID = tabID;
            try
            {
                MySqlCommand cmmd = new MySqlCommand();
                cmmd.CommandText =
                    "INSERT " +
                    "INTO `ORDER`" +
                    "(`Table_nr`, `Paid`, `Status`) " +
                    "VALUES(@cid, @pd, @st);";
                cmmd.Connection = conn;
                cmmd.Parameters.AddWithValue("@cid",tabID);
                cmmd.Parameters.AddWithValue("@pd", paid);
                cmmd.Parameters.AddWithValue("@st",status);
                
                executeNonQuery(cmmd);
                orderID = getLastID();
                orderItems = new OrderItems(orderID);//Object for all the items in the database for this order
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
                                tableID = int.Parse(rd[tableIDName].ToString());
                                orderPaid = int.Parse(rd[paidName].ToString());
                                orderStatus = int.Parse(rd[statusName].ToString());
                            }
                            success = true;
                        }
                    }
                }
                return success;
            }
            catch
            {
                return false;
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
            catch(Exception p)
            {
                throw p;
            }
            return x;
        }

        public void OrderPaid()
        {//Method to update order so that the order has been paid
            throw new NotImplementedException();
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

        public int getTableID()
        {
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
    }
}