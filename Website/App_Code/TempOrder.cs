using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Website.App_Code
{
    public class TempOrder
    {
        private MySqlConnection conn;
        private static MySqlDataAdapter adap;
        private static MySqlCommandBuilder build;
        private static DataTable order;
        private static DataRow orderRow;
        private static OrderItems orderItems;

        private string tabName = "ORDER";
        private string orderIDName = "Order_ID";
        private string tableIDName = "Table_nr";
        private string paidName = "Paid";
        private string statusName = "Status";
        private string orderCustomerName = "Customer_Name";
        private string waiterIDName = "Waiter_ID";
        private string cashOrCardName = "CashOrCard";

        public TempOrder()
        {
            try
            {
                DatabaseConnection connection = new DatabaseConnection();
                conn = connection.getConnection();
                getOrderTable();
                orderRow = order.NewRow();
                orderItems = new OrderItems();
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public void setOrderPaid(int paid)
        {
            try
            {
                orderRow[paidName] = paid;
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public void setCustomerName(string name)
        {
            try
            {
                orderRow[orderCustomerName] = name;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void setTable(int table)
        {
            try
            {
                orderRow[tableIDName] = table;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void setWaiter(int waiter)
        {
            try
            {
                orderRow[waiterIDName] = waiter;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void setCashOrCard(int cocard)
        {
            try
            {
                orderRow[cashOrCardName] = cocard;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void setStatus(int status)
        {
            try
            {
                orderRow[statusName] = status;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void close()
        {
            try
            {
                using(conn)
                {
                    conn.Open();
                    adap.Update(order);
                }
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public void closeAll()
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    adap.Update(order);
                    orderItems.setOrderID(getLastID());
                    orderItems.close();
                }
            }
            catch (Exception x)
            {
                throw x;
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

        public void getOrderTable()
        {
            MySqlCommand cmmd = new MySqlCommand();
            cmmd.CommandText =
                "SELECT * " +
                "FROM `ORDER`;";
            cmmd.Connection = conn;
            try
            {
                adap = new MySqlDataAdapter(cmmd);
                build = new MySqlCommandBuilder(adap);

                DataTable ds = new DataTable();
                adap.Fill(ds);

                order = ds;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

    }
}