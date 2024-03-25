using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WycieczkiV2.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int TripId { get; set; }
        public Trip Trip { get; set; }

        public DateTime DateOfReservation { get; set; }

        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }   

        public decimal TotalPrice { get; set; } 

        public int NumberOfPeople { get; set; } 

        public DateTime? PaymentDate { get; set; } 
    }
}
