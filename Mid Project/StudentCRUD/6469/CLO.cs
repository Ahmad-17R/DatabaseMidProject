using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _6469
{
    public partial class CLO : Form
    {
        public CLO()
        {
            InitializeComponent();
        }

        private void CLO_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var con = Connection.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Clo", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Clo values (@Name , @DateCreated , @DateUpdated)", con);
            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully saved");
            textBox1.Text = "";
            LoadData();
            }
            catch
            {
                MessageBox.Show("Data Cannot Be Inserted");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("update Clo set Name=@Name,DateUpdated=@DateUpdated where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
            cmd.Parameters.AddWithValue("@Id", textBox2.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            EmptyBoxes();
            LoadData();
            MessageBox.Show("CLO Updated!");
            }
            catch
            {
                MessageBox.Show("Data Cannot Be Updated");
            }
        }

        private void EmptyBoxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            { var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Clo where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", textBox2.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
            EmptyBoxes();
            MessageBox.Show("CLO Deleted!");

            }
            catch
            {
                MessageBox.Show("First delete the items against the CLO");
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            MainForm form = new MainForm();
            form.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            dataGridView1.CurrentRow.Selected = true;
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            textBox2.Text= dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
        }
    }
}
