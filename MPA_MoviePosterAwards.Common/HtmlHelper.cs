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
                SetHeaderValue(req.Headers, "Cookie:", "ll=\"118318\"; bid=hrf76zZ0qZY; ps=y; ct=y; ap=1; ue=\"390266249@qq.com\"; dbcl2=\"63063997:jl / SIxJurKY\"; ck=Bmj6; push_noty_num=0; push_doumail_num=0; __utma=30149280.1685418604.1480697741.1481436383.1481438476.30; __utmb=30149280.0.10.1481438476; __utmc=30149280; __utmz=30149280.1480746573.4.2.utmcsr=subhd.com|utmccn=(referral)|utmcmd=referral|utmcct=/a/328513; __utmv=30149280.6306; __utma=223695111.1104703353.1481436506.1481436506.1481438476.2; __utmb=223695111.0.10.1481438476; __utmc=223695111; __utmz=223695111.1481436506.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); _pk_id.100001.4cf6=46c660172a6d11a7.1481436505.2.1481438478.1481436505.; _pk_ses.100001.4cf6=*; _vwo_uuid_v2=893DB0C4FED41ADA954E3859037B57F8|aeb1a7d0e2a6165da60b078d0dd2e3f4");
                SetHeaderValue(req.Headers, "Host", "movie.douban.com");
                SetHeaderValue(req.Headers, "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36");
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                if (resp.StatusCode != HttpStatusCode.OK)
                    GetHtmlCode(url);
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
                //System.Diagnostics.Debug.WriteLine(System.Web.HttpUtility.HtmlDecode("&#39;&#34;&quot;"));
                //string fullhtml = sr.ReadToEnd().Replace("&#39;", "'").Replace("&#34;", "\"").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Trim();
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
        }
    }
}
