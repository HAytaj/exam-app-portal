using ExamAppApi.Application.Dtos.Classes;
using ExamAppApi.Application.Dtos.Teachers;
using ExamAppApi.Core.Entities;
using ExamAppApi.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TeachersController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;

    public TeachersController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()//TeachersDto
    {
      var teachers = await _unitOfWork.Teachers.GetAllAsync();

      var teachersDto = teachers.Select(s => new TeachersDto
      {
        Id = s.Id,
        TeacherName = s.TeacherName,
        TeacherSurname = s.TeacherSurname,
        
      }).ToList();


      return Ok(teachersDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
      if (teacher == null) return NotFound();

      var teacherDto = new TeachersDto
      {
        Id = teacher.Id,
        TeacherName = teacher.TeacherName,
      };

      return Ok(teacher);
    }
  }
}
