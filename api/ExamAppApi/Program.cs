using ExamAppApi.Application.Mappings;
using ExamAppApi.Core.Interfaces;
using ExamAppApi.Infrastructure.Data;
using ExamAppApi.Infrastructure.Repositories;
using ExamAppApi.Middlewares;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
var myAllowedSpecificOrigins = "_myAllowedSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(
  options =>
  {
    options.AddPolicy(
      name: myAllowedSpecificOrigins,
      builder =>
      {
        builder.WithOrigins("http://localhost", "https://localhost:4200", "http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowedToAllowWildcardSubdomains();
      });
  });



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseLazyLoadingProxies());

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowedSpecificOrigins);

app.UseMiddleware<ExamApiExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
