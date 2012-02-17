using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Net;

namespace ECouponsPrinter
{
    class DownloadInfo
    {
        private HttpRequest request = new HttpRequest();

        public DownloadInfo() { }

        public void download()
        {
            //下载商家信息
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/ShopDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
            XmlDocument doc = new XmlDocument();
            string strXml = request.HtmlDocument;
            if (strXml.IndexOf("<shops>") > 0)
            {
                doc.LoadXml(strXml);
                XmlNodeList xnlShops = doc.GetElementsByTagName("shops");
                for (int i = 0; i < xnlShops.Count; i++)
                {
                    XmlElement xeShops = (XmlElement)xnlShops.Item(i);
                    String strOpe = xeShops.FirstChild.InnerText;
                    if (strOpe.Equals("add"))
                    {
                        foreach (XmlNode xnShop in xeShops.GetElementsByTagName("shop"))
                        {
                            addShop(xnShop);
                        }
                    }
                    else if (strOpe.Equals("update"))
                    {
                        foreach (XmlNode xnShop in xeShops.GetElementsByTagName("shop"))
                        {
                            updateShop(xnShop);
                        }
                    }
                    else if (strOpe.Equals("delete"))
                    {
                        foreach (XmlNode xnShop in xeShops.GetElementsByTagName("shop"))
                        {
                            deleteShop(xnShop);
                        }
                    }
                }
            }
            //下载优惠券信息
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/CouponDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
            strXml = request.HtmlDocument;
            if (strXml.IndexOf("<coupons>") > 0)
            {
                doc.LoadXml(strXml);
                XmlNodeList xnlCoupons = doc.GetElementsByTagName("coupons");
                for (int i = 0; i < xnlCoupons.Count; i++)
                {
                    XmlElement xeCoupons = (XmlElement)xnlCoupons.Item(i);
                    String strOpe = xeCoupons.FirstChild.InnerText;
                    if (strOpe.Equals("add"))
                    {
                        foreach (XmlNode xnCoupon in xeCoupons.GetElementsByTagName("coupon"))
                        {
                            addCoupon(xnCoupon);
                        }
                    }
                    else if (strOpe.Equals("update"))
                    {
                        foreach (XmlNode xnCoupon in xeCoupons.GetElementsByTagName("coupon"))
                        {
                            updateCoupon(xnCoupon);
                        }
                    }
                    else if (strOpe.Equals("delete"))
                    {
                        foreach (XmlNode xnCoupon in xeCoupons.GetElementsByTagName("coupon"))
                        {
                            deleteCoupon(xnCoupon);
                        }
                    }
                }
            }
            //下载广告信息
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/AdDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
            strXml = request.HtmlDocument;
            if (strXml.IndexOf("<ads>") > 0)
            {
                doc.LoadXml(strXml);
                XmlNodeList xnls = doc.GetElementsByTagName("ads");
                for (int i = 0; i < xnls.Count; i++)
                {
                    XmlElement xes = (XmlElement)xnls.Item(i);
                    String strOpe = xes.FirstChild.InnerText;
                    if (strOpe.Equals("add"))
                    {
                        foreach (XmlNode xn in xes.GetElementsByTagName("ad"))
                        {
                            addAd(xn);
                        }
                    }
                    else if (strOpe.Equals("update"))
                    {
                        foreach (XmlNode xn in xes.GetElementsByTagName("ad"))
                        {
                            updateAd(xn);
                        }
                    }
                    else if (strOpe.Equals("delete"))
                    {
                        foreach (XmlNode xn in xes.GetElementsByTagName("ad"))
                        {
                            deleteAd(xn);
                        }
                    }
                }
            }
        }

