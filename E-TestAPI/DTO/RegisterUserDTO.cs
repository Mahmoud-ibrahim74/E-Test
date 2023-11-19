using System.ComponentModel.DataAnnotations;

namespace E_TestAPI.DTO
{
    public class RegisterUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string phoneNumber { get; set; }
        public string Address { get; set; }

        public int StudentclassId { get; set; }
        public string imgUrl { get; set; }
        public string roleType { get; set; }
    }
}