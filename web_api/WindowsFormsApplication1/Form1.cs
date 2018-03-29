using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Touppser;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        } 


        private void button2_Click(object sender, EventArgs e)
        {
            c1 x = new c1();//用new 建立類別實體
            textBox2.Text = x.Touppser(textBox2.Text);//使用類別的方法
        }
    }
}
