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
        #region 获取电影

        public static Basic_Movie_Info GetMovie(string douban)
        {
            Basic_Movie_Info _basic_movie = new Basic_Movie_Info();

            //List<Step_Celeb_Movie_Info> _step_celeb_movies = new List<Step_Celeb_Movie_Info>();

            _basic_movie.Douban = douban;
            _basic_movie.Id = Guid.NewGuid();
            GetMovieFromJson(ref _basic_movie);

            string strhtml = HtmlHelper.GetHtmlCode(string.Format("https://movie.douban.com/subject/{0}/", douban));
            if (strhtml.IsBlank())
                throw new HtmlWebException("读取网页源代码失败");
            HtmlDocument hdoc = new HtmlDocument();
            hdoc.LoadHtml(strhtml);
            HtmlNode html = hdoc.DocumentNode;

            if (_basic_movie.Title.IsBlank())
            {
                _basic_movie.Title = html.CssSelect("Title").FirstOrDefault().InnerText.Replace("(豆瓣)", "").Trim();
            }
            if (_basic_movie.Title_En.IsBlank())
            {
                _basic_movie.Title_En = html.CssSelect("a.nbgnbg']").FirstOrDefault().CssSelect("img").FirstOrDefault().ChildAttributes("alt").FirstOrDefault().Value;
            }
            if (_basic_movie.Year > 0)
            {
                _basic_movie.Year = short.Parse(html.CssSelect("span.year").FirstOrDefault().InnerText.Replace("(", "").Replace(")", "").Trim());
            }
            if (_basic_movie.Poster.Large.Length == 0)
            {
                _basic_movie.Poster.Large = html.CssSelect("div#mainpic").FirstOrDefault().CssSelect("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;
            }

            foreach (var item in html.CssSelect("div#info").FirstOrDefault().InnerHtml.Split('\n'))
            {
                HtmlDocument docinfo = new HtmlDocument();
                docinfo.LoadHtml(item);
                HtmlNode hnInfo = docinfo.DocumentNode;

                if (hnInfo.InnerHtml.Contains(">导演<"))
                {
                    var dires = hnInfo.CssSelect("a");
                    byte order = 0;
                    foreach (var dir in dires)
                    {
                        if (dir.ChildAttributes("href").FirstOrDefault().Value.Contains("/search/"))
                            continue;

                        Guid workguid = InsertCeleb(dir.ChildAttributes("href").FirstOrDefault().Value.Replace("celebrity", "").Replace("/", ""));

                        if (workguid != Guid.Empty)
                        {
                            Step_Celeb_Movie_Info celeb_movie = new Step_Celeb_Movie_Info();
                            celeb_movie.Id = Guid.NewGuid();
                            celeb_movie.Celeb = workguid;
                            celeb_movie.Movie = _basic_movie.Id;
                            celeb_movie.Position = "导演";
                            celeb_movie.Order = order++;
                            //_step_celeb_movies.Add(celeb_movie);
                            _basic_movie.Celebs.Add(celeb_movie);
                        }
                    }
                }
                if (hnInfo.InnerHtml.Contains(">编剧<"))
                {
                    var dires = hnInfo.CssSelect("a");
                    byte order = 0;
                    foreach (var dir in dires)
                    {
                        if (dir.ChildAttributes("href").FirstOrDefault().Value.Contains("/search/"))
                            continue;

                        Guid workguid = InsertCeleb(dir.ChildAttributes("href").FirstOrDefault().Value.Replace("celebrity", "").Replace("/", ""));

                        if (workguid != Guid.Empty)
                        {
                            Step_Celeb_Movie_Info celeb_movie = new Step_Celeb_Movie_Info();
                            celeb_movie.Id = Guid.NewGuid();
                            celeb_movie.Celeb = workguid;
                            celeb_movie.Movie = _basic_movie.Id;
                            celeb_movie.Position = "编剧";
                            celeb_movie.Order = order++;
                            //_step_celeb_movies.Add(celeb_movie);
                            _basic_movie.Celebs.Add(celeb_movie);
                        }
                    }
                }
                if (hnInfo.InnerHtml.Contains(">主演<"))
                {
                    var dires = hnInfo.CssSelect("a");
                    byte order = 0;
                    foreach (var dir in dires)
                    {
                        if (dir.ChildAttributes("href").FirstOrDefault().Value.Contains("/search/"))
                            continue;

                        Guid workguid = InsertCeleb(dir.ChildAttributes("href").FirstOrDefault().Value.Replace("celebrity", "").Replace("/", ""));

                        if (workguid != Guid.Empty)
                        {
                            Step_Celeb_Movie_Info celeb_movie = new Step_Celeb_Movie_Info();
                            celeb_movie.Id = Guid.NewGuid();
                            celeb_movie.Celeb = workguid;
                            celeb_movie.Movie = _basic_movie.Id;
                            celeb_movie.Position = "主演";
                            celeb_movie.Order = order++;
                            //_step_celeb_movies.Add(celeb_movie);
                            _basic_movie.Celebs.Add(celeb_movie);
                        }
                    }
                }

                if (hnInfo.InnerHtml.Contains(">类型:<"))
                {
                    var dires = hnInfo.CssSelect("span[property='v:genre']");
                    foreach (var dir in dires)
                    {
                        _basic_movie.Genres.Add(new Step_Movie_Genre_Info() { Genre = dir.InnerText.Trim(), Id = Guid.NewGuid(), Movie = _basic_movie.Id });
                    }
                }
                if (hnInfo.InnerHtml.Contains(">官方网站:<"))
                {
                    var dires = hnInfo.CssSelect("a").FirstOrDefault();
                    _basic_movie.Website = dires.ChildAttributes("href").FirstOrDefault().Value;
                }
                if (hnInfo.InnerHtml.Contains(">制片国家/地区:<"))
                {
                    var dires = hnInfo.InnerText.Replace("制片国家/地区:", "").Split('/');
                    foreach (var dir in dires)
                    {
                        _basic_movie.Countries.Add(new Step_Movie_Country_Info() { Country = dir.Trim(), Id = Guid.NewGuid(), Movie = _basic_movie.Id });
                    }
                }
                if (hnInfo.InnerHtml.Contains(">语言:<"))
                {
                    var dires = hnInfo.InnerText.Replace("语言:", "").Split('/');
                    foreach (var dir in dires)
                    {
                        _basic_movie.Langs.Add(new Step_Movie_Lang_Info() { Lang = dir.Trim(), Id = Guid.NewGuid(), Movie = _basic_movie.Id });
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
                    _basic_movie.Pubdate = string.Join(" / ", str);
                }
                if (hnInfo.InnerHtml.Contains(">季数:<") && (_basic_movie.Season_Count < 0 || _basic_movie.Current_Season < 0))
                {
                    var dires = hnInfo.CssSelect("option");
                    if (dires.Count() > 0)
                    {
                        if (_basic_movie.Current_Season < 0)
                        {
                            foreach (var dir in dires)
                            {
                                if (dir.ChildAttributes("selected").Count() > 0 && dir.ChildAttributes("selected").FirstOrDefault().Value == "selected")
                                {
                                    _basic_movie.Current_Season = (short)(dires.ToList().IndexOf(dir) + 1);
                                }
                            }
                        }
                        if (_basic_movie.Season_Count < 0)
                        {
                            _basic_movie.Season_Count = (short)(dires.Count());
                        }
                    }
                    else
                    {
                        if (_basic_movie.Current_Season < 0)
                        {
                            _basic_movie.Current_Season = short.Parse(hnInfo.InnerText.Replace("季数:", "").Trim());
                        }
                    }
                }
                if (hnInfo.InnerHtml.Contains(">集数:<") && _basic_movie.Episode_Count < 0)
                {
                    _basic_movie.Episode_Count = short.Parse(hnInfo.InnerText.Replace("集数:", "").Trim());
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
                        _basic_movie.Duration = string.Join(" / ", str);
                    }
                    else
                    {
                        _basic_movie.Duration = hnInfo.InnerText.Replace("片长:", "").Replace("单集", "").Trim();
                    }
                }
                if (hnInfo.InnerHtml.Contains(">又名:<"))
                {
                    var dires = hnInfo.InnerText.Replace("又名:", "").Split('/');
                    List<string> str = new List<string>();
                    foreach (var dir in dires)
                    {
                        str.Add(dir.Trim());
                        _basic_movie.Aka = string.Join(" / ", str);
                    }
                }
                if (hnInfo.InnerHtml.Contains(">IMDb链接:<"))
                {
                    _basic_movie.IMDb = hnInfo.CssSelect("a").FirstOrDefault().InnerText.Trim();
                }
            }

            if (html.CssSelect("span.all.hidden").Count() > 0)
            {
                _basic_movie.Summary = html.CssSelect("span.all.hidden").FirstOrDefault().InnerText.TrimAll();
            }
            else if (html.CssSelect("span[property='v:summary']").Count() > 0)
            {
                _basic_movie.Summary = html.CssSelect("span[property='v:summary']").FirstOrDefault().InnerText.TrimAll();
            }

            var ratingNode = html.CssSelect("div#interest_sectl");
            if (ratingNode.CssSelect("strong.rating_num").FirstOrDefault().InnerText.Length > 0)
            {
                _basic_movie.Rating.Score = double.Parse(ratingNode.CssSelect("strong.rating_num").FirstOrDefault().InnerText);
            }
            if (ratingNode.CssSelect("span[property='v:votes']").Count() > 0)
            {
                _basic_movie.Rating.Rated_Num = int.Parse(ratingNode.CssSelect("span[property='v:votes']").FirstOrDefault().InnerText);
            }
            var ratingpers = ratingNode.CssSelect("span.rating_per");
            if (ratingNode.CssSelect("span.rating_per").Count() > 0)
            {
                _basic_movie.Rating.Star5 = ratingpers.First().InnerText.Trim();
                _basic_movie.Rating.Star4 = ratingpers.ToList()[1].InnerText.Trim();
                _basic_movie.Rating.Star3 = ratingpers.ToList()[1].InnerText.Trim();
                _basic_movie.Rating.Star2 = ratingpers.ToList()[1].InnerText.Trim();
                _basic_movie.Rating.Star1 = ratingpers.Last().InnerText.Trim();
            }
            _basic_movie.Rating.Id = Guid.NewGuid();
            _basic_movie.Rating.Movie = _basic_movie.Id;

            _basic_movie.Poster.Id = Guid.NewGuid();
            _basic_movie.Poster.Movie = _basic_movie.Id;

            //Step_Celeb_Movie_BLL.Insert(_step_celeb_movies);
            return _basic_movie;
        }
        //public static RequestResult GetMovie(string douban)
        //{
        //    if (Basic_Movie_BLL.Exist(douban))
        //    {
        //        return new RequestResult() { Error = douban + "已存在", Succeeded = false };
        //    }

        //    Basic_Movie_Info _basic_movie = new Basic_Movie_Info();

        //    List<Step_Celeb_Movie_Info> _step_celeb_movies = new List<Step_Celeb_Movie_Info>();

        //    _basic_movie.Douban = douban;
        //    _basic_movie.Id = Guid.NewGuid();
        //    GetMovieFromJson(ref _basic_movie);

        //    string strhtml = HtmlHelper.GetHtmlCode(string.Format("https://movie.douban.com/subject/{0}/", douban));
        //    if (strhtml.IsBlank())
        //        return new RequestResult() { Error = "不存在豆瓣编号为 " + douban + " 的电影", Succeeded = false };
        //    HtmlDocument hdoc = new HtmlDocument();
        //    hdoc.LoadHtml(strhtml);
        //    HtmlNode html = hdoc.DocumentNode;

        //    try
        //    {
        //        if (_basic_movie.Title.IsBlank())
        //        {
        //            _basic_movie.Title = html.CssSelect("Title").FirstOrDefault().InnerText.Replace("(豆瓣)", "").Trim();
        //        }
        //        if (_basic_movie.Title_En.IsBlank())
        //        {
        //            _basic_movie.Title_En = html.CssSelect("a.nbgnbg']").FirstOrDefault().CssSelect("img").FirstOrDefault().ChildAttributes("alt").FirstOrDefault().Value;
        //        }
        //        if (_basic_movie.Year > 0)
        //        {
        //            _basic_movie.Year = short.Parse(html.CssSelect("span.year").FirstOrDefault().InnerText.Replace("(", "").Replace(")", "").Trim());
        //        }
        //        if (_basic_movie.Poster.Large.Length == 0)
        //        {
        //            _basic_movie.Poster.Large = html.CssSelect("div#mainpic").FirstOrDefault().CssSelect("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;
        //        }

        //        foreach (var item in html.CssSelect("div#info").FirstOrDefault().InnerHtml.Split('\n'))
        //        {
        //            HtmlDocument docinfo = new HtmlDocument();
        //            docinfo.LoadHtml(item);
        //            HtmlNode hnInfo = docinfo.DocumentNode;

        //            if (hnInfo.InnerHtml.Contains(">导演<"))
        //            {
        //                var dires = hnInfo.CssSelect("a");
        //                byte order = 0;
        //                foreach (var dir in dires)
        //                {
        //                    if (dir.ChildAttributes("href").FirstOrDefault().Value.Contains("/search/"))
        //                        continue;

        //                    Guid workguid = GetCeleb(dir.ChildAttributes("href").FirstOrDefault().Value.Replace("celebrity", "").Replace("/", ""));

        //                    if (workguid != Guid.Empty)
        //                    {
        //                        Step_Celeb_Movie_Info celeb_movie = new Step_Celeb_Movie_Info();
        //                        celeb_movie.Id = Guid.NewGuid();
        //                        celeb_movie.Celeb = workguid;
        //                        celeb_movie.Movie = _basic_movie.Id;
        //                        celeb_movie.Position = "导演";
        //                        celeb_movie.Order = order++;
        //                        _step_celeb_movies.Add(celeb_movie);
        //                    }
        //                }
        //            }
        //            if (hnInfo.InnerHtml.Contains(">编剧<"))
        //            {
        //                var dires = hnInfo.CssSelect("a");
        //                byte order = 0;
        //                foreach (var dir in dires)
        //                {
        //                    if (dir.ChildAttributes("href").FirstOrDefault().Value.Contains("/search/"))
        //                        continue;

        //                    Guid workguid = GetCeleb(dir.ChildAttributes("href").FirstOrDefault().Value.Replace("celebrity", "").Replace("/", ""));

        //                    if (workguid != Guid.Empty)
        //                    {
        //                        Step_Celeb_Movie_Info celeb_movie = new Step_Celeb_Movie_Info();
        //                        celeb_movie.Id = Guid.NewGuid();
        //                        celeb_movie.Celeb = workguid;
        //                        celeb_movie.Movie = _basic_movie.Id;
        //                        celeb_movie.Position = "编剧";
        //                        celeb_movie.Order = order++;
        //                        _step_celeb_movies.Add(celeb_movie);
        //                    }
        //                }
        //            }
        //            if (hnInfo.InnerHtml.Contains(">主演<"))
        //            {
        //                var dires = hnInfo.CssSelect("a");
        //                byte order = 0;
        //                foreach (var dir in dires)
        //                {
        //                    if (dir.ChildAttributes("href").FirstOrDefault().Value.Contains("/search/"))
        //                        continue;

        //                    Guid workguid = GetCeleb(dir.ChildAttributes("href").FirstOrDefault().Value.Replace("celebrity", "").Replace("/", ""));

        //                    if (workguid != Guid.Empty)
        //                    {
        //                        Step_Celeb_Movie_Info celeb_movie = new Step_Celeb_Movie_Info();
        //                        celeb_movie.Id = Guid.NewGuid();
        //                        celeb_movie.Celeb = workguid;
        //                        celeb_movie.Movie = _basic_movie.Id;
        //                        celeb_movie.Position = "主演";
        //                        celeb_movie.Order = order++;
        //                        _step_celeb_movies.Add(celeb_movie);
        //                    }
        //                }
        //            }

        //            if (hnInfo.InnerHtml.Contains(">类型:<"))
        //            {
        //                var dires = hnInfo.CssSelect("span[property='v:genre']");
        //                foreach (var dir in dires)
        //                {
        //                    _basic_movie.Genres.Add(new Step_Movie_Genre_Info() { Genre = dir.InnerText.Trim(), Id = Guid.NewGuid(), Movie = _basic_movie.Id });
        //                }
        //            }
        //            if (hnInfo.InnerHtml.Contains(">官方网站:<"))
        //            {
        //                var dires = hnInfo.CssSelect("a").FirstOrDefault();
        //                _basic_movie.Website = dires.ChildAttributes("href").FirstOrDefault().Value;
        //            }
        //            if (hnInfo.InnerHtml.Contains(">制片国家/地区:<"))
        //            {
        //                var dires = hnInfo.InnerText.Replace("制片国家/地区:", "").Split('/');
        //                foreach (var dir in dires)
        //                {
        //                    _basic_movie.Countries.Add(new Step_Movie_Country_Info() { Country = dir.Trim(), Id = Guid.NewGuid(), Movie = _basic_movie.Id });
        //                }
        //            }
        //            if (hnInfo.InnerHtml.Contains(">语言:<"))
        //            {
        //                var dires = hnInfo.InnerText.Replace("语言:", "").Split('/');
        //                foreach (var dir in dires)
        //                {
        //                    _basic_movie.Langs.Add(new Step_Movie_Lang_Info() { Lang = dir.Trim(), Id = Guid.NewGuid(), Movie = _basic_movie.Id });
        //                }
        //            }
        //            if (hnInfo.InnerHtml.Contains(">上映日期:<") || hnInfo.InnerHtml.Contains(">首播:<"))
        //            {
        //                var dires = hnInfo.CssSelect("span[property='v:initialReleaseDate']");
        //                List<string> str = new List<string>();
        //                foreach (var dir in dires)
        //                {
        //                    str.Add(dir.InnerText.Trim());
        //                }
        //                _basic_movie.Pubdate = string.Join(" / ", str);
        //            }
        //            if (hnInfo.InnerHtml.Contains(">季数:<") && (_basic_movie.Season_Count < 0 || _basic_movie.Current_Season < 0))
        //            {
        //                var dires = hnInfo.CssSelect("option");
        //                if (dires.Count() > 0)
        //                {
        //                    if (_basic_movie.Current_Season < 0)
        //                    {
        //                        foreach (var dir in dires)
        //                        {
        //                            if (dir.ChildAttributes("selected").Count() > 0 && dir.ChildAttributes("selected").FirstOrDefault().Value == "selected")
        //                            {
        //                                _basic_movie.Current_Season = (short)(dires.ToList().IndexOf(dir) + 1);
        //                            }
        //                        }
        //                    }
        //                    if (_basic_movie.Season_Count < 0)
        //                    {
        //                        _basic_movie.Season_Count = (short)(dires.Count());
        //                    }
        //                }
        //                else
        //                {
        //                    if (_basic_movie.Current_Season < 0)
        //                    {
        //                        _basic_movie.Current_Season = short.Parse(hnInfo.InnerText.Replace("季数:", "").Trim());
        //                    }
        //                }
        //            }
        //            if (hnInfo.InnerHtml.Contains(">集数:<") && _basic_movie.Episode_Count < 0)
        //            {
        //                _basic_movie.Episode_Count = short.Parse(hnInfo.InnerText.Replace("集数:", "").Trim());
        //            }
        //            if (hnInfo.InnerHtml.Contains(">片长:<") || hnInfo.InnerHtml.Contains(">单集片长:<"))
        //            {
        //                var dires = hnInfo.CssSelect("span[property='v:runtime']");
        //                if (dires.Count() > 0)
        //                {
        //                    List<string> str = new List<string>();
        //                    foreach (var dir in dires)
        //                    {
        //                        str.Add(dir.InnerText.Trim());
        //                    }
        //                    _basic_movie.Duration = string.Join(" / ", str);
        //                }
        //                else
        //                {
        //                    _basic_movie.Duration = hnInfo.InnerText.Replace("片长:", "").Replace("单集", "").Trim();
        //                }
        //            }
        //            if (hnInfo.InnerHtml.Contains(">又名:<"))
        //            {
        //                var dires = hnInfo.InnerText.Replace("又名:", "").Split('/');
        //                List<string> str = new List<string>();
        //                foreach (var dir in dires)
        //                {
        //                    str.Add(dir.Trim());
        //                    _basic_movie.Aka = string.Join(" / ", str);
        //                }
        //            }
        //            if (hnInfo.InnerHtml.Contains(">IMDb链接:<"))
        //            {
        //                _basic_movie.IMDb = hnInfo.CssSelect("a").FirstOrDefault().InnerText.Trim();
        //            }
        //        }

        //        if (html.CssSelect("span.all.hidden").Count() > 0)
        //        {
        //            _basic_movie.Summary = html.CssSelect("span.all.hidden").FirstOrDefault().InnerText.TrimAll();
        //        }
        //        else if (html.CssSelect("span[property='v:summary']").Count() > 0)
        //        {
        //            _basic_movie.Summary = html.CssSelect("span[property='v:summary']").FirstOrDefault().InnerText.TrimAll();
        //        }

        //        var ratingNode = html.CssSelect("div#interest_sectl");
        //        if (ratingNode.CssSelect("strong.rating_num").FirstOrDefault().InnerText.Length > 0)
        //        {
        //            _basic_movie.Rating.Score = double.Parse(ratingNode.CssSelect("strong.rating_num").FirstOrDefault().InnerText);
        //        }
        //        if (ratingNode.CssSelect("span[property='v:votes']").Count() > 0)
        //        {
        //            _basic_movie.Rating.Rated_Num = int.Parse(ratingNode.CssSelect("span[property='v:votes']").FirstOrDefault().InnerText);
        //        }
        //        var ratingpers = ratingNode.CssSelect("span.rating_per");
        //        if (ratingNode.CssSelect("span.rating_per").Count() > 0)
        //        {
        //            _basic_movie.Rating.Star5 = ratingpers.First().InnerText.Trim();
        //            _basic_movie.Rating.Star4 = ratingpers.ToList()[1].InnerText.Trim();
        //            _basic_movie.Rating.Star3 = ratingpers.ToList()[1].InnerText.Trim();
        //            _basic_movie.Rating.Star2 = ratingpers.ToList()[1].InnerText.Trim();
        //            _basic_movie.Rating.Star1 = ratingpers.Last().InnerText.Trim();
        //        }
        //        _basic_movie.Rating.Id = Guid.NewGuid();
        //        _basic_movie.Rating.Movie = _basic_movie.Id;

        //        _basic_movie.Poster.Id = Guid.NewGuid();
        //        _basic_movie.Poster.Movie = _basic_movie.Id;

        //        Basic_Movie_BLL.Insert(_basic_movie);
        //        Step_Celeb_Movie_BLL.Insert(_step_celeb_movies);
        //    }
        //    catch (System.IO.InvalidDataException dataExp)
        //    {
        //        return new RequestResult() { Error = dataExp.Message, Succeeded = false };
        //        //Console.WriteLine(dataExp.Message);
        //    }
        //    catch (Exception)
        //    {
        //        return new RequestResult() { Error = douban + "电影源代码解析失败", Succeeded = false };
        //        //Console.WriteLine(_basic_movie.Douban + "电影源代码解析失败");
        //    }
        //    return new RequestResult() { Error = douban + "添加成功", Succeeded = true };
        //}

        public static RequestResult InsertMovie(string douban)
        {
            if (Basic_Movie_BLL.Exist(douban))
            {
                return new RequestResult() { Error = douban + "已存在", Succeeded = false };
            }

            try
            {
                Basic_Movie_Info movie = GetMovie(douban);
                Basic_Movie_BLL.Insert(movie);
                Step_Celeb_Movie_BLL.DeleteList(Basic_Movie_BLL.GetSingle(douban).Id);
                Step_Celeb_Movie_BLL.Insert(movie.Celebs);
                return new RequestResult() { Error = douban + "添加成功", Succeeded = true };
            }
            catch (System.IO.InvalidDataException dataExp)
            {
                return new RequestResult() { Error = dataExp.Message, Succeeded = false };
                //Console.WriteLine(dataExp.Message);
            }
            catch (Exception)
            {
                return new RequestResult() { Error = douban + "电影源代码解析失败", Succeeded = false };
                //Console.WriteLine(_basic_movie.Douban + "电影源代码解析失败");
            }
        }

        public static RequestResult InsertMovie(string douban, Guid movie)
        {
            try
            {
                Basic_Movie_Info _basic_movie = GetMovie(douban);
                _basic_movie.Id = movie;
                Basic_Movie_BLL.Insert(_basic_movie);
                Step_Celeb_Movie_BLL.DeleteList(Basic_Movie_BLL.GetSingle(douban).Id);
                Step_Celeb_Movie_BLL.Insert(_basic_movie.Celebs);
                return new RequestResult() { Error = douban + "添加成功", Succeeded = true };
            }
            catch (System.IO.InvalidDataException dataExp)
            {
                return new RequestResult() { Error = dataExp.Message, Succeeded = false };
                //Console.WriteLine(dataExp.Message);
            }
            catch (Exception)
            {
                return new RequestResult() { Error = douban + "电影源代码解析失败", Succeeded = false };
                //Console.WriteLine(_basic_movie.Douban + "电影源代码解析失败");
            }
        }

        public static void GetMovieFromJson(ref Basic_Movie_Info _basic_movie)
        {
            string strjson = HtmlHelper.GetHtmlCode(string.Format("http://api.douban.com/v2/movie/subject/{0}", _basic_movie.Douban));
            if (strjson.IsBlank())
                return;
            MovieJson movie = JsonHelper.DeserializeJsonToObject<MovieJson>(strjson);

            _basic_movie.Title = movie.title;
            _basic_movie.Title_En = movie.original_title;
            _basic_movie.Douban = movie.id;
            _basic_movie.Summary = movie.summary;

            _basic_movie.Poster.Large = movie.images.large;
            _basic_movie.Poster.Medium = movie.images.medium;
            _basic_movie.Poster.Small = movie.images.small;
            if (!movie.year.IsBlank())
                _basic_movie.Year = short.Parse(movie.year);
            if (!movie.current_season.IsBlank())
                _basic_movie.Current_Season = short.Parse(movie.current_season);
            if (!movie.seasons_count.IsBlank())
                _basic_movie.Season_Count = short.Parse(movie.seasons_count);
            if (!movie.episodes_count.IsBlank())
                _basic_movie.Episode_Count = short.Parse(movie.episodes_count);
        }
        #endregion

        #region 获取影人
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
            if (_basic_celeb.Avatar.Large.IsBlank())
                _basic_celeb.Avatar.Large = html.CssSelect("a.nbg").FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value;
            if (_basic_celeb.Avatar.Medium.IsBlank())
                _basic_celeb.Avatar.Medium = html.CssSelect("a.nbg").FirstOrDefault().CssSelect("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;

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
                //System.Diagnostics.Debug.WriteLine(html.CssSelect("span.all.hidden").FirstOrDefault().InnerText);
            }
            else if (html.CssSelect("div#intro").FirstOrDefault().CssSelect("div.bd").Count() > 0)
            {
                _basic_celeb.Summary = html.CssSelect("div#intro").FirstOrDefault().CssSelect("div.bd").FirstOrDefault().InnerText.Replace("　　", "\n").TrimAll();
            }


            _basic_celeb.Id = Guid.NewGuid();

            _basic_celeb.Avatar.Id = Guid.NewGuid();
            _basic_celeb.Avatar.Celeb = _basic_celeb.Id;

            return _basic_celeb;
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
                //Console.WriteLine(_basic_celeb.Id + "影人源代码解析失败");

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
                //Console.WriteLine(_basic_celeb.Id + "影人源代码解析失败");

                return Guid.Empty;
            }

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

            _basic_celeb.Avatar.Large = celeb.avatars.large;
            _basic_celeb.Avatar.Medium = celeb.avatars.medium;
            _basic_celeb.Avatar.Small = celeb.avatars.small;
        }
        #endregion

        #region 帮助方法

        #endregion
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
