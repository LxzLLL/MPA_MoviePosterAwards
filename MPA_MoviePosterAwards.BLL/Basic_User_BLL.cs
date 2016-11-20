using MPA_MoviePosterAwards.Common;
using MPA_MoviePosterAwards.DAL;
using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.BLL
{
    public class Basic_User_BLL
    {
        public static Basic_User_Info GetSingleById(Guid id)
        {
            Basic_User_DAL _Basic_User = new Basic_User_DAL();
            return _Basic_User.GetSingle(id);
        }

        public static Basic_User_Info GetSingleByAccount(string account)
        {
            Basic_User_DAL _Basic_User = new Basic_User_DAL();
            return _Basic_User.GetSingleByAccount(account);
        }

        public static List<Basic_User_Info> GetList(string condition)
        {
            Basic_User_DAL _Basic_User = new Basic_User_DAL();
            return _Basic_User.GetList(condition);
        }

        public static bool Insert(Basic_User_Info info)
        {
            Basic_User_DAL _Basic_User = new Basic_User_DAL();
            info.Id = Guid.NewGuid();
            info.Avatar = "User.jpg";
            info.Cover = "Cover.jpg";
            if (info.Id == Guid.Empty || info.Account.IsBlank() || info.Password.IsBlank())
                return false;
            return _Basic_User.Insert(info);
        }

        public static bool Update(Guid id, Dictionary<string, string> changes)
        {
            Basic_User_DAL _Basic_User = new Basic_User_DAL();
            if (!_Basic_User.HasItem(id))
                return false;
            return _Basic_User.Update(id, changes);
        }

        public static bool Delete(Guid id)
        {
            Basic_User_DAL _Basic_User = new Basic_User_DAL();
            if (!_Basic_User.HasItem(id))
                return false;
            return _Basic_User.Delete(id);
        }
    }
}
