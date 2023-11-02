using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Campus.ViewModels
{
    public class RegisterViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "{0}不能为空")]
        [RegularExpression("^[a-zA-Z0-9]{6,16}$", ErrorMessage = "{0}至少包含字母、数字，6-16位")]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "{0}不能为空")]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d).{6,16}$", ErrorMessage = "{0}至少包含字母、数字，6-16位")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请再次输入密码")]
        [Compare("Password", ErrorMessage = "两次密码不一致")]
        public string ConfirmPassword { get; set; }
    }
}
