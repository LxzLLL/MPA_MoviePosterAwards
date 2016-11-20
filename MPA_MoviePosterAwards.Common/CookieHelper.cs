using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MPA_MoviePosterAwards.Common
{
    public class CookieHelper
    {
        /// <summary>
        /// 判断Cookie是否为空
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool HasCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies[key] != null && !HttpContext.Current.Request.Cookies[key].Value.Trim().IsBlank())
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取Cookie的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies[key] == null)
                return null;
            else
                return HttpContext.Current.Request.Cookies[key].Value;
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCookie(string key, string value)
        {
            HttpCookie cookie = new HttpCookie(key, value);
            cookie.Expires = DateTime.Now.AddHours(12);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expire">过期时间</param>
        public static void SetCookie(string key, DateTime expire)
        {
            HttpCookie cookie = new HttpCookie(key);
            cookie.Expires = expire;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 清空Cookie
        /// </summary>
        public static void ClearCookie()
        {
            HttpContext.Current.Response.Clear();
        }
    }
}
