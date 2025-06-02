using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public class Grade : IEquatable<Grade>
    {
        public Grade()
        {
            TeamStudents = new HashSet<TeamStudent>();
        }

        public int GradeId { get; set; }

        [Required]
        [StringLength(50)]
        public string GradeValue { get; set; }

        public virtual ICollection<TeamStudent> TeamStudents { get; set; }

        public override string ToString() => GradeValue;

        public bool Equals(Grade other) => other != null && this.GradeId == other.GradeId;
        public override bool Equals(object obj) => Equals(obj as Grade);

        public static bool operator ==(Grade first, Grade second) => first is null && second is null ? true : first.Equals(second);
        public static bool operator !=(Grade first, Grade second) => !(first == second);
    }
}
