using AutoMapper;
using ExamAppApi.Application.Dtos.Exams;
using ExamAppApi.Application.Dtos.Students;
using ExamAppApi.Application.Dtos.Subjects;
using ExamAppApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExamAppApi.Application.Mappings
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<SubjectUpsertDto, Subjects>();
      CreateMap<Subjects, SubjectUpsertDto>();

      CreateMap<StudentUpsertDto, Students>();
      CreateMap<Students, StudentUpsertDto>();

      CreateMap<ExamsDto, Exams>();
      CreateMap<Exams, ExamsDto>();
    }
  }
}
