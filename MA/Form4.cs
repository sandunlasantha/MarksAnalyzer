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
    public partial class Form4 : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        Connectionstring cs = new Connectionstring();
        public Form4()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    textBox1.Focus();
                    return;
                }
                con = new SqlConnection(cs.DBcon);
                con.Open();
                string ct = "select Username,Password from Passwords   where Username='" + comboBox1.Text + "' and Password='" + textBox1.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    MessageBox.Show("This combination already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";


                    textBox1.Focus();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Please select username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.Focus();
                    return;
                }

                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Focus();
                    return;
                }
                con = new SqlConnection(cs.DBcon);
                con.Open();
                string cb = "insert into Passwords(Username,Password) VALUES (@d1,@d2)";
                cmd = new SqlCommand(cb);

                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", comboBox1.Text);
                cmd.Parameters.AddWithValue("@d2", textBox1.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox1.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Please select username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.Text = "";
                    return;
                }
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    return;
                }
                else
                {
                    int RowsAffected = 0;
                    con = new SqlConnection(cs.DBcon);
                    con.Open();
                    string cm5 = "select Username,Password from Passwords where Username=@d1 and Password=@d2";
                    cmd = new SqlCommand(cm5);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 50, "Username"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 50, "Password"));

                    cmd.Parameters["@d1"].Value = comboBox1.Text;
                    cmd.Parameters["@d2"].Value = textBox1.Text;
                    con = new SqlConnection(cs.DBcon);
                    con.Open();

                    {
                        string cq1 = "delete from Passwords where Username='" + comboBox1.Text + "' and Password='" + textBox1.Text + "'";


                        rdr = cmd.ExecuteReader();

                        cmd = new SqlCommand(cq1);
                        cmd.Connection = con;
                        RowsAffected = cmd.ExecuteNonQuery();

                        con.Close();
                        if (RowsAffected > 0)
                        {
                            MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Text = "";
            
                        }

                        else
                        {
                            MessageBox.Show("Username or Password not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox1.Text = "";

                            return;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form10 ss = new Form10();
            ss.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form3 ss = new Form3();
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to exit", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
