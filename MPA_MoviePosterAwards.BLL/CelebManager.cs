using HtmlAgilityPack;
using MPA_MoviePosterAwards.Common;
using MPA_MoviePosterAwards.Model;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.BLL
{
    public class CelebManager
    {
        public static Basic_Celebrity_Info GetCeleb(string douban)
        {
            Basic_Celebrity_Info _basic_celeb = new Basic_Celebrity_Info();
            _basic_celeb.Douban = douban;
            GetCelebFromJson(ref _basic_celeb);

            string strhtml = HtmlHelper.GetHtmlCode(string.Format("https://movie.douban.com/celebrity/{0}/", douban));
            HtmlDocument hdoc = new HtmlDocument();
            hdoc.LoadHtml(strhtml);
            HtmlNode html = hdoc.DocumentNode;
            if (_basic_celeb.Name.IsBlank())
                _basic_celeb.Name = html.CssSelect("Title").FirstOrDefault().InnerText.Replace("(豆瓣)", "").Trim();
            if (_basic_celeb.Name_En.IsBlank())
                _basic_celeb.Name_En = html.CssSelect("div#content").FirstOrDefault().CssSelect("h1").FirstOrDefault().InnerText.Replace(_basic_celeb.Name, "").Trim();
            if (_basic_celeb.Avatar_Large.IsBlank())
                _basic_celeb.Avatar_Large = html.CssSelect("a.nbg").FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value;
            if (_basic_celeb.Avatar_Medium.IsBlank())
                _basic_celeb.Avatar_Medium = html.CssSelect("a.nbg").FirstOrDefault().CssSelect("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;

            foreach (var item in html.CssSelect("div#headline").FirstOrDefault().CssSelect("div.info").FirstOrDefault().CssSelect("li"))
            {
                HtmlDocument docinfo = new HtmlDocument();
                docinfo.LoadHtml(item.InnerHtml);
                HtmlNode hnInfo = docinfo.DocumentNode;

                if (item.InnerHtml.Contains(">性别"))
                {
                    _basic_celeb.Gender = item.InnerText.Replace("性别:", "").Trim() == "男";
                }
                if (item.InnerHtml.Contains(">出生日期"))
                {
                    _basic_celeb.Birth_Date = item.InnerText.Replace("出生日期:", "").Trim();
                }
                if (item.InnerHtml.Contains(">生卒日期"))
                {
                    _basic_celeb.Birth_Date = item.InnerText.Replace("生卒日期:", "").Split('至')[0].Trim();
                    _basic_celeb.Death_Date = item.InnerText.Replace("生卒日期:", "").Split('至')[1].Trim();
                }
                if (item.InnerHtml.Contains(">出生地") && _basic_celeb.Born_Place.IsBlank())
                {
                    _basic_celeb.Born_Place = item.InnerText.Replace("出生地:", "").Trim();
                }
                if (item.InnerHtml.Contains(">职业") && _basic_celeb.Profession.IsBlank())
                {
                    _basic_celeb.Profession = item.InnerText.Replace("职业:", "").TrimSplit();
                }
                if (item.InnerHtml.Contains(">更多外文名") && _basic_celeb.Aka_En.IsBlank())
                {
                    _basic_celeb.Aka_En = item.InnerText.Replace("更多外文名:", "").TrimSplit();
                }
                if (item.InnerHtml.Contains(">更多中文名") && _basic_celeb.Aka.IsBlank())
                {
                    _basic_celeb.Aka = item.InnerText.Replace("更多中文名:", "").TrimSplit();
                }
                if (item.InnerHtml.Contains(">家庭成员"))
                {
                    _basic_celeb.Family = item.InnerText.Replace("家庭成员:", "").TrimSplit();
                }
                if (item.InnerHtml.Contains(">imdb编号"))
                {
                    _basic_celeb.IMDb = item.CssSelect("a").FirstOrDefault().InnerText.Trim();
                }
            }


            if (html.CssSelect("span.all.hidden").Count() > 0)
            {
                _basic_celeb.Summary = html.CssSelect("span.all.hidden").FirstOrDefault().InnerText.Replace("　　", "\n").TrimAll();
            }
            else if (html.CssSelect("div#intro").FirstOrDefault().CssSelect("div.bd").Count() > 0)
            {
                _basic_celeb.Summary = html.CssSelect("div#intro").FirstOrDefault().CssSelect("div.bd").FirstOrDefault().InnerText.Replace("　　", "\n").TrimAll();
            }

            _basic_celeb.Id = Guid.NewGuid();

            return _basic_celeb;
        }

        public static void GetCelebFromJson(ref Basic_Celebrity_Info _basic_celeb)
        {
            string strjson = HtmlHelper.GetHtmlCode(string.Format("http://api.douban.com/v2/movie/celebrity/{0}", _basic_celeb.Douban));
            if (strjson.IsBlank())
                return;
            CelebJson celeb = JsonHelper.DeserializeJsonToObject<CelebJson>(strjson);

            _basic_celeb.Name = celeb.name;
            _basic_celeb.Name_En = celeb.name_en;
            _basic_celeb.Douban = celeb.id;
            _basic_celeb.Aka = string.Join(" / ", celeb.aka);
            _basic_celeb.Aka_En = string.Join(" / ", celeb.aka_en);
            _basic_celeb.Gender = (celeb.gender == "男") ? true : false;
            _basic_celeb.Born_Place = celeb.born_place;

            _basic_celeb.Avatar_Large = celeb.avatars.large;
            _basic_celeb.Avatar_Medium = celeb.avatars.medium;
            _basic_celeb.Avatar_Small = celeb.avatars.small;
        }

        public static Guid InsertCeleb(string douban)
        {
            if (Basic_Celebrity_BLL.Exist(douban))
            {
                return Basic_Celebrity_BLL.GetSingle(douban).Id;
            }
            try
            {
                Basic_Celebrity_Info _basic_celeb = GetCeleb(douban);
                Basic_Celebrity_BLL.Insert(_basic_celeb);

                return _basic_celeb.Id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }

        }
        public static Guid InsertCeleb(string douban, Guid celeb)
        {
            try
            {
                Basic_Celebrity_Info _basic_celeb = GetCeleb(douban);
                _basic_celeb.Id = celeb;
                Basic_Celebrity_BLL.Insert(_basic_celeb);

                return _basic_celeb.Id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }
        public static RequestResult UpdateCeleb(Guid id)
        {
            var celeb = Basic_Celebrity_BLL.GetSingle(id);
            try
            {
                Basic_Celebrity_BLL.Delete(id);
                InsertCeleb(celeb.Douban, id);
                return new RequestResult() { Error = celeb.Douban + "更新成功", Succeeded = true };
            }
            catch (Exception)
            {
                return new RequestResult() { Error = celeb.Douban + "更新失败", Succeeded = true };
            }
        }
    }
}
