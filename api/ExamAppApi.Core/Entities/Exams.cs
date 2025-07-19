using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Core.Entities
{
  public class Exams
  {
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public int StudentId { get; set; }
    public DateOnly ExamDate { get; set; }
    public byte Score { get; set; }

    public virtual Students Student { get; set; } = null!;
    public virtual Subjects Subject { get; set; } = null!;
  }
}
