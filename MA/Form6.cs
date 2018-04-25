using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MA
{
    public partial class Form6 : Form
    {

        
        SqlConnection con = null;
        Connectionstring cs = new Connectionstring();
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 ss = new Form3();
            ss.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to exit", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {

                MessageBox.Show("Please enter test number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }
            int n;
            if (!int.TryParse(textBox1.Text, out n))
            {
                MessageBox.Show("Invalid test number ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                return;
            }
            else
            {
                con = new SqlConnection(cs.DBcon);
                con.Open();
                SqlDataReader dr2 = null;

                int testNo = int.Parse(textBox1.Text);
                SqlCommand cmd = new SqlCommand("SELECT Mark FROM Marks WHERE TestNo='" + testNo + "'", con);
                try
                {
                    int studentCount = 0;
                    double totalMark = 0;
                    int passCount = 0;
                    double average = 0;
                    double passPercentage = 0;
                    dr2 = cmd.ExecuteReader();


                    while (dr2.Read())
                    {
                        studentCount += 1;
                        int mark = int.Parse(dr2["Mark"].ToString());
                        totalMark += mark;

                        if (mark >= 40) passCount += 1;
                    }

                    average = totalMark / studentCount;
                    passPercentage = (passCount * 100.0) / studentCount;



                    textBox2.Text = studentCount.ToString();
                    textBox3.Text = Math.Round(average, 2).ToString();
                    textBox4.Text = Math.Round(passPercentage, 2).ToString();
                    MessageBox.Show("Search Successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    dr2.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
            }
            }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
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

       
    }
}
