namespace Campus.Models
{




    /// <summary>
    /// 点赞表
    /// </summary>
    public class Fabulous
    {
        public int FabulousId { get; set; }

        public string UserId { get; set; }

        public int WorksId { get; set; }

        public virtual AppUser User { get; set; }

        public virtual Works Works { get; set; }
    }



}
