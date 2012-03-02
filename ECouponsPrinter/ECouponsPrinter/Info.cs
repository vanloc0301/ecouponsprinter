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
    public partial class Info : Form
    {
        MainFrame mf;
        public Info(double price, String name, MainFrame frame)
        {
            InitializeComponent();

            this.mf = frame;
            this.labelInfo.Text = "";
            this.labelInfo.Text += "优惠劵名称: " + name + "\n优惠劵价格:" + price + "元\n";

            if (price.Equals(0))
            {
                this.labelInfo.Text += "您要打印的优惠劵是免费卷，不需要支付任何金额，是否继续打印？";
            }
            else
            {
                this.labelInfo.Text += "您要打印的优惠劵是有价卷，需要从您的账户中扣除相应的金额，是否继续打印？";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mf.InitUserQuitTime();
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
