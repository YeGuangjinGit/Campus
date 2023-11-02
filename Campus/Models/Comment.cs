using System.ComponentModel.DataAnnotations;

namespace Campus.Models
{



    /// <summary>
    /// 评论表
    /// </summary>
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public int? WorksId { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }

        public int? Parentid { get; set; }

        public DateTime ReleaseTime { get; set; }

        public int Like { get; set; }

        public virtual AppUser User { get; set; }

        public virtual Works Works { get; set; }

        public virtual Comment TComment { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }



}
