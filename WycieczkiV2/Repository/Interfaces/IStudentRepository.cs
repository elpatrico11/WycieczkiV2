using WycieczkiV2.Models;

namespace WycieczkiV2.Repository.Interfaces
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student GetById(int studentId);
        void Insert(Student student);
        void Update(Student student);
        void Delete(int studentId);
        void Save();
    }
}
