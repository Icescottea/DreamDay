using System.ComponentModel.DataAnnotations;

namespace DreamDay.Models
{
    public class TimelineEvent
    {
        [Key]
        public int EventId { get; set; }

        public int WeddingId { get; set; } // Link to Wedding

        [Required]
        public string Title { get; set; } // Ceremony, Vendor Arrival, Reception, etc.

        [Required]
        public DateTime EventTime { get; set; } // Exact Time

        public string Description { get; set; } // Optional additional details

        public Wedding Wedding { get; set; } // Navigation property
    }
}
