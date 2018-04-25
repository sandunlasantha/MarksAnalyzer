using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace MA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Thread t = new Thread(new ThreadStart(SplasaStart));
            t.Start();
            Thread.Sleep(3000);

            InitializeComponent();

            t.Abort();
        }

        public void SplasaStart()
        {
            Application.Run(new Form2());
        }




        private void button1_Click(object sender, EventArgs e)
       {
            String Password;
            String Username;
               Username = comboBox1.Text;
            Password=textBox2.Text;
            {
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Please select Username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.Focus();
                    return;
                }
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Please enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Focus();
                    return;

                }


                SqlConnection con = new SqlConnection("Data Source=ACER-E5\\MADB;Initial Catalog=madb;Integrated Security=True ");
                SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) from [Passwords] Where Username  ='" + comboBox1.Text + "' and Password  ='" + textBox2.Text + "'  ", con);
                DataTable dt = new System.Data.DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    this.Hide();
                    Form3 ss = new Form3(comboBox1.Text);
                    ss.Show();

                }
                else
                {
                    textBox2.Text = "";
                    MessageBox.Show("Password is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;






                }

            } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to exit", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr==DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
       

    }
}
