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

        public bool Delete(Guid id)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                try
                {
                    Basic_Movie movie = database.Basic_Movie.FirstOrDefault(p => p.Id == id);
                    database.Basic_Movie.Remove(movie);

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
            info.Country = movie.Country;
            info.Genre = movie.Genre;
            info.Language = movie.Language;
            info.Website = movie.Website;
            info.Current_Season = (short)movie.Current_Season;
            info.Season_Count = (short)movie.Season_Count;
            info.Episode_Count = (int)movie.Episode_Count;
            info.Pubdate = movie.Pubdate;
            info.Duration = movie.Duration;
            info.Douban = movie.Douban;
            info.IMDb = movie.IMDb;
            info.Summary = movie.Summary;
            info.Avatar_Large = movie.Avatar_Large;
            info.Avatar_Medium = movie.Avatar_Medium;
            info.Avatar_Small = movie.Avatar_Small;
            info.Rating_Score = (double)movie.Rating_Score;
            info.Rating_Count = (int)movie.Rating_Count;
            info.Rating_Star5 = movie.Rating_Star5;
            info.Rating_Star4 = movie.Rating_Star4;
            info.Rating_Star3 = movie.Rating_Star3;
            info.Rating_Star2 = movie.Rating_Star2;
            info.Rating_Star1 = movie.Rating_Star1;

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
            movie.Country = info.Country;
            movie.Genre = info.Genre;
            movie.Language = info.Language;
            movie.Website = info.Website;
            movie.Current_Season = info.Current_Season;
            movie.Season_Count = info.Season_Count;
            movie.Episode_Count = info.Episode_Count;
            movie.Pubdate = info.Pubdate;
            movie.Duration = info.Duration;
            movie.Douban = info.Douban;
            movie.IMDb = info.IMDb;
            movie.Summary = info.Summary;
            movie.Avatar_Large = info.Avatar_Large;
            movie.Avatar_Medium = info.Avatar_Medium;
            movie.Avatar_Small = info.Avatar_Small;
            movie.Rating_Score = info.Rating_Score;
            movie.Rating_Count = info.Rating_Count;
            movie.Rating_Star5 = info.Rating_Star5;
            movie.Rating_Star4 = info.Rating_Star4;
            movie.Rating_Star3 = info.Rating_Star3;
            movie.Rating_Star2 = info.Rating_Star2;
            movie.Rating_Star1 = info.Rating_Star1;

            return movie;
        }
    }
}
