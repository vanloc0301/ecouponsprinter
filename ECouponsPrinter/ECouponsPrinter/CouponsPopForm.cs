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
    public partial class CouponsPopForm : Form
    {
        private String path = System.Windows.Forms.Application.StartupPath;

        public CouponsPopForm()
        {
            InitializeComponent();    

        }

        #region 添加收藏

        private void Button_Document_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_Document.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券详细弹出\\收藏.jpg");
            //添加收藏代码
        }

        private void Button_Document_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_Document.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券详细弹出\\收藏_1.jpg");
        }

        #endregion

        #region 关闭优惠劵弹出窗口

        private void Button_Close_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_Close.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券详细弹出\\关闭_1.jpg");
        }

        private void Button_Close_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_Close.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券详细弹出\\关闭.jpg");

            this.Close();
        }

        #endregion

        private void Button_Print_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_Print.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券详细弹出\\打印_1.jpg");
        }

        private void Button_Print_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_Print.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券详细弹出\\打印.jpg");

      //      this.pageSetupDialog1.Document = this.printDocument1;
     //       this.pageSetupDialog1.ShowDialog();

      //      this.printDialog1.ShowDialog();
        }
    }
}
