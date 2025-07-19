using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Application.Dtos.Students
{
  public class BaseStudentDto
  {
    public int Id { get; set; }
    public int StudentNumber { get; set; }
    public string? StudentName { get; set; }
    public string? StudentSurname { get; set; }
    public int ClassId { get; set; }
  }
}
