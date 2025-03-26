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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace _6469
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Loaddata();
          
        }

        private void Loaddata()
        {
            var con = Connection.getInstance().getConnection(); 
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Student", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into Student values (@FirstName,@LastName,@Contact,@Email,@RegisterationNumber,@Status)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
            cmd.Parameters.AddWithValue("@Contact", textBox3.Text);
            cmd.Parameters.AddWithValue("@Email", textBox4.Text);
            cmd.Parameters.AddWithValue("@RegisterationNumber", textBox5.Text);
            string status = comboBox1.Text.ToString();
            if(status == "Active"  ) cmd.Parameters.AddWithValue("@Status", 1);
            else  cmd.Parameters.AddWithValue("@Status", 2);
            // cmd.Parameters.AddWithValue("@Status", textBox4.Text);


            cmd.ExecuteNonQuery();
            con.Close();
            Loaddata();
            emptytextboxes();
            MessageBox.Show("Data Inserted Successfully");
            }
            catch
            {
                MessageBox.Show("Data Cannot Be Inserted");
            }
        }

     

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.CurrentRow.Selected = true;
            textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();
            textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells["LastName"].Value.ToString();
            textBox3.Text = dataGridView2.Rows[e.RowIndex].Cells["Contact"].Value.ToString();
            textBox4.Text = dataGridView2.Rows[e.RowIndex].Cells["Email"].Value.ToString();
            textBox5.Text = dataGridView2.Rows[e.RowIndex].Cells["RegistrationNumber"].Value.ToString();
            //textBox6.Text = dataGridView2.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            textBox7.Text = dataGridView2.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            // textBox6.Text = dataGridView2.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            string status = dataGridView2.Rows[e.RowIndex].Cells["Status"].Value.ToString();

            if (status == "5")
            {
                string itemtoselect = "Active";
                comboBox1.SelectedItem = itemtoselect;
                
            }
            else
            {
                string item = "Inactive";
                comboBox1.SelectedItem = item;
                
            }
            //Loaddata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection(); 
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Student where Id=@Id",con);
            cmd.Parameters.AddWithValue("@Id", textBox7.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Student Deleted!");
            Loaddata();
            emptytextboxes();
            }
            catch
            {
                MessageBox.Show("It canot be deleted!");
            }

        }
        private void emptytextboxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            //textBox7.Text = "";
            comboBox1.SelectedItem = null;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("update Student set FirstName=@FirstName, LastName=@LastName, Contact=@Contact, Email=@Email, RegistrationNumber=@RegistrationNumber,Status=@Status" +
                                            "  where Id=@Id", con);
            cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
            cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
            cmd.Parameters.AddWithValue("@Contact", textBox3.Text);
            cmd.Parameters.AddWithValue("@Email", textBox4.Text);
            cmd.Parameters.AddWithValue("@RegistrationNumber", textBox5.Text);
            cmd.Parameters.AddWithValue("@Id", textBox7.Text);
            string status = comboBox1.Text.ToString();
            if (status == "Active") cmd.Parameters.AddWithValue("@Status", 5);
            else cmd.Parameters.AddWithValue("@Status", 6);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Student Updated!");
            Loaddata();
            emptytextboxes();

            }
            catch
            {
                MessageBox.Show("Data Cannot Be Updated");
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm clo = new MainForm();
            clo.Show();
        }
    }
}
