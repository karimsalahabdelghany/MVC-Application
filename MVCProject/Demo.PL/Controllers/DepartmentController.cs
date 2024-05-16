using System.Threading.Tasks;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Demo.PL.Controllers
{
	
	public class DepartmentController : Controller
	{
	
        private readonly IUnitOfWork _unitOfWork;

        //private DepartmentRepository DepartmentRepository;
        public DepartmentController(IUnitOfWork unitOfWork) // ASK CLR for creating object from class that immplement interface IUnitOfWork
		{
            _unitOfWork = unitOfWork;

            //departmentRepository = new DepartmentRepository(); ;
        }
		//Base url /controller/Index
		public async Task<IActionResult> Index()
		{
			var deparments =await _unitOfWork.DepartmentRepository.GetAllAsync();
			return View(deparments);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(Department department)
		{
			if (ModelState.IsValid)       //server side validation
			{
				await _unitOfWork.DepartmentRepository.AddAsync(department);
				int Resualt =await _unitOfWork.CompleteAsync();
				//TempData => Dictionary object
				//TRansfer data from action to action
				if(Resualt> 0)
				{
					TempData["Message"] = "Department is created Succefuly";

                }
				return RedirectToAction(nameof(Index));
			}
			return View(department);

		}
		public async  Task<IActionResult> Details(int? id, string ViewName = "Details")
		{
			if (id is null)
			{
				return BadRequest();  //status code 400
			}
			var department =await _unitOfWork.DepartmentRepository.GetbyidAsync(id.Value);
			if (department == null)
				return NotFound();
			return View(ViewName, department);
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			//if (id is null)
			//{
			//    return BadRequest();  //status code 400
			//}
			//var department = _departmentRepository.GetbyId(id.Value);
			//if (department == null)
			//    return NotFound();
			//return View(department);
			return await Details(id, "Edit");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task< IActionResult> Edit(Department department, [FromRoute] int id)
		{
			if (id != department.Id)
			{
				return BadRequest();
			}
			if (ModelState.IsValid)
			{
				try
				{
					_unitOfWork.DepartmentRepository.Update(department);
					await _unitOfWork.CompleteAsync();
					return RedirectToAction(nameof(Index));
				}
				catch (System.Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(department);
		}
		public async Task<IActionResult> Delete(int? id)
		{
			//if (id is null)
			//{
			//	return BadRequest();  //status code 400
			//}
			//var department = _departmentRepository.GetbyId(id.Value);
			//if (department == null)
			//	return NotFound();
			//return View(department);
			return await Details(id, "Delete");
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
               ModelState.AddModelError(string.Empty, ex.Message);

            }
            return View(department);
            
        }
    }
}
