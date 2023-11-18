using E_TestAPI.Identity;
using E_TestAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_TestAPI.Context
{
	public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string> // use this classes ti inherit from user and role to implement in DB
	{

		public AppDbContext(DbContextOptions options) : base(options)
		{

		}
		public virtual DbSet<StudentClass>  StudentClasses { get; set; }
	}
}
