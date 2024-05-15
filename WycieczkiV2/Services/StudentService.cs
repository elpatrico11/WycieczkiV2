using WycieczkiV2.Models;
using WycieczkiV2.Repository.Interfaces;
using WycieczkiV2.Services.Interfaces;

namespace WycieczkiV2.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
        }

        public async Task DeleteAsync(Student studentId)
        {
            await _studentRepository.DeleteAsync(studentId);
        }

        public bool Exist(Student student)
        {
            return _studentRepository.Exist(student);
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async ValueTask<Student?> GetByIdAsync(int? StudentId)
        {
            return await _studentRepository.GetByIdAsync(StudentId);
        }

        public async Task InsertAsync(Student student)
        {
            await _studentRepository.InsertAsync(student);
        }

        public async Task SaveAsync()
        {
            await _studentRepository.SaveAsync();
        }

        public void Update(Student student) 
        {
            _studentRepository.Update(student);
        }


    }
}
