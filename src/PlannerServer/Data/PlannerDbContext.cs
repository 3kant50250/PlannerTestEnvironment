using Microsoft.EntityFrameworkCore;
using PlannerServer.Model;
using PlannerServer.Models;

namespace PlannerServer.Data
{
    public class PlannerDbContext : DbContext
    {
        public PlannerDbContext(DbContextOptions<PlannerDbContext> options)
        : base(options) { }

        public virtual DbSet<ContactInformation> ContactInformations { get; set; }
        public virtual DbSet<Date> Dates { get; set; }
        public virtual DbSet<DayPlan> DayPlans { get; set; }
        public virtual DbSet<DayPlanRow> DayPlansRows { get; set; }
        public virtual DbSet<DaysWeekPlan> DaysWeekPlans { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<EnrollmentKind> EnrollmentKinds { get; set; }
        public virtual DbSet<Evaluation> Evaluations { get; set; }
        public virtual DbSet<FormativeEvaluation> FormativeEvaluations { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<GraduationKind> GraduationKinds { get; set; }
        public virtual DbSet<GrantInformation> GrantInformations { get; set; }
        public virtual DbSet<GrantKind> GrantKinds { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Municipality> Municipalities { get; set; }
        public virtual DbSet<PlannedActivity> PlannedActivities { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentEnrollment> StudentEnrollments { get; set; }
        public virtual DbSet<StudentGraduation> StudentGraduations { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SummativeEvaluation> SummativeEvaluations { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamStudent> TeamStudents { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WeekPlan> WeekPlans { get; set; }

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

            modelBuilder.Entity<Date>(entity =>
            {
                entity.ToTable("Dates");

                // Primary Key
                entity.HasKey(e => e.DateId)
                      .HasName("PK__Dates__3214EC07");

                entity.Property(e => e.DateId)
                      .HasColumnName("DateId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.Date_)
                      .HasColumnName("Date")
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.IsSchoolDay)
                      .HasColumnName("IsSchoolDay")
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasColumnName("Description")
                      .HasColumnType("text")
                      .IsRequired();

                // Relationships
                entity.HasMany(e => e.StarDateModules)
                      .WithOne(m => m.StartDate)
                      .HasForeignKey(m => m.StartDateId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.EndDateModules)
                      .WithOne(m => m.EndDate)
                      .HasForeignKey(m => m.EndDateId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DayPlan>(entity =>
            {
                entity.ToTable("DayPlans");

                // Primary Key
                entity.HasKey(e => e.DayPlanId)
                      .HasName("PK__DayPlans__182E9AC0DD49542D");

                entity.Property(e => e.DayPlanId)
                      .HasColumnName("DayPlanId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.ModuleName)
                      .HasColumnName("ModuleName")
                      .HasMaxLength(9)
                      .IsUnicode(true);

                entity.Property(e => e.Date)
                      .HasColumnName("Date")
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.SubjectId)
                      .HasColumnName("SubjectId");

                // Relationships
                entity.HasMany(e => e.DayPlanRows)
                      .WithOne()
                      .HasForeignKey("DayPlanId")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Subject)
                      .WithMany(s => s.DayPlans)
                      .HasForeignKey(e => e.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Students)
                      .WithMany(s => s.DayPlans)
                      .UsingEntity(j => j.ToTable("StudentsDayPlans"));
            });

            modelBuilder.Entity<DayPlanRow>(entity =>
            {
                entity.ToTable("DayPlanRows");

                // Primary Key
                entity.HasKey(e => e.DayPlanRowId)
                      .HasName("PK__DayPlanR__3DD6B5F17844931B");

                entity.Property(e => e.DayPlanRowId)
                      .HasColumnName("DayPlanRowId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.Theme)
                      .HasColumnName("Theme")
                      .IsUnicode(true);

                entity.Property(e => e.LearningGoal)
                      .HasColumnName("LearningGoal")
                      .IsUnicode(true);

                entity.Property(e => e.Content)
                      .HasColumnName("Content")
                      .IsUnicode(true);

                entity.Property(e => e.Materials)
                      .HasColumnName("Materials")
                      .IsUnicode(true);

                entity.Property(e => e.DayPlanId)
                      .HasColumnName("DayPlanId")
                      .IsRequired();

                // Relationships
                entity.HasOne(e => e.DayPlan)
                      .WithMany(d => d.DayPlanRows)
                      .HasForeignKey(e => e.DayPlanId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DaysWeekPlan>(entity =>
            {
                entity.ToTable("DaysWeekPlans");

                // Primary Key
                entity.HasKey(e => e.DaysWeekPlanId)
                      .HasName("PK__DaysWeek__025B56F565F5075C");

                entity.Property(e => e.DaysWeekPlanId)
                      .HasColumnName("DaysWeekPlanId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.DayOfWeek)
                      .HasColumnName("DayOfWeek")
                      .HasMaxLength(10)
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.Purpose)
                      .HasColumnName("Purpose")
                      .IsUnicode(true);

                entity.Property(e => e.LearningGoal)
                      .HasColumnName("LearningGoal")
                      .IsUnicode(true);

                entity.Property(e => e.Content)
                      .HasColumnName("Content")
                      .IsUnicode(true);

                entity.Property(e => e.Materials)
                      .HasColumnName("Materials")
                      .IsUnicode(true);

                entity.Property(e => e.WeekPlanId)
                      .HasColumnName("WeekPlanId")
                      .IsRequired();

                // Relationships
                entity.HasOne(e => e.WeekPlan)
                      .WithMany(wp => wp.DaysWeekPlans)
                      .HasForeignKey(e => e.WeekPlanId)
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

            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.ToTable("Evaluations");

                // Primary Key
                entity.HasKey(e => e.EvalId)
                      .HasName("PK__Evaluati__DCC6AA48DE6EEFF2");

                entity.Property(e => e.EvalId)
                      .HasColumnName("EvalId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.SubjectEval)
                      .HasColumnName("SubjectEval")
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.PersonalEval)
                      .HasColumnName("PersonalEval")
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.WorkEval)
                      .HasColumnName("WorkEval")
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.OtherEval)
                      .HasColumnName("OtherEval")
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.SummativePerson)
                      .HasColumnName("SummativePerson");

                entity.Property(e => e.SumId)
                      .HasColumnName("SumId")
                      .IsRequired();

                // Relationships
                entity.HasOne(e => e.SummativeEvaluation)
                      .WithMany(se => se.Evaluations)
                      .HasForeignKey(e => e.SumId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FormativeEvaluation>(entity =>
            {
                entity.ToTable("FormativeEvaluations");

                // Primary Key
                entity.HasKey(e => e.FormId)
                      .HasName("PK__Formativ__51BCB72BE37A86A0");

                entity.Property(e => e.FormId)
                      .HasColumnName("FormId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.DateOfForm)
                      .HasColumnName("DateOfForm")
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.Module)
                      .HasColumnName("Module")
                      .HasMaxLength(20)
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.Eval)
                      .HasColumnName("Eval")
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.Wishes)
                      .HasColumnName("Wishes")
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.UserId)
                      .HasColumnName("UserId")
                      .IsRequired();

                entity.Property(e => e.StudentId)
                      .HasColumnName("StudentId")
                      .IsRequired();

                entity.Property(e => e.FirstPrioritySubjectId)
                      .HasColumnName("FirstPrioritySubjectId");

                entity.Property(e => e.SecondPrioritySubjectId)
                      .HasColumnName("SecondPrioritySubjectId");

                entity.Property(e => e.ThirdPrioritySubjectId)
                      .HasColumnName("ThirdPrioritySubjectId");

                // Relationships
                entity.HasOne(e => e.Student)
                      .WithMany(s => s.FormativeEvaluations)
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.FormativeEvaluations)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.FirstPrioritySubject)
                      .WithMany()
                      .HasForeignKey(e => e.FirstPrioritySubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SecondPrioritySubject)
                      .WithMany()
                      .HasForeignKey(e => e.SecondPrioritySubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ThirdPrioritySubject)
                      .WithMany()
                      .HasForeignKey(e => e.ThirdPrioritySubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Goals)
                      .WithOne()
                      .HasForeignKey("FormId")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.ToTable("Goals");

                // Primary Key
                entity.HasKey(e => e.GoalId)
                      .HasName("PK__Goals__7E225EB11BC7AEB4");

                entity.Property(e => e.GoalId)
                      .HasColumnName("GoalId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.GoalDefinition)
                      .HasColumnName("GoalDefinition")
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.LearningObjective)
                      .HasColumnName("LearningObjective")
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.GoalContent)
                      .HasColumnName("GoalContent")
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.Criteria)
                      .HasColumnName("Criteria")
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.TypeOfGoals)
                      .HasColumnName("TypeOfGoals")
                      .IsRequired();

                entity.Property(e => e.FormId)
                      .HasColumnName("FormId")
                      .IsRequired();

                // Relationships
                entity.HasOne(e => e.FormativeEvaluation)
                      .WithMany(f => f.Goals)
                      .HasForeignKey(e => e.FormId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grades");

                // Primary Key
                entity.HasKey(e => e.GradeId);

                entity.Property(e => e.GradeId)
                      .HasColumnName("GradeId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.GradeValue)
                      .HasColumnName("GradeValue")
                      .HasMaxLength(50)
                      .IsRequired()
                      .IsUnicode(true);

                // Relationships
                entity.HasMany(e => e.TeamStudents)
                      .WithOne(ts => ts.Grade)
                      .HasForeignKey(ts => ts.GradeId)
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

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("ModulePeriods");

                // Primary Key
                entity.HasKey(e => e.ModuleId);

                entity.Property(e => e.ModuleId)
                      .HasColumnName("ModulePeriodId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.StartDateId)
                      .HasColumnName("StartDateId")
                      .IsRequired();

                entity.Property(e => e.EndDateId)
                      .HasColumnName("EndDateId")
                      .IsRequired();

                entity.Property(e => e.Year)
                      .HasColumnName("Year")
                      .IsRequired();

                entity.Property(e => e.Semester)
                      .HasColumnName("Semester")
                      .IsRequired();

                entity.Property(e => e.ModuleText)
                      .HasColumnName("Module")
                      .HasColumnType("nchar")
                      .HasMaxLength(2)
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.Text)
                      .HasColumnName("Text")
                      .HasColumnType("nchar")
                      .HasMaxLength(7)
                      .IsUnicode(true);

                // Relationships
                entity.HasOne(e => e.StartDate)
                      .WithMany(d => d.StarDateModules)
                      .HasForeignKey(e => e.StartDateId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.EndDate)
                      .WithMany(d => d.EndDateModules)
                      .HasForeignKey(e => e.EndDateId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.PlannedActivities)
                      .WithOne(pa => pa.Module)
                      .HasForeignKey(pa => pa.ModuleId)
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

            modelBuilder.Entity<PlannedActivity>(entity =>
            {
                entity.ToTable("PlannedActivities");

                // Primary Key
                entity.HasKey(e => new { e.StudentId, e.SubjectId, e.ModuleId })
                      .HasName("PK_PlannedActivities_Students_Subjects_ModulePeriods");

                entity.Property(e => e.StudentId)
                      .HasColumnName("StudentId")
                      .IsRequired();

                entity.Property(e => e.SubjectId)
                      .HasColumnName("SubjectId")
                      .IsRequired();

                entity.Property(e => e.ModuleId)
                      .HasColumnName("ModulePeriodId")
                      .IsRequired();

                // Properties
                entity.Property(e => e.BL)
                      .HasColumnName("BL")
                      .HasColumnType("bit")
                      .IsRequired();

                // Relationships
                entity.HasOne(e => e.Module)
                      .WithMany(m => m.PlannedActivities)
                      .HasForeignKey(e => e.ModuleId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Student)
                      .WithMany(s => s.PlannedActivities)
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Subject)
                      .WithMany()
                      .HasForeignKey(e => e.SubjectId)
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

                entity.HasMany(e => e.FormativeEvaluations)
                      .WithOne(fe => fe.Student)
                      .HasForeignKey(fe => fe.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.SummativeEvaluations)
                      .WithOne(se => se.Student)
                      .HasForeignKey(se => se.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.TeamStudents)
                      .WithOne(ts => ts.Student)
                      .HasForeignKey(ts => ts.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.DayPlans)
                      .WithMany(dp => dp.Students)
                      .UsingEntity(j => j.ToTable("StudentsDayPlans"));

                entity.HasMany(e => e.WeekPlans)
                      .WithMany(wp => wp.Students)
                      .UsingEntity(j => j.ToTable("StudentsWeekPlans"));

                entity.HasMany(e => e.PlannedActivities)
                      .WithOne(pa => pa.Student)
                      .HasForeignKey(pa => pa.StudentId)
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

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subjects");

                // Primary Key
                entity.HasKey(e => e.SubjectId)
                      .HasName("PK__Subjects__3214EC0723D63841");

                entity.Property(e => e.SubjectId)
                      .HasColumnName("SubjectId")
                      .ValueGeneratedNever();

                // Properties
                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasMaxLength(50)
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.StandardSubjectDescription)
                      .HasColumnName("StandardSubjectDescription")
                      .IsUnicode(true);

                // Relationships
                entity.HasMany(e => e.DayPlans)
                      .WithOne(dp => dp.Subject)
                      .HasForeignKey(dp => dp.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Teams)
                      .WithOne(t => t.Subject)
                      .HasForeignKey(t => t.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.WeekPlans)
                      .WithOne(wp => wp.Subject)
                      .HasForeignKey(wp => wp.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.FirstPrioritySubjectFormativeEvaluations)
                      .WithOne(fe => fe.FirstPrioritySubject)
                      .HasForeignKey(fe => fe.FirstPrioritySubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.SecondPrioritySubjectFormativeEvaluations)
                      .WithOne(fe => fe.SecondPrioritySubject)
                      .HasForeignKey(fe => fe.SecondPrioritySubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.ThirdPrioritySubjectFormativeEvaluations)
                      .WithOne(fe => fe.ThirdPrioritySubject)
                      .HasForeignKey(fe => fe.ThirdPrioritySubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.PlannedActivities)
                      .WithOne(pa => pa.Subject)
                      .HasForeignKey(pa => pa.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SummativeEvaluation>(entity =>
            {
                entity.ToTable("SummativeEvaluations");

                // Primary Key
                entity.HasKey(e => e.SumId);

                entity.Property(e => e.SumId)
                      .HasColumnName("SumId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.DateOfSum)
                      .HasColumnName("DateOfSum")
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(e => e.Module)
                      .HasColumnName("Module")
                      .HasMaxLength(20)
                      .IsRequired()
                      .IsUnicode(true);

                entity.Property(e => e.UserId)
                      .HasColumnName("UserId")
                      .IsRequired();

                entity.Property(e => e.StudentId)
                      .HasColumnName("StudentId")
                      .IsRequired();

                // Relationships
                entity.HasOne(e => e.Student)
                      .WithMany(s => s.SummativeEvaluations)
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.SummativeEvaluations)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Evaluations)
                      .WithOne(ev => ev.SummativeEvaluation)
                      .HasForeignKey(ev => ev.SumId)
                      .OnDelete(DeleteBehavior.Cascade);
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

                entity.Property(e => e.SubjectId)
                      .HasColumnName("SubjectId")
                      .IsRequired();

                entity.Property(e => e.UserId)
                      .HasColumnName("UserId")
                      .IsRequired();

                // Relationships
                entity.HasOne(e => e.Subject)
                      .WithMany(s => s.Teams)
                      .HasForeignKey(e => e.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Teacher)
                      .WithMany(u => u.Teams)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.WeekPlanDrafts)
                      .WithOne()
                      .HasForeignKey("DraftTeamId")
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.TeamStudents)
                      .WithOne(ts => ts.Team)
                      .HasForeignKey(ts => ts.TeamId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.FavoriteUsers)
                      .WithMany(u => u.FavoriteTeams)
                      .UsingEntity(j => j.ToTable("TeamUsers"));
            });

            modelBuilder.Entity<TeamStudent>(entity =>
            {
                entity.ToTable("TeamStudents");

                // Primary Key
                entity.HasKey(e => new { e.TeamId, e.StudentId })
                      .HasName("PK_TeamStudents");

                entity.Property(e => e.TeamId)
                      .HasColumnName("TeamId")
                      .IsRequired();

                entity.Property(e => e.StudentId)
                      .HasColumnName("StudentId")
                      .IsRequired();

                // Properties
                entity.Property(e => e.ExternalCensor)
                      .HasColumnName("ExternalCensor")
                      .IsUnicode(true);

                entity.Property(e => e.SubjectDescription)
                      .HasColumnName("SubjectDescription")
                      .IsUnicode(true);

                entity.Property(e => e.GradeId)
                      .HasColumnName("GradeId");

                entity.Property(e => e.InternalCensorId)
                      .HasColumnName("InternalCensorId");

                // Relationships
                entity.HasOne(e => e.Team)
                      .WithMany(t => t.TeamStudents)
                      .HasForeignKey(e => e.TeamId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Student)
                      .WithMany(s => s.TeamStudents)
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Grade)
                      .WithMany(g => g.TeamStudents)
                      .HasForeignKey(e => e.GradeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.InternalCensor)
                      .WithMany()
                      .HasForeignKey(e => e.InternalCensorId)
                      .OnDelete(DeleteBehavior.Restrict);
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

                entity.HasMany(e => e.FormativeEvaluations)
                      .WithOne(fe => fe.User)
                      .HasForeignKey(fe => fe.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.SummativeEvaluations)
                      .WithOne(se => se.User)
                      .HasForeignKey(se => se.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Teams)
                      .WithOne(t => t.Teacher)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.TeamStudents)
                      .WithOne(ts => ts.InternalCensor)
                      .HasForeignKey(ts => ts.InternalCensorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.FavoriteTeams)
                      .WithMany(t => t.FavoriteUsers)
                      .UsingEntity(j => j.ToTable("TeamUsers"));
            });

            modelBuilder.Entity<WeekPlan>(entity =>
            {
                entity.ToTable("WeekPlans");

                // Primary Key
                entity.HasKey(e => e.WeekPlanId)
                      .HasName("PK__WeekPlan__3214EC07B80351BE");

                entity.Property(e => e.WeekPlanId)
                      .HasColumnName("WeekPlanId")
                      .ValueGeneratedOnAdd();

                // Properties
                entity.Property(e => e.SubjectId)
                      .HasColumnName("SubjectId");

                entity.Property(e => e.ModuleName)
                      .HasColumnName("ModuleName")
                      .HasMaxLength(9)
                      .IsUnicode(true);

                entity.Property(e => e.WeekNumber)
                      .HasColumnName("WeekNumber")
                      .IsRequired();

                entity.Property(e => e.DraftTeamId)
                      .HasColumnName("DraftTeamId");

                entity.Property(e => e.DraftTitle)
                      .HasColumnName("DraftTitle")
                      .IsUnicode(true);

                // Relationships
                entity.HasMany(e => e.DaysWeekPlans)
                      .WithOne(dwp => dwp.WeekPlan)
                      .HasForeignKey(dwp => dwp.WeekPlanId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Subject)
                      .WithMany(s => s.WeekPlans)
                      .HasForeignKey(e => e.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.DraftTeam)
                      .WithMany(t => t.WeekPlanDrafts)
                      .HasForeignKey(e => e.DraftTeamId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Students)
                      .WithMany(s => s.WeekPlans)
                      .UsingEntity(j => j.ToTable("StudentsWeekPlans"));
            });
        }
    }
}
