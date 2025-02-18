using Lab1.Models;

namespace Lab1.Services
{
    public interface ICrudService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<StudentViewModel>> GetStudentsAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}