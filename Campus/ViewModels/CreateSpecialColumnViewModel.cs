using System.ComponentModel.DataAnnotations;

namespace Campus.ViewModels
{
    public class CreateSpecialColumnViewModel
    {
        [Display(Name = "分栏名称")]
        [MaxLength(10, ErrorMessage = "最长不能超过10个字")]
        public string SpecialColumnValue { get; set; }
    }
}
