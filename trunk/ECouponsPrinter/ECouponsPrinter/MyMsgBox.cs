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
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            this.closetimer.Stop();
            this.closetimer.Dispose();
            this.Close();
        }

        public void ShowMsg(string text, int count)
        {
            this.confirm.Visible = false;
            this.cancel.Visible = false;
            this.Size = new Size(this.Size.Width, this.Size.Height - 60);

            closetimer.Enabled = true;
            closetimer.Interval = count * 1000;
            msg.Text = text;
            closetimer.Start();
            ShowDialog();
        }

        public void ShowMsg(string text, char type)
        {
            if (type == '0')
            {
                this.confirm.Visible = false;
                this.cancel.Visible = false;
                this.Size = new Size(this.Size.Width, this.Size.Height - 60);
            }
            else if (type == '1')
            {
                this.confirm.Visible = true;
                this.cancel.Visible = true;
            }
            msg.Text = text;
            ShowDialog();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
