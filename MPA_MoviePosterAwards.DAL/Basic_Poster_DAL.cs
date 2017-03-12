using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.DAL
{
    public class Basic_Poster_DAL
    {
        #region 查
        public Basic_Poster_Info GetSingle(Guid id)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_Poster.FirstOrDefault(p => p.Id == id);

                return ToModel(result);
            }
        }

        public List<Basic_Poster_Info> GetList(Guid Movie)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_Poster.Where(p => p.Movie == Movie);
                List<Basic_Poster_Info> infos = new List<Basic_Poster_Info>();
                foreach (var item in result)
                {
                    infos.Add(ToModel(item));
                }
                return infos;
            }
        }

        public List<Basic_Poster_Info> GetList(string condition)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_Poster.SqlQuery("SELECT * FROM Basic_Poster WHERE 1=1" + condition);
                List<Basic_Poster_Info> infos = new List<Basic_Poster_Info>();
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
                return database.Basic_Poster.Where(p => p.Id == id).Count() > 0 ? true : false;
            }
        }
        #endregion end查

        #region 增删改
        public bool Insert(Basic_Poster_Info info)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                try
                {
                    database.Basic_Poster.Add(ToDatabase(info));
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
                    Basic_Poster poster = database.Basic_Poster.FirstOrDefault(p => p.Id == id);
                    database.Basic_Poster.Remove(poster);

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

        public static Basic_Poster_Info ToModel(Basic_Poster poster)
        {
            Basic_Poster_Info info = new Basic_Poster_Info();

            info.Id = poster.Id;
            info.Movie = poster.Movie;
            info.User = poster.User;
            info.Poster = poster.Poster;
            info.Poster_M = poster.Poster_M;
            info.Poster_S = poster.Poster_S;
            info.Poster_XS = poster.Poster_XS;
            info.Time = (DateTime)poster.Time;
            info.Height = (int)poster.Height;
            info.Width = (int)poster.Width;

            return info;
        }

        public static Basic_Poster ToDatabase(Basic_Poster_Info info)
        {
            Basic_Poster poster = new Basic_Poster();

            poster.Id = info.Id;
            poster.Movie = info.Movie;
            poster.User = info.User;
            poster.Poster = info.Poster;
            poster.Poster_M = info.Poster_M;
            poster.Poster_S = info.Poster_S;
            poster.Poster_XS = info.Poster_XS;
            poster.Time = info.Time;
            poster.Height = info.Height;
            poster.Width = info.Width;

            return poster;
        }
    }
}
