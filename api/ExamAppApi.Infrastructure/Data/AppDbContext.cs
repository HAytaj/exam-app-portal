using ExamAppApi.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace ExamAppApi.Infrastructure.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Exams> Exams => Set<Exams>();
    public DbSet<Subjects> Subjects => Set<Subjects>();
    public DbSet<Students> Students => Set<Students>();

    public DbSet<Teachers> Teachers => Set<Teachers>();
    public DbSet<Classes> Classes => Set<Classes>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Subjects>(entity =>
      {
        entity.Property(s => s.SubjectName)
              .HasColumnName("subject_name");

        entity.Property(s => s.ClassId)
              .HasColumnName("class_id");

        entity.Property(s => s.TeacherId)
              .HasColumnName("teacher_id");

        entity.HasOne(s => s.Class)
              .WithMany(c => c.Subjects)
              .HasForeignKey(s => s.ClassId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(s => s.Teacher)
              .WithMany(t => t.Subjects)
              .HasForeignKey(s => s.TeacherId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Students>(entity =>
      {
        // Column mapping
        entity.Property(s => s.StudentNumber)
              .HasColumnName("student_number");

        entity.Property(s => s.StudentName)
              .HasColumnName("student_name");

        entity.Property(s => s.StudentSurname)
              .HasColumnName("student_surname");

        entity.Property(s => s.ClassId)
              .HasColumnName("class_id");

        // Relationships
        entity.HasOne(s => s.Class)
              .WithMany(c => c.Students)
              .HasForeignKey(s => s.ClassId);

        entity.HasMany(s => s.Exams)
              .WithOne(e => e.Student)
              .HasForeignKey(e => e.StudentId);
      });


      modelBuilder.Entity<Subjects>(entity =>
      {
        entity.Property(s => s.Code)
              .HasMaxLength(3);

        entity.Property(s => s.SubjectName)
              .HasColumnName("subject_name");

        entity.Property(s => s.ClassId)
              .HasColumnName("class_id");

        entity.Property(s => s.TeacherId)
              .HasColumnName("teacher_id");

        entity.HasOne(s => s.Class)
              .WithMany(c => c.Subjects)
              .HasForeignKey(s => s.ClassId);

        entity.HasOne(s => s.Teacher)
              .WithMany(t => t.Subjects)
              .HasForeignKey(s => s.TeacherId);

        entity.HasMany(s => s.Exams)
              .WithOne(e => e.Subject)
              .HasForeignKey(e => e.SubjectId);
      });

      modelBuilder.Entity<Exams>(entity =>
      {
        entity.Property(e => e.SubjectId)
              .HasColumnName("subject_id");

        entity.Property(e => e.StudentId)
              .HasColumnName("student_id");

        entity.Property(e => e.ExamDate)
              .HasColumnName("exam_date");

        entity.Property(e => e.Score);

        // Relations
        entity.HasOne(e => e.Student)
              .WithMany(s => s.Exams)
              .HasForeignKey(e => e.StudentId);

        entity.HasOne(e => e.Subject)
              .WithMany(s => s.Exams)
              .HasForeignKey(e => e.SubjectId);
      });

      modelBuilder.Entity<Teachers>(entity =>
      {
        entity.Property(t => t.TeacherName)
              .HasColumnName("teacher_name");

        entity.Property(t => t.TeacherSurname)
              .HasColumnName("teacher_surname");

        entity.HasMany(t => t.Subjects)
              .WithOne(s => s.Teacher)
              .HasForeignKey(s => s.TeacherId);
      });

      modelBuilder.Entity<Classes>(entity =>
      {
        entity.Property(c => c.ClassNumber)
              .HasColumnName("class_number");

        entity.HasMany(c => c.Subjects)
              .WithOne(s => s.Class)
              .HasForeignKey(s => s.ClassId);

        entity.HasMany(c => c.Students)
              .WithOne(s => s.Class)
              .HasForeignKey(s => s.ClassId);
      });



    }
  }
}
