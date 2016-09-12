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
    public partial class Form2 : Form
    {
        List<Delaunay3> dlay = new List<Delaunay3>();
        int i,j,k,m,q;
        enum Direction {unused,up,down,left,right}
        Direction dir = Direction.unused;
        Boolean op1=false, op2=false;
        int n = 20,b1;
        Cursor yl;
        Graphics g2;
        GISDocument document = new GISDocument();
        GISView view = null;
        private Bitmap backwindow,dlaybackwind;
        private MOUSECOMMAND MouseCommand = MOUSECOMMAND.Unused;
        private int MouseStartX = 0;
        private int MouseStartY = 0;
        private int MouseMovingX = 0;
        private int MouseMovingY = 0;
        private bool MouseOnMap = false;
        private Form3 layerDialog = null;
        Line ggb = new Line();
        Line ggb1 = new Line();
        PointF pc, p1, p2;
        Boolean n1;
        List<PointF> pt = new List<PointF>();
        Delaunay3 de, de1, de2;
        PointF[] ptA = new PointF[3];
        int[,] sjbh = new int[50, 10];
        List<Delaunay3> workset = new List<Delaunay3>();
        double[,] jd = new double[2, 50];
        public Form2()
        {
            InitializeComponent();
            view = new GISView(new GISExtent(new GISVertex(0, 0), new GISVertex(100, 100)),
                    ClientRectangle);
            yl = this.Cursor;
            ptA[0].X = this.Width / 2;
            ptA[0].Y = -Convert.ToSingle(ptA[0].X / Math.Sqrt(3) * 2);
            ptA[1].X = -ptA[0].X;
            ptA[1].Y = this.Height;
            ptA[2].X = this.Width + ptA[0].X;
            ptA[2].Y = this.Height - 2;
            g2 = this.CreateGraphics();
            ((Control)this).MouseWheel += new MouseEventHandler(Form1_MouseWheel);
        }

        void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            try 
            {
                if (e.Location.X > 0 && e.Location.X < this.Width && e.Location.Y > 0 && e.Location.Y < this.Height)
                {
                    if (e.Delta > 0)
                    {
                        GISVertex MouseLocation = view.ToMapVertex(new Point(e.X, e.Y));
                        double ZoomInfactor = 0.8;
                        double newwidth = view.CurrentMapExtent.Width * ZoomInfactor;
                        double newheight = view.CurrentMapExtent.Height * ZoomInfactor;
                        double newminx = MouseLocation.x - (MouseLocation.x - view.CurrentMapExtent.MinX) * ZoomInfactor;
                        double newminy = MouseLocation.y - (MouseLocation.y - view.CurrentMapExtent.MinY) * ZoomInfactor;
                        view.CurrentMapExtent.SetValue(new GISVertex(newminx, newminy), new GISVertex(newminx + newwidth, newminy + newheight));
                   }
                    if (e.Delta < 0)
                    {
 GISVertex MouseLocation = view.ToMapVertex(new Point(e.X, e.Y));
                            double ZoomOutfactor = 0.8;
                            double newwidth = view.CurrentMapExtent.Width / ZoomOutfactor;
                            double newheight = view.CurrentMapExtent.Height / ZoomOutfactor;
                           double newminx = MouseLocation.x - (MouseLocation.x - view.CurrentMapExtent.MinX) / ZoomOutfactor;
                            double newminy = MouseLocation.y - (MouseLocation.y - view.CurrentMapExtent.MinY) / ZoomOutfactor;
                            view.CurrentMapExtent.SetValue(new GISVertex(newminx, newminy), new GISVertex(newminx + newwidth, newminy + newheight));
                        
                    }
                    if (delaunayToolStripMenuItem1.Checked || 泰森多边形ToolStripMenuItem.Checked)
                        updateview2();
                    else
                        updateview();
                }
            }
            catch 
            {
                MessageBox.Show("操作错误");
            }
        }
        public void updateview()
        {
            view.SetValue(view.CurrentMapExtent, ClientRectangle);
            if (backwindow != null) backwindow.Dispose();
            backwindow = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics g = Graphics.FromImage(backwindow);
            g.FillRectangle(new SolidBrush(Color.Black), ClientRectangle);
            document.draw(g, view);
            Graphics graphics = CreateGraphics();
            graphics.DrawImage(backwindow, 0, 0);
        }
        public void updateview2()
        {
            if (delaunayToolStripMenuItem1.Checked&&dlay.Count>0)
                delaunayToolStripMenuItem1.PerformClick();
            if (泰森多边形ToolStripMenuItem.Checked && dlay.Count > 0)
                泰森多边形ToolStripMenuItem.PerformClick();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            if (backwindow != null)
            {
                if (MouseOnMap)
                {
                    if (MouseCommand == MOUSECOMMAND.Pan)
                    {
                        e.Graphics.DrawImage(backwindow, MouseMovingX - MouseStartX, MouseMovingY - MouseStartY);
                    }
                    else
                    {
                        e.Graphics.DrawImage(backwindow, 0, 0);
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 0, 0, 0)), new Rectangle(
                            Math.Min(MouseStartX, MouseMovingX), Math.Min(MouseStartY, MouseMovingY),
                            Math.Abs(MouseStartX - MouseMovingX), Math.Abs(MouseStartY - MouseMovingY)));
                    }
                }
                else
                {
                    e.Graphics.DrawImage(backwindow, 0, 0);
                }
            }
        }

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            if (delaunayToolStripMenuItem1.Checked || 泰森多边形ToolStripMenuItem.Checked)
                updateview2();
            else
                updateview();
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            MouseStartX = e.X;
            MouseStartY = e.Y;
            if (e.Button == MouseButtons.Left && MouseCommand != MOUSECOMMAND.Unused)
            {
                MouseOnMap = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this.PointToScreen(new Point(e.X, e.Y)));
            }
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMovingX = e.X;
            MouseMovingY = e.Y;
            if (MouseCommand == MOUSECOMMAND.MY)
            {
                if (e.X < n)
                {
                    dir = Direction.left;
                    if (op1 == false)
                    {
                        op1 = true;
                        timer1.Start();
                    }
                    this.Cursor = System.Windows.Forms.Cursors.PanWest;
                }
                if (e.X > this.Width - n - 17)
                {
                    dir = Direction.right;
                    if (op1 == false)
                    {
                        op1 = true;
                        timer1.Start();
                    }
                    this.Cursor = System.Windows.Forms.Cursors.PanEast;
                }
                if (e.Y < n)
                {
                    dir = Direction.up;
                    if (op2 == false)
                    {
                        op2 = true;
                        timer2.Start();
                    }
                    this.Cursor = System.Windows.Forms.Cursors.PanNorth;
                }
                if (e.Y > this.Height - n - 40)
                {
                    dir = Direction.down;
                    if (op2 == false)
                    {
                        op2 = true;
                        timer2.Start();
                    }
                    this.Cursor = System.Windows.Forms.Cursors.PanSouth;
                }
                if (e.X >= n && e.X <= this.Width - n - 17 && e.Y >= n && e.Y <= this.Height - n - 40)
                {
                    op1 = false;
                    op2 = false;
                    timer1.Stop();
                    timer2.Stop();
                    this.Cursor = yl;
                }
            }
            if (MouseOnMap)
            {
                Invalidate();
            }
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseOnMap)
            {
                MouseOnMap = false;
                switch (MouseCommand)
                {
                    case MOUSECOMMAND.Select:
                        for (int i = 0; i < document.layers.Count;i++ )
                            document.layers[i].ClearSelection();
                        if (e.X == MouseStartX && e.Y == MouseStartY)
                        {
                            for (int i = 0; i < document.layers.Count; i++)
                            {
                                GISFeature feature = document.layers[i].SelectByClick(new Point(e.X, e.Y), view);
                                if (feature != null) feature.Selected = true;
                            }
                        }
                        else
                        {

                            GISExtent extent = view.RectToExtent(new Rectangle(
                                Math.Min(e.X, MouseStartX),
                                Math.Min(e.Y, MouseStartY),
                                Math.Abs(e.X - MouseStartX),
                                Math.Abs(e.Y - MouseStartY)));
                            for (int i = 0; i < document.layers.Count; i++)
                            {
                                List<GISFeature> features = document.layers[i].SelectByExtent(extent);
                                for (int j = 0; j < features.Count; j++) features[j].Selected = true;
                            }
                        }
                        updateview();
                        if (layerDialog != null) layerDialog.UpdateSelection();
                        break;
                    case MOUSECOMMAND.ZoomIn:
                        if (e.X == MouseStartX && e.Y == MouseStartY)
                        {
                            GISVertex MouseLocation = view.ToMapVertex(new Point(e.X, e.Y));
                            double ZoomInfactor = 0.8;
                            double newwidth = view.CurrentMapExtent.Width * ZoomInfactor;
                            double newheight = view.CurrentMapExtent.Height * ZoomInfactor;
                            double newminx = MouseLocation.x - (MouseLocation.x - view.CurrentMapExtent.MinX) * ZoomInfactor;
                            double newminy = MouseLocation.y - (MouseLocation.y - view.CurrentMapExtent.MinY) * ZoomInfactor;
                            view.CurrentMapExtent.SetValue(new GISVertex(newminx, newminy), new GISVertex(newminx + newwidth, newminy + newheight));
                        }
                        else
                        {
                            view.CurrentMapExtent = view.RectToExtent(new Rectangle(
                                Math.Min(e.X, MouseStartX),
                                Math.Min(e.Y, MouseStartY),
                                Math.Abs(e.X - MouseStartX),
                                Math.Abs(e.Y - MouseStartY)));
                        }
                        if (delaunayToolStripMenuItem1.Checked || 泰森多边形ToolStripMenuItem.Checked)
                            updateview2();
                        else
                            updateview();
                        break;
                    case MOUSECOMMAND.ZoomOut:
                        if (e.X == MouseStartX && e.Y == MouseStartY)
                        {
                            GISVertex MouseLocation = view.ToMapVertex(new Point(e.X, e.Y));
                            double ZoomOutfactor = 0.8;
                            double newwidth = view.CurrentMapExtent.Width / ZoomOutfactor;
                            double newheight = view.CurrentMapExtent.Height / ZoomOutfactor;
                            double newminx = MouseLocation.x - (MouseLocation.x - view.CurrentMapExtent.MinX) / ZoomOutfactor;
                            double newminy = MouseLocation.y - (MouseLocation.y - view.CurrentMapExtent.MinY) / ZoomOutfactor;
                            view.CurrentMapExtent.SetValue(new GISVertex(newminx, newminy), new GISVertex(newminx + newwidth, newminy + newheight));
                        }
                        else
                        {
                            GISExtent extent = view.RectToExtent(new Rectangle(
                                Math.Min(e.X, MouseStartX),
                                Math.Min(e.Y, MouseStartY),
                                Math.Abs(e.X - MouseStartX),
                                Math.Abs(e.Y - MouseStartY)));
                            double newwidth = view.CurrentMapExtent.Width * view.CurrentMapExtent.Width / extent.Width;
                            double newheight = view.CurrentMapExtent.Height * view.CurrentMapExtent.Height / extent.Height;
                            double newminx = extent.MinX - (extent.MinX - view.CurrentMapExtent.MinX) * newwidth / view.CurrentMapExtent.Width;
                            double newminy = extent.MinY - (extent.MinY - view.CurrentMapExtent.MinY) * newheight / view.CurrentMapExtent.Height;
                            view.CurrentMapExtent.SetValue(new GISVertex(newminx, newminy), new GISVertex(newminx + newwidth, newminy + newheight));
                        }
                        if (delaunayToolStripMenuItem1.Checked || 泰森多边形ToolStripMenuItem.Checked)
                            updateview2();
                        else
                            updateview();
                        break;
                    case MOUSECOMMAND.Pan:
                        GISVertex C1 = view.CurrentMapExtent.MapCenter;
                        GISVertex M1 = view.ToMapVertex(new Point(MouseStartX, MouseStartY));
                        GISVertex M2 = view.ToMapVertex(new Point(e.X, e.Y));
                        GISVertex C2 = new GISVertex(C1.x - (M2.x - M1.x), C1.y - (M2.y - M1.y));
                        view.CurrentMapExtent.SetMapCenter(C2);
                        updateview();
                              if (delaunayToolStripMenuItem1.Checked&&dlay.Count>0)
                delaunayToolStripMenuItem1.PerformClick();
            if (泰森多边形ToolStripMenuItem.Checked && dlay.Count > 0)
                泰森多边形ToolStripMenuItem.PerformClick();
                        break;
                }
            }
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.Equals(toolStripMenuItem8))
            {
                if (layerDialog == null)
                    layerDialog = new Form3(document, this);
                layerDialog.Show();
                if (layerDialog.WindowState == FormWindowState.Minimized)
                    layerDialog.WindowState = FormWindowState.Normal;
                layerDialog.BringToFront();
            }
            else if (sender.Equals(toolStripMenuItem6))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Shapefile文件|*.shp";
                openFileDialog.RestoreDirectory = false;
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                document.AddLayer(GISShapefile.ReadShapeFile(openFileDialog.FileName));
                if (document.layers.Count == 1)
                    view.CurrentMapExtent.CopyFrom(document.extent);
                updateview();
                if (layerDialog != null) layerDialog.UpdateLayer();
            }
            else if (sender.Equals(toolStripMenuItem7))
            {
                toolStripComboBox1.Items.Clear();
                for (int i = 0; i < document.layers.Count; i++)
                    toolStripComboBox1.Items.Add(document.layers[i].Name);
            }
            else if (sender.Equals(toolStripMenuItem5))
            {
                view.CurrentMapExtent.CopyFrom(document.extent);
                if (delaunayToolStripMenuItem1.Checked || 泰森多边形ToolStripMenuItem.Checked)
                    updateview2();
                else
                    updateview();
            }
            else
            {
                toolStripMenuItem1.Checked = false;
                toolStripMenuItem2.Checked = false;
                toolStripMenuItem3.Checked = false;
                toolStripMenuItem4.Checked = false;
                toolStripMenuItem9.Checked = false;
                ((ToolStripMenuItem)sender).Checked = true;
                if (sender.Equals(toolStripMenuItem1))
                {
                    MouseCommand = MOUSECOMMAND.Select;
                }
                else if (sender.Equals(toolStripMenuItem2))
                {
                    MouseCommand = MOUSECOMMAND.ZoomIn;
                }
                else if (sender.Equals(toolStripMenuItem3))
                {
                    MouseCommand = MOUSECOMMAND.ZoomOut;
                }
                else if (sender.Equals(toolStripMenuItem4))
                {
                    MouseCommand = MOUSECOMMAND.Pan;
                }
                else if (sender.Equals(toolStripMenuItem9))
                {
                    MouseCommand = MOUSECOMMAND.MY;
                }
            }
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            document.RemoveLayer(document.layers[toolStripComboBox1.SelectedIndex]);
            toolStripComboBox1.Items.Clear();
            for (int i = 0; i < document.layers.Count; i++)
                toolStripComboBox1.Items.Add(document.layers[i].Name);
            updateview();
            if (layerDialog != null) layerDialog.UpdateLayer();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dir == Direction.left)
            {
                view.CurrentMapExtent.MoveRight();
                if (delaunayToolStripMenuItem1.Checked || 泰森多边形ToolStripMenuItem.Checked)
                    updateview2();
                else
                    updateview();
            }
            if (dir == Direction.right)
            {
                view.CurrentMapExtent.MoveLeft();
                if (delaunayToolStripMenuItem1.Checked || 泰森多边形ToolStripMenuItem.Checked)
                    updateview2();
                else
                    updateview();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (dir == Direction.up)
            {
                view.CurrentMapExtent.MoveDown();
                if (delaunayToolStripMenuItem1.Checked || 泰森多边形ToolStripMenuItem.Checked)
                    updateview2();
                else
                    updateview();
            }
            if (dir == Direction.down)
            {
                view.CurrentMapExtent.MoveUp();
                if (delaunayToolStripMenuItem1.Checked || 泰森多边形ToolStripMenuItem.Checked)
                    updateview2();
                else
                    updateview();
            }
        }
        private Boolean LOP(Delaunay3 sj1, Delaunay3 sj2)
        {
            if (sj1.Ifggb(sj2) == true)//判断是否有公共线
            {//1if
                ggb = sj1.Findggb(sj2);//找到公共线
                for (j = 0; j < 3; j++)
                {//3for
                    if (sj2.pt[j] != ggb.StartNode && sj2.pt[j] != ggb.EndNode)
                        pc = sj2.pt[j];
                }//3for  找出除公共线以外三角形上2的另一个点pc
                float dis = sj1.DisToCen(pc);//计算pc距离三角形1外接圆圆心的距离dis
                if (dis < sj1.r)
                {//2if
                    return false;
                }//2if  比较dis和半径的大小，dis<r代表pc在三角形1的外接圆内，不符合空圆规则
                else
                {
                    for (j = 0; j < 3; j++)
                    {
                        if (sj1.pt[j] != ggb.StartNode && sj1.pt[j] != ggb.EndNode)
                            pc = sj1.pt[j];
                    }
                    dis = sj2.DisToCen(pc);
                    if (dis < sj2.r)
                    {
                        return false;
                    }
                }
            }
            else
                return false;
            return true;
        }

        public  void DrawDelaunay()
        {
            delaunayToolStripMenuItem1.Checked = true;
            泰森多边形ToolStripMenuItem.Checked = false;
            delaunay();
             n = 20;
                    for (m = dlay.Count - 1; m >= 0; m--)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            if (dlay[m].pt[k] == ptA[0] || dlay[m].pt[k] == ptA[1] || dlay[m].pt[k] == ptA[2])
                            {
                                dlay.Remove(dlay[m]);
                                break;
                            }
                        }
                    }
                    if (dlaybackwind != null) dlaybackwind.Dispose();
                    dlaybackwind = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                    g2 = Graphics.FromImage(dlaybackwind);
                    g2.FillRectangle(new SolidBrush(Color.Black), ClientRectangle);
                    document.draw(g2, view);
                    for (k = 0; k < dlay.Count; k++)
                     dlay[k].drawsjx(g2, Color.Green);    
                    Graphics graphics = CreateGraphics();
                    graphics.DrawImage(dlaybackwind, 0, 0);
        }
        public void delaunay()
        {
            GISVertex v;
            updateview();
            pt.Clear();
            dlay.Clear();
            for (i = 0; i < document.layers.Count; i++)
            {
                if (document.layers[i].ShapeType == SHAPETYPE.POINT)
                {
                    for (j = 0; j < document.layers[i].Features.Count; j++)
                    {
                        v = new GISVertex(view.ToScreenPoint(document.layers[i].Features[j].spatialpart.centroid).X, view.ToScreenPoint(document.layers[i].Features[j].spatialpart.centroid).Y);
                        pt.Add(new PointF(Convert.ToSingle(v.x), Convert.ToSingle(v.y)));
                    }
                    ptA[0].X = this.Width / 2;
                    ptA[0].Y = -Convert.ToSingle(ptA[0].X / Math.Sqrt(3) * 2);
                    ptA[1].X = -ptA[0].X;
                    ptA[1].Y = this.Height;
                    ptA[2].X = this.Width + ptA[0].X;
                    ptA[2].Y = this.Height - 10;
                    dlay.Add(new Delaunay3(ptA[0], ptA[1], ptA[2]));
                    for (q = 0; q < pt.Count; q++)
                    {
                        de = null;
                        for (j = 0; j < dlay.Count; j++)
                        {
                            if (dlay[j].contain(pt[q]))
                            {
                                de = dlay[j];
                                break;
                            }
                        }
                        if (de != null)
                        {
                            dlay.Remove(de);
                            dlay.Add(new Delaunay3(de.pt[0], de.pt[1], pt[q]));
                            dlay.Add(new Delaunay3(de.pt[0], de.pt[2], pt[q]));
                            dlay.Add(new Delaunay3(de.pt[2], de.pt[1], pt[q]));
                        }
                        n1 = false;
                        while (n1 == false)
                        {
                            n1 = true;
                            for (m = 0; m < dlay.Count - 1; m++)
                            {
                                for (n = m + 1; n < dlay.Count; n++)
                                {
                                    if (dlay[m].Ifggb(dlay[n]) && LOP(dlay[m], dlay[n]) == false)
                                    {
                                        de1 = dlay[m];
                                        de2 = dlay[n];
                                        n1 = false;
                                        ggb1 = de1.Findggb(de2);
                                        for (k = 0; k < 3; k++)
                                        {
                                            if (de1.pt[k] != ggb1.StartNode && de1.pt[k] != ggb1.EndNode)
                                                p1 = de1.pt[k];
                                            if (de2.pt[k] != ggb1.StartNode && de2.pt[k] != ggb1.EndNode)
                                                p2 = de2.pt[k];
                                        }//交换对角线
                                        dlay.Add(new Delaunay3(ggb1.StartNode, p1, p2));
                                        dlay.Add(new Delaunay3(ggb1.EndNode, p1, p2));
                                        dlay.Remove(de1);
                                        dlay.Remove(de2);
                                    }
                                    if (n1 == false)
                                        break;
                                }
                                if (n1 == false)
                                    break;
                            }
                        }
                 }
                        break;  
                }
                
            }
        }

        private void DrawTyson()
        {
            delaunayToolStripMenuItem1.Checked = false;
            泰森多边形ToolStripMenuItem.Checked = true;
            //MouseCommand = MOUSECOMMAND.TS;
            delaunay();
            if (dlaybackwind != null) dlaybackwind.Dispose();
            dlaybackwind = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            g2 = Graphics.FromImage(dlaybackwind);
            g2.FillRectangle(new SolidBrush(Color.Black), ClientRectangle);
            document.draw(g2, view);
            double dis;
            double s;
            for (m = 0; m < 50; m++)
                sjbh[m,0] = 0;
                if (dlay.Count >= 1)
                {
                    for (i = 0; i < pt.Count; i++)
                    {
                        b1 = 0;
                        for (j = 0; j < dlay.Count; j++)
                        {
                            if (dlay[j].pt[0] == pt[i] || dlay[j].pt[1] == pt[i] || dlay[j].pt[2] == pt[i])
                            {
                                b1++;
                                sjbh[i, 0] = b1;
                                sjbh[i, b1] = j;
                            }
                        }
                    }
                    for (i = 0; i < pt.Count; i++)
                    {
                        workset.Clear();
                        for (j = 1; j < sjbh[i, 0] + 1; j++)
                        {
                            workset.Add(dlay[sjbh[i, j]]);
                        }
                        for (j = 0; j < workset.Count; j++)
                        {
                            dis = Math.Sqrt((pt[i].X - workset[j].center.X) * (pt[i].X - workset[j].center.X) + (pt[i].Y - workset[j].center.Y) * (pt[i].Y - workset[j].center.Y));
                            s = Math.Acos((pt[i].X - workset[j].center.X) / dis) * 180 / Math.PI;
                            if ((pt[i].Y - workset[j].center.Y) > 0)
                            {
                                s = 360 - s;
                            }
                            jd[0, j] = s;
                            jd[1, j] = j;
                        }
                        for (m = 0; m < workset.Count - 1; m++)
                        {
                            for (n = m + 1; n < workset.Count; n++)
                            {
                                if (jd[0, m] < jd[0, n])
                                {
                                    s = jd[0, m];
                                    jd[0, m] = jd[0, n];
                                    jd[0, n] = s;
                                    s = jd[1, m];
                                    jd[1, m] = jd[1, n];
                                    jd[1, n] = s;
                                }
                            }
                        }//排序
                        for (j = 0; j < workset.Count - 1; j++)
                        g2.DrawLine(new Pen(Color.Blue), workset[Convert.ToInt16(jd[1, j])].center, workset[Convert.ToInt16(jd[1, j + 1])].center);            
                    }
                }
                Graphics graphics = CreateGraphics();
                graphics.DrawImage(dlaybackwind, 0, 0);  
            n = 20;
        }

        private void Form2_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            泰森多边形ToolStripMenuItem.Checked = false;
            delaunayToolStripMenuItem1.Checked = false;
            dlay.Clear();
            updateview();
        }

    }
}
