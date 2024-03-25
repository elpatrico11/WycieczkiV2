using WycieczkiV2.Models;

namespace WycieczkiV2.Repository.Interfaces
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAll();
        Reservation GetById(int reservationId);
        void Insert(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(int reservationId);
        void Save();
    }
}
