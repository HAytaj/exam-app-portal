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
  public class ExamsRepository : Repository<Exams>, IExamsRepository
  {
    private readonly AppDbContext _context;
    public ExamsRepository(AppDbContext context) : base(context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Exams>> GetExamsWithSubjectsAndStudentsAsync()
    {
       return await _context.Exams
        .Include(e => e.Student)
        .Include(e => e.Subject)
        .ToListAsync();
    }
  }
}
