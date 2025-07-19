using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Application.Dtos.Subjects
{
  public class SubjectsWithTeacherAndClassesDto: BaseSubjectDto
  {
    public byte? ClassNumber { get; set; } 
    public string? TeacherFullName { get; set; } = string.Empty;

  }
}
