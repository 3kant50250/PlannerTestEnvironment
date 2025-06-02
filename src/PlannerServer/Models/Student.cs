using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class Student
    {
        public Student()
        {
            FormativeEvaluations = new HashSet<FormativeEvaluation>();
            SummativeEvaluations = new HashSet<SummativeEvaluation>();
            TeamStudents = new HashSet<TeamStudent>();
            DayPlans = new HashSet<DayPlan>();
            WeekPlans = new HashSet<WeekPlan>();
            PlannedActivities = new HashSet<PlannedActivity>();
        }

        //public bool IsAspIN => this.IsAspIN();

        //public bool IsEnrolled => this.IsActiveEnrolled();

        //public string SemesterText => !IsEnrolled ? "0. sem" : this.GetCurrentSemester().ToString() + ". sem.";

        //public string NameSemesterText => $"{Name}, {SemesterText}";

        //public string NameSemesterDepartmentText => $"{NameSemesterText}, {Department.Name}";

        public int StudentId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Unilogin { get; set; }

        public int DepartmentId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime GraduationDate { get; set; }

        public int MunicipalityId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthdate { get; set; }

        [StringLength(4)]
        public string SerialNumber { get; set; }

        public int? IdFromUms { get; set; }

        [StringLength(256)]
        public string UmsActivity { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<FormativeEvaluation> FormativeEvaluations { get; set; }

        public virtual Municipality Municipality { get; set; }

        public virtual ICollection<SummativeEvaluation> SummativeEvaluations { get; set; }

        public virtual ICollection<DayPlan> DayPlans { get; set; }

        public virtual ICollection<WeekPlan> WeekPlans { get; set; }

        public virtual ICollection<TeamStudent> TeamStudents { get; set; }

        public virtual ICollection<PlannedActivity> PlannedActivities { get; set; }
        public virtual ContactInformation? ContactInformation { get; set; }
        public virtual ICollection<StudentEnrollment>? StudentEnrollments { get; set; }
        public virtual ICollection<StudentGraduation>? StudentGraduations { get; set; }
        public virtual ICollection<GrantInformation>? GrantInformations { get; set; }
    }

    public enum EnrollmentStatus
    {
        Default,
        Enrolled,       // Indskrevet
        Clarification,  // Afklaring
        Internship,     // Uddannelsespraktik
        Left,
        InconsistentData
    }
}
