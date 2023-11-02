using System.ComponentModel.DataAnnotations.Schema;

namespace Campus.Models
{



    /// <summary>
    /// 关注表
    /// </summary>
    public class Follow
    {
        public int FollowId { get; set; }

        public string UserId { get; set; }

        public string TargetId { get; set; }

        public virtual AppUser User { get; set; }

        public virtual AppUser Target { get; set; }
    }



}
