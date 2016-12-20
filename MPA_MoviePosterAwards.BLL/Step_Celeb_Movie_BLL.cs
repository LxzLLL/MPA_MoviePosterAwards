using MPA_MoviePosterAwards.DAL;
using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.BLL
{
    public class Step_Celeb_Movie_BLL
    {
        public static Step_Celeb_Movie_Info GetSingle(Guid id)
        {
            Step_Celeb_Movie_DAL _Celeb_Movie = new Step_Celeb_Movie_DAL();
            if (!_Celeb_Movie.Exist(id))
                return null;
            return _Celeb_Movie.GetSingle(id);
        }

        public static List<Step_Celeb_Movie_Info> GetList(string condition)
        {
            Step_Celeb_Movie_DAL _Celeb_Movie = new Step_Celeb_Movie_DAL();
            return _Celeb_Movie.GetList(condition);
        }

        public static List<Step_Celeb_Movie_Info> GetList(Guid movie)
        {
            Step_Celeb_Movie_DAL _Celeb_Movie = new Step_Celeb_Movie_DAL();
            return _Celeb_Movie.GetList(movie);
        }

        public static bool Exist(Guid id)
        {
            Step_Celeb_Movie_DAL _Celeb_Movie = new Step_Celeb_Movie_DAL();
            return _Celeb_Movie.Exist(id);
        }

        public static bool Insert(Step_Celeb_Movie_Info info)
        {
            Step_Celeb_Movie_DAL _Celeb_Movie = new Step_Celeb_Movie_DAL();

            if (info.Id == Guid.Empty)
                return false;
            return _Celeb_Movie.Insert(info);
        }

        public static bool Insert(ICollection<Step_Celeb_Movie_Info> infos)
        {
            Step_Celeb_Movie_DAL _Celeb_Movie = new Step_Celeb_Movie_DAL();
            return _Celeb_Movie.Insert(infos);
        }

        public static bool Delete(Guid id)
        {
            Step_Celeb_Movie_DAL _Celeb_Movie = new Step_Celeb_Movie_DAL();
            if (!_Celeb_Movie.Exist(id))
                return false;
            return _Celeb_Movie.Delete(id);
        }

        public static void DeleteList(Guid movie)
        {
            foreach (var item in GetList(movie))
            {
                Delete(item.Id);
            }
        }
    }
}
