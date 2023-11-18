using E_TestAPI.DTO;
using E_TestAPI.Models;
using E_TestAPI.Repo.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;
        public AdminController(IAdmin admin)
        {
            this._admin = admin;
        }

        #region StudentClassEndpoint
        [HttpGet("GetAllStudentClass")]
        public async Task<IActionResult> GetAllStudentClass()
        {
            var studentList = await _admin.GetAllStudentClass();
            var modifiedStudents = studentList.Select(s => new StudentClassDTO
            {
                Id = s.Id,
                name = s.Name,
            }).ToList();
            return Ok(modifiedStudents);
        }
        [HttpPost("AddStudentClass")]
        public async Task<IActionResult> AddStudentClass([FromBody] StudentClassDTO mdl)
        {
            if (mdl != null)
            {
                StudentClass student = new()
                {
                    Name = mdl.name
                };
                await _admin.AddStudentClass(student);
                return Ok(student);
            }
            else
            {
                return BadRequest("error occur");
            }
        }
        [HttpPut("UpdateStudentClass")]
        public async Task<IActionResult> UpdateStudentClass([FromBody] StudentClassDTO mdl)
        {
            if (mdl != null)
            {
                StudentClass student = new()
                {
                    Id = mdl.Id,
                    Name = mdl.name
                };
                var result = await _admin.UpdateStudentClass(student);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteStudentClass/{classId:int}")]
        public async Task<IActionResult> DeleteStudentClass(int classId)
        {
            var IsClassDeleted = await _admin.DeleteStudentClass(classId);
            if (IsClassDeleted > 0)
                return Ok("Item is Deleted Sucessfully");
            else
                return NotFound("Item Not Found");
        }
        #endregion
    }
}
