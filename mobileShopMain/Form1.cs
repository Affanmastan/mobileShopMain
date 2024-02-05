using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace mobileShopMain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SAMSUNG\Documents\MobileShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void populate()
        {
            con.Open();
            string query = "select * from products";
            SqlDataAdapter da=new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            Stock.DataSource = ds.Tables[0];
            con.Close();

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if(pCat.Text=="" || pId.Text=="" || pName.Text=="")
            {
                MessageBox.Show("missing information");
            }

            else
            {
                try
                {
                    con.Open();
                    String sql = "insert into products values('"+pId.Text+"','"+pCat.SelectedItem.ToString()+"','"+pName.Text+"','"+pDes.Text+"','"+pPrice.Text+"','"+pBrand.Text+"','"+pStock.Text+"') ";
                    SqlCommand cmd =new SqlCommand(sql,con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("PRODUCT ADDED SUCCESSFULLY");
                    con.Close();
                    populate();

                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }

                con.Close();

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            populate();

        }

        private void Stock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pId.Text = Stock.SelectedRows[0].Cells[0].Value.ToString();
            pCat.SelectedItem = Stock.SelectedRows[0].Cells[1].Value.ToString();
            pName.Text = Stock.SelectedRows[0].Cells[2].Value.ToString();
            pDes.Text = Stock.SelectedRows[0].Cells[3].Value.ToString();
            pPrice.Text = Stock.SelectedRows[0].Cells[4].Value.ToString();
            pBrand.Text = Stock.SelectedRows[0].Cells[5].Value.ToString();
            pStock.Text = Stock.SelectedRows[0].Cells[6].Value.ToString();
           
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (pCat.Text == "" || pId.Text == "" || pName.Text == "")
            {
                MessageBox.Show("missing information");
            }

            else
            {
                try
                {
                    con.Open();
                    String sql = "update products set pStock='"+pStock.Text+"' where pId='"+ pId.Text+ "';";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("PRODUCT UPDATED SUCCESSFULLY");
                    con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                con.Close();

            }
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            pId.Text = "";
            pCat.Text = "";
            pName.Text = "";
            pDes.Text = "";
            pPrice.Text = "";
            pBrand.Text = "";
            pStock.Text = "";

        }
    }
}
