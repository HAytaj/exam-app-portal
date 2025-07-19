using ExamAppApi.Core.Entities;
using ExamAppApi.Core.Interfaces;
using ExamAppApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAppApi.Infrastructure.Repositories
{
  public class StudentsRepository: Repository<Students>, IStudentsRepository
  {
    private readonly AppDbContext _context;
    public StudentsRepository(AppDbContext context) : base(context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Students>> GetStudentsWithClassesAsync()
    {
      return await _context.Students.Include(s => s.Class).ToListAsync();
    }
  }
}
