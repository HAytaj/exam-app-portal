using AutoMapper;
using ExamAppApi.Application.Dtos.Students;
using ExamAppApi.Application.Dtos.Subjects;
using ExamAppApi.Core.Entities;
using ExamAppApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SubjectsController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public SubjectsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var subjects = await _unitOfWork.Subjects.GetAllAsync();
      var subjectDto = subjects.Select(s => new BaseSubjectDto
      {
        Id = s.Id,
        Code = s.Code,
        SubjectName = s.SubjectName,
        TeacherId = s.TeacherId,
        ClassId = s.ClassId
      }).ToList();
      return Ok(subjectDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var subject = await _unitOfWork.Subjects.GetByIdAsync(id);
      if (subject == null) return NotFound();
      var subjectDto = new BaseSubjectDto
      {
        Id = subject.Id,
        Code = subject.Code,
        SubjectName = subject.SubjectName,
        TeacherId = subject.TeacherId,
        ClassId = subject.ClassId
      };
      return Ok(subject);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SubjectUpsertDto subjectDto)
    {
      var subject = _mapper.Map<Subjects>(subjectDto);

      await _unitOfWork.Subjects.AddAsync(subject);
      await _unitOfWork.SaveChangesAsync();
      return CreatedAtAction(nameof(Get), new { id = subject.Id }, subject);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SubjectUpsertDto subjectDto)
    {
      if (id != subjectDto.Id) return BadRequest();

      var subject = _mapper.Map<Subjects>(subjectDto);
      _unitOfWork.Subjects.Update(subject);
      await _unitOfWork.SaveChangesAsync();
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var subject = await _unitOfWork.Subjects.GetByIdAsync(id);
      if (subject == null) return NotFound();

      _unitOfWork.Subjects.Delete(subject);
      await _unitOfWork.SaveChangesAsync();
      return NoContent();
    }

    [HttpGet("with-teachers-classes")]
    public async Task<IActionResult> GetSubjectsWitTeachersAndClasses()
    {
      var subjects = await _unitOfWork.SubjectsRepository.GetSubjectsWithTeachersAndClassesAsync();

      var subjectsDto = subjects.Select(s => new SubjectsWithTeacherAndClassesDto
      {
        Id = s.Id,
        Code = s.Code,
        SubjectName = s.SubjectName,
        ClassNumber = s.Class.ClassNumber,
        TeacherFullName = s.Teacher.TeacherName + " " + s.Teacher.TeacherSurname,
        TeacherId = s.TeacherId,
        ClassId = s.ClassId
      }).ToList();

      return Ok(subjectsDto);
    }
  }
}
