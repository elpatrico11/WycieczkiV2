using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WycieczkiV2.Data;
using WycieczkiV2.Models;
using WycieczkiV2.Repository.Interfaces;

namespace WycieczkiV2.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly TripsContext _context;

        public ReservationRepository(TripsContext context)
        {
            _context = context;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _context.Reservations.ToList();
        }

        public Reservation GetById(int reservationId)
        {
            return _context.Reservations.Find(reservationId);
        }

        public void Insert(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
        }

        public void Update(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
        }

        public void Delete(int reservationId)
        {
            Reservation reservation = _context.Reservations.Find(reservationId);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
