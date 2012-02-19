using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

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
            string[] aryStrCouponId = di.CouponTop();
            if (aryStrCouponId.Length == 0)
            {
                MessageBox.Show("没有数据！");
            }
            else
            {
                string strCouponIds = "";
                for (int i = 0; i < aryStrCouponId.Length; i++)
                {
                    strCouponIds += aryStrCouponId[i] + ",";
                }
                MessageBox.Show(strCouponIds);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DownloadInfo di = new DownloadInfo();
            string[] aryStrShopId = di.ShopAround();
            if (aryStrShopId.Length == 0)
            {
                MessageBox.Show("没有数据！");
            }
            else
            {
                string strShopIds = "";
                for (int i = 0; i < aryStrShopId.Length; i++)
                {
                    strShopIds += aryStrShopId[i] + ",";
                }
                MessageBox.Show(strShopIds);
            }
        }

        private string[] lines;
        private void button13_Click(object sender, EventArgs e)
        {
            PrintDocument pdDocument = new PrintDocument();
            //訂閱PinrtPage事件,用於繪製各個頁內容
            pdDocument.PrintPage += new PrintPageEventHandler(OnPrintPage);
            //訂閱BeginPrint事件,用於得到被打印的內容
            pdDocument.BeginPrint += new PrintEventHandler(pdDocument_BeginPrint);
            //訂閱EndPrint事件,用於釋放資源
            pdDocument.EndPrint += new PrintEventHandler(pdDocument_EndPrint);
            
            ///////////////
            try
            {
                /*
                * PrintDocument對象的Print()方法在PrintController類的幫助下，執行PrintPage事件。
                */

                //3、調用PrintDocument.Print()方法
                pdDocument.Print();

            }
            catch (InvalidPrinterException ex)
            {
                MessageBox.Show(ex.Message, "Simple Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        void pdDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            lines = new string[2];
            lines[0] = "123";
            lines[1] = "456";
        }

        /// <summary>
        /// ４、繪制多個打印頁面
        /// printDocument的PrintPage事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            int x = 20;
            int y = 20;
            foreach (string line in lines)
            {
                /*
                 * 4、把文本行發送給打印機，其中e是PrintPageEventArgs類型的一個變量，其屬性連接到打印機關聯文本中。
                 * 打印機關聯文本可以寫到打印機設備上。
                 * 輸出結果的位置用變更X和Y定義。
                 */
                e.Graphics.DrawString(line, new Font("Arial", 10), Brushes.Black, x, y);
                y += 15;
            }

        }

        /// <summary>  
        ///５、EndPrint事件,釋放資源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pdDocument_EndPrint(object sender, PrintEventArgs e)
        {
            //變量Lines占用和引用的字符串數組，現在釋放
            MessageBox.Show(e.PrintAction.ToString());
            lines = null;
        }
    }
}
