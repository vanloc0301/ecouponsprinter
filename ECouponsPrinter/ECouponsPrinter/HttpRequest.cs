using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using ICSharpCode.SharpZipLib.GZip;

namespace ECouponsPrinter
{
    /* 
    * 程序设计：月亮太阳 
    * E-mail：stjzp@21cn.com 
    * QQ:548317 
    * */

    /// <summary> 
    /// HTTP/HTTPS资源的请求。 
    /// </summary> 
    public class HttpRequest
    {
        // 保持Cookie为同一Cookie值。        
        protected CookieContainer craboCookie = new CookieContainer();

        protected string __Uri__ = null;         // 标识 Internet 资源的 URI 

        protected string __Referer__ = null;         // 标识 Internet 资源请求的 Referer        

        protected string __Headers__ = null;         // 标识 Internet 资源请求的 Header 

        // 标识 Internet 资源请求的 Accept 
        protected string __Accept__ = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";

        protected string __Method__ = null;         // 标识 Internet 资源请求的 Method 

        protected string __Data__ = null;         // POST请求时的数据 

        protected string __CharacterSet__ = null;         // 响应的字符集 

        protected HttpStatusCode __StatusCode__;         // 响应状态 

        protected StringBuilder __Html_Text__ = new StringBuilder();

        public HttpRequest()
        {
            // 
            // TODO: 在此处添加构造函数逻辑 
            // 
            __CharacterSet__ = "UTF-8";
        }

        /// <summary> 
        /// 对 Internet 资源发起GET请求 
        /// </summary> 
        /// <param name="requestUriString">标识 Internet 资源的 URI</param> 
        /// <param name="requestReferer">标识 Internet 资源请求的 Referer</param> 
        public bool OpenRequest(string requestUriString, string requestReferer)
        {
            __Uri__ = requestUriString;
            __Referer__ = requestReferer;
            __Method__ = "GET";
            return OpenRequest();
        }

        /// <summary> 
        /// 对 Internet 资源发起GET请求 
        /// </summary> 
        /// <param name="requestUriString">标识 Internet 资源的 URI</param> 
        /// <param name="requestReferer">标识 Internet 资源请求的 Referer</param> 
        /// <param name="requestMethod">标识 Internet 资源请求的 Post 数据</param> 
        public bool OpenRequest(string requestUriString, string requestReferer, string requestData)
        {
            __Uri__ = requestUriString;
            __Referer__ = requestReferer;
            __Data__ = requestData;
            __Method__ = "POST";

            return OpenRequest();
        }

        /// <summary> 
        /// 对 Internet 资源发起请求 
        /// </summary> 
        /// <returns></returns> 
        private bool OpenRequest()
        {
            // 清空已保留代码 
            __Html_Text__.Remove(0, __Html_Text__.Length);

            // 创建 HttpWebRequest 实例 
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(__Uri__);

            // 设置跟随重定向 
            Request.AllowAutoRedirect = true;

            #region 判断Uri资源类型
            {
                Regex __RegexUri_ = new Regex("^https://", RegexOptions.IgnoreCase);
                if (__RegexUri_.IsMatch(__Uri__))
                    ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            }
            #endregion

            Request.Timeout = 10000;
            Request.Method = __Method__;
            Request.Accept = __Accept__;
            Request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; Maxthon; .NET CLR 1.1.4322); Http STdns";
            Request.CookieContainer = craboCookie;
            Request.Referer = __Referer__;
            Request.Method = __Method__;
            if (__Method__ == "POST")
            {
                #region 请求为POST
                Request.ContentType = "application/x-www-form-urlencoded";
                byte[] Bytes = Encoding.GetEncoding(CharacterSet).GetBytes(__Data__);
                Request.ContentLength = Bytes.Length;
                using (Stream writer = Request.GetRequestStream())
                {
                    writer.Write(Bytes, 0, Bytes.Length);
                    writer.Close();
                }
                #endregion
            }

            #region 读取返回数据
            // 设置返回值变量 
            bool bResult = true;
            try
            {
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();


                // 获取状态值 
                __StatusCode__ = Response.StatusCode;

                if (__StatusCode__ == HttpStatusCode.OK)
                {
                    // 判断页面编码 
                    string ContentEncoding = Response.ContentEncoding.ToLower();
                    switch (ContentEncoding)
                    {
                        case "gzip":
                            using (Stream reader = new GZipInputStream(Response.GetResponseStream()))
                            {
                                MemoryStream ms = new MemoryStream();
                                int nSize = 4096;
                                byte[] writeData = new byte[nSize];
                                while (true)
                                {
                                    try
                                    {
                                        nSize = reader.Read(writeData, 0, nSize);
                                        if (nSize > 0)
                                            ms.Write(writeData, 0, nSize);
                                        else
                                            break;
                                    }
                                    catch (GZipException)
                                    {
                                        break;
                                    }
                                }
                                reader.Close();
                                __Html_Text__.Append(Encoding.GetEncoding(CharacterSet).GetString(ms.GetBuffer()));
                            }
                            break;
                        default:
                            using (Stream reader = Response.GetResponseStream())
                            {
                                StreamReader sr = new StreamReader(reader, Encoding.GetEncoding(CharacterSet));
                                __Html_Text__.Append(sr.ReadToEnd());
                                sr.Close();
                                reader.Close();
                            }
                            break;
                    }
                }
                else
                    bResult = false;
            }
            catch (Exception pEx)
            {
                __Html_Text__.Append(pEx.Message);
                return false;
            }

            #endregion

            return bResult;
        }

        /// <summary> 
        /// 获取或设置 Uri 资源字符集 
        /// </summary> 
        public string CharacterSet
        {
            get
            {
                return __CharacterSet__;
            }
            set
            {
                __CharacterSet__ = value;
            }
        }

        /// <summary> 
        /// 获取或设置 Uri 资源标识 
        /// </summary> 
        public string RequestUriString
        {
            get
            {
                return __Uri__;
            }
            set
            {
                __Uri__ = value;
            }
        }

        /// <summary> 
        /// 获取或设置 Uri 资源请求 Accept 
        /// </summary> 
        public string Accept
        {
            get
            {
                return __Accept__;
            }
            set
            {
                __Accept__ = value;
            }
        }

        public string HtmlDocument
        {
            get
            {
                return __Html_Text__.ToString();
            }
        }
    }

    /// <summary> 
    /// 当 Uri 资源为 HTTPS 时，忽略证书。 
    /// </summary> 
    public class TrustAllCertificatePolicy : ICertificatePolicy
    {
        public TrustAllCertificatePolicy()
        {
            // 
            // TODO: 在此处添加构造函数逻辑 
            // 
        }

        public bool CheckValidationResult(ServicePoint _ServicePoint_, X509Certificate _Cert_, WebRequest _WebRequest_, int _Problem_)
        {
            return true;
        }
    }
}