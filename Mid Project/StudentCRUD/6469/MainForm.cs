using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6469
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 managestudents = new Form1();
            managestudents.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CLO clo = new CLO();
            clo.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rubric2 clo = new Rubric2();
            clo.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rubric form = new Rubric();
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            AssessmentComponent form = new AssessmentComponent();
            form.Show();
        }

        private void managestbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageAssessment form = new ManageAssessment();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           this.Hide();
           StudentResult form = new StudentResult();
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentAttecdance form = new StudentAttecdance();
            form.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClassAttendance form = new ClassAttendance();
            form.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
