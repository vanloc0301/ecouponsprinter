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
    public partial class Wait : Form
    {
        public Wait()
        {
            InitializeComponent();
            this.Info.Text = "正在打印优惠劵.......";
        }

        public void SetProgressBarPositionP(int currentPosition)
        {
            if (currentPosition < prcBar.Maximum)
            {
                if (currentPosition == 50)
                {
                    if (this.Info.InvokeRequired)
                    {
                        this.Info.Invoke((MethodInvoker)delegate
                        {
                            this.Info.Text = "正在上传您的消费信息.......";
                        }, null);
                    }
                    else
                    {
                        this.Info.Text = "正在上传您的消费信息.......";
                    }
                }

                if (this.prcBar.InvokeRequired)
                {
                    this.prcBar.Invoke((MethodInvoker)delegate
                    {
                        prcBar.Increment(currentPosition - prcBar.Value);
                    }, null);
                }
                else
                {
                    prcBar.Increment(currentPosition - prcBar.Value);
                }
            }
            else
            {
                this.prcBar.Value = prcBar.Maximum;
                Thread.Sleep(1000);
                this.Info.Text = "操作成功！";
                Thread.Sleep(1000);
                this.Close();
            }
        }

        public void CloseScrollBar()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Close();
                }, null);
            }
            else
            {
                this.Close();
            }
        }

    }
}
