using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class DaysWeekPlan
    {
        public int DaysWeekPlanId { get; set; }

        [Required]
        [StringLength(10)]
        public string DayOfWeek { get; set; }

        public string Purpose { get; set; }

        public string LearningGoal { get; set; }

        public string Content { get; set; }

        public string Materials { get; set; }

        public int WeekPlanId { get; set; }

        public virtual WeekPlan WeekPlan { get; set; }

        public virtual DaysWeekPlan Clone()
        {
            return new DaysWeekPlan()
            {
                DayOfWeek = DayOfWeek is null ? null : String.Copy(DayOfWeek),
                Purpose = Purpose is null ? null : String.Copy(Purpose),
                LearningGoal = LearningGoal is null ? null : String.Copy(LearningGoal),
                Content = Content is null ? null : String.Copy(Content),
                Materials = Materials is null ? null : String.Copy(Materials)
            };
        }
    }
}
