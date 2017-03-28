using MPA_MoviePosterAwards.DAL;
using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.BLL
{
    public class Basic_Movie_BLL
    {
        public static Basic_Movie_Info GetSingle(Guid id)
        {
            Basic_Movie_DAL _Basic_Movie = new Basic_Movie_DAL();
            if (!_Basic_Movie.Exist(id))
                return null;
            return _Basic_Movie.GetSingle(id);
        }
        public static Basic_Movie_Info GetSingle(string douban)
        {
            Basic_Movie_DAL _Basic_Movie = new Basic_Movie_DAL();
            if (!_Basic_Movie.Exist(douban))
                return null;
            return _Basic_Movie.GetSingle(douban);
        }

        public static List<Basic_Movie_Info> GetList(string condition)
        {
            Basic_Movie_DAL _Basic_Movie = new Basic_Movie_DAL();
            return _Basic_Movie.GetList(condition);
        }

        public static bool Exist(Guid id)
        {
            Basic_Movie_DAL _Basic_Movie = new Basic_Movie_DAL();
            return _Basic_Movie.Exist(id);
        }

        public static bool Exist(string douban)
        {
            Basic_Movie_DAL _Basic_Movie = new Basic_Movie_DAL();
            return _Basic_Movie.Exist(douban);
        }

        public static bool Insert(Basic_Movie_Info info)
        {
            Basic_Movie_DAL _Basic_Movie = new Basic_Movie_DAL();

            if (info.Id == Guid.Empty)
                return false;
            return _Basic_Movie.Insert(info);
        }

        public static bool Delete(Guid id)
        {
            Basic_Movie_DAL _Basic_Movie = new Basic_Movie_DAL();
            if (!_Basic_Movie.Exist(id))
                return false;
            return _Basic_Movie.Delete(id);
        }
    }
}
