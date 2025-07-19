using ExamAppApi.Application.Dtos.Classes;
using ExamAppApi.Application.Dtos.Subjects;
using ExamAppApi.Core.Entities;
using ExamAppApi.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ClassesController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;

    public ClassesController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var classes = await _unitOfWork.Classes.GetAllAsync();
      var classesDto = classes.Select(s => new ClassesDto
      {
        Id = s.Id,
        ClassNumber = s.ClassNumber,
      }).ToList();
      return Ok(classesDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var _class = await _unitOfWork.Classes.GetByIdAsync(id);
     
      if (_class == null) return NotFound();

      var classDto = new ClassesDto
      {
        Id = _class.Id,
        ClassNumber = _class.ClassNumber,
      };

      return Ok(classDto);
    }
  }
}
