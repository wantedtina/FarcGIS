using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form5 : Form
    {
        Form4 frm;
        public Form5()
        {
            frm= new Form4(this);
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.BackColor = Color.White;
            this.Opacity = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.05;
            if (this.Opacity == 1)
            {
                frm.Show();
                this.Hide();
                timer1.Stop();   
            }
        }
    }
}
