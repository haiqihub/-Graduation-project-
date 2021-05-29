
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 相册排版界面
{
    public partial class Layout : Form
    {
        private int item_size = 5;

        private int cur_pos = 0;
        private int cur_start = 0;
        private int insert_cur = 0;
        private int setting_index = 0;
        //private List<string> listAll = new List<string>();

        private List<ImageBean> listAll = new List<ImageBean>();
        private List<ImageBean> listLeftAll = new List<ImageBean>();
        private List<string> listCur = new List<string>();
        private List<string> listDone = new List<string>();
        private List<string> listDone1 = new List<string>();
        private List<string> listLeft = new List<string>();
        private List<string> listLeftDone = new List<string>();
        Setting setting;

        private Point point1, point2, point3, point4, p, point;
        public int drag_area = 0;
        private List<string> imgpath = new List<string>();
        private List<string> listDrag = new List<string>();
        private List<string> listChange = new List<string>();
        private bool m_bDown = false;
        private bool l_bDown = false;
        private Size size;
        private Rectangle rectSmall;
        private System.Drawing.Image image = null;

        public static string path_url;
        

        // 这个是鼠标点击地点 layout根据不同的setting.index分别判断cur_area =1,2,3,4,5,6,7,8
        //然后把这个值传给SingleDeal 在SingleDeal中把list[cur_area]对应上，只有listChange[cur_area]才变，其余不动
        //在SingleDeal中根据cur_area以及index（1248） 的情况，来判断调用HechengXXX（）
        public int cur_area = 0;
        public int index = 0;
        


        public Layout()
        {
            InitializeComponent();
            //Control.CheckForIllegalCrossThreadCalls = false;
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
            //原  int i = cur_start
            for (int i = cur_start; i < cur_start + item_size && i < listDone.Count; i++)
            {
                string label = (i + 1) + " - " + listDone.Count;

                imageList1.Images.Add(GetImage(listDone[i]));
                listView1.Items.Add(label);
                listView1.Items[i - cur_start].ImageIndex = i - cur_start;
    
            }

            loadImage();
        }
        private void updateTopShow1()
        {
            listView1.Items.Clear();
            imageList1.Images.Clear();

            for (int i = cur_start; i < cur_start + item_size && i < listDone1.Count; i++)
            {
                string label = (i + 1) + " - " + listDone1.Count;

                imageList1.Images.Add(GetImage(listDone1[i]));
                listView1.Items.Add(label);
                listView1.Items[i - cur_start].ImageIndex = i - cur_start;
     
            }

            loadImage1();
        }
        //左侧照片夹列表更新
        private void updateLeftShow()
        {
            listView2.Items.Clear();
            imageList2.Images.Clear();

            for (int i = cur_start;  i < listLeft.Count; i++)
            {
                string label = (i + 1) + " - " + listLeft.Count;

                imageList2.Images.Add(GetImage(listLeft[i]));
                listView2.Items.Add(label);
                listView2.Items[i - cur_start].ImageIndex = i - cur_start;

            }

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
        private void loadImage1()
        {
            if (cur_pos < listDone1.Count)
            {
                scaleImage(pictureBox1, listDone1[cur_pos]);
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
            try
            {
                ChangeForeColor(listView1.Items[select_index2], true);
            }
            catch(System.ArgumentOutOfRangeException)
            {

            }
            
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


        private void up_left_Click(object sender, EventArgs e)
        {
            if (cur_pos < item_size|| cur_pos < 0)
            {
                MessageBox.Show("当前是第一个版面");
                return;
            }
            cur_pos = (cur_pos / item_size - 1) * item_size;
            if (cur_pos >= 0 && cur_pos < cur_start)
            {
                //原 cur_pos > -1 && cur_pos < cur_start+1
                cur_start = (cur_pos / item_size ) * item_size;
                updateTopShow();
            }


            loadImage();
        }

        private void up_right_Click(object sender, EventArgs e)
        {
            //ListView.SelectedIndexCollection indexes = this.listView1.SelectedIndices;
            //if (indexes.Count > 0)
            //{
            //    select_index = indexes[0];
            //}
            //原 cur_pos > listDone.Count - item_size - 1
            if (cur_pos >= (listDone.Count / item_size) * item_size && cur_pos < (listDone.Count / item_size + 1) * item_size)
            {
                MessageBox.Show("当前是最后一个版面");
                return;
            }

            //cur_pos += item_size;
            cur_pos = (cur_pos / item_size + 1) * item_size;
            //if (cur_pos > cur_start + item_size - 1)
            //{
            //    cur_start = (cur_pos / item_size) * item_size;
            //    updateTopShow();
            //}
            
            cur_start = (cur_pos / item_size) * item_size;
            updateTopShow();
            
            loadImage();
        }

        private void left_Click(object sender, EventArgs e)
        {
            if (cur_pos <= 0)
            {
                MessageBox.Show("当前是第一张");
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
                MessageBox.Show("当前是最后一张");
                return;
            }

            cur_pos++;
            if (cur_pos > cur_start + item_size - 1)
            {
                cur_start = cur_pos;
                updateTopShow();
            }
            //loadImage1();
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
            string Path1 = System.Environment.CurrentDirectory + "\\done";
            ListView.SelectedIndexCollection up_indexes = this.listView1.SelectedIndices;
            if (up_indexes.Count == 0)
            {
                //没图片时候
                //删掉之前在done文件夹中的图片
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(Path1);
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)            //判断是否文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);          //删除子目录和文件
                        }
                        else
                        {
                            File.Delete(i.FullName);      //删除指定文件
                        }
                    }
                    //打开对话框
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    DialogResult result = folderBrowserDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        listCur.Clear();
                        //获取用户选择的文件夹路径
                        string folderDirPath = folderBrowserDialog.SelectedPath;

                        //获取目录与子目录
                        DirectoryInfo dirs = new DirectoryInfo(folderDirPath);
                        //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                        FileInfo[] fileInfo = dirs.GetFiles("*.jpg");
                        for (int i = 0; i < fileInfo.Length; i++)
                        {


                            listCur.Add(fileInfo[i].FullName);
                            //listAll.Add(fileInfo[i].FullName); 

                        }
                        StartSetting(3);
                    }
                }
                catch (Exception)
                {
                }

                //return;
            }
            else
            {
                //有图片时候
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
                        //---------------------加入对几拼一图片的判断----------------
                        int add_index = 0;
                        if (setting.index == 1 || setting.index == 2)
                        {
                            add_index = 1;
                        }
                        if (setting.index == 3 || setting.index == 4)
                        {
                            add_index = 2;
                        }
                        if (setting.index == 5 || setting.index == 6)
                        {
                            add_index = 4;
                        }
                        if (setting.index == 7 || setting.index == 8)
                        {
                            add_index = 8;
                        }
                        //----------------------------------------
                        for (int i = 0; i < fileInfo.Length; i++)
                        {
                            ImageBean bean = new ImageBean();
                            int type = setting.getType();

                            bean.name = fileInfo[i].FullName;
                            bean.type = type;

                            if (cur_pos >= 0)
                            {
                                //插入位置
                                insert_cur = (cur_pos + 1) * add_index;
                                listCur.Add(fileInfo[i].FullName);
                                
                            }
                            else
                            {
                                listCur.Add(fileInfo[i].FullName);
                               
                            }

                        }

                        StartSetting(34);
                    }
                }
                catch (Exception msg)
                {
                    //报错提示 未将对象引用设置到对象的实例
                    throw msg;
                }

                //原
                //try
                //{
                //    //打开选择文件夹对话框
                //    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                //    DialogResult result = folderBrowserDialog.ShowDialog();
                //    if (result == DialogResult.OK)
                //    {
                //        listCur.Clear();
                //        //获取用户选择的文件夹路径
                //        string folderDirPath = folderBrowserDialog.SelectedPath;

                //        //获取目录与子目录
                //        DirectoryInfo dir = new DirectoryInfo(folderDirPath);
                //        //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                //        FileInfo[] fileInfo = dir.GetFiles("*.jpg");
                //        for (int i = 0; i < fileInfo.Length; i++)
                //        {
                //            listCur.Add(fileInfo[i].FullName);
                //            //listAll.Add(fileInfo[i].FullName);
                //        }

                //        StartSetting(3);
                //    }
                //}
                //catch (Exception msg)
                //{
                //    //报错提示 未将对象引用设置到对象的实例
                //    throw msg;
                //}
            }
            
        }

        //添加多个图片
        private void 从图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Path1 = System.Environment.CurrentDirectory + "\\done";
            ListView.SelectedIndexCollection up_indexes = this.listView1.SelectedIndices;

            if (up_indexes.Count > 0)
            {
                select_index = up_indexes[0];
            }

            if (up_indexes.Count == 0)
            {
                //没图片时候
                //删掉之前在done文件夹中的图片
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(Path1);
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)            //判断是否文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);          //删除子目录和文件
                        }
                        else
                        {
                            File.Delete(i.FullName);      //删除指定文件
                        }
                    }
                    //打开图片
                    OpenFileDialog dlgs = new OpenFileDialog();
                    dlgs.Multiselect = true;//等于true表示可以选择多个文件
                    dlgs.DefaultExt = ".jpg";
                    dlgs.Filter = "图片|*.jpg";
                    if (dlgs.ShowDialog() == DialogResult.OK)
                    {
                        listCur.Clear();
                        for (int i = 0; i < dlgs.FileNames.Length; i++)
                        {
                            
                            listCur.Add(dlgs.FileNames[i]);
                            
                        }

                        StartSetting(4);
                        updateTopShow();
                    }


                }
                catch (Exception)
                {
                }

                //return;
            }
            else
            {
                //有图片时候
                //添加图片操作
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Multiselect = true;//等于true表示可以选择多个文件
                dlg.DefaultExt = ".jpg";
                dlg.Filter = "图片|*.jpg";
                //---------------------加入对几拼一图片的判断----------------
                int add_index = 0;
                if (setting.index == 1 || setting.index == 2)
                {
                    add_index = 1;
                }
                if (setting.index == 3 || setting.index == 4)
                {
                    add_index = 2;
                }
                if (setting.index == 5 || setting.index == 6)
                {
                    add_index = 4;
                }
                if (setting.index == 7 || setting.index == 8)
                {
                    add_index = 8;
                }
                //----------------------------------------
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    listCur.Clear();
                    for (int i = 0; i < dlg.FileNames.Length; i++)
                    {
                        ImageBean bean = new ImageBean();
                        int type = setting.getType();

                        bean.name = dlg.FileNames[i];
                        bean.type = type;

                        if (cur_pos >= 0)
                        {
                            insert_cur = (cur_pos + 1) * add_index;
                            listCur.Add(dlg.FileNames[i]);
                            
                        }
                        else
                        {
                            listCur.Add(dlg.FileNames[i]);
                            //listAll.Add(fileInfo[i].FullName);
                        }
                        
                    }

                    StartSetting(34);
                    updateTopShow();
                }
            }
            
           
        }

        //打开文件夹到左侧照片夹
        private void 打开文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //打开选择文件夹对话框
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    
                    listLeft.Clear();
                    listLeftAll.Clear();
                    //获取用户选择的文件夹路径
                    string folderDirPath = folderBrowserDialog.SelectedPath;

                    //获取目录与子目录
                    DirectoryInfo dir = new DirectoryInfo(folderDirPath);
                    //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                    FileInfo[] fileInfo = dir.GetFiles("*.jpg");
                    for (int i = 0; i < fileInfo.Length; i++)
                    {
                        listLeft.Add(fileInfo[i].FullName);
                        
                    }

                    //StartSetting(5);
                    updateLeftShow();

                }
            }
            catch (Exception msg)
            {
                //报错提示 未将对象引用设置到对象的实例
                throw msg;
            }
        }
        //打开图片到左侧照片夹
        private void 照片夹打开照片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;//等于true表示可以选择多个文件
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "图片|*.jpg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //listCur.Clear();
                listLeft.Clear();
                listLeftAll.Clear();
                //listAll.Clear();
                foreach (string file in dlg.FileNames)
                {
                    listLeft.Add(file);
                    //listAll.Add(file);
                }
                select_index2 = -1;
                //StartSetting(2);
                updateLeftShow();
            }
            //updateTopShow();
        }

        //照片夹：添加文件夹
        private void 照片夹添加文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //打开选择文件夹对话框
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //listLeft.Clear();
                    //获取用户选择的文件夹路径
                    string folderDirPath = folderBrowserDialog.SelectedPath;

                    //获取目录与子目录
                    DirectoryInfo dir = new DirectoryInfo(folderDirPath);
                    //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                    FileInfo[] fileInfo = dir.GetFiles("*.jpg");
                    for (int i = 0; i < fileInfo.Length; i++)
                    {
                        if (select_index_left > -1)
                        {
                            listLeft.Insert(select_index_left+1, fileInfo[i].FullName);
                        }
                        else
                        {
                            listLeft.Add(fileInfo[i].FullName);
                            //listAll.Add(fileInfo[i].FullName);
                        }

                    }

                    //StartSetting(3);
                    updateLeftShow();
                    
                }
            }
            catch (Exception msg)
            {
                //报错提示 未将对象引用设置到对象的实例
                throw msg;
            }
        }

        //照片夹：添加图片
        private void 照片夹添加图片ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;//等于true表示可以选择多个文件
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "图片|*.jpg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //listLeft.Clear();
                foreach (string file in dlg.FileNames)
                {

                    if (select_index_left > -1)
                    {
                        listLeft.Insert(select_index_left+1, file);
                    }
                    else
                    {
                        listLeft.Add(file);
                        //listAll.Add(file);
                    }

                }

                //StartSetting(4);
                updateLeftShow();
                //StartLeftSetting(6);
            }
        }
        
        //照片夹 排版 
        private void 照片夹排版ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listLeft.Count == 0)
            {
                MessageBox.Show("请先在照片夹中添加照片，再进行排版操作");
            }
            else
            {
                
                //listDone1.Clear();
            }

            //左边图片少
            if (listCur.Count > listLeft.Count || listCur.Count == listLeft.Count)
            {
                listCur.Clear();
                //listAll.Clear();
                for (int i = 0; i < listLeft.Count; i++)
                {
                    
                    listCur.Add(listLeft[i]);
                }

                StartLeftSetting(100 + setting.index);
            }
            //左边图片多
            else
            {
                listCur.Clear();
                //listAll.Clear();
                for (int i = 0; i < listLeft.Count; i++)
                {
                    
                    listCur.Add(listLeft[i]);
                }
                StartLeftSetting(100 + setting.index);
            }
        }

        private void StartLeftSetting(int index)
        {
            bool clearDone = true;
            
            if (index >= 101 && index <= 108)
            {
                listCur.Clear();
                for (int i = 0; i < listLeft.Count; i++)
                {
                    listCur.Add(listLeft[i]);
                }
            }
  
            //3 添加文件夹 ；4 添加图片 
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

        private void StartSetting(int index)
        {
            bool clearDone = true;
            /*
            if (index >= 101 && index <= 108)
            {
                listCur.Clear();
                for (int i = 0; i < listAll.Count; i++)
                {
                    listCur.Add(listAll[i].name);
                }
            }
            */

            if (index == 101 || index == 102)
            {
                listCur.Clear();
                for (int i = 0; i < listAll.Count; i++)
                {
                    listCur.Add(listAll[i].name);
                }
            }
            if (index == 103 || index == 104)
            {
                listCur.Clear();
                for (int i = 0; i < listAll.Count; i += 2)
                {
                    listCur.Add(listAll[i].name);
                    if (i + 1 < listAll.Count)
                    {
                        listCur.Add(listAll[i + 1].name);
                    }
                }
            }
            if (index == 105 || index == 106)
            {
                listCur.Clear();
                for (int i = 0; i < listAll.Count; i += 4)
                {
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
                }
            }
            if (index == 107 || index == 108)
            {
                listCur.Clear();
                for (int i = 0; i < listAll.Count; i += 8)
                {
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
                    if (i + 4 < listAll.Count)
                    {
                        listCur.Add(listAll[i + 4].name);
                    }
                    if (i + 5 < listAll.Count)
                    {
                        listCur.Add(listAll[i + 5].name);
                    }
                    if (i + 6 < listAll.Count)
                    {
                        listCur.Add(listAll[i + 6].name);
                    }
                    if (i + 7 < listAll.Count)
                    {
                        listCur.Add(listAll[i + 7].name);
                    }
                }
            }

            //当前无图片：3 添加文件夹 ；4 添加图片 
            if (index == 3 || index == 4)
            {
                clearDone = false;
            }
            //当前有图片：添加文件夹和图片
            if(index == 34)
            {
                clearDone = false;
                setting.postData(listCur, index, clearDone);
                setting.ShowDialog(this);
                int result = setting.getResult();
                if (result == 1)
                {
                    int type = setting.getType();
                    loadImageAfterInsert(type, index,insert_cur,cur_pos + 1);
                }
            }
            else
            {
                setting.postData(listCur, index, clearDone);
                setting.ShowDialog(this);
                int result = setting.getResult();
                if (result == 1)
                {
                    int type = setting.getType();
                    loadImageAfterDeal(type, index);
                }
            }
            

        }



        //private void StartLeftSetting(int index)
        //{
        //    bool clearDone = true;
        //    //5 添加文件夹到照片夹 ；6 添加图片到照片夹
        //    if ( index == 5 || index == 6)
        //    {
        //        clearDone = false;
        //    }

        //    setting.postData(listCur, index, clearDone);
        //    //setting.ShowDialog(this);
        //    int result = setting.getResult();
        //    if (result == 1)
        //    {
        //        int type = setting.getType();
        //        loadLeftImageAfterDeal(type, index);
        //    }

        //}


        private void loadImageAfterDeal(int type, int index)
        {
            //获取用户选择的文件夹路径
            string folderDirPath = System.Environment.CurrentDirectory + "\\done";

            //获取目录与子目录
            DirectoryInfo dir = new DirectoryInfo(folderDirPath);
            //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
            FileInfo[] fileInfo = dir.GetFiles("*.jpg");

            if (index >= 101 && index <= 108)
            {
                listAll.Clear();
                listDone1.Clear();

                for (int i = 0; i < listCur.Count; i++)
                {
                    ImageBean bean = new ImageBean();
                    bean.name = listCur[i];
                    bean.type = type;
                    listAll.Add(bean);
                }

                for (int i = 0; i < fileInfo.Length; i++)
                {
                                    
                    listDone1.Add(fileInfo[i].FullName);
                        
                }
                
                cur_pos = 0;
                cur_start = 0;
                updateTopShow1();

            }
            else
            {
                if (index == 1 || index == 2)
                {
                    listDone.Clear();
                }
                //------------
                //if (index == 3 || index == 4)
                //{
                //    listDone.Clear();
                //}

                for (int i = 0; i < listCur.Count; i++)
                {
                    ImageBean bean = new ImageBean();
                    bean.name = listCur[i];
                    bean.type = type;
                    listAll.Add(bean);
                }

                //move up dir...

                for (int i = 0; i < fileInfo.Length; i++)
                {
                    if (!listDone.Contains(fileInfo[i].FullName))
                    {
                        listDone.Add(fileInfo[i].FullName);
                    }
                }

                //catch (Exception msg)
                //{
                //    //报错提示 未将对象引用设置到对象的实例
                //    throw msg;
                //}

                cur_pos = 0;
                cur_start = 0;
                updateTopShow();

            }

                
        }
        //插入文件夹和照片
        private void loadImageAfterInsert(int type, int index,int insert_cur,int insert_done)
        {
            //获取用户选择的文件夹路径
            string folderDirPath = System.Environment.CurrentDirectory + "\\done";

            //获取目录与子目录
            DirectoryInfo dir = new DirectoryInfo(folderDirPath);
            //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
            FileInfo[] fileInfo = dir.GetFiles("*.jpg");

         
            if (index == 1 || index == 2)
            {
                listDone.Clear();
            }
            
            for (int i = listCur.Count - 1; i >= 0; i--)
            {
                ImageBean bean = new ImageBean();
                bean.name = listCur[i];
                bean.type = type;
                //listAll.Add(bean);
                listAll.Insert(insert_cur, bean);
                
            }
            listCur.Clear();
            for (int i = 0; i < listAll.Count; i++)
            {
                listCur.Add(listAll[i].name);
            }

            for (int i = fileInfo.Length - 1; i >= 0; i--)
            {
                if (!listDone.Contains(fileInfo[i].FullName))
                {
                    //listDone.Add(fileInfo[i].FullName);
                    listDone.Insert(insert_done, fileInfo[i].FullName);
                }
            }

            cur_pos = 0;
            cur_start = 0;
            updateTopShow();

        }


        private void 单张ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            select_index2 = -1;
            StartSetting(101);
        }

        private void 单张转90度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            select_index2 = -1;
            StartSetting(102);
        }

        private void 两张合并ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            select_index2 = -1;
            StartSetting(103);
        }

        private void 两张合并转90度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            select_index2 = -1;
            StartSetting(104);
        }

        private void 四张合并ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            select_index2 = -1;
            StartSetting(105);
        }


        private void 四张合并转90度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            select_index2 = -1;
            StartSetting(106);
        }
        private void 八张合并ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            select_index2 = -1;
            StartSetting(107);
        }

        private void 八张合并转90度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            select_index2 = -1;
            StartSetting(108);
        }

        
        int fontIndex = 0;
        TextDescription textDescription = new TextDescription();

        //照片的描述 textbox绘制： layout加入文字ToolStripMenuItem_Click
        public void AddDescription(int num)
        {
            //if (this.InvokeRequired)
            //{
              //  this.Invoke(new MethodInvoker(delegate { AddDescription(num); }));
               // return;
           // }
            string fontName = textDescription.fontName;
            int fontSize = textDescription.fontSize;
            string content = textDescription.content;
            string fontColor = textDescription.fontColor;
            //颜色 string转为system.drawing.color
            Color sfontColor = System.Drawing.ColorTranslator.FromHtml(fontColor);

            //// Bitmap bmp = new Bitmap(listDone[cur_pos]);
            //Bitmap bmp = null;
            //try
            //{
            //    bmp = new Bitmap(CC.A4CHANG_XS, CC.A4GAO_XS);
            //}
            //catch (Exception)
            //{
            //}

            //Graphics g = Graphics.FromImage(bmp);
            //Font font = new Font(fontName, fontSize);
            //SolidBrush sbrush = new SolidBrush(sfontColor);
            ////g.DrawString(content, font, sbrush, new PointF(10, 10));

            //TextBox 绘制
            int up = setting.up;
            int down = setting.down;
            int left = setting.left;
            int right = setting.right;
            int v_middle = setting.v_middle;
            int h_middle = setting.h_middle;
            int text_photo = setting.text_photo;
            float x = 0;
            float y = 0;
            float width_xs = (CC.A4CHANG - left - right) * CC.A4GBILV;
            float height_xs = (CC.A4GAO - up - down) * CC.A4GBILV;

            if (num == 1 | num == 2)
            { 

                TextBox tb1 = new TextBox();
                tb1.Name  = "textBox" ;
                tb1.Text = "aaaaaaaaaaaaaaaaaaaaaaaa";
                x = left * CC.A4GBILV;
                y = up * CC.A4GBILV + height_xs + text_photo * CC.A4GBILV;
                tb1.Location= new Point((int)x,(int)y);
                tb1.Size = new Size(400,50);
                tb1.Visible = true;
                this.Controls.Add(tb1);
                tb1.BringToFront();

                //g.DrawString(content, font, sbrush, new PointF(x, y));
            }
    


            //string name = listDone[cur_pos];
            //name += fontIndex;

            //bmp.Save(name);

            //string path = name;
            //listDone[cur_pos] = path;
            //updateTopShow();
        }

        
        FontSelect fontSelect = new FontSelect();
        private void 加入文字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            fontSelect.ShowDialog(this);

            string fontName = fontSelect.fontName;
            int fontSize = fontSelect.fontSize;
            string content = fontSelect.content;
            string location = fontSelect.location;
            string fontColor = fontSelect.fontColor;

            Bitmap bmp = new Bitmap(listDone[cur_pos]);
            Graphics g = Graphics.FromImage(bmp);
            Font font = new Font(fontName, fontSize);
            SolidBrush sbrush = new SolidBrush(Color.FromName(fontColor));
            //g.DrawString(content, font, sbrush, new PointF(10, 10));

            if (location=="图片上方")
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
        
        private void 添加文字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            textDescription.ShowDialog(this);
           
            var width = CC.A4CHANG_XS;
            var height = CC.A4GAO_XS;
            //----------------------------
            string fontName = textDescription.fontName;
            int fontSize = textDescription.fontSize;
            string content = textDescription.content;
            string fontColor = textDescription.fontColor;
          
            Bitmap bmp = new Bitmap(listDone[cur_pos]);
            Graphics g = Graphics.FromImage(bmp);
            Font font = new Font(fontName, fontSize);
            SolidBrush sbrush = new SolidBrush(Color.FromName(fontColor));

            //动态添加 textbox ：tb1
            //TextBox tb1 = new TextBox();
            //tb1.Name = "textBox";
            //tb1.Text = content;
            //tb1.ForeColor = Color.FromName(fontColor);
            //tb1.Font = new Font(fontName, fontSize);
            //tb1.Location = new Point(400, 200);
            ////tb1.Size = new Size(400, 50);
            //tb1.Visible = true;
            //tb1.TextChanged += new System.EventHandler(this.textChange);
            //this.Controls.Add(tb1);
            //tb1.BringToFront();


            //留存字体绘制保存
            float x;
            x = 854 - content.Length * 8 / 2; //x=800;
            g.DrawString(content, font, sbrush, new PointF(x, height - setting.down * CC.A4GBILV + setting.text_photo * CC.A4GBILV));

            string name = listDone[cur_pos];
            name += fontIndex;

            bmp.Save(name);

            string path = name;
            listDone[cur_pos] = path;
            updateTopShow();

        }

        //textbox 适应文本长度
        //private void textChange(object sender, EventArgs e)
        //{
        //    if (textBox_X.Visible)
        //    {
        //        int k = textBox_X.Text.Length;
        //        textBox_X.Width = k * 12;
        //    }
            
        //}

        private void 调整图片大小ToolStripMenuItem_Click2(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            MessageBox.Show("在画布中，鼠标右击选中图片，可调整单张图片");
            //string folderDirPath = System.Environment.CurrentDirectory + "\\done\\";

            //string fullPath = listDone[cur_pos];

            ////string filename = System.IO.Path.GetFileName(fullPath);//文件名  “Default.aspx”
            //string extension = System.IO.Path.GetExtension(fullPath);//扩展名 “.aspx”
            //char[] MyChar = { 'd', 'o', 'n', 'e' };
            //string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fullPath).TrimEnd(MyChar);// 没有扩展名的文件名 “Default”


            //int i = 0;
            //for (; i < listAll.Count; i++)
            //{
            //    string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i].name);
            //    if (fileNameWithoutExtension == fileName)
            //    {
            //        break;
            //    }
            //}

            //string img1 = listAll[i].name;
            //string img2 = null;
            //string img3 = null;
            //string img4 = null;
            //string img5 = null;
            //string img6 = null;
            //string img7 = null;
            //string img8 = null;

            //listChange.Clear();

            //listChange.Add(img1);

            //if (i + 1 < listAll.Count)
            //{
            //    img2 = listAll[i + 1].name;
            //    listChange.Add(img2);
            //}
            //if (i + 2 < listAll.Count)
            //{
            //    img3 = listAll[i + 2].name;
            //    listChange.Add(img3);
            //}
            //if (i + 3 < listAll.Count)
            //{
            //    img4 = listAll[i + 3].name;
            //    listChange.Add(img4);
            //}
            //if (i + 4 < listAll.Count)
            //{
            //    img5 = listAll[i + 4].name;
            //    listChange.Add(img5);
            //}
            //if (i + 5 < listAll.Count)
            //{
            //    img6 = listAll[i + 5].name;
            //    listChange.Add(img6);
            //}
            //if (i + 6 < listAll.Count)
            //{
            //    img7 = listAll[i + 6].name;
            //    listChange.Add(img7);
            //}
            //if (i + 7 < listAll.Count)
            //{
            //    img8 = listAll[i + 7].name;
            //    listChange.Add(img8);
            //}

            ////调 “调整”  窗口    index=0   cur_area=0
            //相册排版界面.SingleDeal singleDeal = new 相册排版界面.SingleDeal(listChange, index, cur_area);
            //singleDeal.ShowDialog();
            //loadImageAfterDeal(3, 1);
        }

        //调整 “该张” 图片
        private void 调整图片大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先选择图片，再进行操作");
                return; 
            }
            
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

            listChange.Clear();
            if (index == 1)
            {
                listChange.Add(listAll[i].name);
            }
            if (index == 2)
            {
                listChange.Add(listAll[i].name);
                if (i + 1 < listAll.Count)
                {
                    listChange.Add(listAll[i + 1].name);
                }
            }
            if (index == 4)
            {
                listChange.Add(listAll[i].name);
                if (i + 1 < listAll.Count)
                {
                    listChange.Add(listAll[i + 1].name);
                }
                if (i + 2 < listAll.Count)
                {
                    listChange.Add(listAll[i + 2].name);
                }
                if (i + 3 < listAll.Count)
                {
                    listChange.Add(listAll[i + 3].name);
                }
            }
            if (index == 8)
            {
                listChange.Add(listAll[i].name);
                if (i + 1 < listAll.Count)
                {
                    listChange.Add(listAll[i + 1].name);
                }
                if (i + 2 < listAll.Count)
                {
                    listChange.Add(listAll[i + 2].name);
                }
                if (i + 3 < listAll.Count)
                {
                    listChange.Add(listAll[i + 3].name);
                }
                if (i + 4 < listAll.Count)
                {
                    listChange.Add(listAll[i + 4].name);
                }
                if (i + 5 < listAll.Count)
                {
                    listChange.Add(listAll[i + 5].name);
                }
                if (i + 6 < listAll.Count)
                {
                    listChange.Add(listAll[i + 6].name);
                }
                if (i + 7 < listAll.Count)
                {
                    listChange.Add(listAll[i + 7].name);
                }
            }
            

            //调 “调整”  窗口
            相册排版界面.SingleDeal singleDeal = new 相册排版界面.SingleDeal(listChange,index,cur_area);
            singleDeal.ShowDialog();
            loadImageAfterDeal(3 , 1);
        }

        //上方listView1的index
        int select_index = 0;
        private void listView1_Click(object sender, EventArgs e)
        {
            
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

            //loadImage1();
            loadImage();

        }

        //左侧照片夹listView2的index
        int select_index_left = -1;

        private void listView2_Click(object sender, EventArgs e)
        {
           
            ListView.SelectedIndexCollection left_indexes = this.listView2.SelectedIndices;
            
            if (left_indexes.Count > 0)
            {
                select_index_left = left_indexes[0];
            }
         
        }
        
        private void 更换图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;//等于true表示可以选择多个文件
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "图片|*.jpg";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string path = dlg.FileNames[0];
                //listDone[cur_pos] = path;
                //listCur[cur_pos] = path;

                image = GetImageFromServer(path);

                //----------------------------------------------------------------
                var width = CC.A4CHANG_XS;
                var height = CC.A4GAO_XS;

                Bitmap bmSmall = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                Graphics grSmall = Graphics.FromImage(bmSmall);
                grSmall.FillRectangle(Brushes.White, 0, 0, width, height);

                float width_xs = (CC.A4CHANG - setting.left - setting.right) * CC.A4GBILV;
                float height_xs = (CC.A4GAO - setting.up - setting.down) * CC.A4GBILV;

                float xs = 0.0f;
                float x_xs = width_xs / image.Width;
                float y_xs = height_xs / image.Height;
                //1
                if (x_xs < y_xs)
                {
                    xs = x_xs;
                    grSmall.DrawImage(image,
                            setting.left * CC.A4GBILV,
                            setting.up * CC.A4GBILV + (height_xs - image.Height * xs) / 2,
                            width_xs,
                            image.Height * xs);
                }
                else
                {
                    xs = y_xs;
                    grSmall.DrawImage(image,
                            setting.left * CC.A4GBILV + (width_xs - image.Width * xs) / 2,
                            setting.up * CC.A4GBILV,
                            image.Width * xs,
                            height_xs);
                }

                this.pictureBox1.Image = bmSmall;

                var vv = System.IO.Path.GetFileNameWithoutExtension(listDone[cur_pos]);
                var vv2 = System.IO.Path.GetExtension(path);
                vv2 = ".jpg";

                Bitmap im = bmSmall;
                im.SetResolution(300, 300);
                //转成jpg

                var eps = new EncoderParameters(1);
                var ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                eps.Param[0] = ep;

                var jpsEncodeer = GetEncoder(ImageFormat.Jpeg);
                //保存图片
                String imgurl = setting.Path1 + "\\" + vv + "replace_done" + vv2;
                im.Save(imgurl, jpsEncodeer, eps);

                //-------------------------------------------------------------------

                listDone[cur_pos] = imgurl;
                listCur[cur_pos] = imgurl;
                listAll[cur_pos].name = imgurl;


                updateTopShow();

               
            }
        }

        private void jpg格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
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
        private void 导出pngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            
           
            //打开选择文件夹对话框
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                //获取用户选择的文件夹路径
                string folderDirPath = folderBrowserDialog.SelectedPath;

                //获取目录与子目录
                DirectoryInfo dir = new DirectoryInfo(System.Environment.CurrentDirectory + "\\done");
                //获取当前目录JPG文件列表 GetFiles获取指定目录中文件的名称(包括其路径)
                FileInfo[] fileInfo = dir.GetFiles("*.jpg");

                
                for (int i = 0; i < fileInfo.Length; i++)
                {
                    var vv = System.IO.Path.GetFileNameWithoutExtension(fileInfo[i].FullName);
                    var vv2 = ".png";
                    //listChange.Add(fileInfo[i].FullName);
                    Image jpgimage = Image.FromFile(fileInfo[i].FullName);
                    Bitmap bitmap = new Bitmap(jpgimage);
                    bitmap.MakeTransparent(Color.Transparent);//透明背景 
                    Bitmap im = bitmap;

                    String imgurl = folderDirPath + "\\" + vv +  vv2;
                    im.Save(imgurl, System.Drawing.Imaging.ImageFormat.Png);
                    jpgimage.Dispose();
                    bitmap.Dispose();
                }
                MessageBox.Show("导出成功");
                 
                System.Diagnostics.Process.Start("explorer.exe", folderDirPath);
             }
            else if (result == DialogResult.Cancel)
            {
                //MessageBox.Show("取消显示图片列表");
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
            if (listDone.Count == 0)
            {
                MessageBox.Show("请先打开图片，再进行操作");
                return;

            }
            this.saveFileDialog1.Filter = "PDF(*.pdf)|*.pdf";//设置文件类型
            saveFileDialog1.FileName = "相册 "+ DateTime.Now.ToLongDateString().ToString();//设置默认文件名
            saveFileDialog1.DefaultExt = "pdf";//设置默认格式（可以不设）
            saveFileDialog1.AddExtension = true;//设置自动在文件名中添加扩展名
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                toPdf(saveFileDialog1.FileName);
                MessageBox.Show("导出成功");
                this.Dispose();
                string localFilePath = saveFileDialog1.FileName.ToString();
                System.Diagnostics.Process.Start(localFilePath);
            }
            

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
                        
                        if (StartSetting2(101) == 1)
                        {
                            if (i + 1 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 1].name);
                                //string fileName= (i + 1) + " - " + listDone.Count;
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;
                    case 7:
                    case 8:
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
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
                        }
                        if (StartSetting2(101) == 1)
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
                            if (i + 4 < listAll.Count)
                            {
                            }
                            if (i + 5 < listAll.Count)
                            {
                            }
                            if (i + 6 < listAll.Count)
                            {
                            }
                            if (i + 7 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 7].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;
                }
                bean.type = 1;
                updateTopShow();
            }
            else
            {
                if (listDone.Count == 0)
                {
                    MessageBox.Show("请先打开图片，再进行操作");
                    return;

                }
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
                    case 7:
                    case 8:
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
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
                        }
                        if (StartSetting2(102) == 1)
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
                            if (i + 4 < listAll.Count)
                            {
                            }
                            if (i + 5 < listAll.Count)
                            {
                            }
                            if (i + 6 < listAll.Count)
                            {
                            }
                            if (i + 7 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 7].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;
                }
                bean.type = 2;
                updateTopShow();
            }
            else
            {
                if (listDone.Count == 0)
                {
                    MessageBox.Show("请先打开图片，再进行操作");
                    return;

                }
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
                    case 7:
                    case 8:
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
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
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
                            if (i + 4 < listAll.Count)
                            {
                            }
                            if (i + 5 < listAll.Count)
                            {
                            }
                            if (i + 6 < listAll.Count)
                            {
                            }
                            if (i + 7 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 7].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;
                }
                bean.type = 3;
                updateTopShow();
            }
            else
            {
                if (listDone.Count == 0)
                {
                    MessageBox.Show("请先打开图片，再进行操作");
                    return;

                }
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
                        
                        if (StartSetting2(104) == 1)
                        {
                            listDone.RemoveAt(i + 1);
                           
                        }
                        break;
                    case 3:
                    case 4:
                        MessageBox.Show("相同类型无需转换");
                        return;
                    case 5:
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
                    case 7:
                    case 8:
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
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
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
                            if (i + 4 < listAll.Count)
                            {
                            }
                            if (i + 5 < listAll.Count)
                            {
                            }
                            if (i + 6 < listAll.Count)
                            {
                            }
                            if (i + 7 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 7].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;
                }
                bean.type = 4;
                updateTopShow();
            }
            else
            {
                if (listDone.Count == 0)
                {
                    MessageBox.Show("请先打开图片，再进行操作");
                    return;

                }
            }
        }

        private void 四张合并ToolStripMenuItem1_Click(object sender, EventArgs e)
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
                        if (i > listAll.Count - 4)
                        {
                            MessageBox.Show("后续张数不足，无法合并");
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
                        if (StartSetting2(105) == 1)
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
                    case 7:
                    case 8:
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
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
                        }
                        if (StartSetting2(105) == 1)
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
                            if (i + 4 < listAll.Count)
                            {
                            }
                            if (i + 5 < listAll.Count)
                            {
                            }
                            if (i + 6 < listAll.Count)
                            {
                            }
                            if (i + 7 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 7].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;
                }
                bean.type = 5;
                updateTopShow();
            }
            else
            {
                if (listDone.Count == 0)
                {
                    MessageBox.Show("请先打开图片，再进行操作");
                    return;

                }
            }
            
        }

        private void 四张合并转90度ToolStripMenuItem1_Click(object sender, EventArgs e)
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
                        if (i > listAll.Count - 4)
                        {
                            MessageBox.Show("后续张数不足，无法合并");
                            return;
                        }
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        listCur.Add(listAll[i + 1].name);
                        listCur.Add(listAll[i + 2].name);
                        listCur.Add(listAll[i + 3].name);
                        if (StartSetting2(106) == 1)
                        {
                            listDone.RemoveAt(i + 1);
                            listDone.RemoveAt(i + 2);
                            listDone.RemoveAt(i + 3);
                        }
                        break;
                    case 3:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        if (i + 1 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 1].name);
                        }
                        if (StartSetting2(106) == 1)
                        {
                            ;
                        }
                        break;
                    case 4:
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
                        if (StartSetting2(106) == 1)
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
                    case 5:
                        listCur.Clear();
                        listCur.Add(listAll[i].name);
                        listCur.Add(listAll[i + 1].name);
                        listCur.Add(listAll[i + 2].name);
                        listCur.Add(listAll[i + 3].name);
                    
                        if (StartSetting2(106) == 1)
                        {
                            ;
                        }
                        break;
                    case 6:
                        MessageBox.Show("相同类型无需转换");
                        return;
                    case 7:
                    case 8:
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
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
                        }
                        if (StartSetting2(106) == 1)
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
                            if (i + 4 < listAll.Count)
                            {
                            }
                            if (i + 5 < listAll.Count)
                            {
                            }
                            if (i + 6 < listAll.Count)
                            {
                            }
                            if (i + 7 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 7].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;

                }
                bean.type = 6;
                updateTopShow();
            }
            else
            {
                if (listDone.Count == 0)
                {
                    MessageBox.Show("请先打开图片，再进行操作");
                    return;

                }
            }
            
        }
        private void 八张合并ToolStripMenuItem1_Click(object sender, EventArgs e)
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
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
                        }
                        if (StartSetting2(107) == 1)
                        {
                            listDone.RemoveAt(i + 1);
                            listDone.RemoveAt(i + 2);
                            listDone.RemoveAt(i + 3);
                            listDone.RemoveAt(i + 4);
                            listDone.RemoveAt(i + 5);
                            listDone.RemoveAt(i + 6);
                            listDone.RemoveAt(i + 7);
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
                        if (i + 2 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 2].name);
                        }
                        if (i + 3 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 3].name);
                        }
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
                        }
                        if (StartSetting2(107) == 1)
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
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
                        }
                        if (StartSetting2(107) == 1)
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
                            if (i + 4 < listAll.Count)
                            {
                            }
                            if (i + 5 < listAll.Count)
                            {
                            }
                            if (i + 6 < listAll.Count)
                            {
                            }
                            if (i + 7 < listAll.Count)
                            {
                            }
                           
                        }
                        break;
                    case 7:
                        MessageBox.Show("相同类型无需转换");
                        return;
                    case 8:
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
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
                        }
                        if (StartSetting2(107) == 1)
                        {
                            if (i + 1 < listAll.Count)
                            {
                            }
                            if (i + 2 < listAll.Count)
                            {
                                /*
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 2].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            */
                                }
                            if (i + 3 < listAll.Count)
                            {
                            }
                            if (i + 4 < listAll.Count)
                            {
                            }
                            if (i + 5 < listAll.Count)
                            {
                            }
                            if (i + 6 < listAll.Count)
                            {
                            }
                            if (i + 7 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 7].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;

                }
                bean.type = 7;
                updateTopShow();
            }
            else
            {
                if (listDone.Count == 0)
                {
                    MessageBox.Show("请先打开图片，再进行操作");
                    return;

                }
            }
        }

        private void 八张合并转90度ToolStripMenuItem1_Click(object sender, EventArgs e)
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
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
                        }

                       
                        if (StartSetting2(108) == 1)
                        {
                            listDone.RemoveAt(i + 1);
                            listDone.RemoveAt(i + 2);
                            listDone.RemoveAt(i + 3);
                            listDone.RemoveAt(i + 4);
                            listDone.RemoveAt(i + 5);
                            listDone.RemoveAt(i + 6);
                            listDone.RemoveAt(i + 7);
                            /*
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
                             */

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
                        if (i + 2 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 2].name);
                        }
                        if (i + 3 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 3].name);
                        }
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
                        }
                        if (StartSetting2(108) == 1)
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
                            if (i + 4 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 4].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 5 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 5].name);
                                listDone.Insert(cur_pos + 2, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 6 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 6].name);
                                listDone.Insert(cur_pos + 3, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 7 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 7].name);
                                listDone.Insert(cur_pos + 3, folderDirPath + fileName + "done.jpg");
                            }

                            /*
                            listDone.RemoveAt(i + 1);
                            listDone.RemoveAt(i + 2);
                            listDone.RemoveAt(i + 3);
                            listDone.RemoveAt(i + 4);
                            listDone.RemoveAt(i + 5);
                            listDone.RemoveAt(i + 6);
                            listDone.RemoveAt(i + 7);
                            */
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
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
                        }

                        if (StartSetting2(108) == 1)
                        {
                            if (i + 1 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 1].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 2 < listAll.Count)
                            {
                               
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 2].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                               
                            }
                            if (i + 3 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 3].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 4 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 4].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 5 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 5].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 6 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 6].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                            if (i + 7 < listAll.Count)
                            {
                                string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[i + 7].name);
                                listDone.Insert(cur_pos + 1, folderDirPath + fileName + "done.jpg");
                            }
                        }
                        break;
                    case 7:
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
                        if (i + 4 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 4].name);
                        }
                        if (i + 5 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 5].name);
                        }
                        if (i + 6 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 6].name);
                        }
                        if (i + 7 < listAll.Count)
                        {
                            listCur.Add(listAll[i + 7].name);
                        }

                        if (StartSetting2(108) == 1)
                        {
                            ;
                        }
                        break;
                    case 8:
                        MessageBox.Show("相同类型无需转换");
                        return;
                }
                bean.type = 8;
                updateTopShow();
            }
            else
            {
                if (listDone.Count == 0)
                {
                    MessageBox.Show("请先打开图片，再进行操作");
                    return;

                }
            }
        }


        private void Layout_Load(object sender, EventArgs e)
        {
            //point = this.Location;

        }
        private void Layout_Move(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        

        
        //鼠标拖拽移动 中展示出来的框体大小  
        public void AcquireRectangleImage(Image source, Rectangle rect)
        {
            if (source == null || rect.IsEmpty) return;

            Bitmap bmSmall = new Bitmap(CC.A4CHANG_XS, CC.A4GAO_XS, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            rectSmall = new Rectangle(0, 0, CC.A4CHANG_XS, CC.A4GAO_XS);

            using (Graphics grSmall = Graphics.FromImage(bmSmall))
            {
                grSmall.FillRectangle(Brushes.White, 0, 0, CC.A4CHANG_XS, CC.A4GAO_XS);
                //grSmall.Clear(color_back);
                grSmall.DrawImage(source,
                                  rectSmall,
                                  rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top,
                                  GraphicsUnit.Pixel);

                this.pictureBox1.Image = bmSmall;

            }
        }

        public Image GetImageFromServer(string fileName)
        {
            Image img = null;
            string filePath = fileName;// Application.StartupPath + "\\Image\\" + fileName;
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

        /*
         * pictureBox1 中的鼠标移动
         * 事件
         */
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_bDown == true)
            {
                point2.X -= e.X - point1.X;
                point2.Y -= e.Y - point1.Y;

                //AcquireRectangleImage(image, new Rectangle(point2, size));

                point1.X = e.X;
                point1.Y = e.Y;
            }
        }
        //鼠标 获取点击位置 判断area的值  调整该张图片
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
            m_bDown = true;
            point1.X = e.X;
            point1.Y = e.Y;
            //-------------
            int x = point1.X;
            int y = point1.Y;
            if (setting_index == 0)
            {
                //没读档
                if (setting.index == 1 || setting.index == 2)
                {
                    index = 1;
                    if (listCur.Count > 0)
                    {
                        cur_area = 1;
                    }
                }
                if (setting.index == 3 || setting.index == 4)
                {
                    index = 2;
                    if (y > 257)
                    {
                        cur_area = 2;
                    }
                    else
                    {
                        cur_area = 1;
                    }
                }
                if (setting.index == 5 || setting.index == 6)
                {
                    index = 4;
                    if (y > 257)
                    {
                        if (x < 349)
                        {
                            //cur_area = 1;
                            cur_area = 3;
                        }
                        else
                        {
                            cur_area = 4;

                        }
                    }
                    else
                    {
                        if (x < 349)
                        {
                            //cur_area = 2;
                            cur_area = 1;
                        }
                        else
                        {
                            //cur_area = 3;
                            cur_area = 2;
                        }
                    }
                }
                if (setting.index == 7 || setting.index == 8)
                {
                    index = 8;
                    if (x < 349)
                    {//左
                        if (y < 128)
                        {
                            //cur_area = 4;
                            cur_area = 1;
                        }
                        if (y > 129 && y < 257)
                        {
                            cur_area = 3;
                        }
                        if (y > 257 && y < 385)
                        {
                            //cur_area = 2;
                            cur_area = 5;
                        }
                        if (y > 386)
                        {
                            //cur_area = 1;
                            cur_area = 7;
                        }
                    }
                    else
                    {//右
                        if (y < 128)
                        {
                            //cur_area = 5;
                            cur_area = 2;
                        }
                        if (y > 129 && y < 257)
                        {
                            //cur_area = 6;
                            cur_area = 4;
                        }
                        if (y > 257 && y < 385)
                        {
                            //cur_area = 7;
                            cur_area = 6;
                        }
                        if (y > 386)
                        {
                            cur_area = 8;
                        }

                    }
                }


            }
            else
            {
                //读档

                if (setting_index == 1 || setting_index == 2)
                {
                    index = 1;
                    if (listCur.Count > 0)
                    {
                        cur_area = 1;
                    }
                }
                if (setting_index == 3 || setting_index == 4)
                {
                    index = 2;
                    if (y > 257)
                    {
                        cur_area = 2;
                    }
                    else
                    {
                        cur_area = 1;
                    }
                }
                if (setting_index == 5 || setting_index == 6)
                {
                    index = 4;
                    if (y > 257)
                    {
                        if (x < 349)
                        {
                            //cur_area = 1;
                            cur_area = 3;
                        }
                        else
                        {
                            cur_area = 4;

                        }
                    }
                    else
                    {
                        if (x < 349)
                        {
                            //cur_area = 2;
                            cur_area = 1;
                        }
                        else
                        {
                            //cur_area = 3;
                            cur_area = 2;
                        }
                    }
                }
                if (setting_index == 7 || setting_index == 8)
                {
                    index = 8;
                    if (x < 349)
                    {//左
                        if (y < 128)
                        {
                            //cur_area = 4;
                            cur_area = 1;
                        }
                        if (y > 129 && y < 257)
                        {
                            cur_area = 3;
                        }
                        if (y > 257 && y < 385)
                        {
                            //cur_area = 2;
                            cur_area = 5;
                        }
                        if (y > 386)
                        {
                            //cur_area = 1;
                            cur_area = 7;
                        }
                    }
                    else
                    {//右
                        if (y < 128)
                        {
                            //cur_area = 5;
                            cur_area = 2;
                        }
                        if (y > 129 && y < 257)
                        {
                            //cur_area = 6;
                            cur_area = 4;
                        }
                        if (y > 257 && y < 385)
                        {
                            //cur_area = 7;
                            cur_area = 6;
                        }
                        if (y > 386)
                        {
                            cur_area = 8;
                        }

                    }
                }


            }
            


            //鼠标右键点击   测试
            //if (e.Button == MouseButtons.Right)
            //{
            //    MessageBox.Show(point1.ToString(), cur_area.ToString());
            //}

            //鼠标相对屏幕 拖拽功能   测试
            //p.X = MousePosition.X;
            //p.Y = MousePosition.Y;
            //MessageBox.Show(p.ToString(), drag_area.ToString());
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            m_bDown = false;
           
        }
         /*
         * layout 窗体 中的鼠标移动
         * 事件
         */
        private void Layout_MouseUp(object sender, MouseEventArgs e)
        {
            l_bDown = false;
        }

        private void Layout_MouseMove(object sender, MouseEventArgs e)
        {
            if (l_bDown == true)
            {
                point4.X -= e.X - point3.X;
                point4.Y -= e.Y - point3.Y;

                point3.X = e.X;
                point3.Y = e.Y;
            }
        }

        private void Layout_MouseDown(object sender, MouseEventArgs e)
        {
            l_bDown = true;
            point3.X = e.X;
            point3.Y = e.Y;
            //鼠标点击测试坐标
            //MessageBox.Show(point3.ToString(), cur_area.ToString());
        }

       
        /*
         * ContextMenuTrip 
         * 复制剪切粘贴删除
         * 功能
         */

        //左侧listview2 右键 菜单栏 复制粘贴删除剪切
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection left_indexes = this.listView2.SelectedIndices;
            if (left_indexes.Count == 0)
            {
                MessageBox.Show("请先选择图片，再进行操作");
                return;
            }
            select_index_left = left_indexes[0];
            listLeft.RemoveAt(select_index_left);
            updateLeftShow();

        }

        string img_buff = null;

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection left_indexes = this.listView2.SelectedIndices;
            if (left_indexes.Count == 0)
            {
                MessageBox.Show("请先选择图片，再进行操作");
                return;
            }
            select_index_left = left_indexes[0];
            img_buff = listLeft[select_index_left];
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection left_indexes = this.listView2.SelectedIndices;
            if (left_indexes.Count == 0)
            {
                MessageBox.Show("请先选择图片，再进行操作");
                return;
            }
            select_index_left = left_indexes[0];
            img_buff = listLeft[select_index_left];
            listLeft.RemoveAt(select_index_left);
            updateLeftShow();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img_buff == null)
            {
               // MessageBox.Show("无可粘贴图片");
                return;
            }
            ListView.SelectedIndexCollection left_indexes = this.listView2.SelectedIndices;
            if (left_indexes.Count == 0)
            {
                MessageBox.Show("请先选择图片，再进行操作");
                return;
            }
            select_index_left = left_indexes[0];
            listLeft.Insert(select_index_left+1, img_buff);
            updateLeftShow();
        }

        //上方 listview1 右键 菜单栏 复制粘贴删除剪切
        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection up_indexes = this.listView1.SelectedIndices;
            if (up_indexes.Count == 0)
            {
                MessageBox.Show("请先选择图片，再进行操作");
                return;
            }
            select_index = up_indexes[0];
            listDone.RemoveAt(select_index);
            //listCur.RemoveAt(select_index);
            updateTopShow();
        }

        private void 复制ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection up_indexes = this.listView1.SelectedIndices;
            if (up_indexes.Count == 0)
            {
                MessageBox.Show("请先选择图片，再进行操作");
                return;
            }
            select_index = up_indexes[0];
            img_buff = listDone[select_index];
        }

        private void 剪切ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection up_indexes = this.listView1.SelectedIndices;
            if (up_indexes.Count == 0)
            {
                MessageBox.Show("请先选择图片，再进行操作");
                return;
            }
            select_index = up_indexes[0];
            img_buff = listDone[select_index];
            listDone.RemoveAt(select_index);
            //listCur.RemoveAt(select_index);
            updateTopShow();

        }

        private void 粘贴ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (img_buff == null)
            {
                // MessageBox.Show("无可粘贴图片");
                return;
            }
            ListView.SelectedIndexCollection up_indexes = this.listView1.SelectedIndices;
            if (up_indexes.Count == 0)
            {
                MessageBox.Show("请先选择图片，再进行操作");
                return;
            }
            select_index = up_indexes[0];
            listDone.Insert(select_index+1, img_buff);
            updateTopShow();
        }

        /*
         * 拖拽事件
         * 左边 listview2 -> picturebox1
         */
        FirstDlg modelDlg = new FirstDlg();
        private void listView2_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //左边 listview2 的数据传送
            ListView.SelectedIndexCollection left_indexes = this.listView2.SelectedIndices;

            if (left_indexes.Count > 0)
            {
                select_index_left = left_indexes[0];
            }

            //string path1 = e.Item.ToString(); 获取的是label
            //path_url = path1;
            //获取的左边图片的内容
            path_url = listLeft[select_index_left];
            DoDragDrop(path_url, DragDropEffects.Copy);
        }

        //处理 drag drop 事件
        private void left_DragDrop(object sender, DragEventArgs e)
        {
            listCur.Clear();
            for(int i = 0; i < listAll.Count; i++)
            {
                listCur.Add(listAll[i].name);
            }

            //获取拖拽到picturebox1时的图片顺序 ：drag_area
            //-------------
            int x = MousePosition.X;
            int y = MousePosition.Y;
            if (setting_index == 0)
            {
                //没读档
                if (setting.index == 1 || setting.index == 2)
                {
                    index = 1;
                    if (listCur.Count > 0)
                    {
                        drag_area = 1;
                    }
                }
                if (setting.index == 3 || setting.index == 4)
                {
                    index = 2;
                    if (y > 499)
                    {
                        drag_area = 2;
                    }
                    else
                    {
                        drag_area = 1;
                    }
                }
                if (setting.index == 5 || setting.index == 6)
                {
                    index = 4;
                    if (y > 499)
                    {
                        if (x < 854)
                        {
                            //drag_area = 1;
                            drag_area = 3;
                        }
                        else
                        {
                            drag_area = 4;

                        }
                    }
                    else
                    {
                        if (x < 854)
                        {
                            //drag_area = 2;
                            drag_area = 1;
                        }
                        else
                        {
                            //drag_area = 3;
                            drag_area = 2;
                        }
                    }
                }
                if (setting.index == 7 || setting.index == 8)
                {
                    index = 8;
                    if (x < 854)
                    {//左
                        if (y < 370)
                        {
                            //drag_area = 4;
                            drag_area = 1;
                        }
                        if (y > 370 && y < 498)
                        {
                            drag_area = 3;
                        }
                        if (y > 499 && y < 627)
                        {
                            //drag_area = 2;
                            drag_area = 5;
                        }
                        if (y > 627)
                        {
                            //drag_area = 1;
                            drag_area = 7;
                        }
                    }
                    else
                    {//右
                        if (y < 370)
                        {
                            //drag_area = 5;
                            drag_area = 2;
                        }
                        if (y > 370 && y < 498)
                        {
                            //drag_area = 6;
                            drag_area = 4;
                        }
                        if (y > 499 && y < 627)
                        {
                            //drag_area = 7;
                            drag_area = 6;
                        }
                        if (y > 627)
                        {
                            drag_area = 8;
                        }

                    }
                }

            }
            else
            {
                //读档
                if (setting_index == 1 || setting_index == 2)
                {
                    index = 1;
                    if (listCur.Count > 0)
                    {
                        drag_area = 1;
                    }
                }
                if (setting_index == 3 || setting_index == 4)
                {
                    index = 2;
                    if (y > 499)
                    {
                        drag_area = 2;
                    }
                    else
                    {
                        drag_area = 1;
                    }
                }
                if (setting_index == 5 || setting_index == 6)
                {
                    index = 4;
                    if (y > 499)
                    {
                        if (x < 854)
                        {
                            //drag_area = 1;
                            drag_area = 3;
                        }
                        else
                        {
                            drag_area = 4;

                        }
                    }
                    else
                    {
                        if (x < 854)
                        {
                            //drag_area = 2;
                            drag_area = 1;
                        }
                        else
                        {
                            //drag_area = 3;
                            drag_area = 2;
                        }
                    }
                }
                if (setting_index == 7 || setting_index == 8)
                {
                    index = 8;
                    if (x < 854)
                    {//左
                        if (y < 370)
                        {
                            //drag_area = 4;
                            drag_area = 1;
                        }
                        if (y > 370 && y < 498)
                        {
                            drag_area = 3;
                        }
                        if (y > 499 && y < 627)
                        {
                            //drag_area = 2;
                            drag_area = 5;
                        }
                        if (y > 627)
                        {
                            //drag_area = 1;
                            drag_area = 7;
                        }
                    }
                    else
                    {//右
                        if (y < 370)
                        {
                            //drag_area = 5;
                            drag_area = 2;
                        }
                        if (y > 370 && y < 498)
                        {
                            //drag_area = 6;
                            drag_area = 4;
                        }
                        if (y > 499 && y < 627)
                        {
                            //drag_area = 7;
                            drag_area = 6;
                        }
                        if (y > 627)
                        {
                            drag_area = 8;
                        }

                    }
                }


            }
            

            //-------------
            ListView.SelectedIndexCollection up_indexes = this.listView1.SelectedIndices;
            if (up_indexes.Count == 0)
            {
                return;
            }
            select_index = up_indexes[0];
          

            if (e.Data.Equals(typeof(string)))
            {
                //左侧图片 path_url = listLeft[select_index_left]
                path_url = e.Data.GetData(typeof(string)).ToString();
            }

            if (path_url.Length > 0)
            {
                m_bDown = false;

                //----------------------测试拖拉后的鼠标坐标
               
                //p.X = MousePosition.X;
                //p.Y = MousePosition.Y;
               // MessageBox.Show(p.ToString(), drag_area.ToString());
                //-----------------

                image = GetImageFromServer(path_url);//左侧图片
                //-------------------------------
                //listChange
                listChange.Clear();

                string fullPath = listDone[cur_pos];

                //string filename = System.IO.Path.GetFileName(fullPath);//文件名  “Default.aspx”
                string extension = System.IO.Path.GetExtension(fullPath);//扩展名 “.aspx”
                char[] MyChar = { 'd', 'o', 'n', 'e' };
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fullPath).TrimEnd(MyChar);
                int m = 0;
                for (; m < listAll.Count; m++)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(listAll[m].name);
                    if (fileNameWithoutExtension == fileName)
                    {
                        break;
                    }
                }

                if (index == 1)
                {
                    listChange.Add(listAll[m].name);
                }
                if (index == 2)
                {
                    listChange.Add(listAll[m].name);
                    if (m + 1 < listAll.Count)
                    {
                        listChange.Add(listAll[m + 1].name);
                    }
                }
                if (index == 4)
                {
                    listChange.Add(listAll[m].name);
                    if (m + 1 < listAll.Count)
                    {
                        listChange.Add(listAll[m + 1].name);
                    }
                    if (m + 2 < listAll.Count)
                    {
                        listChange.Add(listAll[m + 2].name);
                    }
                    if (m + 3 < listAll.Count)
                    {
                        listChange.Add(listAll[m + 3].name);
                    }
                }
                if (index == 8)
                {
                    listChange.Add(listAll[m].name);
                    if (m + 1 < listAll.Count)
                    {
                        listChange.Add(listAll[m + 1].name);
                    }
                    if (m + 2 < listAll.Count)
                    {
                        listChange.Add(listAll[m + 2].name);
                    }
                    if (m + 3 < listAll.Count)
                    {
                        listChange.Add(listAll[m + 3].name);
                    }
                    if (m + 4 < listAll.Count)
                    {
                        listChange.Add(listAll[m + 4].name);
                    }
                    if (m + 5 < listAll.Count)
                    {
                        listChange.Add(listAll[m + 5].name);
                    }
                    if (m + 6 < listAll.Count)
                    {
                        listChange.Add(listAll[m + 6].name);
                    }
                    if (m + 7 < listAll.Count)
                    {
                        listChange.Add(listAll[m + 7].name);
                    }
                }

                //---
                imgpath.Clear();
                for (int j = 0; j < listChange.Count; j++)
                {
                    
                    if (index == 1)
                    {
                        imgpath.Add(listChange[j]);
                    }
                    if (index == 2)
                    {
                        imgpath.Add(listChange[j]);
                        if (j + 1 < listChange.Count)
                        {
                            // imgpath2 = listChange[j + 1];
                            imgpath.Add(listChange[j + 1]);
                        }
                    }
                    if (index == 4)
                    {
                        imgpath.Add(listChange[j]);
                        if (j + 1 < listChange.Count)
                        {
                            imgpath.Add(listChange[j + 1]);
                        }
                        if (j + 2 < listChange.Count)
                        {
                            imgpath.Add(listChange[j + 2]);
                        }
                        if (j + 3 < listChange.Count)
                        {
                            imgpath.Add(listChange[j + 3]);
                        }
                    }
                    if (index == 8)
                    {
                        imgpath.Add(listChange[j]);
                        if (j + 1 < listChange.Count)
                        {
                            imgpath.Add(listChange[j + 1]);
                        }
                        if (j + 2 < listChange.Count)
                        {
                            imgpath.Add(listChange[j + 2]);
                        }
                        if (j + 3 < listChange.Count)
                        {
                            imgpath.Add(listChange[j + 3]);
                        }
                        if (j + 4 < listChange.Count)
                        {
                            imgpath.Add(listChange[j + 4]);
                        }
                        if (j + 5 < listChange.Count)
                        {
                            imgpath.Add(listChange[j + 5]);
                        }
                        if (j + 6 < listChange.Count)
                        {
                            imgpath.Add(listChange[j + 6]);
                        }
                        if (j + 7 < listChange.Count)
                        {
                            imgpath.Add(listChange[j + 7]);
                        }
                    }


                }

                listDrag.Clear();
                //替换掉左边的图 后 的listDrag
                if (index == 1)
                {
                    listDrag.Add(imgpath[0]);
                    listDrag[drag_area - 1] = path_url;
                    //解决拖拽替换后 再右键替换 picturebox框中是原来图片的问题
                    //同时要将替换后的图片在listCur和listAll中存成原来图片的名字。
                    //!!!
                    listCur[m + drag_area - 1] = path_url;
                    listAll[m + drag_area - 1].name = path_url;

                }

                if (index == 2)
                {
                    listDrag.Add(imgpath[0]);
                    listDrag.Add(imgpath[1]);
                    listDrag[drag_area - 1] = path_url;
                    //解决拖拽替换后 再右键替换 picturebox框中是原来图片的问题，
                    //同时要将替换后的图片在listCur和listAll中存成原来图片的名字。
                    //!!!
                    listCur[m + drag_area - 1] = path_url;
                    listAll[m + drag_area - 1].name = path_url;

                }
                if (index == 4)
                {
                    listDrag.Add(imgpath[0]);
                    listDrag.Add(imgpath[1]);
                    listDrag.Add(imgpath[2]);
                    listDrag.Add(imgpath[3]);
                    listDrag[drag_area - 1] = path_url;
                    //解决拖拽替换后 再右键替换 picturebox框中是原来图片的问题
                    //!!!
                    listCur[m + drag_area - 1] = path_url;
                    listAll[m + drag_area - 1].name = path_url;

                }
                if (index == 8)
                {
                    listDrag.Add(imgpath[0]);
                    listDrag.Add(imgpath[1]);
                    listDrag.Add(imgpath[2]);
                    listDrag.Add(imgpath[3]);
                    listDrag.Add(imgpath[4]);
                    listDrag.Add(imgpath[5]);
                    listDrag.Add(imgpath[6]);
                    listDrag.Add(imgpath[7]);
                    listDrag[drag_area - 1] = path_url;
                    //解决拖拽替换后 再右键替换 picturebox框中是原来图片的问题
                    //!!!
                    listCur[m + drag_area - 1] = path_url;
                    listAll[m + drag_area - 1].name = path_url;

                }


                using (BackgroundWorker bw = new BackgroundWorker())
                {
                    bw.WorkerReportsProgress = true; // 设置可以通告进度
                    bw.WorkerSupportsCancellation = true; // 设置可以取消
                    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                    bw.ProgressChanged += new ProgressChangedEventHandler(UpdateProgress);
                    bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                    bw.RunWorkerAsync("Tank");
                }
                modelDlg.StartPosition = FormStartPosition.CenterParent;
                modelDlg.label1.Text = "图片处理中，请稍后......";
                modelDlg.ShowDialog(this);

            }

        }

        private void left_DragEnter(object sender, DragEventArgs e)
        {
            //判断是不是可以接收的数据类型  
            //DataFormats.FileDrop  typeof(string)
            if (e.Data.GetDataPresent(typeof(string)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // 这里是后台线程， 是在另一个线程上完成的
            // 这里是真正做事的工作线程
            // 可以在这里做一些费时的，复杂的操作
            BackgroundWorker bw = sender as BackgroundWorker;
            if (index == 1)
            {
                HechengA1();
                
                updateTopShow();
            }
            if (index == 2)
            {
                HechengB1();
               
                updateTopShow();
            }
            if (index == 4)
            {
                HechengC1();
                ////listCur[cur_pos+drag_area - 1] = path_url;
                ////listAll[cur_pos+drag_area - 1].name = path_url;
                //listChange[drag_area - 1] = path_url;
                //imgpath[drag_area - 1] = path_url;
                //listDrag[drag_area-1] = path_url;
                
                updateTopShow();
            }
            if (index == 8)
            {
                HechengD1();
                
                updateTopShow();
            }

            bw.ReportProgress(100);
        }

        void UpdateProgress(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            if (i == 100)
            {
                modelDlg.Close();
                MessageBox.Show("保存成功");
               
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //这时后台线程已经完成，并返回了主线程，所以可以直接使用UI控件了 
        }

        /*
         * 存档 读档
         * 写入txt文件
         */
        string CurPath = "listCur.txt";
        string AllPath = "listAll.txt";
        string DonePath = "listDone.txt";
        string LeftPath = "listLeft.txt";
        string IndexPath = "setting_index.txt";
        bool flag = false;

        private void 存档ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //清空原文档中的数据
                //listCur.txt
            FileStream stream1 = File.Open(CurPath, FileMode.OpenOrCreate, FileAccess.Write);
            stream1.Seek(0, SeekOrigin.Begin);
            stream1.SetLength(0);
            stream1.Close();
                //listAll.txt
            FileStream stream2 = File.Open(AllPath, FileMode.OpenOrCreate, FileAccess.Write);
            stream2.Seek(0, SeekOrigin.Begin);
            stream2.SetLength(0);
            stream2.Close();
                //listDone.txt
            FileStream stream3 = File.Open(DonePath, FileMode.OpenOrCreate, FileAccess.Write);
            stream3.Seek(0, SeekOrigin.Begin);
            stream3.SetLength(0);
            stream3.Close();
                //listLeft.txt
            FileStream stream4 = File.Open(LeftPath, FileMode.OpenOrCreate, FileAccess.Write);
            stream4.Seek(0, SeekOrigin.Begin);
            stream4.SetLength(0);
            stream4.Close();
                //setting_index.txt
            FileStream stream5 = File.Open(IndexPath, FileMode.OpenOrCreate, FileAccess.Write);
            stream5.Seek(0, SeekOrigin.Begin);
            stream5.SetLength(0);
            stream5.Close();
            //--------------------------------------------------------------
            //添加新数据
            for (int i = 0; i < listCur.Count; i++)
            {
                WriteFile(listCur[i] + "?", CurPath);
            }
            for (int i = 0; i < listAll.Count; i++)
            {
                WriteFile(listAll[i].name + "?", AllPath);
            }
            for (int i = 0; i < listDone.Count; i++)
            {
                WriteFile(listDone[i] + "?", DonePath);
            }
            for (int i = 0; i < listLeft.Count; i++)
            {
                WriteFile(listLeft[i] + "?", LeftPath);
            }
            
            WriteFile(setting.index.ToString() + "?", IndexPath);
           
            

            MessageBox.Show("存档成功");
        }

        private void 读档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //listCur
            var fileCur = ReadFile(CurPath);
            if (fileCur != "")
            {
                flag = true;
                listCur.Clear();
                string[] filesCur;
                filesCur = fileCur.Trim().Replace("\r", "").Replace("\n", "").Split('?');
                //filesCur = Regex.Replace(fileCur, @"\s", "").Split('?');
                for (int i = 0; i < filesCur.Length - 1; i++)
                {
                    listCur.Add(filesCur[i]);
                   // Console.WriteLine(files[i]);
                }

            }
            //listAll
            var fileAll = ReadFile(AllPath);
            if (fileAll != "")
            {
                flag = true;
                listAll.Clear();
                string[] filesAll;
                filesAll = fileAll.Trim().Replace("\r", "").Replace("\n", "").Split('?');
                //filesAll = Regex.Replace(fileAll, @"\s", "").Split('?');
                for (int i = 0; i < filesAll.Length - 1; i++)
                {
                    ImageBean bean = new ImageBean();
                    int type = setting.getType();
                    bean.name = filesAll[i];
                    bean.type = type;

                    listAll.Add(bean);
                    // Console.WriteLine(files[i]);
                }
            }
            //listDone
            var fileDone = ReadFile(DonePath);
            if (fileDone != "")
            {
                flag = true;
                listDone.Clear();
                string[] filesDone;
                filesDone = fileDone.Trim().Replace("\r", "").Replace("\n", "").Split('?');
                //filesDone = Regex.Replace(fileDone, @"\s", "").Split('?');
                for (int i = 0; i < filesDone.Length - 1; i++)
                {
                    listDone.Add(filesDone[i]);
                    // Console.WriteLine(files[i]);
                }
                if (listDone.Count == 0)
                {
                    return;
                }
                updateTopShow();
            }
            //listLeft
            var fileLeft = ReadFile(LeftPath);
            if (fileLeft != "")
            {
                flag = true;
                listLeft.Clear();
                string[] filesLeft;
                filesLeft = fileLeft.Trim().Replace("\r","").Replace("\n", "").Split('?');
                //filesLeft = Regex.Replace(fileLeft, @"\s", "").Split('?');
                for (int i = 0; i < filesLeft.Length - 1; i++)
                {
                    listLeft.Add(filesLeft[i]);
                    //Console.WriteLine("left:" + listLeft[i]);
                }
                if (listLeft.Count == 0)
                {
                    return;
                }
                updateLeftShow();

            }
            //setting_index.txt
            var fileIndex = ReadFile(IndexPath);
            if (fileIndex != "")
            {
                flag = true;
                string[] filesIndex;
                filesIndex = fileIndex.Trim().Replace("\r", "").Replace("\n", "").Split('?');
                //filesLeft = Regex.Replace(fileLeft, @"\s", "").Split('?');
                for (int i = 0; i < filesIndex.Length - 1; i++)
                {
                    setting_index = int.Parse(filesIndex[i]);
                    //Console.WriteLine("index:" + setting_index);
                }

            }

            if (!flag)
            {
                MessageBox.Show("无存档记录，无法进行读档操作");
            }
            else
            {
                MessageBox.Show("读档完成");
            }
        }
        public void WriteFile(String file,string fileName)
        {
            //StreamWriter sw = File.CreateText(fileName);
            StreamWriter sw = File.AppendText(fileName);    //添加文本
            sw.WriteLine(file);                //写入一行文本
            sw.Flush();                    //清空
            sw.Close();                    //关闭
        }
        public String ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return "";
            }
            //StreamReader sr = File.OpenText(fileName);
            //String files = sr.ReadLine();    //读取一行数据
            //sr.Close();                //释放资源

            String files = File.ReadAllText(fileName);     //读取所有文本           
            return files;
        }


        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }

        

        public void HechengA1()
        {
            int up = setting.up;
            int down = setting.down;
            int left = setting.left;
            int right = setting.right;
            int v_middle = setting.v_middle;
            int h_middle = setting.h_middle;
            int text_photo = setting.text_photo;

            string Path1 = System.Environment.CurrentDirectory + "\\done";
            Image img1 = null;
            Bitmap bitMap = null;
            Graphics g1;

            var width = CC.A4CHANG_XS;
            var height = CC.A4GAO_XS;
            // 初始化画布(最终的拼图画布)并设置宽高
            try
            {
                bitMap = new Bitmap(width, height);
            }
            catch (Exception)
            {
            }
            // 初始化画板
            g1 = Graphics.FromImage(bitMap);

            var eps = new EncoderParameters(1);
            var ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            eps.Param[0] = ep;

            //foreach (var item in listDrag)
            for (int i = 0; i < listDrag.Count; i++)
            {
                String item = listDrag[i];
                //String item_o = imgpath[i];
                String item_o = listDone[cur_pos];
                Console.WriteLine(i++);

                img1 = Image.FromFile(item);
                // 将画布涂为白色(底部颜色可自行设置);
                g1.FillRectangle(Brushes.White, 0, 0, width, height);

                float width_xs = (CC.A4CHANG - left - right) * CC.A4GBILV;
                float height_xs = (CC.A4GAO - up - down) * CC.A4GBILV;

                float xs = 0.0f;
                float x_xs = width_xs / img1.Width;
                float y_xs = height_xs / img1.Height;
                //1
                if (x_xs < y_xs)
                {
                    xs = x_xs;
                    g1.DrawImage(img1,
                            left * CC.A4GBILV,
                            up * CC.A4GBILV + (height_xs - img1.Height * xs) / 2,
                            width_xs,
                            img1.Height * xs);
                }
                else
                {
                    xs = y_xs;
                    g1.DrawImage(img1,
                            left * CC.A4GBILV + (width_xs - img1.Width * xs) / 2,
                            up * CC.A4GBILV,
                            img1.Width * xs,
                            height_xs);
                }

                char[] MyChar = { 'd', 'o', 'n', 'e' };
                //!!! 改 item_o -> item
                var vv = System.IO.Path.GetFileNameWithoutExtension(item);
                var vv_o = System.IO.Path.GetFileNameWithoutExtension(item_o).TrimEnd(MyChar);

                //var vv = System.IO.Path.GetFileNameWithoutExtension(item_o);
                var vv2 = System.IO.Path.GetExtension(item);
                vv2 = ".jpg";

                Bitmap im = bitMap;
                im.SetResolution(300, 300);
                //转成jpg

                /*
                var eps = new EncoderParameters(1);
                var ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                eps.Param[0] = ep;
                */

                var jpsEncodeer = GetEncoder(ImageFormat.Jpeg);
                //保存图片

                //更换的是第一张图片 drag_area=1
                //保存图片 删除图片
                String imgurl_o = Path1 + "\\" + vv_o + "done" + vv2;

                String imgurl = Path1 + "\\" + vv + "done" + vv2;
                im.Save(imgurl, jpsEncodeer, eps);
                listDone[cur_pos] = imgurl;

                //DeleteDir(imgurl_o);
                File.Delete(imgurl_o);


                //String imgurl = Path1 + "\\" + vv  + vv2;
                //im.Save(imgurl, jpsEncodeer, eps);

                //---------------
                //listCur[cur_pos + drag_area - 1] = path_url;
                //listAll[cur_pos + drag_area - 1].name = path_url;
                listChange[drag_area - 1] = path_url;
                imgpath[drag_area - 1] = path_url;
                listDrag[drag_area - 1] = path_url;
                //---------------

                //释放资源
                //im.Dispose();
                //ep.Dispose();
                //eps.Dispose();

                img1.Dispose();

                //bitMap.Dispose();

                //Image img = bitMap;
                ////保存
                //var vv = System.IO.Path.GetFileNameWithoutExtension(item);
                //var vv2 = System.IO.Path.GetExtension(item);
                //vv2 = ".png";

                //img.Save(Path1 + "\\" + vv + "done" + vv2);
            }
            ep.Dispose();
            eps.Dispose();

            bitMap.Dispose();
        }
        public void HechengB1()
        {
            int up = setting.up;
            int down = setting.down;
            int left = setting.left;
            int right = setting.right;
            int v_middle = setting.v_middle;
            int h_middle = setting.h_middle;
            int text_photo = setting.text_photo;

            string Path1 = System.Environment.CurrentDirectory + "\\done";

            Image img1 = null;
            Image img2 = null;

            Graphics g1;
            var width = CC.A4CHANG_XS;
            var height = CC.A4GAO_XS;
            // 初始化画布(最终的拼图画布)并设置宽高

            Bitmap bitMap = null;
            try
            {
                bitMap = new Bitmap(width, height);
            }
            catch (Exception)
            {
            }
            // 初始化画板
            g1 = Graphics.FromImage(bitMap);

            var eps = new EncoderParameters(1);
            var ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            eps.Param[0] = ep;
            var jpsEncodeer = GetEncoder(ImageFormat.Jpeg);

            for (int i = 0; i < listDrag.Count; i++)
            {
                String item = listDrag[i];
                //String item_o = imgpath[drag_area-1];
                String item_o = listDone[cur_pos];

                img1 = Image.FromFile(listDrag[i]);
                img2 = null;
                if (i + 1 < listDrag.Count)
                {
                    img2 = Image.FromFile(listDrag[i + 1]);


                }
                else
                {
                    //break;
                }
                // 将画布涂为白色(底部颜色可自行设置)
                g1.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));

                float width_xs = (CC.A4CHANG - left - right) * CC.A4GBILV;
                float height_xs = (CC.A4GAO / 2 - up - v_middle / 2) * CC.A4GBILV;

                float xs = 0.0f;
                float x_xs = width_xs / img1.Width;
                float y_xs = height_xs / img1.Height;

                float top_half = 0.0f;

                if (x_xs < y_xs)
                {
                    xs = x_xs;
                    g1.DrawImage(img1,
                            left * CC.A4GBILV,
                            up * CC.A4GBILV + height_xs - img1.Height * xs,
                            width_xs,
                            img1.Height * xs);

                    top_half = up * CC.A4GBILV + height_xs - img1.Height * xs + img1.Height * xs;
                }
                else
                {
                    xs = y_xs;
                    g1.DrawImage(img1,
                            left * CC.A4GBILV + (width_xs - img1.Width * xs) / 2,
                            up * CC.A4GBILV,
                            img1.Width * xs,
                            height_xs);

                    top_half = up * CC.A4GBILV + height_xs;
                }

                //2
                if (img2 != null)
                {
                    float width_xs2 = (CC.A4CHANG - left - right) * CC.A4GBILV;
                    float height_xs2 = (CC.A4GAO / 2 - down - v_middle / 2) * CC.A4GBILV;

                    float xs2 = 0.0f;
                    float x_xs2 = width_xs2 / img2.Width;
                    float y_xs2 = height_xs2 / img2.Height;

                    if (x_xs2 < y_xs2)
                    {
                        xs2 = x_xs2;
                        g1.DrawImage(img2,
                                left * CC.A4GBILV,
                                //CC.A4GAO_XS / 2 + v_middle / 2 * CC.A4GBILV + height_xs2 - img2.Height * xs2,
                                top_half + v_middle * CC.A4GBILV,
                                width_xs2,
                                img2.Height * xs2);
                    }
                    else
                    {
                        xs2 = y_xs2;
                        g1.DrawImage(img2,
                                left * CC.A4GBILV + (width_xs2 - img2.Width * xs2) / 2,
                                //CC.A4GAO_XS / 2 + v_middle / 2 * CC.A4GBILV,
                                top_half + v_middle * CC.A4GBILV,
                                img2.Width * xs2,
                                height_xs2);
                    }
                }
                char[] MyChar = { 'd', 'o', 'n', 'e' };
                //!!! 改 item_o -> item
                var vv = System.IO.Path.GetFileNameWithoutExtension(item);
                var vv_o = System.IO.Path.GetFileNameWithoutExtension(item_o).TrimEnd(MyChar);

                //var vv2 = System.IO.Path.GetExtension(imgpath1);
                var vv2 = ".jpg";

                Bitmap im = bitMap;
                im.SetResolution(300, 300);
                //转成jpg

                if (vv == vv_o)
                {
                    //更换的不是第一张图片
                    //保存图片
                    String imgurl = Path1 + "\\" + vv + "done" + vv2;
                    im.Save(imgurl, jpsEncodeer, eps);
                }
                else
                {
                    //更换的是第一张图片 drag_area=1
                    //保存图片 删除图片
                    String imgurl_o = Path1 + "\\" + vv_o + "done" + vv2;
                   
                    String imgurl = Path1 + "\\" + vv + "done" + vv2;
                    im.Save(imgurl, jpsEncodeer, eps);
                    listDone[cur_pos] = imgurl;

                    //DeleteDir(imgurl_o);
                    File.Delete(imgurl_o);
                }

                //保存图片
                //String imgurl = Path1 + "\\" + vv + "done" + vv2;
                //im.Save(imgurl, jpsEncodeer, eps);

                //---------------
                //listCur[cur_pos + drag_area - 1] = path_url;
                //listAll[cur_pos + drag_area - 1].name = path_url;
                listChange[drag_area - 1] = path_url;
                imgpath[drag_area - 1] = path_url;
                listDrag[drag_area - 1] = path_url;
                //---------------

                //释放资源
                img1.Dispose();
                if (img2 != null)
                {
                    img2.Dispose();
                }
                i++;
            }

            ep.Dispose();
            eps.Dispose();

            bitMap.Dispose();
        }
        public void HechengC1()
        {
            int up = setting.up;
            int down = setting.down;
            int left = setting.left;
            int right = setting.right;
            int v_middle = setting.v_middle;
            int h_middle = setting.h_middle;
            int text_photo = setting.text_photo;

            string Path1 = System.Environment.CurrentDirectory + "\\done";
            Image img1 = null;
            Image img2 = null;
            Image img3 = null;
            Image img4 = null;

            Graphics g1;
            var width = CC.A4CHANG_XS;
            var height = CC.A4GAO_XS;
            // 初始化画布(最终的拼图画布)并设置宽高

            Bitmap bitMap = null;
            try
            {
                bitMap = new Bitmap(width, height);
            }
            catch (Exception)
            {
            }
            // 初始化画板
            g1 = Graphics.FromImage(bitMap);

            var eps = new EncoderParameters(1);
            var ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            eps.Param[0] = ep;
            var jpsEncodeer = GetEncoder(ImageFormat.Jpeg);

            for (int i = 0; i < listDrag.Count; i++)
            {
                String item = listDrag[i];
                //String item_o = imgpath[drag_area-1];
                String item_o = listDone[cur_pos];

                img1 = Image.FromFile(listDrag[i]);
                img2 = null;
                img3 = null;
                img4 = null;
                if (i + 1 < listDrag.Count)
                {
                    img2 = Image.FromFile(listDrag[i + 1]);
                }
                else
                {
                    //break;
                }
                if (i + 2 < listDrag.Count)
                {
                    img3 = Image.FromFile(listDrag[i + 2]);
                }
                else
                {
                    //break;
                }
                if (i + 3 < listDrag.Count)
                {
                    img4 = Image.FromFile(listDrag[i + 3]);
                }
                else
                {
                    //break;
                }
                // 将画布涂为白色(底部颜色可自行设置)
                g1.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));

                float width_xs = ((CC.A4CHANG - left - right - h_middle) / 2) * CC.A4GBILV;
                float height_xs = ((CC.A4GAO - up - v_middle - down) / 2) * CC.A4GBILV;

                float xs = 0.0f;
                float x_xs = width_xs / img1.Width;
                float y_xs = height_xs / img1.Height;

                float top_1 = 0.0f;
                float top_2 = 0.0f;
                float top_3 = 0.0f;
                float top_4 = 0.0f;

                if (x_xs < y_xs)
                {
                    xs = x_xs;
                    g1.DrawImage(img1,
                            left * CC.A4GBILV,
                            up * CC.A4GBILV + height_xs - img1.Height * xs,
                            width_xs,
                            img1.Height * xs);

                    top_1 = up * CC.A4GBILV + height_xs - img1.Height * xs + img1.Height * xs;
                }
                else
                {
                    xs = y_xs;
                    g1.DrawImage(img1,
                            left * CC.A4GBILV + width_xs - img1.Width * xs,
                            up * CC.A4GBILV,
                            img1.Width * xs,
                            height_xs);

                    top_1 = up * CC.A4GBILV + height_xs;
                }

                //2
                if (img2 != null)
                {
                    float width_xs2 = width_xs;
                    float height_xs2 = height_xs;

                    float xs2 = 0.0f;
                    float x_xs2 = width_xs2 / img2.Width;
                    float y_xs2 = height_xs2 / img2.Height;

                    //top_2 = top_1 + v_middle * CC.A4GBILV;
                    //top_2 = top_1 ;

                    if (x_xs2 < y_xs2)
                    {
                        xs2 = x_xs2;
                        g1.DrawImage(img2,
                                left * CC.A4GBILV + width_xs2 + h_middle * CC.A4GBILV,
                                //CC.A4GAO_XS / 2 + v_middle / 2 * CC.A4GBILV + height_xs2 - img2.Height * xs2,
                                up * CC.A4GBILV + height_xs - img2.Height * xs2,
                                width_xs2,
                                img2.Height * xs2);
                    }
                    else
                    {
                        xs2 = y_xs2;
                        g1.DrawImage(img2,
                                left * CC.A4GBILV + width_xs2 + h_middle * CC.A4GBILV,
                                //CC.A4GAO_XS / 2 + v_middle / 2 * CC.A4GBILV,
                                up * CC.A4GBILV,
                                img2.Width * xs2,
                                height_xs2);
                    }
                    //top_2 = top_2 + height_xs2;
                    top_2 = top_1;
                }
                //3
                if (img3 != null)
                {


                    float xs3 = 0.0f;
                    float x_xs3 = width_xs / img3.Width;
                    float y_xs3 = height_xs / img3.Height;

                    top_3 = top_1 + v_middle * CC.A4GBILV;

                    if (x_xs3 < y_xs3)
                    {
                        xs3 = x_xs3;
                        g1.DrawImage(img3,
                                left * CC.A4GBILV,
                                //CC.A4GAO_XS / 2 + v_middle / 2 * CC.A4GBILV + height_xs2 - img2.Height * xs2,
                                top_3,
                                width_xs,
                                img3.Height * xs3);
                    }
                    else
                    {
                        xs3 = y_xs3;
                        g1.DrawImage(img3,
                                left * CC.A4GBILV + width_xs - img3.Width * xs3,
                                //CC.A4GAO_XS / 2 + v_middle / 2 * CC.A4GBILV,
                                top_3,
                                img3.Width * xs3,
                                height_xs);
                    }
                    // top_3 = top_3 + height_xs;
                }
                //4
                if (img4 != null)
                {

                    float xs4 = 0.0f;
                    float x_xs4 = width_xs / img4.Width;
                    float y_xs4 = height_xs / img4.Height;

                    //top_4 = top_3 + v_middle * CC.A4GBILV;

                    if (x_xs4 < y_xs4)
                    {
                        xs4 = x_xs4;
                        g1.DrawImage(img4,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                //CC.A4GAO_XS / 2 + v_middle / 2 * CC.A4GBILV + height_xs2 - img2.Height * xs2,
                                top_3,
                                width_xs,
                                img4.Height * xs4);
                    }
                    else
                    {
                        xs4 = y_xs4;
                        g1.DrawImage(img4,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                //CC.A4GAO_XS / 2 + v_middle / 2 * CC.A4GBILV + height_xs2 - img2.Height * xs2,
                                top_3,
                                img4.Width * xs4,
                                height_xs);
                    }

                }
                //!!!
                char[] MyChar = { 'd', 'o', 'n', 'e' };
                //!!! 改 item_o -> item
                var vv = System.IO.Path.GetFileNameWithoutExtension(item);
                var vv_o = System.IO.Path.GetFileNameWithoutExtension(item_o).TrimEnd(MyChar);
                
                var vv2 = System.IO.Path.GetExtension(item);
                vv2 = ".jpg";



                Bitmap im = bitMap;
                im.SetResolution(300, 300);
                //转成jpg

                if (vv == vv_o)
                {
                    //更换的不是第一张图片
                    //保存图片
                    String imgurl = Path1 + "\\" + vv + "done" + vv2;
                    im.Save(imgurl, jpsEncodeer, eps);
                }
                else
                {
                    //更换的是第一张图片 drag_area=1
                    //保存图片 删除图片
                    String imgurl_o = Path1 + "\\" + vv_o + "done" + vv2;

                    String imgurl = Path1 + "\\" + vv + "done" + vv2;
                    im.Save(imgurl, jpsEncodeer, eps);
                    listDone[cur_pos] = imgurl;

                    //DeleteDir(imgurl_o);
                    File.Delete(imgurl_o);
                }

                //保存图片
                //String imgurl = Path1 + "\\" + vv + "done" + vv2;
                //im.Save(imgurl, jpsEncodeer, eps);
                //---------------
                //listCur[cur_pos+drag_area-1] = path_url;
                //listAll[cur_pos + drag_area - 1].name = path_url;
                listChange[drag_area - 1] = path_url;
                imgpath[drag_area - 1] = path_url;
                listDrag[drag_area - 1] = path_url;
                //---------------

                //释放资源
                img1.Dispose();
                if (img2 != null)
                {
                    img2.Dispose();
                    i++;
                }

                if (img3 != null)
                {
                    img3.Dispose();
                    i++;
                }

                if (img4 != null)
                {
                    img4.Dispose();
                    i++;
                }


            }

            ep.Dispose();
            eps.Dispose();

            bitMap.Dispose();
        }
        public void HechengD1()
        {
            int up = setting.up;
            int down = setting.down;
            int left = setting.left;
            int right = setting.right;
            int v_middle = setting.v_middle;
            int h_middle = setting.h_middle;
            int text_photo = setting.text_photo;

            string Path1 = System.Environment.CurrentDirectory + "\\done";
            Image img1 = null;
            Image img2 = null;
            Image img3 = null;
            Image img4 = null;
            Image img5 = null;
            Image img6 = null;
            Image img7 = null;
            Image img8 = null;

            Graphics g1;
            var width = CC.A4CHANG_XS;
            var height = CC.A4GAO_XS;
            // 初始化画布(最终的拼图画布)并设置宽高

            Bitmap bitMap = null;
            try
            {
                bitMap = new Bitmap(width, height);
            }
            catch (Exception)
            {
            }
            // 初始化画板
            g1 = Graphics.FromImage(bitMap);

            var eps = new EncoderParameters(1);
            var ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            eps.Param[0] = ep;
            var jpsEncodeer = GetEncoder(ImageFormat.Jpeg);

            for (int i = 0; i < listDrag.Count; i++)
            {
                String item = listDrag[i];
                // String item_o = imgpath[drag_area-1];
                String item_o = listDone[cur_pos];

                img1 = Image.FromFile(listDrag[i]);
                img2 = null;
                img3 = null;
                img4 = null;
                img5 = null;
                img6 = null;
                img7 = null;
                img8 = null;
                if (i + 1 < listDrag.Count)
                {
                    img2 = Image.FromFile(listDrag[i + 1]);
                }
                else
                {
                    //break;
                }
                if (i + 2 < listDrag.Count)
                {
                    img3 = Image.FromFile(listDrag[i + 2]);
                }
                else
                {
                    //break;
                }
                if (i + 3 < listDrag.Count)
                {
                    img4 = Image.FromFile(listDrag[i + 3]);
                }
                else
                {
                    //break;
                }
                if (i + 4 < listDrag.Count)
                {
                    img5 = Image.FromFile(listDrag[i + 4]);
                }
                else
                {
                    //break;
                }
                if (i + 5 < listDrag.Count)
                {
                    img6 = Image.FromFile(listDrag[i + 5]);
                }
                else
                {
                    //break;
                }
                if (i + 6 < listDrag.Count)
                {
                    img7 = Image.FromFile(listDrag[i + 6]);
                }
                else
                {
                    //break;
                }
                if (i + 7 < listDrag.Count)
                {
                    img8 = Image.FromFile(listDrag[i + 7]);
                }
                else
                {
                    //break;
                }
                // 将画布涂为白色(底部颜色可自行设置)
                g1.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));

                float width_xs = ((CC.A4CHANG - left - right - h_middle) / 2) * CC.A4GBILV;
                float height_xs = ((CC.A4GAO - up - 3 * v_middle - down) / 4) * CC.A4GBILV;

                float xs = 0.0f;
                float x_xs = width_xs / img1.Width;
                float y_xs = height_xs / img1.Height;

                float top_1 = 0.0f;
                //float top_2 = 0.0f;
                float top_3 = 0.0f;
                //float top_4 = 0.0f;
                float top_5 = 0.0f;
                //float top_6 = 0.0f;
                float top_7 = 0.0f;
                //float top_8 = 0.0f;

                //1
                if (x_xs < y_xs)
                {
                    xs = x_xs;
                    g1.DrawImage(img1,
                            left * CC.A4GBILV,
                            up * CC.A4GBILV + height_xs - img1.Height * xs,
                            width_xs,
                            img1.Height * xs);

                    top_1 = up * CC.A4GBILV;
                }
                else
                {
                    xs = y_xs;
                    g1.DrawImage(img1,
                            left * CC.A4GBILV + width_xs - img1.Width * xs,
                            up * CC.A4GBILV,
                            img1.Width * xs,
                            height_xs);

                    //top_1 = up * CC.A4GBILV + height_xs;
                }

                //2
                if (img2 != null)
                {
                    float width_xs2 = width_xs;
                    float height_xs2 = height_xs;

                    float xs2 = 0.0f;
                    float x_xs2 = width_xs2 / img2.Width;
                    float y_xs2 = height_xs2 / img2.Height;

                    //top_2 = top_1 + v_middle * CC.A4GBILV;

                    if (x_xs2 < y_xs2)
                    {
                        xs2 = x_xs2;
                        g1.DrawImage(img2,
                                left * CC.A4GBILV + width_xs2 + h_middle * CC.A4GBILV,
                                top_1 + height_xs2 - img2.Height * xs2,
                                width_xs2,
                                img2.Height * xs2);
                    }
                    else
                    {
                        xs2 = y_xs2;
                        g1.DrawImage(img2,
                                left * CC.A4GBILV + width_xs2 + h_middle * CC.A4GBILV,
                                top_1,
                                img2.Width * xs2,
                                height_xs2);
                    }
                    //top_3 = top_1 + height_xs2 + v_middle * CC.A4GBILV;
                }
                //3
                if (img3 != null)
                {
                    float xs3 = 0.0f;
                    float x_xs3 = width_xs / img3.Width;
                    float y_xs3 = height_xs / img3.Height;

                    top_3 = top_1 + height_xs + v_middle * CC.A4GBILV;

                    if (x_xs3 < y_xs3)
                    {
                        xs3 = x_xs3;
                        g1.DrawImage(img3,
                                left * CC.A4GBILV,
                                top_3 + height_xs - img3.Height * xs3,
                                width_xs,
                                img3.Height * xs3);
                    }
                    else
                    {
                        xs3 = y_xs3;
                        g1.DrawImage(img3,
                                left * CC.A4GBILV + width_xs - img3.Width * xs3,
                                top_3,
                                img3.Width * xs3,
                                height_xs);
                    }
                    // top_3 = top_3 + height_xs;
                }
                //4
                if (img4 != null)
                {
                    float xs4 = 0.0f;
                    float x_xs4 = width_xs / img4.Width;
                    float y_xs4 = height_xs / img4.Height;

                    //top_4 = top_3 + v_middle * CC.A4GBILV;

                    if (x_xs4 < y_xs4)
                    {
                        xs4 = x_xs4;
                        g1.DrawImage(img4,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_3 + height_xs - img4.Height * xs4,
                                width_xs,
                                img4.Height * xs4);
                    }
                    else
                    {
                        xs4 = y_xs4;
                        g1.DrawImage(img4,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_3,
                                img4.Width * xs4,
                                height_xs);
                    }
                    //top_5 = top_3 + height_xs + v_middle * CC.A4GBILV;
                }
                //5
                if (img5 != null)
                {
                    float xs5 = 0.0f;
                    float x_xs5 = width_xs / img5.Width;
                    float y_xs5 = height_xs / img5.Height;

                    top_5 = top_3 + height_xs + v_middle * CC.A4GBILV;

                    if (x_xs5 < y_xs5)
                    {
                        xs5 = x_xs5;
                        g1.DrawImage(img5,
                                left * CC.A4GBILV,
                                top_5,
                                width_xs,
                                img5.Height * xs5);
                    }
                    else
                    {
                        xs5 = y_xs5;
                        g1.DrawImage(img5,
                                left * CC.A4GBILV + width_xs - img5.Width * xs5,
                                top_5,
                                img5.Width * xs5,
                                height_xs);
                    }
                    //top_5 = top_5 + height_xs;
                }
                //6
                if (img6 != null)
                {
                    float xs6 = 0.0f;
                    float x_xs6 = width_xs / img6.Width;
                    float y_xs6 = height_xs / img6.Height;

                    //top_6 = top_5 + v_middle * CC.A4GBILV;

                    if (x_xs6 < y_xs6)
                    {
                        xs6 = x_xs6;
                        g1.DrawImage(img6,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_5,
                                width_xs,
                                img6.Height * xs6);
                    }
                    else
                    {
                        xs6 = y_xs6;
                        g1.DrawImage(img6,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_5,
                                img6.Width * xs6,
                                height_xs);
                    }
                    top_7 = top_5 + height_xs + v_middle * CC.A4GBILV;
                }
                //7
                if (img7 != null)
                {

                    float xs7 = 0.0f;
                    float x_xs7 = width_xs / img7.Width;
                    float y_xs7 = height_xs / img7.Height;

                    //top_7 = top_6 + v_middle * CC.A4GBILV;

                    if (x_xs7 < y_xs7)
                    {
                        xs7 = x_xs7;
                        g1.DrawImage(img7,
                                left * CC.A4GBILV,
                                top_7,
                                width_xs,
                                img7.Height * xs7);
                    }
                    else
                    {
                        xs7 = y_xs7;
                        g1.DrawImage(img7,
                                left * CC.A4GBILV + width_xs - img7.Width * xs7,
                                top_7,
                                img7.Width * xs7,
                                height_xs);
                    }
                    //top_7 = top_7 + height_xs;
                }
                //8
                if (img8 != null)
                {
                    float xs8 = 0.0f;
                    float x_xs8 = width_xs / img8.Width;
                    float y_xs8 = height_xs / img8.Height;

                    //top_8 = top_7 + v_middle * CC.A4GBILV;

                    if (x_xs8 < y_xs8)
                    {
                        xs8 = x_xs8;
                        g1.DrawImage(img8,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_7,
                                width_xs,
                                img8.Height * xs8);
                    }
                    else
                    {
                        xs8 = y_xs8;
                        g1.DrawImage(img8,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_7,
                                img8.Width * xs8,
                                height_xs);
                    }
                    //top_8 = top_8 + height_xs;
                }
                //!!!

                char[] MyChar = { 'd', 'o', 'n', 'e' };
                //!!! 改 item_o -> item
                var vv = System.IO.Path.GetFileNameWithoutExtension(item);
                var vv_o = System.IO.Path.GetFileNameWithoutExtension(item_o).TrimEnd(MyChar);
                var vv2 = System.IO.Path.GetExtension(item);
                vv2 = ".jpg";

                Bitmap im = bitMap;
                im.SetResolution(300, 300);
                //转成jpg

                if (vv == vv_o)
                {
                    //更换的不是第一张图片
                    //保存图片
                    String imgurl = Path1 + "\\" + vv + "done" + vv2;
                    im.Save(imgurl, jpsEncodeer, eps);
                }
                else
                {
                    //更换的是第一张图片 drag_area=1
                    //保存图片 删除图片
                    String imgurl_o = Path1 + "\\" + vv_o + "done" + vv2;

                    String imgurl = Path1 + "\\" + vv + "done" + vv2;
                    im.Save(imgurl, jpsEncodeer, eps);
                    listDone[cur_pos] = imgurl;

                    //DeleteDir(imgurl_o);
                    File.Delete(imgurl_o);
                }


                //保存图片
                //String imgurl = Path1 + "\\" + vv + "done" + vv2;
                //im.Save(imgurl, jpsEncodeer, eps);

                //---------------
                //listCur[cur_pos+drag_area-1] = path_url;
                //listAll[cur_pos + drag_area - 1].name = path_url;
                listChange[drag_area - 1] = path_url;
                imgpath[drag_area - 1] = path_url;
                listDrag[drag_area - 1] = path_url;
                //---------------

                //释放资源
                img1.Dispose();
                if (img2 != null)
                {
                    img2.Dispose();
                    i++;
                }

                if (img3 != null)
                {
                    img3.Dispose();
                    i++;
                }

                if (img4 != null)
                {
                    img4.Dispose();
                    i++;
                }
                if (img5 != null)
                {
                    img5.Dispose();
                    i++;
                }
                if (img6 != null)
                {
                    img6.Dispose();
                    i++;
                }
                if (img7 != null)
                {
                    img7.Dispose();
                    i++;
                }
                if (img8 != null)
                {
                    img8.Dispose();
                    i++;
                }


            }

            ep.Dispose();
            eps.Dispose();

            bitMap.Dispose();
        }

        public static void DeleteDir(string srcPath)
        {

            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
            }

        }

    }
}
