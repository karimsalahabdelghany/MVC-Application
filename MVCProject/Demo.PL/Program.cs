using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Demo.PL.MappingProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo.PL
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
            
            #region Configure Services that allow Dependancy Injection
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MVCAppDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"))
            );
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();    // allow dependancy injection for craeting object from class DepartmentRepository

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(M => M.AddProfiles(new List<Profile>()
            {
                new EmployeeProfile(),
                new UserProfile(),
                new RoleProfile(),
            }));

            //services.AddScoped<UserManager<ApplicationUser>>();
            //add interfaces
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;   //@ $
                options.Password.RequireDigit = true;    //123
                options.Password.RequireLowercase = true; // ab
                options.Password.RequireUppercase = true; //AB
            })
                .AddEntityFrameworkStores<MVCAppDbContext>().AddDefaultTokenProviders();       //add classes that implement its interfaces
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)       //add 3 services of Authentication [usermanager-signinmanager-RoleManager]
                .AddCookie(option =>
                {
                    option.LoginPath = "Account/Login";
                    option.AccessDeniedPath = "Home/Error";
                });
            #endregion
            var app = builder.Build();
            #region Configure Http Request Pipelines

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
            #endregion
            app.Run();
        }


    }
}
