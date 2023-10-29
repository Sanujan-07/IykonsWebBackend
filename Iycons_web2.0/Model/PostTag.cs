using Microsoft.Extensions.Hosting;

namespace Iycons_web2._0.Model
{
    public class PostTag
    {
        public int PostId { get; set; }
    
        public virtual Posts Posts { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tags { get; set; }
    }
}
