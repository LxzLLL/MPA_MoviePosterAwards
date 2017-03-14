using MPA_MoviePosterAwards.BLL;
using MPA_MoviePosterAwards.Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapySharp.Extensions;
using System.IO;

namespace MPA_MoviePosterAwards.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieManager.InsertMovie("26325320", Guid.Empty);
            //GetMyMovie();
            Console.ReadLine();
        }

        public static async void GetMyMovie()
        {
            List<MyMovie> mymovies = new List<MyMovie>();
            string nexturl = "https://movie.douban.com/people/63063997/collect";
            int i = 0;
            do
            {
                string htmlcode = HtmlHelper.GetHtmlCode(nexturl);
                HtmlDocument docinfo = new HtmlDocument();
                docinfo.LoadHtml(htmlcode);
                HtmlNode hnInfo = docinfo.DocumentNode;
                var movienodes = hnInfo.CssSelect("div.item");

                foreach (var item in movienodes)
                {
                    MyMovie mymovie = new MyMovie();
                    var movie = item.CssSelect("a.nbg").FirstOrDefault();
                    Console.WriteLine(movie.Attributes["title"].Value.ToString());
                    Console.WriteLine(movie.Attributes["href"].Value.ToString());

                    mymovie.Douban = movie.Attributes["href"].Value.ToString();
                    movie = item.CssSelect("span.date").FirstOrDefault();
                    mymovie.Date = movie.InnerText;
                    movie = item.CssSelect("span.tags").FirstOrDefault();
                    mymovie.Tag = movie.InnerText;
                    var ssssss = item.CssSelect("li").ToList();
                    var xxxxxx = ssssss[2];
                    var zzzzzz = xxxxxx.CssSelect("span").ToList();
                    var eeeeee = zzzzzz[0];
                    mymovie.Rating = item.CssSelect("li").ToList()[2].CssSelect("span").ToList()[0].Attributes["class"].Value.ToString();
                    mymovie.Order = i++;

                    mymovies.Add(mymovie);
                }
                try
                {
                    nexturl = hnInfo.CssSelect("div.paginator>span.next>link").FirstOrDefault().Attributes["href"].Value.ToString();
                }
                catch (Exception)
                {
                    break;
                }
                //if (hnInfo.CssSelect("div.paginator>span.next").FirstOrDefault().HasChildNodes)
                //    nexturl = hnInfo.CssSelect("div.paginator>span.next>link").FirstOrDefault().Attributes["href"].Value.ToString();
                //else
                //    break;
            } while (true);

            StreamWriter writer = new StreamWriter("D://MyMovie.json", false, Encoding.UTF8);

            await writer.WriteAsync(JsonHelper.SerializeObject(mymovies));
            writer.Close();
            Console.ReadLine();
        }
    }

    public class MyMovie
    {
        public string Douban { get; set; }
        public string Date { get; set; }
        public string Tag { get; set; }
        public string Rating { get; set; }
        public int Order { get; set; }
    }
}
