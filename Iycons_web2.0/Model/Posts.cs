using Azure;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Iycons_web2._0.Model
{
    public class Posts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
   
        public DateTime CreateDate { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
       
        public ICollection<Media> MediaItems { get; set; } = new List< Media > ();
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Posts()
        {
            // Set the CreateDate property to the current date and time when a new Post is created.
            CreateDate = DateTime.Now;

        }

        
        
    }
}
