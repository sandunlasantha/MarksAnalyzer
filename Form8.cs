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
    public partial class Form8 : Form
    {

       
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        
        DataTable dt = new DataTable();
        Connectionstring cs = new Connectionstring();

        public Form8()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form7 ss = new Form7();
            ss.Show();
            this.Hide();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

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
            if (textBox6.Text == "")
            {
                MessageBox.Show("Please enter test number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox6.Text = "";
                textBox6.Focus();
                return;

            }
            int m;
            if (!int.TryParse(textBox6.Text, out m))
            {
                MessageBox.Show("Invalid test number ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox6.Text = "";
                textBox6.Focus();

                return;
            }
            else
            {

                con = new SqlConnection(cs.DBcon);
                con.Open();
                SqlDataReader dr1 = null;
                SqlCommand cmd = new SqlCommand("SELECT Regno, Name, Mark,TestNo, Rank, Result,ZScore FROM Marks WHERE Regno='" + textBox1.Text + "' AND TestNo='" + textBox6.Text + "' ", con);
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

                    int rank = int.Parse(dr1["Rank"].ToString());
                    dr1.Close();
                    if (rank == 0)
                    {
                        string b = textBox6.Text;
                        int testNo = int.Parse(b);
                        rankCalculate(testNo, con);
                    }

                    dr1 = cmd.ExecuteReader();
                    if (dr1.Read())
                    {
                        textBox2.Text = dr1["Name"].ToString();
                        textBox4.Text = dr1["Mark"].ToString();
                        textBox6.Text = dr1["TestNo"].ToString();
                        textBox1.Text = dr1["Regno"].ToString();
                        textBox3.Text = dr1["Rank"].ToString();
                        textBox5.Text = dr1["Result"].ToString();

                        MessageBox.Show("Search Successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
                else


                    MessageBox.Show("Registration number or test number not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                textBox1.Text = "";
                textBox6.Text = "";


                dr1.Close();

                con.Close();

                return;



            }



        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter registration number to view the average and chart ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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



                }
                else
                {


                    MessageBox.Show("Registration number not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";

                    dr1.Close();


                    con.Close();
                    return;

                }
                
                

                    Form13 ss = new Form13(textBox1.Text);
                    ss.Show();
                    this.Hide();
                

            }
        }

        private void rankCalculate(int testNo, SqlConnection con)
        {
            var list = new List<KeyValuePair<int, String>>();
            SqlDataReader dr2 = null;
            SqlCommand cmd = new SqlCommand("SELECT Regno, Mark FROM Marks WHERE TestNo='" + testNo + "' ORDER BY Mark DESC", con);
            try
            {
                int rank = 0;
                dr2 = cmd.ExecuteReader();
                while (dr2.Read())
                {
                    int Regno = int.Parse(dr2["Regno"].ToString());
                    int mark = int.Parse(dr2["Mark"].ToString());

                    rank += 1;
                    String result = "F";
                    if (mark >= 75) result = "A";
                    else if (mark >= 65) result = "B";
                    else if (mark >= 50) result = "C";
                    else if (mark >= 40) result = "S";

                    result = result + "_" + rank;

                    list.Add(new KeyValuePair<int, String>(Regno, result));
                }
                dr2.Close();

                foreach (var res in list)
                {
                    int regNo = res.Key;
                    //update Marks set Regno=@d1,Name=@d2,Mark=@d3 where Regno=@d1
                    string cb = "update Marks set Rank=@d1,Result=@d2 WHERE Regno ='" + regNo + "' AND TestNo='" + testNo + "'";
                    SqlCommand cmd2 = new SqlCommand(cb);

                    String[] ResAr = res.Value.Split('_');

                    cmd2.Connection = con;
                    cmd2.Parameters.AddWithValue("@d1", ResAr[1]);
                    cmd2.Parameters.AddWithValue("@d2", ResAr[0]);


                    cmd2.ExecuteNonQuery();



                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
