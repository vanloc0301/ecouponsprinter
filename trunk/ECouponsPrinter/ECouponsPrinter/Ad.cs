using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.OleDb;

namespace ECouponsPrinter
{
    public partial class Ad : Form
    {
        Form tran;
        string path = Application.StartupPath;
        public Ad(Form tf)
        {
            InitializeComponent();

            tran = tf;
            this.Panel_Ad.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_Ad, true, null);
        //    this.FormBorderStyle = FormBorderStyle.None;
            
        }

        #region 广告数据处理

        private Bitmap MyBitmap;
        private AxWMPLib.AxWindowsMediaPlayer Ad_MediaPlayer1, Ad_MediaPlayer2;
        Panel Ad_Panel1, Ad_Panel2;
        List<string> Ad_str;
        List<int> Ad_type;
        Thread PicThread = null;
        Thread AdThread = null;
        int speed = 4, showType = 1;
        bool showContinue = true;

        private void RefreshAd()
        {
            string time = DateTime.Now.ToString("H:m:s");
            string strSql = "select * from t_bz_advertisement where #" + time + "#>=dtStartTime And #" + time + "#<dtEndTime And intType=1 or intType=2";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);

            Ad_type = new List<int>();
            Ad_str = new List<string>();

            while (reader.Read())
            {
                Ad_type.Add(reader.GetInt32(2));
                Ad_str.Add(reader.GetString(3));
            }

            reader.Close();
            cmd.Close();

