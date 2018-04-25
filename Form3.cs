using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MA
{
    public partial class Form3 : Form
    {
        static string USER_TYPE;
        public Form3(string userType)
        {
            USER_TYPE = userType;
            InitializeComponent();

            validateAuth();

        }

        public Form3()
        {
            InitializeComponent();
            validateAuth();          

        }

        private void validateAuth(){
            if (USER_TYPE == "Admin" || USER_TYPE == "Teacher")
            {
                groupBox2.Enabled= true;
            }
            else
            {
                //button3.Enabled = false;
                groupBox1.Enabled = false;
                
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 ss = new Form3();
            ss.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 ss = new Form3();
            ss.Show();
            this.Hide();
        }

       

      

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to exit", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to logout", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                Form1 ss = new Form1();
                ss.Show();
                this.Hide();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form10 ss = new Form10();
            ss.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form11 ss = new Form11();
            ss.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form12 ss = new Form12();
            ss.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form6 ss = new Form6();
            ss.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form7 ss = new Form7();
            ss.Show();
            this.Hide();
        }
    }
}
