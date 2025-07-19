using ExamAppApi.Core.Entities;
using ExamAppApi.Core.Interfaces;
using ExamAppApi.Infrastructure.Data;
using ExamAppApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Infrastructure.Repositories
{

  public class UnitOfWork : IUnitOfWork
  {
    private readonly AppDbContext _context;
    public IRepository<Exams> Exams { get; }
    public IRepository<Students> Students { get; }
    public IRepository<Subjects> Subjects { get; }
    public IRepository<Teachers> Teachers { get; }
    public IRepository<Classes> Classes { get; }
    public ISubjectsRepository SubjectsRepository { get; }
    public IStudentsRepository StudentsRepository { get; }

    public IExamsRepository ExamsRepository { get; }

    public UnitOfWork(AppDbContext context)
    {
      _context = context;
      Exams = new Repository<Exams>(_context);
      Students = new Repository<Students>(_context);
      Subjects = new Repository<Subjects>(_context);
      Classes = new Repository<Classes>(_context);
      Teachers = new Repository<Teachers>(_context);
      SubjectsRepository = new SubjectsRepository(_context);
      StudentsRepository = new StudentsRepository(_context);
      ExamsRepository = new ExamsRepository(_context);
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
  }
}
