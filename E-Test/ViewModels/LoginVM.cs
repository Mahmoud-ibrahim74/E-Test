using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;

namespace E_Test.ViewModels
{
    public class LoginVM
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
