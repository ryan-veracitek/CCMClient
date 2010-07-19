using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CCMClient
{
    public partial class dialogForm : Form
    {
        public Form1 par;
        public dialogForm(Form1 parent)
        {
            InitializeComponent();
            par = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  MessageBox.Show(this.textBox1.Text);
            if (!par.getMenuItem11())
            {

                if (this.textBox1.Text == "allow it")
                {
                    par.setMenuItem11(true);
                    this.label1.Text = "Checked!";
                }
                else
                {
                    this.label1.Text = "FAILED!";
                }
            }
            else
            {
                par.setMenuItem11(false);
                this.label1.Text = "Unchecked!";
            }
            this.Close();
        }

     
    }
}