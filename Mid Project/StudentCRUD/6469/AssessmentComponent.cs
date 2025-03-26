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
    public partial class AssessmentComponent : Form
    {
        public AssessmentComponent()
        {
            InitializeComponent();
           
        }

        private void AssessmentComponent_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCombos();
            Loadcombo2();
        }
        private void LoadData()
        {
           
            
                var con = Connection.getInstance().getConnection();
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from AssessmentComponent", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            
        }
        private void LoadCombos()
        {
            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Details FROM Rubric", con);
           // SqlCommand cmd2 = new SqlCommand("Select Title FROM Assessment", con);
            SqlDataReader reader = cmd.ExecuteReader();
            comboBox1.Items.Clear();
           // comboBox2.Items.Clear();
            while (reader.Read()) { comboBox1.Items.Add(reader.GetString(0));
               // comboBox2.Items.Add(reader.GetString(0));
            }
            reader.Close();
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            MainForm form = new MainForm();
            form.Show();
        }

        private void Loadcombo2()
        {
            var con = Connection.getInstance().getConnection();
            con.Open();
           
            SqlCommand cmd = new SqlCommand("Select Title FROM Assessment", con);
            SqlDataReader reader = cmd.ExecuteReader();
          //  comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            while (reader.Read())
            {
                //comboBox1.Items.Add(reader.GetString(0));
                comboBox2.Items.Add(reader.GetString(0));
            }
            reader.Close();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into AssessmentComponent values (@Name,@RubricId,@TotalMarks,@DateCreated,@DateUpdated,@AssessementId)", con);
            con.Open();
          //  SqlCommand cmd2 = new SqlCommand("Select Id from Rubric where Details=@Details", con);
          //  SqlCommand cmd3 = new SqlCommand("Select Id from Assessment where Title=@Title", con);

            SqlCommand cmd2 = new SqlCommand("select Id from Rubric where Details=@Details", con);
            cmd2.Parameters.AddWithValue("@Details", comboBox1.Text);
            SqlDataReader sqlDataReader = cmd2.ExecuteReader();
            sqlDataReader.Read();
            int rubricid = sqlDataReader.GetInt32(0);
            sqlDataReader.Close();
            cmd2.ExecuteScalar();

            SqlCommand cmd3 = new SqlCommand("select Id from Assessment where Title=@Title", con);
            cmd3.Parameters.AddWithValue("@Title", comboBox2.Text);
            SqlDataReader DataReader = cmd3.ExecuteReader();
            DataReader.Read();
            int assessmentId = DataReader.GetInt32(0);
            DataReader.Close();
            cmd3.ExecuteScalar();

            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@TotalMarks", textBox3.Text);
            cmd.Parameters.AddWithValue("@RubricId", rubricid);
            cmd.Parameters.AddWithValue("@AssessementId", assessmentId);
            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
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
        private void emptytextboxes()
        {
            textBox1.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem=null;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["TotalMarks"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Update  AssessmentComponent set Name=@Name,RubricId=@RubricId,TotalMarks=@TotalMarks,DateUpdated=@DateUpdated, AssessmentId=@AssessementId where Id=@id", con);
            con.Open();
            //  SqlCommand cmd2 = new SqlCommand("Select Id from Rubric where Details=@Details", con);
            //  SqlCommand cmd3 = new SqlCommand("Select Id from Assessment where Title=@Title", con);

            SqlCommand cmd2 = new SqlCommand("select Id from Rubric where Details=@Details", con);
            cmd2.Parameters.AddWithValue("@Details", comboBox1.Text);
            SqlDataReader sqlDataReader = cmd2.ExecuteReader();
            sqlDataReader.Read();
            int rubricid = sqlDataReader.GetInt32(0);
            sqlDataReader.Close();
            cmd2.ExecuteScalar();

            SqlCommand cmd3 = new SqlCommand("select Id from Assessment where Title=@Title", con);
            cmd3.Parameters.AddWithValue("@Title", comboBox2.Text);
            SqlDataReader DataReader = cmd3.ExecuteReader();
            DataReader.Read();
            int assessmentId = DataReader.GetInt32(0);
            DataReader.Close();
            cmd3.ExecuteScalar();

            cmd.Parameters.AddWithValue("@Name", textBox1.Text);
            cmd.Parameters.AddWithValue("@TotalMarks", textBox3.Text);
            cmd.Parameters.AddWithValue("@RubricId", rubricid);
            cmd.Parameters.AddWithValue("@AssessementId", assessmentId);
            cmd.Parameters.AddWithValue("@Id", int .Parse(textBox2.Text));
            cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
         
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
            emptytextboxes();
            MessageBox.Show("Data Updated Successfully");
            }
            catch
            {
                MessageBox.Show("Data Cannot Be Updated");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete AssessmentComponent where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", textBox2.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
            emptytextboxes();
            MessageBox.Show("Assessment Component Level Deleted!");
            }
            catch
            {
                MessageBox.Show("It canot be deleted due to its use in other tables!");
            }
        }
    }
}
