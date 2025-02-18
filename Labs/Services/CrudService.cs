using AutoMapper;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Services
{
    public class CrudService<T> : ICrudService<T> where T : class
    {
        private readonly UniversitydbContext _context;
        private readonly IMapper _mapper;
        
        public CrudService(UniversitydbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        
        public async Task<IEnumerable<StudentViewModel>> GetStudentsAsync()
        {
            var students = await _context.Set<Student>()
                .ToListAsync();

            return _mapper.Map<IEnumerable<StudentViewModel>>(students);
        }
        
        

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}