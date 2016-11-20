using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPA_MoviePosterAwards.Common;

namespace MPA_MoviePosterAwards.Web.Filters
{
    /// <summary>
    /// 限制该操作方法只限于未登录用户执行，否则转到用户个人首页
    /// </summary>
    public class LogonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CookieHelper.HasCookie("user"))
            {
                filterContext.Result = new RedirectResult("/Mine/Index");
            }
        }
    }
}