using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WycieczkiV2.Models;

namespace WycieczkiV2.ViewModel
{
    public class TripViewModel
    {
        public int TripId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; } 

        public string Country { get; set; } 
    }
}
