using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Application.Dtos.Subjects
{
  public class BaseSubjectDto
  {
    public int Id { get; set; }
    public string? Code { get; set; } = string.Empty;
    public string? SubjectName { get; set; } = string.Empty;
    public int ClassId { get; set; }
    public int TeacherId { get; set; }
  }
}
