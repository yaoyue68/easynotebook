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

namespace 简易记事本
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 全局变量,当前打开的文件路径储存在隐藏的marklocationlabel1.text中
        /// </summary>
        filesoperator fop = new filesoperator();//文件操作类
        Dictionary<string, string> Dc = new Dictionary<string, string>();//<路径名,路径>字典

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择文件：";
            ofd.Filter = "文本文件|*.txt|所有文件|*.txt";
            ofd.Multiselect = false;
            ofd.InitialDirectory = @"G:\"; //初始目录
            ofd.ShowDialog();
            //marklocationlabel1.text中的路径总是最新的这一次

            fop.ReadFile(ofd.FileName, this.textBox1, marklocation, 默认ToolStripMenuItem.Text);
            HistoryFiles();

        }
        /// <summary>
        /// 历史记录生成listbox1
        /// </summary>
        private void HistoryFiles()
        {
            //判断路径是否为空，去重后加入DC，空则不管

            if (marklocation.Text != null && marklocation.Text != "")
            {
                if (Dc.ContainsValue(marklocation.Text))
                {

                }
                else
                {
                    Dc.Add(Path.GetFileName(marklocation.Text), marklocation.Text);
                }

            }

            //将dc文件名加入listbox，必须先clear（）

            listBox1.Items.Clear();
            foreach (var item in Dc)
            {
                listBox1.Items.Add(item.Key);
            }
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FontDialog fd = new FontDialog();
            fd.Font = new Font("宋体", 13.8f, FontStyle.Regular);//字体初始化设置
            fd.ShowDialog();
            textBox1.Font = fd.Font; //注意
        }

        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();

            textBox1.ForeColor = cd.Color; //注意
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Visible = false; //form1加载时隐藏listbox

        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //新文件路径为空，打开对话框保存,老文件直接保存
            if (marklocation.Text == null | marklocation.Text == "")
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "文本文件|*.txt";
                sfd.ShowDialog();
                fop.WriteFile(sfd.FileName, textBox1.Text, toolStripStatusLabel1, corderlabel.Text);
            }
            else
            {
                fop.WriteFile(marklocation.Text, textBox1.Text, toolStripStatusLabel1, corderlabel.Text);
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "文本文件|*.txt";
            sf.ShowDialog();
            fop.WriteFile(sf.FileName, this.textBox1.Text, toolStripStatusLabel1, corderlabel.Text);

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            //60s为一个保存周期，60s时新文件提示启用，打开的文件自动保存,其他时间再分2种情况
            if (DateTime.Now.Second % 60 == 0)
            {

                if (marklocation.Text != "" | marklocation.Text != null)
                {

                    fop.WriteFile(marklocation.Text, this.textBox1.Text, toolStripStatusLabel1, corderlabel.Text);
                    toolStripStatusLabel1.Text = "自动保存成功!";
                }
                else
                {
                    toolStripStatusLabel1.Text = "打开文件后启用自动保存!";
                }


            }
            else if (marklocation.Text == "" | marklocation.Text == null)

            {

                toolStripStatusLabel1.Text = "打开文件后启用自动保存!";
            }
            else
            {
                toolStripStatusLabel1.Text = "自动保存倒计时中---" + (60 - Convert.ToInt32(DateTime.Now.Second));
            }

        }

        private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (自动换行ToolStripMenuItem.Text == "自动换行")
            {
                textBox1.WordWrap = true;
                自动换行ToolStripMenuItem.Text = "取消自动换行";
            }
            else
            {
                textBox1.WordWrap = false;
                自动换行ToolStripMenuItem.Text = "自动换行";
            }

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            listBox1.Visible = false; //双击隐藏listbox
        }

        private void 历史ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                listBox1.Visible = false;
            }
            else
            {
                listBox1.Visible = true;
            }

        }

        //随着listbox选择不同项，当前项先保存，再打开新的项文件
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fop.WriteFile(marklocation.Text, textBox1.Text, toolStripStatusLabel1, corderlabel.Text);
            fop.ReadFile(Dc[listBox1.SelectedItem.ToString()], this.textBox1, marklocation, 默认ToolStripMenuItem.Text);

        }

        //双击隐藏切换
        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.Visible == true)
            {
                listBox1.Visible = false;
            }
            else
            {
                listBox1.Visible = true;
            }
        }

        private void 版本说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string updatehistory = "2021.4.25 实现自动保存，listbox列表" + "\t\r" +
                "2021.";

            MessageBox.Show(updatehistory, "", MessageBoxButtons.OK);

        }

        private void 待实现功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newfunction = "添加设置选项，长久保存" + "\t\r";

            MessageBox.Show(newfunction, "", MessageBoxButtons.OK);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
                this.textBox1.Cursor = System.Windows.Forms.Cursors.Arrow;  //指定鼠标形状（更好看）  
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }


        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            //GetValue(0) 为第1个文件全路径  
            //DataFormats 数据的格式，下有多个静态属性都为string型，除FileDrop格式外还有Bitmap,Text,WaveAudio等格式  
            string path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            fop.ReadFile(path, textBox1, marklocation, 默认ToolStripMenuItem.Text);
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam; //还原鼠标形状  
            HistoryFiles();
        }

        private void uTF8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] bytetextbox1;
            if (corderlabel.Text == "默认")
            {
                bytetextbox1 = Encoding.Default.GetBytes(textBox1.Text);
            }
            else
            {
                bytetextbox1 = Encoding.GetEncoding(corderlabel.Text).GetBytes(textBox1.Text);
            }
            string newtextbox1 = Encoding.UTF8.GetString(bytetextbox1);
            textBox1.Text = newtextbox1;
            corderlabel.Text = "UTF-8";

        }

        private void 默认ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] bytetextbox1;
            if (corderlabel.Text == "默认")
            {
               bytetextbox1 = Encoding.Default.GetBytes(textBox1.Text);
            }

            else
            {
               bytetextbox1 = Encoding.GetEncoding(corderlabel.Text).GetBytes(textBox1.Text);
                }

            string newtextbox1 = Encoding.ASCII.GetString(bytetextbox1);
            textBox1.Text = newtextbox1;
            corderlabel.Text = "默认";
        }

        private void aNTIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] bytetextbox1;
            if (corderlabel.Text == "默认")
            {
                bytetextbox1 = Encoding.Default.GetBytes(textBox1.Text);
            }
            else
            {
                bytetextbox1 = Encoding.GetEncoding(corderlabel.Text).GetBytes(textBox1.Text);
            }
            string newtextbox1 = Encoding.ASCII.GetString(bytetextbox1);
            textBox1.Text = newtextbox1;
            corderlabel.Text = "ASCII";

        }

        private void gBK232ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            byte[] bytetextbox1;
            if (corderlabel.Text == "默认")
            {
                bytetextbox1 = Encoding.Default.GetBytes(textBox1.Text);
            }
            else
            {
                bytetextbox1 = Encoding.GetEncoding(corderlabel.Text).GetBytes(textBox1.Text);
            }

            string newtextbox1 = Encoding.GetEncoding("GB2312").GetString(bytetextbox1);
            textBox1.Text = newtextbox1;
            corderlabel.Text = "GB2312";
        }
    }
}
