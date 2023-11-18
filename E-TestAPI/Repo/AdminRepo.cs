using E_TestAPI.Context;
using E_TestAPI.Models;
using E_TestAPI.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_TestAPI.Repo
{
    public class AdminRepo : IAdmin
    {
        private readonly AppDbContext _context;
        public AdminRepo(AppDbContext context)
        {
            this._context = context;
        }

        #region StudentClassActions
        public async Task<int> AddStudentClass(StudentClass studentClass)
        {
            _context.StudentClasses.Add(studentClass);
            return await _context.SaveChangesAsync(); 
        }

        public async Task<int> DeleteStudentClass(int studentClassId)
        {
            var getStuClass = await _context.StudentClasses.FindAsync(studentClassId);
            if (getStuClass != null) {
                _context.StudentClasses.Remove(getStuClass);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<List<StudentClass>> GetAllStudentClass()
        {
            return await _context.StudentClasses.ToListAsync();
        }

        public async Task<int> UpdateStudentClass(StudentClass studentClass)
        {
            _context.StudentClasses.Update(studentClass);
            return await _context.SaveChangesAsync();
            
        }
        #endregion
    }
}
