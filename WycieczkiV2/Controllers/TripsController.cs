using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FluentValidation;
using WycieczkiV2.Data;
using WycieczkiV2.Models;
using WycieczkiV2.Services.Interfaces;
using WycieczkiV2.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace WycieczkiV2.Controllers
{
    [Authorize]
    public class TripsController : Controller
    {
        private readonly ITripService _context;
        private readonly IValidator<TripViewModel> _tripValidator;
        private readonly IMapper _mapper;

        public TripsController(ITripService context, IValidator<TripViewModel> tripValidator, IMapper mapper)
        {
            _context = context;
            _tripValidator = tripValidator;
            _mapper = mapper;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            var trips = await _context.GetAllAsync();
            var tripList = _mapper.Map<List<Trip>, List<TripViewModel>>(trips);
            return View(tripList);
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var trip = await _context.GetByIdAsync(id);
            if (trip == null)
                return NotFound();

            var tripViewModel = _mapper.Map<Trip, TripViewModel>(trip);
            return View(tripViewModel);
        }

        // GET: Trips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripId,Name,Date,Price,Origin,Destination,Country")] TripViewModel tripViewModel)
        {
            var validationResult = await _tripValidator.ValidateAsync(tripViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(tripViewModel);
            }

            var trip = _mapper.Map<TripViewModel, Trip>(tripViewModel);
            await _context.InsertAsync(trip);
            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var trip = await _context.GetByIdAsync(id);
            if (trip == null)
                return NotFound();

            var tripViewModel = _mapper.Map<Trip, TripViewModel>(trip);
            return View(tripViewModel);
        }

        // POST: Trips/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TripId,Name,Date,Price,Origin,Destination,Country")] TripViewModel tripViewModel)
        {
            if (id != tripViewModel.TripId)
                return NotFound();

            var validationResult = await _tripValidator.ValidateAsync(tripViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(tripViewModel);
            }

            var trip = _mapper.Map<TripViewModel, Trip>(tripViewModel);
            try
            {
                _context.Update(trip);
                await _context.SaveAsync();
            }
            catch
            {
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var trip = await _context.GetByIdAsync(id);
            if (trip == null)
                return NotFound();

            var tripViewModel = _mapper.Map<Trip, TripViewModel>(trip);
            return View(tripViewModel);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.GetByIdAsync(id);
            if (trip != null)
            {
                await _context.DeleteAsync(trip);
                await _context.SaveAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(Trip trip)
        {
            return _context.Exist(trip);
        }
    }
}
