using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDay.Models
{
    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; } // Venue, Photographer, Florist, etc.

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceStartingFrom { get; set; }

        public string ServicesOffered { get; set; }

        public string Reviews { get; set; } // (Optional) for now plain text, later can improve
    }
}
