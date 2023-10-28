using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Iycons_web2._0.Model
{
    public class ContactUs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; } = string.Empty;
        public string? Subject { get; set; }
        public string? Message { get; set; }
    }
}
