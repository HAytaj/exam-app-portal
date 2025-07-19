using AutoMapper;
using ExamAppApi.Application.Dtos.Exams;
using ExamAppApi.Application.Dtos.Students;
using ExamAppApi.Core.Entities;
using ExamAppApi.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ExamsController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ExamsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var exams = await _unitOfWork.Exams.GetAllAsync();
      return Ok(exams);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var exam = await _unitOfWork.Exams.GetByIdAsync(id);
      if (exam == null) return NotFound();
      return Ok(exam);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ExamsDto examDto)
    {
      var exam = _mapper.Map<Exams>(examDto);
      await _unitOfWork.Exams.AddAsync(exam);
      await _unitOfWork.SaveChangesAsync();
      return CreatedAtAction(nameof(Get), new { id = exam.Id }, exam);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ExamsDto examDto)
    {
      if (id != examDto.Id) return BadRequest();

      var exam = _mapper.Map<Exams>(examDto);
      _unitOfWork.Exams.Update(exam);
      await _unitOfWork.SaveChangesAsync();
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var exam = await _unitOfWork.Exams.GetByIdAsync(id);
      if (exam == null) return NotFound();

      _unitOfWork.Exams.Delete(exam);
      await _unitOfWork.SaveChangesAsync();
      return NoContent();
    }

    [HttpGet("with-subjects-students")]
    public async Task<IActionResult> GetExamsWithSubjectsAndStudents()
    {
      var subjects = await _unitOfWork.ExamsRepository.GetExamsWithSubjectsAndStudentsAsync();

      var result = subjects.Select(s => new ExamsDto
      {
        Id = s.Id,
        Code = s.Subject.Code,
        StudentNumber = s.Student.StudentNumber,
        ExamDate = s.ExamDate,
        Score = s.Score,
        StudentId = s.StudentId,
        SubjectId = s.SubjectId
      }).ToList();

      return Ok(result);
    }
  }
}
