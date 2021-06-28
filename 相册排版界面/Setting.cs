using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 相册排版界面
{
    public partial class Setting : Form
    {
        public int up = 0;
        public int down = 0;
        public int left = 0;
        public int right = 0;
        public int v_middle = 0;
        public int h_middle = 0;
        public int text_photo = 0;

        bool checkFlag = false;
        FirstDlg modelDlg = new FirstDlg();
        public int index = 1;

        //string Path2 = "";
        public string Path1 = "";

        List<string> list;
        int indexDeal;

        int result = 0;
        bool bClearDone = false;
        public Layout layout;
        public SingleDeal singleDeal;

        private string fontName = "宋体";
        private int fontSize = 10;
        private string content = "";
        private string fontColor = "Black";

        public int getResult()
        {
            return result;
        }

        public int getType()
        {
            return index;
        }

        public Setting(Layout layout)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.comboBox1.SelectedIndex = 0;
            Path1 = System.Environment.CurrentDirectory + "\\done";
            this.layout = layout;
        }

        public Setting()
        {
            InitializeComponent();
            
        }

        public void postData(List<string> list2, int index2, bool clearDone)
        {
            bClearDone = clearDone;
            result = 0;
            list = list2;
            indexDeal = index2;
        }
        
        public void DelectDir(string srcPath)
        {
            if (bClearDone == false)
            {
                return;
            }

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

        private void autoDeal()
        {
            if (!Directory.Exists(Path1))//如果不存在就创建 dir 文件夹  
                Directory.CreateDirectory(Path1);
            DelectDir(Path1);

            if (indexDeal == 101)
            {
                index = 1;
            }
            else if (indexDeal == 102)
            {
                index = 2;
            }
            else if (indexDeal == 103)
            {
                index = 3;
            }
            else if (indexDeal == 104)
            {
                index = 4;
            }
            else if (indexDeal == 105)
            {
                index = 5;
            }
            else if (indexDeal == 106)
            {
                index = 6;
            }
            else if (indexDeal == 107)
            {
                index = 7;
            }
            else if (indexDeal == 108)
            {
                index = 8;
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

            //System.Diagnostics.Process.Start("explorer.exe", Path1);
        }
        
        private void button2_Click(object sender, EventArgs e)//开始加工图片
        {
            if (!Directory.Exists(Path1))//如果不存在就创建 dir 文件夹  
                Directory.CreateDirectory(Path1);
            DelectDir(Path1);

            //添加文字描述：TextDescription
            if (checkBox1.Checked)
            {
                checkFlag = true;
            }

            if (comboBox1.SelectedIndex == 0)
            {
                index = 1;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                index = 2;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                index = 3;
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                index = 4;
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                index = 5;
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                index = 6;
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                index = 7;
            }
            else if (comboBox1.SelectedIndex == 7)
            {
                index = 8;
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
            
            //System.Diagnostics.Process.Start("explorer.exe", Path1);
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // 这里是后台线程， 是在另一个线程上完成的
            // 这里是真正做事的工作线程
            // 可以在这里做一些费时的，复杂的操作
            BackgroundWorker bw = sender as BackgroundWorker;
            if (index == 1)
            {
                //单张
                HechengA1();
                //if (checkFlag)
                //{
                //    Layout layout = new Layout();
                //    layout.AddDescription(index);
                //}
            }
            else if (index == 2)
            {
                //单张转90
                HechengA2();
                //if (checkFlag)
                //{
                //    Layout layout = new Layout();
                //    layout.AddDescription(index);
                //}
            }
            else if (index == 3)
            {
                //两张
                HechengB1();
            }
            else if (index == 4)
            {
                //两张转90
                HechengB2();
            }
            else if (index == 5)
            {
                //四张
                HechengC1();
            }
            else if (index == 6)
            {
                //四张转90
                HechengC2();
            }
            else if (index == 7)
            {
                //八张
                HechengD1();
            }
            else if (index == 8)
            {
                //八张转90
                HechengD2();
            }

            // 勾选添加文字框并点击最后确认后，进行文字渲染 ：TextDescription
            //if (checkFlag)
            //{
            //    Layout layout = new Layout();
            //    layout.AddDescription(index);
            //}

            bw.ReportProgress(100);
        }

        void UpdateProgress(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            if (i == 100)
            {
                result = 1;
                modelDlg.Close();
                MessageBox.Show("图片处理已完成");
                this.Close();
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //这时后台线程已经完成，并返回了主线程，所以可以直接使用UI控件了 
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

        //照片的描述textbox绘制： layout加入文字ToolStripMenuItem_Click
        //public void AddDescription(int index)
        //{
        //    fontSelect.ShowDialog(this);

        //    string fontName = fontSelect.fontName;
        //    int fontSize = fontSelect.fontSize;
        //    string content = fontSelect.content;
        //    string location = fontSelect.location;

        //    Bitmap bmp = new Bitmap(listDone[cur_pos]);
        //    Graphics g = Graphics.FromImage(bmp);
        //    Font font = new Font(fontName, fontSize);
        //    SolidBrush sbrush = new SolidBrush(Color.Black);
        //    //g.DrawString(content, font, sbrush, new PointF(10, 10));

        //    if (location == "图片上方")
        //    {
        //        g.DrawString(content, font, sbrush, new PointF(810, 200));
        //    }
        //    else if (location == "图片中间")
        //    {
        //        g.DrawString(content, font, sbrush, new PointF(810, 1600));
        //    }
        //    if (location == "图片下方")
        //    {
        //        g.DrawString(content, font, sbrush, new PointF(810, 3100));
        //    }
        //    //MemoryStream ms = new MemoryStream();
        //    //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

        //    string name = listDone[cur_pos];
        //    name += fontIndex;

        //    bmp.Save(name);

        //    string path = name;
        //    listDone[cur_pos] = path;
        //    updateTopShow();
        //}

        public void HechengA1()
        {
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
            
            int i = 1;
            foreach (var item in list)
            {
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
                //加入文字绘制
                if (checkFlag)
                {
                    Font font = new Font(fontName, fontSize);
                    SolidBrush sbrush = new SolidBrush(Color.FromName(fontColor));
                    //留存字体绘制保存
                    float x;
                    x = 854 - content.Length * 8 / 2; //x=800;
                    g1.DrawString(content, font, sbrush, new PointF(x, height - down * CC.A4GBILV + text_photo * CC.A4GBILV));

                }


                var vv = System.IO.Path.GetFileNameWithoutExtension(item);
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
                String imgurl = Path1 + "\\" + vv + "done" + vv2;
                im.Save(imgurl, jpsEncodeer, eps);
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

        public void HechengA2()
        {
            Image img1 = null;
            //Bitmap map1 = null;
            //Image img2 = null;
            //Bitmap map2 = null;
            Bitmap bitMap = null;
            Graphics g1;

            var width = CC.A4CHANG_XS;
            var height = CC.A4GAO_XS;
            // 初始化画布(最终的拼图画布)并设置宽高
            bitMap = new Bitmap(width, height);
            // 初始化画板
            g1 = Graphics.FromImage(bitMap);

            //转成jpg
            var eps = new EncoderParameters(1);
            var ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            eps.Param[0] = ep;

            foreach (var item in list)
            {
                
                img1 = Image.FromFile(item);

                img1.RotateFlip(RotateFlipType.Rotate270FlipNone);
                //map1 = new Bitmap(img1);

                /*
                var width = CC.A4CHANG_XS;
                var height = CC.A4GAO_XS;
                // 初始化画布(最终的拼图画布)并设置宽高
                bitMap = new Bitmap(width, height);
                // 初始化画板
                g1 = Graphics.FromImage(bitMap);
                */

                // 将画布涂为白色(底部颜色可自行设置)
                g1.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));

                float width_xs = (CC.A4CHANG - left - right) * CC.A4GBILV;
                float height_xs = (CC.A4GAO - up - down) * CC.A4GBILV;

                float xs = 0.0f;
                float x_xs = width_xs / img1.Width;
                float y_xs = height_xs / img1.Height;

                if (x_xs < y_xs)
                {
                    xs = x_xs;
                    g1.DrawImage(img1, left * CC.A4GBILV, up * CC.A4GBILV + (height_xs - img1.Height * xs) / 2, width_xs, img1.Height * xs);
                }
                else
                {
                    xs = y_xs;
                    g1.DrawImage(img1, left * CC.A4GBILV + (width_xs - img1.Width * xs) / 2, up * CC.A4GBILV, img1.Width * xs, height_xs);
                }

                //加入文字绘制
                if (checkFlag)
                {
                    Font font = new Font(fontName, fontSize);
                    SolidBrush sbrush = new SolidBrush(Color.FromName(fontColor));
                    //留存字体绘制保存
                    float x;
                    x = 854 - content.Length * 8 / 2; //x=800;
                    g1.DrawString(content, font, sbrush, new PointF(x, height - down * CC.A4GBILV + text_photo * CC.A4GBILV));

                }

                var vv = System.IO.Path.GetFileNameWithoutExtension(item);
                var vv2 = System.IO.Path.GetExtension(item);
                vv2 = ".jpg";

                Bitmap im = bitMap;
                im.SetResolution(300, 300);
                /*
                //转成jpg
                var eps = new EncoderParameters(1);
                var ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                eps.Param[0] = ep;
                */
                var jpsEncodeer = GetEncoder(ImageFormat.Jpeg);
                //保存图片
                String imgurl = Path1 + "\\" + vv + "done" + vv2;
                im.Save(imgurl, jpsEncodeer, eps);

                img1.Dispose();

                //释放资源
                //im.Dispose();
                /*
                ep.Dispose();
                eps.Dispose();

                bitMap.Dispose();
                */

                //Image img = bitMap;
                ////保存  item
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

            for (int i = 0; i < list.Count; i++)
            {
                String item = list[i];

                img1 = Image.FromFile(list[i]);
                img2 = null;
                if (i + 1 < list.Count)
                {
                    img2 = Image.FromFile(list[i + 1]);
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

                //加入文字绘制
                if (checkFlag)
                {
                    Font font = new Font(fontName, fontSize);
                    SolidBrush sbrush = new SolidBrush(Color.FromName(fontColor));
                    //留存字体绘制保存
                    float x;
                    x = 854 - content.Length * 8 / 2; //x=800;
                    g1.DrawString(content, font, sbrush, new PointF(x, height - down * CC.A4GBILV + text_photo * CC.A4GBILV));

                }

                var vv = System.IO.Path.GetFileNameWithoutExtension(item);
                var vv2 = System.IO.Path.GetExtension(item);
                vv2 = ".jpg";

                Bitmap im = bitMap;
                im.SetResolution(300, 300);
                //转成jpg

                //保存图片
                String imgurl = Path1 + "\\" + vv + "done" + vv2;
                im.Save(imgurl, jpsEncodeer, eps);
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
		
        public void HechengB2()
        {
            Image img1 = null;
            Image img2 = null;
            Bitmap bitMap = null;
            Graphics g1;

            var eps = new EncoderParameters(1);
            var ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            eps.Param[0] = ep;
            var jpsEncodeer = GetEncoder(ImageFormat.Jpeg);

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

            for (int i = 0; i < list.Count; i++)
            {
                String item = list[i];

                img1 = Image.FromFile(list[i]);
                img1.RotateFlip(RotateFlipType.Rotate270FlipNone);
                img2 = null;

                if (i + 1 < list.Count)
                {
                    img2 = Image.FromFile(list[i + 1]);
                    img2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }

                // 将画布涂为白色(底部颜色可自行设置)
                g1.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));

                float top_half = 0.0f;

                if (img2 == null)
                {
                    float width_xs = (CC.A4CHANG - left - right) * CC.A4GBILV;
                    float height_xs = (CC.A4GAO / 2 - up - v_middle / 2) * CC.A4GBILV;

                    float xs = 0.0f;
                    float x_xs = width_xs / img1.Width;
                    float y_xs = height_xs / img1.Height;

                    if (x_xs < y_xs)
                    {
                        xs = x_xs;
                        g1.DrawImage(img1,
                                left * CC.A4GBILV,
                                CC.A4GAO_XS - down * CC.A4GBILV - img1.Height * xs,
                                width_xs,
                                img1.Height * xs);
                    }
                    else
                    {
                        xs = y_xs;
                        g1.DrawImage(img1,
                                left * CC.A4GBILV + (width_xs - img1.Width * xs) / 2,
                                CC.A4GAO_XS - down * CC.A4GBILV - height_xs,
                                img1.Width * xs,
                                height_xs);
                    }
                }
                else
                {
                    float width_xs = (CC.A4CHANG - left - right) * CC.A4GBILV;
                    float height_xs = (CC.A4GAO / 2 - up - v_middle / 2) * CC.A4GBILV;

                    float xs = 0.0f;
                    float x_xs = width_xs / img2.Width;
                    float y_xs = height_xs / img2.Height;

                    if (x_xs < y_xs)
                    {
                        xs = x_xs;
                        g1.DrawImage(img2,
                                left * CC.A4GBILV,
                                up * CC.A4GBILV + height_xs - img2.Height * xs,
                                width_xs,
                                img2.Height * xs);

                        top_half = up * CC.A4GBILV + height_xs - img2.Height * xs + img2.Height * xs;
                    }
                    else
                    {
                        xs = y_xs;
                        g1.DrawImage(img2,
                                left * CC.A4GBILV + (width_xs - img2.Width * xs) / 2,
                                up * CC.A4GBILV,
                                img2.Width * xs,
                                height_xs);

                        top_half = up * CC.A4GBILV + height_xs;
                    }
                }

                //2
                if (img2 != null)
                {
                    float width_xs2 = (CC.A4CHANG - left - right) * CC.A4GBILV;
                    float height_xs2 = (CC.A4GAO / 2 - down - v_middle / 2) * CC.A4GBILV;

                    float xs2 = 0.0f;
                    float x_xs2 = width_xs2 / img1.Width;
                    float y_xs2 = height_xs2 / img1.Height;

                    if (x_xs2 < y_xs2)
                    {
                        xs2 = x_xs2;
                        g1.DrawImage(img1,
                                left * CC.A4GBILV,
                                top_half + v_middle * CC.A4GBILV,
                                width_xs2,
                                img1.Height * xs2);
                    }
                    else
                    {
                        xs2 = y_xs2;
                        g1.DrawImage(img1,
                                left * CC.A4GBILV + (width_xs2 - img1.Width * xs2) / 2,
                                top_half + v_middle * CC.A4GBILV,
                                img1.Width * xs2,
                                height_xs2);
                    }
                }

                //加入文字绘制
                if (checkFlag)
                {
                    Font font = new Font(fontName, fontSize);
                    SolidBrush sbrush = new SolidBrush(Color.FromName(fontColor));
                    //留存字体绘制保存
                    float x;
                    x = 854 - content.Length * 8 / 2; //x=800;
                    g1.DrawString(content, font, sbrush, new PointF(x, height - down * CC.A4GBILV + text_photo * CC.A4GBILV));

                }

                var vv = System.IO.Path.GetFileNameWithoutExtension(item);
                var vv2 = System.IO.Path.GetExtension(item);
                vv2 = ".jpg";

                Bitmap im = bitMap;
                im.SetResolution(300, 300);
                //转成jpg

                //保存图片
                String imgurl = Path1 + "\\" + vv + "done" + vv2;
                im.Save(imgurl, jpsEncodeer, eps);
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

            for (int i = 0; i < list.Count; i++)
            {
                String item = list[i];

                img1 = Image.FromFile(list[i]);
                img2 = null;
                img3 = null;
                img4 = null;
                if (i + 1 < list.Count)
                {
                    img2 = Image.FromFile(list[i + 1]);
                }
                else
                {
                    //break;
                }
                if (i + 2 < list.Count)
                {
                    img3 = Image.FromFile(list[i + 2]);
                }
                else
                {
                    //break;
                }
                if (i + 3 < list.Count)
                {
                    img4 = Image.FromFile(list[i + 3]);
                }
                else
                {
                    //break;
                }
                // 将画布涂为白色(底部颜色可自行设置)
                g1.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));

                float width_xs = ((CC.A4CHANG - left - right- h_middle)/2) * CC.A4GBILV;
                float height_xs = ((CC.A4GAO - up -  v_middle - down) / 2) * CC.A4GBILV;

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

                //加入文字绘制
                if (checkFlag)
                {
                    Font font = new Font(fontName, fontSize);
                    SolidBrush sbrush = new SolidBrush(Color.FromName(fontColor));
                    //留存字体绘制保存
                    float x;
                    x = 854 - content.Length * 8 / 2; //x=800;
                    g1.DrawString(content, font, sbrush, new PointF(x, height - down * CC.A4GBILV + text_photo * CC.A4GBILV));

                }

                var vv = System.IO.Path.GetFileNameWithoutExtension(item);
                var vv2 = System.IO.Path.GetExtension(item);
                vv2 = ".jpg";

                Bitmap im = bitMap;
                im.SetResolution(300, 300);
                //转成jpg

                //保存图片
                String imgurl = Path1 + "\\" + vv + "done" + vv2;
                im.Save(imgurl, jpsEncodeer, eps);
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

        public void HechengC2()
        {
            //四拼一转90
            Image img1 = null;
            Image img2 = null;
            Image img3 = null;
            Image img4 = null;
            Bitmap bitMap = null;
            Graphics g1;

            var eps = new EncoderParameters(1);
            var ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            eps.Param[0] = ep;
            var jpsEncodeer = GetEncoder(ImageFormat.Jpeg);

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

            for (int i = 0; i < list.Count; i++)
            {
                String item = list[i];

                img1 = Image.FromFile(list[i]);
                img1.RotateFlip(RotateFlipType.Rotate270FlipNone);
                img2 = null;
                img3 = null;
                img4 = null;

                if (i + 1 < list.Count)
                {
                    img2 = Image.FromFile(list[i + 1]);
                    img2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                if (i + 2 < list.Count)
                {
                    img4 = Image.FromFile(list[i + 2]);
                    img4.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                if (i + 3 < list.Count)
                {
                    img3 = Image.FromFile(list[i + 3]);
                    img3.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }

                // 将画布涂为白色(底部颜色可自行设置)
                g1.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));

                float top_half1 = 0.0f;
                float top_half2 = 0.0f;


                float width_xs = ((CC.A4CHANG - left - right - h_middle)/2) * CC.A4GBILV;
                float height_xs = ((CC.A4GAO - up -  v_middle - down) / 2) * CC.A4GBILV;

                float xs = 0.0f;
                float x_xs = width_xs / img1.Width;
                float y_xs = height_xs / img1.Height;
                //1
                if (x_xs < y_xs)
                {
                    xs = x_xs;
                    g1.DrawImage(img1,
                            left * CC.A4GBILV,
                            CC.A4GAO_XS - down * CC.A4GBILV - height_xs ,
                            width_xs,
                            img1.Height * xs);

                }
                else
                {
                    xs = y_xs;
                    g1.DrawImage(img1,
                            left * CC.A4GBILV + width_xs - img1.Width * xs,
                            CC.A4GAO_XS - down * CC.A4GBILV - height_xs,
                            img1.Width * xs,
                            height_xs);

                }
                top_half1 = CC.A4GAO_XS - down * CC.A4GBILV - height_xs;
                //2
                if (img2 != null)
                {

                    xs = 0.0f;
                    x_xs = width_xs / img2.Width;
                    y_xs = height_xs / img2.Height;

                    top_half2 = top_half1 - height_xs - v_middle * CC.A4GBILV;
                   

                    if (x_xs < y_xs)
                    {
                        xs = x_xs;
                        g1.DrawImage(img2,
                                left * CC.A4GBILV,
                                top_half2 + height_xs - img2.Height * xs,
                                width_xs,
                                img2.Height * xs);

                        //top_half = top_half - height_xs - v_middle;
                    }
                    else
                    {
                        xs = y_xs;
                        g1.DrawImage(img2,
                                left * CC.A4GBILV + width_xs - img2.Width * xs,
                                top_half2,
                                img2.Width * xs,
                                height_xs);

                        //top_half = up * CC.A4GBILV + height_xs;
                    }

                }
                //3
                if (img3 != null)
                {
                    xs = 0.0f;
                    x_xs = width_xs / img3.Width;
                    y_xs = height_xs / img3.Height;

                    //top_half = top_half - height_xs - v_middle * CC.A4GBILV;

                    if (x_xs < y_xs)
                    {
                        xs = x_xs;
                        g1.DrawImage(img3,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_half2 + height_xs - img3.Height * xs,
                                width_xs,
                                img3.Height * xs);

                        //top_half = top_half - height_xs - v_middle;
                    }
                    else
                    {
                        xs = y_xs;
                        g1.DrawImage(img3,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_half2,
                                img3.Width * xs,
                                height_xs);

                        //top_half = up * CC.A4GBILV + height_xs;
                    }

                }
                if (img4 != null)
                {
                    xs = 0.0f;
                    x_xs = width_xs / img4.Width;
                    y_xs = height_xs / img4.Height;

                    //top_half = top_half + height_xs + v_middle * CC.A4GBILV;

                    if (x_xs < y_xs)
                    {
                        xs = x_xs;
                        g1.DrawImage(img4,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_half1 ,
                                width_xs,
                                img4.Height * xs);

                        //top_half = top_half - height_xs - v_middle;
                    }
                    else
                    {
                        xs = y_xs;
                        g1.DrawImage(img4,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_half1,
                                img4.Width * xs,
                                height_xs);

                        //top_half = up * CC.A4GBILV + height_xs;
                    }

                }

                //加入文字绘制
                if (checkFlag)
                {
                    Font font = new Font(fontName, fontSize);
                    SolidBrush sbrush = new SolidBrush(Color.FromName(fontColor));
                    //留存字体绘制保存
                    float x;
                    x = 854 - content.Length * 8 / 2; //x=800;
                    g1.DrawString(content, font, sbrush, new PointF(x, height - down * CC.A4GBILV + text_photo * CC.A4GBILV));

                }

                var vv = System.IO.Path.GetFileNameWithoutExtension(item);
                var vv2 = System.IO.Path.GetExtension(item);
                vv2 = ".jpg";

                Bitmap im = bitMap;
                im.SetResolution(300, 300);
                //转成jpg

                //保存图片
                String imgurl = Path1 + "\\" + vv + "done" + vv2;
                im.Save(imgurl, jpsEncodeer, eps);
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

            for (int i = 0; i < list.Count; i++)
            {
                String item = list[i];

                img1 = Image.FromFile(list[i]);
                img2 = null;
                img3 = null;
                img4 = null;
                img5 = null;
                img6 = null;
                img7 = null;
                img8 = null;
                if (i + 1 < list.Count)
                {
                    img2 = Image.FromFile(list[i + 1]);
                }
                else
                {
                    //break;
                }
                if (i + 2 < list.Count)
                {
                    img3 = Image.FromFile(list[i + 2]);
                }
                else
                {
                    //break;
                }
                if (i + 3 < list.Count)
                {
                    img4 = Image.FromFile(list[i + 3]);
                }
                else
                {
                    //break;
                }
                if (i + 4 < list.Count)
                {
                    img5 = Image.FromFile(list[i + 4]);
                }
                else
                {
                    //break;
                }
                if (i + 5 < list.Count)
                {
                    img6 = Image.FromFile(list[i + 5]);
                }
                else
                {
                    //break;
                }
                if (i + 6 < list.Count)
                {
                    img7 = Image.FromFile(list[i + 6]);
                }
                else
                {
                    //break;
                }
                if (i + 7 < list.Count)
                {
                    img8 = Image.FromFile(list[i + 7]);
                }
                else
                {
                    //break;
                }
                // 将画布涂为白色(底部颜色可自行设置)
                g1.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));

                float width_xs = ((CC.A4CHANG - left - right - h_middle)/2) * CC.A4GBILV;
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

                    top_1 = up * CC.A4GBILV ;
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

                    
                    top_1 = up * CC.A4GBILV;

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

                    
                    top_3 = top_1 + height_xs + v_middle * CC.A4GBILV;
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

                    top_5 = top_3 + height_xs + v_middle * CC.A4GBILV;

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
                    
                }
                //7
                if (img7 != null)
                {
                   
                    float xs7 = 0.0f;
                    float x_xs7 = width_xs / img7.Width;
                    float y_xs7 = height_xs / img7.Height;

                    top_7 = top_5 + height_xs + v_middle * CC.A4GBILV;

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
                    
                }
                //8
                if (img8 != null)
                {
                    float xs8 = 0.0f;
                    float x_xs8 = width_xs / img8.Width;
                    float y_xs8 = height_xs / img8.Height;

                    top_7 = top_5 + height_xs + v_middle * CC.A4GBILV;

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
                    
                }

                //加入文字绘制
                if (checkFlag)
                {
                    Font font = new Font(fontName, fontSize);
                    SolidBrush sbrush = new SolidBrush(Color.FromName(fontColor));
                    //留存字体绘制保存
                    float x;
                    x = 854 - content.Length * 8 / 2; //x=800;
                    g1.DrawString(content, font, sbrush, new PointF(x, height - down * CC.A4GBILV + text_photo * CC.A4GBILV));

                }

                var vv = System.IO.Path.GetFileNameWithoutExtension(item);
                var vv2 = System.IO.Path.GetExtension(item);
                vv2 = ".jpg";

                Bitmap im = bitMap;
                im.SetResolution(300, 300);
                //转成jpg

                //保存图片
                String imgurl = Path1 + "\\" + vv + "done" + vv2;
                im.Save(imgurl, jpsEncodeer, eps);
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

        public void HechengD2()
        {
            Image img1 = null;
            Image img2 = null;
            Image img3 = null;
            Image img4 = null;
            Image img5 = null;
            Image img6 = null;
            Image img7 = null;
            Image img8 = null;
            Bitmap bitMap = null;
            Graphics g1;

            var eps = new EncoderParameters(1);
            var ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            eps.Param[0] = ep;
            var jpsEncodeer = GetEncoder(ImageFormat.Jpeg);

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

            for (int i = 0; i < list.Count; i++)
            {
                String item = list[i];

                img1 = Image.FromFile(list[i]);
                img1.RotateFlip(RotateFlipType.Rotate270FlipNone);
                img2 = null;
                img3 = null;
                img4 = null;
                img5 = null;
                img6 = null;
                img7 = null;
                img8 = null;

                if (i + 1 < list.Count)
                {
                    img2 = Image.FromFile(list[i + 1]);
                    img2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                if (i + 2 < list.Count)
                {
                    img3 = Image.FromFile(list[i + 2]);
                    img3.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                if (i + 3 < list.Count)
                {
                    img4 = Image.FromFile(list[i + 3]);
                    img4.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                if (i + 4 < list.Count)
                {
                    img8 = Image.FromFile(list[i + 4]);
                    img8.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                if (i + 5 < list.Count)
                {
                    img7 = Image.FromFile(list[i + 5]);
                    img7.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                if (i + 6 < list.Count)
                {
                    img6 = Image.FromFile(list[i + 6]);
                    img6.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                if (i + 7 < list.Count)
                {
                    img5 = Image.FromFile(list[i + 7]);
                    img5.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }

                // 将画布涂为白色(底部颜色可自行设置)
                g1.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));

                float top_half1 = 0.0f;
                float top_half2 = 0.0f;
                float top_half3 = 0.0f;
                float top_half4 = 0.0f;

                //1
                float width_xs = ((CC.A4CHANG - left - right - h_middle) / 2) * CC.A4GBILV;
                float height_xs = ((CC.A4GAO - up - 3 * v_middle - down) / 4) * CC.A4GBILV;

                float xs = 0.0f;
                float x_xs = width_xs / img1.Width;
                float y_xs = height_xs / img1.Height;

                //1
                if (x_xs < y_xs)
                {
                    xs = x_xs;
                    g1.DrawImage(img1,
                            left * CC.A4GBILV,
                            CC.A4GAO_XS - down * CC.A4GBILV - height_xs ,
                            width_xs,
                            img1.Height * xs);
                }
                else
                {
                    xs = y_xs;
                    g1.DrawImage(img1,
                            left * CC.A4GBILV + width_xs - img1.Width * xs,
                            CC.A4GAO_XS - down * CC.A4GBILV - height_xs,
                            img1.Width * xs,
                            height_xs);
                }
                top_half1 = CC.A4GAO_XS - down * CC.A4GBILV - height_xs;
                //2
                if (img2 != null)
                {
                    xs = 0.0f;
                    x_xs = width_xs / img2.Width;
                    y_xs = height_xs / img2.Height;

                    top_half2 = top_half1 - height_xs - v_middle * CC.A4GBILV;

                    if (x_xs < y_xs)
                    {
                        xs = x_xs;
                        g1.DrawImage(img2,
                                left * CC.A4GBILV,
                                top_half2,
                                width_xs,
                                img2.Height * xs);

                        //top_half = top_half - height_xs - v_middle;
                    }
                    else
                    {
                        xs = y_xs;
                        g1.DrawImage(img2,
                                left * CC.A4GBILV + width_xs - img2.Width * xs,
                                top_half2,
                                img2.Width * xs,
                                height_xs);

                        //top_half = up * CC.A4GBILV + height_xs;
                    }

                }
                //3
                if (img3 != null)
                {
                    xs = 0.0f;
                    x_xs = width_xs / img3.Width;
                    y_xs = height_xs / img3.Height;

                    top_half3 = top_half2 - height_xs - v_middle * CC.A4GBILV;

                    if (x_xs < y_xs)
                    {
                        xs = x_xs;
                        g1.DrawImage(img3,
                                left * CC.A4GBILV,
                                top_half3 + height_xs - img3.Height * xs,
                                width_xs,
                                img3.Height * xs);

                        //top_half = top_half - height_xs - v_middle;
                    }
                    else
                    {
                        xs = y_xs;
                        g1.DrawImage(img3,
                                left * CC.A4GBILV + width_xs - img3.Width * xs,
                                top_half3,
                                img3.Width * xs,
                                height_xs);

                        //top_half = up * CC.A4GBILV + height_xs;
                    }

                }
                //4
                if (img4 != null)
                {
                    xs = 0.0f;
                    x_xs = width_xs / img4.Width;
                    y_xs = height_xs / img4.Height;

                    top_half4 = top_half3 - height_xs - v_middle * CC.A4GBILV;

                    if (x_xs < y_xs)
                    {
                        xs = x_xs;
                        g1.DrawImage(img4,
                                left * CC.A4GBILV,
                                top_half4 + height_xs - img4.Height * xs,
                                width_xs,
                                img4.Height * xs);

                        //top_half = top_half - height_xs - v_middle;
                    }
                    else
                    {
                        xs = y_xs;
                        g1.DrawImage(img4,
                                left * CC.A4GBILV + width_xs - img4.Width * xs,
                                top_half4,
                                img4.Width * xs,
                                height_xs);

                        //top_half = up * CC.A4GBILV + height_xs;
                    }

                }
                //5
                if (img5 != null)
                {
                    xs = 0.0f;
                    x_xs = width_xs / img5.Width;
                    y_xs = height_xs / img5.Height;

                    //top_half = top_half - height_xs - v_middle * CC.A4GBILV;

                    if (x_xs < y_xs)
                    {
                        xs = x_xs;
                        g1.DrawImage(img5,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_half4 + height_xs - img5.Height * xs,
                                width_xs,
                                img5.Height * xs);

                        //top_half = top_half - height_xs - v_middle;
                    }
                    else
                    {
                        xs = y_xs;
                        g1.DrawImage(img5,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_half4,
                                img5.Width * xs,
                                height_xs);

                        //top_half = up * CC.A4GBILV + height_xs;
                    }

                }
                //6
                if (img6 != null)
                {
                    xs = 0.0f;
                    x_xs = width_xs / img6.Width;
                    y_xs = height_xs / img6.Height;

                    //top_half = top_half - height_xs - v_middle * CC.A4GBILV;

                    if (x_xs < y_xs)
                    {
                        xs = x_xs;
                        g1.DrawImage(img6,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_half3 + height_xs - img6.Height * xs,
                                width_xs,
                                img6.Height * xs);

                        //top_half = top_half - height_xs - v_middle;
                    }
                    else
                    {
                        xs = y_xs;
                        g1.DrawImage(img6,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_half3,
                                img6.Width * xs,
                                height_xs);

                        //top_half = up * CC.A4GBILV + height_xs;
                    }

                }
                //7
                if (img7 != null)
                {
                    xs = 0.0f;
                    x_xs = width_xs / img7.Width;
                    y_xs = height_xs / img7.Height;

                    //top_half = top_half - height_xs - v_middle * CC.A4GBILV;

                    if (x_xs < y_xs)
                    {
                        xs = x_xs;
                        g1.DrawImage(img7,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_half2,
                                width_xs,
                                img7.Height * xs);

                        //top_half = top_half - height_xs - v_middle;
                    }
                    else
                    {
                        xs = y_xs;
                        g1.DrawImage(img7,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_half2,
                                img7.Width * xs,
                                height_xs);

                        //top_half = up * CC.A4GBILV + height_xs;
                    }

                }
                //8
                if (img8 != null)
                {
                    xs = 0.0f;
                    x_xs = width_xs / img8.Width;
                    y_xs = height_xs / img8.Height;

                    //top_half = top_half - height_xs - v_middle * CC.A4GBILV;

                    if (x_xs < y_xs)
                    {
                        xs = x_xs;
                        g1.DrawImage(img8,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_half1,
                                width_xs,
                                img8.Height * xs);

                        //top_half = top_half - height_xs - v_middle;
                    }
                    else
                    {
                        xs = y_xs;
                        g1.DrawImage(img8,
                                left * CC.A4GBILV + width_xs + h_middle * CC.A4GBILV,
                                top_half1,
                                img8.Width * xs,
                                height_xs);

                        //top_half = up * CC.A4GBILV + height_xs;
                    }

                }

                //加入文字绘制
                if (checkFlag)
                {
                    Font font = new Font(fontName, fontSize);
                    SolidBrush sbrush = new SolidBrush(Color.FromName(fontColor));
                    //留存字体绘制保存
                    float x;
                    x = 854 - content.Length * 8 / 2; //x=800;
                    g1.DrawString(content, font, sbrush, new PointF(x, height - down * CC.A4GBILV + text_photo * CC.A4GBILV));

                }
                var vv = System.IO.Path.GetFileNameWithoutExtension(item);
                var vv2 = System.IO.Path.GetExtension(item);
                vv2 = ".jpg";

                Bitmap im = bitMap;
                im.SetResolution(300, 300);
                //转成jpg

                //保存图片
                String imgurl = Path1 + "\\" + vv + "done" + vv2;
                im.Save(imgurl, jpsEncodeer, eps);
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

        private void Setting_Shown(object sender, EventArgs e)
        {
            if (indexDeal >= 101)
            {
                autoDeal();
            }
        }

        public String ReadFile()
        {
            if (!File.Exists("text.txt"))
            {
                return "";
            }
            StreamReader sr = File.OpenText("text.txt");
            String files = sr.ReadLine();                  //读取一行数据
            sr.Close();                    //释放资源
            return files;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //设置
            String file = ReadFile();
            if (file != "")
            {
                var files = file.Split('-');
                numericUpDown1.Value = int.Parse(files[0]);
                numericUpDown2.Value = int.Parse(files[1]);
                numericUpDown3.Value = int.Parse(files[2]);
                numericUpDown4.Value = int.Parse(files[3]);
                numericUpDown5.Value = int.Parse(files[4]);
                numericUpDown6.Value = int.Parse(files[5]);
                numericUpDown7.Value = int.Parse(files[6]);
            }
            else
            {
                numericUpDown1.Value = 8;
                numericUpDown2.Value = 8;
                numericUpDown3.Value = 8;
                numericUpDown4.Value = 8;
                numericUpDown5.Value = 7;
                numericUpDown6.Value = 5;
                numericUpDown7.Value = 2;
            }
            if (!panel1.Visible)
            {
                panel1.Visible = true;
            }
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            var file = ReadFile();
            if (file != "")
            {
                var files = file.Split('-');
                up = int.Parse(files[0]);
                down = int.Parse(files[1]);
                left = int.Parse(files[2]);
                right = int.Parse(files[3]);
                v_middle = int.Parse(files[4]);
                h_middle = int.Parse(files[5]);
                text_photo = int.Parse(files[6]);
            }
            else
            {
                up = 8;
                down = 8;
                left = 8;
                right = 8;
                v_middle = 7;
                h_middle = 5;
                text_photo = 2;
            }
            panel1.Visible = false;
            this.panel1.Location = new System.Drawing.Point(12, 12);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WriteFile(numericUpDown1.Value.ToString() + "-" +
                         numericUpDown2.Value.ToString() + "-" +
                         numericUpDown3.Value.ToString() + "-" +
                         numericUpDown4.Value.ToString() + "-" +
                         numericUpDown5.Value.ToString() + "-" +
                         numericUpDown6.Value.ToString() + "-" +
                         numericUpDown7.Value.ToString());

            up = int.Parse(numericUpDown1.Value.ToString());
            down = int.Parse(numericUpDown2.Value.ToString());
            left = int.Parse(numericUpDown3.Value.ToString());
            right = int.Parse(numericUpDown4.Value.ToString());
            v_middle = int.Parse(numericUpDown5.Value.ToString());
            h_middle = int.Parse(numericUpDown6.Value.ToString());
            text_photo = int.Parse(numericUpDown7.Value.ToString());

            if (panel1.Visible)
            {
                panel1.Visible = false;
            }
        }

        public void WriteFile(String file)
        {
            StreamWriter sw = File.CreateText("text.txt");
            sw.WriteLine(file);                //写入一行文本
            sw.Flush();                    //清空
            sw.Close();                    //关闭
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                panel1.Visible = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                相册排版界面.TextDescription textDescription = new 相册排版界面.TextDescription();
                textDescription.ShowDialog(this);
                fontName = textDescription.fontName;
                fontSize = textDescription.fontSize;
                content = textDescription.content;
                fontColor = textDescription.fontColor;
            } 
        }


    }
}
