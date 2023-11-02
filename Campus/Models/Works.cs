using System.ComponentModel.DataAnnotations.Schema;

namespace Campus.Models
{


    public class Works
    {
        [NotMapped]
        public string EncryptedId { get; set; }

        public int WorksId { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string DisplayContent { get; set; }

        public DateTime ReleaseTime { get; set; }

        public int SpecialColumnId { get; set; }

        public int Browse { get; set; }

        public bool IsDelete { get; set; } = false;

		public virtual AppUser User { get; set; }

        public virtual SpecialColumn SpecialColumn { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Fabulous> Fabulous { get; set; }

    }

}
