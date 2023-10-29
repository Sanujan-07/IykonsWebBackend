using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iycons_web2._0.Model
{
    public class Media
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }
        public string? Filename { get; set; }
       

        public int PostId { get; set; }

        public virtual Posts Post { get; set; }
        public string FilePath { get; internal set; }
    }
}
