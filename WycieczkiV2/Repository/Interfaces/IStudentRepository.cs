using WycieczkiV2.Models;

namespace WycieczkiV2.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        ValueTask<Student> GetByIdAsync(int? StudentId);
        Task InsertAsync(Student student);
        void Update(Student student);
        Task DeleteAsync(Student StudentId);
        Task SaveAsync();

        public bool Exist(Student StudentId);
    }
}
