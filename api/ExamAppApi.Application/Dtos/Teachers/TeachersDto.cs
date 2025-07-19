using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Application.Dtos.Teachers
{
  public class TeachersDto
  {
    public int Id { get; set; }
    public string? TeacherName { get; set; }
    public string? TeacherSurname { get; set; }
  }
}