            if (this.Panel_Ad.InvokeRequired)
            {
                this.Panel_Ad.Invoke((MethodInvoker)delegate
                {
                    Panel_Ad.Controls.Clear();
                    ShowAd();
                }, null);
            }
            else
            {
                Panel_Ad.Controls.Clear();
                ShowAd();
            }
            Thread.Sleep(60000 * 5);
        }

        private void Ad_Load(object sender, EventArgs e)
        {
            AdThread = new Thread(new ThreadStart(RefreshAd));
            AdThread.Start();
        }

        private void ShowAd()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            if (Ad_type.Count == 1)
            {
                if (Ad_type[0] == 1)
                {
                    Ad_MediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
                    ((System.ComponentModel.ISupportInitialize)(Ad_MediaPlayer1)).BeginInit();
                    Ad_MediaPlayer1.Enabled = true;
                    Ad_MediaPlayer1.Location = new System.Drawing.Point(13, 49);
                    Ad_MediaPlayer1.Name = "Ad_MediaPlayer1";
                    Ad_MediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Ad_MediaPlayer.OcxState")));
                    Ad_MediaPlayer1.Size = new System.Drawing.Size(745, 440);

                    Panel_Ad.Controls.Add(Ad_MediaPlayer1);
                    ((System.ComponentModel.ISupportInitialize)(Ad_MediaPlayer1)).EndInit();
                    Ad_MediaPlayer1.uiMode = "none";
                    Ad_MediaPlayer1.settings.setMode("loop", true);

                    showType = 1;
                }
                else
                {
                    Ad_Panel1 = new Panel();
                    Ad_Panel1.Location = new System.Drawing.Point(13, 49);
                    Ad_Panel1.Name = "Ad_Panel1";
                    Ad_Panel1.Size = new System.Drawing.Size(745, 440);
                    Panel_Ad.Controls.Add(Ad_Panel1);
                    showType = 2;
                }
            }

            if (Ad_type.Count == 2)
            {
                if (Ad_type[0] == 1)
                {
                    Ad_MediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
                    ((System.ComponentModel.ISupportInitialize)(Ad_MediaPlayer1)).BeginInit();
                    Ad_MediaPlayer1.Enabled = true;
                    Ad_MediaPlayer1.Location = new System.Drawing.Point(13, 49);
                    Ad_MediaPlayer1.Name = "Ad_MediaPlayer1";
                    Ad_MediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Ad_MediaPlayer.OcxState")));
                    Ad_MediaPlayer1.Size = new System.Drawing.Size(745, 440);
                    
                    Panel_Ad.Controls.Add(Ad_MediaPlayer1);
                    ((System.ComponentModel.ISupportInitialize)(Ad_MediaPlayer1)).EndInit();
                    Ad_MediaPlayer1.uiMode = "none";
                    Ad_MediaPlayer1.settings.setMode("loop", true);
                    showType = 1;

                }
                else
                {
                    Ad_Panel1 = new Panel();
                    Ad_Panel1.Location = new System.Drawing.Point(13, 659);
                    Ad_Panel1.Name = "Ad_Panel1";
                    Ad_Panel1.Size = new System.Drawing.Size(745, 440);
                    Panel_Ad.Controls.Add(Ad_Panel1);

                    showType = 2;
                }

                if (Ad_type[1] == 1)
                {
                    int y;
                    if (showType == 1)
                        y = 659;
                    else
                        y = 49;

                    Ad_MediaPlayer2 = new AxWMPLib.AxWindowsMediaPlayer();
                    ((System.ComponentModel.ISupportInitialize)(Ad_MediaPlayer2)).BeginInit();
                    Ad_MediaPlayer2.Enabled = true;
                    Ad_MediaPlayer2.Location = new System.Drawing.Point(13, y);
                    Ad_MediaPlayer2.Name = "Ad_MediaPlayer2";
                    Ad_MediaPlayer2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Ad_MediaPlayer.OcxState")));
                    Ad_MediaPlayer2.Size = new System.Drawing.Size(745, 440);
                    Panel_Ad.Controls.Add(Ad_MediaPlayer2);
                    ((System.ComponentModel.ISupportInitialize)(Ad_MediaPlayer2)).EndInit();
                    Ad_MediaPlayer2.uiMode = "none";
                    Ad_MediaPlayer2.settings.setMode("loop", true);

                    if (showType == 1)
                        showType = 3;
                    else
                        showType = 4;
                }
                else
                {
                    int y;
                    if (showType == 1)
                        y = 659;
                    else
                        y = 49;

                    Ad_Panel2 = new Panel();
                    Ad_Panel2.Location = new System.Drawing.Point(13, y);
                    Ad_Panel2.Name = "Ad_Panel2";
                    Ad_Panel2.Size = new System.Drawing.Size(745, 440);
                    Panel_Ad.Controls.Add(Ad_Panel2);

                    if (showType == 1)
                        showType = 5;
                    else
                        showType = 6;
                }
            }

            if (Ad_str != null && Ad_str.Count != 0)
            {
                showContinue = true;
                PicThread = new Thread(new ThreadStart(doAnimate));
                PicThread.Start();
            }
        }

        private void doAnimate()
        {
            string[] Ad_Picture, Ad_Picture1;
            int i, j, num;
            Random rand;

            switch (showType)
            {
                case 1:
                    Ad_MediaPlayer1.URL = path + @"\ad\" + Ad_str[0];
                    Ad_MediaPlayer1.Ctlcontrols.play();
                    break;
                case 2:
                    Ad_Picture = Ad_str[0].Split(',');
                    MyBitmap = new Bitmap(Image.FromFile(path + "\\ad\\" + Ad_Picture[0]), 745, 440);
                    i = 1;
                    while (showContinue)
                    {
                        rand = new Random();
                        num = rand.Next(0, 100);
                        if (num % 2 == 0)
                        {
                            doAnimateType1(Ad_Panel1);
                        }
                        else
                        {
                            doAnimateType2(Ad_Panel1);
                        }
                        if (i == Ad_Picture.Length - 1)
                        {
                            i = 0;
                        }
                        MyBitmap = new Bitmap(Image.FromFile(path + "\\ad\\" + Ad_Picture[i++]), 745, 440);
                        Thread.Sleep(1000 * 3);
                    }
                    break;
                case 3:
                    Ad_MediaPlayer1.URL = path + @"\ad\" + Ad_str[0];
                    Ad_MediaPlayer1.Ctlcontrols.play();

                    Ad_MediaPlayer2.URL = path + @"\ad\" + Ad_str[1];
                    Ad_MediaPlayer2.Ctlcontrols.play();
                    break;

                case 4:
                    Ad_MediaPlayer2.URL = path + @"\ad\" + Ad_str[1];
                    Ad_MediaPlayer2.Ctlcontrols.play();

                    Ad_Picture = Ad_str[0].Split(',');
                    MyBitmap = new Bitmap(Image.FromFile(path + "\\ad\\" + Ad_Picture[0]), 745, 440);
                    i = 1;
                    while (showContinue)
                    {
                        rand = new Random();
                        num = rand.Next(0, 100);
                        if (num % 2 == 0)
                        {
                            doAnimateType1(Ad_Panel1);
                        }
                        else
                        {
                            doAnimateType2(Ad_Panel1);
                        }
                        if (i == Ad_Picture.Length - 1)
                        {
                            i = 0;
                        }
                        MyBitmap = new Bitmap(Image.FromFile(path + "\\ad\\" + Ad_Picture[i++]), 745, 440);
                        Thread.Sleep(1000 * 3);
                    }

                    break;
                case 5:
                    Ad_MediaPlayer1.URL = path + @"\ad\" + Ad_str[0];
                    Ad_MediaPlayer1.Ctlcontrols.play();

                    Ad_Picture = Ad_str[1].Split(',');
                    MyBitmap = new Bitmap(Image.FromFile(path + "\\ad\\" + Ad_Picture[0]), 745, 440);
                    i = 1;
                    while (showContinue)
                    {
                        rand = new Random();
                        num = rand.Next(0, 100);
                        if (num % 2 == 0)
                        {
                            doAnimateType1(Ad_Panel2);
                        }
                        else
                        {
                            doAnimateType2(Ad_Panel2);
                        }
                        if (i == Ad_Picture.Length - 1)
                        {
                            i = 0;
                        }
                        MyBitmap = new Bitmap(Image.FromFile(path + "\\ad\\" + Ad_Picture[i++]), 745, 440);
                        Thread.Sleep(1000 * 3);
                    }
                    break;
                case 6:
                    Ad_Picture = Ad_str[0].Split(',');
                    MyBitmap = new Bitmap(Image.FromFile(path + "\\ad\\" + Ad_Picture[0]), 745, 440);
                    Ad_Picture1 = Ad_str[1].Split(',');
                    MyBitmap = new Bitmap(Image.FromFile(path + "\\ad\\" + Ad_Picture1[0]), 745, 440);
                    i = j = 1;
                    while (showContinue)
                    {
                        rand = new Random();
                        num = rand.Next(0, 100);
                        if (num % 2 == 0)
                        {
                            doAnimateType1(Ad_Panel1);
                        }
                        else
                        {
                            doAnimateType2(Ad_Panel1);
                        }
                        if (i == Ad_Picture.Length - 1)
                        {
                            i = 0;
                        }
                        MyBitmap = new Bitmap(Image.FromFile(path + "\\ad\\" + Ad_Picture[i++]), 745, 440);
                        Thread.Sleep(1000 * 3);

                        num = rand.Next(0, 100);
                        if (num % 2 == 0)
                        {
                            doAnimateType1(Ad_Panel2);
                        }
                        else
                        {
                            doAnimateType2(Ad_Panel2);
                        }
                        if (j == Ad_Picture1.Length - 1)
                        {
                            j = 0;
                        }
                        MyBitmap = new Bitmap(Image.FromFile(path + "\\ad\\" + Ad_Picture1[j++]), 745, 440);
                        Thread.Sleep(1000 * 3);
                    }
                    break;
                default: return;
            }
        }

        private void doAnimateType1(Panel p)
        {
            Graphics g = null;
            Bitmap bitmapTop = null, bitmapBottom = null;
            try
            {
                int width = p.Width;//图像宽度  
                int height = p.Height;//图像高度  
                g = p.CreateGraphics();

                //g.Clear(Color.Gray);
                bitmapBottom = new Bitmap(width, speed, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                bitmapTop = new Bitmap(width, speed, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                int x = 0, x1 = 0, x2 = height - 1;
                while (x < height)
                {
                    for (int t = 0; t < speed; t++)
                    {
                        for (int i = 0; i <= width - 1; i++)
                        {
                            bitmapTop.SetPixel(i, t, MyBitmap.GetPixel(i, x1));
                        }
                        x1++;
                        x++;
                    }
                    g.DrawImage(bitmapTop, 0, x1 - speed);
                    for (int t = 0; t < speed; t++)
                    {
                        for (int i = 0; i <= width - 1; i++)
                        {
                            bitmapBottom.SetPixel(i, speed - 1 - t, MyBitmap.GetPixel(i, x2));
                        }
                        x2--;
                        x++;
                    }
                    g.DrawImage(bitmapBottom, 0, x2);
                    System.Threading.Thread.Sleep(5);
                }
                //        MessageBox.Show("x1:" + x1.ToString() + ";x2:" + x2.ToString()+";x:"+x);
            }
            catch (Exception ex)
            {
                g.Dispose();
            }
        }

        private void doAnimateType2(Panel p)
        {
            Graphics g = null;
            Bitmap bitmapTop = null, bitmapBottom = null;
            try
            {
                int width = p.Width;//图像宽度  
                int height = p.Height;//图像高度  
                g = p.CreateGraphics();
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //g.Clear(Color.Gray);
                bitmapBottom = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                bitmapTop = new Bitmap(speed, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                int x = 0, x1 = 0, x2 = width - 1;
                while (x < width)
                {
                    for (int t = 0; t < speed; t++)
                    {
                        for (int i = 0; i <= height - 1; i++)
                        {
                            bitmapTop.SetPixel(t, i, MyBitmap.GetPixel(x1, i));
                        }
                        x1++;
                        x++;
                    }
                    g.DrawImage(bitmapTop, x1 - speed, 0);
                    for (int t = 0; t < speed; t++)
                    {
                        for (int i = 0; i <= height - 1; i++)
                        {
                            bitmapBottom.SetPixel(speed - 1 - t, i, MyBitmap.GetPixel(x2, i));
                        }
                        x2--;
                        x++;
                    }
                    g.DrawImage(bitmapBottom, x2, 0);
                    System.Threading.Thread.Sleep(5);
                }
            }
            catch (Exception ex)
            {
                g.Dispose();
            }
        }

        #endregion

        private void Ad_FormClosing(object sender, FormClosingEventArgs e)
        {
            showContinue = false;
            if (Ad_Panel1 != null)
            {
                Ad_Panel1.Dispose();
            }
            if (Ad_Panel2 != null)
            {
                Ad_Panel2.Dispose();
            }
            if (Ad_MediaPlayer1 != null)
            {
                Ad_MediaPlayer1.Dispose();
            }
            if (Ad_MediaPlayer2 != null)
            {
                Ad_MediaPlayer2.Dispose();
            }
            if (PicThread != null)
            {
                try
                {
                    if (PicThread.IsAlive)
                    {
                        PicThread.Abort();
                        PicThread.Join();
                    }
                }
                catch (Exception)
                { }
            }

            if (AdThread != null)
            {
                try
                {
                    if (AdThread.IsAlive)
                    {
                        AdThread.Abort();
                        AdThread.Join();
                    }
                }
                catch (Exception)
                { }
            }
        }
    }
}
