using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_University.Data;

public partial class ApiUniversityContext : DbContext
{
    public ApiUniversityContext()
    {
    }

    public ApiUniversityContext(DbContextOptions<ApiUniversityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<ProfessorClass> ProfessorClasses { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentRegistration> StudentRegistrations { get; set; }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-IRB0ADSL\\MSSQLSERVER01; Database=API_University; Trusted_Connection=True; Encrypt=False;");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Classroom>(entity =>
				{
					  entity.Property(e => e.Code)
								  .HasMaxLength(50)
								  .IsUnicode(false);
        });

        modelBuilder.Entity<Professor>(entity =>
        {
					  entity.Property(e => e.CelNumber)
	              .HasMaxLength(15)
	              .HasColumnName("Cel_number");
					  entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProfessorClass>(entity =>
        {
            entity.HasOne(d => d.Class).WithMany(p => p.ProfessorClasses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfessorClasses_Classes");

            entity.HasOne(d => d.Professor).WithMany(p => p.ProfessorClasses)
                .HasForeignKey(d => d.ProfessorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfessorClasses_Professors");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasOne(d => d.Class).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedules_Classes");

            entity.HasOne(d => d.Classroom).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedules_Classrooms");
            /*
					  entity.HasOne(d => d.Professor).WithMany(p => p.Schedules)
	              .HasForeignKey(d => d.ProfessorId)
	              .OnDelete(DeleteBehavior.ClientSetNull)
	              .HasConstraintName("FK_Schedules_Professors");

					  entity.HasOne(d => d.Student).WithMany(p => p.Schedules)
						    .HasForeignKey(d => d.StudentId)
						    .OnDelete(DeleteBehavior.ClientSetNull)
						    .HasConstraintName("FK_Schedules_Students");
            */
				});

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.CelNumber)
                .HasMaxLength(15)
                .HasColumnName("Cel_number");
            entity.Property(e => e.Code).IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StudentRegistration>(entity =>
        {
            entity.ToTable("StudentRegistration");

            entity.HasOne(d => d.Class).WithMany(p => p.StudentRegistrations)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentRegistration_Classes");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentRegistrations)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentRegistration_Students");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
