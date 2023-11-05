using Azure;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Iycons_web2._0.Model
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public ICollection<PostTag> TagPosts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Media> MediaItems { get; set; }
        public int UserId { get; set; }
       public User User { get; set; }
       
    }

    public class PostDetail
    {
        [Key]
        public int PostId { get; set; }

        public Posts Posts { get; set; }


        public Media media { get; set; }
        public Tag Tag { get; set; }

        public Comment Comment { get; set; }


    }
}
