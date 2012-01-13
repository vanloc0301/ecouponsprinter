﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECouponsPrinter
{
    public static class GlobalVariables
    {
        private static Form _HomePage;
        private static Form _ShopInfoPage;
        private static Form _ShopPage;
        private static Form _MyInfoPage;
        private static Form _CouponsPage;
        private static Form _CouponsPopPage;
        private static Form _Option;
        private static Form _KeyBoard;

        private static String strTerminalNo;
        private static String strServerUrl;

        public static String StrServerUrl
        {
            get { return GlobalVariables.strServerUrl; }
            set { GlobalVariables.strServerUrl = value; }
        }

        public static String StrTerminalNo
        {
            get { return GlobalVariables.strTerminalNo; }
            set { GlobalVariables.strTerminalNo = value; }
        }

        public static void Set_Homepage(Form form)
        {
           _HomePage = form;
        }

        public static Form Get_HomePage()
        {
            return _HomePage;
        }

        public static void Set_ShopInfoPage(Form form)
        {
            _ShopInfoPage = form;
        }

        public static Form Get_ShopInfoPage()
        {
            return _ShopInfoPage;
        }

        public static void Set_ShopPage(Form form)
        {
            _ShopPage = form;
        }

        public static Form Get_ShopPage()
        {
            return _ShopPage;
        }

        public static void Set_MyInfoPage(Form form)
        {
            _MyInfoPage = form;
        }

        public static Form Get_MyInfoPage()
        {
            return _MyInfoPage;
        }

        public static void Set_CouponsPage(Form form)
        {
            _CouponsPage = form;
        }

        public static Form Get_CouponsPage()
        {
            return _CouponsPage;
        }

        public static void Set_CouponsPopPage(Form form)
        {
            _CouponsPopPage = form;
        }

        public static Form Get_CouponsPopPage()
        {
            return _CouponsPopPage;
        }

        public static void Set_Option(Form form)
        {
            _Option = form;
        }

        public static Form Get_Option()
        {
            return _Option;
        }

        public static void Set_KeyBoard(Form form)
        {
            _KeyBoard = form;
        }

        public static Form Get_KeyBoard()
        {
            return _KeyBoard;
        }
    }
}
