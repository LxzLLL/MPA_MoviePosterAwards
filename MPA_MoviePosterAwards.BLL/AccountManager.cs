using MPA_MoviePosterAwards.Common;
using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.BLL
{
    public class AccountManager
    {
        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>登录状态</returns>
        public static RequestResult SignInWithPassword(string account, string password)
        {
            var user = Basic_User_BLL.GetSingle(account);

            if (user == null)
            {
                return new RequestResult() { Succeeded = false, Error = "用户名不存在。" };
            }
            else if (user.Password == password.DESEncryption())
            {
                CookieHelper.SetCookie("user", account);
                CookieHelper.SetCookie("userid", user.Id.ToString());
                CookieHelper.SetCookie("usertype", user.Limit.ToString());
                return new RequestResult() { Succeeded = true };
            }
            else
            {
                return new RequestResult() { Succeeded = false, Error = "请检查用户名或密码是否正确。" };
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">用户名</param>
        public static void SignInWithCookie()
        {
            CookieHelper.SetCookie("user", CookieHelper.GetCookie("user"));
            CookieHelper.SetCookie("userid", CookieHelper.GetCookie("userid"));
            CookieHelper.SetCookie("usertype", CookieHelper.GetCookie("usertype"));
        }
        #endregion

        #region 登出
        /// <summary>
        /// 注销
        /// </summary>
        public static void SignOut()
        {
            CookieHelper.SetCookie("user", DateTime.Now.AddDays(-1000));
            CookieHelper.SetCookie("userid", DateTime.Now.AddDays(-1000));
            CookieHelper.SetCookie("usertype", DateTime.Now.AddDays(-1000));

            CookieHelper.ClearCookie();
        }
        #endregion

        #region 注册
        /// <summary>
        /// 创建账户
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>成功 or 失败，错误信息</returns>
        public static RequestResult Create(string account, string password)
        {
            if (Basic_User_BLL.GetSingle(account) != null)
                return new RequestResult() { Succeeded = false, Error = "用户名已存在" };
            Basic_User_Info user = new Basic_User_Info();
            user.Account = account;
            user.Password = password.DESEncryption();
            if (Basic_User_BLL.Insert(user))
                return new RequestResult() { Succeeded = true };
            else
                return new RequestResult() { Succeeded = false, Error = "插入数据库失败" };
        }
        #endregion

        #region 重置密码
        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="email">输入的邮箱</param>
        /// <returns>成功 or 失败，错误信息</returns>
        public static RequestResult ValidateEmail(string account, string email)
        {
            var user = Basic_User_BLL.GetSingle(account);
            if (user == null)
            {
                return new RequestResult() { Succeeded = false, Error = "用户名不存在" };
            }
            else if (user.Email == email)
            {
                return new RequestResult() { Succeeded = true };
            }
            else
            {
                return new RequestResult() { Succeeded = false, Error = "电子邮件错误" };
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns>成功 or 失败，错误信息</returns>
        public static RequestResult ResetPassword(string account, string password)
        {
            var user = Basic_User_BLL.GetSingle(account);
            password = password.DESEncryption();
            Dictionary<string, string> changes = new Dictionary<string, string>();
            changes.Add("password", password);
            if (Basic_User_BLL.Update(user.Id, changes))
            {
                return new RequestResult() { Succeeded = true };
            }
            else
            {
                return new RequestResult() { Succeeded = false, Error = "重置密码失败,请重试。。。" };
            }
        }
        #endregion

        /// <summary>
        /// 检查当前登录用户是否是管理员
        /// </summary>
        /// <returns></returns>
        public static bool CheckAdmin()
        {
            return bool.Parse(CookieHelper.GetCookie("usertype"));
        }
        /// <summary>
        /// 检查某个用户时候为管理员
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public static bool CheckAdmin(string id)
        {
            var user = Basic_User_BLL.GetSingleById(Guid.Parse(id));
            if (user == null)
                return false;
            else
                return user.Limit;
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>存在true，不存在false</returns>
        public static bool Exist(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id) || Basic_User_BLL.GetSingleById(Guid.Parse(id)) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class RequestResult
    {
        public bool Succeeded { get; set; }
        public string Error { get; set; }
    }
}
