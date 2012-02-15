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
        private string strCode = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DownloadInfo di = new DownloadInfo();
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
            string strReturn = ui.CouponAuth("3897", "3403", "1325207051877004");
            if (strReturn.Equals("OK"))
            {
                MessageBox.Show("成功！");
            }
            else if (strReturn.Equals("balance_error"))
            {
                MessageBox.Show("余额不足！");
            }
            else if (strReturn.Equals("sms_error"))
            {
                MessageBox.Show("短信发送失败！");
            }
            else
            {
                MessageBox.Show("未知错误！");
            }
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
                if (m.StrMobileNo.Length == 0)
                {
                    MessageBox.Show("未注册用户，请在下面文本框中输入手机号码进行注册！");
                }
                else
                {
                    MessageBox.Show("已注册用户！");
                }
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

        private void button7_Click(object sender, EventArgs e)
        {
            DownloadInfo di = new DownloadInfo();
            di.SynParam();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            this.strCode = rand.Next(0, 10000).ToString("D4");
            
            UploadInfo ui = new UploadInfo();
            if (ui.MemberLogon(this.textBox2.Text, this.textBox3.Text, this.strCode))
            {
                MessageBox.Show("注册信息已发送，请在下面文本框中输入你手机收到的验证码");
            }
            else
            {
                MessageBox.Show("发生异常！");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (this.textBox4.Text.Equals(this.strCode))
            {
                UploadInfo ui = new UploadInfo();
                if (ui.MemberLogon(this.textBox2.Text, this.textBox3.Text))
                {
                    MessageBox.Show("注册成功！");
                }
                else
                {
                    MessageBox.Show("发生异常！");
                }
            }
            else
            {
                MessageBox.Show("验证码错误，请重新输入！");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            UploadInfo ui = new UploadInfo();
            ui.form1 = this;
            if (ui.CouponFavourite("0526", "123456"))
            {
                MessageBox.Show("操作成功！");
            }
            else
            {
                MessageBox.Show("操作失败！");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DownloadInfo di = new DownloadInfo();
            
        }
    }
}
