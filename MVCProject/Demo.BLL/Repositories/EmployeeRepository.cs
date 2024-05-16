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
    public class EmployeeRepository :GenericRepository<Employee> ,IEmployeeRepository
    {
        private readonly MVCAppDbContext _dbContext;

        public EmployeeRepository(MVCAppDbContext dbContext):base(dbContext) {
            _dbContext = dbContext;
        }
        public IQueryable<Employee> GetEmployeesByAddress(string address)
        =>_dbContext.Employees.Where(e => e.Address == address);

        public IQueryable<Employee> GetEmployeesByName(string searchvalue)
        => _dbContext.Employees.Where(e=>e.Name.ToLower().Contains(searchvalue.ToLower())).Include(em=>em.Department);
        
    }
}
