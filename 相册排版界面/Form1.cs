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
    public partial class Form1 : Form
    {
        public Form2 form2;

        public Form1(Form2 form2)
        {
            InitializeComponent();
            this.form2 = form2;
        }

        public Form1()
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            form2.Show();

            TextBox tb1 = new TextBox();
            tb1.Name = "textBox" + 3;
            tb1.Text = "aaaaaaaaaaaaaaaaaaaaaaaa";
            tb1.Location = new Point(30, 70);
            tb1.Size = new Size(400, 50);
            tb1.BringToFront();
            this.form2.Controls.Add(tb1);

        }
    }
}
