using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Campus.Models
{



    /// <summary>
    /// 性别表
    /// </summary>
    public class Gender
    {
        public int GenderId { get; set; }

        public string GenderValue { get; set; }

        public virtual ICollection<AppUser> Users { get; set; }
    }



}
