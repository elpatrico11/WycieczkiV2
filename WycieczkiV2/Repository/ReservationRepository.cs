using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<List<Reservation>> GetAllAsync()
        {
            return _context.Reservations.ToListAsync();
        }

        public ValueTask<Reservation?> GetByIdAsync(int? reservationId)
        {
            return _context.Reservations.FindAsync(reservationId);
        }

        public async Task InsertAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }

        public void Update(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
        }

        public async Task DeleteAsync(Reservation reservationId)
        {

            var reservation = await _context.Reservations.FindAsync(reservationId.ReservationId);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }

        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool Exist(Reservation reservation)
        {
            return _context.Reservations.Any(e => e.ReservationId == reservation.ReservationId);
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<List<Trip>> GetAllTripsAsync()
        {
            return await _context.Trips.ToListAsync();
        }


    }
}
