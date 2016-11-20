using HtmlAgilityPack;
using MPA_MoviePosterAwards.Common;
using MPA_MoviePosterAwards.DAL;
using MPA_MoviePosterAwards.Model;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.BLL
{
    public class MovieManager
    {
        public static void GetMovie(string id)
        {
            Basic_Movie_DAL _movie_dal = new Basic_Movie_DAL();
            Step_Celeb_Movie_DAL _celeb_movie_dal = new Step_Celeb_Movie_DAL();

            if (_movie_dal.HasItem(id))
            {
                return;
            }

            Basic_Movie_Info tblMovie = new Basic_Movie_Info();
            tblMovie.Genres = new List<Step_Movie_Genre_Info>();
            tblMovie.Countries = new List<Step_Movie_Country_Info>();
            tblMovie.Langs = new List<Step_Movie_Lang_Info>();
            tblMovie.Poster = new Step_Movie_Poster_Info();
            tblMovie.Rating = new Step_Movie_Rating_Info();

            List<Step_Celeb_Movie_Info> tblCelebMovies = new List<Step_Celeb_Movie_Info>();

            tblMovie.Douban = id;
            tblMovie.Id = Guid.NewGuid();
            GetMovieFromJson(ref tblMovie);

            string strhtml = HtmlHelper.GetHtmlCode(string.Format("https://movie.douban.com/subject/{0}/", id));
            if (strhtml.IsBlank())
                return;
            HtmlDocument hdoc = new HtmlDocument();
            hdoc.LoadHtml(strhtml);
            HtmlNode html = hdoc.DocumentNode;

            try
            {
                if (tblMovie.Title.IsBlank())
                {
                    tblMovie.Title = html.CssSelect("Title").FirstOrDefault().InnerText.Replace("(豆瓣)", "").Trim();
                }
                if (tblMovie.Title_En.IsBlank())
                {
                    tblMovie.Title_En = html.CssSelect("a.nbgnbg']").FirstOrDefault().CssSelect("img").FirstOrDefault().ChildAttributes("alt").FirstOrDefault().Value;
                }
                if (tblMovie.Year > 0)
                {
                    tblMovie.Year = short.Parse(html.CssSelect("span.year").FirstOrDefault().InnerText.Replace("(", "").Replace(")", "").Trim());
                }
                if (tblMovie.Poster.Large.Length == 0)
                {
                    tblMovie.Poster.Large = html.CssSelect("div#mainpic").FirstOrDefault().CssSelect("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;
                }

                foreach (var item in html.CssSelect("div#info").FirstOrDefault().InnerHtml.Split('\n'))
                {
                    HtmlDocument docinfo = new HtmlDocument();
                    docinfo.LoadHtml(item);
                    HtmlNode hnInfo = docinfo.DocumentNode;

                    if (hnInfo.InnerHtml.Contains(">导演<"))
                    {
                        var dires = hnInfo.CssSelect("a");
                        foreach (var dir in dires)
                        {
                            if (dir.ChildAttributes("href").FirstOrDefault().Value.Contains("/search/"))
                                continue;

                            Guid workguid = GetCeleb(dir.ChildAttributes("href").FirstOrDefault().Value.Replace("celebrity", "").Replace("/", ""));

                            if (workguid != Guid.Empty)
                            {
                                Step_Celeb_Movie_Info celebwork = new Step_Celeb_Movie_Info();
                                celebwork.Id = Guid.NewGuid();
                                celebwork.Celeb = workguid;
                                celebwork.Movie = tblMovie.Id;
                                celebwork.Position = "导演";
                                tblCelebMovies.Add(celebwork);
                            }
                        }
                    }
                    if (hnInfo.InnerHtml.Contains(">编剧<"))
                    {
                        var dires = hnInfo.CssSelect("a");
                        foreach (var dir in dires)
                        {
                            if (dir.ChildAttributes("href").FirstOrDefault().Value.Contains("/search/"))
                                continue;

                            Guid workguid = GetCeleb(dir.ChildAttributes("href").FirstOrDefault().Value.Replace("celebrity", "").Replace("/", ""));

                            if (workguid != Guid.Empty)
                            {
                                Step_Celeb_Movie_Info celebwork = new Step_Celeb_Movie_Info();
                                celebwork.Id = Guid.NewGuid();
                                celebwork.Celeb = workguid;
                                celebwork.Movie = tblMovie.Id;
                                celebwork.Position = "编剧";
                                tblCelebMovies.Add(celebwork);
                            }
                        }
                    }
                    if (hnInfo.InnerHtml.Contains(">主演<"))
                    {
                        var dires = hnInfo.CssSelect("a");
                        foreach (var dir in dires)
                        {
                            if (dir.ChildAttributes("href").FirstOrDefault().Value.Contains("/search/"))
                                continue;

                            Guid workguid = GetCeleb(dir.ChildAttributes("href").FirstOrDefault().Value.Replace("celebrity", "").Replace("/", ""));

                            if (workguid != Guid.Empty)
                            {
                                Step_Celeb_Movie_Info celebwork = new Step_Celeb_Movie_Info();
                                celebwork.Id = Guid.NewGuid();
                                celebwork.Celeb = workguid;
                                celebwork.Movie = tblMovie.Id;
                                celebwork.Position = "主演";
                                tblCelebMovies.Add(celebwork);
                            }
                        }
                    }

                    if (hnInfo.InnerHtml.Contains(">类型:<"))
                    {
                        var dires = hnInfo.CssSelect("span[property='v:genre']");
                        foreach (var dir in dires)
                        {
                            tblMovie.Genres.Add(new Step_Movie_Genre_Info() { Genre = dir.InnerText.Trim(), Id = Guid.NewGuid(), Movie = tblMovie.Id });
                        }
                    }
                    if (hnInfo.InnerHtml.Contains(">官方网站:<"))
                    {
                        var dires = hnInfo.CssSelect("a").FirstOrDefault();
                        tblMovie.Website = dires.ChildAttributes("href").FirstOrDefault().Value;
                    }
                    if (hnInfo.InnerHtml.Contains(">制片国家/地区:<"))
                    {
                        var dires = hnInfo.InnerText.Replace("制片国家/地区:", "").Split('/');
                        foreach (var dir in dires)
                        {
                            tblMovie.Countries.Add(new Step_Movie_Country_Info() { Country = dir.Trim(), Id = Guid.NewGuid(), Movie = tblMovie.Id });
                        }
                    }
                    if (hnInfo.InnerHtml.Contains(">语言:<"))
                    {
                        var dires = hnInfo.InnerText.Replace("语言:", "").Split('/');
                        foreach (var dir in dires)
                        {
                            tblMovie.Langs.Add(new Step_Movie_Lang_Info() { Lang = dir.Trim(), Id = Guid.NewGuid(), Movie = tblMovie.Id });
                        }
                    }
                    if (hnInfo.InnerHtml.Contains(">上映日期:<") || hnInfo.InnerHtml.Contains(">首播:<"))
                    {
                        var dires = hnInfo.CssSelect("span[property='v:initialReleaseDate']");
                        List<string> str = new List<string>();
                        foreach (var dir in dires)
                        {
                            str.Add(dir.InnerText.Trim());
                        }
                        tblMovie.Pubdate = string.Join("/", str);
                    }
                    if (hnInfo.InnerHtml.Contains(">季数:<") && (tblMovie.Season_Count < 0 || tblMovie.Current_Season < 0))
                    {
                        var dires = hnInfo.CssSelect("option");
                        if (dires.Count() > 0)
                        {
                            if (tblMovie.Current_Season < 0)
                            {
                                foreach (var dir in dires)
                                {
                                    if (dir.ChildAttributes("selected").Count() > 0 && dir.ChildAttributes("selected").FirstOrDefault().Value == "selected")
                                    {
                                        tblMovie.Current_Season = (short)(dires.ToList().IndexOf(dir) + 1);
                                    }
                                }
                            }
                            if (tblMovie.Season_Count < 0)
                            {
                                tblMovie.Season_Count = (short)(dires.Count());
                            }
                        }
                        else
                        {
                            if (tblMovie.Current_Season < 0)
                            {
                                tblMovie.Current_Season = short.Parse(hnInfo.InnerText.Replace("季数:", "").Trim());
                            }
                        }
                    }
                    if (hnInfo.InnerHtml.Contains(">集数:<") && tblMovie.Episode_Count < 0)
                    {
                        tblMovie.Episode_Count = short.Parse(hnInfo.InnerText.Replace("集数:", "").Trim());
                    }
                    if (hnInfo.InnerHtml.Contains(">片长:<") || hnInfo.InnerHtml.Contains(">单集片长:<"))
                    {
                        var dires = hnInfo.CssSelect("span[property='v:runtime']");
                        if (dires.Count() > 0)
                        {
                            List<string> str = new List<string>();
                            foreach (var dir in dires)
                            {
                                str.Add(dir.InnerText.Trim());
                            }
                            tblMovie.Duration = string.Join("/", str);
                        }
                        else
                        {
                            tblMovie.Duration = hnInfo.InnerText.Replace("片长:", "").Replace("单集", "").Trim();
                        }
                    }
                    if (hnInfo.InnerHtml.Contains(">又名:<"))
                    {
                        var dires = hnInfo.InnerText.Replace("又名:", "").Split('/');
                        List<string> str = new List<string>();
                        foreach (var dir in dires)
                        {
                            str.Add(dir.Trim());
                            tblMovie.Aka = string.Join("/", str);
                        }
                    }
                    if (hnInfo.InnerHtml.Contains(">IMDb链接:<"))
                    {
                        tblMovie.IMDb = hnInfo.CssSelect("a").FirstOrDefault().InnerText.Trim();
                    }
                }

                if (html.CssSelect("span.all.hidden").Count() > 0)
                {
                    tblMovie.Summary = html.CssSelect("span.all.hidden").FirstOrDefault().InnerText.TrimAll();
                }
                else if (html.CssSelect("span[property='v:summary']").Count() > 0)
                {
                    tblMovie.Summary = html.CssSelect("span[property='v:summary']").FirstOrDefault().InnerText.TrimAll();
                }
                
                var ratingNode = html.CssSelect("div#interest_sectl");
                if (ratingNode.CssSelect("strong.rating_num").FirstOrDefault().InnerText.Length > 0)
                {
                    tblMovie.Rating.Score = double.Parse(ratingNode.CssSelect("strong.rating_num").FirstOrDefault().InnerText);
                }
                if (ratingNode.CssSelect("span[property='v:votes']").Count() > 0)
                {
                    tblMovie.Rating.Rated_Num = int.Parse(ratingNode.CssSelect("span[property='v:votes']").FirstOrDefault().InnerText);
                }
                var ratingpers = ratingNode.CssSelect("span.rating_per");
                if (ratingNode.CssSelect("span.rating_per").Count() > 0)
                {
                    tblMovie.Rating.Star5 = ratingpers.First().InnerText.Trim();
                    tblMovie.Rating.Star4 = ratingpers.ToList()[1].InnerText.Trim();
                    tblMovie.Rating.Star3 = ratingpers.ToList()[1].InnerText.Trim();
                    tblMovie.Rating.Star2 = ratingpers.ToList()[1].InnerText.Trim();
                    tblMovie.Rating.Star1 = ratingpers.Last().InnerText.Trim();
                }
                tblMovie.Rating.Id = Guid.NewGuid();
                tblMovie.Rating.Movie = tblMovie.Id;

                tblMovie.Poster.Id = Guid.NewGuid();
                tblMovie.Poster.Movie = tblMovie.Id;

                _movie_dal.Insert(tblMovie);
                _celeb_movie_dal.Insert(tblCelebMovies);
            }
            catch (System.IO.InvalidDataException dataExp)
            {
                Console.WriteLine(dataExp.Message);
            }
            catch (Exception)
            {
                Console.WriteLine(tblMovie.Douban + "电影源代码解析失败");
            }
        }

        static void GetMovieFromJson(ref Basic_Movie_Info tblMovie)
        {
            string strjson = HtmlHelper.GetHtmlCode(string.Format("http://api.douban.com/v2/movie/subject/{0}", tblMovie.Douban));
            if (strjson.IsBlank())
                return;
            MovieJson movie = JsonHelper.DeserializeJsonToObject<MovieJson>(strjson);

            tblMovie.Title = movie.title;
            tblMovie.Title_En = movie.original_title;
            tblMovie.Douban = movie.id;
            tblMovie.Summary = movie.summary;
            
            tblMovie.Poster.Large = movie.images.large;
            tblMovie.Poster.Medium = movie.images.medium;
            tblMovie.Poster.Small = movie.images.small;
            if (!movie.year.IsBlank())
                tblMovie.Year = short.Parse(movie.year);
            if (!movie.current_season.IsBlank())
                tblMovie.Current_Season = short.Parse(movie.current_season);
            if (!movie.seasons_count.IsBlank())
                tblMovie.Season_Count = short.Parse(movie.seasons_count);
            if (!movie.episodes_count.IsBlank())
                tblMovie.Episode_Count = short.Parse(movie.episodes_count);
        }


        static Guid GetCeleb(string id)
        {
            //MovieResShareEntities _db = new MovieResShareEntities();
            Basic_Celebrity_DAL _celeb_dal = new Basic_Celebrity_DAL();

            if (_celeb_dal.HasItem(id))
            {
                return _celeb_dal.GetSingleByDouban(id).Id; ;
            }

            Basic_Celebrity_Info tblCeleb = new Basic_Celebrity_Info();
            tblCeleb.Douban = id;
            GetCelebFromJson(ref tblCeleb);

            string strhtml = HtmlHelper.GetHtmlCode(string.Format("https://movie.douban.com/celebrity/{0}/", id));
            HtmlDocument hdoc = new HtmlDocument();
            hdoc.LoadHtml(strhtml);
            HtmlNode html = hdoc.DocumentNode;

            try
            {
                if (tblCeleb.Name.IsBlank())
                    tblCeleb.Name = html.CssSelect("Title").FirstOrDefault().InnerText.Replace("(豆瓣)", "").Trim();
                if (tblCeleb.Name_En.IsBlank())
                    tblCeleb.Name_En = html.CssSelect("div#content").FirstOrDefault().CssSelect("h1").FirstOrDefault().InnerText.Replace(tblCeleb.Name, "").Trim();
                if (tblCeleb.Avatar.Large.IsBlank())
                    tblCeleb.Avatar.Large = html.CssSelect("a.nbg").FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value;
                if (tblCeleb.Avatar.Medium.IsBlank())
                    tblCeleb.Avatar.Medium = html.CssSelect("a.nbg").FirstOrDefault().CssSelect("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;

                foreach (var item in html.CssSelect("div#headline").FirstOrDefault().CssSelect("div.info").FirstOrDefault().CssSelect("li"))
                {
                    HtmlDocument docinfo = new HtmlDocument();
                    docinfo.LoadHtml(item.InnerHtml);
                    HtmlNode hnInfo = docinfo.DocumentNode;

                    if (item.InnerHtml.Contains(">性别"))
                    {
                        tblCeleb.Gender = item.InnerText.Replace("性别:", "").Trim() == "男";
                    }
                    if (item.InnerHtml.Contains(">出生日期"))
                    {
                        tblCeleb.Birth_Date = item.InnerText.Replace("出生日期:", "").Trim();
                    }
                    if (item.InnerHtml.Contains(">生卒日期"))
                    {
                        tblCeleb.Birth_Date = item.InnerText.Replace("生卒日期:", "").Split('至')[0].Trim();
                        tblCeleb.Death_Date = item.InnerText.Replace("生卒日期:", "").Split('至')[1].Trim();
                    }
                    if (item.InnerHtml.Contains(">出生地") && tblCeleb.Born_Place.IsBlank())
                    {
                        tblCeleb.Born_Place = item.InnerText.Replace("出生地:", "").Trim();
                    }
                    if (item.InnerHtml.Contains(">职业") && tblCeleb.Profession.IsBlank())
                    {
                        tblCeleb.Profession = item.InnerText.Replace("职业:", "").TrimSplit();
                    }
                    if (item.InnerHtml.Contains(">更多外文名") && tblCeleb.Aka_En.IsBlank())
                    {
                        tblCeleb.Aka_En = item.InnerText.Replace("更多外文名:", "").TrimSplit();
                    }
                    if (item.InnerHtml.Contains(">更多中文名") && tblCeleb.Aka.IsBlank())
                    {
                        tblCeleb.Aka = item.InnerText.Replace("更多中文名:", "").TrimSplit();
                    }
                    if (item.InnerHtml.Contains(">家庭成员"))
                    {
                        tblCeleb.Family = item.InnerText.Replace("家庭成员:", "").TrimSplit();
                    }
                    if (item.InnerHtml.Contains(">imdb编号"))
                    {
                        tblCeleb.IMDb = item.CssSelect("a").FirstOrDefault().InnerText.Trim();
                    }
                }


                if (html.CssSelect("span.all.hidden").Count() > 0)
                {
                    tblCeleb.Summary = html.CssSelect("span.all.hidden").FirstOrDefault().InnerText.Replace("　　", "\n").TrimAll();
                    System.Diagnostics.Debug.WriteLine(html.CssSelect("span.all.hidden").FirstOrDefault().InnerText);
                }
                else if (html.CssSelect("div#intro").FirstOrDefault().CssSelect("div.bd").Count() > 0)
                {
                    tblCeleb.Summary = html.CssSelect("div#intro").FirstOrDefault().CssSelect("div.bd").FirstOrDefault().InnerText.Replace("　　", "\n").TrimAll();
                }


                tblCeleb.Id = Guid.NewGuid();
                //tblCeleb.Create_User = UserGuid;

                tblCeleb.Avatar.Id = Guid.NewGuid();
                tblCeleb.Avatar.Celeb = tblCeleb.Id;

                _celeb_dal.Insert(tblCeleb);

                return tblCeleb.Id;
            }
            catch (Exception)
            {
                Console.WriteLine(tblCeleb.Id + "影人源代码解析失败");

                return Guid.Empty;
            }
        }

        static void GetCelebFromJson(ref Basic_Celebrity_Info tblCeleb)//, ref Step_Celeb_Avatar_Info tblAvatar)
        {
            string strjson = HtmlHelper.GetHtmlCode(string.Format("http://api.douban.com/v2/movie/celebrity/{0}", tblCeleb.Douban));
            if (strjson.IsBlank())
                return;
            CelebJson celeb = JsonHelper.DeserializeJsonToObject<CelebJson>(strjson);

            tblCeleb.Name = celeb.name;
            tblCeleb.Name_En = celeb.name_en;
            tblCeleb.Douban = celeb.id;
            tblCeleb.Aka = string.Join("/", celeb.aka);
            tblCeleb.Aka_En = string.Join("/", celeb.aka_en);
            tblCeleb.Gender = (celeb.gender == "男") ? true : false;
            tblCeleb.Born_Place = celeb.born_place;

            tblCeleb.Avatar = new Step_Celeb_Avatar_Info();
            tblCeleb.Avatar.Large = celeb.avatars.large;
            tblCeleb.Avatar.Medium = celeb.avatars.medium;
            tblCeleb.Avatar.Small = celeb.avatars.small;
        }
    }


    #region Json帮助类
    public class MovieJson
    {
        public MovieRatingJson rating { get; set; }
        public string reviews_count { get; set; }
        public string wish_count { get; set; }
        public string douban_site { get; set; }
        public string year { get; set; }
        public MovieImageJson images { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
        public string mobile_url { get; set; }
        public string title { get; set; }
        public string do_count { get; set; }
        public string share_url { get; set; }
        public string seasons_count { get; set; }
        public string schedule_url { get; set; }
        public string episodes_count { get; set; }
        public List<string> countries { get; set; }
        public List<string> genres { get; set; }
        public string collect_count { get; set; }
        public List<MovieCastJson> casts { get; set; }
        public string current_season { get; set; }
        public string original_title { get; set; }
        public string summary { get; set; }
        public string subtype { get; set; }
        public List<MovieCastJson> directors { get; set; }
        public string comments_count { get; set; }
        public string ratings_count { get; set; }
        public List<string> aka { get; set; }
    }

    public class MovieRatingJson
    {
        public double max { get; set; }
        public double average { get; set; }
        public string stars { get; set; }
        public double min { get; set; }
    }

    public class MovieImageJson
    {
        public string small { get; set; }
        public string large { get; set; }
        public string medium { get; set; }
    }

    public class MovieCastJson
    {
        public string alt { get; set; }
        public MovieImageJson avatars { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class CelebJson
    {
        public string mobile_url { get; set; }
        public List<string> aka_en { get; set; }
        public string name { get; set; }
        public string gender { get; set; }

        public MovieImageJson avatars { get; set; }
        public string id { get; set; }
        public List<string> aka { get; set; }
        public string name_en { get; set; }
        public string born_place { get; set; }
        public string alt { get; set; }
    }
    #endregion
}
