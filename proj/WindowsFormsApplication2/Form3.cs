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
    public partial class Form3 : Form
    {
        GISDocument document;
        Form2 MapWindow;

        public Form3(GISDocument _document, Form2 _mapwindow)
        {
            InitializeComponent();
            document = _document;
            MapWindow = _mapwindow;      
        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            UpdateLayer();
        }

        public void UpdateLayer()
        {
            listBox1.Items.Clear();
            dataGridView1.Columns.Clear();
            for (int i=0;i<document.layers.Count;i++)
            {
                listBox1.Items.Add(document.layers[i].Name);
            }
            if (listBox1.Items.Count > 0)
            {
                listBox1.SetSelected(0, true);
                FillValue(document.layers[0]);
            }
        }
        private void FillValue(GISLayer layer)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("InternalID", "InternalID");
            dataGridView1.Columns[0].Visible = false;
            for (int i = 0; i < layer.Fields.Count; i++)
            {
                dataGridView1.Columns.Add(layer.Fields[i].name, layer.Fields[i].name);
            }
            for (int i = 0; i < layer.Features.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = i;
                for (int j = 0; j < layer.Fields.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j + 1].Value = layer.Features[i].getAttributeValue(j);
                }
                dataGridView1.Rows[i].Selected = layer.Features[i].Selected;
            }
        }

        public void UpdateSelection()
        {
            GISLayer layer = document.layers[listBox1.SelectedIndex];
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                int index=(int)(dataGridView1.Rows[i].Cells[0].Value);
                dataGridView1.Rows[i].Selected = layer.Features[index].Selected;
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            GISLayer layer = document.layers[listBox1.SelectedIndex];
            layer.ClearSelection();
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                layer.Features[(int)(dataGridView1.SelectedRows[i].Cells[0].Value)].Selected = true;
            }
            MapWindow.updateview();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                FillValue(document.layers[listBox1.SelectedIndex]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
