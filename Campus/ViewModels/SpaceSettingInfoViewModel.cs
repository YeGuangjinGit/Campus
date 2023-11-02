using System.ComponentModel.DataAnnotations;

namespace Campus.ViewModels
{
    public class SpaceSettingInfoViewModel
    {
        [Required(ErrorMessage = "昵称不能为空")]
        [MinLength(1,ErrorMessage = "昵称长度最少1个字符")]
        [MaxLength(15,ErrorMessage = "昵称长度不能超过15个字符")]
        public string Nickname { get; set; }

        public string UserName { get; set; }
        [MaxLength(65,ErrorMessage = "个性签名长度不能超过65个字符")]
        public string PersonalSignature { get; set; }

        public int Gender { get; set; }
        [Required(ErrorMessage = "生日信息不能为空")]
        public DateTime? Birth { get; set; }
    }
}
