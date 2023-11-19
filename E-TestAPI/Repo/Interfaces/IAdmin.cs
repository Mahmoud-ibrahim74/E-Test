using E_TestAPI.DTO;
using E_TestAPI.Identity;
using E_TestAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_TestAPI.Repo.Interfaces
{
    public interface IAdmin
    {






        #region StudentClassCRUD
        Task<List<StudentClass>> GetAllStudentClass();
        Task<int> AddStudentClass(StudentClass studentClass);
        Task<int> UpdateStudentClass(StudentClass studentClass);
        Task<int> DeleteStudentClass(int studentClassId); 
        #endregion







    }
}
