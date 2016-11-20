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
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
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
                SetHeaderValue(req.Headers, "Cookie:", "ll=\"118318\"; bid=SXzIrzYyibM; _vwo_uuid_v2=411549EE6A39D1D75DE4EF70B19F5C19|6e2274c180190b436c520fac4baa2b9c; gr_user_id=3fef1e6d-1788-4f01-8bb6-30680ba3a770; ue=\"390266249@qq.com\"; dbcl2=\"63063997:MuwrBIoaDFA\"; ct=y; ap=1; __utmt=1; ck=eN8_; push_noty_num=0; push_doumail_num=0; __utma=30149280.947188865.1473772136.1477281412.1477313577.173; __utmb=30149280.54.10.1477313577; __utmc=30149280; __utmz=30149280.1477065035.166.10.utmcsr=bttiantang99.com|utmccn=(referral)|utmcmd=referral|utmcct=/movie/35157.html; __utmv=30149280.6306; __utma=223695111.1337641447.1477283425.1477283425.1477313577.2; __utmb=223695111.0.10.1477313577; __utmc=223695111; __utmz=223695111.1477283425.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)");
                SetHeaderValue(req.Headers, "Host", "movie.douban.com");
                SetHeaderValue(req.Headers, "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36");
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                if (resp.StatusCode != HttpStatusCode.OK)
                    GetHtmlCode(url);
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
                string fullhtml = sr.ReadToEnd().Replace("&#39;", "'").Replace("&#34;", "\"").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Trim();
                resp.Close();
                sr.Close();
                return fullhtml;
            }
            catch (WebException)
            {
                Console.WriteLine(url + "获取源码失败");
                return string.Empty;
            }
        }
    }
}
