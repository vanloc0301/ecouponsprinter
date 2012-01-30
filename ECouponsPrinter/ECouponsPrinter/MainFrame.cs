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
    public partial class MainFrame : Form
    {
        private String path = System.Windows.Forms.Application.StartupPath;
        private static int CountDownNumber = GlobalVariables.WindowWaitTime;
        private string _stringScrollText = GlobalVariables.MarqueeText;

        public MainFrame()
        {          
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.Panel_Shop.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |System.Reflection.BindingFlags.NonPublic).SetValue(Panel_Shop, true, null);
            this.Panel_Coupons.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_Coupons, true, null);
            this.Panel_ShopInfo.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_ShopInfo, true, null);
            this.Panel_MyInfo.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_MyInfo, true, null);
            this.Panel_Home.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_Home, true, null);

        }

        #region 主要

        #region 首页

        #region 商家"上一页"按钮事件

        private void Button_LastShop_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_LastShop.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\上一页_1.jpg");
        }

        private void Button_LastShop_MouseUp(object sender, MouseEventArgs e)
        {

            this.Button_LastShop.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\上一页.jpg");
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


            this.UnVisibleAllPanels();
            this.InitTimer();  
     
            int y = this.VerticalScroll.Value;
            this.Panel_ShopInfo.Location = new System.Drawing.Point(0, 142 - y);
            this.Panel_ShopInfo.Visible = true;
                   
//            this.Panel_ShopInfo.BringToFront();
//            this.Panel_ShopInfo.SendToBack();
        }

        #endregion

        #endregion

        #region 商家详细


        #endregion

        #region 商家二级


        #endregion

        #region 我的专区

        #region "上一页"和"下一页"事件处理
        private void Button_LastDocument_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\我的专区\\last_1.jpg");
        }

        private void Button_LastDocument_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\我的专区\\last.jpg");
        }

        private void Button_NextDocument_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\我的专区\\next_1.jpg");
        }

        private void Button_NextDocument_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\我的专区\\next.jpg");
        }


        #endregion

        #endregion

        #region 优惠劵二级

        #region "左箭头"和"右箭头"事件处理
        private void Button_CouponsLeft_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_CouponsLeft.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券二级\\left_1.jpg");
        }

        private void Button_CouponsLeft_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_CouponsLeft.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券二级\\left.jpg");
        }

        private void Button_CouponsRight_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_CouponsRight.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券二级\\right_1.jpg");
        }

        private void Button_CouponsRight_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_CouponsRight.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券二级\\right.jpg");
        }
        #endregion

        #region "商家种类"按钮事件处理

        private void Button_ShopType_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券二级\\Shoptype_1.jpg");
        }

        private void Button_ShopType_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券二级\\Shoptype.jpg");
        }

        #endregion

        #endregion

        #endregion

        #region Frame

        #region "首页"按钮事件

        private void Button_HomePage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_HomePage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\首页.jpg");

            //准备工作
            this.UnVisibleAllPanels();
            this.InitTimer();

            //显示隐藏按钮
            this.Button_LastCouponsPage.Visible = true;
            this.Button_NextCouponsPage.Visible = true;

      /*         
            Form1 f = new Form1();
            f.TopLevel = false;
            this.Panel_HomePage.Controls.Clear();
            this.Panel_HomePage.Controls.Add(f);
            this.Panel_HomePage.Show();
               
       *    this.Opacity = 0;
       *    this.Panel_Home.BringToFront();
       *    p.SendToBack();
       */
                //切换
                int y = this.VerticalScroll.Value;
                this.Panel_Home.Location = new System.Drawing.Point(0, 142 - y);
  
                this.Panel_Home.Visible = true;
      //      Thread.Sleep(100);
            
        //    this.Opacity = 100;

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

            //准备工作
            this.UnVisibleAllPanels();
            this.InitTimer();

            //取消不必要的按钮
            this.Button_LastCouponsPage.Visible = false;
            this.Button_NextCouponsPage.Visible = false;

                //切换
                int y = this.VerticalScroll.Value;
                this.Panel_Shop.Location = new System.Drawing.Point(0, 142 - y);
                this.Panel_Shop.Visible = true;
    //        Thread.Sleep(100);
            

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
            
            //准备工作
            this.UnVisibleAllPanels();
            this.InitTimer();

            //取消不必要的按钮
            this.Button_LastCouponsPage.Visible = false;
            this.Button_NextCouponsPage.Visible = false;

                //切换
                int y = this.VerticalScroll.Value;
                this.Panel_Coupons.Location = new System.Drawing.Point(0, 142 - y);

                //        this.Panel_Coupons.SendToBack();
                //        p.BringToFront();
                this.Panel_Coupons.Visible = true;
   //         Thread.Sleep(100);
            
/*
            Form1 f = new Form1();
            f.TopLevel = false;
            this.Panel_Home.Controls.Clear();
            f.Opacity = 0;
            this.Panel_Home.Controls.Add(f);
            this.Panel_Home.Show();
            Thread.Sleep(10);
            f.Opacity = 100;
 * */
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

            //准备工作
            this.UnVisibleAllPanels();
            this.InitTimer();

            //取消不必要的按钮
            this.Button_LastCouponsPage.Visible = false;
            this.Button_NextCouponsPage.Visible = false;

            //切换
            int y = this.VerticalScroll.Value;
            this.Panel_MyInfo.Location = new System.Drawing.Point(0, 142 - y);
            /*  
                   this.Panel_MyInfo.BringToFront();
                   p.SendToBack();
                   Thread.Sleep(100);
            */
            this.Panel_MyInfo.Visible = true;
            
           
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

        #region 隐藏按钮"系统设置"
        private void Show_Option(object sender, EventArgs e)
        {
            Option op = new Option();
            op.Location = new Point(0, 0);
            op.ShowDialog();
        }
        #endregion

        #endregion

        #region 其他

        #region Load事件
        private void MainFrame_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //加载走马灯
            this.Label_ScrollText.strContent = _stringScrollText;
            this.Label_ScrollText.Start();

            //加载计时器
            this.Timer_Countdown.Enabled = true;

            //加载隐藏按钮
            this.Label_Option.BackColor = Color.Transparent;

            //加载主页面
            this.Panel_Home.BringToFront();
        }
        #endregion

        #region 初始化倒计时
        private void InitTimer()
        {
            this.Timer_Countdown.Stop();
            CountDownNumber = 5;
            this.Timer_Countdown.Start();
        }

        #endregion

        #region 初始化所有panel,将它们的visible设置为false
        private Panel UnVisibleAllPanels()
        {
            this.Panel_Home.Visible = false;
            this.Panel_ShopInfo.Visible = false;
            this.Panel_Shop.Visible = false;
            this.Panel_MyInfo.Visible= false;
            this.Panel_Coupons.Visible = false;

            return null;
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

        private void Label_Countdown_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }
 
        #endregion

    }
}
