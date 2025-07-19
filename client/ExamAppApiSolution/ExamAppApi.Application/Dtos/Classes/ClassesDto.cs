using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Application.Dtos.Classes
{
  public class ClassesDto
  {
    public int Id { get; set; }
    public byte ClassNumber { get; set; }
  }
}
