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
    public partial class ClassAttendance : Form
    {
        public ClassAttendance()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ClassAttendance_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ClassAttendance", con);
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
            SqlCommand cmd = new SqlCommand("Insert into ClassAttendance values (@AttendanceDate)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@AttendanceDate", dateTimePicker1.Value);
            cmd.ExecuteNonQuery();
            con.Close() ;
            LoadData();

            }
            catch
            {
                MessageBox.Show("Data Cannot Be Inserted");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["AttendanceDate"].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from ClassAttendance where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", textBox1.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Student Deleted!");
            LoadData();
            textBox1.Text = "";
            textBox3.Text = "";     
            }
            catch
            {
                MessageBox.Show("It canot be deleted due to its use in places!");
            }
           }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm form = new MainForm();
            form.Show();
        }
    }
}
