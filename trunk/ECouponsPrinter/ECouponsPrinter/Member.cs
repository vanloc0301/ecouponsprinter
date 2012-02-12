using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECouponsPrinter
{
    public class Member
    {
        private string strCardNo;

        public string StrCardNo
        {
            get { return strCardNo; }
            set { strCardNo = value; }
        }

        private string[] aryFavourite;

        public string[] AryFavourite
        {
            get { return aryFavourite; }
            set { aryFavourite = value; }
        }

        private string[] aryHistory;

        public string[] AryHistory
        {
            get { return aryHistory; }
            set { aryHistory = value; }
        }

        private int intType;

        public int IntType
        {
            get { return intType; }
            set { intType = value; }
        }

        private string strMobileNo;

        public string StrMobileNo
        {
            get { return strMobileNo; }
            set { strMobileNo = value; }
        }

        public Member() { }
    }
}
