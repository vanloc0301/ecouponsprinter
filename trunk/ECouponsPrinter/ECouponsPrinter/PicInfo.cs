using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ECouponsPrinter
{
    public class PicInfo
    {
        private String _lpath = null;        //大图片的路径
        private String _spath = null;       //小图片的路径
        private String _id = null;          //图片代表的商家或优惠劵的id
        private String _trade = null;       //类别
        private Image _image = null;        //预加载的图片
        private String _name = null;
        private int _intType;

        public int intType
        {
            get { return _intType; }
            set { _intType = value; }
        }

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

        public String trade
        {
            get { return _trade; }
            set { _trade = value; }
        }

        public String id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Image image
        {
            get { return _image; }
            set { _image = value; }
        }

        public String name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

    public class CouponPicInfo : PicInfo 
    {
        
        private String _shopId;
        private double _flaPrice;
        private int _vip;

        public String shopId
        {
            get { return _shopId; }
            set { _shopId = value; }
        }

        public double flaPrice
        {
            get { return _flaPrice; }
            set { _flaPrice = value; }
        }

        public int vip
        {
            get { return _vip; }
            set { _vip = value; }
        } 
    }
}
