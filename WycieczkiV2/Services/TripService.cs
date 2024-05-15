using WycieczkiV2.Models;
using WycieczkiV2.Repository.Interfaces;
using WycieczkiV2.Services.Interfaces;

namespace WycieczkiV2.Services
{
    public class TripService : ITripService
    {
        
            private readonly ITripRepository _tripRepository;

            public TripService(ITripRepository tripRepository)
            {
                this._tripRepository = tripRepository;
            }

            public async Task DeleteAsync(Trip tripId)
            {
                await _tripRepository.DeleteAsync(tripId);
            }

            public bool Exist(Trip trip)
            {
                return _tripRepository.Exist(trip);
            }

            public async Task<List<Trip>> GetAllAsync()
            {
                return await _tripRepository.GetAllAsync();
            }

            public async ValueTask<Trip?> GetByIdAsync(int? TripId)
            {
                return await _tripRepository.GetByIdAsync(TripId);
            }

            public async Task InsertAsync(Trip trip)
            {
                await _tripRepository.InsertAsync(trip);
            }

            public async Task SaveAsync()
            {
                await _tripRepository.SaveAsync();
            }

            public void Update(Trip trip)
            {
                _tripRepository.Update(trip);
            }
        }
}
