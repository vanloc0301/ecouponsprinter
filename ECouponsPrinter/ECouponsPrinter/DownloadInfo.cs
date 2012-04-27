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
            AccessCmd cmd = new AccessCmd();
            //下载商家信息
            try
            {
                request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/ShopDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
                XmlDocument doc = new XmlDocument();
                string strXml = request.HtmlDocument;
                if (strXml.IndexOf("<shops>") > 0)
                {
                    //加载服务器信息
                    doc.LoadXml(strXml);
                    XmlNodeList xnlShop = doc.GetElementsByTagName("shop");
                    //加载本地信息
                    string strSql = "select strId,strSmallImg,strLargeImg from t_bz_shop";
                    OleDbDataReader reader=cmd.ExecuteReader(strSql);
                    List<Shop> lstShop=new List<Shop>();
                    while (reader.Read())
                    {
                        Shop shop = new Shop(reader);
                        lstShop.Add(shop);
                    }
                    reader.Close();
                    //依次对比服务器与本地信息，保证两者一致
                    for (int i = 0; i < xnlShop.Count; i++)
                    {
                        try
                        {
                            //获得服务器记录信息
                            XmlElement xeShop = (XmlElement)xnlShop.Item(i);
                            String strId, strBizName, strShopName, strTrade, strAddr, strIntro, strSmallImg, strLargeImg, intType, intSort;
                            getShopProps(xeShop, out strId, out strBizName, out strShopName, out strTrade, out strAddr, out strIntro, out strSmallImg, out strLargeImg, out intType, out intSort);
                            //比对本地信息
                            for (int j = 0; j < lstShop.Count; j++)
                            {
                                Shop localShop = lstShop.ElementAt(j);
                                if (localShop.StrId.Equals(strId))
                                {
                                    //找到本地信息，先更新图片
                                    bool bolImg = true;
                                    if (strSmallImg.Length > 0 && !strSmallImg.Equals(localShop.StrSmallImg))
                                    {
                                        bolImg = createImg("shop", strSmallImg);
                                    }
                                    if (bolImg && strLargeImg.Length > 0 && !strLargeImg.Equals(localShop.StrLargeImg))
                                    {
                                        bolImg = createImg("shop", strLargeImg);
                                    }
                                    //考虑数据库中有记录，但图片不存在的情况
                                    if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\shop\\" + strSmallImg))
                                    {
                                        bolImg = bolImg && createImg("shop", strSmallImg);
                                    }
                                    if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\shop\\" + strLargeImg))
                                    {
                                        bolImg = bolImg && createImg("shop", strLargeImg);
                                    }
                                    //图片操作正常，删除原图片并更新数据库
                                    if (bolImg)
                                    {
                                        cmd.ExecuteNonQuery("update t_bz_shop set strBizName='" + strBizName + "',strShopName='" + strShopName + "',strTrade='" + strTrade + "',strAddr='" + strAddr +
                                            "',strIntro='" + strIntro + "',strSmallImg='" + strSmallImg + "',strLargeImg='" + strLargeImg + "',intType=" + intType + ",intSort=" + intSort +
                                            " where strId='" + strId + "'");
                                        if(!strSmallImg.Equals(localShop.StrSmallImg))
                                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\shop\\" + localShop.StrSmallImg);
                                        if (!strLargeImg.Equals(localShop.StrLargeImg))
                                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\shop\\" + localShop.StrLargeImg);
                                    }
                                    strId = "";
                                    lstShop.Remove(localShop);
                                    break;
                                }
                            }
                            if (strId.Length > 0)
                            {
                                //本地没有找到，先增加图片
                                bool bolImg = true;
                                if (strSmallImg.Length > 0)
                                {
                                    bolImg = createImg("shop", strSmallImg);
                                }
                                if (bolImg && strLargeImg.Length > 0)
                                {
                                    bolImg = createImg("shop", strLargeImg);
                                }
                                //图片操作正常，更新数据库
                                if (bolImg)
                                {
                                    cmd.ExecuteNonQuery("insert into t_bz_shop(strId,strBizName,strShopName,strTrade,strAddr,strIntro,strSmallImg,strLargeImg,intType,intSort) values('" +
                                        strId + "','" + strBizName + "','" + strShopName + "','" + strTrade + "','" + strAddr + "','" + strIntro + "','" + strSmallImg + "','" + strLargeImg +
                                        "'," + intType + "," + intSort + ")");
                                }
                            }
                        }
                        catch(Exception e)
                        {
                            ErrorLog.log(e);
                        }
                    }
                    //删除本地没有匹配的记录
                    for (int i = 0; i < lstShop.Count; i++)
                    {
                        Shop localShop = lstShop.ElementAt(i);
                        cmd.ExecuteNonQuery("delete from t_bz_shop where strId='" + localShop.StrId + "'");
                        if(localShop.StrSmallImg.Length>0)
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\shop\\" + localShop.StrSmallImg);
                        if(localShop.StrLargeImg.Length>0)
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\shop\\" + localShop.StrLargeImg);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.log(e);
            }

            //下载优惠券信息
            try
            {
                request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/CouponDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
                XmlDocument doc = new XmlDocument();
                string strXml = request.HtmlDocument;
                if (strXml.IndexOf("<coupons>") > 0)
                {
                    //加载服务器信息
                    doc.LoadXml(strXml);
                    XmlNodeList xnlCoupon = doc.GetElementsByTagName("coupon");
                    //加载本地信息
                    string strSql = "select strId,strSmallImg,strLargeImg,strPrintImg from t_bz_coupon";
                    OleDbDataReader reader = cmd.ExecuteReader(strSql);
                    List<Coupon> lstCoupon = new List<Coupon>();
                    while (reader.Read())
                    {
                        Coupon coupon = new Coupon(reader);
                        lstCoupon.Add(coupon);
                    }
                    reader.Close();
                    //依次对比服务器与本地信息，保证两者一致
                    for (int i = 0; i < xnlCoupon.Count; i++)
                    {
                        try
                        {
                            //获得服务器记录信息
                            XmlElement xeCoupon = (XmlElement)xnlCoupon.Item(i);
                            String strId, strName, dtActiveTime, dtExpireTime, strShopId, strSmallImg, strLargeImg, strPrintImg, strIntro, strInstruction;
                            int intVip, intRecommend;
                            float flaPrice;
                            getCouponProps(xeCoupon, out strId, out strName, out dtActiveTime, out dtExpireTime, out strShopId, out intVip, out flaPrice, out intRecommend,
                                out strSmallImg, out strLargeImg, out strPrintImg, out strIntro, out strInstruction);
                            //比对本地信息
                            for (int j = 0; j < lstCoupon.Count; j++)
                            {
                                Coupon localCoupon = lstCoupon.ElementAt(j);
                                if (localCoupon.StrId.Equals(strId))
                                {
                                    //找到本地信息，先更新图片
                                    bool bolImg = true;
                                    if (strSmallImg.Length > 0 && !strSmallImg.Equals(localCoupon.StrSmallImg))
                                    {
                                        bolImg = createImg("coupon", strSmallImg);
                                    }
                                    if (bolImg && strLargeImg.Length > 0 && !strLargeImg.Equals(localCoupon.StrLargeImg))
                                    {
                                        bolImg = createImg("coupon", strLargeImg);
                                    }
                                    if (bolImg && strPrintImg.Length > 0 && !strPrintImg.Equals(localCoupon.StrPrintImg))
                                    {
                                        bolImg = createImg("coupon", strPrintImg);
                                    }
                                    //考虑数据库中有记录，但图片不存在的情况
                                    if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strSmallImg))
                                    {
                                        bolImg = bolImg && createImg("coupon", strSmallImg);
                                    }
                                    if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strLargeImg))
                                    {
                                        bolImg = bolImg && createImg("coupon", strLargeImg);
                                    }
                                    if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + strPrintImg))
                                    {
                                        bolImg = bolImg && createImg("coupon", strPrintImg);
                                    }
                                    //图片操作正常，删除原图片并更新数据库
                                    if (bolImg)
                                    {
                                        cmd.ExecuteNonQuery("update t_bz_coupon set strName='" + strName + "',dtActiveTime='" + dtActiveTime + "',dtExpireTime='" + dtExpireTime +
                                            "',strShopId='" + strShopId + "',intVip=" + intVip + ",intRecommend=" + intRecommend + ",flaPrice=" + flaPrice + ",strSmallImg='" + strSmallImg +
                                            "',strLargeImg='" + strLargeImg + "',strPrintImg='" + strPrintImg + "',strIntro='" + strIntro + "',strInstruction='" + strInstruction + 
                                            "' where strId='" + strId + "'");
                                        if(!strSmallImg.Equals(localCoupon.StrSmallImg))
                                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + localCoupon.StrSmallImg);
                                        if(!strLargeImg.Equals(localCoupon.StrLargeImg))
                                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + localCoupon.StrLargeImg);
                                        if(!strPrintImg.Equals(localCoupon.StrPrintImg))
                                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + localCoupon.StrPrintImg);
                                    }
                                    strId = "";
                                    lstCoupon.Remove(localCoupon);
                                    break;
                                }
                            }
                            if (strId.Length > 0)
                            {
                                //本地没有找到，先增加图片
                                bool bolImg = true;
                                if (strSmallImg.Length > 0)
                                {
                                    bolImg = createImg("coupon", strSmallImg);
                                }
                                if (bolImg && strLargeImg.Length > 0)
                                {
                                    bolImg = createImg("coupon", strLargeImg);
                                }
                                if (bolImg && strPrintImg.Length > 0)
                                {
                                    bolImg = createImg("coupon", strPrintImg);
                                }
                                //图片操作正常，更新数据库
                                if (bolImg)
                                {
                                    cmd.ExecuteNonQuery("insert into t_bz_coupon(strId,strName,dtActiveTime,dtExpireTime,strShopId,intVip,intRecommend,flaPrice,strSmallImg,strLargeImg,"+
                                        "strPrintImg,strIntro,strInstruction) values('" + strId + "','" + strName + "','" + dtActiveTime + "','" + dtExpireTime + "','" + strShopId + "'," + 
                                        intVip + "," + intRecommend + "," + flaPrice + ",'" + strSmallImg + "','" + strLargeImg + "','" + strPrintImg + "','" + strIntro + "','" + 
                                        strInstruction + "')");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            ErrorLog.log(e);
                        }
                    }
                    //删除本地没有匹配的记录
                    for (int i = 0; i < lstCoupon.Count; i++)
                    {
                        Coupon localCoupon = lstCoupon.ElementAt(i);
                        cmd.ExecuteNonQuery("delete from t_bz_coupon where strId='" + localCoupon.StrId + "'");
                        if(localCoupon.StrSmallImg.Length>0)
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + localCoupon.StrSmallImg);
                        if (localCoupon.StrLargeImg.Length > 0)
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + localCoupon.StrLargeImg);
                        if (localCoupon.StrPrintImg.Length > 0)
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\coupon\\" + localCoupon.StrPrintImg);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.log(e);
            }

            //下载广告信息
            try
            {
                request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/AdDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
                XmlDocument doc = new XmlDocument();
                string strXml = request.HtmlDocument;
                if (strXml.IndexOf("<ads>") > 0)
                {
                    //加载服务器信息
                    doc.LoadXml(strXml);
                    XmlNodeList xnlAd = doc.GetElementsByTagName("ad");
                    //加载本地信息
                    string strSql = "select strId,intType,strContent from t_bz_advertisement";
                    OleDbDataReader reader = cmd.ExecuteReader(strSql);
                    List<Advertisement> lstAd = new List<Advertisement>();
                    while (reader.Read())
                    {
                        Advertisement ad = new Advertisement(reader);
                        lstAd.Add(ad);
                    }
                    reader.Close();
                    //依次对比服务器与本地信息，保证两者一致
                    for (int i = 0; i < xnlAd.Count; i++)
                    {
                        try
                        {
                            //获得服务器记录信息
                            XmlElement xeAd = (XmlElement)xnlAd.Item(i);
                            String strId, strName, strContent, dtStartTime, dtEndTime;
                            int intType;
                            getAdProps(xeAd, out strId, out strName, out intType, out strContent, out dtStartTime, out dtEndTime);
                            //比对本地信息
                            for (int j = 0; j < lstAd.Count; j++)
                            {
                                Advertisement localAd = lstAd.ElementAt(j);
                                if (localAd.StrId.Equals(strId))
                                {
                                    //找到本地信息，先更新图片
                                    bool bolImg = true;
                                    if (intType < 3 && strContent.Length > 0 && !strContent.Equals(localAd.StrContent))
                                    {
                                        if (intType == 1)
                                        {
                                            bolImg = createImg("ad", strContent);
                                        }
                                        else if (intType == 2)
                                        {
                                            string[] aryFile = strContent.Split(new char[] { ',' });
                                            for (int k = 0; k < aryFile.Length; k++)
                                            {
                                                bolImg = bolImg && createImg("ad", aryFile[k]);
                                            }
                                        }
                                    }
                                    //考虑数据库中有记录，但图片不存在的情况
                                    if (intType == 1 && !File.Exists(System.Windows.Forms.Application.StartupPath + "\\ad\\" + strContent))
                                    {
                                        bolImg = bolImg && createImg("ad", strContent);
                                    }
                                    else if (intType==2)
                                    {
                                        string[] aryFile = strContent.Split(new char[] { ',' });
                                        for (int k = 0; k < aryFile.Length; k++)
                                        {
                                            if(!File.Exists(System.Windows.Forms.Application.StartupPath + "\\ad\\" + aryFile[k]))
                                            {
                                                bolImg = bolImg && createImg("ad", aryFile[k]);
                                            }
                                        }
                                        
                                    }
                                    //图片操作正常，删除原图片并更新数据库
                                    if (bolImg)
                                    {
                                        cmd.ExecuteNonQuery("update t_bz_advertisement set strName='" + strName + "',intType=" + intType + ",strContent='" + strContent +
                                            "',dtStartTime='" + dtStartTime + "',dtEndTime='" + dtEndTime + "' where strId='" + strId + "'");
                                        if (localAd.IntType == 1 && !strContent.Equals(localAd.StrContent))
                                        {
                                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\ad\\" + localAd.StrContent);
                                        }
                                        else if (localAd.IntType == 2 && !strContent.Equals(localAd.StrContent))
                                        {
                                            string[] aryFile = localAd.StrContent.Split(new char[] { ',' });
                                            for (int k = 0; k < aryFile.Length; k++)
                                            {
                                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\ad\\" + aryFile[k]);
                                            }
                                        }
                                    }
                                    strId = "";
                                    lstAd.Remove(localAd);
                                    break;
                                }
                            }
                            if (strId.Length > 0)
                            {
                                //本地没有找到，先增加图片
                                bool bolImg = true;
                                if (intType < 3 && strContent.Length > 0)
                                {
                                    if (intType == 1)
                                    {
                                        bolImg = createImg("ad", strContent);
                                    }
                                    else if (intType == 2)
                                    {
                                        string[] aryFile = strContent.Split(new char[] { ',' });
                                        for (int k = 0; k < aryFile.Length; k++)
                                        {
                                            bolImg = bolImg && createImg("ad", aryFile[k]);
                                        }
                                    }
                                }
                                //图片操作正常，更新数据库
                                if (bolImg)
                                {
                                    cmd.ExecuteNonQuery("insert into t_bz_advertisement(strId,strName,intType,strContent,dtStartTime,dtEndTime) values('" + strId + "','" + strName + "'," + 
                                        intType + ",'" + strContent + "','" + dtStartTime + "','" + dtEndTime + "')");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            ErrorLog.log(e);
                        }
                    }
                    //删除本地没有匹配的记录
                    for (int i = 0; i < lstAd.Count; i++)
                    {
                        Advertisement localAd = lstAd.ElementAt(i);
                        cmd.ExecuteNonQuery("delete from t_bz_advertisement where strId='" + localAd.StrId + "'");
                        if (localAd.StrContent.Length > 0)
                        {
                            if (localAd.IntType == 1)
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\ad\\" + localAd.StrContent);
                            }
                            else if (localAd.IntType == 2)
                            {
                                string[] aryFile = localAd.StrContent.Split(new char[] { ',' });
                                for (int k = 0; k < aryFile.Length; k++)
                                {
                                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\ad\\" + aryFile[k]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.log(e);
            }
            cmd.Close();
        }

        private static bool createImg(String strType, String strImg)
        {
            if (strImg == null || strImg.Equals("null") || strImg.Length==0)    //如果null或空，直接返回
                return true;
            WebRequest request = HttpWebRequest.Create(GlobalVariables.StrServerUrl + "/servlet/FileDownload?strFileType=" + strType + "&strFileName=" + strImg);
            Stream stream = request.GetResponse().GetResponseStream();
            byte[] bytes = new byte[2048];
            int i;
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + strType + "\\" + strImg))
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + strType + "\\" + strImg);
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
                    StreamWriter sw = File.AppendText(System.Windows.Forms.Application.StartupPath + "\\error.log");
                    sw.WriteLine("[File]" + strType+"  "+strImg);
                    sw.Close();
                    ErrorLog.log(e);
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + strType + "\\" + strImg);
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
            out String strIntro, out String strSmallImg, out String strLargeImg, out String intType, out String intSort)
        {
            XmlElement xeShop = (XmlElement)xnShop;
            strId = xeShop.GetElementsByTagName("strId").Item(0).InnerText.Trim();
            strBizName = xeShop.GetElementsByTagName("strBizName").Item(0).InnerText.Trim().Replace("'","''");
            strShopName = xeShop.GetElementsByTagName("strShopName").Item(0).InnerText.Trim().Replace("'", "''");
            strTrade = xeShop.GetElementsByTagName("strTrade").Item(0).InnerText.Trim().Replace("'", "''");
            strAddr = xeShop.GetElementsByTagName("strAddr").Item(0).InnerText.Trim().Replace("'", "''");
            strIntro = xeShop.GetElementsByTagName("strIntro").Item(0).InnerText.Trim().Replace("'", "''");
            strSmallImg = xeShop.GetElementsByTagName("strSmallImg").Item(0).InnerText.Trim();
            strLargeImg = xeShop.GetElementsByTagName("strLargeImg").Item(0).InnerText.Trim();
            intType = xeShop.GetElementsByTagName("intType").Item(0).InnerText.Trim();
            try
            {
                intSort = xeShop.GetElementsByTagName("intSort").Item(0).InnerText.Trim();
            }
            catch (Exception e)
            {
                intSort = "0";
            }
        }

        private static void getCouponProps(XmlNode xnCoupon, out string strId, out string strName, out string dtActiveTime, out string dtExpireTime, out string strShopId,
            out int intVip, out float flaPrice, out int intRecommend, out string strSmallImg, out string strLargeImg, out string strPrintImg, out string strIntro, out string strInstruction)
        {
            XmlElement xeCoupon = (XmlElement)xnCoupon;
            strId = xeCoupon.GetElementsByTagName("strId").Item(0).InnerText.Trim();
            strName = xeCoupon.GetElementsByTagName("strName").Item(0).InnerText.Trim().Replace("'", "''");
            dtActiveTime = xeCoupon.GetElementsByTagName("dtActiveTime").Item(0).InnerText.Trim();
            dtExpireTime = xeCoupon.GetElementsByTagName("dtExpireTime").Item(0).InnerText.Trim();
            strShopId = xeCoupon.GetElementsByTagName("strShopId").Item(0).InnerText.Trim();
            intVip = Int32.Parse(xeCoupon.GetElementsByTagName("intVip").Item(0).InnerText.Trim());
            flaPrice = float.Parse(xeCoupon.GetElementsByTagName("flaPrice").Item(0).InnerText.Trim());
            intRecommend = Int32.Parse(xeCoupon.GetElementsByTagName("intRecommend").Item(0).InnerText.Trim());
            strSmallImg = xeCoupon.GetElementsByTagName("strSmallImg").Item(0).InnerText.Trim();
            strLargeImg = xeCoupon.GetElementsByTagName("strLargeImg").Item(0).InnerText.Trim();
            strPrintImg = xeCoupon.GetElementsByTagName("strPrintImg").Item(0).InnerText.Trim();
            strIntro = xeCoupon.GetElementsByTagName("strIntro").Item(0).InnerText.Trim().Replace("'", "''");
            strInstruction = xeCoupon.GetElementsByTagName("strInstruction").Item(0).InnerText.Trim().Replace("'", "''");
        }

        private void getAdProps(XmlNode xn, out string strId, out string strName, out int intType, out string strContent, out string dtStartTime, out string dtEndTime)
        {
            XmlElement xe = (XmlElement)xn;
            strId = xe.GetElementsByTagName("strId").Item(0).InnerText.Trim();
            strName = xe.GetElementsByTagName("strName").Item(0).InnerText.Trim().Replace("'", "''");
            intType = Int32.Parse(xe.GetElementsByTagName("intType").Item(0).InnerText.Trim());
            strContent = xe.GetElementsByTagName("strContent").Item(0).InnerText.Trim().Replace("'", "''");
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

    class Shop
    {
        private string strId;

        public string StrId
        {
            get { return strId; }
            set { strId = value; }
        }
        private string strSmallImg;

        public string StrSmallImg
        {
            get { return strSmallImg; }
            set { strSmallImg = value; }
        }
        private string strLargeImg;

        public string StrLargeImg
        {
            get { return strLargeImg; }
            set { strLargeImg = value; }
        }

        public Shop(OleDbDataReader reader)
        {
            this.StrId = reader.GetString(0);
            if (!reader.IsDBNull(1))
                this.StrSmallImg = reader.GetString(1);
            else
                this.StrSmallImg = "";
            if(!reader.IsDBNull(2))
                this.StrLargeImg = reader.GetString(2);
            else
                this.StrLargeImg = "";
        }
    }

    class Coupon
    {
        private string strId;

        public string StrId
        {
            get { return strId; }
            set { strId = value; }
        }
        private string strSmallImg;

        public string StrSmallImg
        {
            get { return strSmallImg; }
            set { strSmallImg = value; }
        }
        private string strLargeImg;

        public string StrLargeImg
        {
            get { return strLargeImg; }
            set { strLargeImg = value; }
        }
        private string strPrintImg;

        public string StrPrintImg
        {
            get { return strPrintImg; }
            set { strPrintImg = value; }
        }

        public Coupon(OleDbDataReader reader)
        {
            this.StrId = reader.GetString(0);
            if (!reader.IsDBNull(1))
                this.StrSmallImg = reader.GetString(1);
            else
                this.strSmallImg = "";
            if (!reader.IsDBNull(2))
                this.StrLargeImg = reader.GetString(2);
            else
                this.StrLargeImg = "";
            if (!reader.IsDBNull(3))
                this.StrPrintImg = reader.GetString(3);
            else
                this.StrPrintImg = "";
        }
    }

    class Advertisement
    {
        private string strId;

        public string StrId
        {
            get { return strId; }
            set { strId = value; }
        }
        private int intType;

        public int IntType
        {
            get { return intType; }
            set { intType = value; }
        }
        private string strContent;

        public string StrContent
        {
            get { return strContent; }
            set { strContent = value; }
        }

        public Advertisement(OleDbDataReader reader)
        {
            this.StrId = reader.GetString(0);
            if(!reader.IsDBNull(1))
                this.IntType = reader.GetInt32(1);
            else
                this.IntType = 3;
            if (!reader.IsDBNull(2))
                this.StrContent = reader.GetString(2);
            else
                this.StrContent = "";
        }
    }
}
