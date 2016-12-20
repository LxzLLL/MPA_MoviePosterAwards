using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.DAL
{
    public class Basic_Movie_DAL
    {
        #region 查
        public Basic_Movie_Info GetSingle(Guid id)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_Movie.FirstOrDefault(p => p.Id == id);

                return ToModel(result);
            }
        }

        public Basic_Movie_Info GetSingle(string douban)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_Movie.FirstOrDefault(p => p.Douban == douban);

                return ToModel(result);
            }
        }

        public List<Basic_Movie_Info> GetList(string condition)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_Movie.SqlQuery("SELECT * FROM Basic_Movie WHERE 1=1" + condition);
                List<Basic_Movie_Info> infos = new List<Basic_Movie_Info>();
                foreach (var item in result)
                {
                    infos.Add(ToModel(item));
                }
                return infos;
            }
        }

        public bool Exist(Guid id)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                return database.Basic_Movie.FirstOrDefault(p => p.Id == id) != null ? true : false;
            }
        }

        public bool Exist(string douban)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                //System.Diagnostics.Debug.Write(database.Basic_Movie.Where(p => p.Douban == douban).Count());
                return database.Basic_Movie.FirstOrDefault(p => p.Douban == douban) != null ? true : false;
            }
        }
        #endregion end查

        #region 增删改
        public bool Insert(Basic_Movie_Info info)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                try
                {
                    database.Basic_Movie.Add(ToDatabase(info));
                    database.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        //public bool Update(Guid id, Dictionary<string, object> changes)
        //{
        //    using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
        //    {
        //        try
        //        {
        //            var movie = database.Basic_Movie.FirstOrDefault(p => p.Id == id);

        //            foreach (var item in changes)
        //            {
        //                switch (item.Key)
        //                {
        //                    case "title":
        //                        movie.Title = (string)item.Value;
        //                        break;
        //                    case "titleen":
        //                        movie.Title_En = (string)item.Value;
        //                        break;
        //                    case "aka":
        //                        movie.Aka = (string)item.Value;
        //                        break;
        //                    case "year":
        //                        movie.Year = (short)item.Value;
        //                        break;
        //                    case "website":
        //                        movie.Website = (string)item.Value;
        //                        break;
        //                    case "currentseason":
        //                        movie.Current_Season = (short)item.Value;
        //                        break;
        //                    case "seasoncount":
        //                        movie.Season_Count = (short)item.Value;
        //                        break;
        //                    case "episodecount":
        //                        movie.Episode_Count = (int)item.Value;
        //                        break;
        //                    case "pubdate":
        //                        movie.Pubdate = (string)item.Value;
        //                        break;
        //                    case "duration":
        //                        movie.Duration = (string)item.Value;
        //                        break;
        //                    case "douban":
        //                        movie.Douban = (string)item.Value;
        //                        break;
        //                    case "imdb":
        //                        movie.IMDb = (string)item.Value;
        //                        break;
        //                    case "summary":
        //                        movie.Summary = (string)item.Value;
        //                        break;
        //                    case "collectcount":
        //                        movie.Summary = (string)item.Value;
        //                        break;
        //                    case "altertime":
        //                        movie.Alter_Time = (DateTime)item.Value;
        //                        break;
        //                    case "country":
        //                        var removecountry = database.Step_Movie_Country.Where(p => p.Movie == id);
        //                        foreach (var item1 in removecountry)
        //                        {
        //                            database.Step_Movie_Country.Remove(item1);
        //                        }
        //                        foreach (var item2 in (List<Step_Movie_Country_Info>)item.Value)
        //                        {
        //                            movie.Step_Movie_Country.Add(Step_Movie_Country_DAL.ToDatabase(item2));
        //                        }
        //                        break;
        //                    case "genre":
        //                        var removegenre = database.Step_Movie_Genre.Where(p => p.Movie == id);
        //                        foreach (var item1 in removegenre)
        //                        {
        //                            database.Step_Movie_Genre.Remove(item1);
        //                        }
        //                        foreach (var item2 in (List<Step_Movie_Genre_Info>)item.Value)
        //                        {
        //                            movie.Step_Movie_Genre.Add(Step_Movie_Genre_DAL.ToDatabase(item2));
        //                        }
        //                        break;
        //                    case "lang":
        //                        var removelang = database.Step_Movie_Lang.Where(p => p.Movie == id);
        //                        foreach (var item1 in removelang)
        //                        {
        //                            database.Step_Movie_Lang.Remove(item1);
        //                        }
        //                        foreach (var item2 in (List<Step_Movie_Lang_Info>)item.Value)
        //                        {
        //                            movie.Step_Movie_Lang.Add(Step_Movie_Lang_DAL.ToDatabase(item2));
        //                        }
        //                        break;
        //                    case "poster":
        //                        movie.Step_Movie_Poster.FirstOrDefault().Large = ((Step_Movie_Poster_Info)item.Value).Large;
        //                        movie.Step_Movie_Poster.FirstOrDefault().Small = ((Step_Movie_Poster_Info)item.Value).Small;
        //                        movie.Step_Movie_Poster.FirstOrDefault().Medium = ((Step_Movie_Poster_Info)item.Value).Medium;
        //                        break;
        //                    case "rating":
        //                        movie.Step_Movie_Rating.FirstOrDefault().Rated_Num = ((Step_Movie_Rating_Info)item.Value).Rated_Num;
        //                        movie.Step_Movie_Rating.FirstOrDefault().Score = ((Step_Movie_Rating_Info)item.Value).Score;
        //                        movie.Step_Movie_Rating.FirstOrDefault().Star1 = ((Step_Movie_Rating_Info)item.Value).Star1;
        //                        movie.Step_Movie_Rating.FirstOrDefault().Star2 = ((Step_Movie_Rating_Info)item.Value).Star2;
        //                        movie.Step_Movie_Rating.FirstOrDefault().Star3 = ((Step_Movie_Rating_Info)item.Value).Star3;
        //                        movie.Step_Movie_Rating.FirstOrDefault().Star4 = ((Step_Movie_Rating_Info)item.Value).Star4;
        //                        movie.Step_Movie_Rating.FirstOrDefault().Star5 = ((Step_Movie_Rating_Info)item.Value).Star5;
        //                        break;
        //                    default: break;
        //                }
        //            }
        //            database.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            return false;
        //        }
        //    }
        //}

        public bool Delete(Guid id)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                try
                {
                    Basic_Movie movie = database.Basic_Movie.FirstOrDefault(p => p.Id == id);
                    database.Basic_Movie.Remove(movie);

                    Step_Movie_Poster poster = database.Step_Movie_Poster.FirstOrDefault(p => p.Movie == id);
                    database.Step_Movie_Poster.Remove(poster);

                    Step_Movie_Rating rating = database.Step_Movie_Rating.FirstOrDefault(p => p.Movie == id);
                    database.Step_Movie_Rating.Remove(rating);

                    var country = database.Step_Movie_Country.Where(p => p.Movie == id);
                    database.Step_Movie_Country.RemoveRange(country);

                    var genre = database.Step_Movie_Genre.Where(p => p.Movie == id);
                    database.Step_Movie_Genre.RemoveRange(genre);

                    var lang = database.Step_Movie_Lang.Where(p => p.Movie == id);
                    database.Step_Movie_Lang.RemoveRange(lang);

                    var celeb = database.Step_Celeb_Movie.Where(p => p.Movie == id);
                    database.Step_Celeb_Movie.RemoveRange(celeb);

                    database.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        #endregion end增删改

        public static Basic_Movie_Info ToModel(Basic_Movie movie)
        {
            Basic_Movie_Info info = new Basic_Movie_Info();

            info.Id = movie.Id;
            info.Title = movie.Title;
            info.Title_En = movie.Title_En;
            info.Aka = movie.Aka;
            info.Year = (short)movie.Year;
            info.Website = movie.Website;
            info.Current_Season = (short)movie.Current_Season;
            info.Season_Count = (short)movie.Season_Count;
            info.Episode_Count = (int)movie.Episode_Count;
            info.Pubdate = movie.Pubdate;
            info.Duration = movie.Duration;
            info.Douban = movie.Douban;
            info.IMDb = movie.IMDb;
            info.Summary = movie.Summary;

            info.Poster = Step_Movie_Poster_DAL.ToModel(movie.Step_Movie_Poster.FirstOrDefault());
            info.Rating = Step_Movie_Rating_DAL.ToModel(movie.Step_Movie_Rating.FirstOrDefault());

            //info.Countries = new List<Step_Movie_Country_Info>();
            foreach (var item in movie.Step_Movie_Country)
            {
                info.Countries.Add(Step_Movie_Country_DAL.ToModel(item));
            }

            //info.Genres = new List<Step_Movie_Genre_Info>();
            foreach (var item in movie.Step_Movie_Genre)
            {
                info.Genres.Add(Step_Movie_Genre_DAL.ToModel(item));
            }

            //info.Langs = new List<Step_Movie_Lang_Info>();
            foreach (var item in movie.Step_Movie_Lang)
            {
                info.Langs.Add(Step_Movie_Lang_DAL.ToModel(item));
            }

            return info;
        }

        public static Basic_Movie ToDatabase(Basic_Movie_Info info)
        {
            Basic_Movie movie = new Basic_Movie();

            movie.Id = info.Id;
            movie.Title = info.Title;
            movie.Title_En = info.Title_En;
            movie.Aka = info.Aka;
            movie.Year = info.Year;
            movie.Website = info.Website;
            movie.Current_Season = info.Current_Season;
            movie.Season_Count = info.Season_Count;
            movie.Episode_Count = info.Episode_Count;
            movie.Pubdate = info.Pubdate;
            movie.Duration = info.Duration;
            movie.Douban = info.Douban;
            movie.IMDb = info.IMDb;
            movie.Summary = info.Summary;

            List<Step_Movie_Poster> poster = new List<Step_Movie_Poster>();
            poster.Add(new Step_Movie_Poster()
            {
                Id = info.Poster.Id,
                Movie = info.Poster.Movie,
                Large = info.Poster.Large,
                Medium = info.Poster.Medium,
                Small = info.Poster.Small
            });
            movie.Step_Movie_Poster = poster;

            List<Step_Movie_Rating> rating = new List<Step_Movie_Rating>();
            rating.Add(new Step_Movie_Rating()
            {
                Id = info.Rating.Id,
                Movie = info.Rating.Movie,
                Rated_Num = info.Rating.Rated_Num,
                Score = info.Rating.Score,
                Star1 = info.Rating.Star1,
                Star2 = info.Rating.Star2,
                Star3 = info.Rating.Star3,
                Star4 = info.Rating.Star4,
                Star5 = info.Rating.Star5
            });
            movie.Step_Movie_Rating = rating;

            List<Step_Movie_Country> countries = new List<Step_Movie_Country>();
            foreach (var item in info.Countries)
            {
                countries.Add(new Step_Movie_Country()
                {
                    Id = item.Id,
                    Movie = item.Movie,
                    Country = item.Country
                });
            }
            movie.Step_Movie_Country = countries;

            List<Step_Movie_Lang> langs = new List<Step_Movie_Lang>();
            foreach (var item in info.Langs)
            {
                langs.Add(new Step_Movie_Lang()
                {
                    Id = item.Id,
                    Movie = item.Movie,
                    Lang = item.Lang
                });
            }
            movie.Step_Movie_Lang = langs;

            List<Step_Movie_Genre> genres = new List<Step_Movie_Genre>();
            foreach (var item in info.Genres)
            {
                genres.Add(new Step_Movie_Genre()
                {
                    Id = item.Id,
                    Movie = item.Movie,
                    Genre = item.Genre
                });
            }
            movie.Step_Movie_Genre = genres;

            return movie;
        }
    }
}
