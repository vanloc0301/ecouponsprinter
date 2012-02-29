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
using System.Drawing.Printing;
using System.Threading;
using System.Data.OleDb;
using Text;

namespace ECouponsPrinter
{
    public partial class CouponsPopForm : Form
    {
        private String path = System.Windows.Forms.Application.StartupPath;
        private CouponPicInfo pi;
        PrintDocument pd = new PrintDocument();
        Image printimage = null;
        Wait wait;
        string MD5code;
        string Intro, Instruction, bottomText;

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
            pd.DocumentName = "coupon";
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
                            new System.Threading.Thread(DoWork).Start();
                            Thread.Sleep(500);
                            wait.ShowDialog();
                        }
                        catch (Exception e1)
                        {
                            ErrorLog.log(e1);
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
                        new System.Threading.Thread(DoWork).Start();
                        wait.ShowDialog();
                    }
                    catch (Exception e1)
                    {
                        ErrorLog.log(e1);
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

            this.PB_Couponpop.Image = new Bitmap(pi.image, 110, 70);

            string strSql = "select * from t_bz_coupon where strId=" + pi.id;
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);

            if (reader.Read())
            {
                if (!reader.IsDBNull(11))
                {
                    Intro = reader.GetString(11);
                }
                else
                {
                    Intro = "暂无介绍";
                }

                if (!reader.IsDBNull(12))
                {
                    Instruction = reader.GetString(12);
                }
                else
                {
                    Instruction = "暂无说明";
                }
            }
            reader.Close();

            strSql = "select * from t_bz_terminal_param where strParamName='strPrintBottom'";
            reader = cmd.ExecuteReader(strSql);

            if (reader.Read())
            {
                if (reader.IsDBNull(2))
                {
                    bottomText = reader.GetString(2);
                }
                else
                {
                    bottomText = "";
                }
            }

            reader.Close();
            cmd.Close();

            if (pi.flaPrice != 0)
            {
                MD5code = this.ReturnCode();
                this.Code.Text = "验证码：" + MD5code;
            }
        }

        public String ReturnCode()
        {

            String MD5str = "";
            DateTime localtime = DateTime.Now;
            String time = localtime.ToString("yyyyMMddhhmmss", DateTimeFormatInfo.InvariantInfo);

            MD5str += GlobalVariables.LoginUserId + pi.id + time;
            String pwdCode = StrToMD5(MD5str);
            //          MessageBox.Show(pwdCode);

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
            //Rectangle start = this.Bounds;
            //Rectangle rect = new Rectangle(start.Left + 6, start.Top + 95, 425, 597);

            //printimage = new Bitmap(rect.Width, rect.Height);
            //Graphics g = Graphics.FromImage(printimage);

            //g.CopyFromScreen(new Point(rect.Left, rect.Top), new Point(0, 0), new Size(rect.Width, rect.Height));

        }

        public void pd_EndPrint(Object sender, PrintEventArgs e)
        {
            printimage.Dispose();
            pd.Dispose();
        }

        private void pd_PrintPage(Object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            //float pi = (float)3.78;
            //double bi = (1000 / 425.00) * 58 * pi;
            //float width = (float)58 * pi;
            //float height = (float)bi;

            pd.DefaultPageSettings.PaperSize.Height = 450;//您可以修改pagesize的大小               
            pd.DefaultPageSettings.PaperSize.Width = 220;

            //    e.Graphics.DrawImage(printimage, new RectangleF(0, 0, width, height));

            g.DrawString("鑫九天公司鑫九天公司鑫九天公司", new Font("宋体", 20), Brushes.Black, 10, 20);
            g.DrawString("123456778668", new Font("宋体", 20), Brushes.Black, 10, 50);
            g.DrawString("123456778668", new Font("宋体", 20), Brushes.Black, 10, 80);
            g.DrawString("鑫九天公司", new Font("宋体", 20), Brushes.Black, 10, 110);
            g.DrawString("123456778668", new Font("宋体", 20), Brushes.Black, 10, 140);
            g.DrawString("123456778668", new Font("宋体", 20), Brushes.Black, 10, 170);
            g.DrawString("鑫九天公司", new Font("宋体", 20), Brushes.Black, 10, 200);
            g.DrawString("123456778668", new Font("宋体", 20), Brushes.Black, 10, 230);
            g.DrawString("123456778668", new Font("宋体", 20), Brushes.Black, 10, 260);
            g.DrawString("2010-11-12 13:13:13", new Font("宋体", 10), Brushes.Black, 10, 300);

            //     e.Graphics.DrawImage(printimage, new RectangleF(0, 0, width, height));
        }

        private void DoWork()
        {
            try
            {
                printQueue pq = new printQueue();
                Dictionary<string, int> myprinter;
                string defaultPrinterName = Printer.GetDeaultPrinterName();
                //       MessageBox.Show(defaultPrinterName);

                if (pq.CanelAllPrintJob() == false)
                {
                    MyMsgBox mb = new MyMsgBox();
                    mb.ShowMsg("打印纸已用尽！打印机暂停服务1！", 1);
                    wait.CloseScrollBar();
                    return;
                }

                myprinter = pq.GetAllPrinterQueues();
                if (0 == myprinter[defaultPrinterName])
                {
                    pd.Print();
                    myprinter = pq.GetAllPrinterQueues();
                    if (myprinter[defaultPrinterName] == 0)
                    {
                        MyMsgBox mb = new MyMsgBox();
                        mb.ShowMsg("打印纸已用尽！打印机暂停服务2！", 1);
                        wait.CloseScrollBar();
                        return;
                    }

                    for (int i = 0; i <= 70; i += 1)
                    {
                        wait.SetProgressBarPositionP(i);//设置进度条当前位置
                        System.Threading.Thread.Sleep(100);//sleep一下减缓进度条进度，实际代码中，此处应该是实际的工作
                    }

                    myprinter = pq.GetAllPrinterQueues();
                    if (myprinter[defaultPrinterName] == 0)
                    {
                        int tempId = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")).Milliseconds;
                        //   long tempId = DateTime.Now.Ticks;

                        string strSql = "insert into t_bz_coupon_print values(" + tempId + "," + GlobalVariables.LoginUserId + "," + pi.id + "," + DateTime.Now.ToString("yyyy-M-d H:m:s") + "," + MD5code + ")";
                        AccessCmd cmd = new AccessCmd();
                        cmd.ExecuteNonQuery(strSql);

                        cmd.Close();
                    }
                }
                else
                {
                    MyMsgBox mb = new MyMsgBox();
                    mb.ShowMsg("打印纸已用尽！打印机暂停服务3！", 1);
                    wait.CloseScrollBar();
                    return;
                }
            }
            catch (Exception e)
            {
                MyMsgBox mb = new MyMsgBox();
                mb.ShowMsg("打印出错！暂时停止服务\n" + e.Message + "\n" + e.StackTrace, 5);
                wait.CloseScrollBar();
                return;
            }

            for (int i = 70; i <= 100; i += 1)
            {
                wait.SetProgressBarPositionP(i);//设置进度条当前位置
                System.Threading.Thread.Sleep(50);//sleep一下减缓进度条进度，实际代码中，此处应该是实际的工作
            }
        }
    }
}
