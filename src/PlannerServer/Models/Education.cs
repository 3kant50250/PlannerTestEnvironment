namespace PlannerServer.Models
{
    public partial class Education
    {
        public int EducationId { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<StudentEnrollment>? StudentEnrollments { get; set; }
    }
}
