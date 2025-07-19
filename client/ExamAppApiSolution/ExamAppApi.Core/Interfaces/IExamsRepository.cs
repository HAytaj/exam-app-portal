using ExamAppApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Core.Interfaces
{
  public interface IExamsRepository : IRepository<Exams>
  {
    Task<IEnumerable<Exams>> GetExamsWithSubjectsAndStudentsAsync();
  }
}