        private void deleteAd(XmlNode xn)
        {
            String strId, strContentOld = "";
            int intTypeOld = 0;
            XmlElement xe = (XmlElement)xn;
            strId = xe.GetElementsByTagName("strId").Item(0).InnerText.Trim();
            //查询文件
            string strSql = "select intType,strContent from t_bz_advertisement where strId='" + strId + "'";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);
            if (reader.Read())
            {
                intTypeOld = reader.GetInt32(0);
                strContentOld = reader.GetString(1);
            }
            reader.Close();
            //删除原文件
            if (intTypeOld == 1)
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\ad\\" + strContentOld);
            }
            else if (intTypeOld == 2)
            {
                string[] aryFile = strContentOld.Split(new char[] { ',' });
                for (int i = 0; i < aryFile.Length; i++)
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\ad\\" + aryFile[i]);
                }
            }
            //写入数据库
            strSql = "delete from t_bz_advertisement where strId='" + strId + "'";
            cmd.ExecuteNonQuery(strSql);                
            cmd.Close();
        }

        private void updateAd(XmlNode xn)
        {
            String strId, strName, strContent, strContentOld = "", dtStartTime, dtEndTime;
            int intType, intTypeOld = 0;
            getAdProps(xn, out strId, out strName, out intType, out strContent, out dtStartTime, out dtEndTime);
            //查询文件
            string strSql = "select intType,strContent from t_bz_advertisement where strId='" + strId + "'";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);
            if (reader.Read())
            {
                intTypeOld = reader.GetInt32(0);
                strContentOld = reader.GetString(1);
            }
            reader.Close();
            //删除原文件
            if (intTypeOld == 1)
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\ad\\" + strContentOld);
            }
            else if (intTypeOld == 2)
            {
                string[] aryFile = strContentOld.Split(new char[] { ',' });
                for (int i = 0; i < aryFile.Length; i++)
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\ad\\" + aryFile[i]);
                }
            }
            //创建文件
            if (intType == 1)
            {
                createImg("ad", strContent);
            }
            else if (intType == 2)
            {
                string[] aryFile = strContent.Split(new char[] { ',' });
                for (int i = 0; i < aryFile.Length; i++)
                {
                    createImg("ad", aryFile[i]);
                }
            }
            //写入数据库
            strSql = "update t_bz_advertisement set strName='" + strName + "',intType=" + intType + ",strContent='" + strContent + "',dtStartTime='" + dtStartTime +
                "',dtEndTime='" + dtEndTime + "' where strId='" + strId + "'";
            cmd.ExecuteNonQuery(strSql);
            cmd.Close();
        }

        private void addAd(XmlNode xn)
        {
            String strId, strName, strContent, dtStartTime, dtEndTime;
            int intType;
            getAdProps(xn, out strId, out strName, out intType, out strContent, out dtStartTime, out dtEndTime);
            //创建文件
            if (intType == 1)
            {
                createImg("ad", strContent);
            }
            else if (intType == 2)
            {
                string[] aryFile = strContent.Split(new char[] { ',' });
                for (int i = 0; i < aryFile.Length; i++)
                {
                    createImg("ad", aryFile[i]);
                }
            }
            //写入数据库
            AccessCmd cmd = new AccessCmd();
            string strSql = "insert into t_bz_advertisement(strId,strName,intType,strContent,dtStartTime,dtEndTime) values('" + strId + "','" + strName + "'," + intType +
                ",'" + strContent + "','" + dtStartTime + "','" + dtEndTime + "')";
            cmd.ExecuteNonQuery(strSql);
            cmd.Close();
        }

        private void deleteCoupon(XmlNode xnCoupon)
        {
            String strId, strSmallImg = "", strLargeImg = "", strPrintImg = "";
            XmlElement xeCoupon = (XmlElement)xnCoupon;
            strId = xeCoupon.GetElementsByTagName("strId").Item(0).InnerText.Trim();
            //查询文件
            string strSql = "select strSmallImg,strLargeImg,strPrintImg from t_bz_coupon where strId='" + strId + "'";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);
            if (reader.Read())
            {
                strSmallImg = reader.GetString(0);
                strLargeImg = reader.GetString(1);
                strPrintImg = reader.GetString(2);
            }
            reader.Close();
            //删除原文件
            if (!strSmallImg.Equals(""))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strSmallImg);
            }
            if (!strLargeImg.Equals(""))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strLargeImg);
            }
            if (!strPrintImg.Equals(""))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strPrintImg);
            }
            //写入数据库
            strSql = "delete from t_bz_coupon where strId='" + strId + "'";
            cmd.ExecuteNonQuery(strSql);
            cmd.Close();
        }

        private void updateCoupon(XmlNode xnCoupon)
        {
            String strId, strName, dtActiveTime, dtExpireTime, strShopId, strSmallImg, strLargeImg, strPrintImg, strSmallImgOld = "", strLargeImgOld = "", strPrintImgOld = "";
            int intVip, intRecommend;
            float flaPrice;
            getCouponProps(xnCoupon, out strId, out strName, out dtActiveTime, out dtExpireTime, out strShopId, out intVip, out flaPrice, out intRecommend,
                out strSmallImg, out strLargeImg, out strPrintImg);
            //查询文件
            string strSql = "select strSmallImg,strLargeImg,strPrintImg from t_bz_coupon where strId='" + strId + "'";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);
            if (reader.Read())
            {
                strSmallImgOld = reader.GetString(0);
                strLargeImgOld = reader.GetString(1);
                strPrintImgOld = reader.GetString(2);
            }
            reader.Close();
            //删除原文件
            if (!strSmallImgOld.Equals(""))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strSmallImgOld);
            }
            if (!strLargeImgOld.Equals(""))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strLargeImgOld);
            }
            if (!strPrintImgOld.Equals(""))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strPrintImgOld);
            }
            //创建文件
            if (strSmallImg.Length > 0)
                createImg("coupon", strSmallImg);
            if (strLargeImg.Length > 0)
                createImg("coupon", strLargeImg);
            if (strPrintImg.Length > 0)
                createImg("coupon", strPrintImg);
            //写入数据库
            strSql = "update t_bz_coupon set strName='" + strName + "',dtActiveTime='" + dtActiveTime + "',dtExpireTime='" + dtExpireTime + "',strShopId='" + strShopId +
                "',intVip=" + intVip + ",intRecommend=" + intRecommend + ",flaPrice=" + flaPrice + ",strSmallImg='" + strSmallImg + "',strLargeImg='" + strLargeImg +
                "',strPrintImg='" + strPrintImg + "' where strId='" + strId + "'";
            cmd.ExecuteNonQuery(strSql);
            cmd.Close();
        }

        private void addCoupon(XmlNode xnCoupon)
        {
            String strId, strName, dtActiveTime, dtExpireTime, strShopId, strSmallImg, strLargeImg, strPrintImg;
            int intVip, intRecommend;
            float flaPrice;
            getCouponProps(xnCoupon, out strId, out strName, out dtActiveTime, out dtExpireTime, out strShopId, out intVip, out flaPrice, out intRecommend, 
                out strSmallImg, out strLargeImg, out strPrintImg);
            //创建文件
            if (strSmallImg.Length > 0)
                createImg("coupon", strSmallImg);
            if (strLargeImg.Length > 0)
                createImg("coupon", strLargeImg);
            if (strPrintImg.Length > 0)
                createImg("coupon", strPrintImg);
            //写入数据库
            AccessCmd cmd = new AccessCmd();
            string strSql = "insert into t_bz_coupon(strId,strName,dtActiveTime,dtExpireTime,strShopId,intVip,intRecommend,flaPrice,strSmallImg,strLargeImg,strPrintImg) " +
                "values('" + strId + "','" + strName + "','" + dtActiveTime + "','" + dtExpireTime + "','" + strShopId + "'," + intVip + "," + intRecommend + "," + flaPrice + 
                ",'" + strSmallImg + "','" + strLargeImg + "','" + strPrintImg + "')";
            cmd.ExecuteNonQuery(strSql);
            cmd.Close();
        }

        private void deleteShop(XmlNode xnShop)
        {
            String strId, strSmallImg = "", strLargeImg = "";
            XmlElement xeShop = (XmlElement)xnShop;
            strId = xeShop.GetElementsByTagName("strId").Item(0).InnerText.Trim();
            //查询文件
            string strSql = "select strSmallImg,strLargeImg from t_bz_shop where strId='" + strId + "'";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);
            if (reader.Read())
            {
                strSmallImg = reader.GetString(0);
                strLargeImg = reader.GetString(1);
            }
            reader.Close();
            //删除原文件
            if (!strSmallImg.Equals(""))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\shop\\" + strSmallImg);
            }
            if (!strLargeImg.Equals(""))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\shop\\" + strLargeImg);
            }
            //写入数据库
            strSql = "delete from t_bz_shop where strId='" + strId + "'";
            cmd.ExecuteNonQuery(strSql);
            cmd.Close();
        }

        private void updateShop(XmlNode xnShop)
        {
            String strId, strBizName, strShopName, strTrade, strAddr, strIntro, strSmallImg, strSmallImgOld = "", strLargeImg, strLargeImgOld = "";
            getShopProps(xnShop, out strId, out strBizName, out strShopName, out strTrade, out strAddr, out strIntro, out strSmallImg, out strLargeImg);
            //查询文件
            string strSql = "select strSmallImg,strLargeImg from t_bz_shop where strId='" + strId + "'";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);
            if (reader.Read())
            {
                strSmallImgOld = reader.GetString(0);
                strLargeImgOld = reader.GetString(1);
            }
            reader.Close();
            //删除原文件
            if (!strSmallImgOld.Equals(""))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\shop\\" + strSmallImgOld);
            }
            if (!strLargeImgOld.Equals(""))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\shop\\" + strLargeImgOld);
            }
            //创建文件
            if (strSmallImg.Length > 0)
                createImg("shop", strSmallImg);
            if (strLargeImg.Length > 0)
                createImg("shop", strLargeImg);
            //写入数据库
            strSql = "update t_bz_shop set strBizName='" + strBizName + "',strShopName='" + strShopName + "',strTrade='" + strTrade + "',strAddr='" + strAddr + 
                "',strIntro='" + strIntro + "',strSmallImg='" + strSmallImg + "',strLargeImg='" + strLargeImg + "' where strId='" + strId + "'";
            cmd.ExecuteNonQuery(strSql);
            cmd.Close();
        }

        private void addShop(XmlNode xnShop)
        {
            String strId, strBizName, strShopName, strTrade, strAddr, strIntro, strSmallImg, strLargeImg;
            getShopProps(xnShop, out strId, out strBizName, out strShopName, out strTrade, out strAddr, out strIntro, out strSmallImg, out strLargeImg);
            //创建文件
            if (strSmallImg.Length > 0)
                createImg("shop", strSmallImg);
            if (strLargeImg.Length > 0)
                createImg("shop", strLargeImg);
            //写入数据库
            AccessCmd cmd = new AccessCmd();
            string strSql = "insert into t_bz_shop(strId,strBizName,strShopName,strTrade,strAddr,strIntro,strSmallImg,strLargeImg) values('" + strId + "','" + strBizName + "','" + strShopName +
                "','" + strTrade + "','" + strAddr + "','" + strIntro + "','" + strSmallImg + "','" + strLargeImg + "')";
            cmd.ExecuteNonQuery(strSql);
            cmd.Close();
        }

        private static void createImg(String strType, String strImg)
        {
            ;
            WebRequest request = HttpWebRequest.Create(GlobalVariables.StrServerUrl + "/servlet/FileDownload?strFileType=" + strType + "&strFileName=" + strImg);
            Stream stream = request.GetResponse().GetResponseStream();
            byte[] bytes = new byte[2048];
            int i;
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + strType + "\\" + strImg, FileMode.CreateNew);
            while ((i = stream.Read(bytes, 0, 2048)) > 0)
            {
                fs.Write(bytes, 0, i);
            }
            stream.Close();
            fs.Close();
        }

        private static void getShopProps(XmlNode xnShop, out String strId, out String strBizName, out String strShopName, out String strTrade, out String strAddr, 
            out String strIntro, out String strSmallImg, out String strLargeImg)
        {
            XmlElement xeShop = (XmlElement)xnShop;
            strId = xeShop.GetElementsByTagName("strId").Item(0).InnerText.Trim();
            strBizName = xeShop.GetElementsByTagName("strBizName").Item(0).InnerText.Trim();
            strShopName = xeShop.GetElementsByTagName("strShopName").Item(0).InnerText.Trim();
            strTrade = xeShop.GetElementsByTagName("strTrade").Item(0).InnerText.Trim();
            strAddr = xeShop.GetElementsByTagName("strAddr").Item(0).InnerText.Trim();
            strIntro = xeShop.GetElementsByTagName("strIntro").Item(0).InnerText.Trim();
            strSmallImg = xeShop.GetElementsByTagName("strSmallImg").Item(0).InnerText.Trim();
            strLargeImg = xeShop.GetElementsByTagName("strLargeImg").Item(0).InnerText.Trim();
        }

        private static void getCouponProps(XmlNode xnCoupon, out string strId, out string strName, out string dtActiveTime, out string dtExpireTime, out string strShopId,
            out int intVip, out float flaPrice, out int intRecommend, out string strSmallImg, out string strLargeImg, out string strPrintImg)
        {
            XmlElement xeCoupon = (XmlElement)xnCoupon;
            strId = xeCoupon.GetElementsByTagName("strId").Item(0).InnerText.Trim();
            strName = xeCoupon.GetElementsByTagName("strName").Item(0).InnerText.Trim();
            dtActiveTime = xeCoupon.GetElementsByTagName("dtActiveTime").Item(0).InnerText.Trim();
            dtExpireTime = xeCoupon.GetElementsByTagName("dtExpireTime").Item(0).InnerText.Trim();
            strShopId = xeCoupon.GetElementsByTagName("strShopId").Item(0).InnerText.Trim();
            intVip = Int32.Parse(xeCoupon.GetElementsByTagName("intVip").Item(0).InnerText.Trim());
            flaPrice = float.Parse(xeCoupon.GetElementsByTagName("flaPrice").Item(0).InnerText.Trim());
            intRecommend = Int32.Parse(xeCoupon.GetElementsByTagName("intRecommend").Item(0).InnerText.Trim());
            strSmallImg = xeCoupon.GetElementsByTagName("strSmallImg").Item(0).InnerText.Trim();
            strLargeImg = xeCoupon.GetElementsByTagName("strLargeImg").Item(0).InnerText.Trim();
            strPrintImg = xeCoupon.GetElementsByTagName("strPrintImg").Item(0).InnerText.Trim();
        }

        private void getAdProps(XmlNode xn, out string strId, out string strName, out int intType, out string strContent, out string dtStartTime, out string dtEndTime)
        {
            XmlElement xe = (XmlElement)xn;
            strId = xe.GetElementsByTagName("strId").Item(0).InnerText.Trim();
            strName = xe.GetElementsByTagName("strName").Item(0).InnerText.Trim();
            intType = Int32.Parse(xe.GetElementsByTagName("intType").Item(0).InnerText.Trim());
            strContent = xe.GetElementsByTagName("strContent").Item(0).InnerText.Trim();
            dtStartTime = xe.GetElementsByTagName("dtStartTime").Item(0).InnerText.Trim();
            dtEndTime = xe.GetElementsByTagName("dtEndTime").Item(0).InnerText.Trim();
        }

        internal void SynParam()
        {
            //下载参数信息
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/TerminalParam?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
            string strXml = request.HtmlDocument;
            if (strXml.IndexOf("<params>") > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXml);
                //删除原纪录
                AccessCmd cmd = new AccessCmd();
                string strSql = "delete from t_bz_terminal_param";
                cmd.ExecuteNonQuery(strSql);
                //增加新纪录
                XmlNodeList xnl = doc.GetElementsByTagName("param");
                for (int i = 0; i < xnl.Count; i++)
                {
                    XmlElement xe = (XmlElement)xnl.Item(i);
                    //获取信息
                    string strId = xe.ChildNodes.Item(0).InnerText.Trim();
                    string strParamName = xe.ChildNodes.Item(1).InnerText.Trim();
                    string strParamValue = xe.ChildNodes.Item(2).InnerText.Trim();
                    //更新数据库
                    strSql = "insert into t_bz_terminal_param (strId,strParamName,strParamValue) values ('" + strId + "','" + strParamName + "','" + strParamValue + "')";
                    cmd.ExecuteNonQuery(strSql);
                }
                cmd.Close();
            }
        }

        internal string[] CouponTop()
        {
            //下载参数信息
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/CouponTop?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
            string strXml = request.HtmlDocument;
            if (strXml.IndexOf("<coupons>") > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXml);
                XmlNodeList xnl = doc.GetElementsByTagName("coupon");
                string[] aryCouponId = new string[xnl.Count];
                for (int i = 0; i < xnl.Count; i++)
                {
                    aryCouponId[i] = xnl.Item(i).InnerText.Trim();
                }
                return aryCouponId;
            }
            else
            {
                return new string[0];
            }
        }
    }
}
