using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECouponsPrinter
{
    class Member
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

        public Member() { }
    }
}
