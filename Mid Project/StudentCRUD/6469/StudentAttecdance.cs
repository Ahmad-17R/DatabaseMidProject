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

namespace _6469
{
    public partial class StudentAttecdance : Form
    {
        public StudentAttecdance()
        {
            InitializeComponent();
            Loadcombo1();
            Loadcombo2();
        }

        private void StudentAttecdance_Load(object sender, EventArgs e)
        {
          
            List<object> status = new List<object>();

            var con = Connection.getInstance().getConnection();
            con.Open();
          
            SqlCommand cmd2 = new SqlCommand("Select Name from lookup where category ='ATTENDANCE_STATUS'", con);
           
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
           
            DataTable dt2 = new DataTable();
         
            da2.Fill(dt2);
           
            foreach (DataRow row in dt2.Rows)
            {
                object value3 = row["Name"];
                status.Add(value3);
            }
           
            comboBox3.DataSource = status;
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

            // SqlConnection con = new SqlConnection("Data Source=DESKTOP-R7EGUO6;Initial Catalog=Midproject;Integrated Security=True;");
            // con.Open();
            int StudentID = getstudentId();
            int AttendanceID = getdateId();
            string AttendanceStatus = comboBox3.SelectedItem.ToString();
            var con = Connection.getInstance().getConnection();
            con.Open();
            SqlCommand Statusoptioins = new SqlCommand("Select lookupid from lookup where name = @STATUS and Category = 'ATTENDANCE_STATUS'", con);
            Statusoptioins.Parameters.AddWithValue("STATUS", AttendanceStatus);
            int Status = (int)Statusoptioins.ExecuteScalar();
            SqlCommand cmd = new SqlCommand("Insert into StudentAttendance values (@classID , @ID , @STATUS)", con);
            cmd.Parameters.AddWithValue("@ID", StudentID);
            cmd.Parameters.AddWithValue("classID", AttendanceID);
            cmd.Parameters.AddWithValue("STATUS", Status);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully saved");
           // showData();
            }
            catch{
                MessageBox.Show("Data Cannot Be Inserted");
            }
        }
        private int getstudentId()
        {
           
            var con = Connection.getInstance().getConnection();
            con.Open();
           // SqlCommand cmd = new SqlCommand("delete StudentResult where StudentId=@StId", con);
            SqlCommand cmd2 = new SqlCommand("Select Id from Student where RegistrationNumber=@regno", con);
            cmd2.Parameters.AddWithValue("@regno", comboBox1.Text);
            SqlDataReader DataReader = cmd2.ExecuteReader();
            DataReader.Read();
            int id = DataReader.GetInt32(0);
            DataReader.Close();
            cmd2.ExecuteScalar();
            con.Close();
            return id;
            
        

        }

        private int getdateId()
        {
           

            var con = Connection.getInstance().getConnection();
            con.Open();
            // SqlCommand cmd = new SqlCommand("delete StudentResult where StudentId=@StId", con);
            SqlCommand cmd2 = new SqlCommand("Select Id from ClassAttendance where AttendanceDate=@thisdate", con);
            cmd2.Parameters.AddWithValue("@thisdate", comboBox2.SelectedItem);
            SqlDataReader DataReader = cmd2.ExecuteReader();
            DataReader.Read();
            int id = DataReader.GetInt32(0);
            DataReader.Close();
            cmd2.ExecuteScalar();
            con.Close();
            return id;
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm form = new MainForm();
            form.Show();
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
            SqlCommand cmd = new SqlCommand("Select AttendanceDate FROM ClassAttendance", con);
            // SqlCommand cmd2 = new SqlCommand("Select Title FROM Assessment", con);
            SqlDataReader reader = cmd.ExecuteReader();
            comboBox2.Items.Clear();
            // comboBox2.Items.Clear();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader.GetDateTime(0));

                // comboBox2.Items.Add(reader.GetString(0));
            }
            reader.Close();
            con.Close();
        }
    }
}
