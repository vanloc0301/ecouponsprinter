using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Data.OleDb;

namespace ECouponsPrinter
{
    public partial class MainFrame : Form
    {
        private String path = System.Windows.Forms.Application.StartupPath;  //应用程序当前路径
        private static int CountDownNumber = GlobalVariables.WindowWaitTime;
        private string _stringScrollText = GlobalVariables.MarqueeText;

        private List<PicInfo> LP_shop;
        private List<PicInfo> LP_coupon;
        private List<PicInfo>[] LP_type;

        private static int curType = 0;                         //指示主页当前显示的类别
        private static int count, curPage, totalPage, curPageShowCount;
        private static int tPage1, tPage2, tPage3;
        private static int cPage1, cPage2;
        private static int theCouponNum;

        Member m = null;

        enum part { up = 1, middle = 2, bottom = 3 };

        public MainFrame()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Panel_Shop.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_Shop, true, null);
            this.Panel_Coupons.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_Coupons, true, null);
            this.Panel_ShopInfo.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_ShopInfo, true, null);
            this.Panel_MyInfo.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_MyInfo, true, null);
            this.Panel_Home.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_Home, true, null);
            //设置定时刷新时钟
            this.Timer_DownloadInfo.Stop();
            this.Timer_DownloadInfo.Interval = GlobalVariables.IntRefreshSec * 1000;
       //     this.Timer_DownloadInfo.Start();
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

            if (curType > 0)
            {
                curType--;
                ShowHomeTopPicure();
            }
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

            int length = LP_shop.Count;

            if (curType < length - 1)
            {
                curType++;
                ShowHomeTopPicure();
            }

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

            InitShopInfoData(LP_shop[curType].name);
            Thread.Sleep(20);

            this.Panel_ShopInfo.Visible = true;
            ShowShopInfo();

            //            this.Panel_ShopInfo.BringToFront();
            //            this.Panel_ShopInfo.SendToBack();
        }

        #endregion

        #endregion

        #region 商家详细


        #endregion

        #region 商家二级

        private void PB_Shop_Type_Page_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            switch (pb.Name)
            {
                case "PB_Shop_Type1_LastPage":
                case "PB_Shop_Type2_LastPage":
                case "PB_Shop_Type3_LastPage":
                    pb.Image = Image.FromFile(path + "\\images\\切图\\商家二级\\L1.jpg");
                    return;
                case "PB_Shop_Type1_NextPage":
                case "PB_Shop_Type2_NextPage":
                case "PB_Shop_Type3_NextPage":
                    pb.Image = Image.FromFile(path + "\\images\\切图\\商家二级\\N1.jpg");
                    return;

            }
        }

        private void PB_Shop_Type_Page_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            int tPage = 0;

            int iCount = 0;
            switch (pb.Name)
            {
                case "PB_Shop_Type1_LastPage":
                    pb.Image = Image.FromFile(path + "\\images\\切图\\商家二级\\L.jpg");
                    if (tPage1 > 1)
                    {
                        tPage1--;
                        ShowShopPart((int)part.up);
                    }
                    return;
                case "PB_Shop_Type2_LastPage":
                    pb.Image = Image.FromFile(path + "\\images\\切图\\商家二级\\L.jpg");
                    if (tPage2 > 1)
                    {
                        tPage2--;
                        ShowShopPart((int)part.middle);
                    }
                    return;
                case "PB_Shop_Type3_LastPage":
                    pb.Image = Image.FromFile(path + "\\images\\切图\\商家二级\\L.jpg");
                    if (tPage3 > 1)
                    {
                        tPage3--;
                        ShowShopPart((int)part.bottom);
                    }
                    return;
                case "PB_Shop_Type1_NextPage":
                    pb.Image = Image.FromFile(path + "\\images\\切图\\商家二级\\N.jpg");
                    iCount = LP_type[0].Count;
                    tPage = iCount / 12 + (iCount % 12 == 0 ? 0 : 1);

                    if (tPage1 < tPage)
                    {
                        tPage1++;
                        ShowShopPart((int)part.up);
                    }
                    return;
                case "PB_Shop_Type2_NextPage":
                    pb.Image = Image.FromFile(path + "\\images\\切图\\商家二级\\N.jpg");
                    iCount = LP_type[1].Count;
                    tPage = iCount / 12 + (iCount % 12 == 0 ? 0 : 1);
                    if (tPage2 < tPage)
                    {
                        tPage2++;
                        ShowShopPart((int)part.middle);
                    }
                    return;
                case "PB_Shop_Type3_NextPage":
                    pb.Image = Image.FromFile(path + "\\images\\切图\\商家二级\\N.jpg");
                    iCount = LP_type[2].Count;
                    tPage = iCount / 12 + (iCount % 12 == 0 ? 0 : 1);
                    if (tPage3 < tPage)
                    {
                        tPage3++;
                        ShowShopPart((int)part.bottom);
                    }
                    return;
            }
        }

        #endregion

        #region 我的专区

        #region "上一页"和"下一页"事件处理
        private void Button_Coupon_LastPage_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\我的专区\\last.jpg");

            String name = btn.Name;
            switch (name)
            {
                case "Button_LastDocument":
                    if (cPage1 > 1)
                    {
                        cPage1--;
                        ShowMyInfoPart((int)part.up);
                    }
                    return;
                case "Button_LastConsumption":
                    if (cPage2 > 1)
                    {
                        cPage2--;
                        ShowMyInfoPart((int)part.bottom);
                    }
                    return;
            }
        }

        private void Button_Coupon_LastPage_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\我的专区\\last_1.jpg");
        }

        private void Button_Coupon_NextPage_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\我的专区\\next.jpg");

            String name = btn.Name;
            int pageAll = 1;
            switch (name)
            {
                case "Button_NextDocument":
                    pageAll = LP_type[0].Count / 6 + (LP_type[0].Count % 6 == 0 ? 0 : 1);
                    if (cPage1 < pageAll)
                    {
                        cPage1++;
                        ShowMyInfoPart((int)part.up);
                    }
                    return;
                case "Button_NextConsumption":
                    pageAll = LP_type[1].Count / 6 + (LP_type[0].Count % 6 == 0 ? 0 : 1);
                    if (cPage2 < pageAll)
                    {
                        cPage2++;
                        ShowMyInfoPart((int)part.bottom);
                    }
                    return;
            }
        }

        private void Button_Coupon_NextPage_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = Image.FromFile(path + "\\images\\切图\\我的专区\\next_1.jpg");

        }

        #endregion

        #endregion

        #region 优惠劵二级

        #region "左箭头"和"右箭头"事件处理
        private void Button_CouponsLeft_MouseDown(object sender, MouseEventArgs e)
        {
            PB_Coupon_Left.Image = Image.FromFile(path + "\\images\\切图\\优惠券二级\\left_1.jpg");
        }

        private void Button_CouponsLeft_MouseUp(object sender, MouseEventArgs e)
        {
            PB_Coupon_Left.Image = Image.FromFile(path + "\\images\\切图\\优惠券二级\\left.jpg");

            if (curPage > 1)
            {
                curPage--;
                ShowCoupon();
            }
        }

        private void Button_CouponsRight_MouseDown(object sender, MouseEventArgs e)
        {
            this.PB_Coupon_Right.Image = Image.FromFile(path + "\\images\\切图\\优惠券二级\\right_1.jpg");
        }

        private void Button_CouponsRight_MouseUp(object sender, MouseEventArgs e)
        {
            this.PB_Coupon_Right.Image = Image.FromFile(path + "\\images\\切图\\优惠券二级\\right.jpg");

            if (curPage < totalPage)
            {
                curPage++;
                ShowCoupon();
            }
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

            String trade = btn.Name.Substring(15, 1);

            InitCouponData(trade);
            ShowCoupon();
        }

        #endregion

        #endregion

        #endregion

        #region 主框架

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

            this.InitHomeData();
            this.Panel_Home.Visible = true;
            this.ShowHome();
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

            //取消不必要的按钮
            this.Button_LastCouponsPage.Visible = false;
            this.Button_NextCouponsPage.Visible = false;

            //切换
            int y = this.VerticalScroll.Value;
            this.Panel_Shop.Location = new System.Drawing.Point(0, 142 - y);

            //准备工作
            this.UnVisibleAllPanels();
            this.InitTimer();

            this.Panel_Shop.Visible = true;

            InitShopData();
            ShowShop();
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



            //Form3 f = new Form3();
            //f.TopLevel = false;
            //f.Opacity = 0;
            //Panel_MyInfo.Controls.Clear();
            //Panel_MyInfo.Controls.Add(f);
            //f.Show();
            //Thread.Sleep(1000);
            //f.Opacity = 100;

            //切换
            int y = this.VerticalScroll.Value;
            this.Panel_Coupons.Location = new System.Drawing.Point(0, 142 - y);

            InitCouponData("1");
            Thread.Sleep(20);
            //准备工作
            this.UnVisibleAllPanels();
            this.InitTimer();

            //取消不必要的按钮
            this.Button_LastCouponsPage.Visible = false;
            this.Button_NextCouponsPage.Visible = false;
            this.Panel_Coupons.Visible = true;
            ShowCoupon();

            //         Thread.Sleep(100);
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

            m = GlobalVariables.testM;

            //准备工作
            this.UnVisibleAllPanels();
            this.InitTimer();

            //取消不必要的按钮
            this.Button_LastCouponsPage.Visible = false;
            this.Button_NextCouponsPage.Visible = false;

            //切换
            int y = this.VerticalScroll.Value;
            this.Panel_MyInfo.Location = new System.Drawing.Point(0, 142 - y);

            InitMyInfoData();

            this.Panel_MyInfo.Visible = true;
            ShowMyInfo();


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

            if (curPage > 1)
            {
                curPage--;
                this.ShowBottomPicure();
            }
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

            if (curPage < totalPage)
            {
                curPage++;
                this.ShowBottomPicure();
            }
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
            this.UnVisibleAllPanels();
            InitData();
            InitHomeData();
            this.Panel_Home.Visible = true;
            ShowHome();

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
            this.Panel_MyInfo.Visible = false;
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

        #region 数据处理和图片绑定

        #region 加载所有小图信息

        /// <summary>
        /// 预加载所有的图片信息
        /// </summary>
        private void InitData()
        {
            try
            {
                //读取数据库
                string strSql = "select * from t_bz_shop";
                AccessCmd cmd = new AccessCmd();
                OleDbDataReader reader = cmd.ExecuteReader(strSql);
                LP_shop = new List<PicInfo>();

                String lPath, sPath, name, id, trade, shopid;

                while (reader.Read())
                {
                    PicInfo pi = new PicInfo();

                    lPath = reader.GetString(9);
                    if (lPath != "" && lPath != null)
                    {
                        pi.lpath = path + "\\shop\\" + lPath;
                    }

                    sPath = reader.GetString(8);
                    if (sPath != "" && sPath != null)
                    {
                        pi.spath = path + "\\shop\\" + sPath;
                        pi.image = new Bitmap(Image.FromFile(pi.spath), 168, 195);
                    }

                    name = reader.GetString(1);
                    if (name != "" && name != null)
                    {
                        pi.name = name;
                    }

                    id = reader.GetString(0);
                    if (id != "" && id != null)
                    {
                        pi.id = id;
                    }

                    trade = reader.GetString(3);
                    if (trade != "" && trade != null)
                    {
                        pi.trade = trade;
                    }

                    LP_shop.Add(pi);
                }

                strSql = "select * from t_bz_coupon order by strShopId asc";
                reader = cmd.ExecuteReader(strSql);
                LP_coupon = new List<PicInfo>();

                while (reader.Read())
                {
                    PicInfo pi = new PicInfo();

                    if (!reader.IsDBNull(9))
                    {
                        lPath = reader.GetString(9);
                        if (lPath != "" && lPath != null)
                        {
                            pi.lpath = path + "\\coupon\\" + lPath;
                        }
                    }

                    if (!reader.IsDBNull(8))
                    {
                        sPath = reader.GetString(8);
                        if (sPath != "" && sPath != null)
                        {
                            pi.spath = path + "\\coupon\\" + sPath;
                            pi.image = new Bitmap(Image.FromFile(pi.spath), 168, 175);
                        }
                    }

                    if (!reader.IsDBNull(1))
                    {
                        name = reader.GetString(1);
                        if (name != "" && name != null)
                        {
                            pi.name = name;
                        }
                    }

                    if (!reader.IsDBNull(0))
                    {
                        id = reader.GetString(0);
                        if (id != "" && id != null)
                        {
                            pi.id = id;
                        }
                    }

                    if (!reader.IsDBNull(4))
                    {
                        shopid = reader.GetString(4);
                        if (shopid != "" && shopid != null)
                        {
                            pi.shopid = shopid;

                            strSql = "select strTrade from t_bz_shop where strId='" + shopid + "'";
                            OleDbDataReader oddr = cmd.ExecuteReader(strSql);

                            if (oddr.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    pi.trade = oddr.GetString(0);
                                }
                            }
                            oddr.Close();
                        }
                    }

                    LP_coupon.Add(pi);
                }
                reader.Close();
                //加载参数
                strSql = "select * from t_bz_terminal_param";
                reader = cmd.ExecuteReader(strSql);
                while (reader.Read())
                {
                    string strParamName = reader.GetString(1);
                    if (strParamName.Equals("strExitPwd"))
                        GlobalVariables.StrExitPwd = reader.GetString(2);
                    else if (strParamName.Equals("intMemberSec"))
                        GlobalVariables.UserWaitTime = Int16.Parse(reader.GetString(2));
                    else if (strParamName.Equals("intRefreshSec"))
                        GlobalVariables.IntRefreshSec = Int16.Parse(reader.GetString(2));
                    else if (strParamName.Equals("strPhone"))
                        GlobalVariables.StrPhone = reader.GetString(2);
                    else if (strParamName.Equals("intAdSec"))
                        GlobalVariables.WindowWaitTime = Int16.Parse(reader.GetString(2));
                    else if (strParamName.Equals("intAdImg"))
                        GlobalVariables.IntAdImg = Int16.Parse(reader.GetString(2));
                    else if (strParamName.Equals("intHistory"))
                        GlobalVariables.IntHistory = Int16.Parse(reader.GetString(2));
                    else if (strParamName.Equals("intCouponPrint"))
                        GlobalVariables.IntCouponPrint = Int16.Parse(reader.GetString(2));
                    else if (strParamName.Equals("strTerminalNo"))
                        GlobalVariables.StrTerminalNo = reader.GetString(2);
                    else if (strParamName.Equals("strServerUrl"))
                        GlobalVariables.StrServerUrl = reader.GetString(2);
                }
                cmd.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion

        #region 首页数据处理

        /// <summary>
        /// 加载首页的数据
        /// </summary>
        private void InitHomeData()
        {
            //暂时不需要做任何事  
            curPage = 1;
            curType = 0;
            theCouponNum = 0;
        }

        /// <summary>
        /// 将加载好的图片显示在首页上
        /// </summary>
        private void ShowHome()
        {
            curPage = 1;
            curType = 0;
            ShowHomeTopPicure();
            ShowBottomPicure();
        }

        /// <summary>
        /// 显示首页顶端的商家图片和商家名称
        /// </summary>
        private void ShowHomeTopPicure()
        {
            //    MessageBox.Show(curType.ToString());  

            Label_ShopName.Text = LP_shop[curType].name;

            if (LP_shop.Count > 0)
            {
                PB_Home_Up.Image = new Bitmap(Image.FromFile(LP_shop[curType].lpath), 1071, 540);
            }
        }

        /// <summary>
        /// 处理首页点击小优惠劵处理事件, 即显示所点击优惠劵的大图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeHomePicture(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            String name = pb.Name;
            //   MessageBox.Show(name.Length.ToString());
            String numstr = "1";
            int num = 1;

            if (name.Length == 15)
            {
                numstr = name.Substring(14, 1);
                num = (numstr[0] - '0');
            }

            if (name.Length == 16)
            {
                numstr = name.Substring(14, 2);
                num = (numstr[0] - '0') * 10 + (numstr[1] - '0');
            }

            theCouponNum = num - 1;
            //      MessageBox.Show(num.ToString());

            PB_Home_Down.Image.Dispose();

            PB_Home_Down.Image = new Bitmap(Image.FromFile(LP_coupon[num - 1 + (curPage - 1) * 12].lpath), 1071, 548);

        }

        #endregion

        #region 商家详细数据处理

        /// <summary>
        /// 加载商家详细页面的数据
        /// </summary>
        private void InitShopInfoData(String shopname)
        {
            string strSql = "select * from t_bz_shop where strBizName ='" + shopname + "'";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);

            String lPath, name, address, info, id = null;

            LP_type = new List<PicInfo>[1];

            LP_type[0] = new List<PicInfo>();

            if (reader.Read())
            {
                lPath = reader.GetString(9);
                if (lPath != "" && lPath != null)
                {
                    PB_ShopInfo_Shop.Image = new Bitmap(Image.FromFile(path + "\\shop\\" + lPath), 1070, 559);
                }

                name = reader.GetString(1);
                if (name != "" && name != null)
                {
                    Label_ShopInfo_Name.Text = "商家名称: " + name;
                }

                id = reader.GetString(0);

                address = reader.GetString(4);
                if (address != "" && address != null)
                {
                    Label_ShopInfo_Address.Text = "地址: " + address;
                }

                info = reader.GetString(7);
                if (info != "" && info != null)
                {
                    Label_ShopInfo_Detail.Text = "简介: " + info;
                }
            }

            LP_type[0] = FindCouponByShopId(id);

            reader.Close();
            cmd.Close();

            curPage = 1;
            curType = 0;
            theCouponNum = 0;
        }

        /// <summary>
        /// 将数据显示到商家详细页面上
        /// </summary>
        private void ShowShopInfo()
        {
            ShowBottomPicure();
        }

        /// <summary>
        /// 处理商家详细页面点击小优惠劵处理事件, 即显示所点击优惠劵的大图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeShopInfoPicture(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            String name = pb.Name;
            //   MessageBox.Show(name.Length.ToString());
            String numstr = "1";
            int num = 1;

            numstr = name.Substring(20, 1);
            num = (numstr[0] - '0');

            theCouponNum = num - 1;

            //      MessageBox.Show(num.ToString());

            PB_ShopInfo_Coupons.Image.Dispose();

            PB_ShopInfo_Coupons.Image = new Bitmap(Image.FromFile(LP_coupon[num - 1 + (curPage - 1) * 6].lpath), 1070, 573);
        }

        #endregion

        #region 商家二级数据处理

        /// <summary>
        /// 加载商家页面的数据
        /// </summary>
        private void InitShopData()
        {
            if (LP_type != null)
            {
                for (int i = 0; i < LP_type.Length; i++)
                {
                    LP_type[i].Clear();
                }
            }

            LP_type = new List<PicInfo>[3];
            LP_type[0] = FindShopByTrade("1");
            LP_type[1] = FindShopByTrade("2");
            LP_type[2] = FindShopByTrade("3");

            tPage1 = 1;
            tPage2 = 1;
            tPage3 = 1;

            #region 暂时不用的代码
            //    try{
            //    //读取数据库
            //    string strSql = "select * from t_bz_shop where strTrade='1'";
            //    AccessCmd cmd = new AccessCmd();
            //    OleDbDataReader reader = cmd.ExecuteReader(strSql);
            //    LP_shoptype1 = new List<PicInfo>();
            //    Label_Shop_Type1.Text = "1";
            //    Label_Shop_Type2.Text = "2";
            //    Label_Shop_Type3.Text = "3";
            //    String sPath, name;

            //    while (reader.Read())
            //    {
            //        PicInfo pi = new PicInfo();

            //        sPath = reader.GetString(8);
            //        if (sPath != "" && sPath != null)
            //        {
            //            pi.spath = path + "\\shop\\" + sPath;
            //        }

            //        name = reader.GetString(1);
            //        if (name != "" && name != null)
            //        {
            //            pi.name = name;
            //        }
            //        LP_shoptype1.Add(pi);      
            //    }

            //    strSql = "select * from t_bz_shop where strTrade='2'";
            //    reader = cmd.ExecuteReader(strSql);
            //    LP_shoptype2 = new List<PicInfo>();

            //    while (reader.Read())
            //    {
            //        PicInfo pi = new PicInfo();

            //        sPath = reader.GetString(8);
            //        if (sPath != "" && sPath != null)
            //        {
            //            pi.spath = path + "\\shop\\" + sPath;
            //        }

            //        name = reader.GetString(1);
            //        if (name != "" && name != null)
            //        {
            //            pi.name = name;
            //        }

            //        LP_shoptype2.Add(pi);
            //    }

            //    strSql = "select * from t_bz_shop where strTrade='3'";
            //    reader = cmd.ExecuteReader(strSql);
            //    LP_shoptype3 = new List<PicInfo>();

            //    while (reader.Read())
            //    {
            //        PicInfo pi = new PicInfo();

            //        sPath = reader.GetString(8);
            //        if (sPath != "" && sPath != null)
            //        {
            //            pi.spath = path + "\\shop\\" + sPath;
            //        }

            //        name = reader.GetString(1);
            //        if (name != "" && name != null)
            //        {
            //            pi.name = name;
            //        }

            //        LP_shoptype3.Add(pi);
            //    }

            //    reader.Close();
            //    cmd.Close();

            //    tPage1 = 1;
            //    tPage2 = 1;
            //    tPage3 = 1;
            //}
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.Message);
            //    }
            #endregion

        }

        /// <summary>
        /// 更新商家页面
        /// </summary>
        private void ShowShop()
        {
            ShowShopPart((int)part.up);         //显示商家页面的上面部分
            ShowShopPart((int)part.middle);     //显示商家页面的中间部分
            ShowShopPart((int)part.bottom);         //显示商家页面的下面部分
        }

        /// <summary>
        /// 更新商家页面的某一部分
        /// </summary>
        /// <param name="type">指示要更新的部分,1、2、3分别代表上、中、下三个部分</param>
        private void ShowShopPart(int type)
        {
            String controlName = "";        //显示小优惠劵的控件的Name
            Panel container = Panel_Shop;         //现在正在操作的Panel容器的对象
            int perNum = 12;                 //每页显示小优惠劵的控件的数量
            List<PicInfo> lp = null;

            if (type == 1)
            {
                controlName = "PB_Shop_type1_";
                lp = LP_type[0];
                curPage = tPage1;
            }
            else if (type == 2)
            {
                controlName = "PB_Shop_type2_";
                lp = LP_type[1];
                curPage = tPage2;
            }
            else
            {
                controlName = "PB_Shop_type3_";
                lp = LP_type[2];
                curPage = tPage3;
            }

            count = lp.Count;
            totalPage = count / perNum + (count % perNum == 0 ? 0 : 1);
            //    MessageBox.Show(totalPage.ToString());
            if (curPage == totalPage)
            {
                curPageShowCount = count % (perNum + 1);
            }
            else
                curPageShowCount = perNum;

            if (curPageShowCount > 0)
                for (int i = 0; i < perNum; i++)
                {
                    String name = controlName + (i + 1);
                    PictureBox temp = null;

                    if ((temp = (PictureBox)GetControl(name, container)) != null)
                    {
                        if ((i) < (curPageShowCount))
                        {
                            if (temp.Visible.CompareTo(true) != 0)
                                temp.Visible = true;
                        }
                        else
                        {
                            //     MessageBox.Show(temp.Visible.ToString());

                            if (temp.Visible.CompareTo(false) != 0)
                                temp.Visible = false;
                        }
                    }
                }

            for (int i = 0; i < curPageShowCount; i++)
            {
                String name = controlName + (i + 1);
                PictureBox temp = null;
                if ((temp = (PictureBox)GetControl(name, container)) != null)
                {
                    temp.Image = lp[i + perNum * (curPage - 1)].image;
                }
            }
        }

        private void ChangeShopPic(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            String name = pb.Name;
            //   MessageBox.Show(name.Length.ToString());
            String numstr = "1";
            int num = 1;

            if (name.Length == 15)
            {
                numstr = name.Substring(14, 1);
                num = (numstr[0] - '0');
            }

            if (name.Length == 16)
            {
                numstr = name.Substring(14, 2);
                num = (numstr[0] - '0') * 10 + (numstr[1] - '0');
            }

            //      MessageBox.Show(num.ToString());

            int curNumType = name.Substring(12, 1)[0] - '0';

            this.UnVisibleAllPanels();
            this.InitTimer();

            int y = this.VerticalScroll.Value;
            this.Panel_ShopInfo.Location = new System.Drawing.Point(0, 142 - y);

            InitShopInfoData(LP_type[curNumType - 1][num + (tPage1 - 1) * 12 - 1].name);
            Thread.Sleep(20);

            this.Panel_ShopInfo.Visible = true;
            ShowShopInfo();
        }

        #endregion

        #region 优惠劵数据处理

        /// <summary>
        /// 加载商家页面的数据
        /// </summary>
        private void InitCouponData(String trade)
        {
            if (LP_type != null)
            {
                for (int i = 0; i < LP_type.Length; i++)
                {
                    LP_type[i].Clear();
                }
            }

            LP_type = new List<PicInfo>[1];
            LP_type[0] = FindCouponByTrade(trade);

            curPage = 1;
            curType = 0;
            theCouponNum = 0;
        }

        /// <summary>
        /// 更新优惠劵页面
        /// </summary>
        private void ShowCoupon()
        {
            String controlName = "PB_Coupon_";        //显示小优惠劵的控件的Name
            Panel container = Panel_Coupons;         //现在正在操作的Panel容器的对象
            int perNum = 12;                        //每页显示小优惠劵的控件的数量
            List<PicInfo> lp = LP_type[curType];

            count = lp.Count;
            totalPage = count / perNum + (count % perNum == 0 ? 0 : 1);
            //    MessageBox.Show(totalPage.ToString());
            if (curPage == totalPage)
            {
                curPageShowCount = count % (perNum + 1);
            }
            else
                curPageShowCount = perNum;

            PB_Coupon_Top.Image = new Bitmap(Image.FromFile(LP_type[curType][(curPage - 1) * 12].lpath), 1070, 609);

            if (curPageShowCount > 0)
                for (int i = 0; i < perNum; i++)
                {
                    String name = controlName + (i + 1);
                    PictureBox temp = null;

                    if ((temp = (PictureBox)GetControl(name, container)) != null)
                    {
                        if ((i) < (curPageShowCount))
                        {
                            if (temp.Visible.CompareTo(true) != 0)
                                temp.Visible = true;
                        }
                        else
                        {
                            //     MessageBox.Show(temp.Visible.ToString());

                            if (temp.Visible.CompareTo(false) != 0)
                                temp.Visible = false;
                        }
                    }
                }


            for (int i = 0; i < curPageShowCount; i++)
            {
                String name = controlName + (i + 1);
                PictureBox temp = null;
                if ((temp = (PictureBox)GetControl(name, container)) != null)
                {
                    temp.Image = new Bitmap(lp[i + perNum * (curPage - 1)].image,195,211);
                }
            }
        }

        private void ChangeCouponPicture(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;


            String name = pb.Name;
            String numstr = null;
            int num = 1;

            if (name.Length == 11)
            {
                numstr = name.Substring(10, 1);
                num = (numstr[0] - '0');
            }

            if (name.Length == 12)
            {
                numstr = name.Substring(10, 2);
                num = (numstr[0] - '0') * 10 + (numstr[1] - '0');
            }

            theCouponNum = num - 1;

            PB_Coupon_Top.Image.Dispose();

            PB_Coupon_Top.Image = new Bitmap(Image.FromFile(LP_type[curType][(curPage - 1) * 12 + num - 1].lpath), 1070, 609);

        }

        #endregion

        #region 我的专区数据处理
        private void InitMyInfoData()
        {
            if (m == null)
            {
                MessageBox.Show("下载用户信息失败！");
                return;
            }

            if (LP_type != null)
            {
                for (int i = 0; i < LP_type.Length; i++)
                {
                    LP_type[i].Clear();
                }
            }

            LP_type = new List<PicInfo>[2];

            LP_type[0] = FindCouponById(m.AryFavourite);
            LP_type[1] = FindCouponById(m.AryHistory);

            cPage1 = 1;
            cPage2 = 1;
            theCouponNum = 0;
        }

        /// <summary>
        /// 显示我的专区页面
        /// </summary>
        private void ShowMyInfo()
        {
            ShowMyInfoPart((int)part.up);         //显示页面的上面部分
            ShowMyInfoPart((int)part.bottom);     //显示页面的下间部分
        }

        /// <summary>
        /// 更新我的专区页面的某一部分
        /// </summary>
        /// <param name="type">指示要更新的部分,1、2、3分别代表上、中、下三个部分</param>
        private void ShowMyInfoPart(int type)
        {
            String controlName = "";        //显示小优惠劵的控件的Name
            Panel container = Panel_MyInfo;         //现在正在操作的Panel容器的对象
            int perNum = 6;                 //每页显示小优惠劵的控件的数量
            List<PicInfo> lp = null;

            if (type == 1)
            {
                controlName = "PB_MyInfo_Fav";
                lp = LP_type[0];
                curPage = cPage1;
                PB_MyInfo_Fav.Image = new Bitmap(Image.FromFile(lp[(curPage - 1) * 6].lpath), 1033, 515);
            }
            else
            {
                controlName = "PB_MyInfo_His";
                lp = LP_type[1];
                curPage = cPage2;
                PB_MyInfo_His.Image = new Bitmap(Image.FromFile(lp[(curPage - 1) * 6].lpath), 1033, 515);
            }

            count = lp.Count;
            totalPage = count / perNum + (count % perNum == 0 ? 0 : 1);
            //    MessageBox.Show(totalPage.ToString());
            if (curPage == totalPage)
            {
                curPageShowCount = count % (perNum + 1);
            }
            else
                curPageShowCount = perNum;

            if (curPageShowCount > 0)
                for (int i = 0; i < perNum; i++)
                {
                    String name = controlName + (i + 1);
                    PictureBox temp = null;

                    if ((temp = (PictureBox)GetControl(name, container)) != null)
                    {
                        if ((i) < (curPageShowCount))
                        {
                            if (temp.Visible.CompareTo(true) != 0)
                                temp.Visible = true;
                        }
                        else
                        {
                            //     MessageBox.Show(temp.Visible.ToString());
                            if (temp.Visible.CompareTo(false) != 0)
                                temp.Visible = false;
                        }
                    }
                }

            

            for (int i = 0; i < curPageShowCount; i++)
            {
                String name = controlName + (i + 1);
                PictureBox temp = null;
                if ((temp = (PictureBox)GetControl(name, container)) != null)
                {
                    temp.Image = lp[i + perNum * (curPage - 1)].image;
                }
            }
        }

        private void ChangeMyInfoPic(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            String type = pb.Name.Substring(10, 3);
            String numstr = pb.Name.Substring(13, 1);
            int num = numstr[0] - '0';

            theCouponNum = num - 1;

            if (type == "Fav")
            {
                PB_MyInfo_Fav.Image = new Bitmap(Image.FromFile(LP_type[0][num - 1 + (cPage1 - 1) * 6].lpath), 1033, 515);
            }
            else
            {
                PB_MyInfo_His.Image = new Bitmap(Image.FromFile(LP_type[1][num - 1 + (cPage2 - 1) * 6].lpath), 1033, 515);
            }

        }

        #endregion

        #region 优惠卷弹出页面数据处理
        private PicInfo InitCouponPopData(String strid)
        {
            //读取数据库
            string strSql = "select * from t_bz_coupon where strId='"+strid+"'";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);

            String pPath = null, flaPrice = null;
            PicInfo pi = new PicInfo();
            pi.id = strid;

            if (reader.Read())
            {
                pPath = reader.GetString(10);
                if (pPath != "" && pPath != null)
                {
                    pi.lpath = path + "\\coupon\\" + pPath;
                }

                flaPrice = reader.GetString(7);
                if (flaPrice != "" && flaPrice != null)
                {
                    pi.flaPrice = flaPrice;
                }                    
            }
            return pi;
        }
        #endregion


        #region 一些公用的函数

        /// <summary>
        /// 显示下端的优惠卷, 包括一张大图, 12张小图, 只由首页和商家信息页面可以使用
        /// </summary>
        private void ShowBottomPicure()
        {
            String controlName = "";        //显示小优惠劵的控件的Name
            Panel container = null;         //现在正在操作的Panel容器的对象
            int perNum = 0;                 //每页显示小优惠劵的控件的数量
            List<PicInfo> LP_temp = null;

            if (this.Panel_ShopInfo.Visible == true)
            {
                controlName = "PB_ShopInfo_Coupons0";
                container = Panel_ShopInfo;
                perNum = 6;
                LP_temp = LP_type[0];
            }
            else
            {
                controlName = "PB_Home_Bottom";
                container = Panel_Home;
                perNum = 12;
                LP_temp = LP_coupon;
            }

            count = LP_temp.Count;
            totalPage = count / perNum + (count % perNum == 0 ? 0 : 1);
            //    MessageBox.Show(totalPage.ToString());
            if (curPage == totalPage)
            {
                curPageShowCount = count % (perNum + 1);
            }
            else
                curPageShowCount = perNum;

            if (curPageShowCount > 0)
                for (int i = 0; i < perNum; i++)
                {
                    String name = controlName + (i + 1);
                    PictureBox temp = null;

                    if ((temp = (PictureBox)GetControl(name, container)) != null)
                    {
                        if ((i) < (curPageShowCount))
                        {
                            if (temp.Visible.CompareTo(true) != 0)
                                temp.Visible = true;
                        }
                        else
                        {
                            //     MessageBox.Show(temp.Visible.ToString());

                            if (temp.Visible.CompareTo(false) != 0)
                                temp.Visible = false;
                        }
                    }
                }

            if (container == Panel_Home)
                PB_Home_Down.Image = new Bitmap(Image.FromFile(LP_temp[(curPage - 1) * 12].lpath), 1071, 548);

            if (container == Panel_ShopInfo)
                PB_ShopInfo_Coupons.Image = new Bitmap(Image.FromFile(LP_temp[(curPage - 1) * 6].lpath), 1070, 573);

            for (int i = 0; i < curPageShowCount; i++)
            {
                String name = controlName + (i + 1);
                PictureBox temp = null;
                if ((temp = (PictureBox)GetControl(name, container)) != null)
                {
                    temp.Image = LP_temp[i + perNum * (curPage - 1)].image;
                }
            }
        }

        /// <summary>
        /// 获取指定Panel指定名称的控件
        /// </summary>
        /// <param name="ControlName">控件的Name</param>
        /// <param name="container">指定的Panel对象</param>
        /// <returns></returns>
        private Control GetControl(String ControlName, Panel container)
        {
            foreach (Control item in container.Controls)
            {
                if (item.Name == ControlName)
                    return item;
            }
            return null;
        }

        /// <summary>
        /// 按商店来划分优惠劵
        /// </summary>
        /// <param name="shopId">商店id</param>
        /// <returns></returns>
        private List<PicInfo> FindCouponByShopId(String shopId)
        {
            List<PicInfo> temp = new List<PicInfo>();

            foreach (PicInfo pi in LP_coupon)
            {
                if (pi.shopid == shopId)
                {
                    temp.Add(pi);
                }
            }

            return temp;
        }

        /// <summary>
        /// 按商家类别来划分商家
        /// </summary>
        /// <param name="trade">商家类别</param>
        /// <returns></returns>
        private List<PicInfo> FindShopByTrade(String trade)
        {
            List<PicInfo> temp = new List<PicInfo>();

            foreach (PicInfo pi in LP_shop)
            {
                if (pi.trade == trade)
                {
                    temp.Add(pi);
                }
            }
            return temp;
        }

        /// <summary>
        /// 按商家类别来划分优惠劵
        /// </summary>
        /// <param name="trade">商家类别</param>
        /// <returns></returns>
        private List<PicInfo> FindCouponByTrade(String trade)
        {
            List<PicInfo> temp = new List<PicInfo>();

            foreach (PicInfo pi in LP_coupon)
            {
                if (pi.trade == trade)
                {
                    temp.Add(pi);
                }
            }
            return temp;
        }

        /// <summary>
        /// 按优惠劵id来划分优惠劵
        /// </summary>
        /// <param name="id">优惠劵id</param>
        /// <returns></returns>
        private List<PicInfo> FindCouponById(String [] id)
        {
            List<PicInfo> temp = new List<PicInfo>();

            String [] couponId = id;

            foreach (String tempId in couponId)
            {
                foreach (PicInfo pi in LP_coupon)
                {
                    if (pi.id == tempId)
                    {
                        temp.Add(pi);
                    }
                }
            }
            return temp;
        }

        /// <summary>
        /// 点击优惠劵大图弹出的优惠劵打印页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PB_Coupons_Click(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            String id = null;

            switch (pb.Name)
            {
                case "PB_Home_Down":
                    id = LP_coupon[(curPage - 1) * 6 + theCouponNum].id;
                    return;
                case "PB_ShopInfo_Coupons":
                case "PB_Coupon_Top":
                case "PB_MyInfo_Fav":
                    id = LP_type[0][(curPage - 1) * 6 + theCouponNum].id;
                    break;
                case "PB_MyInfo_His":
                    id = LP_type[1][(curPage - 1) * 6 + theCouponNum].id;
                    break;
                default: break;
            }

            PicInfo pi = null;

            if (id != null)
            {
                pi = InitCouponPopData(id);
            }
            else
                return;

            CouponsPopForm cpf = new CouponsPopForm(pi);
            cpf.ShowDialog();
            Thread.Sleep(200);

        }

        #endregion

        private void Label_Countdown_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        #endregion
      

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        


        private void Timer_DownloadInfo_Tick(object sender, EventArgs e)
        {
            this.Timer_DownloadInfo.Stop();
       //     MessageBox.Show("begin download info");
            try
            {
                //下载信息
                DownloadInfo di = new DownloadInfo();
                di.download();
                di.SynParam();
                //同步数据
                this.InitData();
                this.Timer_DownloadInfo.Interval = GlobalVariables.IntRefreshSec * 1000;
       //         MessageBox.Show("下载信息成功");
            }
            catch (Exception ep)
            {
                ErrorLog.log(ep);
            }
            this.Timer_DownloadInfo.Start();
        }

    }
}
