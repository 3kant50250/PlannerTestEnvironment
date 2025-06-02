using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class WeekPlan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WeekPlan()
        {
            DaysWeekPlans = new HashSet<DaysWeekPlan>();
            Students = new HashSet<Student>();
        }

        public int WeekPlanId { get; set; }

        public int? SubjectId { get; set; }

        [StringLength(9)]
        public string ModuleName { get; set; }

        public int WeekNumber { get; set; }

        public int? DraftTeamId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DaysWeekPlan> DaysWeekPlans { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Team DraftTeam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }

        public string DraftTitle { get; set; }

        public virtual WeekPlan DeepClone()
        {
            WeekPlan weekPlan = new WeekPlan()
            {
                ModuleName = String.Copy(ModuleName),
                WeekNumber = WeekNumber,
                Subject = Subject,
                DraftTitle = DraftTitle
            };

            List<DaysWeekPlan> daysWeekPlans = new List<DaysWeekPlan>();
            daysWeekPlans.Add(DaysWeekPlans.SingleOrDefault(dwp => dwp.DayOfWeek == "Monday").Clone());
            daysWeekPlans.Add(DaysWeekPlans.SingleOrDefault(dwp => dwp.DayOfWeek == "Tuesday").Clone());
            daysWeekPlans.Add(DaysWeekPlans.SingleOrDefault(dwp => dwp.DayOfWeek == "Wednesday").Clone());
            daysWeekPlans.Add(DaysWeekPlans.SingleOrDefault(dwp => dwp.DayOfWeek == "Thursday").Clone());
            daysWeekPlans.Add(DaysWeekPlans.SingleOrDefault(dwp => dwp.DayOfWeek == "Friday").Clone());
            weekPlan.DaysWeekPlans = daysWeekPlans;

            return weekPlan;
        }
    }
}
