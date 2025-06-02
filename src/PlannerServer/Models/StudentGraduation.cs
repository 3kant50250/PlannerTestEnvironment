namespace PlannerServer.Models
{
    public partial class StudentGraduation
    {
        public int StudentGraduationId { get; set; }
        public int StudentId { get; set; }
        public int GraduationKindId { get; set; }
        public string Note { get; set; } = string.Empty;

        public virtual Student? Student { get; set; }
        public virtual GraduationKind? GraduationKind { get; set; }
    }
}
