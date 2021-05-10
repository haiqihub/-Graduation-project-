﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 相册排版界面
{
    public partial class SingleDeal : Form
    {
        private string file1;
        //private string file2;

        private Size size;
        private double m_scale = 1.0;
        private int m_rotate = 0;

        private Point point1, point2;
        private Rectangle rectSmall;

        private System.Drawing.Image image = null;
        private System.Drawing.Image image3 = null;

        private bool m_bDown = false;

        private string imgpath1;
        private string imgpath2;
        //private Size size2;
        //private double m_scale11 = 1.0;
        //private int m_rotate11 = 0;

        //private Point point111, point112;
        //private Rectangle rectSmall2;

        //private System.Drawing.Image image11 = null;
        //private System.Drawing.Image image113 = null;

        //private bool m_bDown11 = false;

        //private DateTime dateTime;



        public SingleDeal(string img1, string img2)
        {
            InitializeComponent();

            imgpath1 = img1;
            imgpath2 = img2;
            //1
            size = new Size(pictureBox1.Width, pictureBox1.Height);
            //可调整坐标和矩形宽高达到鼠标位置的选择
            rectSmall = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);

            ////2
            //size2 = new Size(pictureBox2.Width, pictureBox2.Height);
            ////可调整坐标和矩形宽高达到鼠标位置的选择
            //rectSmall2 = new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height);

            //dateTime = DateTime.Now;

            //1
            m_bDown = false;
            point1.X = point1.Y = 0;
            point2.X = point2.Y = 0;
            image = GetImageFromServer(img1);
            image3 = GetImageFromServer(img1);
            scaleImage(pictureBox1, img1);
            AcquireRectangleImage(image, new Rectangle(point2, size));
            //2
            //m_bDown11 = false;
            //point111.X = point111.Y = 0;
            //point112.X = point112.Y = 0;
            //image11 = GetImageFromServer(img2);
            //image113 = GetImageFromServer(img2);
            //scaleImage(pictureBox2, img2);
            //AcquireRectangleImage11(image11, new Rectangle(point112, size2));


        }
        //在picturebox 里加载照片
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

        //放大缩小
        public Image ReduceImage(Image originalImage, int toWidth, int toHeight)
        {
            Console.WriteLine(toWidth + "," + toHeight);
            Image toBitmap = new Bitmap(toWidth, toHeight);
            using (Graphics g = Graphics.FromImage(toBitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(Color.Transparent);

                //   image:
                //     要绘制的 System.Drawing.Image。
                //
                //   destRect:    ！ 这里应该可以得到坐标 绘制在坐标处 ！
                //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。 将图像进行缩放以适合该矩形。
                //
                //   srcRect:
                //                 
                //     System.Drawing.Rectangle 结构，它指定 image 对象中要绘制的部分。
                //
                //   srcUnit:
                //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
                g.DrawImage(originalImage,
                            new Rectangle(0, 0, toWidth, toHeight),
                            new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                            GraphicsUnit.Pixel);
                return toBitmap;
            }
        }
        //旋转
        public Image RotateImg(Image b, int angle)
        {
            angle = angle % 360;
            //弧度转换 
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //原图的宽和高 
            int w = b.Width;
            int h = b.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
            //目标位图 
            Bitmap dsImage = new Bitmap(W, H);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //计算偏移量 
            Point Offset = new Point((W - w) / 2, (H - h) / 2);
            //构造图像显示区域:让图像的中心与窗口的中心点一致 
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(angle);
            //恢复图像在水平和垂直方向的平移 
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(b, rect);
            //重至绘图的所有变换 
            g.ResetTransform();
            //g.Save();
            g.Dispose();
            //保存旋转后的图片 
            //dsImage.Save(@"D:\img\" + Path.GetFileNameWithoutExtension(file) + "\\" + angle + ".png", System.Drawing.Imaging.ImageFormat.Png);
            return dsImage;
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
        
        //把图片绘制在规定大小规定位置的矩形内
        public void AcquireRectangleImage(Image source, Rectangle rect)
        {
            if (source == null || rect.IsEmpty)
            {
                return;
            }

            Bitmap bmSmall = new Bitmap(pictureBox1.Width, pictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            using (Graphics grSmall = Graphics.FromImage(bmSmall))
            {
                //grSmall.Clear(color_back);
                grSmall.DrawImage(source,
                                  rectSmall,
                                  rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top,
                                  GraphicsUnit.Pixel);
                                    //第三行参数
                                    //   srcX:
                                    //     要绘制的源图像部分的左上角的 x 坐标。
                                    //
                                    //   srcY:
                                    //     要绘制的源图像部分的左上角的 y 坐标。
                                    //
                                    //   srcWidth:
                                    //     要绘制的源图像部分的宽度。
                                    //
                                    //   srcHeight:
                                    //     要绘制的源图像部分的高度。

                this.pictureBox1.Image = bmSmall;
                
            }
        }

        //public void AcquireRectangleImage11(Image source, Rectangle rect)
        //{
        //    if (source == null || rect.IsEmpty) return;

        //    Bitmap bmSmall = new Bitmap(pictureBox1.Width, pictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

        //    using (Graphics grSmall = Graphics.FromImage(bmSmall))
        //    {
        //        //grSmall.Clear(color_back);
        //        grSmall.DrawImage(source,
        //                          rectSmall2,
        //                          rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top,
        //                          GraphicsUnit.Pixel);

        //        this.pictureBox2.Image = bmSmall;

        //    }
        //}

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_bDown == true)
            {
                point2.X -= e.X - point1.X;
                point2.Y -= e.Y - point1.Y;

                AcquireRectangleImage(image, new Rectangle(point2, size));

                point1.X = e.X;
                point1.Y = e.Y;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            m_bDown = true;
            point1.X = e.X;
            point1.Y = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            m_bDown = false;
        }

        //更换图片
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;//等于true表示可以选择多个文件
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "图片|*.jpg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dlg.FileNames)
                {
                    m_bDown = false;
                    point1.X = point1.Y = 0;
                    point2.X = point2.Y = 0;

                    image = GetImageFromServer(file);
                    image3 = GetImageFromServer(file);
                    AcquireRectangleImage(image, new Rectangle(point2, size));
                }
            }
        }
        //左旋
        private void button2_Click(object sender, EventArgs e)
        {
            m_rotate -= 90;
            Image tmp2 = this.ReduceImage(image3, (int)(m_scale * image3.Width), (int)(m_scale * image3.Height));
            Image tmp3 = this.RotateImg(tmp2, m_rotate);
            image = tmp3;
            AcquireRectangleImage(image, new Rectangle(point2, size));
        }
        //右旋
        private void button3_Click(object sender, EventArgs e)
        {
            m_rotate += 90;
            Image tmp2 = this.ReduceImage(image3, (int)(m_scale * image3.Width), (int)(m_scale * image3.Height));
            Image tmp3 = this.RotateImg(tmp2, m_rotate);
            image = tmp3;
            AcquireRectangleImage(image, new Rectangle(point2, size));
        }
        //放大
        private void button4_Click(object sender, EventArgs e)
        {
            m_scale += 0.1;
            if (m_scale > 5)
                return;
            Image tmp2 = this.ReduceImage(image3, (int)(m_scale * image3.Width), (int)(m_scale * image3.Height));
            Image tmp3 = this.RotateImg(tmp2, m_rotate);
            image = tmp3;
            AcquireRectangleImage(image, new Rectangle(point2, size));
        }
        //缩小
        private void button5_Click(object sender, EventArgs e)
        {
            m_scale -= 0.1;
            if (m_scale < 0.1)
                return;
            Image tmp2 = this.ReduceImage(image3, (int)(m_scale * image3.Width), (int)(m_scale * image3.Height));
            Image tmp3 = this.RotateImg(tmp2, m_rotate);
            image = tmp3;
            AcquireRectangleImage(image, new Rectangle(point2, size));
        }
        
        //private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (m_bDown11 == true)
        //    {
        //        point112.X -= e.X - point111.X;
        //        point112.Y -= e.Y - point111.Y;

        //        AcquireRectangleImage11(image11, new Rectangle(point112, size2));

        //        point111.X = e.X;
        //        point111.Y = e.Y;
        //    }
        //}

        //private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        //{
        //    m_bDown11 = true;
        //    point111.X = e.X;
        //    point111.Y = e.Y;
        //}

        //private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        //{
        //    m_bDown11 = false;
        //}

        //private void button10_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog dlg = new OpenFileDialog();
        //    dlg.Multiselect = false;//等于true表示可以选择多个文件
        //    dlg.DefaultExt = ".jpg";
        //    dlg.Filter = "图片|*.jpg";
        //    if (dlg.ShowDialog() == DialogResult.OK)
        //    {
        //        foreach (string file in dlg.FileNames)
        //        {
        //            m_bDown11 = false;
        //            point111.X = point111.Y = 0;
        //            point112.X = point112.Y = 0;

        //            image11 = GetImageFromServer(file);
        //            image113 = GetImageFromServer(file);
        //            AcquireRectangleImage11(image11, new Rectangle(point112, size2));
        //        }
        //    }
        //}

        //private void button9_Click(object sender, EventArgs e)
        //{
        //    m_rotate11 -= 90;
        //    Image tmp112 = this.ReduceImage(image113, (int)(m_scale11 * image113.Width), (int)(m_scale11 * image113.Height));
        //    Image tmp113 = this.RotateImg(tmp112, m_rotate11);
        //    image11 = tmp113;
        //    AcquireRectangleImage11(image11, new Rectangle(point112, size2));
        //}

        //private void button8_Click(object sender, EventArgs e)
        //{
        //    m_rotate11 += 90;
        //    Image tmp112 = this.ReduceImage(image113, (int)(m_scale11 * image113.Width), (int)(m_scale11 * image113.Height));
        //    Image tmp113 = this.RotateImg(tmp112, m_rotate11);
        //    image11 = tmp113;
        //    AcquireRectangleImage11(image11, new Rectangle(point112, size2));
        //}

        //private void button7_Click(object sender, EventArgs e)
        //{
        //    m_scale11 += 0.05;
        //    if (m_scale11 < 0.1)
        //        return;
        //    Image tmp112 = this.ReduceImage(image113, (int)(m_scale11 * image113.Width), (int)(m_scale11 * image113.Height));
        //    Image tmp113 = this.RotateImg(tmp112, m_rotate11);
        //    image11 = tmp113;
        //    AcquireRectangleImage11(image11, new Rectangle(point112, size2));
        //}

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    m_scale11 -= 0.05;
        //    if (m_scale11 < 0.1)
        //        return;
        //    Image tmp112 = this.ReduceImage(image113, (int)(m_scale11 * image113.Width), (int)(m_scale11 * image113.Height));
        //    Image tmp113 = this.RotateImg(tmp112, m_rotate11);
        //    image11 = tmp113;
        //    AcquireRectangleImage11(image11, new Rectangle(point112, size2));
        //}

        List<string> list = new List<string>();
        FirstDlg modelDlg = new FirstDlg();
        int up = 8;
        int down = 8;
        int left = 8;
        int right = 8;
        int v_middle = 8;

        //保存
        private void button11_Click(object sender, EventArgs e)
        {
            string folderDirPath = System.Environment.CurrentDirectory + "\\";

            this.pictureBox1.Image.Save(folderDirPath + "a.jpg");
            //this.pictureBox2.Image.Save(folderDirPath + "b.jpg");
            
            //图片list -----------
            list.Clear();
            list.Add(folderDirPath + "a.jpg");
            list.Add(imgpath2);

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
            File.Delete(folderDirPath + "a.jpg");
            //File.Delete(imgpath2);
        }


        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // 这里是后台线程， 是在另一个线程上完成的
            // 这里是真正做事的工作线程
            // 可以在这里做一些费时的，复杂的操作
            BackgroundWorker bw = sender as BackgroundWorker;

            HechengB1();

            bw.ReportProgress(100);
        }

        void UpdateProgress(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            if (i == 100)
            {
                modelDlg.Close();
                MessageBox.Show("保存成功");
                this.Dispose();
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //这时后台线程已经完成，并返回了主线程，所以可以直接使用UI控件了 
        }

        public void HechengB1()
        {
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

                var vv = System.IO.Path.GetFileNameWithoutExtension(imgpath1);
                //var vv2 = System.IO.Path.GetExtension(imgpath1);
                var vv2 = ".jpg";

                Bitmap im = bitMap;
                im.SetResolution(300, 300);
                //转成jpg

                //保存图片
                String imgurl = Path1 + "\\" + vv + "done" + vv2;
                //String imgurl = imgpath1;
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

    }
}
