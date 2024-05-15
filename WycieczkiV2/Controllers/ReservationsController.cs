using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WycieczkiV2.Data;
using WycieczkiV2.Models;
using WycieczkiV2.Repository.Interfaces;
using WycieczkiV2.Services.Interfaces;
using WycieczkiV2.ViewModel;
using FluentValidation;
using AutoMapper;


namespace WycieczkiV2.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _context;

        private readonly IValidator<ReservationViewModel> _reservationValidator;

        private readonly IMapper _mapper;
        public ReservationsController(IReservationService context, IValidator<ReservationViewModel> reservationValidator, IMapper mapper)
        {
            _context = context;
            _reservationValidator = reservationValidator;
            _mapper = mapper;
        }



        public async Task<IActionResult> Index()
        {
            var reservations = await _context.GetAllAsync();
            var reservationList = _mapper.Map<List<Reservation>, List<ReservationViewModel>>(reservations);
            return View(reservationList);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationViewModel = _mapper.Map<Reservation, ReservationViewModel>(reservation);

            return View(reservationViewModel);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["StudentId"] = new SelectList(await _context.GetAllStudentsAsync(), "StudentId", "FirstName");
            ViewData["TripId"] = new SelectList(await _context.GetAllTripsAsync(), "TripId", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,StudentId,TripId,DateOfReservation,StartDate,EndDate,TotalPrice,NumberOfPeople,PaymentDate")] ReservationViewModel reservationViewModel)
        {
            if (ModelState.IsValid)
            {
                

                var reservation = _mapper.Map<ReservationViewModel, Reservation>(reservationViewModel);
                await _context.InsertAsync(reservation);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(await _context.GetAllStudentsAsync(), "StudentId", "FirstName", reservationViewModel.StudentId);
            ViewData["TripId"] = new SelectList(await _context.GetAllTripsAsync(), "TripId", "Name", reservationViewModel.TripId);
            return View(reservationViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationViewModel = _mapper.Map<Reservation, ReservationViewModel>(reservation);

            ViewData["StudentId"] = new SelectList(await _context.GetAllStudentsAsync(), "StudentId", "FirstName", reservationViewModel.StudentId);
            ViewData["TripId"] = new SelectList(await _context.GetAllTripsAsync(), "TripId", "Name", reservationViewModel.TripId);

            return View(reservationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,StudentId,TripId,DateOfReservation,StartDate,EndDate,TotalPrice,NumberOfPeople,PaymentDate")] ReservationViewModel reservationViewModel)
        {
            if (id != reservationViewModel.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var reservation = _mapper.Map<ReservationViewModel, Reservation>(reservationViewModel);
                try
                {
                    _context.Update(reservation);
                    await _context.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ReservationExists(reservation.ReservationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(await _context.GetAllStudentsAsync(), "StudentId", "FirstName", reservationViewModel.StudentId);
            ViewData["TripId"] = new SelectList(await _context.GetAllTripsAsync(), "TripId", "Name", reservationViewModel.TripId);
            return View(reservationViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.GetByIdAsync(id);

            var reservationViewModel = _mapper.Map<Reservation, ReservationViewModel>(reservation);

            if (reservation == null)
            {
                return NotFound();
            }

            ViewData["StudentId"] = new SelectList(await _context.GetAllStudentsAsync(), "StudentId", "FirstName", reservationViewModel.StudentId);
            ViewData["TripId"] = new SelectList(await _context.GetAllTripsAsync(), "TripId", "Name", reservationViewModel.TripId);

            return View(reservationViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.GetByIdAsync(id);
            if (reservation != null)
            {
                await _context.DeleteAsync(reservation);
            }

            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ReservationExists(int id)
        {
            var reservation = await _context.GetByIdAsync(id);
            return reservation != null;
        }
    }
}
