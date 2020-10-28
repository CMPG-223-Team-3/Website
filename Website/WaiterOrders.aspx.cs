using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Website.App_Code;

namespace Website
{
    public partial class WaiterOrders : System.Web.UI.Page
    {
        private MySqlConnection connection;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }

            
        }

        private void BindGrid()
        {
            DatabaseConnection connection4545 = new DatabaseConnection();
            MySqlConnection connection = connection4545.getConnection();
            using (connection)
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ORDER where Order_ID = @NUM;");
                cmd.Parameters.AddWithValue("@NUM", 1);
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
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