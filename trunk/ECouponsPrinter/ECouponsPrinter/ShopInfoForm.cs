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
    public partial class ShopInfoForm : Form
    {
        private Form parent;
        private String path = System.Windows.Forms.Application.StartupPath;
        private Bitmap SourceBitmap;
        private Bitmap MyBitmap;
        private static int CountDownNumber = 5;
        private string _stringScrollText = "欢迎使用本系统";
        private BackgroundWorker _workerScrollText = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };

        public ShopInfoForm()
        {
            InitializeComponent();
            InitializeScrollTextWorker();

            this.parent = GlobalVariables.Get_HomePage();
        }

        private void ShopInfoForm_Load(object sender, EventArgs e)
        {
            //最大化窗口
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //加载走马灯
            this.Label_ScrollText.Text = _stringScrollText;
            _workerScrollText.RunWorkerAsync();

            //加载计时器
            this.Timer_Countdown.Enabled = true;
            
        }


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

        #region 商家详细"走马灯"实现

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

        #region "首页"按钮事件处理

        private void HomePage_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_HomePage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\商家详细\\首页_1.jpg");
        }

        private void HomePage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_HomePage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\商家详细\\首页.jpg");
            this.Hide();

            if (GlobalVariables.Get_HomePage() != null)
                GlobalVariables.Get_HomePage().Show();
            else
            {
                MainForm mf = new MainForm();
                GlobalVariables.Set_Homepage(mf);
                mf.Show();
            }  
        }

        #endregion

        #region "商家"按钮事件

        private void Button_ShopPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_ShopPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\商家详细\\商家.jpg");
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
            this.Button_ShopPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\商家详细\\商家_1.jpg");
        }

        #endregion

        #region "优惠劵"按钮事件

        private void Button_CouponsPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_CouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\商家详细\\优惠券.jpg");
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
            this.Button_CouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\商家详细\\优惠券_1.jpg");
        }

        #endregion

        #region "我的专区"按钮事件

        private void Button_MyInfoPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_MyInfoPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\商家详细\\我的专区.jpg");
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
            this.Button_MyInfoPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\商家详细\\我的专区_1.jpg");
        }

        #endregion

        #region 优惠劵"上一页"按钮事件

        private void Button_LastCouponsPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_LastCouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\商家详细\\上一页(小).jpg");
        }

        private void Button_LastCouponsPage_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_LastCouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\商家详细\\上一页(小)_1.jpg");
        }

        #endregion

        #region 优惠劵"后一页"按钮事件

        private void Button_NextCouponsPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_NextCouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\商家详细\\后一页(小).jpg");
        }

        private void Button_NextCouponsPage_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_NextCouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\商家详细\\后一页(小)_1.jpg");
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _workerScrollText.CancelAsync();
        }

    }
}
