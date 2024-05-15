using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WycieczkiV2.Data;
using WycieczkiV2.Models;
using WycieczkiV2.Repository.Interfaces;

namespace WycieczkiV2.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly TripsContext _context;

        public StudentRepository(TripsContext context)
        {
            _context = context;
        }
        public Task<List<Student>> GetAllAsync()
        {
            return _context.Students.ToListAsync();
        }

        public ValueTask<Student?> GetByIdAsync(int? studentId)
        {
            return _context.Students.FindAsync(studentId);
        }
        public async Task InsertAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
        }

        public async Task DeleteAsync(Student studentId)
        {
            var student = await _context.Students.FindAsync(studentId.StudentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }

        public bool Exist(Student student)
        {
            return _context.Students.Any(e => e.StudentId == student.StudentId);
        }

    }
}
