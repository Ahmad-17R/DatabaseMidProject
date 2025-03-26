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
    public partial class StudentResult : Form
    {
        public StudentResult()
        {
            InitializeComponent();
        }

        private void StudentResult_Load(object sender, EventArgs e)
        {
            Loadcombos();
            LoadData();
        }
        private void Loadcombo1()
        {
            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select RegistrationNumber FROM Student", con);
            // SqlCommand cmd2 = new SqlCommand("Select Title FROM Assessment", con);
            SqlDataReader reader = cmd.ExecuteReader();
            comboBox1.Items.Clear();
            // comboBox2.Items.Clear();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
                // comboBox2.Items.Add(reader.GetString(0));
            }
            reader.Close();
            con.Close();
        }
        private void Loadcombo2()
        {
            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Details FROM RubricLevel", con);
            // SqlCommand cmd2 = new SqlCommand("Select Title FROM Assessment", con);
            SqlDataReader reader = cmd.ExecuteReader();
            comboBox2.Items.Clear();
            // comboBox2.Items.Clear();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader.GetString(0));
                // comboBox2.Items.Add(reader.GetString(0));
            }
            reader.Close();
            con.Close();
        }

        private void Loadcombo3()
        {
            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Name FROM AssessmentComponent", con);
            // SqlCommand cmd2 = new SqlCommand("Select Title FROM Assessment", con);
            SqlDataReader reader = cmd.ExecuteReader();
            comboBox4.Items.Clear();
            // comboBox2.Items.Clear();
            while (reader.Read())
            {
                comboBox4.Items.Add(reader.GetString(0));
                // comboBox2.Items.Add(reader.GetString(0));
            }
            reader.Close();
            con.Close();
        }

        private void Loadcombos()
        {
            Loadcombo1();
            Loadcombo2();
            Loadcombo3();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
        private void LoadData()
        {
            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from StudentResult", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void emptytextboxes()
        {
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            comboBox4.SelectedItem = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm form = new MainForm();
            form.Show();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into StudentResult values (@StudentId,@AssessmentComponentId,@RubricMeasurementId,@EvaluationDate)", con);
            con.Open();
            //  SqlCommand cmd2 = new SqlCommand("Select Id from Rubric where Details=@Details", con);
            //  SqlCommand cmd3 = new SqlCommand("Select Id from Assessment where Title=@Title", con);

            SqlCommand cmd2 = new SqlCommand("select Id from Student where RegistrationNumber=@regno", con);
            cmd2.Parameters.AddWithValue("@regno", comboBox1.Text);
            SqlDataReader sqlDataReader2 = cmd2.ExecuteReader();
            sqlDataReader2.Read();
            int studentId = sqlDataReader2.GetInt32(0);
            sqlDataReader2.Close();
            cmd2.ExecuteScalar();

            SqlCommand cmd3 = new SqlCommand("select Id from RubricLevel where Details=@Details", con);
            cmd3.Parameters.AddWithValue("@Details", comboBox2.Text);
            SqlDataReader DataReader3 = cmd3.ExecuteReader();
            DataReader3.Read();
            int levelId = DataReader3.GetInt32(0);
            DataReader3.Close();
            cmd3.ExecuteScalar();

            SqlCommand cmd4 = new SqlCommand("select Id from AssessmentComponent where Name=@name", con);
            cmd4.Parameters.AddWithValue("@name", comboBox4.Text);
            SqlDataReader DataReader4 = cmd4.ExecuteReader();
            DataReader4.Read();
            int assessmentId = DataReader4.GetInt32(0);
            DataReader4.Close();
            cmd4.ExecuteScalar();

            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@AssessmentComponentId", assessmentId);
            // cmd.Parameters.AddWithValue("@RubricId", rubricid);
            cmd.Parameters.AddWithValue("@RubricMeasurementId", levelId);
            cmd.Parameters.AddWithValue("@EvaluationDate", DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
            emptytextboxes();
            MessageBox.Show("Data Inserted Successfully");
            }
            catch
            {
                    
                MessageBox.Show("Data Cannot Be inserted");
            
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete StudentResult where StudentId=@StId", con);
            SqlCommand cmd2 = new SqlCommand("Select Id from Student where RegistrationNumber=@regno", con);
            cmd2.Parameters.AddWithValue("@regno", comboBox1.Text);
            SqlDataReader DataReader = cmd2.ExecuteReader();
            DataReader.Read();
            int id = DataReader.GetInt32(0);
            DataReader.Close();
            cmd2.ExecuteScalar();
            cmd.Parameters.AddWithValue("@StId", id);
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
            emptytextboxes();
            MessageBox.Show("Assessment Component Level Deleted!");
            }
            catch
            {
                MessageBox.Show("It canot be deleted!");
            }


           
        }
    }
}
