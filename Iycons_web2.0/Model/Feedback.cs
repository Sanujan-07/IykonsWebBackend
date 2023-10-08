using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iycons_web2._0.Model
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string CreatedDate { get; set; }
        public int PageId { get; set; }
        public string PageName { get; set; }

    }
}
