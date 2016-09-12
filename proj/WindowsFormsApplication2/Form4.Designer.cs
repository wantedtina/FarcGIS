namespace WindowsFormsApplication2
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AddLayer = new System.Windows.Forms.ToolStripButton();
            this.Save = new System.Windows.Forms.ToolStripButton();
            this.ZoomOut = new System.Windows.Forms.ToolStripButton();
            this.ZoomIn = new System.Windows.Forms.ToolStripButton();
            this.Find = new System.Windows.Forms.ToolStripButton();
            this.Select = new System.Windows.Forms.ToolStripButton();
            this.Pan = new System.Windows.Forms.ToolStripButton();
            this.FullExtent = new System.Windows.Forms.ToolStripButton();
            this.Distance = new System.Windows.Forms.ToolStripButton();
            this.Delaunay = new System.Windows.Forms.ToolStripButton();
            this.Tyson = new System.Windows.Forms.ToolStripButton();
            this.Clear = new System.Windows.Forms.ToolStripButton();
            this.MY = new System.Windows.Forms.ToolStripButton();
            this.psc = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1Layer = new System.Windows.Forms.TabPage();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.tabPage2SX = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1Layer.SuspendLayout();
            this.tabPage2SX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.帮助ToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(704, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(48, 25);
            this.文件ToolStripMenuItem.Text = "File";
            this.文件ToolStripMenuItem.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.文件ToolStripMenuItem.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(51, 25);
            this.编辑ToolStripMenuItem.Text = "Edit";
            this.编辑ToolStripMenuItem.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.编辑ToolStripMenuItem.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(57, 25);
            this.帮助ToolStripMenuItem.Text = "Help";
            this.帮助ToolStripMenuItem.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.帮助ToolStripMenuItem.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(69, 25);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.aboutToolStripMenuItem.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddLayer,
            this.Save,
            this.ZoomOut,
            this.ZoomIn,
            this.Find,
            this.Select,
            this.Pan,
            this.FullExtent,
            this.Distance,
            this.Delaunay,
            this.Tyson,
            this.Clear,
            this.MY,
            this.psc});
            this.toolStrip1.Location = new System.Drawing.Point(0, 29);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(704, 32);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AddLayer
            // 
            this.AddLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddLayer.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddLayer.Image = ((System.Drawing.Image)(resources.GetObject("AddLayer.Image")));
            this.AddLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddLayer.Name = "AddLayer";
            this.AddLayer.Size = new System.Drawing.Size(29, 29);
            this.AddLayer.Text = "AddLayer";
            this.AddLayer.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.AddLayer.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.AddLayer.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // Save
            // 
            this.Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Save.Image = ((System.Drawing.Image)(resources.GetObject("Save.Image")));
            this.Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(29, 29);
            this.Save.Text = "Save";
            this.Save.Click += new System.EventHandler(this.Save_Click);
            this.Save.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.Save.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // ZoomOut
            // 
            this.ZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOut.Image")));
            this.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOut.Name = "ZoomOut";
            this.ZoomOut.Size = new System.Drawing.Size(29, 29);
            this.ZoomOut.Text = "ZoomOut";
            this.ZoomOut.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.ZoomOut.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.ZoomOut.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // ZoomIn
            // 
            this.ZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("ZoomIn.Image")));
            this.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.Size = new System.Drawing.Size(29, 29);
            this.ZoomIn.Text = "ZoomIn";
            this.ZoomIn.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.ZoomIn.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.ZoomIn.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // Find
            // 
            this.Find.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Find.Image = ((System.Drawing.Image)(resources.GetObject("Find.Image")));
            this.Find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Find.Name = "Find";
            this.Find.Size = new System.Drawing.Size(29, 29);
            this.Find.Text = "Find";
            this.Find.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.Find.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.Find.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // Select
            // 
            this.Select.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Select.Image = ((System.Drawing.Image)(resources.GetObject("Select.Image")));
            this.Select.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(29, 29);
            this.Select.Text = "Select";
            this.Select.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.Select.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.Select.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // Pan
            // 
            this.Pan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Pan.Image = ((System.Drawing.Image)(resources.GetObject("Pan.Image")));
            this.Pan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Pan.Name = "Pan";
            this.Pan.Size = new System.Drawing.Size(29, 29);
            this.Pan.Text = "Pan";
            this.Pan.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.Pan.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.Pan.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // FullExtent
            // 
            this.FullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FullExtent.Image = ((System.Drawing.Image)(resources.GetObject("FullExtent.Image")));
            this.FullExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FullExtent.Name = "FullExtent";
            this.FullExtent.Size = new System.Drawing.Size(29, 29);
            this.FullExtent.Text = "FullExtent";
            this.FullExtent.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.FullExtent.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.FullExtent.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // Distance
            // 
            this.Distance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Distance.Image = ((System.Drawing.Image)(resources.GetObject("Distance.Image")));
            this.Distance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Distance.Name = "Distance";
            this.Distance.Size = new System.Drawing.Size(29, 29);
            this.Distance.Text = "Distance";
            this.Distance.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.Distance.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.Distance.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // Delaunay
            // 
            this.Delaunay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Delaunay.Image = ((System.Drawing.Image)(resources.GetObject("Delaunay.Image")));
            this.Delaunay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Delaunay.Name = "Delaunay";
            this.Delaunay.Size = new System.Drawing.Size(29, 29);
            this.Delaunay.Text = "Delaunay";
            this.Delaunay.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.Delaunay.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.Delaunay.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // Tyson
            // 
            this.Tyson.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Tyson.Image = ((System.Drawing.Image)(resources.GetObject("Tyson.Image")));
            this.Tyson.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tyson.Name = "Tyson";
            this.Tyson.Size = new System.Drawing.Size(29, 29);
            this.Tyson.Text = "Tyson";
            this.Tyson.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.Tyson.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.Tyson.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // Clear
            // 
            this.Clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Clear.Image = ((System.Drawing.Image)(resources.GetObject("Clear.Image")));
            this.Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(29, 29);
            this.Clear.Text = "Clear";
            this.Clear.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.Clear.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.Clear.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // MY
            // 
            this.MY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MY.Image = ((System.Drawing.Image)(resources.GetObject("MY.Image")));
            this.MY.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MY.Name = "MY";
            this.MY.Size = new System.Drawing.Size(29, 29);
            this.MY.Text = "MY";
            this.MY.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.MY.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.MY.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // psc
            // 
            this.psc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.psc.Image = ((System.Drawing.Image)(resources.GetObject("psc.Image")));
            this.psc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.psc.Name = "psc";
            this.psc.Size = new System.Drawing.Size(29, 29);
            this.psc.Text = "ExportMap";
            this.psc.Click += new System.EventHandler(this.ToolStrimButton_Click);
            this.psc.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.psc.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 398);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(704, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 61);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(704, 337);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1Layer);
            this.tabControl1.Controls.Add(this.tabPage2SX);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(200, 337);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1Layer
            // 
            this.tabPage1Layer.Controls.Add(this.radioButton11);
            this.tabPage1Layer.Controls.Add(this.radioButton10);
            this.tabPage1Layer.Controls.Add(this.radioButton9);
            this.tabPage1Layer.Controls.Add(this.radioButton8);
            this.tabPage1Layer.Controls.Add(this.button3);
            this.tabPage1Layer.Controls.Add(this.button2);
            this.tabPage1Layer.Controls.Add(this.button1);
            this.tabPage1Layer.Controls.Add(this.radioButton7);
            this.tabPage1Layer.Controls.Add(this.radioButton6);
            this.tabPage1Layer.Controls.Add(this.radioButton5);
            this.tabPage1Layer.Controls.Add(this.radioButton4);
            this.tabPage1Layer.Controls.Add(this.radioButton3);
            this.tabPage1Layer.Controls.Add(this.radioButton2);
            this.tabPage1Layer.Controls.Add(this.radioButton1);
            this.tabPage1Layer.Location = new System.Drawing.Point(4, 22);
            this.tabPage1Layer.Name = "tabPage1Layer";
            this.tabPage1Layer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1Layer.Size = new System.Drawing.Size(192, 311);
            this.tabPage1Layer.TabIndex = 0;
            this.tabPage1Layer.Text = "图层";
            this.tabPage1Layer.UseVisualStyleBackColor = true;
            // 
            // radioButton11
            // 
            this.radioButton11.AutoSize = true;
            this.radioButton11.Location = new System.Drawing.Point(12, 232);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(101, 16);
            this.radioButton11.TabIndex = 13;
            this.radioButton11.TabStop = true;
            this.radioButton11.Text = "radioButton11";
            this.radioButton11.UseVisualStyleBackColor = true;
            this.radioButton11.Visible = false;
            this.radioButton11.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Location = new System.Drawing.Point(12, 210);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(101, 16);
            this.radioButton10.TabIndex = 12;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "radioButton10";
            this.radioButton10.UseVisualStyleBackColor = true;
            this.radioButton10.Visible = false;
            this.radioButton10.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Location = new System.Drawing.Point(13, 188);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(95, 16);
            this.radioButton9.TabIndex = 11;
            this.radioButton9.TabStop = true;
            this.radioButton9.Text = "radioButton9";
            this.radioButton9.UseVisualStyleBackColor = true;
            this.radioButton9.Visible = false;
            this.radioButton9.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new System.Drawing.Point(13, 166);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(95, 16);
            this.radioButton8.TabIndex = 10;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "radioButton8";
            this.radioButton8.UseVisualStyleBackColor = true;
            this.radioButton8.Visible = false;
            this.radioButton8.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(121, 256);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(55, 47);
            this.button3.TabIndex = 9;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.button3.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(72, 256);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 47);
            this.button2.TabIndex = 8;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.button2.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(21, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 47);
            this.button1.TabIndex = 7;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.button1.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(12, 144);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(95, 16);
            this.radioButton7.TabIndex = 6;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "radioButton7";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.Visible = false;
            this.radioButton7.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(13, 122);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(95, 16);
            this.radioButton6.TabIndex = 5;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "radioButton6";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.Visible = false;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(12, 100);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(95, 16);
            this.radioButton5.TabIndex = 4;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "radioButton5";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.Visible = false;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(13, 78);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(95, 16);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "radioButton4";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Visible = false;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(13, 56);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(95, 16);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "radioButton3";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Visible = false;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(13, 34);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Visible = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(95, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // tabPage2SX
            // 
            this.tabPage2SX.Controls.Add(this.dataGridView1);
            this.tabPage2SX.Location = new System.Drawing.Point(4, 22);
            this.tabPage2SX.Name = "tabPage2SX";
            this.tabPage2SX.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2SX.Size = new System.Drawing.Size(192, 311);
            this.tabPage2SX.TabIndex = 1;
            this.tabPage2SX.Text = "属性";
            this.tabPage2SX.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(186, 305);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 337);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 200;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 420);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATGIS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form4_FormClosing);
            this.Load += new System.EventHandler(this.Form4_Load);
            this.SizeChanged += new System.EventHandler(this.Form4_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1Layer.ResumeLayout(false);
            this.tabPage1Layer.PerformLayout();
            this.tabPage2SX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton AddLayer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1Layer;
        private System.Windows.Forms.TabPage tabPage2SX;
        private System.Windows.Forms.ToolStripButton Save;
        private System.Windows.Forms.ToolStripButton ZoomOut;
        private System.Windows.Forms.ToolStripButton ZoomIn;
        private System.Windows.Forms.ToolStripButton Distance;
        private System.Windows.Forms.ToolStripButton Delaunay;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripButton Tyson;
        private System.Windows.Forms.ToolStripButton Select;
        private System.Windows.Forms.ToolStripButton Pan;
        private System.Windows.Forms.ToolStripButton MY;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripButton FullExtent;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripButton Find;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton Clear;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton11;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.ToolStripButton psc;
    }
}