using Microsoft.Extensions.Hosting;

namespace Iycons_web2._0.Model
{
    public class PostTag
    {
        public int PostId { get; set; }
    
        public Posts Posts { get; set; }
        public int TagId { get; set; }
        public Tag Tags { get; set; }
    }
}
