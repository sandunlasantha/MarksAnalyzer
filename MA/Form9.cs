using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace MA
{
    public partial class Form9 : Form
    {
        SqlConnection con = null;
        public Form9()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form7 ss = new Form7();
            ss.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter registration number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox1.Focus();
                return;

            }
            int n;
            if (!int.TryParse(textBox1.Text, out n))
            {
                MessageBox.Show("Invalid registration number ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox1.Focus();
                
                return;
            }
            else
            {
                Connectionstring cs = new Connectionstring();
                con = new SqlConnection(cs.DBcon);
                con.Open();
                SqlDataReader dr1 = null;
                SqlCommand cmd = new SqlCommand("SELECT * FROM Student WHERE Regno ='" + textBox1.Text + "'", con);
                try
                {
                    con.Open();
                }
                catch
                {

                }

                dr1 = cmd.ExecuteReader();
                if
                (dr1.Read())
                {
                    textBox2.Text = dr1[1].ToString();
                    textBox3.Text = dr1[2].ToString();
                    textBox4.Text = dr1[3].ToString();
                    textBox5.Text = dr1[4].ToString();
                    textBox6.Text = dr1[5].ToString();
                    MessageBox.Show("Search Successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                    
                    
                }
                else

                    
                    MessageBox.Show("Registration number not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    
               
                
               


                dr1.Close();

                con.Close();

                return;
                



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
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

        private void button7_Click(object sender, EventArgs e)
        {
            {
                Form3 ss = new Form3();
                ss.Show();
                this.Hide();
            }

        }
    }
}
