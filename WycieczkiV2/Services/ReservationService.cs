using WycieczkiV2.Models;
using WycieczkiV2.Repository.Interfaces;
using WycieczkiV2.Services.Interfaces;


namespace WycieczkiV2.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            this._reservationRepository = reservationRepository;
        }

        public async Task DeleteAsync(Reservation reservationId)
        {
            await _reservationRepository.DeleteAsync(reservationId);
        }

        public bool Exist(Reservation reservation)
        {
            return _reservationRepository.Exist(reservation);
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _reservationRepository.GetAllAsync();
        }

        public async ValueTask<Reservation?> GetByIdAsync(int? reservationId)
        {
            return await _reservationRepository.GetByIdAsync(reservationId);
        }

        public async Task InsertAsync(Reservation reservation)
        {
            await _reservationRepository.InsertAsync(reservation);
        }

        public async Task SaveAsync()
        {
            await _reservationRepository.SaveAsync();
        }

        public void Update(Reservation reservation)
        {
            _reservationRepository.Update(reservation);
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _reservationRepository.GetAllStudentsAsync();
        }
        public async Task<List<Trip>> GetAllTripsAsync()
        {
            return await _reservationRepository.GetAllTripsAsync();
        }
    }
}
