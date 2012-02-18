using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ECouponsPrinter
{
    class UploadInfo
    {
        private HttpRequest request = new HttpRequest();
        public Form1 form1;

        public UploadInfo() { }

        public string CouponAuth(string strCardNo, string strCode, string strCouponId)
        {
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/CouponAuth?strTerminalNo=" + GlobalVariables.StrTerminalNo + "&strCardNo=" +
                strCardNo + "&strCode=" + strCode + "&strCouponId=" + strCouponId, "");
            XmlDocument doc = new XmlDocument();
            string strXml = request.HtmlDocument;
            doc.LoadXml(strXml);
            return doc.GetElementsByTagName("return").Item(0).InnerText.Trim();
        }

        public bool CouponPrint()
        {
            AccessCmd cmd = new AccessCmd();
            try
            {
                //读取数据库
                string strSql = "select * from t_bz_coupon_print";
                OleDbDataReader reader = cmd.ExecuteReader(strSql);
                StringBuilder sbPrintInfo = new StringBuilder();
                StringBuilder sbId = new StringBuilder();
                while (reader.Read())
                {
                    sbId.Append("'").Append(reader.GetString(0)).Append("',");
                    sbPrintInfo.Append(reader.GetString(1)).Append("$").Append(reader.GetString(2)).Append("$").Append(reader.GetDateTime(3).ToString()).Append("$")
                        .Append(reader.GetString(4)).Append("@");
                }
                reader.Close();
                if (sbPrintInfo.Length > 0)
                {
                    string strPrintInfo = sbPrintInfo.ToString(0, sbPrintInfo.Length - 1);
                    string strIds = sbId.ToString(0, sbId.Length - 1);
                    //上传数据
                    request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/CouponPrint?strTerminalNo=" + GlobalVariables.StrTerminalNo + "&strPrintContent=" +
                        strPrintInfo, "");
                    XmlDocument doc = new XmlDocument();
                    string strXml = request.HtmlDocument;
                    doc.LoadXml(strXml);
                    string strReturn = doc.GetElementsByTagName("return").Item(0).InnerText.Trim();
                    if (strReturn.Equals("OK"))
                    {
                        //删除数据库记录
                        strSql = "delete from t_bz_coupon_print where strId in (" + strIds + ")";
                        cmd.ExecuteNonQuery(strSql);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                cmd.Close();
            }
        }

        internal Member MemberAuth(string strCardNo)
        {
            try
            {
                //提交请求
                request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/MemberAuth?strCardNo=" + strCardNo, "");
                XmlDocument doc = new XmlDocument();
                string strXml = request.HtmlDocument;
           //     form1.setText(strXml);
                doc.LoadXml(strXml);
                string strAuth = doc.GetElementsByTagName("auth").Item(0).InnerText.Trim();
                if (strAuth.Equals("yes"))
                {
                    //实例化Member对象并返回
                    Member m = new Member();
                    m.StrCardNo = strCardNo;
                    m.IntType = Int16.Parse(doc.GetElementsByTagName("intType").Item(0).InnerText.Trim());
                    m.StrMobileNo = doc.GetElementsByTagName("strMobileNo").Item(0).InnerText.Trim();
                    XmlNodeList xnlF = doc.GetElementsByTagName("favourite").Item(0).ChildNodes;
                    string[] aryF = new string[xnlF.Count];
                    for (int i = 0; i < xnlF.Count; i++)
                    {
                        aryF[i] = xnlF.Item(i).InnerText.Trim();
                    }
                    m.AryFavourite = aryF;
                    XmlNodeList xnlH = doc.GetElementsByTagName("history").Item(0).ChildNodes;
                    string[] aryH = new string[xnlH.Count];
                    for (int i = 0; i < xnlH.Count; i++)
                    {
                        aryH[i] = xnlH.Item(i).InnerText.Trim();
                    }
                    m.AryHistory = aryH;
                    return m;
                }
                else
                {
                    //返回null
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.StackTrace);
                return null;
            }
        }

        internal bool PrintAlert(int intCouponPrint)
        {
            try
            {
                request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/PrintAlert?strTerminalNo=" + GlobalVariables.StrTerminalNo + "&intCouponPrint=" + intCouponPrint, "");
                XmlDocument doc = new XmlDocument();
                string strXml = request.HtmlDocument;
                form1.setText(strXml);
                doc.LoadXml(strXml);
                return doc.GetElementsByTagName("return").Item(0).InnerText.Trim().Equals("OK");
            }
            catch (Exception e)
            {
         //       MessageBox.Show(e.Message + "\n" + e.StackTrace);
                return false;
            }
        }

        internal bool MemberLogon(string strCardNo, string strMobileNo, string strCode)
        {
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/MemberMobile?strTerminalNo=" + GlobalVariables.StrTerminalNo + "&strCardNo=" + strCardNo + 
                "&strCode=" + strCode + "&strMobileNo=" + strMobileNo, "");
            XmlDocument doc = new XmlDocument();
            string strXml = request.HtmlDocument;
            if (strXml.IndexOf("<return>") > 0)
            {
                doc.LoadXml(strXml);
                return doc.GetElementsByTagName("return").Item(0).InnerText.Trim().Equals("OK");
            }
            else
            {
                return false;
            }
        }

        internal bool MemberLogon(string strCardNo, string strMobileNo)
        {
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/MemberMobile?strTerminalNo=" + GlobalVariables.StrTerminalNo + "&strCardNo=" + strCardNo +
                "&strMobileNo=" + strMobileNo, "");
            XmlDocument doc = new XmlDocument();
            string strXml = request.HtmlDocument;
            if (strXml.IndexOf("<return>") > 0)
            {
                doc.LoadXml(strXml);
                return doc.GetElementsByTagName("return").Item(0).InnerText.Trim().Equals("OK");
            }
            else
            {
                return false;
            }
        }

        internal bool CouponFavourite(string strCardNo, string strCouponId)
        {
            request.OpenRequest(GlobalVariables.StrServerUrl + "/servlet/CouponFavourite?strTerminalNo=" + GlobalVariables.StrTerminalNo + "&strCardNo=" + strCardNo +
                "&strCouponId=" + strCouponId, "");
            XmlDocument doc = new XmlDocument();
            string strXml = request.HtmlDocument;
            if (strXml.IndexOf("<return>") > 0)
            {
                doc.LoadXml(strXml);
                return doc.GetElementsByTagName("return").Item(0).InnerText.Trim().Equals("OK");
            }
            else
            {
                return false;
            }
        }
    }
}
