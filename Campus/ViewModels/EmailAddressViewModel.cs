using System.ComponentModel.DataAnnotations;

namespace Campus.ViewModels
{
	public class EmailAddressViewModel
	{
        [Required(ErrorMessage ="邮箱不能为空")]
		[EmailAddress]
		public string Email { get; set; }
	}
}
