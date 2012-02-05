using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ECouponsPrinter
{
    class PicInfo
    {
        private String _lpath = null;        //大图片的路径
        private String _spath = null;       //小图片的路径
        private String _name = null;        //图片代表的商家或优惠劵的名字
        private String _id = null;          //图片代表的商家或优惠劵的id
        private String _trade = null;       //商家类别，如果当前指代的不是商家图片，则这一项设置为空
        private String _shopid = null;      //优惠劵所属的商家id，如果当前不是优惠劵图片，则这一项置为空
        private Image _image = null;        //预加载的图片

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

        public String trade
        {
            get { return _trade; }
            set { _trade = value; }
        }

        public String shopid
        {
            get { return _shopid; }
            set { _shopid = value; }
        }

        public Image image
        {
            get { return _image; }
            set { _image = value; }
        }
    }
}
