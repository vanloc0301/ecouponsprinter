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
    public partial class MainForm : Form
    {
        private static int num = 10;
        private String path = System.Windows.Forms.Application.StartupPath;
        private Bitmap SourceBitmap;
        private Bitmap MyBitmap;
        private static int CountDownNumber = 5;
        private string _stringScrollText = "欢迎使用本系统";
        private BackgroundWorker _workerScrollText = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };

        public MainForm()
        {
            InitializeComponent();
            InitializeScrollTextWorker();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //加载走马灯
            this.Label_ScrollText.Text = _stringScrollText;
            _workerScrollText.RunWorkerAsync();

            //加载计时器
            this.Timer_Countdown.Enabled = true;
        }

        #region 商家"上一页"按钮事件

        private void Button_LastShop_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_LastShop.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\上一页_1.jpg");
        }

        private void Button_LastShop_MouseUp(object sender, MouseEventArgs e)
        {

            this.Button_LastShop.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\上一页.jpg");

            if (!System.IO.File.Exists(path + "\\images\\(" + (num - 1) + ").jpg"))
                return;

            --num;
            SourceBitmap = new Bitmap(path + "\\images\\(" + num + ").jpg");
            MyBitmap = new Bitmap(SourceBitmap, this.PictureBox_Shop.Width, this.PictureBox_Shop.Height);

            /*       try
                   {
                       int width = this.pictureBox1.Width;     //图像宽度  
                       int height = this.pictureBox1.Height;      //图像高度  
                       Graphics g = this.panel2.CreateGraphics();
                       g.Clear(Color.Gray);
                       Bitmap bitmap = new Bitmap(width, height);

                       int x = 0;
                       while (x <= height / 2)
                       {
                           for (int t = 0; t < 10; t++)
                           {
                               for (int i = 0; i <= width - 1; i++)
                               {
                                   bitmap.SetPixel(i, x, MyBitmap.GetPixel(i, x));
                               }
                               for (int i = 0; i <= width - 1; i++)
                               {
                                   bitmap.SetPixel(i, height - x - 1, MyBitmap.GetPixel(i, height - x - 1));
                               }
                               x++;
                           }           
                           this.panel2.Refresh();
                           g.DrawImage(bitmap, 0, 0);
                           this.pictureBox1.Image = bitmap;
                           System.Threading.Thread.Sleep(1);
                       }
                   }
                   catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message, "错误!");
                   } 
                   */
            if (MyBitmap != null)
                this.PictureBox_Shop.Image = MyBitmap;
        }

        #endregion

        #region 商家"下一页"按钮事件

        private void Button_NextShop_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_NextShop.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\后一页_1.jpg");
        }

        private void Button_NextShop_MouseUp(object sender, MouseEventArgs e)
        {

            this.Button_NextShop.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\后一页.jpg");

            ++num;
            SourceBitmap = new Bitmap(path + "\\images\\(" + num + ").jpg");
            MyBitmap = new Bitmap(SourceBitmap, this.PictureBox_Shop.Width, this.PictureBox_Shop.Height);

       //     Thread th = new Thread(new ThreadStart(Next_Picture));

       //     th.Start();

            
            /*       try
                   {
                       int width = this.pictureBox1.Width;     //图像宽度  
                       int height = this.pictureBox1.Height;      //图像高度  
                       Graphics g = this.panel2.CreateGraphics();
                       g.Clear(Color.Gray);
                       Bitmap bitmap = new Bitmap(width, height);

                       int x = 0;
                       while (x <= height / 2)
                       {
                           for (int t = 0; t < 10; t++)
                           {
                               for (int i = 0; i <= width - 1; i++)
                               {
                                   bitmap.SetPixel(i, x, MyBitmap.GetPixel(i, x));
                               }
                               for (int i = 0; i <= width - 1; i++)
                               {
                                   bitmap.SetPixel(i, height - x - 1, MyBitmap.GetPixel(i, height - x - 1));
                               }
                               x++;
                           }           
                           this.panel2.Refresh();
                           g.DrawImage(bitmap, 0, 0);
                           this.pictureBox1.Image = bitmap;
                           System.Threading.Thread.Sleep(1);
                       }
                   }
                   catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message, "错误!");
                   } 
                   */
            if (MyBitmap != null)
                this.PictureBox_Shop.Image = MyBitmap;
        }

        #endregion

        #region "商家详细"按钮事件

        private void Button_ShopInfo_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_ShopInfo.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\详细_1.jpg");
        }

        private void Button_ShopInfo_MouseUp(object sender, MouseEventArgs e)
        {

            this.Button_ShopInfo.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\详细.jpg");
            this.Hide();

            if (GlobalVariables.Get_ShopInfoPage() != null)
                GlobalVariables.Get_ShopInfoPage().Show();
            else 
            {
                ShopInfoForm sif = new ShopInfoForm();
                GlobalVariables.Set_ShopInfoPage(sif);
                sif.Show();
            }
        }

        #endregion

        #region 计时器事件

        private void Timer_Countdown_Tick(object sender, EventArgs e)
        {
            this.Label_Countdown.Text = CountDownNumber.ToString();

            if (CountDownNumber == 0)
            {
                this.Timer_Countdown.Enabled = false;
            }
            else
            {
                CountDownNumber--;
            }
        }

        #endregion

        #region "首页"按钮事件

        private void Button_HomePage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_HomePage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\首页.jpg");
        }

        private void Button_HomePage_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_HomePage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\首页_1.jpg");
        }

        #endregion

        #region "商家"按钮事件

        private void Button_ShopPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_ShopPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\商家.jpg");
            this.Hide();

            if (GlobalVariables.Get_ShopPage() != null)
                GlobalVariables.Get_ShopPage().Show();
            else
            {
                ShopSecondForm ssf = new ShopSecondForm();
                GlobalVariables.Set_ShopPage(ssf);
                ssf.Show();
            }
        }

        private void Button_ShopPage_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_ShopPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\商家_1.jpg");
        }

        #endregion

        #region "优惠劵"按钮事件

        private void Button_CouponsPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_CouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\优惠券.jpg");
            this.Hide();

            if (GlobalVariables.Get_CouponsPage() != null)
                GlobalVariables.Get_CouponsPage().Show();
            else
            {
                CouponsSecondForm csf = new CouponsSecondForm();
                GlobalVariables.Set_CouponsPage(csf);
                csf.Show();
            }
        }

        private void Button_CouponsPage_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_CouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\优惠券_1.jpg");
           
        }

        #endregion

        #region "我的专区"按钮事件

        private void Button_MyInfoPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_MyInfoPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\我的专区.jpg");
            this.Hide();

            if (GlobalVariables.Get_MyInfoPage() != null)
                GlobalVariables.Get_MyInfoPage().Show();
            else
            {
                MyInfoForm mip = new MyInfoForm();
                GlobalVariables.Set_MyInfoPage(mip);
                mip.Show();
            }
        }

        private void Button_MyInfoPage_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_MyInfoPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\我的专区_1.jpg");
            
        }

        #endregion

        #region 优惠劵"上一页"按钮事件

        private void Button_LastCouponsPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_LastCouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\上一页(小).jpg");
        }

        private void Button_LastCouponsPage_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_LastCouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\上一页(小)_1.jpg");
        }

        #endregion

        #region 优惠劵"后一页"按钮事件

        private void Button_NextCouponsPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_NextCouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\后一页(小).jpg");
        }

        private void Button_NextCouponsPage_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_NextCouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\后一页(小)_1.jpg");
        }

        #endregion

        #region "走马灯"实现

        private void InitializeScrollTextWorker()
        {
            this.Label_ScrollText.BackColor = Color.Transparent;
            _workerScrollText.DoWork += new DoWorkEventHandler(
                            (s, t) =>
                            {
                                int LENGTHOFSCROLLTEXT = _stringScrollText.Length;
                                int shift = 0;

                                while (true)
                                {
                                    if (_workerScrollText.CancellationPending)
                                    {
                                        t.Cancel = true;
                                        return;
                                    }

                                    Thread.Sleep(500);
                                    _workerScrollText.ReportProgress(shift);

                                    shift++;
                                    if (shift > LENGTHOFSCROLLTEXT)
                                        shift = 0;
                                }
                            });

            _workerScrollText.ProgressChanged += new ProgressChangedEventHandler(
                (s, t) =>
                {
                    StringBuilder temp = new StringBuilder();
                    temp.Append(_stringScrollText.Substring(t.ProgressPercentage));
                    temp.Append("    ");
                    temp.Append(_stringScrollText.Substring(0, t.ProgressPercentage));

                    this.Label_ScrollText.Text = temp.ToString();
                });
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _workerScrollText.CancelAsync();
        }     
        
        #region 不要的代码
        public void Invoke_Next_Picture()
        { 
            try
            {
                int width = this.Panel_Shop.Width;     //图像宽度  
                int height = this.Panel_Shop.Height;      //图像高度  
                Graphics g = this.Panel_Shop.CreateGraphics();
                g.Clear(this.BackColor);

                this.Panel_Shop.BackgroundImage = MyBitmap;
                int x = 0;
                while (x < width)
                {
                    g.TranslateTransform(0-x, 0);
                    g.DrawImage(MyBitmap,0,0);
                    x += 5;
                    g.ResetTransform();
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误!");
            }
        }

        private void Next_Picture()
        {
            if (!System.IO.File.Exists(path + "\\images\\(" + (num + 1) + ").jpg"))
                return;

            SourceBitmap = new Bitmap(path + "\\images\\(" + num + ").jpg");
            MyBitmap = new Bitmap(SourceBitmap, this.Panel_Shop.Width, this.Panel_Shop.Height);
            num++;

            SourceBitmap = new Bitmap(path + "\\images\\(" + num + ").jpg");

            MethodInvoker mi = new MethodInvoker(Invoke_Next_Picture);

            this.BeginInvoke(mi);
            

                
          /*      while (grow < width)
                {
                    x = 0;
                    while (x < width - grow)
                    {
                        for (int i = 0; i < height; i++)
                            bitmap.SetPixel(x, i, MyBitmap.GetPixel(x + grow, i));
                        x++;

                    }
                    //      MessageBox.Show(x.ToString());
                    while (x < width)
                    {
                        for (int i = 0; i < height; i++)
                            bitmap.SetPixel(x, i, Next.GetPixel(grow + x - width + 1, i));
                        x++;
                    }
                    //      MessageBox.Show(x.ToString());
                    grow+=10;
                    g.DrawImage(bitmap, 0, 0);
                    Thread.Sleep(1);
                }
            */    
                  //  this.Panel_Shop.Refresh();
                    
            //        this.Timer_ImageAnimate.Enabled = false;
                //    this.PictureBox_Shop.Image = bitmap;

        }
        #endregion

    }
        
}
