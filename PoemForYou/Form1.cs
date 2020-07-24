using JiebaNet.Segmenter;
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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // Directory.CreateDirectory("./in/");
           // Directory.CreateDirectory("./out/");
            var segmenter = new JiebaSegmenter();
            var file = File.OpenRead("./in/src.txt");
            var rbuf = new StreamReader(file);
            var txt = rbuf.ReadToEnd();
            rbuf.Close();
            file.Close();
            int cnt = 1;
            var sb = new StringBuilder();
            for (int i = 0; i < txt.Length; i++)
            {
                var chr = txt[i];
                if ((chr >= 'A' && chr <= 'Z') || (chr >= 'a' && chr <= 'z') || (chr >= '0' && chr <= '9') || (chr >= 0x4e00 && chr <= 0x9fa5))
                {
                    sb.Append(chr);
                }
                else
                {
                    if (sb.Length >= 80)
                    {
                        file = File.OpenWrite($"./out/无题{cnt}.txt");
                        var wbuf = new StreamWriter(file);
                        wbuf.WriteLine("  无题  ");
                        wbuf.WriteLine("作者：佚名");
                        wbuf.WriteLine();
                        var splitWords = segmenter.Cut(sb.ToString());
                        int sp = 0;
                        while (sp < splitWords.Count())
                        {
                            wbuf.WriteLine(string.Join("", splitWords.Skip(sp).Take(3)));
                            sp += 3;
                        }
                        wbuf.Close();
                        file.Close();
                        cnt++;
                        sb.Clear();
                    }
                }
                if (cnt >= 1000)
                {
                    break;
                }
            }
        }
    }
}
