using WycieczkiV2.Models;

namespace WycieczkiV2.Services.Interfaces;

public interface IStudentService
{
    Task<List<Student>> GetAllAsync();
    ValueTask<Student> GetByIdAsync(int? StudentId);
    Task InsertAsync(Student student);
    void Update(Student student);
    Task DeleteAsync(Student StudentId);
    Task SaveAsync();
    public bool Exist(Student StudentId);
}
