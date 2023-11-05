using System.ComponentModel.DataAnnotations.Schema;

namespace Iycons_web2._0.Model
{
    public class PostTag
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Posts Post { get; set; }
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
