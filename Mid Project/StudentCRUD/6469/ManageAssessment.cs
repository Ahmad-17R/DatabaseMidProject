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
    public partial class ManageAssessment : Form
    {
        public ManageAssessment()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void LoadData()
        {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Assessment", con);
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
            SqlCommand cmd = new SqlCommand("Insert into Assessment values (@Title,@DateTime,@TotalMarks,@TotalWeightage)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@Title", textBox1.Text);
            cmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@TotalMarks", int.Parse(textBox2.Text));
            cmd.Parameters.AddWithValue("@TotalWeightage", int.Parse(textBox3.Text));
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
            MessageBox.Show("Data Inserted Successfully");
            }
            catch
            {
                MessageBox.Show("Data Cannot Be Inserted");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            // SqlConnection sqlConnection = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("update Assessment set Title=@Title, TotalMarks=@TotalMarks, TotalWeightage=@TotalWeightage" +
                                            "  where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Title", textBox1.Text);
            cmd.Parameters.AddWithValue("@TotalMarks", int.Parse(textBox2.Text));
            cmd.Parameters.AddWithValue("@TotalWeightage", int.Parse(textBox3.Text));
            cmd.Parameters.AddWithValue("@Id", textBox4.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Student Updated!");
            LoadData();
            emptyboxes();
            }
            catch
            {
                MessageBox.Show("Data Cannot Be Updated");
            }
        }

        private void emptyboxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Assessment where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", textBox4.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Student Deleted!");
            LoadData();
            emptyboxes();
            }
            catch
            {
                MessageBox.Show("It canot be deleted due to its use in other places!");
            }
        }

        private void ManageAssessment_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Title"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["TotalMarks"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["TotalWeightage"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm form = new MainForm();
            form.Show();
        }
    }
}
