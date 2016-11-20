using MPA_MoviePosterAwards.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPA_MoviePosterAwards.Web.Filters
{
    /// <summary>
    /// 限制该操作方法只限于已登录用户执行，否则转到登录页面
    /// </summary>
    public class SignedInAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!CookieHelper.HasCookie("user"))
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}