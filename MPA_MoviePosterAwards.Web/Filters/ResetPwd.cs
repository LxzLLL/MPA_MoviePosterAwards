using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPA_MoviePosterAwards.Web.Filters
{
    public class ResetPwdAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Convert.ToBoolean(filterContext.HttpContext.Session["CanReset"]))
            {
                filterContext.Result = new RedirectResult("/Account/ForgotPwd");
            }
        }
    }
}