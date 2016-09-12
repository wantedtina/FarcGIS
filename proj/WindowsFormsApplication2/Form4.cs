using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyGIS;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form4 : Form
    {
        int hh,tcs=0;
        List<Delaunay3> dlay = new List<Delaunay3>();
        int i, j, k, m, q;
        enum Direction { unused, up, down, left, right }
        Direction dir = Direction.unused;
        Boolean op1 = false, op2 = false;
        int n = 20, b1;
        Cursor yl;
        Graphics g2;
        GISDocument document = new GISDocument();
        GISView view = null;
        private Bitmap backwindow, dlaybackwind;
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
        RadioButton[] radiob=new RadioButton[15];
        int[] tcgl = new int[20];
        Form5 f;
        Boolean need;
        public Form4(Form5 frm)
        {
            f = frm;
            InitializeComponent();
            tcgl[0] = 0;
            view = new GISView(new GISExtent(new GISVertex(0, 0), new GISVertex(100, 100)),
                    pictureBox1.ClientRectangle);
            yl = this.Cursor;
            ptA[0].X = pictureBox1.Width / 2;
            ptA[0].Y = -Convert.ToSingle(ptA[0].X / Math.Sqrt(3) * 2);
            ptA[1].X = -ptA[0].X;
            ptA[1].Y = pictureBox1.Height;
            ptA[2].X = pictureBox1.Width + ptA[0].X;
            ptA[2].Y = pictureBox1.Height - 2;
            ((Control)pictureBox1).MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);
        }

        void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Location.X > 0 && e.Location.X < pictureBox1.Width && e.Location.Y > 0 && e.Location.Y < pictureBox1.Height)
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
                    if (Delaunay.Checked || Tyson.Checked)
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
            view.SetValue(view.CurrentMapExtent, pictureBox1.ClientRectangle);
            if (backwindow != null) backwindow.Dispose();
            backwindow = new Bitmap(pictureBox1.ClientRectangle.Width, pictureBox1.ClientRectangle.Height);
            Graphics g = Graphics.FromImage(backwindow);
            g.FillRectangle(new SolidBrush(Color.White), pictureBox1.ClientRectangle);
            document.draw(g, view);
            Graphics graphics = this.pictureBox1.CreateGraphics();
            graphics.DrawImage(backwindow, 0, 0);
        }
        public void updateview2()
        {
            if (Delaunay.Checked && dlay.Count > 0)
                Delaunay.PerformClick();
            if (Tyson.Checked && dlay.Count > 0)
                Tyson.PerformClick();
        }
        private void ToolStrimButton_Click(object sender, EventArgs e)
        {
            //if (sender.Equals(Properties))
            //{
            //    if (layerDialog == null)
            //    layerDialog.Show();
            //    if (layerDialog.WindowState == FormWindowState.Minimized)
            //        layerDialog.WindowState = FormWindowState.Normal;
            //    layerDialog.BringToFront();
            //}
            if (sender.Equals(Clear))
            {
                Tyson.Checked = false;
                Delaunay.Checked = false;
                updateview();
            }
            if (sender.Equals(AddLayer))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Shapefile文件|*.shp";
                openFileDialog.RestoreDirectory = false;
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                document.AddLayer(GISShapefile.ReadShapeFile(openFileDialog.FileName));
                // MessageBox.Show(Path.GetFileNameWithoutExtension(openFileDialog.FileName));
                {
                    tcs += 1;
                    tcgl[tcs] = tcs;
                    radiob[tcs].Visible = true;
                    radiob[tcs].Checked = true;
                    radiob[tcs].Text = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                }
                FillValue(document.layers[document.layers.Count - 1]);
                if (document.layers.Count == 1)
                    view.CurrentMapExtent.CopyFrom(document.extent);
                updateview();
                if (layerDialog != null) layerDialog.UpdateLayer();
            }
            //else if (sender.Equals(RemoveLayer))
            //{
            //    //toolStripComboBox1.Items.Clear();
            //    //for (int i = 0; i < document.layers.Count; i++)
            //    //    toolStripComboBox1.Items.Add(document.layers[i].Name);
            //}
            else if (sender.Equals(FullExtent))
            {
                view.CurrentMapExtent.CopyFrom(document.extent);
                if (Delaunay.Checked || Tyson.Checked)
                    updateview2();
                else
                    updateview();
            }
            else
            {
                ZoomIn.Checked = false;
                ZoomOut.Checked = false;
                Select.Checked = false;
                Pan.Checked = false;
                Distance.Checked = false;
                if ((ToolStripButton)sender != MY && (ToolStripButton)sender != Clear && (ToolStripButton)sender != psc)
                    ((ToolStripButton)sender).Checked = true;
                if (sender.Equals(Select))
                {
                    MouseCommand = MOUSECOMMAND.Select;
                }
                else if (sender.Equals(ZoomIn))
                {
                    MouseCommand = MOUSECOMMAND.ZoomIn;
                }
                else if (sender.Equals(ZoomOut))
                {
                    MouseCommand = MOUSECOMMAND.ZoomOut;
                }
                else if (sender.Equals(Pan))
                {
                    MouseCommand = MOUSECOMMAND.Pan;
                }
                else if (sender.Equals(Distance))
                {
                    MouseCommand = MOUSECOMMAND.Dis;
                }
                else if (sender.Equals(MY))
                {
                    if (MY.Checked == false)
                    {
                        MouseCommand = MOUSECOMMAND.MY;
                        MY.Checked = true;
                    }
                    else
                    {
                        MouseCommand = MOUSECOMMAND.Unused;
                        MY.Checked = false;
                    }
                }
                else if (sender.Equals(Delaunay))
                {
                    DrawDelaunay();
                }
                else if (sender.Equals(Tyson))
                {
                    DrawTyson();
                }
                else if (sender.Equals(psc))
                {
                    PrintScreen();
                }
            }
        }
        private void PrintScreen()
        {
            Bitmap bit1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bit1, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            int border = (pictureBox1.Width - pictureBox1.ClientSize.Width) / 2;
            int caption = (pictureBox1.Height - pictureBox1.ClientSize.Height) - border;
            Bitmap bit2 = bit1.Clone(new Rectangle(border, caption, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            bit2.Save("E:\\OutPut.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            bit1.Dispose();
            bit2.Dispose();
            MessageBox.Show("默认保存在E盘根目录");
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseStartX = e.X;
            MouseStartY = e.Y;
            if (e.Button == MouseButtons.Left && MouseCommand != MOUSECOMMAND.Unused)
            {
                MouseOnMap = true;
            }
            //else if (e.Button == MouseButtons.Right)
            //{
            //    contextMenuStrip1.Show(this.PointToScreen(new Point(e.X, e.Y)));
            //}
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMovingX = e.X;
            MouseMovingY = e.Y;
            toolStripStatusLabel1.Text = "ScreenCoordinate:" + e.X.ToString() + " , " + e.Y.ToString();
            GISVertex v = view.ToMapVertex(new Point(e.X, e.Y));
            toolStripStatusLabel1.Text += "    " + "MapCoordinate:" + Math.Round(v.x, 2).ToString() + " , " + Math.Round(v.y, 2).ToString();
            if (MouseCommand == MOUSECOMMAND.MY)
            {
                if (e.X < n && e.X > 0)
                {
                    dir = Direction.left;
                    if (op1 == false)
                    {
                        op1 = true;
                        timer1.Start();
                    }
                    pictureBox1.Cursor = System.Windows.Forms.Cursors.PanWest;
                }
                if (e.X > pictureBox1.Width - n)
                {
                    dir = Direction.right;
                    if (op1 == false)
                    {
                        op1 = true;
                        timer1.Start();
                    }
                    pictureBox1.Cursor = System.Windows.Forms.Cursors.PanEast;
                }
                if (e.Y < n && e.Y > 0)
                {
                    dir = Direction.up;
                    if (op2 == false)
                    {
                        op2 = true;
                        timer2.Start();
                    }
                    pictureBox1.Cursor = System.Windows.Forms.Cursors.PanNorth;
                }
                if (e.Y > pictureBox1.Height - n)
                {
                    dir = Direction.down;
                    if (op2 == false)
                    {
                        op2 = true;
                        timer2.Start();
                    }
                    pictureBox1.Cursor = System.Windows.Forms.Cursors.PanSouth;
                }
                if ((e.X >= n && e.X <= pictureBox1.Width - n && e.Y >= n && e.Y <= pictureBox1.Height - n) || e.X < 0 || e.Y < 0 || e.X > pictureBox1.Width || e.Y > pictureBox1.Height)
                {
                    op1 = false;
                    op2 = false;
                    timer1.Stop();
                    timer2.Stop();
                    pictureBox1.Cursor = yl;
                }
            }
            else
            {
                pictureBox1.Cursor = yl;
                timer1.Stop();
                timer2.Stop();
            }
            if (MouseOnMap)
            {
                Invalidate();
            }
            if ((MouseCommand == MOUSECOMMAND.ZoomIn || MouseCommand == MOUSECOMMAND.ZoomOut)&&MouseOnMap)
            {
                view.SetValue(view.CurrentMapExtent, pictureBox1.ClientRectangle);
                if (backwindow != null) backwindow.Dispose();
                backwindow = new Bitmap(pictureBox1.ClientRectangle.Width, pictureBox1.ClientRectangle.Height);
                Graphics g = Graphics.FromImage(backwindow);
                g.FillRectangle(new SolidBrush(Color.White), pictureBox1.ClientRectangle);
                document.draw(g, view);
                g.FillRectangle(new SolidBrush(Color.FromArgb(30,0,0,0)),new Rectangle(
                                Math.Min(e.X, MouseStartX),
                                Math.Min(e.Y, MouseStartY),
                                Math.Abs(e.X - MouseStartX),
                                Math.Abs(e.Y - MouseStartY)));
                Graphics graphics = this.pictureBox1.CreateGraphics();
                graphics.DrawImage(backwindow, 0, 0);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseOnMap)
            {
                MouseOnMap = false;
                switch (MouseCommand)
                {
                    case MOUSECOMMAND.Select:
                        for (int i = 0; i < document.layers.Count; i++)
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
                        if (Delaunay.Checked || Tyson.Checked)
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
                        if (Delaunay.Checked || Tyson.Checked)
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
                        if (Delaunay.Checked && dlay.Count > 0)
                            Delaunay.PerformClick();
                        if (Tyson.Checked && dlay.Count > 0)
                            Tyson.PerformClick();
                        break;
                    case MOUSECOMMAND.Dis:
                        GISVertex v1 = view.ToMapVertex(new Point(MouseStartX, MouseStartY));
                        GISVertex v2 = view.ToMapVertex(new Point(e.X, e.Y));
                        double dis = Math.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y));
                        MessageBox.Show(dis.ToString());
                        break;
                }
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
                    //ptA[0].X = pictureBox1.Width / 2;
                    //ptA[0].Y = -Convert.ToSingle(ptA[0].X / Math.Sqrt(3) * 2);
                    //ptA[1].X = -ptA[0].X;
                    //ptA[1].Y = pictureBox1.Height;
                    //ptA[2].X = pictureBox1.Width + ptA[0].X;
                    //ptA[2].Y = pictureBox1.Height - 5;
                    float xmax, xmin, ymax, ymin, dx, dy, ddx, ddy;
                    xmin = xmax = pt[0].X;
                    ymin = ymax = pt[0].Y;
                    for (j = 1; j < pt.Count; j++)
                    {
                        if (xmin > pt[j].X) xmin = pt[j].X;
                        if (xmax < pt[j].X) xmax = pt[j].X;
                        if (ymin > pt[j].Y) ymin = pt[j].Y;
                        if (ymax < pt[j].Y) ymax = pt[j].Y;
                    }
                    dx = xmax - xmin;
                    dy = ymax - ymin;
                    ddx = dx / 100;
                    ddy = dy / 100;
                    xmin -= ddx;
                    xmax += ddx;
                    dx += 2 * ddx;
                    ymin -= ddy;
                    ymax += ddy;
                    dy += 2 * ddy;
                    ptA[0].X = xmin - dy * Convert.ToSingle(Math.Sqrt(3) / 3);
                    ptA[0].Y = ymin;
                    ptA[1].X = xmax + dy * Convert.ToSingle(Math.Sqrt(3) / 3);
                    ptA[1].Y = ymin;
                    ptA[2].X = (xmin + xmax) / 2;
                    ptA[2].Y = xmax + dy * Convert.ToSingle(Math.Sqrt(3) / 2);
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
                }
                if (document.layers[i].ShapeType == SHAPETYPE.POINT)
                    break;
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.Focus();
        }
        private void DrawTyson()
        {
            Delaunay.Checked = false;
            Tyson.Checked = true;
            //MouseCommand = MOUSECOMMAND.TS;
            try
            {
                delaunay();
                if (dlaybackwind != null) dlaybackwind.Dispose();
                dlaybackwind = new Bitmap(pictureBox1.ClientRectangle.Width, pictureBox1.ClientRectangle.Height);
                g2 = Graphics.FromImage(dlaybackwind);
                g2.FillRectangle(new SolidBrush(Color.White), pictureBox1.ClientRectangle);
                document.draw(g2, view);
                double dis;
                double s;
                for (m = 0; m < 50; m++)
                    sjbh[m, 0] = 0;
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
                Graphics graphics = pictureBox1.CreateGraphics();
                graphics.DrawImage(dlaybackwind, 0, 0);
                n = 20;
            }
            catch
            { }
        }
        public void DrawDelaunay()
        {
            Delaunay.Checked = true;
            Tyson.Checked = false;
            try
            {
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
                dlaybackwind = new Bitmap(pictureBox1.ClientRectangle.Width, pictureBox1.ClientRectangle.Height);
                g2 = Graphics.FromImage(dlaybackwind);
                g2.FillRectangle(new SolidBrush(Color.White), pictureBox1.ClientRectangle);
                document.draw(g2, view);
                for (k = 0; k < dlay.Count; k++)
                    dlay[k].drawsjx(g2, Color.Green);
                Graphics graphics = pictureBox1.CreateGraphics();
                graphics.DrawImage(dlaybackwind, 0, 0);
            }
            catch
            { }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
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
            if (Delaunay.Checked || Tyson.Checked)
                updateview2();
            else
                updateview();
        }

        private void Form4_SizeChanged(object sender, EventArgs e)
        {
            int h = tabPage1Layer.Height;
            button1.Top += h - hh;
            button2.Top += h - hh;
            button3.Top += h - hh;
            hh = h;
            if (Delaunay.Checked || Tyson.Checked)
                updateview2();
            else
                updateview();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (Delaunay.Checked || Tyson.Checked)
                updateview2();
            else
                updateview();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dir == Direction.left)
            {
                view.CurrentMapExtent.MoveRight();
                if (Delaunay.Checked || Tyson.Checked)
                    updateview2();
                else
                    updateview();
            }
            if (dir == Direction.right)
            {
                view.CurrentMapExtent.MoveLeft();
                if (Delaunay.Checked || Tyson.Checked)
                    updateview2();
                else
                    updateview();
            }
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            hh = tabPage1Layer.Height;
            radiob[1] = radioButton1;
            radiob[2] = radioButton2;
            radiob[3] = radioButton3;
            radiob[4] = radioButton4;
            radiob[5] = radioButton5;
            radiob[6] = radioButton6;
            radiob[7] = radioButton7;
            radiob[8] = radioButton8;
            radiob[9] = radioButton9;
            radiob[10] = radioButton10;
            radiob[11] = radioButton11;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (dir == Direction.up)
            {
                view.CurrentMapExtent.MoveDown();
                if (Delaunay.Checked || Tyson.Checked)
                    updateview2();
                else
                    updateview();
            }
            if (dir == Direction.down)
            {
                view.CurrentMapExtent.MoveUp();
                if (Delaunay.Checked || Tyson.Checked)
                    updateview2();
                else
                    updateview();
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            GISLayer layer = document.layers[document.layers.Count - 1];
            layer.ClearSelection();
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                layer.Features[(int)(dataGridView1.SelectedRows[i].Cells[0].Value)].Selected = true;
            }
            updateview();
        }

        public void UpdateSelection()
        {
            GISLayer layer = document.layers[j-1];
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int index = (int)(dataGridView1.Rows[i].Cells[0].Value);
                dataGridView1.Rows[i].Selected = layer.Features[index].Selected;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GISLayer layer;
            string s;
            for (m = 1; m < tcs+1; m++)
            {
                if (radiob[m].Checked == true)
                    break;
            }
            if (m != 1)
            {
                layer = document.layers[m - 1];
                document.layers[m - 1] = document.layers[m - 2];
                document.layers[m - 2] = layer;
                s = radiob[m].Text;
                radiob[m].Text = radiob[m-1].Text;
                radiob[m - 1].Text = s;
                radiob[m - 1].Checked = true;
            }
            updateview();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GISLayer layer;
            string s;
            for (m = 1; m < tcs + 1; m++)
            {
                if (radiob[m].Checked == true)
                    break;
            }
            if (m != tcs)
            {
                s = radiob[m].Text;
                radiob[m].Text = radiob[m+ 1].Text;
                radiob[m +1].Text = s;
                layer = document.layers[m - 1];
                document.layers[m-1] = document.layers[m];
                document.layers[m] = layer;
                radiob[m+ 1].Checked = true;
            }
            updateview();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GISLayer layer;
            for (m = 1; m < tcs + 1; m++)
            {
                if (radiob[m].Checked == true)
                    break;
            }
            layer = document.layers[m - 1];
            document.RemoveLayer(layer);
            for (k = m; k < tcs; k++)
            {
                radiob[k].Text = radiob[k + 1].Text;
            }
            radiob[tcs].Visible = false;
            tcs--;
            if(m==tcs+1&&m!=1)
            radiob[m-1].Checked = true;
            else
                radiob[m].Checked = true;
            updateview();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            for (k = 1; k < tcs + 1; k++)
                if (radiob[k].Checked)
                    break;
            FillValue(document.layers[k - 1]);
        }

        private void MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            MessageBox.Show("尚未完成");
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            f.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        
    }
}