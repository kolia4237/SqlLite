using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SqlLiteTest.readers;

namespace SqlLiteTest
{
    public partial class Form1 : Form
    {
       

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                listBox1.Items.Clear();

                var cookies = ChromeCookierReader.FindCookie(textBox1.Text);
                foreach (var cooky in cookies)
                {
                    listBox1.Items.Add(cooky);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

      //////////////////////////     firefox
      private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                var cookies = FoxCookierReader.FindCookie(textBox1.Text);
                foreach (var cooky in cookies)
                {
                    listBox1.Items.Add(cooky);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
      //////////////////////////  

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                var cookies = OperaCookieReader.FindCookie(textBox1.Text);
                foreach (var cooky in cookies)
                {
                    listBox1.Items.Add(cooky);
                }  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
