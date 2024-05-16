using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)   //ask clr for create objetc from class that implement interface IUintOfWork
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            //viewData =>keyvaluepair[Dictionary object]
            //used to transfer  from controller[action]method to it's view
            //.net framework 3.5
            // ViewData["Message"] = "Hello from view data";

            //2.viewbag => Dynamic Property [based on dynamic keyword]
            //used to transfer  from controller[action]method to it's view
            //.net framework 4.0
            // ViewBag.Message = "Hello from view bag";
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
            {
                 employees =await _unitOfWork.EmployeeRepository.GetAllAsync();
                
            }
            else
            {
                 employees =_unitOfWork.EmployeeRepository.GetEmployeesByName(SearchValue);
                
            }
            var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmployees);

        }
        public IActionResult Create()
        {
           //ViewBag.Departments = _departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create( EmployeeViewModel employeevm)
        {
            if(ModelState.IsValid)
            {
                //Manual mapping
                //var mappedemployee = new Employee()
                //{
                //    Name = employeevm.Name,
                //    Age = employeevm.Age,
                //    Address = employeevm.Address,
                //    Salary = employeevm.Salary, 
                //    Id = employeevm.Id,
                //};

                 employeevm.ImageName= DocumentSettings.UploadFile(employeevm.Image, "Images");
                 var mappedemployee = _mapper.Map<EmployeeViewModel,Employee>(employeevm);
                 _unitOfWork.EmployeeRepository.AddAsync(mappedemployee);
                 _unitOfWork.CompleteAsync();
                //int  Resualt =
                // if (Resualt > 0)
                // {
                //     TempData["Message"] = "Employee is Added Successfuly";

                // }
                return RedirectToAction(nameof(Index));
            }
            return View(employeevm);
            
        }
        public async Task<IActionResult> Details(int? id,string ViewName="Details")
        {
            if(id is null)
                return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.GetbyidAsync(id.Value);
            if(employee is null)
                return NotFound();
            var mappedemployee = _mapper.Map<Employee,EmployeeViewModel>(employee);
            return View(mappedemployee);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id,"Edit");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(EmployeeViewModel employeevm,[FromRoute] int? id )
        {
            if (id != employeevm.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {

                try
                {
                    employeevm.ImageName = DocumentSettings.UploadFile(employeevm.Image, "Images");
                    var MappedEmployee = _mapper.Map<EmployeeViewModel,Employee>(employeevm);
                    _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                    _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeevm);
        }
        public async Task< IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task< IActionResult> Delete(EmployeeViewModel employeevm,[FromRoute]int? id)
        {
            if(id != employeevm.Id)
                return BadRequest();
            if (ModelState.IsValid)       //srever side validation
            {
                try
                {
                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeevm);
                    _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                   var result =await  _unitOfWork.CompleteAsync();
                    if(result > 0 && employeevm.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(employeevm.ImageName, "Images");
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                }
            }
            return View(employeevm);
        }
           
    }
}
