using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Globalization;

namespace ECouponsPrinter
{
    public partial class CouponsPopForm : Form
    {
        private String path = System.Windows.Forms.Application.StartupPath;
        private PicInfo pi;

        public CouponsPopForm(PicInfo info)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.pi = info;
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
        }

        private void CouponPop_OnLoad(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Height = 780;
          
            this.PB_Couponpop.Image = Image.FromFile(pi.lpath);
            this.Code.Text = "验证码：" + this.ReturnCode();
        }

        public String ReturnCode()
        {
            
            String MD5str = "";
            DateTime localtime = DateTime.Now;
            String time = localtime.ToString("yyyyMMddhhmmss",DateTimeFormatInfo.InvariantInfo);

            MD5str += GlobalVariables.LoginUserId + pi.id + time;
            String pwdCode = StrToMD5(MD5str);
            MessageBox.Show(pwdCode);

            return (pwdCode.Substring(0, 4) + pwdCode.Substring(pwdCode.Length - 4, 4));

        }

        public static string StrToMD5(string str)
        {
            byte[] data = Encoding.GetEncoding("GBK").GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] OutBytes = md5.ComputeHash(data);

            string OutString = "";
            for (int i = 0; i < OutBytes.Length; i++)
            {
                OutString += OutBytes[i].ToString("x2");
            }
            return OutString.ToUpper();
        }

        public String ModStr(String str)
        {
            if (str.Length == 1)
            {
                return ("0" + str);
            }
            else
            { 
                return str;
            }
        }
    }
}
