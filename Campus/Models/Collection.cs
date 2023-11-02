using System.ComponentModel.DataAnnotations;

namespace Campus.Models
{



    /// <summary>
    /// 收藏表
    /// </summary>

    public class Collection
    {
        public int CollectionId { get; set; }

        public string UserId { get; set; }

        public virtual AppUser User { get; set; }

        public int WorksId { get; set; }

        public virtual Works Works { get; set; }
    }



}
