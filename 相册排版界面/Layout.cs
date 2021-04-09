
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 相册排版界面
{
    public partial class Layout : Form
    {
        private int item_size = 7;

        private int cur_pos = 0;
        private int cur_start = 0;
        //private List<string> listAll = new List<string>();
        private List<ImageBean> listAll = new List<ImageBean>();
        private List<string> listCur = new List<string>();
        private List<string> listDone = new List<string>();

        Setting setting;

        public Layout()
        {
            InitializeComponent();
            setting = new Setting(this);
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
            listView1.Items.Clear();
            imageList1.Images.Clear();

            for (int i = cur_start; i < cur_start + item_size && i < listDone.Count; i++)
            {
                string label = (i + 1) + " - " + listDone.Count;

                //if (!string.IsNullOrEmpty(listDone[i]))
                //{
                //    imageList1.Images.Add(GetImage(listDone[i]));
                //    listView1.Items.Add(label);
                //    listView1.Items[i - cur_start].ImageIndex = i - cur_start;
                //}

                imageList1.Images.Add(GetImage(listDone[i]));
                listView1.Items.Add(label);
                listView1.Items[i - cur_start].ImageIndex = i - cur_start;
            }

            loadImage();
        }

        int select_index2 = -1;
        private void loadImage()
        {
            if (cur_pos < listDone.Count)
            {
                scaleImage(pictureBox1, listDone[cur_pos]);
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
                    si.BackColor = Color.Blue;
                }
                else
                {
                    si.ResetStyle();
                }
            }
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
                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            pictureBox.LoadAsync(sPicPaht);
        }

        private void left_Click(object sender, EventArgs e)
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

        private void right_Click(object sender, EventArgs e)
        {
            if (cur_pos >= listDone.Count - 1)
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

        //打开文件夹
        private void 文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //打开选择文件夹对话框
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    listCur.Clear();
                    listAll.Clear();
                    //获取用户选择的文件夹路径
                    string folderDirPath = folderBrowserDialog.SelectedPath;

                    //获取目录与子目录
                    DirectoryInfo dir = new DirectoryInfo(folderDirPath);
                    //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                    FileInfo[] fileInfo = dir.GetFiles("*.jpg");
                    for (int i = 0; i < fileInfo.Length; i++)
                    {
                        listCur.Add(fileInfo[i].FullName);
                        //listAll.Add(fileInfo[i].FullName);
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
            //updateTopShow();

            StartSetting(1);
        }

        //打开多个图片
        private void 图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;//等于true表示可以选择多个文件
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "图片|*.jpg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                listCur.Clear();
                listAll.Clear();
                foreach (string file in dlg.FileNames)
                {
                    listCur.Add(file);
                    //listAll.Add(file);
                }
                select_index2 = -1;
                StartSetting(2);
            }
            //updateTopShow();
        }

        //添加文件夹
        private void 从文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //打开选择文件夹对话框
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    listCur.Clear();
                    //获取用户选择的文件夹路径
                    string folderDirPath = folderBrowserDialog.SelectedPath;

                    //获取目录与子目录
                    DirectoryInfo dir = new DirectoryInfo(folderDirPath);
                    //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                    FileInfo[] fileInfo = dir.GetFiles("*.jpg");
                    for (int i = 0; i < fileInfo.Length; i++)
                    {
                        listCur.Add(fileInfo[i].FullName);
                        //listAll.Add(fileInfo[i].FullName);
                    }

                    StartSetting(3);
                }
            }
            catch (Exception msg)
            {
                //报错提示 未将对象引用设置到对象的实例
                throw msg;
            }
        }

        //添加多个文件
        private void 从图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;//等于true表示可以选择多个文件
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "图片|*.jpg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                listCur.Clear();
                foreach (string file in dlg.FileNames)
                {
                    listCur.Add(file);
                    //listAll.Add(file);
                }
                
                StartSetting(4);
            }
        }

        private void StartSetting(int index)
        {
            bool clearDone = true;
            if (index >= 101 && index <= 108)
            {
                listCur.Clear();
                for (int i = 0; i < listAll.Count; i++)
                {
                    listCur.Add(listAll[i].name);
                }
            }
            if (index == 3 || index == 4)
            {
                clearDone = false;
            }

            setting.postData(listCur, index, clearDone);
            setting.ShowDialog(this);
            int result = setting.getResult();
            if (result == 1)
            {
                int type = setting.getType();
                loadImageAfterDeal(type, index);
            }
        }
        
        private void loadImageAfterDeal(int type, int index)
        {
            try
            {
                if (index == 1 || index == 2)
                {
                    listDone.Clear();
                }

                if (index >= 101 && index <= 108)
                {
                    listAll.Clear();
                }

                for (int i = 0; i < listCur.Count; i++)
                {
                    ImageBean bean = new ImageBean();
                    bean.name = listCur[i];
                    bean.type = type;
                    listAll.Add(bean);
                }

                //获取用户选择的文件夹路径
                string folderDirPath = System.Environment.CurrentDirectory + "\\done";

                //获取目录与子目录
                DirectoryInfo dir = new DirectoryInfo(folderDirPath);
                //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                FileInfo[] fileInfo = dir.GetFiles("*.jpg");
                for (int i = 0; i < fileInfo.Length; i++)
                {
                    if (!listDone.Contains(fileInfo[i].FullName))
                    {
                        listDone.Add(fileInfo[i].FullName);
                    }
                }
            }
            catch (Exception msg)
            {
                //报错提示 未将对象引用设置到对象的实例
                throw msg;
            }

            cur_pos = 0;
            cur_start = 0;
            updateTopShow();
        }

        private void 单张ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            select_index2 = -1;
            StartSetting(101);
        }

        private void 单张转90度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            select_index2 = -1;
            StartSetting(102);
        }

        private void 两张合并ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            select_index2 = -1;
            StartSetting(103);
        }

        private void 两张合并转90度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            select_index2 = -1;
            StartSetting(104);
        }

        private void 四张合并ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            select_index2 = -1;
            StartSetting(105);
        }


        private void 四张合并转90度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            select_index2 = -1;
            StartSetting(106);
        }
        private void 八张合并ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            select_index2 = -1;
            StartSetting(107);
        }

        private void 八张合并转90度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            select_index2 = -1;
            StartSetting(108);
        }


        int fontIndex = 0;
        FontSelect fontSelect = new FontSelect();
        private void 加入文字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontSelect.ShowDialog(this);

            string fontName = fontSelect.fontName;
            int fontSize = fontSelect.fontSize;
            string content = fontSelect.content;
            string location = fontSelect.location;

            Bitmap bmp = new Bitmap(listDone[cur_pos]);
            Graphics g = Graphics.FromImage(bmp);
            Font font = new Font(fontName, fontSize);
            SolidBrush sbrush = new SolidBrush(Color.Black);
            //g.DrawString(content, font, sbrush, new PointF(10, 10));

            if(location=="图片上方")
            {
                g.DrawString(content, font, sbrush, new PointF(810, 200));
            }
            else if (location == "图片中间")
            {
                g.DrawString(content, font, sbrush, new PointF(810, 1600));
            }
            if (location == "图片下方")
            {
                g.DrawString(content, font, sbrush, new PointF(810, 3100));
            }
            //MemoryStream ms = new MemoryStream();
            //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            string name = listDone[cur_pos];
            name += fontIndex;

            bmp.Save(name);

            string path = name;
            listDone[cur_pos] = path;
            updateTopShow();
        }

        private void 调整图片大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folderDirPath = System.Environment.CurrentDirectory + "\\done\\";

            string fullPath = listDone[cur_pos];

            //string filename = System.IO.Path.GetFileName(fullPath);//文件名  “Default.aspx”
            string extension = System.IO.Path.GetExtension(fullPath);//扩展名 “.aspx”
            char[] MyChar = { 'd', 'o', 'n', 'e' };
            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fullPath).TrimEnd(MyChar);// 没有扩展名的文件名 “Default”

            int i = 0;
            for (; i < listAll.Count; i++)
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i].name);
                if (fileNameWithoutExtension == fileName)
                {
                    break;
                }
            }

            string img1 = listAll[i].name;
            string img2 = listAll[i+1].name;
            相册排版界面.SingleDeal singleDeal = new 相册排版界面.SingleDeal(img1,img2);
            singleDeal.ShowDialog();
            loadImageAfterDeal(3, 1);
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

            loadImage();

        }

        private void 更换图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;//等于true表示可以选择多个文件
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "图片|*.jpg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string path = dlg.FileNames[0];
                listDone[cur_pos] = path;
                updateTopShow();
            }
        }

        private void jpg格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //打开选择文件夹对话框
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //获取用户选择的文件夹路径
                    string folderDirPath = folderBrowserDialog.SelectedPath;
                    CopyDirectory(System.Environment.CurrentDirectory + "\\done", folderDirPath);

                    MessageBox.Show("导出成功");
                    this.Dispose();
                    System.Diagnostics.Process.Start("explorer.exe", folderDirPath);
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
        }
        public static int GetFilesCount(DirectoryInfo dirInfo)
        {

            int totalFile = 0;
            //totalFile += dirInfo.GetFiles().Length;//获取全部文件
            totalFile += dirInfo.GetFiles("*.jpg").Length;//获取某种格式
            foreach (System.IO.DirectoryInfo subdir in dirInfo.GetDirectories())
            {
                totalFile += GetFilesCount(subdir);
            }
            return totalFile;
        }


        public void CopyDirectory(string srcPath, string destPath)
        {
            
            int num = 1;
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath); 
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                int total = GetFilesCount(dir);
                foreach (FileSystemInfo i in fileinfo)
                {
                    /*if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(destPath + "\\" + i.Name))
                        {
                            Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDir(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
                    }
                    else*/
                    {
                        File.Copy(i.FullName, destPath + "\\" + num + "-" + total + ".jpg", true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                    num++;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void pdf格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Filter = "PDF(*.pdf)|*.pdf";//设置文件类型
            saveFileDialog1.FileName = "相册";//设置默认文件名
            saveFileDialog1.DefaultExt = "pdf";//设置默认格式（可以不设）
            saveFileDialog1.AddExtension = true;//设置自动在文件名中添加扩展名
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                toPdf(saveFileDialog1.FileName);
            }
            MessageBox.Show("导出成功");
            this.Dispose();
            string localFilePath = saveFileDialog1.FileName.ToString();
            System.Diagnostics.Process.Start(localFilePath);

        }
        private void toPdf(string fileName)
        {
            //存放image路径
            /*
            List<string> imageList = new List<string>();
            imageList.Add(@"D:\image1.png");
            imageList.Add(@"D:\image2.png");
            */

            //生成PDF路径
            //fileName = @"D:\testpdf.pdf";
            //iTextSharp.text.Rectangle page = new iTextSharp.text.Rectangle(300f, 250f);
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 0, 0, 0, 0);
            //设置纸张横向
            //document.SetPageSize(page.Rotate());
            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            for (int i = 0; i < listDone.Count; i++)
            {
                document.Open();
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(listDone[i]);
                //img.ScaleAbsolute(100f, 100f);
                img.ScaleToFit(iTextSharp.text.PageSize.A4.Width, iTextSharp.text.PageSize.A4.Height);

                //图片居中
                img.Alignment = iTextSharp.text.Image.MIDDLE_ALIGN;
                //图片绝对定位
                img.SetAbsolutePosition(0, 0);

                //图片打印到PDF
                writer.DirectContent.AddImage(img);
                document.NewPage();
            }

            document.Close();
        }

        private int StartSetting2(int index)
        {
            setting.postData(listCur, index, false);
            setting.ShowDialog(this);
            return setting.getResult();
        }
        
        private void 单张ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listDone.Count > 0)
            {
                string folderDirPath = System.Environment.CurrentDirectory + "\\done\\";

                string fullPath = listDone[cur_pos];

                //string filename = System.IO.Path.GetFileName(fullPath);//文件名  “Default.aspx”
                string extension = System.IO.Path.GetExtension(fullPath);//扩展名 “.aspx”
                char[] MyChar = { 'd', 'o', 'n', 'e' };
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fullPath).TrimEnd(MyChar);// 没有扩展名的文件名 “Default”

                int i = 0;
                for (; i < listAll.Count; i++)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i].name);
                    if (fileNameWithoutExtension == fileName)
                    {
                        break;
                    }
                }

                ImageBean bean = listAll[i];
                switch (bean.type)
                {
                    case 1:
                        MessageBox.Show("相同类型无需转换");
                        return;
                    case 2:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (StartSetting2(101) == 1)
                        {
                            ;
                        }
                        break;
                    case 3:
                    case 4:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 != listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (StartSetting2(101) == 1)
                        {
                            if (i + 1 != listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 1].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;
                    case 5:
                    case 6:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (i + 2 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 2].name);
                        }
                        if (i + 3 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 3].name);
                        }
                        if (StartSetting2(101) == 1)
                        {
                            if (i + 1 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 1].name);
                                //string fileName= (i + 1) + " - " + listDone.Count;
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 2 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[2 + 1].name);
                                //string fileName = (i + 2) + " - " + listDone.Count;
                                listDone.Insert(cur_pos + 2, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 3 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[3 + 1].name);
                                //string fileName = (i + 3) + " - " + listDone.Count;
                                listDone.Insert(cur_pos + 3, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;
                }
                bean.type = 1;
                updateTopShow();
            }
        }

        private void 单张转90度ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listDone.Count > 0)
            {
                string folderDirPath = System.Environment.CurrentDirectory + "\\done\\";

                string fullPath = listDone[cur_pos];

                //string filename = System.IO.Path.GetFileName(fullPath);//文件名  “Default.aspx”
                string extension = System.IO.Path.GetExtension(fullPath);//扩展名 “.aspx”
                char[] MyChar = { 'd', 'o', 'n', 'e' };
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fullPath).TrimEnd(MyChar);// 没有扩展名的文件名 “Default”

                int i = 0;
                for (; i < listAll.Count; i++)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i].name);
                    if (fileNameWithoutExtension == fileName)
                    {
                        break;
                    }
                }

                ImageBean bean = listAll[i];
                switch (bean.type)
                {
                    case 1:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (StartSetting2(102) == 1)
                        {
                            ;
                        }
                        break;
                    case 2:
                        MessageBox.Show("相同类型无需转换");
                        return;
                    case 3:
                    case 4:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 != listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (StartSetting2(102) == 1)
                        {
                            if (i + 1 != listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 1].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;
                    case 5:
                    case 6:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (i + 2 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 2].name);
                        }
                        if (i + 3 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 3].name);
                        }
                        if (StartSetting2(102) == 1)
                        {
                            if (i + 1 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 1].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 2 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 2].name);
                                listDone.Insert(cur_pos + 2, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 3 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 3].name);
                                listDone.Insert(cur_pos + 3, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;
                }
                bean.type = 2;
                updateTopShow();
            }
        }

        private void 两张合并ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listDone.Count > 0)
            {
                string folderDirPath = System.Environment.CurrentDirectory + "\\done\\";

                string fullPath = listDone[cur_pos];

                //string filename = System.IO.Path.GetFileName(fullPath);//文件名  “Default.aspx”
                string extension = System.IO.Path.GetExtension(fullPath);//扩展名 “.aspx”
                char[] MyChar = { 'd', 'o', 'n', 'e' };
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fullPath).TrimEnd(MyChar);// 没有扩展名的文件名 “Default”

                int i = 0;
                for (; i < listAll.Count; i++)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i].name);
                    if (fileNameWithoutExtension == fileName)
                    {
                        break;
                    }
                }

                ImageBean bean = listAll[i];
                switch (bean.type)
                {
                    case 1:
                    case 2:
                        if (i == listAll.Count - 1)
                        {
                            MessageBox.Show("当前选择最后一张无法合并");
                            return;
                        }
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        listCur.Add(listAll[i + 1].name);
                        if (StartSetting2(103) == 1)
                        {
                            listDone.RemoveAt(i + 1);
                        }
                        break;
                    case 3:
                        MessageBox.Show("相同类型无需转换");
                        return;
                    case 4:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (StartSetting2(103) == 1)
                        {
                            ;
                        }
                        break;
                    case 5:
                    case 6:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (i + 2 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 2].name);
                        }
                        if (i + 3 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 3].name);
                        }
                        if (StartSetting2(103) == 1)
                        {
                            if (i + 1 < listAll.Count)
                            {
                            }
                            if (i + 2 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 2].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 3 < listAll.Count)
                            {
                            }
                        }
                        break;
                }
                bean.type = 3;
                updateTopShow();
            }
        }

        private void 两张合并转90度ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listDone.Count > 0)
            {
                string folderDirPath = System.Environment.CurrentDirectory + "\\done\\";

                string fullPath = listDone[cur_pos];

                //string filename = System.IO.Path.GetFileName(fullPath);//文件名  “Default.aspx”
                string extension = System.IO.Path.GetExtension(fullPath);//扩展名 “.aspx”
                char[] MyChar = { 'd', 'o', 'n', 'e' };
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fullPath).TrimEnd(MyChar);// 没有扩展名的文件名 “Default”

                int i = 0;
                for (; i < listAll.Count; i++)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i].name);
                    if (fileNameWithoutExtension == fileName)
                    {
                        break;
                    }
                }

                ImageBean bean = listAll[i];
                switch (bean.type)
                {
                    case 1:
                    case 2:
                        if (i == listAll.Count - 1)
                        {
                            MessageBox.Show("当前选择最后一张无法合并");
                            return;
                        }
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        listCur.Add(listAll[i + 1].name);
                        listCur.Add(listAll[i + 2].name);
                        listCur.Add(listAll[i + 3].name);
                        if (StartSetting2(105) == 1)
                        {
                            listDone.RemoveAt(i + 1);
                            listDone.RemoveAt(i + 2);
                            listDone.RemoveAt(i + 3);
                        }
                        break;
                    case 3:
                    case 4:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (StartSetting2(105) == 1)
                        {
                            ;
                        }
                        break;
                    case 5:
                        MessageBox.Show("相同类型无需转换");
                        return;
                    case 6:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (i + 2 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 2].name);
                        }
                        if (i + 3 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 3].name);
                        }
                        if (StartSetting2(104) == 1)
                        {
                            if (i + 1 < listAll.Count)
                            {
                            }
                            if (i + 2 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 2].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 3 < listAll.Count)
                            {
                            }
                        }
                        break;
                }
                bean.type = 4;
                updateTopShow();
            }
        }

        private void 四张合并ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            /*
            if (listDone.Count > 0)
            {
                string folderDirPath = System.Environment.CurrentDirectory + "\\done\\";

                string fullPath = listDone[cur_pos];

                //string filename = System.IO.Path.GetFileName(fullPath);//文件名  “Default.aspx”
                string extension = System.IO.Path.GetExtension(fullPath);//扩展名 “.aspx”
                char[] MyChar = { 'd', 'o', 'n', 'e' };
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fullPath).TrimEnd(MyChar);// 没有扩展名的文件名 “Default”

                int i = 0;
                for (; i < listAll.Count; i++)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i].name);
                    if (fileNameWithoutExtension == fileName)
                    {
                        break;
                    }
                }

                ImageBean bean = listAll[i];
                switch (bean.type)
                {
                    case 1:
                    case 2:
                        if (i > listAll.Count - 4)
                        {
                            MessageBox.Show("后续张数不足，无法合并");
                            return;
                        }
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        listCur.Add(listAll[i + 1].name);
                        if (StartSetting2(105) == 1)
                        {
                            listDone.RemoveAt(i + 1);
                        }
                        break;
                    case 3:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (StartSetting2(104) == 1)
                        {
                            ;
                        }
                        break;
                    case 4:
                        MessageBox.Show("相同类型无需转换");
                        return;
                    case 5:
                    case 6:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (i + 2 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 2].name);
                        }
                        if (i + 3 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 3].name);
                        }
                        if (StartSetting2(104) == 1)
                        {
                            if (i + 1 < listAll.Count)
                            {
                            }
                            if (i + 2 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 2].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 3 < listAll.Count)
                            {
                            }
                        }
                        break;
                }
                bean.type = 4;
                updateTopShow();
            }
            */
        }

        private void 四张合并转90度ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            /*
            if (listDone.Count > 0)
            {
                string folderDirPath = System.Environment.CurrentDirectory + "\\done\\";

                string fullPath = listDone[cur_pos];

                //string filename = System.IO.Path.GetFileName(fullPath);//文件名  “Default.aspx”
                string extension = System.IO.Path.GetExtension(fullPath);//扩展名 “.aspx”
                char[] MyChar = { 'd', 'o', 'n', 'e' };
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fullPath).TrimEnd(MyChar);// 没有扩展名的文件名 “Default”

                int i = 0;
                for (; i < listAll.Count; i++)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i].name);
                    if (fileNameWithoutExtension == fileName)
                    {
                        break;
                    }
                }

                ImageBean bean = listAll[i];
                switch (bean.type)
                {
                    case 1:
                    case 2:
                        if (i > listAll.Count - 4)
                        {
                            MessageBox.Show("后续张数不足，无法合并");
                            return;
                        }
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        listCur.Add(listAll[i + 1].name);
                        if (StartSetting2(105) == 1)
                        {
                            listDone.RemoveAt(i + 1);
                        }
                        break;
                    case 3:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (StartSetting2(104) == 1)
                        {
                            ;
                        }
                        break;
                    case 4:
                        MessageBox.Show("相同类型无需转换");
                        return;
                    case 5:
                    case 6:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (i + 2 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 2].name);
                        }
                        if (i + 3 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 3].name);
                        }
                        if (StartSetting2(104) == 1)
                        {
                            if (i + 1 < listAll.Count)
                            {
                            }
                            if (i + 2 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 2].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 3 < listAll.Count)
                            {
                            }
                        }
                        break;
                }
                bean.type = 4;
                updateTopShow();
            }
            */
        }
        private void 八张合并ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void 八张合并转90度ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            /*
            if (listDone.Count > 0)
            {
                string folderDirPath = System.Environment.CurrentDirectory + "\\done\\";

                string fullPath = listDone[cur_pos];

                //string filename = System.IO.Path.GetFileName(fullPath);//文件名  “Default.aspx”
                string extension = System.IO.Path.GetExtension(fullPath);//扩展名 “.aspx”
                char[] MyChar = { 'd', 'o', 'n', 'e' };
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fullPath).TrimEnd(MyChar);// 没有扩展名的文件名 “Default”

                int i = 0;
                for (; i < listAll.Count; i++)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i].name);
                    if (fileNameWithoutExtension == fileName)
                    {
                        break;
                    }
                }

                ImageBean bean = listAll[i];
                switch (bean.type)
                {
                    case 1:
                    case 2:
                        if (i > listAll.Count - 4)
                        {
                            MessageBox.Show("后续张数不足，无法合并");
                            return;
                        }
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        listCur.Add(listAll[i + 1].name);
                        if (StartSetting2(105) == 1)
                        {
                            listDone.RemoveAt(i + 1);
                        }
                        break;
                    case 3:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (StartSetting2(104) == 1)
                        {
                            ;
                        }
                        break;
                    case 4:
                        MessageBox.Show("相同类型无需转换");
                        return;
                    case 5:
                    case 6:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (i + 2 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 2].name);
                        }
                        if (i + 3 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 3].name);
                        }
                        if (StartSetting2(104) == 1)
                        {
                            if (i + 1 < listAll.Count)
                            {
                            }
                            if (i + 2 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 2].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 3 < listAll.Count)
                            {
                            }
                        }
                        break;
                }
                bean.type = 4;
                updateTopShow();
            }
            */
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Layout_Load(object sender, EventArgs e)
        {

        }

    }
}
