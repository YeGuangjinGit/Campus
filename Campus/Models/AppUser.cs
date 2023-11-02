using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Campus.Models
{


    /// <summary>
    /// 用户表
    /// </summary>
    public class AppUser : IdentityUser
    {
        public string HeadPortrait { get; set; }

        public string Nickname { get; set; } = "user_" + (new Random().Next(1000000, 9999999).ToString("X"));

        public string PersonalSignature { get; set; }

        public int GenderId { get; set; }

        public DateTime? Birth { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? AuthenticationId { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual Authentication Authentication { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }

        public virtual ICollection<Fabulous> Fabulous { get; set; }

        public virtual ICollection<Follow> Follows { get; set; }

        public virtual ICollection<Follow> Targets { get; set; }

        public virtual ICollection<AccountInformationChange> InformationChanges { get; set; }

        public virtual ICollection<Opinion> Opinions { get; set; }

        public virtual ICollection<Opinion> Handles { get; set; }

        public virtual ICollection<Enroll> Enrolls { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }

        public virtual ICollection<Works> Works { get; set; }

    }


}
