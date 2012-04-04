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
using System.IO;

namespace ECouponsPrinter
{
    public partial class CouponsPopForm : Form
    {
        private String path = System.Windows.Forms.Application.StartupPath;
        private CouponPicInfo pi;
        PrintDocument pd = new PrintDocument();
        Wait wait;
        string MD5code = "";
        string Intro, Instruction, bottomText;
        private MainFrame Frame;

        public CouponsPopForm(CouponPicInfo info, MainFrame myFrame)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Panel_Background.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_Background, true, null);
            this.Location = new Point(10, 10);

            this.pi = info;
            this.Frame = myFrame;
            this.Label_Top.Text = GlobalVariables.StrPhone;
            pd.BeginPrint += new PrintEventHandler(pd_BeginPrint);
            pd.EndPrint += new PrintEventHandler(pd_EndPrint);
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.DocumentName = "coupon";
        }

        #region 关闭优惠劵弹出窗口

        private void Button_Close_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_Close.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券详细弹出\\关闭_1.jpg");
            Frame.InitUserQuitTime();
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
            Frame.InitUserQuitTime();
        }

        private void Button_Print_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_Print.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券详细弹出\\打印.jpg");
            Info info;

            if (pi.flaPrice != 0)
            {
                info = new Info(pi.flaPrice, pi.name, Frame);
                if (info.ShowDialog() == DialogResult.Yes)
                {
                    check check = new check(pi.flaPrice, pi.id, Frame);
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
                info = new Info(pi.flaPrice, pi.name, Frame);

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
            AccessCmd cmd = new AccessCmd(); ;
            OleDbDataReader reader;
            string strSql = "";

            if (pi.pPath.CompareTo(path + "\\coupon\\null.jpg") == 0)
            {
                strSql = "select * from t_bz_shop where strId='" + pi.shopId + "'";
                reader = cmd.ExecuteReader(strSql);
                try
                {
                    FileStream pFileStream;
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(8))
                        {
                            pFileStream = new FileStream(path + "\\shop\\" + reader.GetString(8), FileMode.Open, FileAccess.Read);

                        }
                        else
                        {
                            pFileStream = new FileStream(path + "\\shop\\null.jpg", FileMode.Open, FileAccess.Read);
                        }
                    }
                    else
                    {
                        pFileStream = new FileStream(path + "\\shop\\null.jpg", FileMode.Open, FileAccess.Read);
                    }
                    this.PB_Couponpop.Image = new Bitmap(Image.FromStream(pFileStream), 150, 100);
                    pFileStream.Close();
                    pFileStream.Dispose();

                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }
            else
            {
                FileStream pFileStream = new FileStream(pi.pPath, FileMode.Open, FileAccess.Read);
                this.PB_Couponpop.Image = new Bitmap(Image.FromStream(pFileStream), 150, 100);
                pFileStream.Close();
                pFileStream.Dispose();
            }

            strSql = "select * from t_bz_coupon where strId='" + pi.id + "'";
            reader = cmd.ExecuteReader(strSql);
            try
            {
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
                    if (!reader.IsDBNull(2))
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
            }
            catch (Exception)
            {
                reader.Close();
                cmd.Close();
            }

            Label_Into.Text = Intro;
            Label_Instruction.Text = Instruction;
            Label_phone.Text = bottomText + "\n" + GlobalVariables.StrPhone;

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
            pd.PrinterSettings.DefaultPageSettings.Margins.Left = 5;
            pd.PrinterSettings.DefaultPageSettings.Margins.Right = 5;

        }

        public void pd_EndPrint(Object sender, PrintEventArgs e)
        {
            //   printimage.Dispose();
            pd.Dispose();
        }

        private bool IsUnicode(char word)
        {
            //if ((word >= 0x4e00) && (word <= 0x9fbb))
            //    return true;
            //else if ((word >= 0xff00) && (word <= 0xffef))
            //    return true;
            //else if ((word >= 0x3000) && (word <= 0x303f))
            //    return true;
            //else
            //    return false;

            if ((word >= 0x00) && (word <= 0x7f))
                return false;
            else
                return true;
        }

        private void pd_PrintPage(Object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Brushes.Black);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            //float pi = (float)3.78;
            //double bi = (1000 / 425.00) * 58 * pi;
            //float width = (float)58 * pi;
            //float height = (float)bi;

            //        MessageBox.Show(pd.PrinterSettings.PaperSources.Count.ToString()); ;
            pd.DefaultPageSettings.PaperSize = new PaperSize("paper", 220, 625);        //修改pagesize的大小    

            //    e.Graphics.DrawImage(printimage, new RectangleF(0, 0, width, height));
            g.DrawString("终端：" + GlobalVariables.StrTerminalNo, new Font("黑体", 9), Brushes.Black, 5, 0);
            g.DrawString("卡号：" + GlobalVariables.LoginUserId, new Font("黑体", 9), Brushes.Black, 5, 15);
            g.DrawString("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), new Font("黑体", 9), Brushes.Black, 5, 30);

            g.DrawImage(this.PB_Couponpop.Image, new RectangleF(50, 50, 120, 90));
            //g.DrawImage(,,,GraphicsUnit.Pixel);

            int line = 0, y = 150;
            string perStr = "";
            int count = 0;
            foreach (char word in Intro)
            {
                if (count >= 4)
                {
                    break;
                }

                if (IsUnicode(word))
                    line += 2;
                else
                    line += 1;

                if (word != '\n')
                    perStr += word.ToString();

                if (line >= 15)
                {
                    g.DrawString(perStr, new Font("黑体", 16), Brushes.Black, 8, y);
                    count++;
                    y += 24;
                    line = 0;
                    perStr = "";
                    continue;
                }
            }

            if (count < 4)
            {
                if (perStr != "")
                {
                    g.DrawString(perStr, new Font("黑体", 16), Brushes.Black, 8, y);
                    count++;
                    y += 24;
                    line = 0;
                    perStr = "";
                }

                for (int i = count; i < 4; i++)
                {
                    y += 24;
                    line = 0;
                    perStr = "";
                }
            }

            y += 15;
            g.DrawLine(p, new Point(0, y), new Point(220, y));
            y += 15;
            count = 0;
            foreach (char word in Instruction)
            {
                if (count >= 14)
                    break;

                if (word == '\n' && perStr != "")
                {
                    g.DrawString(perStr, new Font("黑体", 10), Brushes.Black, 8, y);
                    count++;
                    y += 17;
                    line = 0;
                    perStr = "";
                    continue;
                }

                if (IsUnicode(word))
                    line += 2;
                else
                    line += 1;

                if (word != '\n')
                    perStr += word.ToString();

                if (line >= 25)
                {
                    g.DrawString(perStr, new Font("黑体", 10), Brushes.Black, 8, y);
                    count++;
                    y += 17;
                    line = 0;
                    perStr = "";
                    continue;
                }
            }

            if (count < 14)
            {
                if (perStr != "")
                {
                    g.DrawString(perStr, new Font("黑体", 10), Brushes.Black, 8, y);
                    count++;
                    y += 17;
                    line = 0;
                    perStr = "";
                }
                for (int i = count; i < 14; i++)
                {
                    y += 17;
                    line = 0;
                    perStr = "";
                }
            }

            if (pi.flaPrice != 0)
            {
                y += 10;
                g.DrawLine(p, new Point(0, y), new Point(220, y));
                y += 5;
                g.DrawString(Code.Text, new Font("黑体", 13), Brushes.Black, 16, y);
                y += 55;
                g.DrawLine(p, new Point(0, y), new Point(220, y));
            }
            else
            {
                g.DrawLine(p, new Point(0, 580), new Point(220, 585));
            }

            g.DrawString(bottomText, new Font("黑体", 13), Brushes.Black, 15, 590);
            g.DrawString(GlobalVariables.StrPhone, new Font("黑体", 12), Brushes.Black, 30, 615);


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
                    mb.ShowMsg("打印纸已用尽！暂停服务！", 3);
                    wait.CloseScrollBar();
                    return;
                }

                myprinter = pq.GetAllPrinterQueues();
                if (0 == myprinter[defaultPrinterName])
                {
                    if (!pd.PrinterSettings.IsValid)
                    {
                        MyMsgBox mb = new MyMsgBox();
                        mb.ShowMsg("打印机不可用！暂停服务！", 3);
                        wait.CloseScrollBar();
                        return;
                    }
                    pd.Print();
                    myprinter = pq.GetAllPrinterQueues();
                    if (myprinter[defaultPrinterName] == 0)
                    {
                        MyMsgBox mb = new MyMsgBox();
                        mb.ShowMsg("打印纸已用尽！暂停服务！", 3);
                        wait.CloseScrollBar();
                        return;
                    }

                    for (int i = 0; i <= 60; i += 1)
                    {
                        wait.SetProgressBarPositionP(i);//设置进度条当前位置
                        System.Threading.Thread.Sleep(100);//sleep一下减缓进度条进度，实际代码中，此处应该是实际的工作
                    }

                    myprinter = pq.GetAllPrinterQueues();
                    if (myprinter[defaultPrinterName] == 0)
                    {
                        string tempId = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //   long tempId = DateTime.Now.Ticks;
                        if (MD5code == "")
                        {
                            MD5code = "00000000";
                        }
                        string strSql = "insert into t_bz_coupon_print values('" + tempId + "','" + GlobalVariables.LoginUserId + "','" + pi.id + "',#" + DateTime.Now.ToString("yyyy-M-d H:m:s") + "#,'" + MD5code + "')";
                        AccessCmd cmd = new AccessCmd();
                        //    MessageBox.Show(strSql);
                        cmd.ExecuteNonQuery(strSql);
                        strSql = "update t_bz_print_total set intPrintTotal=intPrintTotal+1";
                        cmd.ExecuteNonQuery(strSql);
                        strSql = "select * from t_bz_print_total";
                        OleDbDataReader reader = cmd.ExecuteReader(strSql);
                        int printNum = 0;
                        if (reader.Read())
                        {
                            printNum = reader.GetInt32(0);
                        }

                        if (printNum >= GlobalVariables.IntCouponPrint)
                        {
                            UploadInfo ui = new UploadInfo();
                            ui.PrintAlert(printNum);
                        }
                        reader.Close();
                        cmd.Close();

                        for (int i = 60; i <= 100; i += 1)
                        {
                            wait.SetProgressBarPositionP(i);//设置进度条当前位置
                            System.Threading.Thread.Sleep(50);//sleep一下减缓进度条进度，实际代码中，此处应该是实际的工作
                        }
                    }
                    else
                    {
                        MyMsgBox mb = new MyMsgBox();
                        mb.ShowMsg("打印纸已用尽！暂停服务！", 3);
                        wait.CloseScrollBar();
                        return;
                    }
                }
                else
                {
                    MyMsgBox mb = new MyMsgBox();
                    mb.ShowMsg("打印纸已用尽！暂停服务！", 3);
                    wait.CloseScrollBar();
                    return;
                }
            }
            catch (Exception e)
            {
                ErrorLog.log(e);
                wait.CloseScrollBar();
                return;
            }
        }
    }
}
