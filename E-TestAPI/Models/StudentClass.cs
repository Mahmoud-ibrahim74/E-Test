using E_TestAPI.Identity;
using System.Text.Json.Serialization;

namespace E_TestAPI.Models
{
    public class StudentClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ApplicationUser> ApplicationUsers {  get; set; } 
    }
}
