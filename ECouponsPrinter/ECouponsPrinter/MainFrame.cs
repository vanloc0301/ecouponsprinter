﻿using System;
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
using System.Runtime.InteropServices;
using Marquee;

namespace ECouponsPrinter
{
    public partial class MainFrame : Form
    {
        private String path = System.Windows.Forms.Application.StartupPath;  //应用程序当前路径
        private static int CountDownNumber = GlobalVariables.WindowWaitTime;
        private string _stringScrollText = GlobalVariables.MarqueeText;
        private Thread th, marquee;

        //-----------------------------------------------------------------------------
        private List<PicInfo> LP_shop;
        private List<CouponPicInfo> LP_coupon;
        private List<PicInfo>[] LP_stype;
        private List<CouponPicInfo>[] LP_ctype;
        private static int curType = 0;                         //指示主页当前显示的类别
        private static int count, curPage, totalPage, curPageShowCount;
        private static int tPage1, tPage2, tPage3;
        private static int cPage1, cPage2;
        private static int theCouponNum, theShopNum;
        private Button[] Btn_Coupon_Type;
        private Label[] Label_Shop_Type;
        private string[] tradeName;

        //-----------------------------------------------------------------------------     
        enum part { up = 1, middle = 2, bottom = 3 };

        public MainFrame()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.Panel_Shop.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_Shop, true, null);
            this.Panel_Coupons.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_Coupons, true, null);
            this.Panel_ShopInfo.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_ShopInfo, true, null);
            this.Panel_MyInfo.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_MyInfo, true, null);
            this.Panel_Home.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_Home, true, null);
            this.Panel_NearShop.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(Panel_NearShop, true, null);
            
            // 根据分辨率作出调整
            //Size size = SystemInformation.PrimaryMonitorSize;
            //int width = size.Width;
            //int height = size.Height;
            //float widthRatio = width / 768F;
            //float heightRatio = height / 1366F;
            //foreach (Control c in Controls)
            //{
            //    ChangeControl(c, widthRatio, heightRatio);
            //}

            //设置定时刷新时钟
            this.Timer_DownloadInfo.Stop();
            this.Timer_DownloadInfo.Interval = GlobalVariables.IntRefreshSec * 1000;
            this.Timer_DownloadInfo.Start();

        }

        //private void ChangeControl(Control c, float widthRatio, float heightRatio)
        //{
        //    c.Width = (int)(c.Width * widthRatio);
        //    c.Height = (int)(c.Height * heightRatio);
        //    c.Location = new Point((int)(c.Location.X * widthRatio), (int)(c.Location.Y * heightRatio));
        //    if (c is Button)
        //    {
        //        if (c.BackgroundImage != null)
        //        {
        //            c.BackgroundImageLayout = ImageLayout.Stretch;
        //        }
        //    }
        //    if (c is Label)
        //    {
        //        c.Font = new Font(c.Font.FontFamily, c.Font.Size * heightRatio, c.Font.Style);
        //        if (c.BackgroundImage != null)
        //        {
        //            c.BackgroundImageLayout = ImageLayout.Stretch;
        //        }
        //    }
        //    if (c.Controls != null)
        //    {
        //        if (c is Panel)
        //        {
        //            if (c.BackgroundImage != null)
        //            {
        //                c.BackgroundImageLayout = ImageLayout.Stretch;
        //            }
        //        }
        //        foreach (Control cc in c.Controls)
        //        {
        //            ChangeControl(cc, widthRatio, heightRatio);
        //        }
        //    }
        //}

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

            int length = LP_stype[0].Count;

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
            this.Panel_ShopInfo.Location = new System.Drawing.Point(0, 95 - y);

            InitShopInfoData(LP_shop[curType].id);
            Thread.Sleep(20);

            this.Panel_ShopInfo.Visible = true;
            ShowShopInfo();
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
                    iCount = LP_stype[0].Count;
                    tPage = iCount / 12 + (iCount % 12 == 0 ? 0 : 1);

                    if (tPage1 < tPage)
                    {
                        tPage1++;
                        ShowShopPart((int)part.up);
                    }
                    return;
                case "PB_Shop_Type2_NextPage":
                    pb.Image = Image.FromFile(path + "\\images\\切图\\商家二级\\N.jpg");
                    if (LP_stype.Length < 2)
                    {
                        return;
                    }

                    if (LP_stype[1].Count == 0)
                    {
                        return;
                    }

                    iCount = LP_stype[1].Count;
                    tPage = iCount / 12 + (iCount % 12 == 0 ? 0 : 1);
                    if (tPage2 < tPage)
                    {
                        tPage2++;
                        ShowShopPart((int)part.middle);
                    }
                    return;
                case "PB_Shop_Type3_NextPage":
                    pb.Image = Image.FromFile(path + "\\images\\切图\\商家二级\\N.jpg");
                    if (LP_stype.Length < 3)
                    {
                        return;
                    }

                    if (LP_stype[2].Count == 0)
                    {
                        return;
                    }

                    iCount = LP_stype[2].Count;
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
                    pageAll = LP_ctype[0].Count / 6 + (LP_ctype[0].Count % 6 == 0 ? 0 : 1);
                    if (cPage1 < pageAll)
                    {
                        cPage1++;
                        ShowMyInfoPart((int)part.up);
                    }
                    return;
                case "Button_NextConsumption":
                    pageAll = LP_ctype[1].Count / 6 + (LP_ctype[0].Count % 6 == 0 ? 0 : 1);
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
            PB_Coupon_Left.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券二级\\left_1.jpg");
        }

        private void Button_CouponsLeft_MouseUp(object sender, MouseEventArgs e)
        {
            PB_Coupon_Left.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券二级\\left.jpg");

            if (curPage > 1)
            {
                curPage--;
                ShowCoupon();
            }
        }

        private void Button_CouponsRight_MouseDown(object sender, MouseEventArgs e)
        {
            this.PB_Coupon_Right.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券二级\\right_1.jpg");
        }

        private void Button_CouponsRight_MouseUp(object sender, MouseEventArgs e)
        {
            this.PB_Coupon_Right.BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券二级\\right.jpg");

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

            String strTradeNum = btn.Name.Substring(15, 1);
            int tradeNum = strTradeNum[0] - '0';

            char type = 'c';
            if (!this.Btn_NewCouponList.Visible && !this.Btn_Rank.Visible && !this.Btn_Rec.Visible)
            {
                type = 'v';
            }

            InitCouponData(GetTradeName()[tradeNum - 1], type);
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
            this.Panel_Home.Location = new System.Drawing.Point(0, 95 - y);

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

            if (LP_shop == null || LP_shop.Count == 0)
            {
                MyMsgBox mb = new MyMsgBox();
                mb.ShowMsg("暂无商家信息", 2);
                return;
            }

            //取消不必要的按钮
            this.Button_LastCouponsPage.Visible = false;
            this.Button_NextCouponsPage.Visible = false;

            //切换
            int y = this.VerticalScroll.Value;
            this.Panel_Shop.Location = new System.Drawing.Point(0, 95 - y);

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

            //切换
            int y = this.VerticalScroll.Value;
            this.Panel_Coupons.Location = new System.Drawing.Point(0, 95 - y);

            if (GetTradeName() == null)
            {
                MyMsgBox mb = new MyMsgBox();
                mb.ShowMsg("暂无优惠劵信息！", 2);
                return;
            }
            InitCouponData(GetTradeName()[0], 'c');
            Thread.Sleep(20);
            //准备工作
            this.UnVisibleAllPanels();
            this.InitTimer();

            //取消不必要的按钮
            this.Button_LastCouponsPage.Visible = false;
            this.Button_NextCouponsPage.Visible = false;

            //显示必要的按钮
            this.Btn_Rank.Visible = true;
            this.Btn_Rec.Visible = true;
            this.Btn_NewCouponList.Visible = true;

            InitCouponButton();
            ShowCouponCommonBtn();
            this.Panel_Coupons.Visible = true;
            ShowCoupon();

            //         Thread.Sleep(100);
        }

        private void InitCouponButton()
        {
            if (Btn_Coupon_Type != null)
            {
                for (int i = 0; i < Btn_Coupon_Type.Length; i++)
                {
                    Panel_Coupons.Controls.Remove(Btn_Coupon_Type[i]);
                    Btn_Coupon_Type[i].Dispose();
                }
                Btn_Coupon_Type = null;
            }
        }

        /// <summary>
        /// 显示优惠劵页面下面的类别按钮
        /// </summary>
        private void ShowCouponCommonBtn()
        {
            string[] tradeName = GetTradeName();
            int tradeNum = tradeName.Length;
            Point[] p = CaculatePosition(tradeNum, 'c');
            int width = p[p.Length - 1].X;
            int height = p[p.Length - 1].Y;
            int curNum = tradeNum / 2 + tradeNum % 2 == 0 ? 0 : 1;
            Btn_Coupon_Type = new Button[tradeNum];
            for (int i = 0; i < tradeNum; i++)
            {
                Btn_Coupon_Type[i] = new Button();
                Btn_Coupon_Type[i].BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券二级\\Shoptype.jpg");
                Btn_Coupon_Type[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                Btn_Coupon_Type[i].FlatAppearance.BorderSize = 0;
                Btn_Coupon_Type[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                Btn_Coupon_Type[i].Location = p[i];
                Btn_Coupon_Type[i].Name = "Btn_Coupon_Type" + (i + 1);
                Btn_Coupon_Type[i].Text = tradeName[i];
                Btn_Coupon_Type[i].Font = new Font("宋体", 30F, FontStyle.Bold);
                Btn_Coupon_Type[i].ForeColor = Color.White;
                Btn_Coupon_Type[i].Size = new System.Drawing.Size(width, height);
                Btn_Coupon_Type[i].UseVisualStyleBackColor = true;
                Btn_Coupon_Type[i].MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_ShopType_MouseDown);
                Btn_Coupon_Type[i].MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_ShopType_MouseUp);
                Panel_Coupons.Controls.Add(Btn_Coupon_Type[i]);
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

            if (!GlobalVariables.isUserLogin)
            {
                MyMsgBox mb = new MyMsgBox();
                mb.ShowMsg("您还未登录！", 2);
                return;
            }

            //准备工作
            this.UnVisibleAllPanels();
            this.InitTimer();

            //取消不必要的按钮
            this.Button_LastCouponsPage.Visible = false;
            this.Button_NextCouponsPage.Visible = false;

            //切换
            int y = this.VerticalScroll.Value;
            this.Panel_MyInfo.Location = new System.Drawing.Point(0, 95 - y);

            InitMyInfoData();

            this.Panel_MyInfo.Visible = true;
            ShowMyInfo();
        }

        private void Button_MyInfoPage_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_MyInfoPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\我的专区_1.jpg");

        }

        #endregion

        #region "周边专区"按钮事件
        private void Button_NearShop_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_NearShop.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\周边商家_1.jpg");
        }

        private void Button_NearShop_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_NearShop.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\周边商家.jpg");

            //切换
            int y = this.VerticalScroll.Value;
            this.Panel_NearShop.Location = new System.Drawing.Point(0, 95 - y);

            if (!InitNearShopData())
            {
                MyMsgBox mb = new MyMsgBox();
                mb.ShowMsg("暂无周边商家信息！", 2);
            }
            Thread.Sleep(20);
            //准备工作
            this.UnVisibleAllPanels();
            this.InitTimer();

            //显示必要的按钮
            this.Button_LastCouponsPage.Visible = true;
            this.Button_NextCouponsPage.Visible = true;

            this.Panel_NearShop.Visible = true;
            ShowNearShop();
        }
        #endregion

        #region "VIP专区"按钮事件
        private void Button_VIP_MouseDown(object sender, MouseEventArgs e)
        {
            this.Button_Vip.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\VIP专区_1.jpg");
        }

        private void Button_VIP_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_Vip.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\VIP专区.jpg");

            //切换
            int y = this.VerticalScroll.Value;
            this.Panel_Coupons.Location = new System.Drawing.Point(0, 95 - y);

            if (GetTradeName() == null)
            {
                MyMsgBox mb = new MyMsgBox();
                mb.ShowMsg("暂无VIP优惠劵信息！", 2);
                return;
            }

            InitCouponData(GetTradeName()[0], 'v');
            if (LP_ctype[0] == null || LP_ctype[0].Count == 0)
            {
                MyMsgBox mb = new MyMsgBox();
                mb.ShowMsg("暂无VIP优惠劵信息！", 2);
                return;
            }
            Thread.Sleep(20);

            //准备工作
            this.UnVisibleAllPanels();
            this.InitTimer();

            //取消不必要的按钮
            this.Button_LastCouponsPage.Visible = false;
            this.Button_NextCouponsPage.Visible = false;
            this.Btn_Rank.Visible = false;
            this.Btn_Rec.Visible = false;
            this.Btn_NewCouponList.Visible = false;

            InitCouponButton();


            ShowCouponVipBtn();
            this.Panel_Coupons.Visible = true;
            ShowCoupon();
        }

        /// <summary>
        /// 显示VIP优惠劵页面下面的类别按钮
        /// </summary>
        private void ShowCouponVipBtn()
        {
            string[] tradeName = GetTradeName();
            int tradeNum = tradeName.Length;
            Point[] p = CaculatePosition(tradeNum, 'v');
            int width = p[p.Length - 1].X;
            int height = p[p.Length - 1].Y;
            int curNum = tradeNum / 2 + tradeNum % 2 == 0 ? 0 : 1;
            Btn_Coupon_Type = new Button[tradeNum];
            for (int i = 0; i < tradeNum; i++)
            {
                Btn_Coupon_Type[i] = new Button();
                Btn_Coupon_Type[i].BackgroundImage = Image.FromFile(path + "\\images\\切图\\优惠券二级\\Shoptype.jpg");
                Btn_Coupon_Type[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                Btn_Coupon_Type[i].FlatAppearance.BorderSize = 0;
                Btn_Coupon_Type[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                Btn_Coupon_Type[i].Location = p[i];
                Btn_Coupon_Type[i].Name = "Btn_Coupon_Type" + (i + 1);
                Btn_Coupon_Type[i].Text = tradeName[i];
                Btn_Coupon_Type[i].Font = new Font("宋体", 30F, FontStyle.Bold);
                Btn_Coupon_Type[i].ForeColor = Color.White;
                Btn_Coupon_Type[i].Size = new System.Drawing.Size(width, height);
                Btn_Coupon_Type[i].UseVisualStyleBackColor = true;
                Btn_Coupon_Type[i].MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_ShopType_MouseDown);
                Btn_Coupon_Type[i].MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_ShopType_MouseUp);
                Panel_Coupons.Controls.Add(Btn_Coupon_Type[i]);
            }
        }

        #endregion

        #region 优惠劵"上一页"按钮事件

        private void Button_LastCouponsPage_MouseUp(object sender, MouseEventArgs e)
        {
            this.Button_LastCouponsPage.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\上一页(小).jpg");

            if (curPage > 1)
            {
                curPage--;
                if (this.Panel_NearShop.Visible != true)
                    this.ShowBottomPicure();
                else
                    this.ShowNearShop();
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
                if (this.Panel_NearShop.Visible != true)
                    this.ShowBottomPicure();
                else
                    this.ShowNearShop();
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
            op.StartPosition = FormStartPosition.CenterScreen;
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

            //加载走马灯线程
            marquee = new Thread(new ThreadStart(LoadMarquee));
            marquee.Start();
            //this.Label_ScrollText.strContent = _stringScrollText;
            //this.Label_ScrollText.Start();

            //加载计时器
            this.Timer_Countdown.Enabled = true;

            //加载隐藏按钮
            this.Label_Option.BackColor = Color.Transparent;

            //加载主页面
            this.UnVisibleAllPanels();
            InitData();
            InitHomeData();
            this.Panel_Home.Visible = true;

            //加载半透明的Label
            this.OnLoadLabelStyle(90, Color.White);
            ShowHome();

            //展开遮罩窗体
            Thread.Sleep(100);
            th = new Thread(new ThreadStart(TranslateMain));
            th.Start();
        }
        #endregion

        #region 重载消息循环
        //const int WM_LBUTTONDOWN = 0x0201;
        //const int WM_LBUTTONUP = 0x0202;
        //const int WM_LBUTTONDBLCLK = 0x0203;

        //protected override void WndProc(ref System.Windows.Forms.Message m)
        //{
        //    //ToDo:根据m.Msg来处理你要的按键
        //    if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_LBUTTONDBLCLK)
        //    {
        //        MyMsgBox mb = new MyMsgBox();
        //        mb.ShowMsg("请您先刷卡！", 1);

        //        if (!GlobalVariables.isUserLogin)
        //        {
        //            //MyMsgBox mb = new MyMsgBox();
        //            //mb.ShowMsg("请您先刷卡！", 1);
        //            return;
        //        }
        //    }

        //    base.WndProc(ref m);
        //}
        #endregion

        #region 初始化广告倒计时
        private void InitTimer()
        {
            this.Timer_Countdown.Stop();
            CountDownNumber = 10;
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
            this.Panel_NearShop.Visible = false;

            return null;
        }

        #endregion

        #region 广告计时器事件

        private void Timer_Countdown_Tick(object sender, EventArgs e)
        {
            this.Label_Countdown.Text = CountDownNumber.ToString();

            if (CountDownNumber == 0)
            {
                this.Timer_Countdown.Enabled = false;

                this.UnVisibleAllPanels();
                //切换
                tf.Visible = false;
                Ad ad = new Ad(tf);
                ad.Size = new Size(768, 1265);
                ad.Location = new Point(0, 0);
                ad.ShowDialog();
                tf.Visible = true;
            }
            else
            {
                CountDownNumber--;
            }
        }

        #endregion

        #region 数据处理和图片绑定

        #region 加载所有初始化信息

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
                double flaPrice;
                int intVip, intType;

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
                        pi.image = new Bitmap(Image.FromFile(pi.spath), 119, 138);
                    }

                    id = reader.GetString(0);
                    if (id != "" && id != null)
                    {
                        pi.id = id;
                    }

                    name = reader.GetString(1);
                    if (name != "" && name != null)
                    {
                        pi.name = name;
                    }

                    trade = reader.GetString(3);
                    if (trade != "" && trade != null)
                    {
                        pi.trade = trade;
                    }

                    intType = reader.GetInt32(10);
                    pi.intType = intType;

                    LP_shop.Add(pi);
                }

                strSql = "select * from t_bz_coupon order by strShopId asc";
                reader = cmd.ExecuteReader(strSql);
                LP_coupon = new List<CouponPicInfo>();

                while (reader.Read())
                {
                    CouponPicInfo pi = new CouponPicInfo();

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
                            pi.image = new Bitmap(Image.FromFile(pi.spath), 119, 124);
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
                            pi.shopId = shopid;

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

                    if (!reader.IsDBNull(5))
                    {
                        intVip = reader.GetInt32(5);
                        pi.vip = intVip;

                    }

                    if (!reader.IsDBNull(7))
                    {
                        flaPrice = reader.GetDouble(7);
                        pi.flaPrice = flaPrice;

                    }

                    LP_coupon.Add(pi);
                }
                reader.Close();

                //初始化参数
                tradeName = GetTradeName();
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
                reader.Close();
                cmd.Close();

                //    if(LP_shop
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
        private bool InitHomeData()
        {
            if (LP_stype != null && LP_stype.Length != 0)
            {
                for (int i = 0; i < LP_stype.Length; i++)
                {
                    if (LP_stype[i] != null && LP_stype[i].Count > 0)
                    {
                        LP_stype[i].Clear();
                    }
                }
            }

            if (LP_shop == null || LP_shop.Count == 0)
            {
                return false;
            }

            LP_stype = new List<PicInfo>[1];
            LP_stype[0] = FindShopByIntType(1);

            if (LP_ctype != null && LP_ctype.Length != 0)
            {
                for (int i = 0; i < LP_ctype.Length; i++)
                {
                    if (LP_ctype[i] != null && LP_ctype[i].Count > 0)
                    {
                        LP_ctype[i].Clear();
                    }
                }
            }

            if (LP_coupon == null || LP_coupon.Count == 0)
            {
                return false;
            }

            LP_ctype = new List<CouponPicInfo>[1];
            LP_ctype[0] = FindRecCoupon();

            curPage = 1;
            curType = 0;
            theCouponNum = 0;

            return true;
        }

        /// <summary>
        /// 将加载好的图片显示在首页上
        /// </summary>
        private void ShowHome()
        {
            curPage = 1;
            curType = 0;

            if (LP_stype != null)
            {
                if (LP_stype[0] != null && LP_stype[0].Count > 0)
                {
                    ShowHomeTopPicure();
                }
            }

            if (LP_ctype != null)
            {
                if (LP_ctype[0] != null && LP_ctype[0].Count > 0)
                {
                    ShowBottomPicure();
                }
            }
        }

        /// <summary>
        /// 显示首页顶端的商家图片和商家名称
        /// </summary>
        private void ShowHomeTopPicure()
        {
            //    MessageBox.Show(curType.ToString()); 
            if (LP_stype[0].Count > 0)
            {
                if (LP_stype[0][curType].name != null)
                {
                    Label_ShopName.Text = LP_stype[0][curType].name;
                }
                else
                {
                    Label_ShopName.Text = "暂无商家";
                }

                PB_Home_Up.Image = new Bitmap(Image.FromFile(LP_stype[0][curType].lpath), 761, 384);
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

            PB_Home_Down.Image = new Bitmap(Image.FromFile(LP_ctype[0][num - 1 + (curPage - 1) * 12].lpath), 761, 389);

        }

        #endregion

        #region 商家详细数据处理

        /// <summary>
        /// 加载商家详细页面的数据
        /// </summary>
        private void InitShopInfoData(String shopid)
        {
            if (LP_ctype != null)
            {
                for (int i = 0; i < LP_ctype.Length; i++)
                {
                    if (LP_ctype[i] != null && LP_ctype[i].Count > 0)
                    {
                        LP_ctype[i].Clear();
                    }
                }
            }

            string strSql = "select * from t_bz_shop where strId ='" + shopid + "'";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);

            String lPath, name, address, info, id = null;

            LP_ctype = new List<CouponPicInfo>[1];

            if (reader.Read())
            {
                lPath = reader.GetString(9);
                if (lPath != "" && lPath != null)
                {
                    PB_ShopInfo_Shop.Image = new Bitmap(Image.FromFile(path + "\\shop\\" + lPath), 760, 397);
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
            LP_ctype[0] = FindCouponByShopId(id);
            if (LP_ctype[0] == null)
            {
                LP_ctype[0] = new List<CouponPicInfo>();
            }

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
            if (PB_ShopInfo_Coupons.Image != null)
            {
                PB_ShopInfo_Coupons.Image = null;
            }

            PB_ShopInfo_Coupons.Image = new Bitmap(Image.FromFile(LP_ctype[0][0].lpath), 760, 407);

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

            PB_ShopInfo_Coupons.Image.Dispose();

            PB_ShopInfo_Coupons.Image = new Bitmap(Image.FromFile(LP_ctype[0][num - 1 + (curPage - 1) * 6].lpath), 760, 407);
        }

        #endregion

        #region 商家二级数据处理

        /// <summary>
        /// 加载商家页面的数据
        /// </summary>
        private void InitShopData()
        {
            if (LP_stype != null)
            {
                for (int i = 0; i < LP_stype.Length; i++)
                {
                    LP_stype[i].Clear();
                }
            }

            string[] trade = GetTradeName();

            if (trade.Length != 0)
            {
                SetShopTradeTitle(trade.Length);

                if (trade.Length > 3)
                {
                    LP_stype = new List<PicInfo>[3];
                    LP_stype[0] = FindShopByTrade(trade[0]);
                    LP_stype[1] = FindShopByTrade(trade[1]);
                    LP_stype[2] = FindShopByTrade(trade[2]);
                }
                else
                {
                    LP_stype = new List<PicInfo>[trade.Length];
                    for (int i = 0; i < trade.Length; i++)
                    {
                        LP_stype[i] = FindShopByTrade(trade[i]);
                    }
                }
            }

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
            ShowShopPart((int)part.up);
            ShowShopPart((int)part.middle);
            ShowShopPart((int)part.bottom);
        }

        private void SetShopTradeTitle(int num)
        {
            if (num <= 0)
                return;

            Point[] p = new Point[num];
            string[] name = GetTradeName();
            Label_Shop_Type = new Label[num];

            for (int i = 0; i < num; i++)
            {
                this.Label_Shop_Type[i] = new Label();

                this.Label_Shop_Type[i].Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.Label_Shop_Type[i].ForeColor = System.Drawing.Color.White;
                this.Label_Shop_Type[i].Image = Image.FromFile(path + @"\images\切图\商家二级\商家类别.jpg");
                this.Label_Shop_Type[i].Location = new System.Drawing.Point(3 + i / 3 * 183, 28 + i % 3 * 391);
                this.Label_Shop_Type[i].Margin = new System.Windows.Forms.Padding(0);
                this.Label_Shop_Type[i].Name = "Label_Shop_Type" + (i + 1);
                this.Label_Shop_Type[i].Text = name[i];
                this.Label_Shop_Type[i].Size = new System.Drawing.Size(180, 50);
                this.Label_Shop_Type[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                this.Label_Shop_Type[i].Click += new EventHandler(ShopTypeTitle_Click);
                this.Panel_Shop.Controls.Add(this.Label_Shop_Type[i]);
                this.Label_Shop_Type[i].Show();

            }
        }

        private void ShopTypeTitle_Click(object sender, EventArgs e)
        {
            Label temp = sender as Label;
            string name = temp.Name;

            int num = name.Substring(15, 1)[0] - '0';

            LP_stype[num % 3] = FindShopByTrade(tradeName[num - 1]);

            ShowShopPart(num % 3);
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
                if (LP_stype.Length >= 1)
                {
                    if (LP_stype[0] != null && LP_stype[0].Count > 0)
                    {
                        lp = LP_stype[0];
                    }
                }
                curPage = tPage1;
            }
            else if (type == 2)
            {
                controlName = "PB_Shop_type2_";
                if (LP_stype.Length >= 2)
                {
                    if (LP_stype[1] != null && LP_stype[1].Count > 0)
                    {
                        lp = LP_stype[1];
                    }
                }
                curPage = tPage2;
            }
            else
            {
                controlName = "PB_Shop_type3_";
                if (LP_stype.Length >= 3)
                {
                    if (LP_stype[2] != null && LP_stype[2].Count > 0)
                    {
                        lp = LP_stype[2];
                    }
                }
                curPage = tPage3;
            }

            if (lp == null)
            {
                count = 0;
                lp = new List<PicInfo>();
            }
            else
                count = lp.Count;

            totalPage = count / perNum + (count % perNum == 0 ? 0 : 1);
            //    MessageBox.Show(totalPage.ToString());
            if (count != 0)
            {
                if (curPage == totalPage)
                {
                    curPageShowCount = count % (perNum + 1);
                }
                else
                    curPageShowCount = perNum;
            }
            else
            {
                curPageShowCount = 0;
            }

            if (curPageShowCount >= 0)
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

            if (LP_stype[curNumType - 1].Count > 0)
            {
                this.UnVisibleAllPanels();
                this.InitTimer();

                int y = this.VerticalScroll.Value;
                this.Panel_ShopInfo.Location = new System.Drawing.Point(0, 95 - y);

                InitShopInfoData(LP_stype[curNumType - 1][num + (tPage1 - 1) * 12 - 1].id);
                Thread.Sleep(20);

                this.Panel_ShopInfo.Visible = true;
                ShowShopInfo();
            }
            else
            {
                MyMsgBox mb = new MyMsgBox();
                mb.ShowMsg("无此商家信息", 2);
            }
        }

        #endregion

        #region 优惠劵和VIP专区数据处理

        /// <summary>
        /// 加载商家页面的数据
        /// </summary>
        private void InitCouponData(String trade, char type)
        {
            if (trade == null)
            {
                return;
            }
            if (LP_ctype != null)
            {
                for (int i = 0; i < LP_ctype.Length; i++)
                {
                    if (LP_ctype[i] != null)
                    {
                        LP_ctype[i].Clear();
                    }
                }
            }

            LP_ctype = new List<CouponPicInfo>[1];

            LP_ctype[0] = FindCouponByTrade(trade);
            if (type == 'v')
            {
                LP_ctype[0] = FindVipCoupon(LP_ctype[0]);
            }

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
            List<CouponPicInfo> lp = LP_ctype[0];

            count = lp.Count;
            totalPage = count / perNum + (count % perNum == 0 ? 0 : 1);
            //    MessageBox.Show(totalPage.ToString());

            if (count != 0)
            {
                if (curPage == totalPage)
                {
                    curPageShowCount = count % (perNum + 1);
                }
                else
                    curPageShowCount = perNum;
            }
            else
            {
                curPageShowCount = 0;
            }

            if (lp.Count != 0)
            {
                PB_Coupon_Top.Image = new Bitmap(Image.FromFile(lp[(curPage - 1) * 12].lpath), 760, 433);
            }
            else
            {
                PB_Coupon_Top.Image = null;
            }

            if (curPageShowCount >= 0)
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
                    temp.Image = new Bitmap(lp[i + perNum * (curPage - 1)].image, 138, 150);
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

            if (PB_Coupon_Top.Image != null)
            {
                PB_Coupon_Top.Image.Dispose();
            }

            PB_Coupon_Top.Image = new Bitmap(Image.FromFile(LP_ctype[curType][(curPage - 1) * 12 + num - 1].lpath), 760, 433);

        }

        #endregion

        #region 我的专区数据处理
        private void InitMyInfoData()
        {
            MyMsgBox mb = new MyMsgBox();
            if (GlobalVariables.M == null)
            {
                mb.ShowMsg("下载用户信息失败！", 2);
                return;
            }

            //初始化List
            if (LP_ctype != null)
            {
                for (int i = 0; i < LP_ctype.Length; i++)
                {
                    if (LP_ctype[i] != null)
                    {
                        LP_ctype[i].Clear();
                    }
                }
            }

            LP_ctype = new List<CouponPicInfo>[2];

            LP_ctype[0] = FindCouponById(GlobalVariables.M.AryFavourite);
            LP_ctype[1] = FindCouponById(GlobalVariables.M.AryHistory);

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
            List<CouponPicInfo> lp = null;

            if (type == 1)
            {
                controlName = "PB_MyInfo_Fav";
                lp = LP_ctype[0];
                curPage = cPage1;
                if (lp.Count != 0)
                {
                    PB_MyInfo_Fav.Image = new Bitmap(Image.FromFile(lp[(curPage - 1) * 6].lpath), 734, 366);
                }

            }
            else
            {
                controlName = "PB_MyInfo_His";
                lp = LP_ctype[1];
                curPage = cPage2;
                if (lp.Count != 0)
                {
                    PB_MyInfo_His.Image = new Bitmap(Image.FromFile(lp[(curPage - 1) * 6].lpath), 734, 366);
                }
            }

            count = lp.Count;
            totalPage = count / perNum + (count % perNum == 0 ? 0 : 1);

            //    MessageBox.Show(totalPage.ToString());
            if (count != 0)
            {
                if (curPage == totalPage)
                {
                    curPageShowCount = count % (perNum + 1);
                }
                else
                    curPageShowCount = perNum;
            }
            else
                curPageShowCount = 0;

            if (curPageShowCount >= 0)
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

            if (lp.Count > 0)
            {
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
                PB_MyInfo_Fav.Image = new Bitmap(Image.FromFile(LP_ctype[0][num - 1 + (cPage1 - 1) * 6].lpath), 734, 366);
            }
            else
            {
                PB_MyInfo_His.Image = new Bitmap(Image.FromFile(LP_ctype[1][num - 1 + (cPage2 - 1) * 6].lpath), 734, 366);
            }

        }

        #endregion

        #region 周边商家数据处理
        private bool InitNearShopData()
        {
            if (LP_stype != null)
            {
                for (int i = 0; i < LP_stype.Length; i++)
                {
                    if (LP_stype[i] != null)
                    {
                        LP_stype[i].Clear();
                    }
                }
            }

            DownloadInfo di = new DownloadInfo();
            string[] aryStrShopId = di.ShopAround();
            MyMsgBox mb = new MyMsgBox();

            LP_stype = new List<PicInfo>[1];
            if (aryStrShopId.Length == 0)
            {
                LP_stype[0] = new List<PicInfo>();
                return false;
            }
            else
            {
                LP_stype[0] = FindShopById(aryStrShopId);
            }
            return true;
        }

        private void ShowNearShop()
        {
            curPage = 1;
            curType = 0;
            theShopNum = 0;
            ShowNearShopPicture();
        }

        private void ShowNearShopPicture()
        {
            String controlName = "";        //显示小优惠劵的控件的Name
            Panel container = null;         //现在正在操作的Panel容器的对象
            int perNum = 0;                 //每页显示小优惠劵的控件的数量
            List<PicInfo> LP_temp = null;

            controlName = "PB_NearShop";
            container = Panel_NearShop;
            perNum = 24;
            LP_temp = LP_stype[0];

            count = LP_temp.Count;
            totalPage = count / perNum + (count % perNum == 0 ? 0 : 1);
            //    MessageBox.Show(totalPage.ToString());
            if (count != 0)
            {
                if (curPage == totalPage)
                {
                    curPageShowCount = count % (perNum + 1);
                }
                else
                    curPageShowCount = perNum;
            }
            else
                curPageShowCount = 0;

            if (curPageShowCount >= 0)
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
            if (LP_temp.Count != 0)
            {
                PB_NearShop_Top.Image = new Bitmap(Image.FromFile(LP_temp[(curPage - 1) * 24].lpath), 761, 389);

                if (LP_temp[(curPage - 1) * 24].name != null)
                {
                    Label_NearShopName.Text = LP_temp[(curPage - 1) * 24].name;
                }
                else
                {
                    Label_NearShopName.Text = "暂无此商家";
                }

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
        }

        private void ChangeNearShopPicture(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            String name = pb.Name;
            //   MessageBox.Show(name.Length.ToString());
            String numstr = "1";
            int num = 1;

            if (name.Length == 12)
            {
                numstr = name.Substring(11, 1);
                num = (numstr[0] - '0');
            }

            if (name.Length == 13)
            {
                numstr = name.Substring(11, 2);
                num = (numstr[0] - '0') * 10 + (numstr[1] - '0');
            }

            theShopNum = num - 1;
            //      MessageBox.Show(num.ToString());

            PB_NearShop_Top.Image.Dispose();

            PB_NearShop_Top.Image = new Bitmap(Image.FromFile(LP_stype[0][num - 1 + (curPage - 1) * 24].lpath), 761, 389);
        }

        #endregion

        #region 优惠卷弹出页面数据处理
        private CouponPicInfo InitCouponPopData(String strid)
        {
            //读取数据库
            string strSql = "select * from t_bz_coupon where strId='" + strid + "'";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);

            String pPath = null, name = null;
            double flaPrice = 0;
            CouponPicInfo pi = new CouponPicInfo();
            pi.id = strid;

            if (reader.Read())
            {
                pPath = reader.GetString(10);
                if (pPath != "" && pPath != null)
                {
                    pi.lpath = path + "\\coupon\\" + pPath;
                }

                name = reader.GetString(1);
                if (name != "" && name != null)
                {
                    pi.name = name;
                }

                flaPrice = reader.GetDouble(7);
                pi.flaPrice = flaPrice;
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
            List<CouponPicInfo> LP_temp = null;

            if (this.Panel_ShopInfo.Visible == true)
            {
                controlName = "PB_ShopInfo_Coupons0";
                container = Panel_ShopInfo;
                perNum = 6;
                LP_temp = LP_ctype[0];
            }
            else
            {
                controlName = "PB_Home_Bottom";
                container = Panel_Home;
                perNum = 12;
                LP_temp = LP_ctype[0];
            }

            count = LP_temp.Count;
            totalPage = count / perNum + (count % perNum == 0 ? 0 : 1);
            //    MessageBox.Show(totalPage.ToString());
            if (count != 0)
            {
                if (curPage == totalPage)
                {
                    curPageShowCount = count % (perNum + 1);
                }
                else
                    curPageShowCount = perNum;
            }
            else
                curPageShowCount = 0;

            if (curPageShowCount >= 0)
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

            if (LP_temp != null && LP_temp.Count != 0)
            {
                if (container == Panel_Home)
                    PB_Home_Down.Image = new Bitmap(Image.FromFile(LP_temp[(curPage - 1) * 12].lpath), 761, 389);

                if (container == Panel_ShopInfo)
                    PB_ShopInfo_Coupons.Image = new Bitmap(Image.FromFile(LP_temp[(curPage - 1) * 6].lpath), 760, 407);

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
        private List<CouponPicInfo> FindCouponByShopId(String shopId)
        {
            List<CouponPicInfo> temp = new List<CouponPicInfo>();

            foreach (CouponPicInfo pi in LP_coupon)
            {
                if (pi.shopId == shopId)
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
        private List<CouponPicInfo> FindCouponByTrade(String trade)
        {
            List<CouponPicInfo> temp = new List<CouponPicInfo>();

            foreach (CouponPicInfo pi in LP_coupon)
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
        private List<CouponPicInfo> FindCouponById(String[] id)
        {
            List<CouponPicInfo> temp = new List<CouponPicInfo>();

            String[] couponId = id;

            foreach (String tempId in couponId)
            {
                foreach (CouponPicInfo pi in LP_coupon)
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
        /// 根据商家ID来查询并返回商家对象
        /// </summary>
        /// <param name="id">需要查找的商家的ID</param>
        /// <returns>返回查找到的商家对象</returns>
        private List<PicInfo> FindShopById(String[] id)
        {
            List<PicInfo> temp = new List<PicInfo>();

            String[] shopId = id;
            foreach (String tempId in shopId)
            {
                foreach (PicInfo pi in LP_shop)
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
        /// 查找并返回所有推荐的商家
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private List<PicInfo> FindShopByIntType(int intType)
        {
            List<PicInfo> temp = new List<PicInfo>();

            foreach (PicInfo pi in LP_shop)
            {
                if (pi.intType == intType)
                {
                    temp.Add(pi);
                }
            }

            return temp;
        }

        /// <summary>
        /// 从指定优惠劵List中查找VIP优惠劵
        /// </summary>
        /// <param name="lp">待查找的优惠劵List</param>
        /// <returns>查找出的VIP优惠劵List</returns>
        private List<CouponPicInfo> FindVipCoupon(List<CouponPicInfo> lp)
        {
            List<CouponPicInfo> temp = new List<CouponPicInfo>();

            foreach (CouponPicInfo cpi in lp)
            {
                if (cpi.vip == 1)
                {
                    temp.Add(cpi);
                }
            }

            lp.Clear();

            return temp;
        }

        /// <summary>
        /// 查找推荐优惠劵
        /// </summary>
        /// <returns>返回查找到的优惠劵List</returns>
        private List<CouponPicInfo> FindRecCoupon()
        {
            MyMsgBox mb = new MyMsgBox();
            List<CouponPicInfo> temp = null;
            try
            {
                String time = DateTime.Now.ToString("yyyy-M-d H:m:s");
                string strSql = "select top 24 strId from t_bz_coupon where intRecommend=1 order by dtActiveTime desc";
                AccessCmd cmd = new AccessCmd();
                OleDbDataReader reader = cmd.ExecuteReader(strSql);

                String strId = "";
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        strId += reader.GetString(0) + ",";
                    }
                }
                temp = FindCouponById(strId.Substring(0, strId.Length - 1).Split(','));
                return temp;
            }
            catch (Exception e1)
            {
                mb.ShowMsg(e1.Message + "\n正在修复", 2);
                return new List<CouponPicInfo>();
            }
        }

        /// <summary>
        /// 收藏和打印半透明Label的时间处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TranlateLabel_Click(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            int type = 1;        //0表示收藏，非0表示打印

            String id = null;
            switch (lb.Name)
            {
                case "Home_Fav":
                    type = 0;
                    if (LP_coupon.Count > 0)
                    {
                        id = LP_coupon[(curPage - 1) * 12 + theCouponNum].id;
                    }
                    break;
                case "Home_Print":
                    type = 1;
                    if (LP_coupon.Count > 0)
                    {
                        id = LP_coupon[(curPage - 1) * 12 + theCouponNum].id;
                    }
                    break;
                case "ShopInfo_Fav":
                    type = 0;
                    if (LP_ctype[0].Count > 0)
                    {
                        id = LP_ctype[0][(curPage - 1) * 6 + theCouponNum].id;
                    }
                    break;
                case "ShopInfo_Print":
                    type = 1;
                    if (LP_ctype[0].Count > 0)
                    {
                        id = LP_ctype[0][(curPage - 1) * 6 + theCouponNum].id;
                    }
                    break;
                case "Coupon_Fav":
                    type = 0;
                    if (LP_ctype[0].Count > 0)
                    {
                        id = LP_ctype[0][(curPage - 1) * 12 + theCouponNum].id;
                    }
                    break;
                case "Coupon_Print":
                    type = 1;
                    if (LP_ctype[0].Count > 0)
                    {
                        id = LP_ctype[0][(curPage - 1) * 12 + theCouponNum].id;
                    }
                    break;
                case "MyInfo_Top_Print":
                    type = 1;
                    if (LP_ctype[0].Count > 0)
                    {
                        id = LP_ctype[0][(curPage - 1) * 6 + theCouponNum].id;
                    }
                    break;
                case "MyInfo_Bottom_Fav":
                    type = 0;
                    if (LP_ctype[1].Count > 0)
                    {
                        id = LP_ctype[1][(curPage - 1) * 6 + theCouponNum].id;
                    }
                    break;
                case "MyInfo_Bottom_Print":
                    type = 1;
                    if (LP_ctype[1].Count > 0)
                    {
                        id = LP_ctype[1][(curPage - 1) * 6 + theCouponNum].id;
                    }
                    break;
                default: break;
            }

            CouponPicInfo pi = null;
            MyMsgBox mb = new MyMsgBox();

            if (id != null)
            {
                if (type == 1)
                {
                    if (!GlobalVariables.isUserLogin)
                    {
                        mb.ShowMsg("请您先登录", 2);
                        return;
                    }
                    else
                    {
                        pi = InitCouponPopData(id);
                        CouponsPopForm cpf = new CouponsPopForm(pi);
                        cpf.ShowDialog();
                        Thread.Sleep(200);
                    }
                }
                else
                {
                    if (!GlobalVariables.isUserLogin)
                    {
                        mb.ShowMsg("请您先登录", 2);
                        return;
                    }
                    else
                    {
                        mb.ShowMsg("确认收藏？", '1');
                        if (mb.DialogResult == DialogResult.Yes)
                        {
                            UploadInfo ui = new UploadInfo();

                            if (ui.CouponFavourite(GlobalVariables.LoginUserId, id))
                            {
                                mb.ShowMsg("收藏成功！", 2);
                                string[] str = new string[(GlobalVariables.M.AryFavourite.Length + 1)];
                                str[GlobalVariables.M.AryFavourite.Length] = id;
                                GlobalVariables.M.AryFavourite = str;
                            }
                            else
                            {
                                mb.ShowMsg("收藏失败！请稍后重试", 2);
                            }
                        }
                    }
                }
            }
            else
                return;
        }

        /// <summary>
        /// 预设定半透明的Label
        /// </summary>
        /// <param name="color"></param>
        private void OnLoadLabelStyle(int alph, Color color)
        {
            Point p = PB_Home_Down.Location;
            Home_Fav.Parent = PB_Home_Down;
            Home_Fav.Location = new Point(Home_Fav.Location.X - p.X, Home_Fav.Location.Y - p.Y);
            Home_Fav.BackColor = Color.FromArgb(alph, color);
            Home_Print.Parent = PB_Home_Down;
            Home_Print.Location = new Point(Home_Print.Location.X - p.X, Home_Print.Location.Y - p.Y);
            Home_Print.BackColor = Color.FromArgb(alph, color);

            p = PB_ShopInfo_Coupons.Location;
            ShopInfo_Fav.Parent = PB_ShopInfo_Coupons;
            ShopInfo_Fav.Location = new Point(ShopInfo_Fav.Location.X - p.X, ShopInfo_Fav.Location.Y - p.Y);
            ShopInfo_Fav.BackColor = Color.FromArgb(alph, color);
            ShopInfo_Print.Parent = PB_ShopInfo_Coupons;
            ShopInfo_Print.Location = new Point(ShopInfo_Print.Location.X - p.X, ShopInfo_Print.Location.Y - p.Y);
            ShopInfo_Print.BackColor = Color.FromArgb(alph, color);

            p = PB_Coupon_Top.Location;
            Coupon_Fav.Parent = PB_Coupon_Top;
            Coupon_Fav.Location = new Point(Coupon_Fav.Location.X - p.X, Coupon_Fav.Location.Y - p.Y);
            Coupon_Fav.BackColor = Color.FromArgb(alph, color);
            Coupon_Print.Parent = PB_Coupon_Top;
            Coupon_Print.Location = new Point(Coupon_Print.Location.X - p.X, Coupon_Print.Location.Y - p.Y);
            Coupon_Print.BackColor = Color.FromArgb(alph, color);

            p = PB_MyInfo_His.Location;
            MyInfo_Bottom_Fav.Parent = PB_MyInfo_His;
            MyInfo_Bottom_Fav.Location = new Point(MyInfo_Bottom_Fav.Location.X - p.X, MyInfo_Bottom_Fav.Location.Y - p.Y);
            MyInfo_Bottom_Fav.BackColor = Color.FromArgb(alph, color);
            MyInfo_Bottom_Print.Parent = PB_MyInfo_His;
            MyInfo_Bottom_Print.Location = new Point(MyInfo_Bottom_Print.Location.X - p.X, MyInfo_Bottom_Print.Location.Y - p.Y);
            MyInfo_Bottom_Print.BackColor = Color.FromArgb(alph, color);

            p = PB_MyInfo_Fav.Location;
            MyInfo_Top_Print.Parent = PB_MyInfo_Fav;
            MyInfo_Top_Print.Location = new Point(MyInfo_Top_Print.Location.X - p.X, MyInfo_Top_Print.Location.Y - p.Y);
            MyInfo_Top_Print.BackColor = Color.FromArgb(alph, color);

        }

        /// <summary>
        /// 获取商家种类的个数
        /// </summary>
        /// <returns>返回商家种类的个数</returns>
        private string[] GetTradeName()
        {
            try
            {
                string strSql = "select distinct strTrade from t_bz_shop";
                AccessCmd cmd = new AccessCmd();
                OleDbDataReader reader = cmd.ExecuteReader(strSql);

                String tradeName = "";
                while (reader.Read())
                {
                    tradeName += reader.GetString(0) + ",";
                    count++;
                }
                reader.Close();
                cmd.Close();
                if (tradeName != "")
                {
                    return tradeName.Substring(0, tradeName.Length - 1).Split(',');
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
            }
            return null;
        }

        /// <summary>
        /// 计算优惠劵和VIP页面动态生成的类别按钮的Position
        /// </summary>
        /// <param name="num">需要计算的按钮的个数</param>
        /// <param name="type">计算的类别('v'代表VIP页面, 'c'代表优惠劵页面)</param>
        /// <returns></returns>
        private Point[] CaculatePosition(int num, char type)
        {
            Point[] p = new Point[num + 1];
            int rowNum = num / 2 + ((num % 2 == 0) ? 0 : 1);
            int width = (768 - (rowNum + 1) * 5) / rowNum;
            int pa = 1026, pb = 1095, pheight = 60;
            if (type == 'v')
            {
                pa = 966;
                pb = 1070;
                pheight = 89;
            }

            for (int i = 0; i < rowNum; i++)
            {
                p[i] = new Point(5 * (i + 1) + width * i, pa);
            }

            for (int i = 0; i < num - rowNum; i++)
            {
                p[rowNum + i] = new Point(5 * (i + 1) + width * i, pb);
            }

            p[num] = new Point(width, pheight);
            return p;

        }

        #endregion

        #endregion

        #region 走马灯线程

        /// <summary>
        /// 走马灯线程函数
        /// </summary>
        private void LoadMarquee()
        {

            while (true)
            {
                String dtime = DateTime.Now.ToString("H:m:s");
                string strSql = "select * from t_bz_advertisement where intType=3 and #" + dtime + "#>=dtStartTime and #" + dtime + "#<dtEndTime";
                AccessCmd cmd = new AccessCmd();
                OleDbDataReader reader = cmd.ExecuteReader(strSql);

                string strText = "";
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(3))
                        {
                            strText = reader.GetString(3);
                        }
                    }
                }
                reader.Close();
                cmd.Close();

                if (strText == "")
                {
                    strText = "此处广告未招租，有意向请拨打" + GlobalVariables.StrPhone;
                }

                if (Label_ScrollText.InvokeRequired)
                {
                    this.Label_ScrollText.Invoke((MethodInvoker)delegate
                    {
                        this.Label_ScrollText.Stop();
                        this.Label_ScrollText.strContent = strText;
                        this.Label_ScrollText.Start();
                    }, null);
                }
                else
                {
                    this.Label_ScrollText.Stop();
                    this.Label_ScrollText.strContent = strText;
                    this.Label_ScrollText.Start();
                }

                Thread.Sleep(1800 * 1000);
            }

        }
        #endregion

        private void Label_Countdown_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        #region 加载屏蔽窗口
        TranslateForm tf;
        /// <summary>
        /// 加载屏蔽窗口
        /// </summary>
        private void TranslateMain()
        {
            tf = new TranslateForm(this);
            tf.Location = new Point(0, 0);
            tf.Size = new Size(768, 1366);
            DialogResult dr = DialogResult.No;

            while (dr.CompareTo(DialogResult.No) == 0)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        dr = tf.ShowDialog(this);

                        if (dr.CompareTo(DialogResult.Yes) == 0)
                        {
                            if (GlobalVariables.isUserLogin == true)
                            {
                                this.Timer_Countdown.Enabled = false;
                            }
                        }

                        this.UnVisibleAllPanels();
                        this.InitTimer();

                        //显示隐藏按钮
                        this.Button_LastCouponsPage.Visible = true;
                        this.Button_NextCouponsPage.Visible = true;

                        //切换
                        int y = this.VerticalScroll.Value;
                        this.Panel_Home.Location = new System.Drawing.Point(0, 95 - y);

                        this.InitHomeData();
                        this.Panel_Home.Visible = true;
                        this.ShowHome();

                    }, null);
                }
                else
                {
                    dr = tf.ShowDialog(this);
                    if (dr.CompareTo(DialogResult.Yes) == 0)
                    {
                        if (GlobalVariables.isUserLogin == true)
                        {
                            this.Timer_Countdown.Enabled = false;
                        }
                    }

                    this.UnVisibleAllPanels();
                    this.InitTimer();

                    //显示隐藏按钮
                    this.Button_LastCouponsPage.Visible = true;
                    this.Button_NextCouponsPage.Visible = true;

                    //切换
                    int y = this.VerticalScroll.Value;
                    this.Panel_Home.Location = new System.Drawing.Point(0, 95 - y);

                    this.InitHomeData();
                    this.Panel_Home.Visible = true;
                    this.ShowHome();

                }
            }
        }

        #endregion

        #endregion

        private void Timer_DownloadInfo_Tick(object sender, EventArgs e)
        {
            this.Timer_DownloadInfo.Stop();
            try
            {
                //下载信息
                DownloadInfo di = new DownloadInfo();
                di.download();
                di.SynParam();
                //同步数据
                this.InitData();
                this.Timer_DownloadInfo.Interval = GlobalVariables.IntRefreshSec * 1000;
                //上传消费记录
                UploadInfo ui = new UploadInfo();
                ui.CouponPrint();
            }
            catch (Exception ep)
            {
                ErrorLog.log(ep);
            }
            this.Timer_DownloadInfo.Start();
        }

        private void Btn_NewCouponList_Click(object sender, EventArgs e)
        {
            MyMsgBox mb = new MyMsgBox();
            try
            {
                String time = DateTime.Now.ToString("yyyy/M/d H:m:s");
                string strSql = "select top 24 strId from t_bz_coupon where #" + time + "#>dtActiveTime and #" + time + "#< dtExpireTime order by dtActiveTime desc";
                AccessCmd cmd = new AccessCmd();
                OleDbDataReader reader = cmd.ExecuteReader(strSql);

                String strId = "";
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        strId += reader.GetString(0) + ",";
                    }
                }

                reader.Close();
                cmd.Close();
                if (strId != "")
                {
                    LP_ctype[0] = FindCouponById(strId.Substring(0, strId.Length - 1).Split(','));
                }
                else
                {
                    LP_ctype[0] = new List<CouponPicInfo>();
                }
                curPage = 1;
                curType = 0;
                theCouponNum = 0;

                ShowCoupon();

            }
            catch (Exception e1)
            {
                mb.ShowMsg(e1.Message + "\n正在修复", 1);
            }

        }

        private void Btn_Rank_Click(object sender, EventArgs e)
        {
            DownloadInfo di = new DownloadInfo();
            string[] aryStrCouponId = di.CouponTop();
            MyMsgBox mb = new MyMsgBox();

            if (aryStrCouponId.Length == 0)
            {
                mb.ShowMsg("排行榜暂时无数据!", 2);
                return;
            }

            LP_ctype[0] = FindCouponById(aryStrCouponId);
            curPage = 1;
            curType = 0;
            theCouponNum = 0;

            ShowCoupon();

        }

        private void Btn_Rec_Click(object sender, EventArgs e)
        {
            LP_ctype[0] = FindRecCoupon();
            curPage = 1;
            curType = 0;
            theCouponNum = 0;
            ShowCoupon();
        }

        private void Button_NearShopInfo_MouseDown(object sender, MouseEventArgs e)
        {
            this.Btn_NearShopInfo.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\详细_1.jpg");
        }

        private void Button_NearShopInfo_MouseUp(object sender, MouseEventArgs e)
        {
            this.Btn_NearShopInfo.BackgroundImage = Image.FromFile(path + "\\images\\切图\\首页\\详细.jpg");

            this.InitTimer();
            if (LP_stype[0] != null && LP_stype[0].Count > 0)
            {
                InitShopInfoData(LP_stype[0][(curPage - 1) * 24 + theShopNum].id);
            }
            else
            {
                MyMsgBox mb = new MyMsgBox();
                mb.ShowMsg("没有信息！", 2);
                return;
            }
            Thread.Sleep(20);

            this.UnVisibleAllPanels();
            int y = this.VerticalScroll.Value;
            this.Panel_ShopInfo.Location = new System.Drawing.Point(0, 95 - y);



            this.Panel_ShopInfo.Visible = true;
            ShowShopInfo();
        }

        private void MainFrame_Closing(object sender, FormClosingEventArgs e)
        {
            if (marquee.IsAlive)
            {
                marquee.Abort();
                marquee.Join();
            }

        }
    }
}
