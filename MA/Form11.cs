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
    public partial class Form11 : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        Connectionstring cs = new Connectionstring();
        
        
        public Form11()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 ss = new Form3();
            ss.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            button3.Enabled = true;
           // button5.Enabled = false;
            //button4.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")

                {
                    MessageBox.Show("Please enter registration number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Focus();
                    return;
                }


                int n;
                if (!int.TryParse(textBox1.Text, out n))
                {
                    MessageBox.Show("Invalid registration number ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Focus();
                    Reset();
                    return;
                }

        
                con = new SqlConnection(cs.DBcon);
                con.Open();
                string ct = "select Regno from Student where Regno='" + textBox1.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Registration number already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox1.Focus();



                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Please enter Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Focus();
                    return;
                }
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Please enter NIC", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox3.Focus();
                    return;
                }
                if (textBox4.Text == "")
                {
                    MessageBox.Show("Please enter Tel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox4.Focus();
                    return;
                }
                if (textBox5.Text == "")
                {
                    MessageBox.Show("Please enter Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox5.Focus();
                    return;
                }
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Please select gender", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.Focus();
                    return;
                }
         

                con = new SqlConnection(cs.DBcon);
                con.Open();

                string cb = "insert into Student(Regno,Name,NIC,Tel,Email,Gender) VALUES (@d1,@d2,@d3,@d4,@d5,@d6)";
                cmd = new SqlCommand(cb);

                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", textBox1.Text);
                cmd.Parameters.AddWithValue("@d2", textBox2.Text);
                cmd.Parameters.AddWithValue("@d3", textBox3.Text);
                cmd.Parameters.AddWithValue("@d4", textBox4.Text);
                cmd.Parameters.AddWithValue("@d5", textBox5.Text);
                cmd.Parameters.AddWithValue("@d6", comboBox1.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                // GetData();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                comboBox1.Text = "";
                return;


                //button3.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please search registration number to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            con = new SqlConnection(cs.DBcon);
            con.Open();
            string cb = "update Student set Regno=@d1,Name=@d2,NIC=@d3,Tel=@d4,Email=@d5,Gender=@d6 where Regno=@d1";

            

            SqlCommand cmd = new SqlCommand(cb);

            cmd.Connection = con;


            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "Regno"));
            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "Name"));

            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 15, "NIC"));
            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 15, "Tel"));
            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 15, "Email"));

            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 15, "Gender"));


            cmd.Parameters["@d1"].Value = textBox1.Text;
            cmd.Parameters["@d2"].Value = textBox2.Text;
            cmd.Parameters["@d3"].Value = textBox3.Text;
            cmd.Parameters["@d4"].Value = textBox4.Text;
            cmd.Parameters["@d5"].Value = textBox5.Text;
            cmd.Parameters["@d6"].Value = comboBox1.Text;
            int qstatus = cmd.ExecuteNonQuery();

            if (qstatus > 0)
            {
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Registration number not found", "Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";

            }
            
           

           
            button1.Enabled = true;
            con.Close();
        }
        private void Reset()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {


            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please enter registration number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    comboBox1.Text = "";
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


                    int RowsAffected = 0;
                    con = new SqlConnection(cs.DBcon);

                    con.Open();
                    string cm5 = "select Regno from Student where Regno=@d1";
                    cmd = new SqlCommand(cm5);

                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.Int, 10, "Regno"));


                    cmd.Parameters["@d1"].Value = textBox1.Text;

                    con = new SqlConnection(cs.DBcon);
                    con.Open();



                  /*  DialogResult dr = MessageBox.Show("Are you sure you want to delete this", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dr == DialogResult.Yes) */
                    {


                        string cq1 = "delete from Student where Regno='" + textBox1.Text + "'";

                        rdr = cmd.ExecuteReader();

                        cmd = new SqlCommand(cq1);
                        cmd.Connection = con;
                        RowsAffected = cmd.ExecuteNonQuery();

                        con.Close();



                        if (RowsAffected > 0)
                        {

                            MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            comboBox1.Text = "";



                        }
                        else
                        {





                            MessageBox.Show("Registration number not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox1.Text = "";
                        }

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 


        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {

                MessageBox.Show("Please enter registration number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }
            int n;
            if (!int.TryParse(textBox1.Text, out n))
            {
                MessageBox.Show("Invalid registration number ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                Reset();
                return;
            }
            else
            {
                con = new SqlConnection(cs.DBcon);
                con.Open();
                SqlDataReader dr1 = null;
                SqlCommand cmd = new SqlCommand("SELECT * FROM Student WHERE Regno='" + textBox1.Text + "'", con);
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
                    comboBox1.Text = dr1[5].ToString();
                    MessageBox.Show("Search Successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {  // MessageBox.Show("Record can't found");
                    MessageBox.Show("Registration number not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Reset();



                    dr1.Close();

                    con.Close();
                }





            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to exit", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button7_Click(object sender, EventArgs e)
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
    }
}
