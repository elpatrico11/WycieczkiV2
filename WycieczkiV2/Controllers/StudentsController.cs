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
using AutoMapper;
using FluentValidation;


namespace WycieczkiV2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _context;

        private readonly IValidator<StudentViewModel> _studentValidator;

        private readonly IMapper _mapper;
        public StudentsController(IStudentService context, IValidator<StudentViewModel> studentValidator, IMapper mapper)
        {
            this._context = context;
            _studentValidator = studentValidator;
            _mapper = mapper;
        }


        

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _context.GetAllAsync();
            var studentList = _mapper.Map<List<Student>, List<StudentViewModel>>(students);
            return View(studentList);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var student = await _context.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

           

            var studentViewModel = _mapper.Map<Student, StudentViewModel>(student);
            return View(studentViewModel);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,FirstName,LastName,DateOfBirth,PhoneNumber,Email,Citizenship")] StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
               var student = _mapper.Map<StudentViewModel, Student>(studentViewModel);

                await _context.InsertAsync(student);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var studentViewModel = _mapper.Map<Student, StudentViewModel>(student);
            return View(studentViewModel);

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,DateOfBirth,PhoneNumber,Email,Citizenship")] StudentViewModel studentViewModel)
        {
            if (id != studentViewModel.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var student = _mapper.Map<StudentViewModel, Student>(studentViewModel);

                try
                {
                    _context.Update(student);
                    await _context.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student))
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
            return View(studentViewModel);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.GetByIdAsync(id);

            var studentViewModel = _mapper.Map<Student, StudentViewModel>(student);
               
            if (student == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.GetByIdAsync(id);
            if (student != null)
            {
                await _context.DeleteAsync(student);
            }

            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(Student student)
        {
            return _context.Exist(student);
        }
    }
}
