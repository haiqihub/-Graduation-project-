using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 相册排版界面
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            相册排版界面.Scanner layout = new 相册排版界面.Scanner();
            layout.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            相册排版界面.Layout layout = new 相册排版界面.Layout();
            layout.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*相册排版界面.SingleDeal singleDeal = new 相册排版界面.SingleDeal();
            singleDeal.ShowDialog();*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Start_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
