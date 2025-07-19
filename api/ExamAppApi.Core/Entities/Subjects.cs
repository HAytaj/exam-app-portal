using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Core.Entities
{
  public class Subjects
  {
    public int Id { get; set; }
    [MaxLength(3)]
    public string? Code { get; set; }
    public string? SubjectName { get; set; }
    public int ClassId { get; set; }
    public int TeacherId { get; set; }

    public virtual Classes Class { get; set; } = null!;

    public virtual Teachers Teacher { get; set; } = null!;
    public virtual ICollection<Exams> Exams { get; set; } = new List<Exams>();
  }
}

