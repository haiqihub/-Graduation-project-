using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 相册排版界面
{
    public partial class TextDescription : Form
    {
        public string fontName = "宋体";
        public int fontSize = 12;
        public string content = "";
        public string location = "图片上方";

        Boolean textboxHasText = false;

        public TextDescription()
        {
            InitializeComponent();
        }
        //获取系统字体方法
        public void GetFontNames()
        {
            FontFamily[] fontFamilies;
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            fontFamilies = installedFontCollection.Families;
            for (int i = 0; i < fontFamilies.Length; i++)
            {
                this.comboBox1.Items.Add(fontFamilies[i].Name);
            }
            this.comboBox1.SelectionStart = 1;
            fontName = fontFamilies[0].Name;

            for (int i = 7; i < 60; i++)
            {
                this.comboBox2.Items.Add(i);
            }
            this.comboBox2.SelectionStart = 5;

            this.comboBox3.Items.Add("图片上方");
            this.comboBox3.Items.Add("图片中间");
            this.comboBox3.Items.Add("图片下方");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fontName = (string)comboBox1.SelectedItem;
            fontSize = (int)comboBox2.SelectedItem;
            content = this.textBox1.Text;
            location = (string)comboBox3.SelectedItem;

            this.Hide();
        }

        private void FontSelect_Shown(object sender, EventArgs e)
        {
            GetFontNames();
            //this.textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textBox1.Text = "";
        }

        private void TextDescription_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textboxHasText==false)
            {
                textBox1.Text = "";
            }
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                textBox1.Text = "请输入照片描述";
                textBox1.ForeColor = Color.LightGray;
                textboxHasText = false;

            }
            else
            {
                textboxHasText = true;
            }
        }

       
    }
}
