using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECouponsPrinter
{
    class PicInfo
    {
        private String _lpath = null;        //大图片的路径
        private String _spath = null;       //小图片的路径
        private String _name = null;        //图片代表的商家或优惠劵的名字
        private String _id = null;          //图片代表的商家或优惠劵的id

        public String lpath
        {
            get { return _lpath; }
            set { _lpath = value; }
        }

        public String spath
        {
            get { return _spath; }
            set { _spath = value; }
        }

        public String name
        {
            get { return _name; }
            set { _name = value; }
        }

        public String id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}
