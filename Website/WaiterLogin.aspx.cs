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
    public partial class WaiterLogin : System.Web.UI.Page
    {
        private static string waiterWaiterIDName = "Waiter_ID";
        private static string waiterWaiterUsername = "Username";
        private static string waiterWaiterPassword = "Password";
        private static string fromPageSession = "FromPage";
        private static string errorSession = "Error";
        private static string waiterWaiter = "WaiterLogged";
        private MySqlConnection conn;
        private string pageName = HttpContext.Current.Request.Url.AbsoluteUri;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session[fromPageSession] = pageName;
            try
            {
                DatabaseConnection connection = new DatabaseConnection(); //New connection object (See Connection.cs)
                conn = connection.getConnection();

            }
            catch (Exception x)
            {
                throwEx(x);
            }
        }

        protected void Button1_Click(System.Object sender, System.EventArgs e)
        {
            bool isfound = false;
            MySqlCommand comm = new MySqlCommand
            {
                Connection = conn,
                CommandText =
               "SELECT * " +
               "FROM `WAITER` " +
               "WHERE `Username` = @uname"
            };
            comm.Parameters.AddWithValue("@uname", txtName.Text);

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
                                    if (r[waiterWaiterUsername] != System.DBNull.Value)
                                    {
                                        Session[waiterWaiter] = r[waiterWaiterIDName];
                                        isfound = true;
                                    }
                                }
                            }
                        }
                    }
                }
                if(isfound)
                {
                    Response.Redirect("WaiterOrders.aspx", false);
                }
                else
                {
                    txtStat.Visible = true;
                    txtStat.Text = "We could not find you...";
                }
            }
            catch(Exception x)
            {
                throwEx(x);
            }
        }
        private void throwEx(Exception x)
        {
            Session[errorSession] = x.Message + x.StackTrace;
            HttpContext.Current.Response.Redirect("Error.aspx", false);
        }
    }
}