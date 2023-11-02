namespace Campus.Models
{
    /// <summary>
    /// 校园活动表
    /// </summary>

    public class Activity
    {
        public int ActivityId { get; set; }

        public string UserId { get; set; }

        public string ActivityTitle { get; set; }

        public string ActivityContent { get; set; }

        public string ActivityLocale { get; set; }

        public int ActivityNumber { get; set; }

        public DateTime ActivityTime { get; set; }

        public DateTime ReleaseTime { get; set; }

        public virtual AppUser User { get; set; }

        public virtual ICollection<Enroll> Enrolls { get; set; }
    }
}
