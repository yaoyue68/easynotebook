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

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="textBox1">写入的文本框对象</param>
        /// <param name="marklocation">读取的路径存入label.text中</param>
        /// <param name="corder">编码格式，在菜单text中</param>
        public void ReadFile(string path,TextBox textBox1,Label marklocation,string corder)
        {

            if (path== "")
            {
                return ; //路径空则跳出方法
            }
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] fbytes = new byte[1024 * 1024 * 5];
                int realcount=fs.Read(fbytes, 0, fbytes.Length);

                string textcont = null;
                switch (corder)
                {
                    case "默认":
                        textcont = Encoding.Default.GetString(fbytes, 0, realcount);
                        break;
                    case"UTF-8":
                        textcont = Encoding.UTF8.GetString(fbytes, 0, realcount);
                        break;
                    case "ASCII":
                        textcont = Encoding.ASCII.GetString(fbytes, 0, realcount);
                        break;
                    case "GB2312":
                        textcont = Encoding.GetEncoding("GB2312").GetString(fbytes, 0, realcount);
                        break;
                    default:
                        textcont = Encoding.Default.GetString(fbytes, 0, realcount);
                        break;
                }

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
        /// <param name="corder">编码格式，在菜单text中</param>
        public void WriteFile(string path,string contents, ToolStripStatusLabel toolStripStatusLabel, string corder)
        {
            if (path == ""|path==null)
            {
                return;
            }
            using (FileStream fs=new FileStream(path,FileMode.Create) )
            {
                byte[] filebytes = null;
                switch (corder)
                {
                    case "默认":
                        filebytes= Encoding.Default.GetBytes(contents);
                        break;
                    case "UTF-8":
                        filebytes = Encoding.UTF8.GetBytes(contents);
                        break;
                    case "ASCII":
                        filebytes = Encoding.ASCII.GetBytes(contents);
                        break;
                    case "GB2312":
                        filebytes = Encoding.GetEncoding("GB2312").GetBytes(contents);
                        break;
                    default:
                        filebytes = Encoding.Default.GetBytes(contents);
                        break;
                }

                fs.Write(filebytes, 0, filebytes.Length);
            }

            toolStripStatusLabel.Text = "写入成功！";
        }



    }





}

