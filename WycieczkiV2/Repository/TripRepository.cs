using Microsoft.EntityFrameworkCore;
using WycieczkiV2.Data;
using WycieczkiV2.Models;
using WycieczkiV2.Repository.Interfaces;

namespace WycieczkiV2.Repository
{
    public class TripRepository : ITripRepository
    {
        private readonly TripsContext _context;
        public TripRepository(TripsContext context)
        {
            _context = context;
        }
        public Task<List<Trip>> GetAllAsync()
        {
            return _context.Trips.ToListAsync();
        }

        public ValueTask<Trip?> GetByIdAsync(int? TripId)
        {
            return _context.Trips.FindAsync(TripId);
        }
        public async Task InsertAsync(Trip trip)
        {
            await _context.Trips.AddAsync(trip);
        }

         public void Update(Trip trip)
            {
                _context.Trips.Update(trip);
            }

        public async Task DeleteAsync(Trip tripId)
        {
            var trip = await _context.Trips.FindAsync(tripId.TripId);
            if (trip != null)
            {
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool Exist(Trip client)
        {
            return _context.Trips.Any(e => e.TripId == client.TripId);
        }

       
    }
}