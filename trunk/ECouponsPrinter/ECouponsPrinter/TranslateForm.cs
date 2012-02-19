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
    public partial class TranslateForm : Form
    {
        public TranslateForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void TranslateClick(object sender, EventArgs e)
        {
            if (!GlobalVariables.isUserLogin)
            {
                MyMsgBox mb = new MyMsgBox();
                mb.ShowMsg("请您先刷卡！", 1);
                return;
            }
        }

    }
}
