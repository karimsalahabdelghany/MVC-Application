using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Contexts
{
	public class MVCAppDbContext : IdentityDbContext<ApplicationUser>
	{
		public MVCAppDbContext(DbContextOptions<MVCAppDbContext> options) : base(options) 
		{

		}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//=> optionsBuilder.UseSqlServer("Server=.;Database=MVCAppDb; Trusted_Connection=true;");
	
		
		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }
	     //public DbSet<IdentityUser> Users { get; set; }
		//public DbSet<IdentityRole> Roles { get; set; }

	}
}
