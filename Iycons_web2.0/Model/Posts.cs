using Azure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iycons_web2._0.Model
{
    public class Posts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creater
        {
            get
            {
                return User?.UserName;
            }
        }
        public DateTime CreateDate { get; set; } 
        public ICollection<PostTag> PostTags { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Media> MediaItems { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Posts()
        {
            // Set the CreateDate property to the current date and time when a new Post is created.
            CreateDate = DateTime.Now;
        }
    }
}
