using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;

namespace Demo.BLL.Repositories
{
	public class DepartmentRepository :GenericRepository<Department> ,IDepartmentRepository
	{
		//private readonly MVCAppDbContext _dbcontext;

		//public DepartmentRepository(MVCAppDbContext Dbcontext)    // ASK CLR for object from Dbcontext
		//{

		//	//Dbcontext = new MVCAppDbContext();                 //with every object from DepartmentRepository creating object from Dbcontext
		//	_dbcontext = Dbcontext;                              // _Dbcontext have same address of object Dbcontext
		//}
		//public int Add(Department department)
		//{
		//	 _dbcontext.Add(department);           //add locally
		//	return _dbcontext.SaveChanges();              //Remote
		//}
		
		//public int Delete(Department department)
		//{
		//	_dbcontext.Remove(department);
		//	return _dbcontext.SaveChanges();
		//}

		//public IEnumerable<Department> GetAll()
		//	=> _dbcontext.Departments.ToList();

		//public Department GetbyId(int Id)
		//{
		//	//var department = _dbcontext.Departments.Local.Where(d=>d.Id == Id).FirstOrDefault();     //search local
		//	//if(department == null) 
		//	//{
		//	//      department =  _dbcontext.Departments.Where(d => d.Id == Id).FirstOrDefault();

		//	//}
		//	//return department;
		//	 return _dbcontext.Departments.Find(Id);        //return department which his id match id(parameter)

		//}
		//public int Update(Department department)
		//{
		//	_dbcontext.Update(department);           //add locally
		//	return _dbcontext.SaveChanges();
		//}

		public DepartmentRepository(MVCAppDbContext dbContext):base(dbContext)
		{

		}
	}
}
