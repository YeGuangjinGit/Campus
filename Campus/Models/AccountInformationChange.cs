using System.ComponentModel.DataAnnotations;

namespace Campus.Models
{



    /// <summary>
    /// 账号信息变更表
    /// </summary>

    public class AccountInformationChange
    {
        public int AccountChangeId { get; set; }

        public string UserId { get; set; }

        public string ChangeReason { get; set; }

        public string Code { get; set; }

		public DateTime CreateAt { get; set; }

		public virtual AppUser User { get; set; }
    }


}
