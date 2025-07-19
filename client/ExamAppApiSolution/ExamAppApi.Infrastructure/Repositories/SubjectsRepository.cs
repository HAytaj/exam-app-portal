using ExamAppApi.Core.Entities;
using ExamAppApi.Core.Interfaces;
using ExamAppApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExamAppApi.Infrastructure.Repositories
{
  public class SubjectsRepository : Repository<Subjects>, ISubjectsRepository
  {
    private readonly AppDbContext _context;
    public SubjectsRepository(AppDbContext context) : base(context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Subjects>> GetSubjectsWithTeachersAndClassesAsync()
    {
      return await _context.Subjects
    .Include(s => s.Class)
    .Include(s => s.Teacher)
    .ToListAsync();
    }
  }
}
