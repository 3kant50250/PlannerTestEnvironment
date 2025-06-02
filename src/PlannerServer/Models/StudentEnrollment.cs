namespace PlannerServer.Models
{
    public partial class StudentEnrollment
    {
        public int StudentEnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int EnrollmentKindId { get; set; }
        public int EducationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Student? Student { get; set; }
        public virtual EnrollmentKind? EnrollmentKind { get; set; }
        public virtual Education? Education { get; set; }
    }
}
