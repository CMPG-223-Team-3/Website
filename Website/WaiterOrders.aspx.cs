using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


namespace Website
{
    public partial class WaiterOrders : System.Web.UI.Page
    {
        private MySqlConnection connection;
        private string server = "cmpg-223-db.ci6pbvbzz3x3.us-west-1.rds.amazonaws.com";
        private string database = "sql7368973";
        private string uid = "admin";
        private string password = "cmpg22310";
        string connectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }

            private void BindGrid()
            {
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Order_ID, Table_nr, Status, Paid, CashOrCard FROM tblOrders"))
                    {
                        using (MySqlDataAdapter sda = new SMyqlDataAdapter())
                        {
                            cmd.Connection = connection;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                            }
                        }
                    }
                }
            }
        }

            protected System.Void GridView1_SelectedIndexChanged(System.Object sender, System.EventArgs e)
            {

            }
    }
}