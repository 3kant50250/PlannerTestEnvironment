using PlannerServer.Models;
using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Model
{
    public partial class User
    {
        public User()
        {
            FormativeEvaluations = new HashSet<FormativeEvaluation>();
            SummativeEvaluations = new HashSet<SummativeEvaluation>();
            Teams = new HashSet<Team>();
            TeamStudents = new HashSet<TeamStudent>();
            FavoriteTeams = new HashSet<Team>();
        }

        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(5)]
        public string Initials { get; set; }

        public bool IsActive { get; set; }

        public int Level { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<FormativeEvaluation> FormativeEvaluations { get; set; }

        public virtual ICollection<SummativeEvaluation> SummativeEvaluations { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Team> FavoriteTeams { get; set; }

        public virtual ICollection<TeamStudent> TeamStudents { get; set; }
    }
}
