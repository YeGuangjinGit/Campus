namespace Campus.Models
{



    /// <summary>
    /// 活动登记表
    /// </summary>

    public class Enroll
    {
        public int EnrolId { get; set; }
        public int ActivityId { get; set; }
        public string UserId { get; set; }
        public bool Participate { get; set; }

        public virtual AppUser User { get; set; }

        public virtual Activity Activity { get; set; }
    }




}
