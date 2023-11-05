using System.ComponentModel.DataAnnotations;

namespace Iycons_web2._0.DTO
{
    public class FeedbackDto
    {
        public string? Name { get; set; }
        public string? Message { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public int? PageId { get; set; }
    }
}
