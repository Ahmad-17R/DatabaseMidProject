using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _6469
{
    public partial class Rubric : Form
    {
        public Rubric()
        {
            InitializeComponent();
            Loadcombos();
            LoadData();
        }

        private void Rubric_Load(object sender, EventArgs e)
        {
            Loadcombos();
        }
        private void Loadcombos()
        {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Details FROM Rubric", con);
            SqlDataReader reader = cmd.ExecuteReader();
            comboBox1.Items.Clear();
            while (reader.Read()) { comboBox1.Items.Add(reader.GetString(0)); }
            reader.Close();
            con.Close();
        }
        private void LoadData()
        {
            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from RubricLevel", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            MainForm form = new MainForm();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();//Trust Server Certificate=True
                                                               // SqlConnection sqlConnection = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("update RubricLevel set Details=@Details, MeasurmentLevel=@MLevel," +
                                            "  where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Details", textBox1.Text);
            cmd.Parameters.AddWithValue("@Id", textBox2.Text);
            if (comboBox2.Text == "Exceptional") cmd.Parameters.AddWithValue("@MeasurementLevel", 4);
            // else cmd.Parameters.AddWithValue("@Status", 2);
            else if (comboBox2.Text == "Good") cmd.Parameters.AddWithValue("@MeasurementLevel", 3);
            else if (comboBox2.Text == "Fair") cmd.Parameters.AddWithValue("@MeasurementLevel", 2);
            else if (comboBox2.Text == "Unsatisfactory") cmd.Parameters.AddWithValue("@MeasurementLevel", 1);

            string status = comboBox1.Text.ToString();
            if (status == "Active") cmd.Parameters.AddWithValue("@Status", 1);
            else cmd.Parameters.AddWithValue("@Status", 2);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Student Updated!");
            LoadData();
            emptytextboxes();
            }
            catch
            {
                MessageBox.Show("Data Cannot Be Updated");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into RubricLevel values (@RubricId,@Details,@MeasurementLevel)", con);
            con.Open();
            SqlCommand cmd2 = new SqlCommand("Select Id from Rubric where Details=@iddetail", con);
            cmd2.Parameters.AddWithValue("@iddetail",comboBox1.Text);
            SqlDataReader sqlDataReader = cmd2.ExecuteReader();
            sqlDataReader.Read();
            int rubricid = sqlDataReader.GetInt32(0);
            sqlDataReader.Close();
            cmd2.ExecuteScalar();
            cmd.Parameters.AddWithValue("@RubricId", rubricid);
            cmd.Parameters.AddWithValue("@Details", textBox1.Text);
            if (comboBox2.Text == "Exceptional") cmd.Parameters.AddWithValue("@MeasurementLevel", 4);
            // else cmd.Parameters.AddWithValue("@Status", 2);
            else if (comboBox2.Text == "Good") cmd.Parameters.AddWithValue("@MeasurementLevel", 3);
            else if (comboBox2.Text == "Fair") cmd.Parameters.AddWithValue("@MeasurementLevel", 2);
            else if (comboBox2.Text == "Unsatisfactory") cmd.Parameters.AddWithValue("@MeasurementLevel", 1);




            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
            emptytextboxes();
            MessageBox.Show("Data Inserted Successfully");
            }
            catch
            {
                MessageBox.Show("Data Cannot Be Inserted");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Details"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
           
            string measurement = dataGridView1.Rows[e.RowIndex].Cells["MeasurementLevel"].Value.ToString();
            if (measurement == "1")
            {
                string itemtoselect = "Unsatifactory";
                comboBox2.SelectedItem = itemtoselect;

            }
            else if (measurement == "2")
            {
                string item = "Fair";
                comboBox2.SelectedItem = item;

            }

            else if (measurement == "3")
            {
                string item = "Good";
                comboBox2.SelectedItem = item;

            }

            else if (measurement == "4")
            {
                string item = "Exceptional";
                comboBox2.SelectedItem = item;

            }
           
        }
        private void emptytextboxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete RubricLevel where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", textBox2.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
            emptytextboxes();
            MessageBox.Show("Rubric Level Deleted!");
            }
            catch
            {
                MessageBox.Show("It canot be deleted due to its use in other places!");
            }
        }
    }
}
