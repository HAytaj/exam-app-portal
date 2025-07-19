using ExamAppApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Core.Interfaces
{
  public interface IStudentsRepository : IRepository<Students>
  {
    Task<IEnumerable<Students>> GetStudentsWithClassesAsync();
  }
}
