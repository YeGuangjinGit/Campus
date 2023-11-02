using System.ComponentModel.DataAnnotations;

namespace Campus.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "邮箱不能为空")]
        [EmailAddress]
        [Display(Name = "邮箱地址：")]
        public string Email { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "密码：")]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d).{6,16}$", ErrorMessage = "{0}至少包含字母、数字，6-16位")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码：")]
        [Required(ErrorMessage = "确认密码不能为空")]
        [Compare("Password", ErrorMessage = "密码与确认密码不一致，请重新输入。")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
