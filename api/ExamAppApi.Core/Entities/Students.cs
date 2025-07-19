using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Core.Entities
{
  public class Students
  {
    public int Id { get; set; }
    public int StudentNumber { get; set; }
    public string? StudentName { get; set; }
    public string? StudentSurname { get; set; }
    public int ClassId { get; set; }
    public virtual Classes Class { get; set; } = null!;
    public virtual ICollection<Exams> Exams { get; set; } = new List<Exams>();
  }
}

