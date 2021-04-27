using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 简易记事本
{
     class filesoperator
    {   
       public string Text { get ; set; }

       /// <summary>
       /// 读取文件
       /// </summary>
       /// <param name="path">文件路径</param>
       /// <param name="textBox1">写入的文本框对象</param>
       /// <param name="marklocation">读取的路径存入label.text中</param>
        public void ReadFile(string path,TextBox textBox1,Label marklocation)
        {

            if (path== "")
            {
                return ; //路径空则跳出方法
            }
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] fbytes = new byte[1024 * 1024 * 5];
                int realcount=fs.Read(fbytes, 0, fbytes.Length);
                string textcont= Encoding.Default.GetString(fbytes, 0, realcount);

                textBox1.Text= textcont;
                marklocation.Text = path; 
                
            }
            
        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="contents">内容</param>
        /// <param name="toolStripStatusLabel">写入的状态栏</param>
        public void WriteFile(string path,string contents, ToolStripStatusLabel toolStripStatusLabel)
        {
            if (path == ""|path==null)
            {
                return;
            }
            using (FileStream fs=new FileStream(path,FileMode.Create) )
            {
                byte[] filebytes = Encoding.Default.GetBytes(contents);
                fs.Write(filebytes, 0, filebytes.Length);
            }

            toolStripStatusLabel.Text = "写入成功！";
        }

        }








    }

