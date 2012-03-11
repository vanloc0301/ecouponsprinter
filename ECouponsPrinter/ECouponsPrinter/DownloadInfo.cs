using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Net;
using System.Drawing;

namespace ECouponsPrinter
{
    class DownloadInfo
    {
        private HttpRequest request = new HttpRequest();
        private MainFrame form;

        public DownloadInfo(MainFrame f) 
        {
            form = f;
        }

        public void download()
        {
            //下载商家信息
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/ShopDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
            XmlDocument doc = new XmlDocument();
            string strXml = request.HtmlDocument;
            //MessageBox.Show(strXml);
            bool result = true;
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
                            result = result && addShop(xnShop);
                        }
                    }
                    else if (strOpe.Equals("update"))
                    {
                        foreach (XmlNode xnShop in xeShops.GetElementsByTagName("shop"))
                        {
                            result = result && updateShop(xnShop);
                        }
                    }
                    else if (strOpe.Equals("delete"))
                    {
                        foreach (XmlNode xnShop in xeShops.GetElementsByTagName("shop"))
                        {
                            result = result && deleteShop(xnShop);
                        }
                    }
                }
            }
            //返回本地操作结果
            string strReturn="NO";
            if (result)
            {
                strReturn="OK";
            }
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/ShopDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo + "&strReturn="+strReturn, "");
            strXml = request.HtmlDocument;
            //下载优惠券信息
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/CouponDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
            strXml = request.HtmlDocument;
            result = true;
            //MessageBox.Show(strXml);
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
                            result = result && addCoupon(xnCoupon);
                        }
                    }
                    else if (strOpe.Equals("update"))
                    {
                        foreach (XmlNode xnCoupon in xeCoupons.GetElementsByTagName("coupon"))
                        {
                            result = result && updateCoupon(xnCoupon);
                        }
                    }
                    else if (strOpe.Equals("delete"))
                    {
                        foreach (XmlNode xnCoupon in xeCoupons.GetElementsByTagName("coupon"))
                        {
                            result = result && deleteCoupon(xnCoupon);
                        }
                    }
                }
            }
            //返回本地操作结果
            strReturn = "NO";
            if (result)
            {
                strReturn = "OK";
            }
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/CouponDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo + "&strReturn=" + strReturn, "");
            strXml = request.HtmlDocument;
            //下载广告信息
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/AdDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
            strXml = request.HtmlDocument;
            result = true;
            //MessageBox.Show(strXml);
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
                            result = result && addAd(xn);
                        }
                    }
                    else if (strOpe.Equals("update"))
                    {
                        foreach (XmlNode xn in xes.GetElementsByTagName("ad"))
                        {
                            result = result && updateAd(xn);
                        }
                    }
                    else if (strOpe.Equals("delete"))
                    {
                        foreach (XmlNode xn in xes.GetElementsByTagName("ad"))
                        {
                            result = result && deleteAd(xn);
                        }
                    }
                }
            }
            //返回本地操作结果
            strReturn = "NO";
            if (result)
            {
                strReturn = "OK";
            }
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/AdDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo + "&strReturn=" + strReturn, "");
            strXml = request.HtmlDocument;
        }

        private bool deleteAd(XmlNode xn)
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
            bool result = cmd.ExecuteNonQuery(strSql);                
            cmd.Close();
            return result;
        }

        private bool updateAd(XmlNode xn)
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
            try
            {
                if (intTypeOld == 1)
                {
                    if (!strContentOld.Equals(strContent))
                    {
                        if(!strContentOld.Equals(""))
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\ad\\" + strContentOld);
                        if (!createImg("ad", strContent))
                            return false;

                    }
                }
                else if (intTypeOld == 2)
                {
                    if (!strContentOld.Equals(strContent))
                    {
                        string[] aryFile = strContentOld.Split(new char[] { ',' });
                        for (int i = 0; i < aryFile.Length; i++)
                        {
                            if(!aryFile[i].Equals(""))
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\ad\\" + aryFile[i]);
                        }
                        aryFile = strContent.Split(new char[] { ',' });
                        for (int i = 0; i < aryFile.Length; i++)
                        {
                            if (!createImg("ad", aryFile[i]))
                                return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.log(e);
                return false;
            }
            finally
            {
                //删除原纪录
                strSql = "delete from t_bz_advertisement where strId='" + strId + "'";
                cmd.ExecuteNonQuery(strSql);
                cmd.Close();
            }
            cmd = new AccessCmd();
            //写入数据库（先删除、后增加，保证之前已有的信息可下载）
            strSql = "insert into t_bz_advertisement(strId,strName,intType,strContent,dtStartTime,dtEndTime) values('" + strId + "','" + strName + "'," + intType +
                ",'" + strContent + "','" + dtStartTime + "','" + dtEndTime + "')";
            //MessageBox.Show(strSql);
            bool result = cmd.ExecuteNonQuery(strSql);
            cmd.Close();
            return result;
        }

        private bool addAd(XmlNode xn)
        {
            String strId, strName, strContent, dtStartTime, dtEndTime;
            int intType;
            getAdProps(xn, out strId, out strName, out intType, out strContent, out dtStartTime, out dtEndTime);
            //创建文件
            if (intType == 1)
            {
                if(!createImg("ad", strContent))
                    return false;
            }
            else if (intType == 2)
            {
                string[] aryFile = strContent.Split(new char[] { ',' });
                for (int i = 0; i < aryFile.Length; i++)
                {
                    if(!createImg("ad", aryFile[i]))
                        return false;
                }
            }
            //写入数据库
            AccessCmd cmd = new AccessCmd();
            string strSql = "insert into t_bz_advertisement(strId,strName,intType,strContent,dtStartTime,dtEndTime) values('" + strId + "','" + strName + "'," + intType +
                ",'" + strContent + "','" + dtStartTime + "','" + dtEndTime + "')";
            bool result = cmd.ExecuteNonQuery(strSql);
            cmd.Close();
            return result;
        }

        private bool deleteCoupon(XmlNode xnCoupon)
        {
            String strId, strSmallImg = "", strLargeImg = "";
            XmlElement xeCoupon = (XmlElement)xnCoupon;
            strId = xeCoupon.GetElementsByTagName("strId").Item(0).InnerText.Trim();
            //查询文件
            string strSql = "select strSmallImg,strLargeImg from t_bz_coupon where strId='" + strId + "'";
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
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strSmallImg);
            }
            if (!strLargeImg.Equals(""))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strLargeImg);
            }
            //写入数据库
            strSql = "delete from t_bz_coupon where strId='" + strId + "'";
            bool result = cmd.ExecuteNonQuery(strSql);
            cmd.Close();
            return result;
        }

        private bool updateCoupon(XmlNode xnCoupon)
        {
            String strId, strName, dtActiveTime, dtExpireTime, strShopId, strSmallImg, strLargeImg, strPrintImg, strSmallImgOld = "", strLargeImgOld = "", strIntro, strInstruction;
            int intVip, intRecommend;
            float flaPrice;
            getCouponProps(xnCoupon, out strId, out strName, out dtActiveTime, out dtExpireTime, out strShopId, out intVip, out flaPrice, out intRecommend,
                out strSmallImg, out strLargeImg, out strIntro, out strInstruction);
            //查询文件
            string strSql = "select strSmallImg,strLargeImg from t_bz_coupon where strId='" + strId + "'";
            AccessCmd cmd = new AccessCmd();
            OleDbDataReader reader = cmd.ExecuteReader(strSql);
            if (reader.Read())
            {
                strSmallImgOld = reader.GetString(0);
                strLargeImgOld = reader.GetString(1);
            }
            reader.Close();
            //文件操作
            try
            {
                if(!strSmallImg.Equals(strSmallImgOld))
                {
                    if(!strSmallImgOld.Equals(""))
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strSmallImgOld);
                    if (!createImg("coupon", strSmallImg))
                        return false;
                }
                if (!strLargeImgOld.Equals(strLargeImg))
                {
                    if(!strLargeImgOld.Equals(""))
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strLargeImgOld);
                    if (!createImg("coupon", strLargeImg))
                        return false;
                }
            }
            catch (Exception e)
            {
                ErrorLog.log(e);
                return false;
            }
            finally
            {
                //删除原纪录
                strSql = "delete from t_bz_coupon where strId='" + strId + "'";
                cmd.ExecuteNonQuery(strSql);
                cmd.Close();
            }
            cmd = new AccessCmd();
            //写入数据库（先删除、后增加，保证之前已有的信息可下载）
            strSql = "insert into t_bz_coupon(strId,strName,dtActiveTime,dtExpireTime,strShopId,intVip,intRecommend,flaPrice,strSmallImg,strLargeImg,strIntro,strInstruction) " +
                "values('" + strId + "','" + strName + "','" + dtActiveTime + "','" + dtExpireTime + "','" + strShopId + "'," + intVip + "," + intRecommend + "," + flaPrice +
                ",'" + strSmallImg + "','" + strLargeImg + "','" + strIntro + "','" + strInstruction + "')";
            bool result = cmd.ExecuteNonQuery(strSql);
            cmd.Close();
            return result;
        }

        private bool addCoupon(XmlNode xnCoupon)
        {
            String strId, strName, dtActiveTime, dtExpireTime, strShopId, strSmallImg, strLargeImg, strIntro, strInstruction;
            int intVip, intRecommend;
            float flaPrice;
            getCouponProps(xnCoupon, out strId, out strName, out dtActiveTime, out dtExpireTime, out strShopId, out intVip, out flaPrice, out intRecommend,
                out strSmallImg, out strLargeImg, out strIntro, out strInstruction);
            //创建文件
            if (strSmallImg.Length > 0)
                if(!createImg("coupon", strSmallImg))
                    return false;
            if (strLargeImg.Length > 0)
                if(!createImg("coupon", strLargeImg))
                    return false;
            //写入数据库
            AccessCmd cmd = new AccessCmd();
            string strSql = "insert into t_bz_coupon(strId,strName,dtActiveTime,dtExpireTime,strShopId,intVip,intRecommend,flaPrice,strSmallImg,strLargeImg,strIntro,strInstruction) " +
                "values('" + strId + "','" + strName + "','" + dtActiveTime + "','" + dtExpireTime + "','" + strShopId + "'," + intVip + "," + intRecommend + "," + flaPrice +
                ",'" + strSmallImg + "','" + strLargeImg + "','" + strIntro + "','" + strInstruction + "')";
            bool result = cmd.ExecuteNonQuery(strSql);
            cmd.Close();
            return result;
        }

        private bool deleteShop(XmlNode xnShop)
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
            bool result = cmd.ExecuteNonQuery(strSql);
            cmd.Close();
            return result;
        }

        private bool updateShop(XmlNode xnShop)
        {
            String strId, strBizName, strShopName, strTrade, strAddr, strIntro, strSmallImg, strSmallImgOld = "", strLargeImg, strLargeImgOld = "", intType="0";
            getShopProps(xnShop, out strId, out strBizName, out strShopName, out strTrade, out strAddr, out strIntro, out strSmallImg, out strLargeImg, out intType);
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
            try
            {
                if (!strSmallImgOld.Equals(strSmallImg))
                {
                    if(!strSmallImgOld.Equals(""))
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\shop\\" + strSmallImgOld);
                    if (!createImg("shop", strSmallImg))
                        return false;
                }
                if (!strLargeImgOld.Equals(strLargeImg))
                {
                    if(!strLargeImgOld.Equals(""))
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\shop\\" + strLargeImgOld);
                    if (!createImg("shop", strLargeImg))
                        return false;
                }
            }
            catch (Exception e)
            {
                ErrorLog.log(e);
                return false;
            }
            finally
            {
                //删除原纪录
                strSql = "delete from t_bz_shop where strId='" + strId + "'";
                cmd.ExecuteNonQuery(strSql);
                cmd.Close();
            }
            cmd = new AccessCmd();
            //写入数据库（先删除、后增加，保证之前已有的信息可下载）
            strSql = "insert into t_bz_shop(strId,strBizName,strShopName,strTrade,strAddr,strIntro,strSmallImg,strLargeImg,intType) values('" + strId + "','" + strBizName + "','" +
                strShopName + "','" + strTrade + "','" + strAddr + "','" + strIntro + "','" + strSmallImg + "','" + strLargeImg + "'," + intType + ")";
            bool result = cmd.ExecuteNonQuery(strSql);
            cmd.Close();
            return true;
        }

        private bool addShop(XmlNode xnShop)
        {
            String strId, strBizName, strShopName, strTrade, strAddr, strIntro, strSmallImg, strLargeImg, intType;
            getShopProps(xnShop, out strId, out strBizName, out strShopName, out strTrade, out strAddr, out strIntro, out strSmallImg, out strLargeImg, out intType);
            //创建文件
            if (strSmallImg.Length > 0)
                if(!createImg("shop", strSmallImg))
                    return false;
            if (strLargeImg.Length > 0)
                if(!createImg("shop", strLargeImg))
                    return false;
            //写入数据库
            AccessCmd cmd = new AccessCmd();
            string strSql = "insert into t_bz_shop(strId,strBizName,strShopName,strTrade,strAddr,strIntro,strSmallImg,strLargeImg,intType) values('" + strId + "','" + strBizName + "','" + 
                strShopName + "','" + strTrade + "','" + strAddr + "','" + strIntro + "','" + strSmallImg + "','" + strLargeImg + "'," + intType + ")";
            bool result = cmd.ExecuteNonQuery(strSql);
            cmd.Close();
            return result;
        }

        private static bool createImg(String strType, String strImg)
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
            if (!strImg.ToLower().EndsWith("wmv"))
            {
                //测试图像文件是否受损
                PictureBox pb = new PictureBox();
                FileStream pFileStream = new FileStream(System.Windows.Forms.Application.StartupPath + "\\" + strType + "\\" + strImg, FileMode.Open, FileAccess.Read);
                try
                {
                    pb.Image = new Bitmap(Image.FromStream(pFileStream), 760, 407);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
                finally
                {
                    pFileStream.Close();
                    pb.Dispose();
                    pb = null;
                }
            }
            return true;
        }

        private static void getShopProps(XmlNode xnShop, out String strId, out String strBizName, out String strShopName, out String strTrade, out String strAddr,
            out String strIntro, out String strSmallImg, out String strLargeImg, out String intType)
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
            intType = xeShop.GetElementsByTagName("intType").Item(0).InnerText.Trim();
        }

        private static void getCouponProps(XmlNode xnCoupon, out string strId, out string strName, out string dtActiveTime, out string dtExpireTime, out string strShopId,
            out int intVip, out float flaPrice, out int intRecommend, out string strSmallImg, out string strLargeImg, out string strIntro, out string strInstruction)
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
            strIntro = xeCoupon.GetElementsByTagName("strIntro").Item(0).InnerText.Trim();
            strInstruction = xeCoupon.GetElementsByTagName("strInstruction").Item(0).InnerText.Trim();
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
                string strSql = "delete from t_bz_terminal_param where strParamName<>'strTerminalNo' and strParamName<>'strServerUrl'";
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
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/CouponTop?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
            string strXml = request.HtmlDocument;
            //MessageBox.Show(strXml);
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

        internal string[] ShopAround()
        {
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/ShopAround?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
            string strXml = request.HtmlDocument;
            if (strXml.IndexOf("<shops>") > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXml);
                XmlNodeList xnl = doc.GetElementsByTagName("shop");
                string[] aryShopId = new string[xnl.Count];
                for (int i = 0; i < xnl.Count; i++)
                {
                    aryShopId[i] = xnl.Item(i).FirstChild.InnerText.Trim();
                }
                return aryShopId;
            }
            else
            {
                return new string[0];
            }
        }
    }
}
