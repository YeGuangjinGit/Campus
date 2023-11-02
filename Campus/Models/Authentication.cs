namespace Campus.Models
{



    /// <summary>
    /// 身份验证表
    /// </summary>

    public class Authentication
    {
        public int AuthenticationId { get; set; }

        public string UserId { get; set; }

        public string IdCard { get; set; }

        public string Photo { get; set; }

        public DateTime CreateAt { get; set; }

        public bool? IsPass { get; set; }

        public string Handle { get; set; }

        public virtual AppUser User { get; set; }
    }



}
