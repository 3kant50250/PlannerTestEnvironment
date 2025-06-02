using PlannerServer.Model;
using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class Team //: IComparable<Team>
    {
        public Team()
        {
            WeekPlanDrafts = new HashSet<WeekPlan>();
            TeamStudents = new HashSet<TeamStudent>();
            FavoriteUsers = new HashSet<User>();
        }

        public int TeamId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Module { get; set; }

        public int SubjectId { get; set; }

        public int UserId { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual User Teacher { get; set; }

        public virtual ICollection<WeekPlan> WeekPlanDrafts { get; set; }

        public virtual ICollection<TeamStudent> TeamStudents { get; set; }

        public virtual ICollection<User> FavoriteUsers { get; set; }

        //public int CompareTo(Team other)
        //{
        //    if (other is null) throw new ArgumentNullException(nameof(other));
        //    ModulePeriod thisModule = (ModulePeriod)Module;
        //    ModulePeriod otherModule = (ModulePeriod)other.Module;
        //    return thisModule.CompareTo(otherModule);
        //}

        public virtual void Add(Student student) => Add(new List<Student> { student });

        public virtual void Remove(Student student) => Remove(new List<Student> { student });

        public virtual void Add(IEnumerable<Student> students)
        {
            foreach (var student in students)
                TeamStudents.Add(new TeamStudent() { Team = this, Student = student });
        }

        public virtual void Remove(IEnumerable<Student> students)
        {
            foreach (var student in students)
            {
                var ts = TeamStudents.SingleOrDefault(ts => ts.Student.Equals(student));
                TeamStudents.Remove(ts);
            }
        }
    }
}
