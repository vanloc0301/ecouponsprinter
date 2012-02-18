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
        private static Member _testM = null;
        private static String _LoginUserId = null;               //当前登录用户的ID

        private static String strTerminalNo = "45";//终端编号
        private static String strServerUrl = "http://127.0.0.1:8080/ecoupons";//远程服务端URL
        private static String strExitPwd = "xjtcmzc";//终端推出密码


        public static String LoginUserId
        {
            set { _LoginUserId = value; }
            get { return _LoginUserId; }
        }

        public static Member testM
        {
            set { _testM = value; }
            get { return _testM; }
        }

        public static String StrExitPwd
        {
            get { return GlobalVariables.strExitPwd; }
            set { GlobalVariables.strExitPwd = value; }
        }
        private static int intRefreshSec = 600;//终端定时与服务器同步的时间间隔，以秒为单位

        public static int IntRefreshSec
        {
            get { return GlobalVariables.intRefreshSec; }
            set { GlobalVariables.intRefreshSec = value; }
        }
        private static String strPhone = "0558-2282609";//联系电话

        public static String StrPhone
        {
            get { return GlobalVariables.strPhone; }
            set { GlobalVariables.strPhone = value; }
        }
        private static int intAdImg = 30;//当广告是多张图片时，切换的时间间隔，以秒为单位

        public static int IntAdImg
        {
            get { return GlobalVariables.intAdImg; }
            set { GlobalVariables.intAdImg = value; }
        }
        private static int intHistory = 20;//下载的最近消费记录条数

        public static int IntHistory
        {
            get { return GlobalVariables.intHistory; }
            set { GlobalVariables.intHistory = value; }
        }
        private static int intCouponPrint = 500;//券打机打印张数，达到该数值则自动提醒“纸将近”

        public static int IntCouponPrint
        {
            get { return GlobalVariables.intCouponPrint; }
            set { GlobalVariables.intCouponPrint = value; }
        }

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