﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Globalization;
using System.Drawing.Printing;
using System.Threading;

namespace ECouponsPrinter
{
    public partial class CouponsPopForm : Form
    {
        private String path = System.Windows.Forms.Application.StartupPath;
        private CouponPicInfo pi;
        PrintDocument pd = new PrintDocument();
        Image printimage = null;
        Wait wait;

        public CouponsPopForm(CouponPicInfo info)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Panel_Background.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_Background, true, null);

            this.pi = info;
            pd.BeginPrint += new PrintEventHandler(pd_BeginPrint);
            pd.EndPrint += new PrintEventHandler(pd_EndPrint);
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.DocumentName = "优惠劵";
            this.Height = 780;
        }

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
            Info info;

            if (pi.flaPrice != 0)
            {
                info = new Info(pi.flaPrice, pi.name);
                if (info.ShowDialog() == DialogResult.Yes)
                {
                    check check = new check(pi.flaPrice, pi.id);
                         
                    if (check.ShowDialog() == DialogResult.Yes)
                    {
                        try
                        {              
                            wait = new Wait();
                            new System.Threading.Thread(DoWork).Start();//开启一个线程更新form2的进度条
                            wait.ShowDialog();//现实form2，模式对话框
                        }
                        catch (Exception e1)
                        {
                            
                        }
                    }
                }
            }
            else
            {
                info = new Info(pi.flaPrice, pi.name);

                if (info.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {
                        wait = new Wait();
                        new System.Threading.Thread(DoWork).Start();//开启一个线程更新form2的进度条
                        wait.ShowDialog();//现实form2，模式对话框
                    }
                    catch (Exception e1)
                    {
                  //      MessageBox.Show(e1.Message);
                    }
                }
            }


            Thread.Sleep(200);
            this.Close();
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
            String time = localtime.ToString("yyyyMMddhhmmss", DateTimeFormatInfo.InvariantInfo);

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

        public void pd_BeginPrint(Object sender, PrintEventArgs e)
        {
            Rectangle start = this.Bounds;
            Rectangle rect = new Rectangle(start.Left + 6, start.Top + 95, 425, 597);

            printimage = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(printimage);

            g.CopyFromScreen(new Point(rect.Left, rect.Top), new Point(0, 0), new Size(rect.Width, rect.Height));

        }

        public void pd_EndPrint(Object sender, PrintEventArgs e)
        {
            printimage.Dispose();
            pd.Dispose();
        }

        private void pd_PrintPage(Object sender, PrintPageEventArgs e)
        {
            float pi = (float)3.78;
            double bi = (595.00 / 425.00) * 58 * pi;
            float width = (float)58 * pi;
            float height = (float)bi;

            e.Graphics.DrawImage(printimage, new RectangleF(0, 0, width, height));
        }

        private void DoWork()
        {
            pd.Print();
            UploadInfo ui = new UploadInfo();
            ui.CouponPrint();

            for (int i = 0; i <= 100; i += 1)
            {
                wait.SetProgressBarPositionP(i);//设置进度条当前位置
                System.Threading.Thread.Sleep(50);//sleep一下减缓进度条进度，实际代码中，此处应该是实际的工作
            }
        }
    }
}
