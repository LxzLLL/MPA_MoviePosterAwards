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
                SetHeaderValue(req.Headers, "Cookie:", "ll=\"118318\"; bid=uEzTF1SP5RA; ps=y; ue=\"390266249@qq.com\"; dbcl2=\"63063997:VtVHs / FswIw\"; gr_user_id=03fc3590-700d-47a7-8865-7656455e4721; ap=1; ck=8P4R; push_noty_num=0; push_doumail_num=0; __utma=30149280.1833743162.1479219754.1479731250.1479738117.22; __utmb=30149280.0.10.1479738117; __utmc=30149280; __utmz=30149280.1479650747.18.4.utmcsr=baidu|utmccn=(organic)|utmcmd=organic; __utmv=30149280.6306; __utma=223695111.840018483.1479616913.1479731250.1479738117.7; __utmb=223695111.0.10.1479738117; __utmc=223695111; __utmz=223695111.1479616913.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); _vwo_uuid_v2=050A0BECDBE34E35C996F3A6D7A0B42F|68a974f5e63ee16650102a0821969c90; _pk_id.100001.4cf6=e6ac8b4ef4397ede.1479616913.7.1479741951.1479731770.; _pk_ses.100001.4cf6=*; _ga=GA1.2.1833743162.1479219754; _gat=1");
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
            catch (WebException e)
            {
                Console.WriteLine(url + "获取源码失败");
                return string.Empty;
            }
        }
    }
}
