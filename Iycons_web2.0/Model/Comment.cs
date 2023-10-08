using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iycons_web2._0.Model
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public string Commenddate { get; set; }
        public string CommandText { get; set; }
        public int PostId { get; set; }
        public Posts Posts { get; set; }

    }
}
