using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 相册排版界面
{
    public partial class Scanner : Form
    {

        private int item_size = 6;

        private int cur_pos = 0;
        private int cur_start = 0;
        private List<string> list = new List<string>();

        public Scanner()
        {
            InitializeComponent();
            try
            {
                //打开选择文件夹对话框
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    list.Clear();
                    //获取用户选择的文件夹路径
                    string folderDirPath = folderBrowserDialog.SelectedPath;

                    //获取目录与子目录
                    DirectoryInfo dir = new DirectoryInfo(folderDirPath);
                    //var files = Directory.GetFiles(folderDirPath, "(*.jpg|*.png)");
                    //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                    FileInfo[] fileInfo = dir.GetFiles("*.jpg");
                    FileInfo[] fileInfo2 = dir.GetFiles("*.png");
                    if (fileInfo.Length > 0)
                    {
                        for (int i = 0; i < fileInfo.Length; i++)
                        {
                            list.Add(fileInfo[i].FullName);
                        }
                    }
                    else if(fileInfo2.Length > 0)
                    {
                        for (int i = 0; i < fileInfo2.Length; i++)
                        {
                            list.Add(fileInfo2[i].FullName);
                        }
                    }
                    
                    
                }
                else if (result == DialogResult.Cancel)
                {
                    //MessageBox.Show("取消显示图片列表");
                }
            }
            catch (Exception msg)
            {
                //报错提示 未将对象引用设置到对象的实例
                throw msg;
            }
            updateTopShow();
        }
        
        private Image GetImage(string fileName)
        {
            Image img = null;
            string filePath = fileName;
            try
            {
                FileStream fs = File.OpenRead(filePath);
                img = Image.FromStream(fs);
                fs.Close();
            }
            catch (IOException ie)
            {
                MessageBox.Show(ie.Message);
            }
            return img;
        }

        private void updateTopShow()
        {
            if (list.Count == 0)
            {
                return;
            }
            listView1.Items.Clear();
            imageList1.Images.Clear();

            for (int i = cur_start; i < cur_start + item_size && i < list.Count; i++)
            {
                string label = (i + 1) + " - " + list.Count;
                imageList1.Images.Add(GetImage(list[i]));
                listView1.Items.Add(label);
                listView1.Items[i - cur_start].ImageIndex = i - cur_start;
            }

            loadImage();
        }

        private void scaleImage(PictureBox pictureBox, string sPicPaht)
        {
            Bitmap bmPic = new Bitmap(sPicPaht);

            Point ptLoction = new Point(bmPic.Size);
            if (ptLoction.X > pictureBox.Size.Width || ptLoction.Y > pictureBox.Size.Height)
            {
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            else
            {
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            pictureBox.LoadAsync(sPicPaht);
        }

        int select_index2 = -1;
        private void loadImage()
        {
            if (cur_pos < list.Count)
            {
                //pictureBox1.Image = Image.FromFile(list[cur_pos]);
                scaleImage(pictureBox1, list[cur_pos]);
            }
            if (cur_pos + 1 < list.Count)
            {
                //pictureBox2.Image = Image.FromFile(list[cur_pos + 1]);
                scaleImage(pictureBox2, list[cur_pos + 1]);
            }
            else
            {
                pictureBox2.Image = null;
            }

            if (cur_pos - cur_start == 0 && cur_pos != 0)
            {
                select_index2 = -1;
            }
            
            if (select_index2 != -1)
            {
                ChangeForeColor(listView1.Items[select_index2], false);
            }

            select_index2 = cur_pos - cur_start;
            ChangeForeColor(listView1.Items[select_index2], true);
        }

        private void ChangeForeColor(ListViewItem item, bool bFont)
        {
            // 改变子项的颜色
            foreach (ListViewItem.ListViewSubItem si in item.SubItems)
            {
                if (bFont == true)
                {
                    item.Selected = true;
                    si.BackColor = Color.Black;
                }
                else
                {
                    si.ResetStyle();
                }
            }
        }

        private void 文件夹ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                //打开选择文件夹对话框
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    list.Clear();
                    //获取用户选择的文件夹路径
                    string folderDirPath = folderBrowserDialog.SelectedPath;

                    //获取目录与子目录
                    DirectoryInfo dir = new DirectoryInfo(folderDirPath);
                    //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                    FileInfo[] fileInfo = dir.GetFiles("*.jpg");
                    for (int i = 0; i < fileInfo.Length; i++)
                    {
                        list.Add(fileInfo[i].FullName);
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    //MessageBox.Show("取消显示图片列表");
                }
            }
            catch (Exception msg)
            {
                //报错提示 未将对象引用设置到对象的实例
                throw msg;
            }
            updateTopShow();
        }

        private void 图片ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;//等于true表示可以选择多个文件
            dlg.DefaultExt = ".png";
            dlg.Filter = "图片|*.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                list.Clear();
                ////获取用户选择的文件夹路径
                //string folderDirPath = "C:\\Users\\Dell\\Desktop\\shanghai";

                ////获取目录与子目录
                //DirectoryInfo dir = new DirectoryInfo(folderDirPath);
                ////获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                //FileInfo[] fileInfo = dir.GetFiles("*.jpg");
                //for (int i = 0; i < fileInfo.Length; i++)
                //{
                //    list.Add(fileInfo[i].FullName);
                //}
                foreach (string file in dlg.FileNames)
                {
                    list.Add(file);
                    
                }

            }
            updateTopShow();
        }

        private void left_Click_1(object sender, EventArgs e)
        {
            if (cur_pos <= 0)
            {
                MessageBox.Show("当前第一张");
                return;
            }

            cur_pos--;
            if (cur_pos != 0 && cur_pos < cur_start)
            {
                cur_start = cur_pos - (item_size - 1);
                updateTopShow();
            }
            loadImage();
        }

        private void right_Click_1(object sender, EventArgs e)
        {
            if (cur_pos >= list.Count - 1)
            {
                MessageBox.Show("当前最后一张");
                return;
            }

            cur_pos++;
            if (cur_pos > cur_start + item_size - 1)
            {
                cur_start = cur_pos;
                updateTopShow();
            }
            loadImage();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            int select_index = 0;
            ListView.SelectedIndexCollection indexes = this.listView1.SelectedIndices;
            if (indexes.Count > 0)
            {
                select_index = indexes[0];
            }
            cur_pos = cur_start + select_index;

            Image disImage1 = pictureBox1.Image;
            if (disImage1 != null)
            {
                pictureBox1.Image = null;
                disImage1.Dispose();
            }

            Image disImage2 = pictureBox2.Image;
            if (disImage2 != null)
            {
                pictureBox2.Image = null;
                disImage2.Dispose();
            }

            loadImage();

        }
    }
}
