using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Application.Dtos.Students
{
  public class StudentsWithClassesDto: BaseStudentDto
  {
    public byte ClassNumber { get; set; }
  }
}
