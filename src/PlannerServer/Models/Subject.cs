using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class Subject
    {
        public Subject()
        {
            DayPlans = new HashSet<DayPlan>();
            Teams = new HashSet<Team>();
            WeekPlans = new HashSet<WeekPlan>();
            FirstPrioritySubjectFormativeEvaluations = new HashSet<FormativeEvaluation>();
            SecondPrioritySubjectFormativeEvaluations = new HashSet<FormativeEvaluation>();
            ThirdPrioritySubjectFormativeEvaluations = new HashSet<FormativeEvaluation>();
            PlannedActivities = new HashSet<PlannedActivity>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string StandardSubjectDescription { get; set; }

        public virtual ICollection<DayPlan> DayPlans { get; set; }

        public virtual ICollection<FormativeEvaluation> FirstPrioritySubjectFormativeEvaluations { get; set; }

        public virtual ICollection<FormativeEvaluation> SecondPrioritySubjectFormativeEvaluations { get; set; }

        public virtual ICollection<FormativeEvaluation> ThirdPrioritySubjectFormativeEvaluations { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<WeekPlan> WeekPlans { get; set; }

        public virtual ICollection<PlannedActivity> PlannedActivities { get; set; }

        public override string ToString() => Name;

        public static string GetShortName(string name)
        {
            switch (name)
            {
                case "": return String.Empty;
                case "Afklaring": return "AFK";
                case "AspIT8000": return "8000";
                case "AspitLab": return "LAB";
                case "AspitLab V": return "LABV";
                case "AspitLab S": return "LABS";
                case "AspitLab T": return "LABT";
                case "Security": return "CS";
                case "Praktik": return "PRAK";
                case "Praktikant":
                case "Uddannelsespraktik": return "UP";
                case "V3.1 web": return "V3.1";
                case "V3.2 cms": return "V3.2";
                case "S4.1 DS": return "S4.1";
                case "S4.2 AD": return "S4.2";
                case "AspIN": return "IN";
                case "S1":
                case "S2":
                case "S3":
                case "T1":
                case "T2":
                case "T3":
                case "V1":
                case "V2":
                case "QA": return name;
                default: return "N/A";
            }
        }
    }
}
