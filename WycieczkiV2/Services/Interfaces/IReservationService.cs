using WycieczkiV2.Models;

namespace WycieczkiV2.Services.Interfaces
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetAllAsync();

        ValueTask<Reservation> GetByIdAsync(int? reservationId);
        Task InsertAsync(Reservation reservation);
        void Update(Reservation reservation);
        Task DeleteAsync(Reservation reservationId);
        Task SaveAsync();
        public bool Exist(Reservation reservationId);

        Task<List<Student>> GetAllStudentsAsync();
        Task<List<Trip>> GetAllTripsAsync();

        //Task<List<Reservation>> SortByPriceAsync();
    }
}
