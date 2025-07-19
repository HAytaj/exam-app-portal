using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Core.Entities
{
  public class Classes
  {
    public int Id { get; set; }
    public byte ClassNumber { get; set; }
    public virtual ICollection<Subjects> Subjects { get; set; } = new List<Subjects>();
    public virtual ICollection<Students> Students { get; set; } = new List<Students>();

  }
}



