using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

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
            doc.LoadXml(request.HtmlDocument);
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
            //下载优惠券信息

            //下载广告信息

        }

        private void updateShop(XmlNode xnShop)
        {
            String strId, strBizName, strShopName, strTrade, strAddr, strIntro, strSmallImg, strSmallImgContent, strLargeImg, strLargeImgContent;
            getShopProps(xnShop, out strId, out strBizName, out strShopName, out strTrade, out strAddr, out strIntro, out strSmallImg, out strSmallImgContent,
                out strLargeImg, out strLargeImgContent);
            //写入数据库
            AccessCmd cmd = new AccessCmd();
            string strSql = "update t_bz_shop set strBizName='" + strBizName + "',strShopName='" + strShopName + "',strTrade='" + strTrade + "',strAddr='" + strAddr + 
                "',strIntro='" + strIntro + "',strSmallImg='" + strSmallImg + "',strLargeImg='" + strLargeImg + "' where strId='" + strId + "'";
            cmd.ExecuteNonQuery();
            cmd.Close();
            //删除原文件
            File.Delete(System.Windows.Forms.Application.StartupPath + "shop\\" + strSmallImg);
            File.Delete(System.Windows.Forms.Application.StartupPath + "shop\\" + strLargeImg);
            //创建文件文件
            createShopImg(strSmallImg, strSmallImgContent, strLargeImg, strLargeImgContent);
        }

        private void addShop(XmlNode xnShop)
        {
            String strId, strBizName, strShopName, strTrade, strAddr, strIntro, strSmallImg, strSmallImgContent, strLargeImg, strLargeImgContent;
            getShopProps(xnShop, out strId, out strBizName, out strShopName, out strTrade, out strAddr, out strIntro, out strSmallImg, out strSmallImgContent, 
                out strLargeImg, out strLargeImgContent);
            //写入数据库
            AccessCmd cmd = new AccessCmd();
            string strSql = "insert t_bz_shop(strId,strBizName,strShopName,strTrade,strAddr,strIntro,strSmallImg,strLargeImg) values('" + strId + "','" + strBizName + "','" + strShopName +
                "','" + strTrade + "','" + strAddr + "','" + strIntro + "','" + strSmallImg + "','" + strLargeImg + "')";
            cmd.ExecuteNonQuery();
            cmd.Close();
            //创建文件文件
            createShopImg(strSmallImg, strSmallImgContent, strLargeImg, strLargeImgContent);
        }

        private static void createShopImg(String strSmallImg, String strSmallImgContent, String strLargeImg, String strLargeImgContent)
        {
            Base64Decoder objDec = new Base64Decoder();
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "shop\\" + strSmallImg, FileMode.CreateNew);
            Byte[] info = objDec.GetDecoded(strSmallImgContent);
            fs.Write(info, 0, info.Length);
            fs.Close();
            fs = new FileStream(System.Windows.Forms.Application.StartupPath + "shop\\" + strLargeImg, FileMode.CreateNew);
            Byte[] info = objDec.GetDecoded(strLargeImgContent);
            fs.Write(info, 0, info.Length);
            fs.Close();
        }

        private static void getShopProps(XmlNode xnShop, out String strId, out String strBizName, out String strShopName, out String strTrade, out String strAddr, out String strIntro, out String strSmallImg, out String strSmallImgContent, out String strLargeImg, out String strLargeImgContent)
        {
            XmlElement xeShop = (XmlElement)xnShop;
            strId = xeShop.GetElementsByTagName("strId").Item(0).InnerText;
            strBizName = xeShop.GetElementsByTagName("strBizName").Item(0).InnerText;
            strShopName = xeShop.GetElementsByTagName("strShopName").Item(0).InnerText;
            strTrade = xeShop.GetElementsByTagName("strTrade").Item(0).InnerText;
            strAddr = xeShop.GetElementsByTagName("strAddr").Item(0).InnerText;
            strIntro = xeShop.GetElementsByTagName("strIntro").Item(0).InnerText;
            strSmallImg = xeShop.GetElementsByTagName("strSmallImg").Item(0).InnerText;
            strSmallImgContent = xeShop.GetElementsByTagName("strId").Item(0).InnerText;
            strLargeImg = xeShop.GetElementsByTagName("strId").Item(0).InnerText;
            strLargeImgContent = xeShop.GetElementsByTagName("strId").Item(0).InnerText;
        }
    }
}
