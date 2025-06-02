namespace PlannerServer.Models
{
    public partial class EnrollmentKind
    {
        public int EnrollmentKindId { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<StudentEnrollment>? StudentEnrollments { get; set; }
    }
}
