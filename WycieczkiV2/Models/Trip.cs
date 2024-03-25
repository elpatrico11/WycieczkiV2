using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WycieczkiV2.Models
{
    public class Trip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public string Origin { get; set; } // Start destination of the trip

        public string Destination { get; set; } // End destination of the trip

        public string Country { get; set; } // Country of the trip
    }
}
