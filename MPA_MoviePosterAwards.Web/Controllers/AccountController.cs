using MPA_MoviePosterAwards.Common;
using MPA_MoviePosterAwards.BLL;
using MPA_MoviePosterAwards.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPA_MoviePosterAwards.Web.Filters;

namespace MPA_MoviePosterAwards.Web.Controllers
{
    public class AccountController : Controller
    {
        #region 登录
        //
        // GET: Account/Login/
        [AllowAnonymous]
        [LogonAttribute]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: Account/Login/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnurl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = AccountManager.SignInWithPassword(model.Account, model.Password);
            if (result.Succeeded)
            {
                if (AccountManager.CheckAdmin())
                {
                    return RedirectToAction("Index", "Movie", new { Area = "Manage" });
                }
                else
                {
                    return RedirectToLocal(returnurl);
                }
            }
            else
            {
                ModelState.AddModelError("", result.Error);
                return View(model);
            }
        }
        #endregion

        #region 注册
        //
        // GET: Account/Register/
        [AllowAnonymous]
        public ActionResult Register(string returnurl)
        {
            ViewBag.ReturnUrl = returnurl;
            return View();
        }

        //
        // POST: Account/Register/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model, string returnurl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = AccountManager.Create(model.Account, model.Password);
            if (result.Succeeded)
            {
                AccountManager.SignInWithPassword(model.Account, model.Password);

                if (AccountManager.CheckAdmin())
                {
                    return RedirectToAction("Index", "ManageMovie");
                }
                else
                {
                    return RedirectToLocal(returnurl);
                }
            }
            else
            {
                ModelState.AddModelError("", result.Error);
                return View(model);
            }
        }
        #endregion

        #region 注销
        //
        // POST: Account/LogOff/
        [HttpPost]
        [SignedIn]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(string returnurl)
        {
            AccountManager.SignOut();
            return RedirectToLocal(returnurl);
        }
        #endregion

        #region 修改密码
        //
        // GET: Account/ChangePwd/
        [AllowAnonymous]
        public ActionResult ChangePwd()
        {
            return View();
        }

        //
        // POST: Account/ChangePwd/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePwd(ChangePwdViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = Basic_User_BLL.GetSingle(Guid.Parse(CookieHelper.GetCookie("userid").ToString()));

            if (user.Password != model.OldPassword.DESEncryption())
            {
                ModelState.AddModelError("", "旧密码错误");
                return View(model);
            }
            if (model.Password == model.OldPassword)
            {
                ModelState.AddModelError("", "新旧密码不能相同");
                return View(model);
            }

            var result = AccountManager.ChangePassword(user.Id, model.Password);
            if (result.Succeeded)
            {
                user = Basic_User_BLL.GetSingle(Guid.Parse(CookieHelper.GetCookie("userid").ToString()));
                AccountManager.SignInWithPassword(user.Account, user.Password);
                return RedirectToAction("ChangePwd", "Account");
            }
            else
            {
                ModelState.AddModelError("", result.Error);
                return View(model);
            }
        }
        #endregion


        #region 忘记密码
        //
        // GET: Account/ForgotPwd/
        [AllowAnonymous]
        public ActionResult ForgotPwd()
        {
            return View();
        }

        //
        // POST: Account/ForgotPwd/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPwd(ForgotPwdViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = AccountManager.ValidateEmail(model.Account, model.Email);
            if (result.Succeeded)
            {
                Session["ResetAccount"] = model.Account;
                Session["CanReset"] = true;
                return RedirectToAction("ResetPwd", "Account");
            }
            else
            {
                ModelState.AddModelError("", result.Error);
                return View(model);
            }
        }
        #endregion

        #region 重置密码
        //
        // GET: Account/ResetPwd/
        [AllowAnonymous]
        [ResetPwd]
        public ActionResult ResetPwd()
        {
            return View();
        }

        //
        // POST: Account/ResetPwd/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPwd(ResetPwdViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Account = Session["ResetAccount"].ToString();
            var result = AccountManager.ResetPassword(model.Account, model.Password);
            if (result.Succeeded)
            {
                Session["CanReset"] = false;
                Session["ResetAccount"] = "";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError("", result.Error);
                return View();
            }
        }
        #endregion

        #region 帮助程序
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (!Url.IsLocalUrl(returnUrl) && !returnUrl.IsBlank())
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}