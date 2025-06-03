using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlannerServer.Models;

namespace PlannerServer.Data
{
    public class PlannerServerContext : DbContext
    {
        public PlannerServerContext (DbContextOptions<PlannerServerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactInformation> ContactInformations { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<EnrollmentKind> EnrollmentKinds { get; set; }
        public virtual DbSet<GraduationKind> GraduationKinds { get; set; }
        public virtual DbSet<GrantInformation> GrantInformations { get; set; }
        public virtual DbSet<GrantKind> GrantKinds { get; set; }
        public virtual DbSet<Municipality> Municipalities { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentEnrollment> StudentEnrollments { get; set; }
        public virtual DbSet<StudentGraduation> StudentGraduations { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContactInformation>(entity =>
            {
                entity.ToTable("ContactInformations");

                // Primary Key
                entity.HasKey(e => e.ContactInformationId);

                entity.Property(e => e.ContactInformationId)
                      .HasColumnName("ContactInformationId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.PrivatePhone)
                      .HasColumnName("PrivatePhone")
                      .HasMaxLength(20)
                      .IsUnicode(true);

                entity.Property(e => e.PrivateEmail)
                      .HasColumnName("PrivateEmail")
                      .HasMaxLength(100)
                      .IsUnicode(true);

                entity.Property(e => e.ParentNames)
                      .HasColumnName("ParentNames")
                      .HasMaxLength(200)
                      .IsUnicode(true);

                entity.Property(e => e.ParentPhones)
                      .HasColumnName("ParentPhones")
                      .HasMaxLength(40)
                      .IsUnicode(true);

                entity.Property(e => e.ParentEmails)
                      .HasColumnName("ParentEmails")
                      .HasMaxLength(200)
                      .IsUnicode(true);

                entity.Property(e => e.CurrentAddress)
                      .HasColumnName("CurrentAddress")
                      .HasMaxLength(100)
                      .IsUnicode(true);

                // Relationships
                entity.HasOne(e => e.Student)
                      .WithOne(s => s.ContactInformation)
                      .HasForeignKey<ContactInformation>(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Departments");

                // Primary Key
                entity.HasKey(e => e.DepartmentId);

                entity.Property(e => e.DepartmentId)
                      .HasColumnName("DepartmentId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasMaxLength(50)
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.Street)
                      .HasColumnName("Street")
                      .HasMaxLength(255)
                      .IsUnicode(true);

                entity.Property(e => e.Zip)
                      .HasColumnName("Zip")
                      .HasMaxLength(4)
                      .IsUnicode(true);

                entity.Property(e => e.City)
                      .HasColumnName("City")
                      .HasMaxLength(255)
                      .IsUnicode(true);

                // Relationships
                entity.HasMany(e => e.Students)
                      .WithOne(s => s.Department)
                      .HasForeignKey(s => s.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Users)
                      .WithOne(u => u.Department)
                      .HasForeignKey(u => u.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("Educations");

                // Primary Key
                entity.HasKey(e => e.EducationId);

                entity.Property(e => e.EducationId)
                      .HasColumnName("Id")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasMaxLength(5)
                      .IsRequired()
                      .IsUnicode(true);

                // Relationships
                entity.HasMany(e => e.StudentEnrollments)
                      .WithOne(se => se.Education)
                      .HasForeignKey(se => se.EducationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EnrollmentKind>(entity =>
            {
                entity.ToTable("EnrollmentKinds");

                // Primary Key
                entity.HasKey(e => e.EnrollmentKindId);

                entity.Property(e => e.EnrollmentKindId)
                      .HasColumnName("Id")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasMaxLength(7)
                      .IsRequired()
                      .IsUnicode(true);

                // Relationships
                entity.HasMany(e => e.StudentEnrollments)
                      .WithOne(se => se.EnrollmentKind)
                      .HasForeignKey(se => se.EnrollmentKindId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GraduationKind>(entity =>
            {
                entity.ToTable("GraduationKinds");

                // Primary Key
                entity.HasKey(e => e.GraduationKindId);

                entity.Property(e => e.GraduationKindId)
                      .HasColumnName("GraduationKindId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasMaxLength(17)
                      .IsRequired()
                      .IsUnicode(true);

                // Relationships
                entity.HasMany(e => e.StudentGraduations)
                      .WithOne(sg => sg.GraduationKind)
                      .HasForeignKey(sg => sg.GraduationKindId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GrantInformation>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.GrantInformationId);

                entity.Property(e => e.GrantInformationId)
                      .HasColumnName("GrantInformationId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.GrantKindId)
                      .HasColumnName("GrantKindId")
                      .IsRequired();

                entity.Property(e => e.StudentId)
                      .HasColumnName("StudentId")
                      .IsRequired();

                entity.Property(e => e.CounselorName)
                      .HasColumnName("CounselorName")
                      .HasMaxLength(100)
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.CounselorPhone)
                      .HasColumnName("CounselorPhone")
                      .HasMaxLength(20)
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.CounselorEmail)
                      .HasColumnName("CounselorEmail")
                      .HasMaxLength(100)
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.MunicipalityId)
                      .HasColumnName("MunicipalityId")
                      .IsRequired();

                // Relationships
                entity.HasOne(e => e.GrantKind)
                      .WithMany(gk => gk.GrantInformations)
                      .HasForeignKey(e => e.GrantKindId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Municipality)
                      .WithMany(m => m.GrantInformations)
                      .HasForeignKey(e => e.MunicipalityId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Student)
                      .WithMany(s => s.GrantInformations)
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GrantKind>(entity =>
            {
                entity.ToTable("GrantKinds");

                // Primary Key
                entity.HasKey(e => e.GrantKindId);

                entity.Property(e => e.GrantKindId)
                      .HasColumnName("ModulePeriodId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasMaxLength(5)
                      .IsRequired()
                      .IsUnicode(true);

                // Relationships
                entity.HasMany(entity => entity.GrantInformations)
                      .WithOne(gi => gi.GrantKind)
                      .HasForeignKey(gi => gi.GrantKindId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Municipality>(entity =>
            {
                entity.ToTable("Municipalities");

                // Primary Key
                entity.HasKey(e => e.MunicipalityId)
                      .HasName("PK_Municipalities");

                entity.Property(e => e.MunicipalityId)
                      .HasColumnName("MunicipalityId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasMaxLength(50)
                      .IsRequired()
                      .IsUnicode(true);

                // Relationships
                entity.HasMany(e => e.Students)
                      .WithOne(s => s.Municipality)
                      .HasForeignKey(s => s.MunicipalityId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");

                // Primary Key
                entity.HasKey(e => e.StudentId)
                      .HasName("PK__Students__3214EC072F8FC7F4");

                entity.Property(e => e.StudentId)
                      .HasColumnName("StudentId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .IsRequired()
                      .HasMaxLength(255)
                      .IsUnicode(true);

                entity.Property(e => e.Unilogin)
                    .HasColumnName("Unilogin")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentId")
                    .IsRequired();

                entity.Property(e => e.StartDate)
                      .HasColumnName("StartDate")
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.GraduationDate)
                      .HasColumnName("GraduationDate")
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.MunicipalityId)
                      .HasColumnName("MunicipalityId")
                      .IsRequired();

                entity.Property(e => e.Birthdate)
                      .HasColumnName("Birthdate")
                      .HasColumnType("date");

                entity.Property(e => e.SerialNumber)
                      .HasColumnName("SerialNumber")
                      .HasColumnType("char")
                      .HasMaxLength(4)
                      .IsFixedLength();

                entity.Property(e => e.IdFromUms)
                      .HasColumnName("IdFromUms");

                entity.Property(e => e.UmsActivity)
                      .HasColumnName("UmsActivity")
                      .HasMaxLength(256)
                      .IsUnicode(true);

                // Relationships
                entity.HasOne(e => e.Department)
                      .WithMany(d => d.Students)
                      .HasForeignKey(e => e.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Municipality)
                      .WithMany(m => m.Students)
                      .HasForeignKey(e => e.MunicipalityId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StudentEnrollment>(entity =>
            {
                entity.ToTable("StudentEnrollments");

                entity.HasKey(e => e.StudentEnrollmentId);

                // Primary Key
                entity.Property(e => e.StudentEnrollmentId)
                      .HasColumnName("StudentEnrollmentId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.StudentId)
                      .HasColumnName("StudentId")
                      .IsRequired();

                entity.Property(e => e.EnrollmentKindId)
                      .HasColumnName("EnrollmentKindId")
                      .IsRequired();

                entity.Property(e => e.EducationId)
                      .HasColumnName("EducationId")
                      .IsRequired();

                entity.Property(e => e.StartDate)
                      .HasColumnName("StartDate")
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.EndDate)
                      .HasColumnName("EndDate")
                      .HasColumnType("date")
                      .IsRequired();

                // Relationships
                entity.HasOne(e => e.Student)
                      .WithMany(s => s.StudentEnrollments)
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.EnrollmentKind)
                      .WithMany(e => e.StudentEnrollments)
                      .HasForeignKey(e => e.EnrollmentKindId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Education)
                      .WithMany(e => e.StudentEnrollments)
                      .HasForeignKey(e => e.EducationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StudentGraduation>(entity =>
            {
                entity.ToTable("StudentGraduations");

                // Primary Key
                entity.HasKey(e => e.StudentGraduationId);

                entity.Property(e => e.StudentGraduationId)
                      .HasColumnName("StudentGraduationId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.Note)
                      .HasColumnName("Note")
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.GraduationKindId)
                      .HasColumnName("GraduationKindId")
                      .IsRequired();

                entity.Property(e => e.StudentId)
                      .HasColumnName("StudentId")
                      .IsRequired();

                // Relationships
                entity.HasOne(e => e.GraduationKind)
                      .WithMany(sk => sk.StudentGraduations)
                      .HasForeignKey(sk => sk.GraduationKindId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Student)
                      .WithMany(s => s.StudentGraduations)
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Teams");

                // Primary Key
                entity.HasKey(e => e.TeamId)
                      .HasName("PK__Teams__123AE799865A943D");

                entity.Property(e => e.TeamId)
                      .HasColumnName("TeamId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasMaxLength(50)
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.Module)
                      .HasColumnName("Module")
                      .HasMaxLength(20)
                      .IsRequired()
                      .IsUnicode(true);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                // Primary Key
                entity.HasKey(e => e.UserId)
                      .HasName("PK__Users__1788CC4CEAFC2AF2");

                entity.Property(e => e.UserId)
                      .HasColumnName("UserId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.UserName)
                      .HasColumnName("UserName")
                      .HasMaxLength(50)
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.Initials)
                      .HasColumnName("Initials")
                      .HasMaxLength(5)
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.IsActive)
                      .HasColumnName("IsActive")
                      .IsRequired();

                entity.Property(e => e.Level)
                      .HasColumnName("Level")
                      .IsRequired();

                entity.Property(e => e.DepartmentId)
                      .HasColumnName("DepartmentId")
                      .IsRequired();

                // Relationships
                entity.HasOne(e => e.Department)
                      .WithMany(d => d.Users)
                      .HasForeignKey(e => e.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Teams)
                      .WithMany(t => t.Users)
                      .UsingEntity(j => j.ToTable("UserTeams"));
            });
        }
    }
}
