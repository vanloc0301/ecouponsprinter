using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ECouponsPrinter
{
    public partial class CouponsPopForm : Form
    {
        private String path = System.Windows.Forms.Application.StartupPath;
        private String pPath, id;

        public CouponsPopForm(String pPath, String id)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.pPath = pPath;
            this.id = id;
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

        }

        private void CouponPop_OnLoad(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Height = 780;
            MD5Change();

           
            this.PB_Couponpop.Image = Image.FromFile(this.pPath);
        }

        public void MD5Change()
        {
            
            String MD5str = "";
            DateTime localtime = DateTime.Now;
            String year = localtime.Year.ToString();
            String month = ModStr(localtime.Month.ToString());
            String day = ModStr(localtime.Day.ToString());
            String hour = ModStr(localtime.Hour.ToString());
            String minute = ModStr(localtime.Minute.ToString());
            String second = ModStr(localtime.Second.ToString());

            MD5str += year + month + day + hour + minute + second;

            MessageBox.Show(MD5str);

        }

        public static string StrToMD5(string str)
        {
            byte[] data = Encoding.GetEncoding("GB2312").GetBytes(str);
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
