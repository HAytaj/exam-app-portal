using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Application.Dtos.Exams
{
  public class ExamsDto
  {
    public int Id { get; set; }
    public string? Code { get; set; } = string.Empty;
    public int SubjectId { get; set; }
    public int StudentId { get; set; }
    public int StudentNumber { get; set; }
    public byte Score { get; set; }
    public DateOnly? ExamDate { get; set; }

  }
}
