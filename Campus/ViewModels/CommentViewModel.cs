using Campus.Models;

namespace Campus.ViewModels
{
    public class CommentViewModel
    {
        public string HeadPortrait { get; set; }
        public int WorksId { get; set; }

        public int? Parentid { get; set; }
        public IEnumerable<Comment> Comments { get; set; }


    }
}
