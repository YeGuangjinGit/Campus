using Campus.Models;
using System.ComponentModel.DataAnnotations;

namespace Campus.ViewModels
{
    public class PlatformUploadViewModel
    {
        public string HeadPortrait { get; set; }

        [Display(Name = "标题")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        [Required(ErrorMessage = "{0}不能为空")]
        [MaxLength(2000000, ErrorMessage = "文章字数2000000字以内")]
        public string Content { get; set; }

        public int SpecialColumnId { get; set; }

        public List<SpecialColumn> SpecialColumns { get; set; }

    }
}
