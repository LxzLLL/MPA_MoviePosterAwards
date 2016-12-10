using MPA_MoviePosterAwards.DAL;
using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.BLL
{
    public class Basic_Poster_BLL
    {
        public static Basic_Poster_Info GetSingle(Guid id)
        {
            Basic_Poster_DAL _Basic_Poster = new Basic_Poster_DAL();
            if (!_Basic_Poster.Exist(id))
                return null;
            return _Basic_Poster.GetSingle(id);
        }


        public static List<Basic_Poster_Info> GetList(string condition)
        {
            Basic_Poster_DAL _Basic_Poster = new Basic_Poster_DAL();
            return _Basic_Poster.GetList(condition);
        }
        public static List<Basic_Poster_Info> GetList(Guid movie)
        {
            Basic_Poster_DAL _Basic_Poster = new Basic_Poster_DAL();
            return _Basic_Poster.GetList(movie);
        }

        public static bool Exist(Guid movie)
        {
            Basic_Poster_DAL _Basic_Poster = new Basic_Poster_DAL();
            return _Basic_Poster.Exist(movie);
        }

        public static bool Insert(Basic_Poster_Info info)
        {
            Basic_Poster_DAL _Basic_Poster = new Basic_Poster_DAL();

            if (info.Id == Guid.Empty)
                return false;
            return _Basic_Poster.Insert(info);
        }

        public static bool Delete(Guid id)
        {
            Basic_Poster_DAL _Basic_Poster = new Basic_Poster_DAL();
            if (!_Basic_Poster.Exist(id))
                return false;
            return _Basic_Poster.Delete(id);
        }
    }
}
