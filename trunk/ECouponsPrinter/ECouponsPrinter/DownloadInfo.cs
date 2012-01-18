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
        public Form1 form1;

        public DownloadInfo() { }

        public void download()
        {
            //下载商家信息
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/ShopDownload?strTerminalNo=" + GlobalVariables.StrTerminalNo, "");
            XmlDocument doc = new XmlDocument();
            string strXml = request.HtmlDocument;
            form1.setText(strXml);
            if (strXml.IndexOf("<shops>") > 0)
            {
                try
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            //下载优惠券信息
            //下载广告信息

        }

        private void deleteShop(XmlNode xnShop)
        {
            String strId, strSmallImg = "", strLargeImg = "";
            XmlElement xeShop = (XmlElement)xnShop;
            strId = xeShop.GetElementsByTagName("strId").Item(0).InnerText;
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
                createShopImg(strSmallImg);
            if (strLargeImg.Length > 0)
                createShopImg(strLargeImg);
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
                createShopImg(strSmallImg);
            if (strLargeImg.Length > 0)
                createShopImg(strLargeImg);
            //写入数据库
            AccessCmd cmd = new AccessCmd();
            string strSql = "insert into t_bz_shop(strId,strBizName,strShopName,strTrade,strAddr,strIntro,strSmallImg,strLargeImg) values('" + strId + "','" + strBizName + "','" + strShopName +
                "','" + strTrade + "','" + strAddr + "','" + strIntro + "','" + strSmallImg + "','" + strLargeImg + "')";
            cmd.ExecuteNonQuery(strSql);
            cmd.Close();
        }

        private static void createShopImg(String strImg)
        {
            WebRequest request = HttpWebRequest.Create(GlobalVariables.StrServerUrl + "/servlet/FileDownload?strFileType=shop&strFileName=" + strImg);
            Stream stream = request.GetResponse().GetResponseStream();
            byte[] bytes = new byte[2048];
            int i;
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\shop\\" + strImg, FileMode.CreateNew);
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
    }
}
