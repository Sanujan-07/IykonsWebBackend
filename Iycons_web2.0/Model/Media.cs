using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iycons_web2._0.Model
{
    public class Media
    {
        [Key]
       
        public int ImageId { get; set; }
        public string? Filename { get; set; }
        public string FilePath { get; internal set; }
        public int PostId { get; set; }
        public Posts Post { get; set; }
        
    }
}
