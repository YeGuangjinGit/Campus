namespace Campus.Models
{




    /// <summary>
    /// 意见表
    /// </summary>
    public class Opinion
    {
        public int OpinionId { get; set; }

        public string UserId { get; set; }

        public string OpinionTitle { get; set; }

        public string OpinionContent { get; set; }

        public DateTime ReleaseTime { get; set; }

        public string HandleId { get; set; }

        public string Result { get; set; }

        public virtual AppUser User { get; set; }

        public virtual AppUser Handle { get; set; }

    }



}
