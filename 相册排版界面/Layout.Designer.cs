namespace 相册排版界面
{
    partial class Layout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Layout));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.从文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.从图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批量处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.单张ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.单张转90度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.两张合并ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.两张合并转90度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.四张合并ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.四行合并转90度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.八张合并ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.八张合并转90度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更换当前模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.单张ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.单张转90度ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.两张合并ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.两张合并转90度ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.四张合并ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.四张合并转90度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加入文字ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.调整图片大小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更换图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jpg格式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pdf格式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.left = new System.Windows.Forms.Button();
            this.right = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.八张合并ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.八张合并转90度ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(29, 29);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(29, 29);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.打开ToolStripMenuItem,
            this.添加图片ToolStripMenuItem,
            this.批量处理ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.导出ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1316, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(14, 24);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件夹ToolStripMenuItem,
            this.图片ToolStripMenuItem});
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.打开ToolStripMenuItem.Text = "打开";
            // 
            // 文件夹ToolStripMenuItem
            // 
            this.文件夹ToolStripMenuItem.Name = "文件夹ToolStripMenuItem";
            this.文件夹ToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.文件夹ToolStripMenuItem.Text = "文件夹";
            this.文件夹ToolStripMenuItem.Click += new System.EventHandler(this.文件夹ToolStripMenuItem_Click);
            // 
            // 图片ToolStripMenuItem
            // 
            this.图片ToolStripMenuItem.Name = "图片ToolStripMenuItem";
            this.图片ToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.图片ToolStripMenuItem.Text = "图片";
            this.图片ToolStripMenuItem.Click += new System.EventHandler(this.图片ToolStripMenuItem_Click);
            // 
            // 添加图片ToolStripMenuItem
            // 
            this.添加图片ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.从文件夹ToolStripMenuItem,
            this.从图片ToolStripMenuItem});
            this.添加图片ToolStripMenuItem.Name = "添加图片ToolStripMenuItem";
            this.添加图片ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.添加图片ToolStripMenuItem.Text = "添加图片";
            // 
            // 从文件夹ToolStripMenuItem
            // 
            this.从文件夹ToolStripMenuItem.Name = "从文件夹ToolStripMenuItem";
            this.从文件夹ToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.从文件夹ToolStripMenuItem.Text = "文件夹";
            this.从文件夹ToolStripMenuItem.Click += new System.EventHandler(this.从文件夹ToolStripMenuItem_Click);
            // 
            // 从图片ToolStripMenuItem
            // 
            this.从图片ToolStripMenuItem.Name = "从图片ToolStripMenuItem";
            this.从图片ToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.从图片ToolStripMenuItem.Text = "图片";
            this.从图片ToolStripMenuItem.Click += new System.EventHandler(this.从图片ToolStripMenuItem_Click);
            // 
            // 批量处理ToolStripMenuItem
            // 
            this.批量处理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.单张ToolStripMenuItem,
            this.单张转90度ToolStripMenuItem,
            this.两张合并ToolStripMenuItem,
            this.两张合并转90度ToolStripMenuItem,
            this.四张合并ToolStripMenuItem,
            this.四行合并转90度ToolStripMenuItem,
            this.八张合并ToolStripMenuItem,
            this.八张合并转90度ToolStripMenuItem});
            this.批量处理ToolStripMenuItem.Name = "批量处理ToolStripMenuItem";
            this.批量处理ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.批量处理ToolStripMenuItem.Text = "批量更改";
            // 
            // 单张ToolStripMenuItem
            // 
            this.单张ToolStripMenuItem.Name = "单张ToolStripMenuItem";
            this.单张ToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.单张ToolStripMenuItem.Text = "单张";
            this.单张ToolStripMenuItem.Click += new System.EventHandler(this.单张ToolStripMenuItem_Click);
            // 
            // 单张转90度ToolStripMenuItem
            // 
            this.单张转90度ToolStripMenuItem.Name = "单张转90度ToolStripMenuItem";
            this.单张转90度ToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.单张转90度ToolStripMenuItem.Text = "单张（转90度）";
            this.单张转90度ToolStripMenuItem.Click += new System.EventHandler(this.单张转90度ToolStripMenuItem_Click);
            // 
            // 两张合并ToolStripMenuItem
            // 
            this.两张合并ToolStripMenuItem.Name = "两张合并ToolStripMenuItem";
            this.两张合并ToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.两张合并ToolStripMenuItem.Text = "两张合并";
            this.两张合并ToolStripMenuItem.Click += new System.EventHandler(this.两张合并ToolStripMenuItem_Click);
            // 
            // 两张合并转90度ToolStripMenuItem
            // 
            this.两张合并转90度ToolStripMenuItem.Name = "两张合并转90度ToolStripMenuItem";
            this.两张合并转90度ToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.两张合并转90度ToolStripMenuItem.Text = "两张合并（转90度）";
            this.两张合并转90度ToolStripMenuItem.Click += new System.EventHandler(this.两张合并转90度ToolStripMenuItem_Click);
            // 
            // 四张合并ToolStripMenuItem
            // 
            this.四张合并ToolStripMenuItem.Name = "四张合并ToolStripMenuItem";
            this.四张合并ToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.四张合并ToolStripMenuItem.Text = "四张合并";
            this.四张合并ToolStripMenuItem.Click += new System.EventHandler(this.四张合并ToolStripMenuItem_Click);
            // 
            // 四行合并转90度ToolStripMenuItem
            // 
            this.四行合并转90度ToolStripMenuItem.Name = "四行合并转90度ToolStripMenuItem";
            this.四行合并转90度ToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.四行合并转90度ToolStripMenuItem.Text = "四张合并（转90度）";
            this.四行合并转90度ToolStripMenuItem.Click += new System.EventHandler(this.四行合并转90度ToolStripMenuItem_Click);
            // 
            // 八张合并ToolStripMenuItem
            // 
            this.八张合并ToolStripMenuItem.Name = "八张合并ToolStripMenuItem";
            this.八张合并ToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.八张合并ToolStripMenuItem.Text = "八张合并";
            this.八张合并ToolStripMenuItem.Click += new System.EventHandler(this.八张合并ToolStripMenuItem_Click);
            // 
            // 八张合并转90度ToolStripMenuItem
            // 
            this.八张合并转90度ToolStripMenuItem.Name = "八张合并转90度ToolStripMenuItem";
            this.八张合并转90度ToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.八张合并转90度ToolStripMenuItem.Text = "八张合并（转90度）";
            this.八张合并转90度ToolStripMenuItem.Click += new System.EventHandler(this.八张合并转90度ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.更换当前模板ToolStripMenuItem,
            this.加入文字ToolStripMenuItem,
            this.调整图片大小ToolStripMenuItem,
            this.更换图片ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 更换当前模板ToolStripMenuItem
            // 
            this.更换当前模板ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.单张ToolStripMenuItem1,
            this.单张转90度ToolStripMenuItem1,
            this.两张合并ToolStripMenuItem1,
            this.两张合并转90度ToolStripMenuItem1,
            this.四张合并ToolStripMenuItem1,
            this.四张合并转90度ToolStripMenuItem1,
            this.八张合并ToolStripMenuItem1,
            this.八张合并转90度ToolStripMenuItem1});
            this.更换当前模板ToolStripMenuItem.Name = "更换当前模板ToolStripMenuItem";
            this.更换当前模板ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.更换当前模板ToolStripMenuItem.Text = "更换当前模板";
            // 
            // 单张ToolStripMenuItem1
            // 
            this.单张ToolStripMenuItem1.Name = "单张ToolStripMenuItem1";
            this.单张ToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.单张ToolStripMenuItem1.Text = "单张";
            this.单张ToolStripMenuItem1.Click += new System.EventHandler(this.单张ToolStripMenuItem1_Click);
            // 
            // 单张转90度ToolStripMenuItem1
            // 
            this.单张转90度ToolStripMenuItem1.Name = "单张转90度ToolStripMenuItem1";
            this.单张转90度ToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.单张转90度ToolStripMenuItem1.Text = "单张转90度";
            this.单张转90度ToolStripMenuItem1.Click += new System.EventHandler(this.单张转90度ToolStripMenuItem1_Click);
            // 
            // 两张合并ToolStripMenuItem1
            // 
            this.两张合并ToolStripMenuItem1.Name = "两张合并ToolStripMenuItem1";
            this.两张合并ToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.两张合并ToolStripMenuItem1.Text = "两张合并";
            this.两张合并ToolStripMenuItem1.Click += new System.EventHandler(this.两张合并ToolStripMenuItem1_Click);
            // 
            // 两张合并转90度ToolStripMenuItem1
            // 
            this.两张合并转90度ToolStripMenuItem1.Name = "两张合并转90度ToolStripMenuItem1";
            this.两张合并转90度ToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.两张合并转90度ToolStripMenuItem1.Text = "两张合并转90度";
            this.两张合并转90度ToolStripMenuItem1.Click += new System.EventHandler(this.两张合并转90度ToolStripMenuItem1_Click);
            // 
            // 四张合并ToolStripMenuItem1
            // 
            this.四张合并ToolStripMenuItem1.Name = "四张合并ToolStripMenuItem1";
            this.四张合并ToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.四张合并ToolStripMenuItem1.Text = "四张合并";
            this.四张合并ToolStripMenuItem1.Click += new System.EventHandler(this.四张合并ToolStripMenuItem1_Click);
            // 
            // 四张合并转90度ToolStripMenuItem1
            // 
            this.四张合并转90度ToolStripMenuItem1.Name = "四张合并转90度ToolStripMenuItem";
            this.四张合并转90度ToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.四张合并转90度ToolStripMenuItem1.Text = "四张合并转90度";
            this.四张合并转90度ToolStripMenuItem1.Click += new System.EventHandler(this.四张合并转90度ToolStripMenuItem1_Click);
            // 
            // 八张合并ToolStripMenuItem1
            // 
            this.八张合并ToolStripMenuItem1.Name = "八张合并ToolStripMenuItem1";
            this.八张合并ToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.八张合并ToolStripMenuItem1.Text = "八张合并";
            this.八张合并ToolStripMenuItem1.Click += new System.EventHandler(this.八张合并ToolStripMenuItem1_Click);
            // 
            // 
            // 八张合并转90度ToolStripMenuItem1
            // 
            this.八张合并转90度ToolStripMenuItem1.Name = "八张合并转90度ToolStripMenuItem";
            this.八张合并转90度ToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.八张合并转90度ToolStripMenuItem1.Text = "八张合并转90度";
            this.八张合并转90度ToolStripMenuItem1.Click += new System.EventHandler(this.八张合并转90度ToolStripMenuItem1_Click);
            // 


            // 加入文字ToolStripMenuItem
            // 
            this.加入文字ToolStripMenuItem.Name = "加入文字ToolStripMenuItem";
            this.加入文字ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.加入文字ToolStripMenuItem.Text = "加入文字";
            this.加入文字ToolStripMenuItem.Click += new System.EventHandler(this.加入文字ToolStripMenuItem_Click);
            // 
            // 调整图片大小ToolStripMenuItem
            // 
            this.调整图片大小ToolStripMenuItem.Name = "调整图片大小ToolStripMenuItem";
            this.调整图片大小ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.调整图片大小ToolStripMenuItem.Text = "调整图片";
            this.调整图片大小ToolStripMenuItem.Click += new System.EventHandler(this.调整图片大小ToolStripMenuItem_Click);
            // 
            // 更换图片ToolStripMenuItem
            // 
            this.更换图片ToolStripMenuItem.Name = "更换图片ToolStripMenuItem";
            this.更换图片ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.更换图片ToolStripMenuItem.Text = "更换图片";
            this.更换图片ToolStripMenuItem.Click += new System.EventHandler(this.更换图片ToolStripMenuItem_Click);
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jpg格式ToolStripMenuItem,
            this.pdf格式ToolStripMenuItem});
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.导出ToolStripMenuItem.Text = "导出相册";
            // 
            // jpg格式ToolStripMenuItem
            // 
            this.jpg格式ToolStripMenuItem.Name = "jpg格式ToolStripMenuItem";
            this.jpg格式ToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.jpg格式ToolStripMenuItem.Text = "导出jpg";
            this.jpg格式ToolStripMenuItem.Click += new System.EventHandler(this.jpg格式ToolStripMenuItem_Click);
            // 
            // pdf格式ToolStripMenuItem
            // 
            this.pdf格式ToolStripMenuItem.Name = "pdf格式ToolStripMenuItem";
            this.pdf格式ToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.pdf格式ToolStripMenuItem.Text = "导出pdf";
            this.pdf格式ToolStripMenuItem.Click += new System.EventHandler(this.pdf格式ToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(68, 194);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1175, 650);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // left
            // 
            this.left.BackColor = System.Drawing.SystemColors.Menu;
            this.left.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("left.BackgroundImage")));
            this.left.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.left.Location = new System.Drawing.Point(9, 451);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(53, 74);
            this.left.TabIndex = 3;
            this.left.UseVisualStyleBackColor = false;
            this.left.Click += new System.EventHandler(this.left_Click);
            // 
            // right
            // 
            this.right.BackColor = System.Drawing.SystemColors.Menu;
            this.right.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("right.BackgroundImage")));
            this.right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.right.Location = new System.Drawing.Point(1249, 451);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(53, 74);
            this.right.TabIndex = 4;
            this.right.UseVisualStyleBackColor = false;
            this.right.Click += new System.EventHandler(this.right_Click);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Control;
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(68, 39);
            this.listView1.Margin = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1175, 145);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(80, 90);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // 八张合并ToolStripMenuItem1
            // 
            this.八张合并ToolStripMenuItem1.Name = "八张合并ToolStripMenuItem1";
            this.八张合并ToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.八张合并ToolStripMenuItem1.Text = "八张合并";
            // 
            // 八张合并转90度ToolStripMenuItem1
            // 
            this.八张合并转90度ToolStripMenuItem1.Name = "八张合并转90度ToolStripMenuItem1";
            this.八张合并转90度ToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.八张合并转90度ToolStripMenuItem1.Text = "八张合并转90度";
            // 
            // Layout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1316, 719);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.right);
            this.Controls.Add(this.left);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Layout";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Layout";
            this.Load += new System.EventHandler(this.Layout_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件夹ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图片ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button left;
        private System.Windows.Forms.Button right;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripMenuItem 批量处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更换当前模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加入文字ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 调整图片大小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jpg格式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pdf格式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更换图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 从文件夹ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 从图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 单张ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 单张转90度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 两张合并ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 两张合并转90度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 四张合并ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 四行合并转90度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 单张ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 单张转90度ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 两张合并ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 两张合并转90度ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 四张合并ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 四张合并转90度ToolStripMenuItem1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 八张合并ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 八张合并转90度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 八张合并ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 八张合并转90度ToolStripMenuItem1;
    }
}