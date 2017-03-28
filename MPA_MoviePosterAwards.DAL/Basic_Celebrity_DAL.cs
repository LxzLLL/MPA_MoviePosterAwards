using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.DAL
{
    public class Basic_Celebrity_DAL
    {
        #region 查
        public Basic_Celebrity_Info GetSingle(Guid id)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_Celebrity.FirstOrDefault(p => p.Id == id);

                return ToModel(result);
            }
        }

        public Basic_Celebrity_Info GetSingle(string douban)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_Celebrity.FirstOrDefault(p => p.Douban == douban);

                return ToModel(result);
            }
        }

        public List<Basic_Celebrity_Info> GetList(string condition)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_Celebrity.SqlQuery("SELECT * FROM Basic_Celebrity WHERE 1=1" + condition);
                List<Basic_Celebrity_Info> infos = new List<Basic_Celebrity_Info>();
                foreach (var item in result)
                {
                    infos.Add(ToModel(item));
                }
                return infos;
            }
        }

        public List<Basic_Celebrity_Info> GetList(Guid movie, string position)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_Celebrity.SqlQuery(string.Format(@"SELECT A.*,B.Position FROM Basic_Celebrity A LEFT JOIN Step_Celeb_Movie B ON  A.Id=B.Celeb WHERE B.Movie='{0}' AND B.Position='{1}' ORDER BY B.[Order]", movie.ToString(), position));
                List<Basic_Celebrity_Info> infos = new List<Basic_Celebrity_Info>();
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
                return database.Basic_Celebrity.FirstOrDefault(p => p.Id == id) != null ? true : false;
            }
        }

        public bool Exist(string douban)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                return database.Basic_Celebrity.FirstOrDefault(p => p.Douban == douban) != null ? true : false;
            }
        }
        #endregion end查

        #region 增删改
        public bool Insert(Basic_Celebrity_Info info)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                try
                {
                    database.Basic_Celebrity.Add(ToDatabase(info));
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
                    Basic_Celebrity celeb = database.Basic_Celebrity.FirstOrDefault(p => p.Id == id);
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

        public static Basic_Celebrity_Info ToModel(Basic_Celebrity celeb)
        {
            Basic_Celebrity_Info info = new Basic_Celebrity_Info();

            info.Id = celeb.Id;
            info.Name = celeb.Name;
            info.Aka = celeb.Aka;
            info.Name_En = celeb.Name_En;
            info.Aka_En = celeb.Aka_En;
            info.Gender = (bool)celeb.Gender;
            info.Profession = celeb.Profession;
            info.Birth_Date = celeb.Birth_Date;
            info.Death_Date = celeb.Death_Date;
            info.Born_Place = celeb.Born_Place;
            info.Family = celeb.Family;
            info.Douban = celeb.Douban;
            info.IMDb = celeb.IMDb;
            info.Summary = celeb.Summary;
            info.Avatar_Large = celeb.Avatar_Large;
            info.Avatar_Medium = celeb.Avatar_Medium;
            info.Avatar_Small = celeb.Avatar_Small;

            return info;
        }

        public static Basic_Celebrity ToDatabase(Basic_Celebrity_Info info)
        {
            Basic_Celebrity celeb = new Basic_Celebrity();

            celeb.Id = info.Id;
            celeb.Name = info.Name;
            celeb.Aka = info.Aka;
            celeb.Name_En = info.Name_En;
            celeb.Aka_En = info.Aka_En;
            celeb.Gender = info.Gender;
            celeb.Profession = info.Profession;
            celeb.Birth_Date = info.Birth_Date;
            celeb.Death_Date = info.Death_Date;
            celeb.Born_Place = info.Born_Place;
            celeb.Family = info.Family;
            celeb.Douban = info.Douban;
            celeb.IMDb = info.IMDb;
            celeb.Summary = info.Summary;
            celeb.Avatar_Large = info.Avatar_Large;
            celeb.Avatar_Medium = info.Avatar_Medium;
            celeb.Avatar_Small = info.Avatar_Small;

            return celeb;
        }
    }
}
