using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole>roleManager,IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task< IActionResult> Index(string SearchValue)
        {
            if(string.IsNullOrEmpty(SearchValue))
            {
              var Roles =await _roleManager.Roles.ToListAsync();
                var mappedrole = _mapper.Map<IEnumerable<IdentityRole>,IEnumerable<RoleViewModel>>(Roles);
                return View(mappedrole);
            }   
            else
            {
                var role = await _roleManager.FindByNameAsync(SearchValue);
                var mappedrole = _mapper.Map<IdentityRole,RoleViewModel>(role);
                return View(new List<RoleViewModel>() { mappedrole});  
            }
        }
        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
               var mappedrole = _mapper.Map<RoleViewModel,IdentityRole>(model);
               await _roleManager.CreateAsync(mappedrole);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
            
        }
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            else
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role is not null)
                {
                    var mappedrole = _mapper.Map<IdentityRole, RoleViewModel>(role);
                    return View(ViewName, mappedrole);
                }
                else
                    return NotFound();
            }
        }
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model, [FromRoute] string id)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    //  var mappeduser = _mapper.Map<RoleViewModel, IdentityRole>(model);
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name= model.RoleName;
                    
                    await _roleManager.UpdateAsync(role);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(model);
        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }


        }
    }
}
