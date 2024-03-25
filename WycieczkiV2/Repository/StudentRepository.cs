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

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student GetById(int studentId)
        {
            return _context.Students.Find(studentId);
        }

        public void Insert(Student student)
        {
            _context.Students.Add(student);
        }

        public void Update(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
        }

        public void Delete(int studentId)
        {
            Student student = _context.Students.Find(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
