using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyGIS;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        List<GISFeature> features = new List<GISFeature>();
        GISView view = null;

        public Form1()
        {
            InitializeComponent();
            view = new GISView(new GISExtent(new GISVertex(0, 0), new GISVertex(100, 100)),
                ClientRectangle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x = Convert.ToDouble(textBox1.Text);
            double y = Convert.ToDouble(textBox2.Text);
            string attribute = textBox3.Text;
            GISPoint onepoint = new GISPoint(new GISVertex(x, y));

            GISAttribute attributeset = new GISAttribute();
            attributeset.AddValue(attribute);

            GISFeature onefeasure = new GISFeature(onepoint, attributeset);
            features.Add(onefeasure);

            Graphics graphics = CreateGraphics();
            onefeasure.draw(graphics,view,true, 0);


        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            GISVertex mouselocation = view.ToMapVertex(new Point(e.X, e.Y));

            double mindistance = Double.MaxValue;
            int id = -1;

            for (int i = 0; i < features.Count; i++)
            {
                double onedistance = features[i].getSpatial().getCentroid().Distance(mouselocation);
                if (onedistance < mindistance)
                {
                    id = i;
                    mindistance = onedistance;
                }
            }

            if (id == -1)
            {
                MessageBox.Show("points is an empty set!");
                return;
            }
            int screendistance=view.ToScreenPoint(new GISVertex(mindistance,0)).X-
                view.ToScreenPoint(new GISVertex(0,0)).X;
            if (screendistance > 5)
            {
                MessageBox.Show("please click one point closely!");
                return;
            }

            MessageBox.Show("attribute is " + features[id].getAttributeValue(0));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double minx=Double.Parse(textBox4.Text);
            double miny=Double.Parse(textBox5.Text);
            double maxx=Double.Parse(textBox6.Text);
            double maxy=Double.Parse(textBox7.Text);
            view.CurrentMapExtent.SetValue(new GISVertex(minx, miny),
                new GISVertex(maxx, maxy));
            updateview();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            view.CurrentMapExtent.ZoomIn();
            updateview();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            view.CurrentMapExtent.ZoomOut();
            updateview();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            view.CurrentMapExtent.MoveUp();
            updateview();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            view.CurrentMapExtent.MoveDown();
            updateview();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            view.CurrentMapExtent.MoveLeft();
            updateview();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            view.CurrentMapExtent.MoveRight();
            updateview();
        }

        private void updateview()
        {
            view.SetValue(view.CurrentMapExtent, ClientRectangle);
            Graphics graphics = CreateGraphics();
            graphics.FillRectangle(new SolidBrush(Color.Black), ClientRectangle);
            for (int i = 0; i < features.Count; i++)
            {
                features[i].draw(graphics, view, true, 0);
            }
        }
    }
}
