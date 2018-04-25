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
    public partial class Form13 : Form
    {

        SqlConnection con = null;

        Connectionstring cs = new Connectionstring();
        public Form13(string regNo)
        {
            InitializeComponent();
            drowChart(regNo);
            setTestAverage(regNo);
            
            
        }

      

        private void drowChart(string regNo)
        {
            bool isNameSet = false;
            con = new SqlConnection(cs.DBcon);
                con.Open();
                SqlDataReader dr1 = null;
                //SqlCommand cmd = new SqlCommand("SELECT  Mark,TestNo FROM Marks WHERE Regno='" + regNo + "' LIMIT 10", con);
                SqlCommand cmd = new SqlCommand("SELECT  Mark,TestNo,Name FROM Marks WHERE Regno='" + regNo + "' ORDER BY TestNo ASC ", con);
                try
                {
                    dr1 = cmd.ExecuteReader();
                    while (dr1.Read())
                    {
                        if (!isNameSet)
                        {
                            label2.Text = dr1["Name"].ToString();
                            isNameSet = true;
                        }
                        this.chart1.Series["Marks"].Points.AddXY(dr1["TestNo"].ToString(), int.Parse(dr1["Mark"].ToString()));
                    }
                }
                catch (Exception ) { }
        }

  

   
       

     




        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to exit", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 ss = new Form8();
            ss.Show();
            this.Hide();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 ss = new Form3();
            ss.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }


        private void setTestAverage(string regNo)
        {
            con = new SqlConnection(cs.DBcon);
            con.Open();
            SqlDataReader dr2 = null;

            SqlCommand comm = new SqlCommand("SELECT Mark FROM Marks WHERE Regno='" + regNo + "'", con);
            try
            {
                int testCount = 0;
                double totalMark = 0;
                double average = 0;
                dr2 = comm.ExecuteReader();
                while (dr2.Read())
                {
                    testCount += 1;
                    int mark = int.Parse(dr2["Mark"].ToString());
                    totalMark += mark;
                }
                average = totalMark / testCount;
                textBox1.Text = Math.Round(average, 2).ToString();
                dr2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
       
    }
}
