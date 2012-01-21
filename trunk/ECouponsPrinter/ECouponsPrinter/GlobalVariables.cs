using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECouponsPrinter
{
    public static class GlobalVariables
    {
        private static bool _isKeyBoardExist;


        private static String strTerminalNo = "45";
        private static String strServerUrl = "http://127.0.0.1:8080/ecoupons";

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

        public static bool isKeyBoardExist
        {
            get { return _isKeyBoardExist; }
            set { _isKeyBoardExist = value; }
        }

    }
}