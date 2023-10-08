using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iycons_web2._0.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? UserName { get; set; } 
        public string? PasswordHash { get; set; }
        public string? Email { get; set; } 
        public ICollection<Posts> Posts { get; set; }
    }
}
