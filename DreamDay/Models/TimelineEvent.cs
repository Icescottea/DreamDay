using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int? VendorId { get; set; }            // nullable Vendor reference

        public DateTime? VendorArrivalTime { get; set; } // nullable vendor arrival time

        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }
    }
}
