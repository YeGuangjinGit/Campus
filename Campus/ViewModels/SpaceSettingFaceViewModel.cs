using System.ComponentModel.DataAnnotations;

namespace Campus.ViewModels
{
    public class SpaceSettingFaceViewModel
    {
        [Required(ErrorMessage = "没有选择文件")]
        public IFormFile File { get; set; }

        public string HeadPortrait { get; set; }
    }
}
