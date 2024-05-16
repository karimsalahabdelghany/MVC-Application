﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser>userManager,IMapper mapper) 
		{
			_userManager = userManager;
            _mapper = mapper;
        }
		public async Task<IActionResult> Index(string SearchValue)
		{
			if(string.IsNullOrEmpty(SearchValue))
			{
				var Users =await _userManager.Users.Select
					  (U => new UserViewModel
					  {
						  Id = U.Id,
						  FName = U.FName,
						  LName = U.LName,
						  Email = U.Email,
						  PhoneNumber = U.PhoneNumber,
						  Roles = _userManager.GetRolesAsync(U).Result

					  }).ToListAsync();
				return View(Users);	
					
			  
			}
			else
			{
				var User=await _userManager.FindByEmailAsync(SearchValue);
				var mappeduser = new UserViewModel()
				{
					Id =User.Id,
					FName = User.FName,
					LName = User.LName,
					Email = User.Email,
					PhoneNumber = User.PhoneNumber,
					Roles = _userManager.GetRolesAsync(User).Result

				};
				return View(new List<UserViewModel>{ mappeduser});
			}
		    
		}
		public async Task<IActionResult> Details(string id , string ViewName = "Details")
		{
			if (id is null)
				return BadRequest();
			else
			{
                var user =await _userManager.FindByIdAsync(id);

				if(user is not null)
				{
					var mappeduser= _mapper.Map<ApplicationUser, UserViewModel>(user);
					return View(ViewName,mappeduser);
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
		public async Task< IActionResult> Edit(UserViewModel model,[FromRoute]string id)
		{
			if(id != model.Id)
				return BadRequest();
			if(ModelState.IsValid)
			{
				try
				{
					//  var mappeduser = _mapper.Map<UserViewModel, ApplicationUser>(model);
					    var user =await _userManager.FindByIdAsync(id);
						user.FName = model.FName;
						user.LName = model.LName;
						user.PhoneNumber = model.PhoneNumber;
                        await _userManager.UpdateAsync(user);

                        return RedirectToAction(nameof(Index));
                }
				catch(Exception ex)
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
                var user = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
			catch(Exception ex)
			{
				ModelState.AddModelError(string.Empty,ex.Message);
			     return RedirectToAction("Error", "Home");
			}
				
			
		}
	}
}
