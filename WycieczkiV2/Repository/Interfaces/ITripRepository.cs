﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WycieczkiV2.Models;

namespace WycieczkiV2.Repository.Interfaces
{
    public interface ITripRepository
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
