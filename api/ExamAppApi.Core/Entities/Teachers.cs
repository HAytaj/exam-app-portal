using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Core.Entities
{
  public class Teachers
  {
    public int Id { get; set; }
    public string? TeacherName { get; set; }

    public string? TeacherSurname { get; set; }
    public virtual ICollection<Subjects> Subjects { get; set; } = new List<Subjects>();
  }
}
