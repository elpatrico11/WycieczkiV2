using System.Collections.Generic;
using WycieczkiV2.Models;
namespace WycieczkiV2.Repository.Interfaces
{
    public interface ITripRepository
    {
        IEnumerable<Trip> GetAll();
        Trip? GetById(int TripId);
        void Insert(Trip trip);
        void Update(Trip trip);
        void Delete(int TripId);
        void Save();
    }
}
