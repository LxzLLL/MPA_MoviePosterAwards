using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MPA_MoviePosterAwards.Web.Models
{
    public class UserViewModel
    {
        public string ID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public string Cover { get; set; }
        public bool Limit { get; set; }
        public string CreateTime { get; set; }
        public string ChangeTime { get; set; }

        public UserViewModel(Basic_User_Info user)
        {
            ID = user.Id.ToString();
            Account = user.Account;
            Password = user.Password;
            Email = user.Email;
            PhoneNumber = user.Phone_Number;
            Avatar = user.Avatar;
            Cover = user.Cover;
            Limit = user.Limit;
            CreateTime = user.Create_Time.ToString("yyyy年MM月dd日 hh:mm:ss");
            ChangeTime = user.Change_Time.ToString("yyyy年MM月dd日 hh:mm:ss");
        }
    }
    public class LoginViewModel
    {
        [Required(ErrorMessage = "请输入 用户名。")]
        [Display(Name = "用户名")]
        public string Account { get; set; }

        [Required(ErrorMessage = "请输入 密码。")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "请输入 用户名。")]
        [Display(Name = "用户名")]
        [StringLength(20, ErrorMessage = "{0} 必须少于 {1} 个字符。")]
        public string Account { get; set; }

        [Required(ErrorMessage = "请输入 密码。")]
        [RegularExpression(@"^(?=.*\d.*)(?=.*[a-zA-Z].*).{6,}$", ErrorMessage = "密码 必须包括字符和数字，且长度不小于6")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "确认密码")]
        //[Compare("Password", ErrorMessage = "密码 和 确认密码 不匹配。")]
        //public string ConfirmPassword { get; set; }
    }

    public class ChangePwdViewModel
    {
        [Display(Name = "用户名")]
        public string Account { get; set; }

        [Required(ErrorMessage = "请输入 旧密码。")]
        [DataType(DataType.Password)]
        [Display(Name = "旧密码")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "请输入 新密码。")]
        [RegularExpression(@"^(?=.*\d.*)(?=.*[a-zA-Z].*).{6,}$", ErrorMessage = "密码 必须包括字符和数字，且长度不小于6")]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次输入的密码不一致。")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPwdViewModel
    {
        [Required(ErrorMessage = "请输入 用户名。")]
        [Display(Name = "用户名")]
        public string Account { get; set; }

        [Required(ErrorMessage = "请输入 电子邮件。")]
        [EmailAddress(ErrorMessage = "请输入正确的 电子邮件。")]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }

    public class ResetPwdViewModel
    {
        [Display(Name = "用户名")]
        public string Account { get; set; }

        [Required(ErrorMessage = "请输入 新密码。")]
        [RegularExpression(@"^(?=.*\d.*)(?=.*[a-zA-Z].*).{6,}$", ErrorMessage = "密码 必须包括字符和数字，且长度不小于6")]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }
}