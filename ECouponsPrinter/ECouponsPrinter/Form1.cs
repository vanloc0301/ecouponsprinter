using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECouponsPrinter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DownloadInfo di = new DownloadInfo();
            di.form1 = this;
            di.download();
        }

        public void setText(string str)
        {
            this.textBox1.Text = str;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UploadInfo ui = new UploadInfo();
            ui.form1 = this;
            MessageBox.Show(ui.CouponAuth("0526", "3403") + "");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UploadInfo ui = new UploadInfo();
            ui.form1 = this;
            MessageBox.Show(ui.CouponPrint() + "");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UploadInfo ui = new UploadInfo();
            ui.form1 = this;
            Member m = ui.MemberAuth(textBox2.Text);
            if (m == null)
            {
                textBox1.Text = "无效用户";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("卡号：").Append(m.StrCardNo).Append("\n");
                sb.Append("收藏：");
                for (int i = 0; i < m.AryFavourite.Length; i++)
                {
                    sb.Append(m.AryFavourite[i]).Append(" ");
                }
                sb.Append("\n");
                sb.Append("历史：");
                for (int i = 0; i < m.AryHistory.Length; i++)
                {
                    sb.Append(m.AryHistory[i]).Append(" ");
                }
                textBox1.Text = sb.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UploadInfo ui = new UploadInfo();
            ui.form1 = this;
            MessageBox.Show(ui.PrintAlert(500) + "");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Program.Exit();
        }
    }
}
