using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace CRUD_operations
{
    public partial class index : System.Web.UI.Page
    {
        public string cs = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                Items_Data();
            }
        }
        public void Items_Data() {
            ddl1.Items.Clear();

            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = new SqlCommand("select * from student", con);

            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                ListItem li = new ListItem();

                li.Text = "select";
                ddl1.Items.Add(li);
                while (rdr.Read())
                {
                    ListItem x = new ListItem();
                    x.Text = rdr["studentname"].ToString();
                    ddl1.Items.Add(x);
                }
                rdr.Close();
            }
            catch (Exception e)
            {
                result.Text = e.Message;
            }
            finally {
                con.Close();
            }
        }

        protected void ddl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl1.SelectedItem.Text == "select")
            {
                stdno.Text = "";
                stdname.Text = "";
                stdloc.Text = "";
                result.Text = "please select any student name..!";
            }
            else {

                SqlConnection con = new SqlConnection(cs);
                string qry = "select * from student where studentname='"+ddl1.SelectedItem.Text+"';";
                SqlCommand cmd = new SqlCommand(qry, con);
                try
                {

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Read();
                    stdno.Text = rdr["studentno"].ToString();
                    stdname.Text = rdr["studentname"].ToString();
                    stdloc.Text = rdr["location"].ToString();
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    result.Text = ex.Message;
                }
                finally {
                    con.Close();
                }
            }
        }

        protected void clear_Click(object sender, EventArgs e)
        {
            stdno.Text = "";
            stdname.Text = "";
            stdloc.Text = "";
            result.Text = "please select or add a record";

        }

        protected void insert_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string qry="insert into student values("+stdno.Text+",'"+stdname.Text+"','"+stdloc.Text+"')";
            SqlCommand cmd = new SqlCommand(qry, con);
            try
            {
                con.Open();
                int x = cmd.ExecuteNonQuery();
                result.Text = "No.of rows inserted:" + x.ToString();

            }
            catch (Exception ex)
            {
                result.Text = ex.Message;
            }
            finally {
                con.Close();
                Items_Data();
            }

       }

        protected void update_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string qry = "update student set location='" + stdloc.Text + "' where studentname='" + stdname.Text + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            try
            {
                con.Open();
                int x = cmd.ExecuteNonQuery();
                result.Text = "No.of rows updated:" + x.ToString();
            }
            catch (Exception ex)
            {
                result.Text = ex.Message;
            }
            finally {
                con.Close();
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string qry = "delete from student where studentname='" + stdname.Text + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            try
            {

                con.Open();
                int x = cmd.ExecuteNonQuery();
                result.Text = "No.of rows deleted:" + x.ToString();
                stdno.Text = "";
                stdname.Text = "";
                stdloc.Text = "";

            }
            catch (Exception ex)
            {
                result.Text = ex.Message;
            }
            finally {
                con.Close();
                Items_Data();
            }
        }
    }

}