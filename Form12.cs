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
    public partial class Form12 : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        Connectionstring cs = new Connectionstring();
        public Form12()
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please enter registration number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox4.Text = "";
                    Reset();
                    textBox1.Focus();
                    return;
                }
                if (textBox4.Text == "")
                {
                    MessageBox.Show("Please enter test number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    textBox4.Focus();
                    return;
                }
                int m;
                if (!int.TryParse(textBox4.Text, out m))
                {
                    MessageBox.Show("Invalid test number ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox4.Focus();
                    Reset();
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
                string ct = "select Regno,TestNo from Marks where Regno='" + textBox1.Text + "' and TestNo='" + textBox4.Text + "'";
                //("SELECT Regno, Name, Mark,TestNo FROM Marks WHERE Regno='" + textBox1.Text + "' and TestNo='" + textBox4.Text + "'", con);

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    MessageBox.Show("This combination already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox4.Text = "";

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
                    MessageBox.Show("Please enter Mark", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox3.Focus();
                    return;
                }
              
                
                con = new SqlConnection(cs.DBcon);
                con.Open();

                string cb = "insert into Marks(Regno,Name,Mark,TestNo) VALUES (@d1,@d2,@d3,@d4)";
                cmd = new SqlCommand(cb);

                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", textBox1.Text);
                cmd.Parameters.AddWithValue("@d2", textBox2.Text);
                cmd.Parameters.AddWithValue("@d3", textBox3.Text);
                cmd.Parameters.AddWithValue("@d4", textBox4.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                // GetData();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
               
                textBox3.Text = "";
                textBox4.Text = ""; 


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
                MessageBox.Show("Please search registration number and test number to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (textBox4.Text == "")
            {
                MessageBox.Show("Please search registration number and test number to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                textBox4.Focus();
                
                return;
            }
            int m;
            if (!int.TryParse(textBox4.Text, out m))
            {
                MessageBox.Show("Invalid test number ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Text = "";
                textBox4.Focus();
                return;
            }
           
          
         


            try
            {

                
                con = new SqlConnection(cs.DBcon);
                
                con.Open();
                string cb = "update Marks set Regno=@d1,Name=@d2,Mark=@d3,TestNo=@d4 where Regno=@d1 and TestNo=@d4";
                cmd = new SqlCommand(cb);

                
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", textBox1.Text);
                cmd.Parameters.AddWithValue("@d2", textBox2.Text);
                cmd.Parameters.AddWithValue("@d3", textBox3.Text);
                cmd.Parameters.AddWithValue("@d4", textBox4.Text);


                int qstatus = cmd.ExecuteNonQuery();

                if (qstatus > 0)
                {
                    MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    return;
                }
                else
                {
                    MessageBox.Show("Registration number or test number not found", "Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox4.Text = "";

                }

                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







       /* {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please search regno to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
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
                MessageBox.Show("Please enter Mark", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return;

            }
            con = new SqlConnection(cs.DBcon);
            con.Open();
            string cb = "update Marks set Regno=@d1,Name=@d2,Mark=@d3 where Regno=@d1";

            SqlCommand cmd = new SqlCommand(cb);

            cmd.Connection = con;


            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "Regno"));
            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "Name"));

            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 15, "Mark"));



            cmd.Parameters["@d1"].Value = textBox1.Text;
            cmd.Parameters["@d2"].Value = textBox2.Text;
            cmd.Parameters["@d3"].Value = textBox3.Text;

            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";



            button1.Enabled = true;
            con.Close();
            

            
        }*/
        private void Reset()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }

        private void button5_Click(object sender, EventArgs e)
        {
          
         try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please enter Registration number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
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

               if (textBox4.Text == "")
                     {
                         MessageBox.Show("Please enter test number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         
                        
                         return;
                     }
                     int p;
                     if (!int.TryParse(textBox4.Text, out p))
                     {
                         MessageBox.Show("Invalid test number ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         textBox4.Text = "";
                         textBox4.Focus();
                         return;
                     }

                else
                {


                    int RowsAffected = 0;
                    con = new SqlConnection(cs.DBcon);

                    con.Open();
                    string cm5 = "select Regno,TestNo from Marks where Regno=@d1 and TestNo=@d2";
                    cmd = new SqlCommand(cm5);

                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.Int, 10, "Regno"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.Int, 10, "TestNo"));


                    cmd.Parameters["@d1"].Value = textBox1.Text;
                    cmd.Parameters["@d2"].Value = textBox4.Text;


                    con = new SqlConnection(cs.DBcon);
                    con.Open();



                  /*  DialogResult dr = MessageBox.Show("Are you sure you want to delete this", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (dr == DialogResult.Yes) */
                        

                    {


                        string cq1 = "delete from Marks where Regno='" + textBox1.Text + "' and TestNo='" + textBox4.Text + "'";

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
                         
                           

                        }
                        else
                        {
                           
                            

                          

                            MessageBox.Show("Registration number or test number not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Reset();
                            
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



        private void button6_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter registration number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
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

            if (textBox4.Text == "")
            {
                MessageBox.Show("Please enter test number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
                
                return;

            }
            int m;
            if (!int.TryParse(textBox4.Text, out m))
            {
                MessageBox.Show("Invalid test number ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
                textBox4.Text = "";
                
                return;
            }
         
         
          
            else
            {
                con = new SqlConnection(cs.DBcon);
                con.Open();
                SqlDataReader dr1 = null;
                SqlCommand cmd = new SqlCommand("SELECT Regno, Name, Mark,TestNo FROM Marks WHERE Regno='" + textBox1.Text + "' and TestNo='" + textBox4.Text + "'", con);
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
                    MessageBox.Show("Search Successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {   // MessageBox.Show("Record can't found");
                    MessageBox.Show("Registration number or test number not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

