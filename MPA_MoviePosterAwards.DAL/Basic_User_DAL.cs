using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.DAL
{
    public class Basic_User_DAL
    {
        #region 查
        public Basic_User_Info GetSingle(Guid id)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_User.FirstOrDefault(p => p.Id == id);
                if (result == null)
                    return null;
                else
                    return ToModel(result);
            }

        }


        public Basic_User_Info GetSingleByAccount(string account)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_User.FirstOrDefault(p => p.Account == account);
                if (result == null)
                    return null;
                else
                    return ToModel(result);
            }
        }


        public List<Basic_User_Info> GetList(string condition)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                var result = database.Basic_User.SqlQuery("select * from Basic_User where ", condition);
                List<Basic_User_Info> infos = new List<Basic_User_Info>();
                foreach (var item in result)
                {
                    infos.Add(ToModel(item));
                }
                return infos;
            }

        }


        public bool HasItem(Guid id)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                return database.Basic_User.FirstOrDefault(p => p.Id == id) != null ? true : false;
            }
        }
        #endregion

        #region 增删改
        public bool Insert(Basic_User_Info info)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                try
                {
                    database.Basic_User.Add(ToDatabase(info));
                    database.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }


        public bool Update(Guid id, Dictionary<string, string> changes)
        {
            using (MoviePosterAwardsEntities database = new MoviePosterAwardsEntities())
            {
                try
                {
                    Basic_User user = database.Basic_User.FirstOrDefault(p => p.Id == id);

                    foreach (var item in changes)
                    {
                        switch (item.Key)
                        {
                            case "password":
                                user.Password = item.Value;
                                break;
                            case "avatar":
                                user.Avatar = item.Value;
                                break;
                            case "cover":
                                user.Cover = item.Value;
                                break;
                            case "limit":
                                user.Limit = bool.Parse(item.Value);
                                break;
                            case "email":
                                user.Email = item.Value;
                                break;
                            case "phone":
                                user.Phone_Number = item.Value;
                                break;
                            case "change":
                                user.Change_Time = DateTime.Parse(item.Value);
                                break;
                            default: break;
                        }
                    }
                    database.SaveChanges();
                    return true;
                }
                catch (Exception)
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
                    Basic_User user = database.Basic_User.FirstOrDefault(p => p.Id == id);
                    database.Basic_User.Remove(user);
                    database.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion


        private Basic_User_Info ToModel(Basic_User user)
        {
            Basic_User_Info info = new Basic_User_Info();

            info.Id = user.Id;
            info.Account = user.Account;
            info.Password = user.Password;
            info.Avatar = user.Avatar;
            info.Cover = user.Cover;
            info.Limit = (bool)user.Limit;
            info.Email = user.Email;
            info.Phone_Number = user.Phone_Number;
            info.Create_Time = (DateTime)user.Create_Time;
            info.Change_Time = (DateTime)user.Change_Time;

            return info;
        }


        private Basic_User ToDatabase(Basic_User_Info info)
        {
            Basic_User user = new Basic_User();

            user.Id = info.Id;
            user.Account = info.Account;
            user.Password = info.Password;
            user.Avatar = info.Avatar;
            user.Cover = info.Cover;
            user.Limit = info.Limit;
            user.Email = info.Email;
            user.Phone_Number = info.Phone_Number;
            user.Create_Time = info.Create_Time;
            user.Change_Time = info.Change_Time;

            return user;
        }
    }
}
