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
    public partial class TextInput : Form
    {
        //鼠标按下坐标（control控件的相对坐标）
        Point mouseDownPoint = Point.Empty;
        //显示拖动效果的矩形
        Rectangle rect = Rectangle.Empty;
        //是否正在拖拽
        bool isDrag = false;

        public TextInput()
        {
            InitializeComponent();
        }

        private void TextInput_Load(object sender, EventArgs e)
        {

        }

        private void TextInput_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
