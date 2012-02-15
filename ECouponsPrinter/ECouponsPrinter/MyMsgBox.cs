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
    public partial class MyMsgBox : Form
    {
        public MyMsgBox()
        {
            InitializeComponent();          
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            this.closetimer.Stop();
            this.closetimer.Dispose();
            this.Close();
        }

        public void ShowMsg(string text, int count)
        {
            closetimer.Enabled = true;
            closetimer.Interval = count * 1000;
            msg.Text = text; 
            closetimer.Start();
            ShowDialog();
        }
    }
}
