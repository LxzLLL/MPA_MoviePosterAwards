using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.DAL
{
    public class Step_Celeb_Movie_DAL
    {
        #region 查
        public Step_Celeb_Movie_Info GetSingle(Guid id)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Step_Celeb_Movie.FirstOrDefault(p => p.Id == id);

                return ToModel(result);
            }

        }

        public bool HasItem(Guid id)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                return database.Step_Celeb_Movie.FirstOrDefault(p => p.Id == id) != null ? true : false;
            }
        }
        #endregion end查

        #region 增删改
        public bool Insert(Step_Celeb_Movie_Info info)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                try
                {
                    database.Step_Celeb_Movie.Add(ToDatabase(info));
                    database.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public bool Insert(ICollection<Step_Celeb_Movie_Info> infos)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                try
                {
                    foreach (var item in infos)
                    {
                        database.Step_Celeb_Movie.Add(ToDatabase(item));
                    }
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
        //            var celeb = database.Basic_Movie_Visit.FirstOrDefault(p => p.Id == id);

        //            foreach (var item in changes)
        //            {
        //                switch (item.Key)
        //                {
        //                    case "state":
        //                        celeb.State = (string)item.Value;
        //                        break;
        //                    case "note":
        //                        celeb.Note = (string)item.Value;
        //                        break;
        //                    default: break;
        //                }
        //            }
        //            database.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception)
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
                    var sigin = database.Step_Celeb_Movie.FirstOrDefault(p => p.Id == id);
                    database.Step_Celeb_Movie.Remove(sigin);
                    database.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion end增删改

        public static Step_Celeb_Movie_Info ToModel(Step_Celeb_Movie sigin)
        {
            Step_Celeb_Movie_Info info = new Step_Celeb_Movie_Info();

            info.Id = sigin.Id;
            info.Movie = (Guid)sigin.Movie;
            info.Celeb = (Guid)sigin.Celeb;
            info.Position = sigin.Position;

            return info;
        }

        public static Step_Celeb_Movie ToDatabase(Step_Celeb_Movie_Info info)
        {
            Step_Celeb_Movie sigin = new Step_Celeb_Movie();

            sigin.Id = info.Id;
            sigin.Movie = info.Movie;
            sigin.Celeb = info.Celeb;
            sigin.Position = info.Position;

            return sigin;
        }
    }
}
