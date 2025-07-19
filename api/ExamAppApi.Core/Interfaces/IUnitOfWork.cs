using ExamAppApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Core.Interfaces
{
  public interface IUnitOfWork
  {
    IRepository<Exams> Exams { get; }
    IRepository<Students> Students { get; }
    IRepository<Subjects> Subjects { get; }
    IRepository<Teachers> Teachers { get; }
    IRepository<Classes> Classes { get; }
    ISubjectsRepository SubjectsRepository { get; }
    IStudentsRepository StudentsRepository { get; }
    IExamsRepository ExamsRepository { get; }
    Task SaveChangesAsync();
  }
}
