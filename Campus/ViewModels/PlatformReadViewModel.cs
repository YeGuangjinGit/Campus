using Campus.Models;
using System.Collections.Generic;

namespace Campus.ViewModels
{
    public class PlatformReadViewModel
    {
        public string SpecialColumnValue { get; set; }
        public bool CollectionButton { get; set; } = false;
        public bool GoodButton { get; set; } = false;
        public bool FollowButton { get; set; }=false;
        public string Title { get; set; }

        public DateTime ReleaseTime { get; set; }

        public int Browse { get; set; }
        public int WorksId { get; set; }

        public int FabulousNum { get; set; }

        public AppUser User { get; set; }

        public string MyHeadPortrait { get; set; }

        public string Nickname { get; set; }

        public int TargetsNum { get; set; }

        public string Content { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
