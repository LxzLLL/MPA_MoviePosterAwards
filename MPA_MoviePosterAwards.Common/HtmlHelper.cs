using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.Common
{
    public class HtmlHelper
    {
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as System.Collections.Specialized.NameValueCollection;
                collection[name] = value;
            }
        }

        public static string GetHtmlCode(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.Method = "GET";
                SetHeaderValue(req.Headers, "Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                SetHeaderValue(req.Headers, "Accept-Language", "zh-CN,zh;q=0.8");
                SetHeaderValue(req.Headers, "Cookie:", "ll=\"118318\"; bid=TSiEsQchHUg; ct=y; ps=y; ue=\"390266249@qq.com\"; dbcl2=\"63063997:owWyc7DpvBQ\"; ap=1; ck=BUfv; _pk_ref.100001.4cf6=%5B%22%22%2C%22%22%2C1490621541%2C%22https%3A%2F%2Fwww.douban.com%2Fmisc%2Fsorry%3Foriginal-url%3Dhttps%253A%252F%252Fmovie.douban.com%252F%22%5D; _pk_id.100001.4cf6=c14bd24a056ce7d7.1490613648.3.1490624954.1490616919.; _pk_ses.100001.4cf6=*; __utma=30149280.1569080576.1489412160.1490616756.1490621541.69; __utmb=30149280.0.10.1490621541; __utmc=30149280; __utmz=30149280.1490376199.55.6.utmcsr=douban.com|utmccn=(referral)|utmcmd=referral|utmcct=/group/topic/23218805/; __utmv=30149280.6306; __utma=223695111.895826094.1490613648.1490616756.1490621541.3; __utmb=223695111.0.10.1490621541; __utmc=223695111; __utmz=223695111.1490613648.1.1.utmcsr=douban.com|utmccn=(referral)|utmcmd=referral|utmcct=/misc/sorry; push_noty_num=0; push_doumail_num=0; _vwo_uuid_v2=B616606DF0403988AB6EA86550AC94B3|fafff5cfcd82a35b68e7d14e86e12838");
                SetHeaderValue(req.Headers, "Host", "movie.douban.com");
                SetHeaderValue(req.Headers, "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36");
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                if (resp.StatusCode != HttpStatusCode.OK)
                    GetHtmlCode(url);
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
                string fullhtml = System.Web.HttpUtility.HtmlDecode(sr.ReadToEnd());
                resp.Close();
                sr.Close();
                return fullhtml;
            }
            catch (WebException e)
            {
                try
                {
                    WebClient wc = new WebClient();
                    wc.Credentials = CredentialCache.DefaultCredentials;

                    Stream resStream = wc.OpenRead(url);
                    Encoding enc = Encoding.GetEncoding("UTF-8");
                    StreamReader sr = new StreamReader(resStream, enc);
                    //string fullhtml = sr.ReadToEnd().Replace("&#39;", "'").Replace("&#34;", "\"").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Trim();
                    string fullhtml = System.Web.HttpUtility.HtmlDecode(sr.ReadToEnd());
                    resStream.Close();

                    wc.Dispose();
                    return fullhtml;
                }
                catch (WebException e2)
                {
                    Console.WriteLine(url + "获取源码失败");
                    return string.Empty;
                }
            }
            finally
            {
                System.Threading.Thread.Sleep(1200);
            }
        }
    }
}
