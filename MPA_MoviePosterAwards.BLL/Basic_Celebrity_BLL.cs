using MPA_MoviePosterAwards.DAL;
using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.BLL
{
    public class Basic_Celebrity_BLL
    {
        public static Basic_Celebrity_Info GetSingle(Guid id)
        {
            Basic_Celebrity_DAL _Basic_Celeb = new Basic_Celebrity_DAL();
            if (!_Basic_Celeb.Exist(id))
                return null;
            return _Basic_Celeb.GetSingle(id);
        }

        public static Basic_Celebrity_Info GetSingle(string douban)
        {
            Basic_Celebrity_DAL _Basic_Celeb = new Basic_Celebrity_DAL();
            if (!_Basic_Celeb.Exist(douban))
                return null;
            return _Basic_Celeb.GetSingle(douban);
        }

        public static List<Basic_Celebrity_Info> GetList(string condition)
        {
            Basic_Celebrity_DAL _Basic_User = new Basic_Celebrity_DAL();
            return _Basic_User.GetList(condition);
        }

        public static List<Basic_Celebrity_Info> GetList(Guid movie, string position)
        {
            Basic_Celebrity_DAL _Basic_User = new Basic_Celebrity_DAL();
            return _Basic_User.GetList(movie, position);
        }

        public static bool Exist(Guid id)
        {
            Basic_Celebrity_DAL _Basic_Celeb = new Basic_Celebrity_DAL();
            return _Basic_Celeb.Exist(id);
        }

        public static bool Exist(string douban)
        {
            Basic_Celebrity_DAL _Basic_Celeb = new Basic_Celebrity_DAL();
            return _Basic_Celeb.Exist(douban);
        }

        public static bool Insert(Basic_Celebrity_Info info)
        {
            Basic_Celebrity_DAL _Basic_Celeb = new Basic_Celebrity_DAL();

            if (info.Id == Guid.Empty)
                return false;
            return _Basic_Celeb.Insert(info);
        }

        //public static bool Update(Guid id, Dictionary<string, object> changes)
        //{
        //    Basic_Celebrity_DAL _Basic_Celeb = new Basic_Celebrity_DAL();
        //    if (!_Basic_Celeb.HasItem(id))
        //        return false;
        //    return _Basic_Celeb.Update(id, changes);
        //}

        public static bool Delete(Guid id)
        {
            Basic_Celebrity_DAL _Basic_Celeb = new Basic_Celebrity_DAL();
            if (!_Basic_Celeb.Exist(id))
                return false;
            return _Basic_Celeb.Delete(id);
        }
    }
}
