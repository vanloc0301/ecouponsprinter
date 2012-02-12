using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ECouponsPrinter
{
    public partial class Login : Form
    {
        private static int TickNum = 30;
        private String checkCode;
        Thread th;

        public Login()
        {
            InitializeComponent();
        }

        private void CodeTimer_Tick(object sender, EventArgs e)
        {
            this.Reget.Text = "重新获取(" + --TickNum + ")";
            if (TickNum <= 0)
            {
                this.CodeTimer.Stop();
                this.Reget.Text = "获取验证码";
                this.Reget.Enabled = true;
            }
        }

        private void Reget_Click(object sender, EventArgs e)
        {
            if (th != null)
            {
                th.Abort();
                th.Join();
            }

            th = new Thread(new ThreadStart(sendMes));
            th.Start();
            this.Reget.Enabled = false;
            TickNum = 30;
            this.CodeTimer.Start();
        }

        private void sendCode()
        {
            Random rand = new Random();
            String code = rand.Next(0, 10000).ToString("D4");
            checkCode = code;
            UploadInfo ui = new UploadInfo();

        }

        private void sendMes()
        {
            sendCode();
        }
    }
}
