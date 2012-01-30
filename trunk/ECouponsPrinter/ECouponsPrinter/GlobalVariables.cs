using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECouponsPrinter
{
    public static class GlobalVariables
    {
        private static bool _isKeyBoardExist = false;           //判断是否已经生成键盘
        private static bool _isUserLogin = false;               //判断是否有用户登录
        private static int _WindowWaitTime = 30;                //未登录时,屏幕无操作超过此时间,自动开始播放广告
        private static int _UserWaitTime = 30;                  //登录后，屏幕无操作超过此时间,自动注销用户登录信息,并返回首页
        private static String _MarqueeText = "欢迎使用本系统";  //走马灯的文字

        private static String strTerminalNo = "45";
        private static String strServerUrl = "http://127.0.0.1:8080/ecoupons";

        public static String MarqueeText
        {
            get { return _MarqueeText; }
            set { _MarqueeText = value; }
        }

        public static int WindowWaitTime
        {
            get { return _WindowWaitTime; }
            set { _WindowWaitTime = value; }
        }

        public static int UserWaitTime
        {
            get { return _UserWaitTime; }
            set { _UserWaitTime = value; }
        }

        public static bool isUserLogin
        {
            get { return _isUserLogin; }
            set { _isUserLogin = value; }
        }

        public static bool isKeyBoardExist
        {
            get { return _isKeyBoardExist; }
            set { _isKeyBoardExist = value; }
        }

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

        

    }
}