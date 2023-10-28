using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iycons_web2._0.Model
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Message { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Subject { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public int? PageId { get; set; }

        // This is a read-only property to calculate PageName based on PageId
        public string PageName
        {
            get
            {
                switch (PageId)
                {
                    case 1:
                        return "About Us";
                    case 2:
                        return "Fintech & solution";
                    case 3:
                        return "Contact Us";
                    case 4:
                        return "Technlogi&Innovation";
                    case 5:
                        return "Business&Finance";
                    case 6:
                        return "Business & Solution";
                    case 7:
                        return "Iyconnect";
                    // Add more cases for other PageIds if needed
                    default:
                        return "Unknown"; // Default value if PageId doesn't match any known values
                }
            }
        }
    }
}
