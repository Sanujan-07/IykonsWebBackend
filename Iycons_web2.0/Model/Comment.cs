using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Iycons_web2._0.Model
{
    public class Comment
    {
        [Key]
        
        public int CommentId { get; set; }
        public string? Commenddate { get; set; }
        public string? CommandText { get; set; }
        [ForeignKey("Posts")]
        public int PostId { get; set; }

        
        [JsonIgnore]
        public Posts Posts { get; set; }

    }
}
