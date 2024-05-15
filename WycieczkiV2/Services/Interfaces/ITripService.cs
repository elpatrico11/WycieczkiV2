using WycieczkiV2.Models;
using System.Collections.Generic;

namespace WycieczkiV2.Services.Interfaces
{
    public interface ITripService
    {
        Task<List<Trip>> GetAllAsync();
        ValueTask<Trip> GetByIdAsync(int? tripId);
        Task InsertAsync(Trip trip);
        void Update(Trip trip);
        Task DeleteAsync(Trip tripId);
        Task SaveAsync();

        public bool Exist(Trip tripId);
    }
}
