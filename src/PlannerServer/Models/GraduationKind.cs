namespace PlannerServer.Models
{
    public partial class GraduationKind
    {
        public int GraduationKindId { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<StudentGraduation>? StudentGraduations { get; set; }
    }
}
