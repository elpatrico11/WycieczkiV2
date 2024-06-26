﻿using WycieczkiV2.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WycieczkiV2.Repository.Interfaces
{
    public interface IReservationRepository
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

    }
}
