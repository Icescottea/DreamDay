using System;
using System.ComponentModel.DataAnnotations;

namespace DreamDay.Models
{
    public class ChecklistItem
    {
        [Key]
        public int ItemId { get; set; }
        public int WeddingId { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}
