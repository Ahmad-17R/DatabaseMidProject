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
    public partial class Rubric2 : Form
    {

        public Rubric2()
        {
            InitializeComponent();
        }

        private void Rubric2_Load(object sender, EventArgs e)
        {
            LoadCombo();
            LoadData();
        }

        private void LoadCombo()
        {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Name FROM Clo", con);
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
            SqlCommand cmd = new SqlCommand("Select * from Rubric", con);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Rubric(Details,CloId) values(@Details,@CloId)", con);
            SqlCommand cmd2 = new SqlCommand("select Id from Clo where Name=@Name", con);
            cmd2.Parameters.AddWithValue("@Name", comboBox1.Text);
            cmd2.ExecuteScalar();
            SqlDataReader reader = cmd2.ExecuteReader();
            reader.Read();
            int num = reader.GetInt32(0);
            reader.Close();
            cmd.Parameters.AddWithValue("@Details", textBox1.Text);
            //cmd.Parameters.AddWithValue("@Id", int.Parse(textBox2.Text));///runtime error
            cmd.Parameters.AddWithValue("@CloId", num);
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
            MessageBox.Show("Rubric has been Successfully Added!");
            }
            catch
            {
                MessageBox.Show("Data Cannot Be Inserted");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;

            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Details"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            comboBox1.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells["CloId"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("update Rubric set Details=@Details where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Details", textBox1.Text);
           // cmd.Parameters.AddWithValue("@CloId", int.Parse(comboBox1.Text.ToString()));
            cmd.Parameters.AddWithValue("@Id", textBox2.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Rubric Updated!");
            emptyboxes();
            LoadData();
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
            //comboBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete Rubric where Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", textBox2.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
            emptyboxes();
            MessageBox.Show("Rubric Deleted!");
            }
            catch
            {
                MessageBox.Show("It canot be deleted!");
            }
        }
    }
}
