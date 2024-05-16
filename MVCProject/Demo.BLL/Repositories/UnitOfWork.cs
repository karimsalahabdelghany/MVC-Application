using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly MVCAppDbContext _dbContext;

        public UnitOfWork(MVCAppDbContext dbContext)  //ask clr for object from Dbcontext
        {
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public IEmployeeRepository EmployeeRepository { get ; set; }
        public IDepartmentRepository DepartmentRepository { get; set ; }

        public async Task<int> CompleteAsync()
        {
           return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
           _dbContext.Dispose();
        }
    }
}
