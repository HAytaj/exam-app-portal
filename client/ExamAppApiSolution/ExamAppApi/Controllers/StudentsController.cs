using AutoMapper;
using ExamAppApi.Application.Dtos.Classes;
using ExamAppApi.Application.Dtos.Students;
using ExamAppApi.Application.Dtos.Subjects;
using ExamAppApi.Core.Entities;
using ExamAppApi.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentsController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public StudentsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var students = await _unitOfWork.Students.GetAllAsync();
      var studentsDto = students.Select(s => new BaseStudentDto
      {
        Id = s.Id,
        StudentName = s.StudentName,
        StudentSurname = s.StudentSurname,
        StudentNumber = s.StudentNumber,
        ClassId = s.ClassId
      }).ToList();
      return Ok(studentsDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var student = await _unitOfWork.Students.GetByIdAsync(id);
      if (student == null) return NotFound();
      var studentDto = new BaseStudentDto
      {
        Id = student.Id,
        StudentName = student.StudentName,
        StudentSurname = student.StudentSurname,
        StudentNumber = student.StudentNumber,
        ClassId = student.ClassId
      };
      return Ok(studentDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(StudentUpsertDto studentDto)
    {
      var student = _mapper.Map<Students>(studentDto);

      await _unitOfWork.Students.AddAsync(student);
      await _unitOfWork.SaveChangesAsync();
      return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StudentUpsertDto studentDto)
    {
      if (id != studentDto.Id) return BadRequest();

      var student = _mapper.Map<Students>(studentDto);
      _unitOfWork.Students.Update(student);
      await _unitOfWork.SaveChangesAsync();
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var student = await _unitOfWork.Students.GetByIdAsync(id);
      if (student == null) return NotFound();

      _unitOfWork.Students.Delete(student);
      await _unitOfWork.SaveChangesAsync();
      return NoContent();
    }

    [HttpGet("with-class")]
    public async Task<IActionResult> GetStudentsWithClasses()
    {
      var subjects = await _unitOfWork.StudentsRepository.GetStudentsWithClassesAsync();

      var subjectsDto = subjects.Select(s => new StudentsWithClassesDto
      {
       Id = s.Id,
       StudentName = s.StudentName,
       StudentSurname = s.StudentSurname,
       StudentNumber = s.StudentNumber,
       ClassNumber = s.Class.ClassNumber,
       ClassId = s.ClassId
      }).ToList();

      return Ok(subjectsDto);
    }
  }
}
