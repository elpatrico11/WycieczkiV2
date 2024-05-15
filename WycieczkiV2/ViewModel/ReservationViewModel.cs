using WycieczkiV2.Models;

namespace WycieczkiV2.ViewModel
{
    public class ReservationViewModel
    {
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
