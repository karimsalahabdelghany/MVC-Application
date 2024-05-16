using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MVCAppDbContext _dbContext;

        public GenericRepository(MVCAppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T item)
        {
          await  _dbContext.Set<T>().AddAsync(item);
            
        }

        public void Delete(T item)
        {
            _dbContext.Set<T>().Remove(item);
           
        }

        public async Task<T> GetbyidAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
            
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T)==typeof(Employee))
            {
              return  (IEnumerable<T>) await _dbContext.Employees.Include(e=>e.Department).ToListAsync();
            }
            return await _dbContext.Set<T>().ToListAsync();
        }

        public void Update(T item)
        {
            _dbContext.Set<T>().Update(item);
           
        }
    }
}
