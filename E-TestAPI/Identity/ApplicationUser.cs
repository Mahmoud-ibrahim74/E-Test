using E_TestAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_TestAPI.Identity
{
	public class ApplicationUser:IdentityUser
	{
		[MaxLength(300)]
		[Column(TypeName = "nvarchar")]
        public string Address { get; set; }
		public string ImageUrl { get; set; }
		public int ClassId { get; set; }
		public StudentClass StudentClass { get; set; }
	}
}
